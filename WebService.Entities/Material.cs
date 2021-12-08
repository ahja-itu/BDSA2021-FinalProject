

namespace WebService.Entities;

public class Material
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    //Only used for DBContext
    public Material()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public Material(int id, ICollection<WeightedTag> weightedTags, ICollection<Rating> ratings,
        ICollection<Level> levels, ICollection<ProgrammingLanguage> programmingLanguages, ICollection<Media> medias,
        Language language, string summary, string url, string content, string title, ICollection<Author> authors,
        DateTime timeStamp)
    {
        Id = id;
        WeightedTags = weightedTags;
        Ratings = ratings;
        Levels = levels;
        ProgrammingLanguages = programmingLanguages;
        Medias = medias;
        Language = language;
        Summary = summary;
        URL = url;
        Content = content;
        Title = title;
        Authors = authors;
        TimeStamp = timeStamp;
    }

    public Material(ICollection<WeightedTag> weightedTags, ICollection<Rating> ratings, ICollection<Level> levels,
        ICollection<ProgrammingLanguage> programmingLanguages, ICollection<Media> medias, Language language,
        string summary, string url, string content, string title, ICollection<Author> authors, DateTime timeStamp)
    {
        WeightedTags = weightedTags;
        Ratings = ratings;
        Levels = levels;
        ProgrammingLanguages = programmingLanguages;
        Medias = medias;
        Language = language;
        Summary = summary;
        URL = url;
        Content = content;
        Title = title;
        Authors = authors;
        TimeStamp = timeStamp;
    }

    public int Id { get; set; }
    public ICollection<WeightedTag> WeightedTags { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Level> Levels { get; set; }
    public ICollection<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public ICollection<Media> Medias { get; set; }
    public Language Language { get; set; }

    [StringLength(400)] public string Summary { get; set; }

    public string URL { get; set; }
    public string Content { get; set; }

    [StringLength(50)] public string Title { get; set; }

    public ICollection<Author> Authors { get; set; }
    public DateTime TimeStamp { get; set; }

    public bool hasMinimumAverageRating(int minimumRating)
        => Ratings.Count() == 0 ? true : Ratings.Average(r => r.Value) >= minimumRating;


}