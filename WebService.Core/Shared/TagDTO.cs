namespace WebService.Core.Shared
{
    public record CreateTagDTO
    {
        public CreateTagDTO(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        [StringLength(50)]
        public string Name { get; init; }
        [Range(1, 100)]
        public int Weight { get; init; }
    }

    public record TagDTO : CreateTagDTO
    {
        public TagDTO(int id, string name, int weight) : base(name, weight)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
