namespace WebService.Entities
{
    public class Level
    {
        public Level(string educationLevel)
        {
            EducationLevel = educationLevel;
        }

        public Level(int id, string educationLevel)
        {
            Id = id;
            EducationLevel = educationLevel;
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string EducationLevel { get; set; }
    }
}
