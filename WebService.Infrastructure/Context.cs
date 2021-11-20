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
            //Add some restrictions like index, unique etc.
        }
    }
}
