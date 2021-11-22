namespace WebService.Entities
{
    public class Rating
    {
        public Rating(int value)
        {
            Value = value;
        }

        public int Id { get; set; }

        [Range(1, 10)]
        public int Value { get; init; }
    }
}
