namespace WebService.Entities;

public class Context : DbContext, IContext
{
    public Context(DbContextOptions options) : base(options)
    {
    }

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
        CleanDB(context);

        var medias = new Media[]
        {
            new("Books"),
            new("Video"),
            new("Stream"),
            new("Quiz"),
            new("Reports"),
            new("Articles")
        };

        var languages = new Language[]
        {
            new("Danish"),
            new("English"),
            new("Indian"),
            new("Italian")
        };

        var programmingLanguages = new ProgrammingLanguage[]
        {
            new("C#"),
            new("F#"),
            new("C++"),
            new("Java"),
            new("Python"),
            new("Ruby"),
            new("SQL"),
            new("GoLang")
        };

        var levels = new Level[]
        {
            new("Middle school"),
            new("High school"),
            new("Bachelor"),
            new("Master"),
            new("PHD"),
            new("Everyone")
        };

        var tags = new Tag[]
        {
            new(1, "GOF"),
            new(2, "Advanced"),
            new(3, "Beginner"),
            new(4, "Linq"),
            new(5, "Solid"),
            new(6, "Blazor"),
            new(7, "Docker"),
            new(8, "RAD"),
            new(9, "SDD"),
            new(10, "ODD"),
            new(11, "Algorithms"),
            new(12, "macos"),
            new(13, "windows"),
            new(14, "Linux"),
            new(15, "Azure"),
            new(16, "Collections"),
            new(17, "Generics"),
            new(18, "Refactor"),
            new(19, "Testing"),
            new(20, "TDD"),
            new(21, "Nuget"),
            new(22, "UI"),
            new(23, "UX"),
            new(24, "Multi - Tasking"),
            new(25, "Threads"),
            new(26, "Xunit"),
            new(27, "API"),
            new(28, "Terminal"),
            new(29, "Software Architecture"),
            new(30, "DOTNET"),
            new(31, ".NET"),
            new(32, "Rider"),
            new(33, "Visual Studio"),
            new(34, "Events"),
            new(35, "Regexp"),
            new(36, "Data type"),
            new(37, "Clean Code"),
            new(38, "Workflow"),
            new(39, "Binary Search tree"),
            new(40, "Path finding")
        };


        var contents = new[]
        {
            "content 1",
            "content 2",
            "content 3"
        };

        var authors = new Author[]
        {
            new("John", "Sena"),
            new("Paolo", "Tell"),
            new("Rasmus", "Lystrøm")
        };

        var weightedTag1 = new WeightedTag("SOLID", 10);
        var weightedTag2 = new WeightedTag("RAD", 50);
        var weightedTag3 = new WeightedTag("API", 90);

        var summary1 = "summary 1";
        var summary2 = "summary 2";
        var summary3 = "summary 3";

        var url1 = "url1.com";
        var url2 = "url2.com";
        var url3 = "url3.com";

        var materials = new[]
        {
            new(
                new List<WeightedTag> {weightedTag1},
                new List<Rating> {new(1, "Claus")},
                new List<Level> {levels[0]},
                new List<ProgrammingLanguage> {programmingLanguages[0]},
                new List<Media> {medias[0]},
                languages[0],
                summary1,
                url1,
                contents[0],
                "Material 1",
                new[] {authors[0]},
                DateTime.UtcNow
            ),
            new Material(
                new List<WeightedTag> {weightedTag1, weightedTag2},
                new List<Rating> {new(6, "Sten")},
                new List<Level> {levels[2], levels[0]},
                new List<ProgrammingLanguage> {programmingLanguages[0], programmingLanguages[1]},
                new List<Media> {medias[2], medias[0]},
                languages[2],
                summary2,
                url2,
                contents[1],
                "Material 2",
                new[] {authors[1]},
                DateTime.UtcNow.AddYears(-11).AddDays(10)
            ),
            new Material(
                new List<WeightedTag> {weightedTag1, weightedTag2, weightedTag3},
                new List<Rating> {new(10, "Jens")},
                new List<Level> {levels[0], levels[1], levels[2]},
                new List<ProgrammingLanguage>
                    {programmingLanguages[0], programmingLanguages[1], programmingLanguages[2]},
                new List<Media> {medias[0], medias[1], medias[2]},
                languages[2],
                summary3,
                url3,
                contents[2],
                "Material 3",
                new[] {authors[2]},
                DateTime.UtcNow
            )
        };

        context.AddRange(tags);
        context.AddRange(programmingLanguages);
        context.AddRange(medias);
        context.AddRange(languages);
        context.AddRange(levels);
        context.AddRange(materials);
        context.AddRange(authors);


        context.SaveChanges();
    }
}