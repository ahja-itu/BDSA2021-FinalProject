namespace WebService.Entities
{
    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
