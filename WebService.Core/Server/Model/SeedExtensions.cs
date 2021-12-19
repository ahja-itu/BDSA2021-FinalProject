// ***********************************************************************
// Assembly         : WebService.Core.Server
// Author           : Group BTG
// Created          : 12-10-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="SeedExtensions.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.VisualBasic.FileIO;

namespace WebService.Core.Server.Model;

/// Idea taken from https://github.com/ondfisk/BDSA2021/blob/3fe02352710a920bfb874ed1b219d273a26a92d2/MyApp.Server/Model/SeedExtensions.cs#L3
/// Thank you, OndFisk
/// <summary>
///     Class SeedExtensions.
/// </summary>
public static class SeedExtensions
{
    /// <summary>
    ///     Removes all items from the database and reseeds it with randomly generated materials from specific tags saved in
    ///     the data files found in the data directory when given a host object containing IoT containers such as the DbContext and repositories
    /// </summary>
    public static async Task SeedAsync(this IHost host, IWebHostEnvironment environment)
    {
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IContext>();

        var contentGenerator = new ContentGenerator(environment);

        var languageRepository = scope.ServiceProvider.GetRequiredService<ILanguageRepository>();
        var levelRepository = scope.ServiceProvider.GetRequiredService<ILevelRepository>();
        var mediaRepository = scope.ServiceProvider.GetRequiredService<IMediaRepository>();
        var plRepository = scope.ServiceProvider.GetRequiredService<IProgrammingLanguageRepository>();
        var tagRepository = scope.ServiceProvider.GetRequiredService<ITagRepository>();
        var materialRepository = scope.ServiceProvider.GetRequiredService<IMaterialRepository>();

        var repos = new Dictionary<RepoType, IRepository>
        {
            {RepoType.Tag, tagRepository},
            {RepoType.Level, levelRepository},
            {RepoType.Language, languageRepository},
            {RepoType.ProgrammingLanguage, plRepository},
            {RepoType.Media, mediaRepository},
            {RepoType.Material, materialRepository}
        };

        await CleanDB(context);

        await SeedLanguagesAsync(languageRepository);
        await SeedLevelsAsync(levelRepository);
        await SeedMediaAsync(mediaRepository);
        await SeedProgrammingLanguagesAsync(plRepository);
        await SeedTagsAsync(tagRepository);
        await SeedMaterial(repos, contentGenerator);
    }

    /// <summary>
    ///     Cleans the database.
    /// </summary>
    private static async Task CleanDB(IContext context)
    {
        context.Languages.RemoveRange(context.Languages);
        context.Levels.RemoveRange(context.Levels);
        context.Materials.RemoveRange(context.Materials);
        context.Medias.RemoveRange(context.Medias);
        context.ProgrammingLanguages.RemoveRange(context.ProgrammingLanguages);
        context.Tags.RemoveRange(context.Tags);
        await context.SaveChangesAsync();
    }

