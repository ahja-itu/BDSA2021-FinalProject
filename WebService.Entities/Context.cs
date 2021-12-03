namespace WebService.Entities
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Level> Levels => Set<Level>();
        public DbSet<Material> Materials => Set<Material>();
        public DbSet<Media> Medias => Set<Media>();
        public DbSet<ProgrammingLanguage> ProgrammingLanguages => Set<ProgrammingLanguage>();
        public DbSet<Tag> Tags => Set<Tag>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Level>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Material>()
                .HasIndex(s => s.Title)
                .IsUnique();

            modelBuilder.Entity<Material>()
                .OwnsMany(e => e.WeightedTags, a =>
                {
                    a.WithOwner().HasForeignKey("MaterialId");
                    a.Property<int>("MaterialId");
                    a.Property<string>("Name");
                    a.HasKey("MaterialId", "Name");
                });

            modelBuilder.Entity<Material>()
                .OwnsMany(e => e.Ratings, a =>
                {
                    a.WithOwner().HasForeignKey("MaterialId");
                    a.Property<int>("MaterialId");
                    a.Property<string>("Reviewer");
                    a.HasKey("MaterialId", "Reviewer");
                });

            modelBuilder.Entity<Material>()
                .OwnsMany(e => e.Authors, a =>
                {
                    a.WithOwner().HasForeignKey("MaterialId");
                    a.Property<int>("MaterialId");
                    a.Property<string>("FirstName");
                    a.Property<string>("SurName");
                    a.HasKey("MaterialId", "FirstName", "SurName");
                });

            modelBuilder.Entity<Media>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<ProgrammingLanguage>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(s => s.Name)
                .IsUnique();
        }

        private static void CleanDB(Context context)
        {
            // Solution from https://stackoverflow.com/questions/15220411/entity-framework-delete-all-rows-in-table
            context.Languages.RemoveRange(context.Languages);
            context.Levels.RemoveRange(context.Levels);
            context.Materials.RemoveRange(context.Materials);
            context.Medias.RemoveRange(context.Medias);
            context.ProgrammingLanguages.RemoveRange(context.ProgrammingLanguages);
            context.Tags.RemoveRange(context.Tags);
        }

        public static void Seed(Context context)
        {
            

            // Clear out the database entries
            Context.CleanDB(context);

            var medias = new Media[]
            {
                new Media("Books"),
                new Media("Video"),
                new Media("Stream"),
                new Media("Quiz"),
                new Media("Reports"),
                new Media("Articles")
            };

            var languages = new Language[]
            {
                new Language("Danish"),
                new Language("English"),
                new Language("Indian"),
                new Language("Italian")
            };

            var programmingLanguages = new ProgrammingLanguage[]
            {
                new ProgrammingLanguage("C#"),
                new ProgrammingLanguage("F#"),
                new ProgrammingLanguage("C++"),
                new ProgrammingLanguage("Java"),
                new ProgrammingLanguage("Python"),
                new ProgrammingLanguage("Ruby"),
                new ProgrammingLanguage("SQL"),
                new ProgrammingLanguage("GoLang")
            };

            var levels = new Level[]
            {
                new Level("Middel school"),
                new Level("High school"),
                new Level("Bachelor"),
                new Level("Master"),
                new Level("PHD"),
                new Level("Everyone")
            };
            
            var tags = new Tag[] 
            {
                new Tag(1, "GOF"),
                new Tag(2, "Advanced"),
                new Tag(3, "Beginner"),
                new Tag(4, "Linq"),
                new Tag(5, "Solid"),
                new Tag(6, "Blazor"),
                new Tag(7, "Docker"),
                new Tag(8, "RAD"),
                new Tag(9, "SDD"),
                new Tag(10, "ODD"),
                new Tag(11, "Algorithms"),
                new Tag(12, "macos"),
                new Tag(13, "windows"),
                new Tag(14, "Linux"),
                new Tag(15, "Azure"),
                new Tag(16, "Collections"),
                new Tag(17, "Generics"),
                new Tag(18, "Refactor"),
                new Tag(19, "Testing"),
                new Tag(20, "TDD"),
                new Tag(21, "Nuget"),
                new Tag(22, "UI"),
                new Tag(23, "UX"),
                new Tag(24, "Multi - Tasking"),
                new Tag(25, "Threads"),
                new Tag(26, "Xunit"),
                new Tag(27, "API"),
                new Tag(28, "Terminal"),
                new Tag(29, "Software Architecture"),
                new Tag(30, "DOTNET"),
                new Tag(31, ".NET"),
                new Tag(32, "Rider"),
                new Tag(33, "Visual Studio"),
                new Tag(34, "Events"),
                new Tag(35, "Regexp"),
                new Tag(36, "Data type"),
                new Tag(37, "Clean Code"),
                new Tag(38, "Workflow"),
                new Tag(39, "Binary Search tree"),
                new Tag(40, "Path finding")
            };



            var contents = new IPresentableMaterial?[]
            {
                null,
                null,
                null
            };

            var weightedTag1 = new WeightedTag("SOLID", 10);
            var weightedTag2 = new WeightedTag("RAD", 50);
            var weightedTag3 = new WeightedTag("API", 90);


            var materials = new Material[]
            {
                new Material()
                {
                    WeightedTags = new List<WeightedTag> { weightedTag1 },
                    Levels = new List<Level> { levels[0] },
                    ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[0] },
                    Medias = new List<Media> { medias[0] },
                    Language = languages[0],
                    Content = contents[0],
                    Title = "Material 1",
                    TimeStamp = DateTime.UtcNow
                },
                new Material()
                {
                    WeightedTags = new List<WeightedTag> { weightedTag1, weightedTag2 },
                    Levels = new List<Level> { levels[2], levels[0] },
                    ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[0], programmingLanguages[1] },
                    Medias = new List<Media> { medias[2], medias[0] },
                    Language = languages[2],
                    Content = contents[0],
                    Title = "Material 2",
                    TimeStamp = DateTime.UtcNow.AddYears(-11).AddDays(10)
                },
                new Material()
                {
                    WeightedTags = new List<WeightedTag> { weightedTag1, weightedTag2, weightedTag3 },
                    Levels = new List<Level> { levels[0], levels[1], levels[2] },
                    ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[0], programmingLanguages[1], programmingLanguages[2] },
                    Medias = new List<Media> { medias[0], medias[1], medias[2] },
                    Language = languages[2],
                    Content = contents[0],
                    Title = "Material 3",
                    TimeStamp = DateTime.UtcNow
                }
            };

            context.AddRange(tags);
            context.AddRange(programmingLanguages);
            context.AddRange(medias);
            context.AddRange(languages);
            context.AddRange(levels);
            context.AddRange(materials);
  

            context.SaveChanges();
        }
    }


    
}
