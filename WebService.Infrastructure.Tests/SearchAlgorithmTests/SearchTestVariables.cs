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

            int MaterialID = 1;

            Tag[] tags = new Tag[13];

            for (int i = 1; i < 13; i++)
            {
                tags[i] = new Tag(i, "Tag" + i);
                context.AddRange(tags[i]);
            }

            //makes Reviewer 1-10 for all Ratings 1-10
            Rating[,] ratings = new Rating[11,11];

            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    ratings[i,j] = new Rating(i, ("Reviewer" + j));
                    //context.AddRange(ratings[i,j]);
                }
            }


            /*
            //Makes weight 1-10 for all 3 test tags
            WeightedTag[][] weightedTags = new WeightedTag[3][];
            Tag[] tags = new Tag[] {
                new Tag(1, "SOLID"),
                new Tag(2, "RAD"),
                new Tag(3, "API")};

            for(int i = 0; i<3 ; i++){
                for (int j = 1; j < 11; i++){
                    weightedTags[i] = new WeightedTag[10];
                    weightedTags[i][j] = (new WeightedTag(tags[i].Name, j));
                }
            }
            */

            var author1 = new Author("Rasmus", "Kristensen");
            var author2 = new Author("Alex", "Su");
            var author3 = new Author("Thor", "Lind");


            //Varying tag weight
            Tag1Materials = new List<Material>();
            for (int i = 1; i < 10; i++)
            {
                var material1 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag1", i) },
                    Ratings = new HashSet<Rating> { ratings[5,1] },
                    Levels = new HashSet<Level> { masters },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage> { csharp },
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = "Blazor for beginners"+i,
                    Authors = new HashSet<Author> { author1 },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material1);
                Tag1Materials.Add(material1);
            }


            //Varying rating
            Tag2Materials = new List<Material>();
            for (int i = 0; i < 10; i++)
            {
                var material = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag2", 5) },
                    Ratings = new HashSet<Rating> { ratings[i,1] },
                    Levels = new HashSet<Level> { masters },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage> { java },
                    Medias = new HashSet<Media> { report },
                    Language = english,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = "Blazor for experts"+i,
                    Authors = new HashSet<Author> { author1 },
                    TimeStamp = System.DateTime.UtcNow
                };

                Tag2Materials.Add(material);
            };

            //Varying levels
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
            for (int i = 0; i < 6; i++)
            {
                var material3 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag3", 5) },
                    Ratings = new HashSet<Rating> { ratings[5,1] },
                    Levels = Tag3Levels[i],
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage> { csharp },
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = ".NET Framework tutorial"+i,
                    Authors = new HashSet<Author> { author1 },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material3);
                Tag3Materials.Add(material3);
            }


            //Varying programming Languages
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
            for (int i = 0; i < 6; i++)
            {
                var material5 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag5", 5) },
                    Ratings = new HashSet<Rating> { ratings[5,1] },
                    Levels = new HashSet<Level>() { bachelor },
                    ProgrammingLanguages = Tag5PLanguages[i],
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = ".NET Framework guide"+i,
                    Authors = new HashSet<Author> { author1 },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material5);
                Tag5Materials.Add(material5);
            }


            //Varying author
            List<HashSet<Author>> Tag7Authors = new List<HashSet<Author>>(){
                new HashSet<Author>(){author1},
                 new HashSet<Author>(){author2},
                new HashSet<Author>(){author3},
                new HashSet<Author>(){author1, author2},
                new HashSet<Author>(){author1, author3},
                new HashSet<Author>(){author2, author3},
                new HashSet<Author>(){author1, author2, author3}
            };

            Tag7Materials = new List<Material>();
            for (int i = 0; i < 6; i++)
            {
                var material7 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag5", 5) },
                    Ratings = new HashSet<Rating> { ratings[5,1] },
                    Levels = new HashSet<Level>() { bachelor },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage>() { java },
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = ".NET Framework introduction"+i,
                    Authors = Tag7Authors[i],
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material7);
                Tag7Materials.Add(material7);
            }

            //Varying title
            List<string> Tag7Titles = new List<string>(){
                "Lorem ipsum dolor sit amet",
                "Lorem ipsum dolor sit",
                "Lorem ipsum dolor",
                "Lorem ipsum",    
                "Lorem",
                "consectetuer adipiscing elit",
                "CONSECTETUER ADIPISCING ELIT",

            };

            Tag5Materials = new List<Material>();
            for (int i = 0; i < Tag7Titles.Count; i++)
            {
                var material5 = new Material() //
                {
                    Id = MaterialID++,
                    WeightedTags = new HashSet<WeightedTag> { new WeightedTag("Tag5", 5) },
                    Ratings = new HashSet<Rating> { ratings[5,1] },
                    Levels = new HashSet<Level>() { bachelor },
                    ProgrammingLanguages = new HashSet<ProgrammingLanguage>() { java },
                    Medias = new HashSet<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = Tag7Titles[i],
                    Authors = new HashSet<Author> { author1 },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material5);
                Tag5Materials.Add(material5);
            }




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
        public List<Material> Tag5Materials { get; }

        public List<Material> Tag7Materials { get; }


    }
}
