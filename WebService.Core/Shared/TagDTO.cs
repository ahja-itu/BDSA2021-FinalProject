namespace WebService.Core.Shared
{
    public record CreateTagDTO
    {
        public CreateTagDTO(string name)
        {
            Name = name;
        }

        [StringLength(50)]
        public string Name { get; init; }
    }

    public record TagDTO : CreateTagDTO
    {
        public TagDTO(int id, string name) : base(name)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
