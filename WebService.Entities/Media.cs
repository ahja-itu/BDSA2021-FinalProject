namespace WebService.Entities
{
    public class Media
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }
    }
}
