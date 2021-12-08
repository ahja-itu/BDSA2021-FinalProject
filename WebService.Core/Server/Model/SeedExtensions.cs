using WebService.Entities;
using Microsoft.VisualBasic.FileIO;

namespace WebService.Core.Server.Model
{

    /**
     *  Idea taken from https://github.com/ondfisk/BDSA2021/blob/3fe02352710a920bfb874ed1b219d273a26a92d2/MyApp.Server/Model/SeedExtensions.cs#L3
     *  Thank you, Ondfisk
     */
    public static class SeedExtensions
    {
        private enum RepoType {
            LANGUAGE,
            LEVEL,
            MEDIA,
            PROGRAMMINGLANGUAGE,
            TAG,
            MATERIAL
        }

        public static async Task<IHost> SeedAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IContext>();

                var languageRepository = scope.ServiceProvider.GetRequiredService<ILanguageRepository>();
                var levelRepository = scope.ServiceProvider.GetRequiredService<ILevelRespository>();
                var mediaRepository = scope.ServiceProvider.GetRequiredService<IMediaRepository>();
                var plRepository = scope.ServiceProvider.GetRequiredService<IProgrammingLanguageRespository>();
                var tagRepository = scope.ServiceProvider.GetRequiredService<ITagRepository>();
                var materialRepository = scope.ServiceProvider.GetRequiredService<IMaterialRepository>();

                Dictionary<RepoType, IRepository> repos = new Dictionary<RepoType, IRepository>();
                repos.Add(RepoType.TAG, tagRepository);
                repos.Add(RepoType.LEVEL, levelRepository);
                repos.Add(RepoType.LANGUAGE, languageRepository);
                repos.Add(RepoType.PROGRAMMINGLANGUAGE, plRepository);
                repos.Add(RepoType.MEDIA, mediaRepository);
                repos.Add(RepoType.MATERIAL, materialRepository);

                CleanDB(context).Wait();

                await SeedLanguagesAsync(languageRepository);
                await SeedLevelsAsync(levelRepository);
                await SeedMediaAsync(mediaRepository);
                await SeedProgrmamingLanguages(plRepository);
                await SeedTags(tagRepository);
                await SeedMaterial(repos);
            }

