namespace WebService.Entities
{
    public class Language
    {
        public Language(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
