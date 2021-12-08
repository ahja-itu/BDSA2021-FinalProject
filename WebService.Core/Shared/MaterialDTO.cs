namespace WebService.Core.Shared
{
    public record MaterialDTO : CreateMaterialDTO
    {
        public MaterialDTO(int id, ICollection<CreateWeightedTagDTO> tags, ICollection<CreateRatingDTO> ratings, ICollection<CreateLevelDTO> levels, ICollection<CreateProgrammingLanguageDTO> programmingLanguages, ICollection<CreateMediaDTO> medias, CreateLanguageDTO language, string summary, string url, string content, string title, ICollection<CreateAuthorDTO> authors, DateTime timeStamp) : base(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, timeStamp)
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
