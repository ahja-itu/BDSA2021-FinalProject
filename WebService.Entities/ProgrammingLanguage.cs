namespace WebService.Entities;

public class ProgrammingLanguage
{
    public ProgrammingLanguage(string name)
    {
        Name = name;
    }

    public ProgrammingLanguage(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    [StringLength(50)] public string Name { get; set; }
}