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
            context.Database.ExecuteSqlRaw("DELETE btg.Authors");
            context.Database.ExecuteSqlRaw("DELETE btg.Languages");
            context.Database.ExecuteSqlRaw("DELETE btg.Levels");
            context.Database.ExecuteSqlRaw("DELETE btg.Materials");
            context.Database.ExecuteSqlRaw("DELETE btg.Medias");
            context.Database.ExecuteSqlRaw("DELETE btg.ProgrammingLanguages");
            context.Database.ExecuteSqlRaw("DELETE btg.Tags");


            // Reset id counters
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Authors_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Languages_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Levels_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Materials_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Medias_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.ProgrammingLanguages_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Tags_id_seq RESTART WITH 1");
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
            
            var weightedTags = new WeightedTag[] 
            {
                new WeightedTag(1, "GOF", 10),
                new WeightedTag(2, "Advanced", 10),
                new WeightedTag(3, "Beginner", 10),
                new WeightedTag(4, "Linq", 10),
                new WeightedTag(5, "Solid", 10),
                new WeightedTag(6, "Blazor", 10),
                new WeightedTag(7, "Docker", 10),
                new WeightedTag(8, "RAD", 10),
                new WeightedTag(9, "SDD", 10),
                new WeightedTag(10, "ODD", 10),
                new WeightedTag(11, "Algorithms", 10),
                new WeightedTag(12, "macos", 10),
                new WeightedTag(13, "windows", 10),
                new WeightedTag(14, "Linux", 10),
                new WeightedTag(15, "Azure", 10),
                new WeightedTag(16, "Collections", 10),
                new WeightedTag(17, "Generics", 10),
                new WeightedTag(18, "Refactor", 10),
                new WeightedTag(19, "Testing", 10),
                new WeightedTag(20, "TDD", 10),
                new WeightedTag(21, "Nuget", 10),
                new WeightedTag(22, "UI", 10),
                new WeightedTag(23, "UX", 10),
                new WeightedTag(24, "Multi - Tasking", 10),
                new WeightedTag(25, "Threads", 10),
                new WeightedTag(26, "Xunit", 10),
                new WeightedTag(27, "API", 10),
                new WeightedTag(28, "Terminal", 10),
                new WeightedTag(29, "Software Architecture", 10),
                new WeightedTag(30, "DOTNET", 10),
                new WeightedTag(31, ".NET", 10),
                new WeightedTag(32, "Rider", 10),
                new WeightedTag(33, "Visual Studio", 10),
                new WeightedTag(34, "Events", 10),
                new WeightedTag(35, "Regexp", 10),
                new WeightedTag(36, "Data type", 10),
                new WeightedTag(37, "Clean Code", 10),
                new WeightedTag(38, "Workflow", 10),
                new WeightedTag(39, "Binary Search tree", 10),
                new WeightedTag(40, "Path finding", 10)
            };



            var contents = new IPresentableMaterial?[]
            {
                null,
                null,
                null
            };


            var materials = new Material[]
            {
                new Material()
                {
                    WeightedTags = new List<WeightedTag> { weightedTags[0] },
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
                    WeightedTags = new List<WeightedTag> { weightedTags[2], weightedTags[0]},
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
                    WeightedTags = new List<WeightedTag> { weightedTags[0], weightedTags[1], weightedTags[2] },
                    Levels = new List<Level> { levels[0], levels[1], levels[2] },
                    ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[0], programmingLanguages[1], programmingLanguages[2] },
                    Medias = new List<Media> { medias[0], medias[1], medias[2] },
                    Language = languages[2],
                    Content = contents[0],
                    Title = "Material 3",
                    TimeStamp = DateTime.UtcNow
                }
            };

            context.AddRange(weightedTags,
                             programmingLanguages,
                             medias,
                             languages,
                             levels,
                             materials);
  

            context.SaveChanges();
        }
    }


    
}
