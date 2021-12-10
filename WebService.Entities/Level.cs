namespace WebService.Entities;

public class Level
{
    public Level(string name)
    {
        Name = name;
    }

    public Level(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public ICollection<Material> Materials { get; set; } = new List<Material>();

    [StringLength(50)] public string Name { get; set; }
}