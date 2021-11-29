namespace WebService.Entities
{
    [Owned]
    public class Rating
    {
        public Rating(int value, string reviewer)
        {
            Value = value;
            Reviewer = reviewer;
            TimeStamp = DateTime.UtcNow;
        }

        public Rating(int id, int value, string reviewer)
        {
            Id = id;
            Value = value;
            Reviewer = reviewer;
            TimeStamp = DateTime.UtcNow;
        }

        public int Id { get; set; }

        [Range(1, 10)]
        public int Value { get; init; }
        [StringLength(50)]
        public string Reviewer { get; init; }
        public DateTime TimeStamp { get; init; }
    }
}
