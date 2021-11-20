namespace WebService.Infrastructure.Tests.ContextRepository
{
    internal class TestVariables
    {
        public TestVariables()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseSqlite(connection);
            var context = new Context(builder.Options);
            context.Database.EnsureCreated();

            var category1 = new Category("Algorithms");
            var category2 = new Category("Database");
            var category3 = new Category("UML");

            var language1 = new Language("Danish");
            var language2 = new Language("English");
            var language3 = new Language("Indian");

            var level1 = new Level("Bachelor");
            var level2 = new Level("Master");
            var level3 = new Level("PHD");

            var media1 = new Media("Book");
            var media2 = new Media("Video");
            var media3 = new Media("Report");

            var programmingLanguage1 = new ProgrammingLanguage("C#");
            var programmingLanguage2 = new ProgrammingLanguage("F#");
            var programmingLanguage3 = new ProgrammingLanguage("C++");

            var rating1 = new Rating(2);
            var rating2 = new Rating(5);
            var rating3 = new Rating(9);

            var tag1 = new Tag("SOLID");
            var tag2 = new Tag("RAD");
            var tag3 = new Tag("API");

            IPresentableMaterial? content1 = null;
            IPresentableMaterial? content2 = null;
            IPresentableMaterial? content3 = null;

            var material1 = new Material(new List<Tag> { tag1 }, new List<Category> { category1 }, new List<Rating> { rating1 }, new List<Level> { level1 }, new List<ProgrammingLanguage> { programmingLanguage1 }, new List<Media> { media1 }, language1, content1, "Material 1", new List<string> { "Author 1" }, System.DateTime.UtcNow);
            var material2 = new Material(new List<Tag> { tag2 }, new List<Category> { category2 }, new List<Rating> { rating2 }, new List<Level> { level2 }, new List<ProgrammingLanguage> { programmingLanguage2 }, new List<Media> { media2 }, language2, content2, "Material 2", new List<string> { "Author 2" }, System.DateTime.UtcNow);
            var material3 = new Material(new List<Tag> { tag3 }, new List<Category> { category3 }, new List<Rating> { rating3 }, new List<Level> { level3 }, new List<ProgrammingLanguage> { programmingLanguage3 }, new List<Media> { media3 }, language3, content3, "Material 3", new List<string> { "Author 3" }, System.DateTime.UtcNow);
            var material4 = new Material(new List<Tag> { tag1, tag2 }, new List<Category> { category1, category2 }, new List<Rating> { rating1, rating2 }, new List<Level> { level1, level2 }, new List<ProgrammingLanguage> { programmingLanguage1, programmingLanguage2 }, new List<Media> { media1, media2 }, language1, content1, "Material 4", new List<string> { "Author 4" }, System.DateTime.UtcNow.AddDays(-10));
            var material5 = new Material(new List<Tag> { tag2, tag3 }, new List<Category> { category2, category3 }, new List<Rating> { rating2, rating3 }, new List<Level> { level2, level3 }, new List<ProgrammingLanguage> { programmingLanguage2, programmingLanguage3 }, new List<Media> { media2, media3 }, language2, content2, "Material 5", new List<string> { "Author 5" }, System.DateTime.UtcNow.AddYears(-1));
            var material6 = new Material(new List<Tag> { tag3, tag1}, new List<Category> { category3, category1 }, new List<Rating> { rating3, rating1 }, new List<Level> { level3, level1 }, new List<ProgrammingLanguage> { programmingLanguage3, programmingLanguage1 }, new List<Media> { media3, media1 }, language3, content3, "Material 6", new List<string> { "Author 6" }, System.DateTime.UtcNow.AddYears(-11).AddDays(10));          
            var material7 = new Material(new List<Tag> { tag1, tag3 }, new List<Category> { category1, category3 }, new List<Rating> { rating1, rating3 }, new List<Level> { level1, level3 }, new List<ProgrammingLanguage> { programmingLanguage1, programmingLanguage3 }, new List<Media> { media1, media3 }, language1, content1, "Material 7", new List<string> { "Author 7" }, System.DateTime.UtcNow.AddDays(-10));
            var material8 = new Material(new List<Tag> { tag2, tag1 }, new List<Category> { category2, category1 }, new List<Rating> { rating2, rating1 }, new List<Level> { level2, level1 }, new List<ProgrammingLanguage> { programmingLanguage2, programmingLanguage1 }, new List<Media> { media2, media1 }, language2, content2, "Material 8", new List<string> { "Author 8" }, System.DateTime.UtcNow.AddYears(-1));
            var material9 = new Material(new List<Tag> { tag3, tag2 }, new List<Category> { category3, category2 }, new List<Rating> { rating3, rating2 }, new List<Level> { level3, level2 }, new List<ProgrammingLanguage> { programmingLanguage3, programmingLanguage2 }, new List<Media> { media3, media2 }, language3, content3, "Material 9", new List<string> { "Author 9" }, System.DateTime.UtcNow.AddYears(-11).AddDays(10));
            var material10 = new Material(new List<Tag> { tag3, tag2 , tag1}, new List<Category> { category3, category2, category1 }, new List<Rating> { rating3, rating2, rating1 }, new List<Level> { level3, level2, level1 }, new List<ProgrammingLanguage> { programmingLanguage3, programmingLanguage2, programmingLanguage1 }, new List<Media> { media3, media2, media1 }, language3, content3, "Material 10", new List<string> { "Author 10" }, System.DateTime.UtcNow);

            context.AddRange(category1,category2, category3,language1, language2, language3,level1,level2,level3,media1,media2,media3,programmingLanguage1,programmingLanguage2,programmingLanguage3,rating1,rating2,rating3,tag1,tag2,tag3,material1,material2,material3,material4,material5,material6,material7,material8,material9,material10);

            context.SaveChanges();

            Context = context;

            CategoryRepository = new CategoryRepository(context);
            LanguageRepository = new LanguageRepository(context);
            LevelRepository = new LevelRepository(context);
            MaterialRepository = new MaterialRepository(context);
            MediaRepository = new MediaRepository(context);
            ProgrammingLanguageRepository = new ProgrammingLanguageRepository(context);
            RatingRepository = new RatingRepository(context);
            TagRepository = new TagRepository(context);
        }
        public IContext Context { get; }
        public CategoryRepository CategoryRepository { get; }
        public LanguageRepository LanguageRepository { get; }
        public LevelRepository LevelRepository { get; }
        public MaterialRepository MaterialRepository { get; }
        public MediaRepository MediaRepository { get; }
        public ProgrammingLanguageRepository ProgrammingLanguageRepository { get; }
        public RatingRepository RatingRepository { get; }
        public TagRepository TagRepository { get; }
    }
}
