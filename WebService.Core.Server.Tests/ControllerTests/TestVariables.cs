// ***********************************************************************
// Assembly         : WebService.Core.Server.Tests
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : thorl
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="TestVariables.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace WebService.Core.Server.Tests.ControllerTests;

/// <summary>Class TestVariables.</summary>
internal class TestVariables
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="T:WebService.Core.Server.Tests.ControllerTests.TestVariables" />
    ///     class.
    /// </summary>
    public TestVariables()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<Context>();
        builder.UseSqlite(connection);
        var context = new Context(builder.Options);
        context.Database.EnsureCreated();

        var language1 = new Language(1, "Danish");
        var language2 = new Language(2, "English");
        var language3 = new Language(3, "Swedish");

        var level1 = new Level(1, "Bachelor");
        var level2 = new Level(2, "Master");
        var level3 = new Level(3, "PHD");

        var media1 = new Media(1, "Book");
        var media2 = new Media(2, "Report");
        var media3 = new Media(3, "Video");

        var programmingLanguage1 = new ProgrammingLanguage(1, "C#");
        var programmingLanguage2 = new ProgrammingLanguage(2, "C++");
        var programmingLanguage3 = new ProgrammingLanguage(3, "F#");


        var rating1 = new Rating(2, "Rasmus");
        var rating2 = new Rating(5, "Kim");
        var rating3 = new Rating(9, "Poul");

        var weightedTag1 = new WeightedTag("SOLID", 10);
        var weightedTag2 = new WeightedTag("RAD", 50);
        var weightedTag3 = new WeightedTag("API", 90);

        var tag1 = new Tag(1, "SOLID");
        var tag2 = new Tag(2, "RAD");
        var tag3 = new Tag(3, "API");

        const string summary1 = "I am material 1";
        const string summary2 = "I am material 2";
        const string summary3 = "I am material 3";

        const string url1 = "url1.com";
        const string url2 = "url2.com";
        const string url3 = "url3.com";

        var author1 = new Author("Rasmus", "Kristensen");
        var author2 = new Author("Alex", "Su");
        var author3 = new Author("Thor", "Lind");

        const string content1 = "null";
        const string content2 = "null";
        const string content3 = "null";

        var material1 = new Material(
            new List<WeightedTag> {weightedTag1},
            new List<Rating> {rating1},
            new List<Level> {level1},
            new List<ProgrammingLanguage> {programmingLanguage1},
            new List<Media> {media1},
            language1,
            summary1,
            url1,
            content1,
            "Material 1",
            new List<Author> {author1},
            DateTime.UtcNow
        );
        var material2 = new Material
        (
            new List<WeightedTag> {weightedTag3, weightedTag1},
            new List<Rating> {rating3, rating1},
            new List<Level> {level3, level1},
            new List<ProgrammingLanguage> {programmingLanguage3, programmingLanguage1},
            new List<Media> {media3, media1},
            language3,
            summary2,
            url2,
            content3,
            "Material 2",
            new List<Author> {author3, author1},
            DateTime.UtcNow.AddYears(-11).AddDays(10)
        );

        var material3 = new Material(
            new List<WeightedTag> {weightedTag3, weightedTag2, weightedTag1},
            new List<Rating> {rating3, rating2, rating1},
            new List<Level> {level3, level2, level1},
            new List<ProgrammingLanguage> {programmingLanguage3, programmingLanguage2, programmingLanguage1},
            new List<Media> {media3, media2, media1},
            language3,
            summary3,
            url3,
            content2,
            "Material 3",
            new List<Author> {author3, author2, author1},
            DateTime.UtcNow
        );

        context.AddRange(language1, language2, language3, level1, level2, level3, media1, media2, media3,
            programmingLanguage1, programmingLanguage2, programmingLanguage3, rating1, rating2, rating3, tag1, tag2,
            tag3, material1, material2, material3);

        context.SaveChanges();

            Context = context;

            LanguageRepository = new LanguageRepository(context);
            LevelRepository = new LevelRepository(context);
            MaterialRepository = new MaterialRepository(context);
            MediaRepository = new MediaRepository(context);
            ProgrammingLanguageRepository = new ProgrammingLanguageRepository(context);
            TagRepository = new TagRepository(context);
            SearchAlgorithm = new SearchAlgorithm(MaterialRepository, TagRepository);
        }
        public IContext Context { get; }
        public LanguageRepository LanguageRepository { get; }
        public LevelRepository LevelRepository { get; }
        public MaterialRepository MaterialRepository { get; }
        public MediaRepository MediaRepository { get; }
        public ProgrammingLanguageRepository ProgrammingLanguageRepository { get; }
        public TagRepository TagRepository { get; }
        public SearchAlgorithm SearchAlgorithm { get; }
   
}
