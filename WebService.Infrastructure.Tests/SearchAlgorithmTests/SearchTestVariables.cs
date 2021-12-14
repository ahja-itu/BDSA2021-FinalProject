// ***********************************************************************
// Assembly         : WebService.Infrastructure.Tests
// Author           : Group BTG
// Created          : 12-10-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="SearchTestVariables.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;

namespace WebService.Infrastructure.Tests.SearchAlgorithmTests;

/// <summary>
///     Class SearchTestVariables.
/// </summary>
[SuppressMessage("ReSharper", "StringLiteralTypo")]
internal class SearchTestVariables
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SearchTestVariables" /> class.
    /// </summary>
    public SearchTestVariables()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<Context>();
        builder.UseSqlite(connection);
        var context = new Context(builder.Options);
        context.Database.EnsureCreated();

        var danish = new Language(1, "Danish");
        var english = new Language(2, "English");
        var spanish = new Language(3, "Spanish");

        var bachelor = new Level(1, "Bachelor");
        var masters = new Level(2, "Master");
        var phd = new Level(3, "PHD");

        var book = new Media(1, "Book");
        var report = new Media(2, "Report");
        var video = new Media(3, "Video");

        var csharp = new ProgrammingLanguage(1, "C#");
        var java = new ProgrammingLanguage(2, "JAVA");
        var fsharp = new ProgrammingLanguage(3, "F#");

        var materialId = 1;


        #region Tag1-WeightOneTag

        //Varying tag weight - Tag1
        Tag1Materials = new List<Material>();
        for (var i = 1; i <= 10; i++)
        {
            var material1 = new Material() //
            {
                Id = materialId++,
                WeightedTags = new HashSet<WeightedTag> {new("Tag1", i)},
                Ratings = new HashSet<Rating> {new(5, "Reviewer")},
                Levels = new HashSet<Level> {masters},
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> {csharp},
                Medias = new HashSet<Media> {book},
                Language = danish,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "ELI5: Induction Proofs " + i,
                Authors = new HashSet<Author> {new("Writername", "Writernameson")},
                TimeStamp = DateTime.UtcNow
            };
            context.AddRange(material1);
            Tag1Materials.Add(material1);
        }

        #endregion

        #region Tag2-Rating

        //Varying rating - Tag2
        Tag2Materials = new List<Material>();
        for (var i = 0; i <= 10; i++)
        {
            var material2 = new Material() //
            {
                Id = materialId++,
                WeightedTags = new HashSet<WeightedTag> {new("Tag2", 10)},
                Ratings = new HashSet<Rating> {new(i, "reviewer" + i)},
                Levels = new HashSet<Level> {masters},
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
                Medias = new HashSet<Media> {report},
                Language = english,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "Blazor for kids " + i,
                Authors = new HashSet<Author> {new("Writername", "Writernameson")},
                TimeStamp = DateTime.UtcNow
            };
            context.AddRange(material2);
            Tag2Materials.Add(material2);
        }

        #endregion

        #region Tag3-Levels

        //Varying levels - Tag3
        var tag3Levels = new List<HashSet<Level>>
        {
            new() {bachelor},
            new() {masters},
            new() {phd},
            new() {bachelor, masters},
            new() {bachelor, phd},
            new() {masters, phd},
            new() {bachelor, masters, phd}
        };

        Tag3Materials = new List<Material>();
        for (var i = 0; i <= 6; i++)
        {
            var material3 = new Material() //
            {
                Id = materialId++,
                WeightedTags = new HashSet<WeightedTag> {new("Tag3", 10)},
                Ratings = new HashSet<Rating> {new(5, "Reviewer")},
                Levels = tag3Levels[i],
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> {csharp},
                Medias = new HashSet<Media> {book},
                Language = danish,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = ".NET Framework tutorial " + i,
                Authors = new HashSet<Author> {new("Writername", "Writernameson")},
                TimeStamp = DateTime.UtcNow
            };
            context.AddRange(material3);
            Tag3Materials.Add(material3);
        }

        #endregion


        #region Tag4-Languages

        //Varying Languages - Tag4

        Tag4Materials = new List<Material>();

        var material4_1 = new Material
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag4", 10)},
            Ratings = new HashSet<Rating> {new(6, "Reviewer")},
            Levels = new HashSet<Level> {bachelor},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {csharp},
            Medias = new HashSet<Media> {video},
            Language = danish,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "En titel p� dansk",
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = new DateTime(2011, 1, 1)
        };
        context.AddRange(material4_1);
        Tag4Materials.Add(material4_1);

        var material4_2 = new Material
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag4", 10)},
            Ratings = new HashSet<Rating> {new(6, "Reviewer")},
            Levels = new HashSet<Level> {bachelor},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {csharp},
            Medias = new HashSet<Media> {video},
            Language = english,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "A title in English",
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = new DateTime(2011, 1, 1)
        };
        context.AddRange(material4_2);

        Tag4Materials.Add(material4_2);


        var material4_3 = new Material
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag4", 10)},
            Ratings = new HashSet<Rating> {new(6, "Reviewer")},
            Levels = new HashSet<Level> {bachelor},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {csharp},
            Medias = new HashSet<Media> {video},
            Language = spanish,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Un t�tulo en espa�ol",
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = new DateTime(2011, 1, 1)
        };

        context.AddRange(material4_1);
        context.AddRange(material4_2);
        context.AddRange(material4_3);
        Tag4Materials.Add(material4_1);
        Tag4Materials.Add(material4_2);
        Tag4Materials.Add(material4_3);

        #endregion

        #region Tag5-ProgrammingLanguages

        //Varying programming Languages - Tag5
        var tag5PLanguages = new List<HashSet<ProgrammingLanguage>>
        {
            new() {csharp},
            new() {fsharp},
            new() {java},
            new() {csharp, fsharp},
            new() {csharp, java},
            new() {fsharp, java},
            new() {csharp, fsharp, java}
        };

        Tag5Materials = new List<Material>();
        for (var i = 0; i <= 6; i++)
        {
            var material5 = new Material() //
            {
                Id = materialId++,
                WeightedTags = new HashSet<WeightedTag> {new("Tag5", 10)},
                Ratings = new HashSet<Rating> {new(5, "Reviewer")},
                Levels = new HashSet<Level> {bachelor},
                ProgrammingLanguages = tag5PLanguages[i],
                Medias = new HashSet<Media> {book},
                Language = danish,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = ".NET Framework guide " + i,
                Authors = new HashSet<Author> {new("Writername", "Writernameson")},
                TimeStamp = DateTime.UtcNow
            };
            context.AddRange(material5);
            Tag5Materials.Add(material5);
        }

        #endregion


        #region Tag6-Media

        //Varying media - Tag6
        Tag6Materials = new List<Material>();

        var material6_1 = new Material
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag6", 10)},
            Ratings = new HashSet<Rating> {new(4, "Reviewer")},
            Levels = new HashSet<Level> {bachelor},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
            Medias = new HashSet<Media> {book},
            Language = danish,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Dockerize your life",
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = new DateTime(2013, 1, 1)
        };


        var material6_2 = new Material
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag6", 10)},
            Ratings = new HashSet<Rating> {new(4, "Reviewer")},
            Levels = new HashSet<Level> {bachelor},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
            Medias = new HashSet<Media> {report},
            Language = danish,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Dockerize your life II",
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = new DateTime(2013, 1, 1)
        };

        var material6_3 = new Material
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag6", 10)},
            Ratings = new HashSet<Rating> {new(4, "Reviewer")},
            Levels = new HashSet<Level> {bachelor},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
            Medias = new HashSet<Media> {video},
            Language = danish,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Dockerize your life III",
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = new DateTime(2013, 1, 1)
        };


        context.AddRange(material6_1);
        context.AddRange(material6_2);
        context.AddRange(material6_3);

        Tag6Materials.Add(material6_1);
        Tag6Materials.Add(material6_2);
        Tag6Materials.Add(material6_3);

        #endregion


        #region Tag7-Author

        //Varying author - Tag7
        var tag7Authors = new List<HashSet<Author>>
        {
            new() {new Author("Alfa", "Alfason")},
            new() {new Author("Alfa", "Bravoson")},
            new() {new Author("Bravo", "Bravoson")},
            new() {new Author("Alfa", "Alfason"), new Author("Bravo", "Bravoson")}
        };

        Tag7Materials = new List<Material>();
        for (var i = 0; i < 4; i++)
        {
            var material7 = new Material() //
            {
                Id = materialId++,
                WeightedTags = new HashSet<WeightedTag> {new("Tag5", 10)},
                Ratings = new HashSet<Rating> {new(5, "Reviewer")},
                Levels = new HashSet<Level> {bachelor},
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
                Medias = new HashSet<Media> {book},
                Language = danish,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = ".NET Framework intro " + i,
                Authors = tag7Authors[i],
                TimeStamp = DateTime.UtcNow
            };
            context.AddRange(material7);
            Tag7Materials.Add(material7);
        }

        #endregion


        #region Tag8-Timestamp

        //Varying timestamp - Tag8
        Tag8Materials = new List<Material>();
        for (var i = 2014; i < 2022; i++)
        {
            var material8 = new Material() //
            {
                Id = materialId++,
                WeightedTags = new HashSet<WeightedTag> {new("Tag8", 10)},
                Ratings = new HashSet<Rating> {new(5, "Reviewer")},
                Levels = new HashSet<Level> {masters},
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> {csharp},
                Medias = new HashSet<Media> {video},
                Language = english,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "The lastest news about C# " + i,
                Authors = new HashSet<Author> {new("Writername", "Writernameson")},
                TimeStamp = new DateTime(i, 1, 1)
            };
            context.AddRange(material8);
            Tag8Materials.Add(material8);
        }

        #endregion


        #region Tag9-Title

        //Varying title - Tag9
        Tag9Materials = new List<Material>();
        var tag9Titles = new List<string>
        {
            "Lorem ipsum dolor sit amet",
            "Lorem ipsum dolor sit",
            "Lorem ipsum dolor",
            "Lorem ipsum",
            "Lorem",
            "consectetuer adipiscing elit",
            "CONSECTETUER ADIPISCING ELIT"
        };


        foreach (var material9 in tag9Titles.Select(t => new Material() //
                 {
                     Id = materialId++,
                     WeightedTags = new HashSet<WeightedTag> {new("Tag9", 10)},
                     Ratings = new HashSet<Rating> {new(5, "Reviewer")},
                     Levels = new HashSet<Level> {bachelor},
                     ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
                     Medias = new HashSet<Media> {book},
                     Language = danish,
                     Summary = "Lorem ipsum",
                     URL = "iamaURL",
                     Content = "I am content",
                     Title = t,
                     Authors = new HashSet<Author> {new("Writername", "Writernameson")},
                     TimeStamp = DateTime.UtcNow
                 }))
        {
            context.AddRange(material9);
            Tag9Materials.Add(material9);
        }

        #endregion


        #region Tag10Tag11-WeightTwoTags

        //Varying weight, two tags - Tag10, Tag11
        Tag1011Materials = new List<Material>();
        var counter = 0;

        var material1011_1 = new Material() //
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag10", 1), new("Tag11", 1)},
            Ratings = new HashSet<Rating> {new(10, "Reviewer")},
            Levels = new HashSet<Level> {masters},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
            Medias = new HashSet<Media> {report},
            Language = english,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Blazor " + counter++,
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = DateTime.UtcNow
        };

        context.AddRange(material1011_1);
        Tag1011Materials.Add(material1011_1);

        var material1011_2 = new Material() //
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag10", 10), new("Tag11", 10)},
            Ratings = new HashSet<Rating> {new(10, "Reviewer")},
            Levels = new HashSet<Level> {masters},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
            Medias = new HashSet<Media> {report},
            Language = english,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Blazor " + counter++,
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = DateTime.UtcNow
        };

        context.AddRange(material1011_2);
        Tag1011Materials.Add(material1011_2);

        var material1011_3 = new Material() //
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag10", 2), new("Tag11", 9)},
            Ratings = new HashSet<Rating> {new(10, "Reviewer")},
            Levels = new HashSet<Level> {masters},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
            Medias = new HashSet<Media> {report},
            Language = english,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Blazor " + counter++,
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = DateTime.UtcNow
        };

        context.AddRange(material1011_3);
        Tag1011Materials.Add(material1011_3);

        var material1011_4 = new Material() //
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag10", 5), new("Tag11", 5)},
            Ratings = new HashSet<Rating> {new(10, "Reviewer")},
            Levels = new HashSet<Level> {masters},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
            Medias = new HashSet<Media> {report},
            Language = english,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Blazor " + counter++,
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = DateTime.UtcNow
        };

        context.AddRange(material1011_4);
        Tag1011Materials.Add(material1011_4);

        var material1011_5 = new Material() //
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag10", 9), new("Tag11", 4)},
            Ratings = new HashSet<Rating> {new(10, "Reviewer")},
            Levels = new HashSet<Level> {masters},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
            Medias = new HashSet<Media> {report},
            Language = english,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Blazor " + counter++,
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = DateTime.UtcNow
        };

        context.AddRange(material1011_5);
        Tag1011Materials.Add(material1011_5);

        var material1011_6 = new Material() //
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("Tag10", 6), new("Tag11", 2)},
            Ratings = new HashSet<Rating> {new(10, "Reviewer")},
            Levels = new HashSet<Level> {masters},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {java},
            Medias = new HashSet<Media> {report},
            Language = english,
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "Blazor " + counter++,
            Authors = new HashSet<Author> {new("Writername", "Writernameson")},
            TimeStamp = DateTime.UtcNow
        };

        context.AddRange(material1011_6);
        Tag1011Materials.Add(material1011_6);

        #endregion

        #region UpperLowerCase

        //material for upper lower case
        UpperLowerMaterial = new Material() //
        {
            Id = materialId++,
            WeightedTags = new HashSet<WeightedTag> {new("DOTNET", 10)},
            Ratings = new HashSet<Rating> {new(10, "Reviewer")},
            Levels = new HashSet<Level> {new(4, "School")},
            ProgrammingLanguages = new HashSet<ProgrammingLanguage> {new(4, "Go")},
            Medias = new HashSet<Media> {new(4, "Youtube")},
            Language = new Language(4, "Swedish"),
            Summary = "Lorem ipsum",
            URL = "iamaURL",
            Content = "I am content",
            Title = "I am a very special title with halleluja",
            Authors = new HashSet<Author> {new("Author", "Authorson")},
            TimeStamp = DateTime.UtcNow
        };

        var dotnetTag = new Tag(0, "DOTNET");
        context.Add(dotnetTag);

        context.Add(UpperLowerMaterial);

        #endregion

        context.AddRange(danish, english, spanish, bachelor, masters, phd, book, report, video, csharp, java, fsharp);

        context.SaveChanges();

        Context = context;
    }

    #region getters

    /// <summary>
    ///     Gets the context.
    /// </summary>
    /// <value>The context.</value>
    public IContext Context { get; }

    /// <summary>
    ///     Gets the tag1 materials.
    /// </summary>
    /// <value>The tag1 materials.</value>
    public List<Material> Tag1Materials { get; }

    /// <summary>
    ///     Gets the tag2 materials.
    /// </summary>
    /// <value>The tag2 materials.</value>
    public List<Material> Tag2Materials { get; }

    /// <summary>
    ///     Gets the tag3 materials.
    /// </summary>
    /// <value>The tag3 materials.</value>
    public List<Material> Tag3Materials { get; }

    /// <summary>
    ///     Gets the tag4 materials.
    /// </summary>
    /// <value>The tag4 materials.</value>
    public List<Material> Tag4Materials { get; }

    /// <summary>
    ///     Gets the tag5 materials.
    /// </summary>
    /// <value>The tag5 materials.</value>
    public List<Material> Tag5Materials { get; }

    /// <summary>
    ///     Gets the tag6 materials.
    /// </summary>
    /// <value>The tag6 materials.</value>
    public List<Material> Tag6Materials { get; }

    /// <summary>
    ///     Gets the tag7 materials.
    /// </summary>
    /// <value>The tag7 materials.</value>
    public List<Material> Tag7Materials { get; }

    /// <summary>
    ///     Gets the tag8 materials.
    /// </summary>
    /// <value>The tag8 materials.</value>
    public List<Material> Tag8Materials { get; }

    /// <summary>
    ///     Gets the tag9 materials.
    /// </summary>
    /// <value>The tag9 materials.</value>
    public List<Material> Tag9Materials { get; }

    /// <summary>
    ///     Gets the tag1011 materials.
    /// </summary>
    /// <value>The tag1011 materials.</value>
    public List<Material> Tag1011Materials { get; }

    /// <summary>
    ///     Gets the upper lower material.
    /// </summary>
    /// <value>The upper lower material.</value>
    public Material UpperLowerMaterial { get; }

    #endregion
}