            return host;
        }

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

        private static async Task SeedMaterial(Dictionary<RepoType, IRepository> repos)
        {
            var tagRepo = GetRepo<ITagRepository>(repos, RepoType.TAG);
            var languageRepository = GetRepo<ILanguageRepository>(repos, RepoType.LANGUAGE);
            var levelRepository = GetRepo<ILevelRespository>(repos, RepoType.LEVEL);
            var mediaRepository = GetRepo<IMediaRepository>(repos, RepoType.MEDIA);
            var plRepository = GetRepo<IProgrammingLanguageRespository>(repos, RepoType.PROGRAMMINGLANGUAGE);
            var materialRepository = GetRepo<IMaterialRepository>(repos, RepoType.MATERIAL);

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
                var tagCount = rand.Next(5); // number 1 -> 4
                var ratingsCount = rand.Next(21) - 1; // 0 -> 20

                // ratings, weighted tags, author are owned properties of a material,
                // so we need to create them on a per material basis

                var weightedTags = GetNRandomEntries<TagDTO>(tags, tagCount)
                    .Select(t => new CreateWeightedTagDTO(t.Name, rand.Next(101)))
                    .ToList();

                var ratings = GetNRandomEntries<string>(names, ratingsCount)
                    .Select(name => new CreateRatingDTO(rand.Next(11), name))
                    .ToList();

                var assignedLevel = GetNRandomEntries<CreateLevelDTO>(level, 1);

                var assignedPl = GetNRandomEntries<CreateProgrammingLanguageDTO>(programmingLanguages, 1);

                var assignedMediaType = GetNRandomEntries<CreateMediaDTO>(medias, 1);

                var assignedLanguage = GetSingleRandomEntry<CreateLanguageDTO>(languages);

                // Visual Studio complains later that assignedLanguage might be null, so lets ensure it wont be
                if (assignedLanguage == null)
                {
                    int safetyCounter = 0;
                    while(assignedLanguage == null || safetyCounter++ < 100)
                    {
                        assignedLanguage = GetSingleRandomEntry<CreateLanguageDTO>(languages);
                    }

                    if(safetyCounter >= 100)
                    {
                        // We must have exited the while loop above due to the safetyCounter
                        // reaching its limit. 
                        continue;
                    }
                }

                // Generate content and summary
                var theRandomNumber = rand.Next(1000);
                var summary = $"This person generated a random number, and you wont believe what number they generated!";
                var content = $"The number that was generated was \"{theRandomNumber}\"";
                var url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
                var title = $"Title:{Guid.NewGuid()}";

                var authorsList = new List<CreateAuthorDTO>();
                authorsList.Add(new CreateAuthorDTO(author.FirstName, author.SurName));

                // Created at can be within the last 5 years
                var createdAt = System.DateTime.Now.AddMinutes(-rand.NextDouble() * 525_948 * 5);

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
        
        private static List<string> LoadNames()
            => ReadAllFields("names.csv", 2).Select(f => f[1]).ToList();
        

        private static List<Author> LoadAuthors()
        {
            var authors = new List<Author>();

            foreach (var fields in ReadAllFields("authors.csv", 2))
            {
                string firstName = fields[0];
                string surName = fields[1];
                Console.WriteLine($"I'm writing a new author: {firstName} {surName}");
                authors.Add(new Author(firstName, surName));
            }

            return authors;
        }

        private static async Task SeedLanguagesAsync(ILanguageRepository repo)
        {
            foreach (var fields in ReadAllFields("languages.csv", 2))
            {
                string name = fields[1];

                var language = new CreateLanguageDTO(name);
                await repo.CreateAsync(language);
            }
        }

        private static async Task SeedLevelsAsync(ILevelRespository repo)
        {
            foreach (var fields in ReadAllFields("levels.csv", 2))
            {
                    string name = fields[1];

                    var level = new CreateLevelDTO(name);
                    await repo.CreateAsync(level);
            }               
        }


        private static async Task SeedMediaAsync(IMediaRepository repo)
        {
            foreach(var fields in ReadAllFields("media.csv", 2))
            {
                string type = fields[1];

                var media = new CreateMediaDTO(type);
                await repo.CreateAsync(media);
            }
        }

        private static async Task SeedProgrmamingLanguages(IProgrammingLanguageRespository repo)
        {
            foreach (var fields in ReadAllFields("programminglanguages.csv", 1))
            {
                string name = fields[0];

                var pl = new CreateProgrammingLanguageDTO(name);
                await repo.CreateAsync(pl);
            }
        }

        private static async Task SeedTags(ITagRepository repo)
        {
            foreach(var fields in ReadAllFields("tags.csv", 1))
            {
                string name = fields[0];

                var tag = new CreateTagDTO(name);
                await repo.CreateAsync(tag);
            }
        }

        private static ICollection<T> GetNRandomEntries<T>(IReadOnlyCollection<T> entries, int n)
            => entries.OrderBy(e => Guid.NewGuid()).Take(n).ToList();

        private static T? GetSingleRandomEntry<T>(IReadOnlyCollection<T> entries)
            => entries.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

        private static T GetRepo<T>(Dictionary<RepoType, IRepository> repos, RepoType type)
            => repos.Where(kv => kv.Key == type).Select(kv => (T)kv.Value).First();
 

        private static IEnumerable<string[]> ReadAllFields(string filename, uint fieldCount)
        {
            Console.WriteLine("Going to read all fields...");
            foreach(var (ok, fields) in ReadFields(filename, fieldCount))
            {
                if (ok)
                {
                    yield return fields;
                }
            }
        }

        private static IEnumerable<(bool, string[])> ReadFields(String filename, uint fieldCount)
        {
            using TextFieldParser csvParser = new TextFieldParser(GetDataFileLocation(filename));
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = false;

            csvParser.ReadLine();
            while(!csvParser.EndOfData)
            {
                var fields = csvParser.ReadFields() ?? new string[fieldCount];
                yield return (IsAllNonEmpty(fields), fields);
            }
            
        }

        private static bool IsAllNonEmpty(params string[] fields)
            => fields.All(field => field != null && field.Trim().Length > 0);

        private static TextFieldParser GetParser(string filename)
        {
            // Thanks to https://stackoverflow.com/a/33796861 for showing how to read csv's
            using TextFieldParser csvParser = new TextFieldParser(GetDataFileLocation(filename));
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = false;

            csvParser.ReadLine();
            return csvParser;
        }

        private static string GetDataFileLocation(string filename)
        {
            return $"{Directory.GetCurrentDirectory()}\\..\\..\\data\\{filename}";
        }
    }
}
