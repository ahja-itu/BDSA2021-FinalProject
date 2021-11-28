namespace WebService.Infrastructure
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
                .HasIndex(s => s.EducationLevel)
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
                    a.HasKey("MaterialId", "FirstName","SurName");
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
    }
}
