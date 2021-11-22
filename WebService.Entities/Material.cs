namespace WebService.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<Rating> Ratings { get; set; }
        public IList<Level> Levels { get; set; }
        public IList<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public IList<Media> Medias { get; set; }
        public Language Language { get; set; }
        public IPresentableMaterial Content { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public IList<string> Authors { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
