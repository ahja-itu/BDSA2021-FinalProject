namespace WebService.Entities
{
    public class Author
    {
        public Author(string firstName, string surName)
        {
            FirstName = firstName;
            SurName = surName;
        }

        public Author(int id, string firstName, string surName)
        {
            Id = id;
            FirstName = firstName;
            SurName = surName;
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string SurName { get; set; }
    }
}
