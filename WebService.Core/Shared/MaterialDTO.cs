using WebService.Entities;

namespace WebService.Core.Shared
{
    public record MaterialDTO : CreateMaterialDTO
    {
        public MaterialDTO(int id, ICollection<TagDTO> tags, ICollection<RatingDTO> ratings, ICollection<LevelDTO> levels, ICollection<ProgrammingLanguageDTO> programmingLanguages, ICollection<MediaDTO> medias, LanguageDTO language, IPresentableMaterial content, string title, ICollection<string> authors, DateTime timeStamp) : base(tags, ratings, levels, programmingLanguages, medias, language, content, title, authors, timeStamp)
        {
            Id = id;
        }

        public int Id { get; init; }
    } 
    public record CreateMaterialDTO
    {
        public CreateMaterialDTO(ICollection<TagDTO> tags, ICollection<RatingDTO> ratings, ICollection<LevelDTO> levels, ICollection<ProgrammingLanguageDTO> programmingLanguages, ICollection<MediaDTO> medias, LanguageDTO language, IPresentableMaterial content, string title, ICollection<string> authors, DateTime timeStamp)
        {
            Tags = tags;
            Ratings = ratings;
            Levels = levels;
            ProgrammingLanguages = programmingLanguages;
            Medias = medias;
            Language = language;
            Content = content;
            Title = title;
            Authors = authors;
            TimeStamp = timeStamp;
        }

        public ICollection<TagDTO> Tags { get; init; }
        public ICollection<RatingDTO> Ratings { get; init; }
        public ICollection<LevelDTO> Levels  { get; init; }
        public ICollection<ProgrammingLanguageDTO> ProgrammingLanguages { get; init; }
        public ICollection<MediaDTO> Medias { get; init; }
        public LanguageDTO Language { get; init; }
        public IPresentableMaterial Content { get; init; }
        [StringLength(50)]
        public string Title { get; init; }
        public ICollection<string> Authors { get; init; }
        public DateTime TimeStamp { get; init; }
    }

    public record UpdateMaterialDTO : CreateMaterialDTO
    {
        public UpdateMaterialDTO(int id, ICollection<TagDTO> tags, ICollection<RatingDTO> ratings, ICollection<LevelDTO> levels, ICollection<ProgrammingLanguageDTO> programmingLanguages, ICollection<MediaDTO> medias, LanguageDTO language, IPresentableMaterial content, string title, ICollection<string> authors, DateTime timeStamp) : base(tags, ratings, levels, programmingLanguages, medias, language, content, title, authors, timeStamp)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
