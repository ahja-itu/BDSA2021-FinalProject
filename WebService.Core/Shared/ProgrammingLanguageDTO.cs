namespace WebService.Core.Shared;

public record CreateProgrammingLanguageDTO
{
    public CreateProgrammingLanguageDTO(string name)
    {
        Name = name;
    }

    [StringLength(50)] public string Name { get; }
}

public record ProgrammingLanguageDTO : CreateProgrammingLanguageDTO
{
    public ProgrammingLanguageDTO(int id, string name) : base(name)
    {
        Id = id;
    }

    public int Id { get; }
}