namespace WebService.Core.Shared
{
    public class SearchForm
    {
        public SearchForm(string textField, ICollection<TagDTO> tags, ICollection<LevelDTO> levels, ICollection<ProgrammingLanguageDTO> programmingLanguages, ICollection<LanguageDTO> languages, ICollection<MediaDTO> medias, int rating)
        {
            TextField = textField;
            Tags = tags;
            Levels = levels;
            ProgrammingLanguages = programmingLanguages;
            Languages = languages;
            Medias = medias;
            Rating = rating;
        }
        public string TextField { get; set; }
        public ICollection<TagDTO> Tags { get; set; }
        public ICollection<LevelDTO> Levels { get; set; }
        public ICollection<ProgrammingLanguageDTO> ProgrammingLanguages { get; set; }
        public ICollection<LanguageDTO> Languages { get; set; }
        public ICollection<MediaDTO> Medias { get; set; }
        public int Rating { get; set; }
    }
}
