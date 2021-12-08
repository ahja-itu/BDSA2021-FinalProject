namespace WebService.Entities;

public class Tag
{
    public Tag(string name)
    {
        Name = name;
    }

    public Tag(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public ICollection<Material> Materials { get; set; } = new List<Material>();


    [StringLength(50)] public string Name { get; set; }
}

[Owned]
public class WeightedTag : Tag
{
    public WeightedTag(string name, int weight) : base(name)
    {
        Weight = weight;
    }

    public WeightedTag(int id, string name, int weight) : base(name)
    {
        Id = id;
        Weight = weight;
    }

    [Range(1, 100)] public int Weight { get; set; }
}