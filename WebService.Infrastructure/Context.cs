namespace WebService.Infrastructure
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Language> Languages => Set<Language>();

        public DbSet<Level> Levels => Set<Level>();

        public DbSet<Material> Materials => Set<Material>();

        public DbSet<Media> Medias => Set<Media>();

        public DbSet<ProgrammingLanguage> ProgrammingLanguages => Set<ProgrammingLanguage>();

        public DbSet<Rating> Ratings => Set<Rating>();

        public DbSet<Tag> Tags => Set<Tag>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Language>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Level>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Material>()
                .HasIndex(s => s.Title)
                .IsUnique();

            modelBuilder.Entity<Media>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<ProgrammingLanguage>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Rating>()
                .HasIndex(s => s.Value)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Author>()
                .HasIndex(s => new { s.FirstName,s.SurName})
                .IsUnique();
        }
    }
}
