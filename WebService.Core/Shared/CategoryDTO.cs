namespace WebService.Core.Shared
{
    public record CreateCategoryDTO
    {
        public CreateCategoryDTO(string name)
        {
            Name = name;
        }

        [StringLength(50)]
        public string Name { get; init; }
    }

    public record CategoryDTO : CreateCategoryDTO
    {
        public CategoryDTO(int id, string name) : base(name)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
