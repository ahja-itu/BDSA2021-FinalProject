namespace WebService.Core.Shared;

public record CreateLevelDTO
{
    public CreateLevelDTO(string name)
    {
        Name = name;
    }

    [StringLength(50)]
    public string Name { get; init; }
}

public record LevelDTO : CreateLevelDTO
{
    public LevelDTO(int id, string name) : base(name)
    {
        Id = id;
    }

    public int Id { get; init; }
}