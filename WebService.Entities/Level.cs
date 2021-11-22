namespace WebService.Entities
{
    public class Level
    {
        public Level(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
