using WebService.Core.Shared;
using WebService.Entities;

namespace WebService.Core.Shared
{
    public record MaterialDTO : CreateMaterialDTO
    {
        public MaterialDTO(int id, ICollection<CreateWeightedTagDTO> tags, ICollection<CreateRatingDTO> ratings, ICollection<CreateLevelDTO> levels, ICollection<CreateProgrammingLanguageDTO> programmingLanguages, ICollection<CreateMediaDTO> medias, CreateLanguageDTO language, string summary, string url, string content, string title, ICollection<CreateAuthorDTO> authors, DateTime timeStamp) : base(tags, ratings, levels, programmingLanguages, medias, language, summary, url,content, title, authors, timeStamp)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
    public record CreateMaterialDTO
    {
        public CreateMaterialDTO(ICollection<CreateWeightedTagDTO> tags, ICollection<CreateRatingDTO> ratings, ICollection<CreateLevelDTO> levels, ICollection<CreateProgrammingLanguageDTO> programmingLanguages, ICollection<CreateMediaDTO> medias, CreateLanguageDTO language, string summary, string url, string content, string title, ICollection<CreateAuthorDTO> authors, DateTime timeStamp)
        {
            Tags = tags;
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
        public ICollection<CreateWeightedTagDTO> Tags { get; init; }
        public ICollection<CreateRatingDTO> Ratings { get; init; }
        public ICollection<CreateLevelDTO> Levels { get; init; }
        public ICollection<CreateProgrammingLanguageDTO> ProgrammingLanguages { get; init; }
        public ICollection<CreateMediaDTO> Medias { get; init; }
        public CreateLanguageDTO Language { get; init; }
        [StringLength(400)]
        public string Summary { get; init; }
        public string URL { get; init; }
        public string Content { get; init; }
        [StringLength(50)]
        public string Title { get; init; }
        public ICollection<CreateAuthorDTO> Authors { get; init; }
        public DateTime TimeStamp { get; init; }
    }
}
namespace ExtensionMethods
{
    public static class MaterialExtensions
    {
        public static float AverageRating(this CreateMaterialDTO material)
        {
            float sum = material.Ratings.Sum(e => e.Value);
            return sum / material.Ratings.Count;
        }
        public static string LevelsToString(this CreateMaterialDTO material)
        {
            var levels = material.Levels.Aggregate("", (current, level) => current + level.Name + " ");
            return levels.Remove(levels.Length - 1);
        }

        public static string AuthorsToString(this CreateMaterialDTO material)
        {
            var authors =  material.Authors.Aggregate("Authors: ", (current, author) => current + author.FirstName + " " + author.SurName + ", ");
            return authors.Remove(authors.Length - 2);
        }

        public static MaterialDTO ConvertToMaterialDTO(this Material material)
        {
            return new MaterialDTO(
                material.Id,
                material.WeightedTags.Select(e => new WeightedTagDTO(e.Id, e.Name, e.Weight)).Cast<CreateWeightedTagDTO>().ToList(),
                material.Ratings.Select(e => new RatingDTO(e.Id, e.Value,e.Reviewer)).Cast<CreateRatingDTO>().ToList(),
                material.Levels.Select(e => new LevelDTO(e.Id,e.Name)).Cast<CreateLevelDTO>().ToList(),
                material.ProgrammingLanguages.Select(e => new ProgrammingLanguageDTO(e.Id,e.Name)).Cast<CreateProgrammingLanguageDTO>().ToList(),
                material.Medias.Select(e => new MediaDTO(e.Id,e.Name)).Cast<CreateMediaDTO>().ToList(),
                new LanguageDTO(material.Language.Id,material.Language.Name),
                material.Summary,
                material.URL,
                material.Content,
                material.Title,
                material.Authors.Select(e => new AuthorDTO(e.Id,e.FirstName,e.SurName)).Cast<CreateAuthorDTO>().ToList(),
                material.TimeStamp
            );
        }
    }
}
