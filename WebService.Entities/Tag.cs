namespace WebService.Entities
{
    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
