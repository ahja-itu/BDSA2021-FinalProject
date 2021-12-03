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

            Tag[] tags = new Tag[13];

            for (int i = 1; i < 13; i++) {
                tags[i] = new Tag(i, "Tag" + i);
                context.AddRange(tags[i]);
            }

            //makes Reviewer 1-10 for all Ratings 1-10
            Rating[][] ratings = new Rating[10][];

            for(int i = 1; i<11 ; i++){
                for (int j = 1; j < 11; i++){
                    ratings[i] = new Rating[10];
                    ratings[i][j] = (new Rating(i, ("Reviewer" + j)));
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
                var material1= new Material() //
                {
                    WeightedTags = new List<WeightedTag> { new WeightedTag("Tag1", i) },
                    Ratings = new List<Rating> { ratings[5][1] },
                    Levels = new List<Level> { masters },
                    ProgrammingLanguages = new List<ProgrammingLanguage> { csharp },
                    Medias = new List<Media> { book },
                    Language = danish,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = "Blazor for beginners",
                    Authors = new List<Author> { author1 },
                    TimeStamp = System.DateTime.UtcNow
                };
                context.AddRange(material1);
                Tag1Materials.Add(material1);
            }


            //Varying rating
            Tag2Materials = new List<Material>();
            for(int i = 0; i < 10; i++)
            {
                var material = new Material() //
                {
                    WeightedTags = new List<WeightedTag> { new WeightedTag("Tag2", 5) },
                    Ratings = new List<Rating> { ratings[i][1] },
                    Levels = new List<Level> { masters },
                    ProgrammingLanguages = new List<ProgrammingLanguage> { java },
                    Medias = new List<Media> { report },
                    Language = english,
                    Summary = "Lorem ipsum",
                    URL = "iamaURL",
                    Content = "I am content",
                    Title = "Blazor for beginners",
                    Authors = new List<Author> { author1 },
                    TimeStamp = System.DateTime.UtcNow
                };

                Tag2Materials.Add(material);
            };




            context.AddRange(danish, english, spanish, bachelor, masters, phd, book, report, video, csharp, java, fsharp, rating1, rating2, rating3, tag1, tag2, tag3, material1,material2, material3);

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
    }
}
