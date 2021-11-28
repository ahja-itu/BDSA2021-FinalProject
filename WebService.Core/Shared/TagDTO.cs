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

    public record CreateWeightedTagDTO : CreateTagDTO
    {
        public CreateWeightedTagDTO(string name, int weight) : base(name)
        {
            Weight = weight;
        }

        [Range(1, 100)]
        public int Weight { get; init; }
    }

    public record WeightedTagDTO : CreateWeightedTagDTO
    {
        public WeightedTagDTO(int id, string name, int weight) : base(name, weight)
        {
            Id = id;
        }

        public int Id { get; init; }

    }
}
