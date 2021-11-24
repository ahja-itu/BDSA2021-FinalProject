namespace WebService.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 10)]
        public int? Value { get; init; }
    }
}
