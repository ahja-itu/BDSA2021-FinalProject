namespace WebService.Infrastructure.Tests.SearchAlgorithmTests
{
    internal class SearchTestVariables
    {
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

            var author1 = new Author("Rasmus", "Kristensen");
            var author2 = new Author("Alex", "Su");
            var author3 = new Author("Thor", "Lind");

            int MaterialID = 1;



            #region Tag1-WeightOneTag
            //Varying tag weight - Tag1
            Tag1Materials = new List<Material>();
            for (int i = 1; i <= 10; i++)
            {
                var material1 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag1", i) },
                    Ratings = new HashSet<Rating> { new Rating(5, "Reviewer") },
                    Levels = new HashSet<Level> { masters },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage> { csharp },
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = "Blazor for experts " + i,
                    Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material1);
                Tag1Materials.Add(material1);
            }
            #endregion

            #region Tag2-Rating
            //Varying rating - Tag2
            Tag2Materials = new List<Material>();
            for (int i = 0; i <= 10; i++)
            {
                var material2 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag2", 10) },
                    Ratings = new HashSet<Rating> { new Rating(i, "reviewer" + i) },
                    Levels = new HashSet<Level> { masters },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage> { java },
                    Medias = new HashSet<Media> { report },
                    Language = english,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = "Blazor for kids " + i,
                    Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material2);
                Tag2Materials.Add(material2);
            };
            #endregion

            #region Tag3-Levels
            //Varying levels - Tag3
            List<HashSet<Level>> Tag3Levels = new List<HashSet<Level>>(){
                new HashSet<Level>(){bachelor},
                new HashSet<Level>(){masters},
                new HashSet<Level>(){phd},
                new HashSet<Level>(){bachelor, masters},
                new HashSet<Level>(){bachelor, phd},
                new HashSet<Level>(){masters, phd},
                new HashSet<Level>(){bachelor, masters, phd}
            };

            Tag3Materials = new List<Material>();
            for (int i = 0; i <= 6; i++)
            {
                var material3 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag3", 10) },
                    Ratings = new HashSet<Rating> { new Rating(5, "Reviewer") },
                    Levels = Tag3Levels[i],
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage> { csharp },
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = ".NET Framework tutorial " + i,
                    Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material3);
                Tag3Materials.Add(material3);
            }
            #endregion


            #region Tag4-Languages
            //Varying Languages - Tag4

            Tag4Materials = new List<Material>();

            var material4_1 = new Material()
            {
                Id = MaterialID++,
                WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag4", 10) },
                Ratings = new HashSet<Rating> { new Rating(6, "Reviewer") },
                Levels = new HashSet<Level> { bachelor },
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> { csharp },
                Medias = new HashSet<Media> { video },
                Language = danish,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "En titel p� dansk",
                Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                TimeStamp = new DateTime(2011, 1, 1)
            };
            context.AddRange(material4_1);
            Tag4Materials.Add(material4_1);

            var material4_2 = new Material()
            {
                Id = MaterialID++,
                WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag4", 10) },
                Ratings = new HashSet<Rating> { new Rating(6, "Reviewer") },
                Levels = new HashSet<Level> { bachelor },
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> { csharp },
                Medias = new HashSet<Media> { video },
                Language = english,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "A title in English",
                Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                TimeStamp = new DateTime(2011, 1, 1)
            };
            context.AddRange(material4_2);

            Tag4Materials.Add(material4_2);




            var material4_3 = new Material()
            {
                Id = MaterialID++,
                WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag4", 10) },
                Ratings = new HashSet<Rating> { new Rating(6, "Reviewer") },
                Levels = new HashSet<Level> { bachelor },
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> { csharp },
                Medias = new HashSet<Media> { video },
                Language = spanish,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "Un t�tulo en espa�ol",
                Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
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
            List<HashSet<ProgrammingLanguage>> Tag5PLanguages = new List<HashSet<ProgrammingLanguage>>(){
                new HashSet<ProgrammingLanguage>(){csharp},
                new HashSet<ProgrammingLanguage>(){fsharp},
                new HashSet<ProgrammingLanguage>(){java},
                new HashSet<ProgrammingLanguage>(){csharp, fsharp},
                new HashSet<ProgrammingLanguage>(){csharp, java},
                new HashSet<ProgrammingLanguage>(){fsharp, java},
                new HashSet<ProgrammingLanguage>(){csharp, fsharp, java},

            };

            Tag5Materials = new List<Material>();
            for (int i = 0; i <= 6; i++)
            {
                var material5 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag5", 10) },
                    Ratings = new HashSet<Rating> { new Rating(5, "Reviewer") },
                    Levels = new HashSet<Level>() { bachelor },
                    ProgrammingLanguages = Tag5PLanguages[i],
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = ".NET Framework guide " + i,
                    Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material5);
                Tag5Materials.Add(material5);
            }
            #endregion


            #region Tag6-Media
            //Varying media - Tag6
            Tag6Materials = new List<Material>();

            var material6_1 = new Material()
            {
                Id = MaterialID++,
                WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag6", 10) },
                Ratings = new HashSet<Rating> { new Rating(4, "Reviewer") },
                Levels = new HashSet<Level> { bachelor },
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> { java },
                Medias = new HashSet<Media> { book },
                Language = danish,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "Dockerize your life",
                Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                TimeStamp = new DateTime(2013, 1, 1)
            };



            var material6_2 = new Material()
            {
                Id = MaterialID++,
                WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag6", 10) },
                Ratings = new HashSet<Rating> { new Rating(4, "Reviewer") },
                Levels = new HashSet<Level> { bachelor },
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> { java },
                Medias = new HashSet<Media> { report },
                Language = danish,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "Dockerize your life II",
                Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                TimeStamp = new DateTime(2013, 1, 1)
            };

            var material6_3 = new Material()
            {
                Id = MaterialID++,
                WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag6", 10) },
                Ratings = new HashSet<Rating> { new Rating(4, "Reviewer") },
                Levels = new HashSet<Level> { bachelor },
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> { java },
                Medias = new HashSet<Media> { video },
                Language = danish,
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "Dockerize your life III",
                Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
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
            List<HashSet<Author>> Tag7Authors = new List<HashSet<Author>>(){
                new HashSet<Author>(){new Author("Alfa", "Alfason") },
                new HashSet<Author>(){new Author("Alfa", "Bravoson") },
                new HashSet<Author>(){new Author("Bravo", "Bravoson") },
                new HashSet<Author>(){new Author("Alfa", "Alfason"), new Author("Bravo", "Bravoson") } 
            };

            Tag7Materials = new List<Material>();
            for (int i = 0; i < 4; i++)
            {
                var material7 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag5", 10) },
                    Ratings = new HashSet<Rating> { new Rating(5, "Reviewer") },
                    Levels = new HashSet<Level>() { bachelor },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage>() { java },
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = ".NET Framework intro " + i,
                    Authors = Tag7Authors[i],
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material7);
                Tag7Materials.Add(material7);
            }
            #endregion



            #region Tag8-Timestamp
            //Varying timestamp - Tag8
            Tag8Materials = new List<Material>();
            for (int i = 2014; i < 2022; i++)
            {
                var material8 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag8", 10) },
                    Ratings = new HashSet<Rating> { new Rating(5, "Reviewer") },
                    Levels = new HashSet<Level> { masters },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage> { csharp },
                    Medias = new HashSet<Media> { video },
                    Language = english,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = "The lastest news about C# " + i,
                    Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                    TimeStamp = new DateTime(i, 1, 1)
                };
                context.AddRange(material8);
                Tag8Materials.Add(material8);
            }
            #endregion


            #region Tag9-Title
            //Varying title - Tag9
            Tag9Materials = new List<Material>();
            List<string> Tag9Titles = new List<string>(){
                "Lorem ipsum dolor sit amet",
                "Lorem ipsum dolor sit",
                "Lorem ipsum dolor",
                "Lorem ipsum",    
                "Lorem",
                "consectetuer adipiscing elit",
                "CONSECTETUER ADIPISCING ELIT",
            };

           
            for (int i = 0; i < Tag9Titles.Count; i++)
            {
                var material9 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag9", 10) },
                    Ratings = new HashSet<Rating> { new Rating(5, "Reviewer") },
                    Levels = new HashSet<Level>() { bachelor },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage>() { java },
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = Tag9Titles[i],
                    Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material9);
                Tag9Materials.Add(material9);
            }
            #endregion


            #region Tag10Tag11-WeightTwoTags
            //Varying weight, two tags - Tag10, Tag11
            Tag1011Materials = new List<Material>();
            for (int i = 1; i <= 10; i++)
            {
                var material1011 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag10", i), new WeightedTag("Tag11", 11 - i) },
                    Ratings = new HashSet<Rating> { new Rating(5, "Reviewer") },
                    Levels = new HashSet<Level> { masters },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage> { java },
                    Medias = new HashSet<Media> { report },
                    Language = english,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = "Blazor for beginners " + i,
                    Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material1011);
                Tag1011Materials.Add(material1011);
            }

            //material for upper lower case
            UpperLowerMaterial = new Material() //
            {
                Id = MaterialID++,
                WeightedTags = new HashSet<WeightedTag> { new WeightedTag("DOTNET", 10) },
                Ratings = new HashSet<Rating> { new Rating(5,"Reviewer") },
                Levels = new HashSet<Level> { new Level(4,"School") },
                ProgrammingLanguages = new HashSet<ProgrammingLanguage> { new ProgrammingLanguage(4,"Go") },
                Medias = new HashSet<Media> { new Media(4,"Youtube") },
                Language = new Language(4,"Swedish"),
                Summary = "Lorem ipsum",
                URL = "iamaURL",
                Content = "I am content",
                Title = "Blazor for beginners for upper lower case material",
                Authors = new HashSet<Author> { new Author("Writername", "Writernameson") },
                TimeStamp = System.DateTime.UtcNow
            };

            var dotnetTag = new Tag(0, "DOTNET");
            context.Add(dotnetTag);

            context.Add(UpperLowerMaterial);

            context.AddRange(danish, english, spanish, bachelor, masters, phd, book, report, video, csharp, java, fsharp);

            context.SaveChanges();

            Context = context;

            LanguageRepository = new LanguageRepository(context);
            LevelRepository = new LevelRepository(context);
            MaterialRepository = new MaterialRepository(context);
            MediaRepository = new MediaRepository(context);
            ProgrammingLanguageRepository = new ProgrammingLanguageRepository(context);
            TagRepository = new TagRepository(context);
        }

        #region getters
        public IContext Context { get; }
        public LanguageRepository LanguageRepository { get; }
        public LevelRepository LevelRepository { get; }
        public MaterialRepository MaterialRepository { get; }
        public MediaRepository MediaRepository { get; }
        public ProgrammingLanguageRepository ProgrammingLanguageRepository { get; }
        public TagRepository TagRepository { get; }

        public List<Material> Tag1Materials { get; }
        public List<Material> Tag2Materials { get; }
        public List<Material> Tag3Materials { get; }
        public List<Material> Tag4Materials { get; }
        public List<Material> Tag5Materials { get; }
        public List<Material> Tag6Materials { get; }
        public List<Material> Tag7Materials { get; }
        public List<Material> Tag8Materials { get; }
        public List<Material> Tag9Materials { get; }
        public List<Material> Tag1011Materials { get; }
        public Material UpperLowerMaterial { get; }

        #endregion

    }
}
