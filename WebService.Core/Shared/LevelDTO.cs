namespace WebService.Core.Shared
{
    public record CreateLevelDTO
    {
        public CreateLevelDTO(string educationLevel)
        {
            EducationLevel = educationLevel;
        }

        [StringLength(50)]
        public string EducationLevel { get; init; }
    }

    public record LevelDTO : CreateLevelDTO
    {
        public LevelDTO(int id, string level) : base(level)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