    /// <summary>
    ///     Seeds all the material in the different repositories using a content generator>.
    /// </summary>
    private static async Task SeedMaterial(Dictionary<RepoType, IRepository> repos, ContentGenerator contentGenerator)
    {
        var tagRepo = GetRepo<ITagRepository>(repos, RepoType.Tag);
        var languageRepository = GetRepo<ILanguageRepository>(repos, RepoType.Language);
        var levelRepository = GetRepo<ILevelRepository>(repos, RepoType.Level);
        var mediaRepository = GetRepo<IMediaRepository>(repos, RepoType.Media);
        var plRepository = GetRepo<IProgrammingLanguageRepository>(repos, RepoType.ProgrammingLanguage);
        var materialRepository = GetRepo<IMaterialRepository>(repos, RepoType.Material);

        var rand = new Random();
        var authors = LoadAuthors();
        var names = LoadNames();

        var tags = await tagRepo.ReadAsync();
        var level = await levelRepository.ReadAsync();
        var programmingLanguages = await plRepository.ReadAsync();
        var medias = await mediaRepository.ReadAsync();
        var languages = await languageRepository.ReadAsync();

        // Lets create a material for each author
        foreach (var author in authors)
        {
            var tagCount = 1 + rand.Next(4); // number 1 -> 4
            var ratingsCount = rand.Next(21); // 0 -> 20

            // ratings, weighted tags, author are owned properties of a material,
            // so we need to create them on a per material basis

            var weightedTags = GetNRandomEntries(tags, tagCount)
                .Select(t => new CreateWeightedTagDTO(t.Name, rand.Next(101)))
                .ToList();

            var ratings = GetNRandomEntries(names, ratingsCount)
                .Select(name => new CreateRatingDTO(rand.Next(11), name))
                .ToList();

            var assignedLevel = GetNRandomEntries<CreateLevelDTO>(level, 1);

            var assignedPl = GetNRandomEntries<CreateProgrammingLanguageDTO>(programmingLanguages, 1);

            var assignedMediaType = GetNRandomEntries<CreateMediaDTO>(medias, 1);

            var assignedLanguage = GetSingleRandomEntry<CreateLanguageDTO>(languages);

            // Visual Studio complains later that assignedLanguage might be null, so lets ensure it wont be
            if (assignedLanguage == null)
            {
                var safetyCounter = 0;
                while (assignedLanguage == null || safetyCounter++ < 100)
                    assignedLanguage = GetSingleRandomEntry<CreateLanguageDTO>(languages);

                if (safetyCounter >= 100)
                    // We must have exited the while loop above due to the safetyCounter
                    // reaching its limit. 
                    continue;
            }

            var (convertOk, lang) = ContentGenerator.StringToLanguage(assignedLanguage.Name);
            if (!convertOk)
                Console.WriteLine(
                    "Could not convert given language for the material to the ENUM representation. Skipping this material");


            var (contentOk, content) = contentGenerator.GenerateText(lang, 100 + rand.Next(300));
            if (!contentOk)
            {
                Console.WriteLine("Failed to generate text for a material. Skipping this material.");
                continue;
            }

            var summaryWords = content.Split(' ').Take(30).ToList();
            var summary = string.Join(' ', summaryWords) + "...";
            const string url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

            // Ensure title will be created
            var (titleOk, title) = ContentGenerator.GenerateTitle(weightedTags);
            if (!titleOk)
            {
                Console.WriteLine("Failed to generate title with given input. Skipping this material.");
                continue;
            }

            var authorsList = new List<CreateAuthorDTO> {new(author.FirstName, author.SurName)};

            // Created at can be within the last 5 years
            var createdAt = DateTime.Now.AddMinutes(-rand.NextDouble() * 525_948 * 5);

            var material = new CreateMaterialDTO(weightedTags,
                ratings,
                assignedLevel,
                assignedPl,
                assignedMediaType,
                assignedLanguage,
                summary,
                url,
                content,
                title,
                authorsList,
                createdAt);

            await materialRepository.CreateAsync(material);
        }
    }

    /// <summary>
    ///     Loads the  list of names from the local file names.csv
    /// </summary>
    private static List<string> LoadNames()
    {
        return ReadCSV("names.csv", 2)
            .Select(fields => fields[1])
            .ToList();
    }

    /// <summary>
    ///     Loads the names of authors from the local file authors.csv and generates new Authors.
    /// </summary>
    private static List<Author> LoadAuthors()
    {
        return ReadCSV("authors.csv", 2)
            .Select(fields => fields[1].Split(' '))
            .Select(fields => new Author(fields[0], fields[1]))
            .ToList();
    }

    /// <summary>
    ///     Seed languages as an asynchronous operation based on the list of languages in the local file languages.csv.
    /// </summary>
    private static async Task SeedLanguagesAsync(ILanguageRepository repo)
    {
        foreach (var fields in ReadCSV("languages.csv", 2))
        {
            var name = fields[1];

            var language = new CreateLanguageDTO(name);
            await repo.CreateAsync(language);
        }
    }

