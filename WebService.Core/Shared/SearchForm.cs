namespace WebService.Core.Shared
{
    public class SearchForm
    {
        public SearchForm(string textField, IEnumerable<TagDTO> tags, IEnumerable<LevelDTO> levels, IEnumerable<ProgrammingLanguageDTO> programmingLanguages, IEnumerable<LanguageDTO> languages, IEnumerable<MediaDTO> medias, int rating)
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
        public IEnumerable<TagDTO> Tags { get; set; }
        public IEnumerable<LevelDTO> Levels { get; set; }
        public IEnumerable<ProgrammingLanguageDTO> ProgrammingLanguages { get; set; }
        public IEnumerable<LanguageDTO> Languages { get; set; }
        public IEnumerable<MediaDTO> Medias { get; set; }
        public int Rating { get; set; }
    }
}
