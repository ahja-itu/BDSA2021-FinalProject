namespace WebService.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? SurName { get; set; }
    }
}