    /// <summary>
    ///     Seed levels as an asynchronous operation based on the list of levels in the local file levels.csv.
    /// </summary>
    private static async Task SeedLevelsAsync(ILevelRepository repo)
    {
        foreach (var fields in ReadCSV("levels.csv", 2))
        {
            var name = fields[1];

            var level = new CreateLevelDTO(name);
            await repo.CreateAsync(level);
        }
    }


    /// <summary>
    ///     Seed media as an asynchronous operation based on the list of media types in the local file media.csv.
    /// </summary>
    private static async Task SeedMediaAsync(IMediaRepository repo)
    {
        foreach (var fields in ReadCSV("media.csv", 2))
        {
            var type = fields[1];

            var media = new CreateMediaDTO(type);
            await repo.CreateAsync(media);
        }
    }

    /// <summary>
    ///     Seed programming languages as an asynchronous operation based on the list of programming languages in the local file programminglanguages.csv.
    /// </summary>
    private static async Task SeedProgrammingLanguagesAsync(IProgrammingLanguageRepository repo)
    {
        // ReSharper disable once StringLiteralTypo
        foreach (var fields in ReadCSV("programminglanguages.csv", 1))
        {
            var name = fields[0];

            var pl = new CreateProgrammingLanguageDTO(name);
            await repo.CreateAsync(pl);
        }
    }

    /// <summary>
    ///     Seed tags as an asynchronous operation based on the list of tags in the local file tags.csv.
    /// </summary>
    private static async Task SeedTagsAsync(ITagRepository repo)
    {
        foreach (var fields in ReadCSV("tags.csv", 1))
        {
            var name = fields[0];

            var tag = new CreateTagDTO(name);
            await repo.CreateAsync(tag);
        }
    }

    /// <summary>
    ///     Gets n random entries of the type T and returns them as an IColletion.
    /// </summary>
    private static ICollection<T> GetNRandomEntries<T>(IEnumerable<T> entries, int n)
    {
        return entries.OrderBy(_ => Guid.NewGuid()).Take(n).ToList();
    }

    /// <summary>
    ///     Gets a single random entry of type T from IEnumerable.
    /// </summary>
    private static T? GetSingleRandomEntry<T>(IEnumerable<T> entries)
    {
        return entries.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
    }

    /// <summary>
    ///     Gets a repository based on type from the dictionary mapping repository type to repository.
    /// </summary>
    private static T GetRepo<T>(Dictionary<RepoType, IRepository> repos, RepoType type)
    {
        return repos.Where(kv => kv.Key == type).Select(kv => (T) kv.Value).First();
    }

    /// <summary>
    ///     Reads a given .CSV file with a given number of fields and returns them as a IEnumerable of string arrays.
    /// </summary>
    private static IEnumerable<string[]> ReadCSV(string filename, uint fieldCount)
    {
        using var csvParser = new TextFieldParser(GetDataFileLocation(filename));
        csvParser.CommentTokens = new[] {"#"};
        csvParser.SetDelimiters(",");
        csvParser.HasFieldsEnclosedInQuotes = false;

        csvParser.ReadLine();
        while (!csvParser.EndOfData)
        {
            var fields = csvParser.ReadFields() ?? new string[fieldCount];
            if (IsAllNonEmpty(fields)) yield return fields;
        }
    }

    /// <summary>
    ///     Determines whether all the fields in a given string array are non.empty and returns false otherwise.
    /// </summary>
    private static bool IsAllNonEmpty(params string[] fields)
    {
        return fields.All(field => field.Trim().Length > 0);
    }

    /// <summary>
    ///     Gets the location of a file with the given filename.
    /// </summary>
    private static string GetDataFileLocation(string filename)
    {
        return $"{Directory.GetCurrentDirectory()}/../../data/{filename}";
    }

    /// <summary>
    ///     Enum with the available repository types.
    /// </summary>
    private enum RepoType
    {
        Language,
        Level,
        Media,
        ProgrammingLanguage,
        Tag,
        Material
    }
}