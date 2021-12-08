

namespace WebService.Entities;

public class Material
{
    public Material()
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

}
