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

        public static void Seed(Context context)
        {
            // TODO: This will be finished in a specific Seed method implementation branch
            // don't run this on production DB

            // Clear out the database entries
            context.Database.ExecuteSqlRaw("DELETE btg.Authors");
            context.Database.ExecuteSqlRaw("DELETE btg.Languages");
            context.Database.ExecuteSqlRaw("DELETE btg.Levels");
            context.Database.ExecuteSqlRaw("DELETE btg.Materials");
            context.Database.ExecuteSqlRaw("DELETE btg.Medias");
            context.Database.ExecuteSqlRaw("DELETE btg.ProgrammingLanguages");
            context.Database.ExecuteSqlRaw("DELETE btg.Ratings");
            context.Database.ExecuteSqlRaw("DELETE btg.Tags");


            // Reset id counters
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Authors_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Languages_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Levels_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Materials_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Medias_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.ProgrammingLanguages_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Ratings_id_seq RESTART WITH 1");
            context.Database.ExecuteSqlRaw("ALTER SEQUENCE btg.Tags_id_seq RESTART WITH 1");
        }
    }


    
}
