namespace WebService.Core.Shared;

public record CreateAuthorDTO
{
    public CreateAuthorDTO(string firstName, string surName)
    {
        FirstName = firstName;
        SurName = surName;
    }

    [StringLength(50)] public string FirstName { get; }

    [StringLength(50)] public string SurName { get; }
}

public record AuthorDTO : CreateAuthorDTO
{
    public AuthorDTO(int id, string firstName, string surName) : base(firstName, surName)
    {
        Id = id;
    }

    public int Id { get; }
}