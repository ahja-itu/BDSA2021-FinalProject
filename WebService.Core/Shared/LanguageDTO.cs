namespace WebService.Core.Shared
{
    public record CreateLanguageDTO
    {
        public CreateLanguageDTO(string name)
        {
            Name = name;
        }

        [StringLength(50)]
        public string Name { get; init; }
    }

    public record LanguageDTO : CreateLanguageDTO
    {
        public LanguageDTO(int id, string name) : base(name)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
