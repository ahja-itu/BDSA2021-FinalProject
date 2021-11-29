namespace WebService.Core.Shared
{
    public class SearchForm
    {
        public string? TextField { get; set; }
        public ICollection<TagDTO>? Tags { get; set; }
        public ICollection<LevelDTO>? Levels { get; set; }
        public ICollection<ProgrammingLanguageDTO>? ProgrammingLanguages { get; set; }
        public ICollection<LanguageDTO>? Languages { get; set; }
        public ICollection<MediaDTO>? Medias { get; set; }
        public int Rating{ get; set; }
    }
}
