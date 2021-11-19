namespace WebService.Core.Shared
{
    public record CreateMediaDTO
    {
        public CreateMediaDTO(string name)
        {
            Name = name;
        }

        [StringLength(50)]
        public string Name { get; init; }
    }

    public record MediaDTO : CreateMediaDTO
    {
        public MediaDTO(int id, string name) : base(name)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
