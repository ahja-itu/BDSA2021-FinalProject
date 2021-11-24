namespace WebService.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Level>? Levels { get; set; }
        public ICollection<ProgrammingLanguage>? ProgrammingLanguages { get; set; }
        public ICollection<Media>? Medias { get; set; }
        public Language? Language { get; set; }
        [NotMapped]
        public IPresentableMaterial? Content { get; set; }
        [StringLength(50)]
        public string? Title { get; set; }
        public ICollection<Author>? Authors { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
