namespace WebService.Entities
{
    public class Media
    {
        public Media(string name)
        {
            Name = name;
        }

        public Media(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
