namespace WebService.Entities
{
    public class Tag
    {
        public Tag(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public Tag(int id, string name, int weight)
        {
            Id = id;
            Name = name;
            Weight = weight;
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        [Range(1, 100)]
        public int Weight { get; set; }
    }
}
