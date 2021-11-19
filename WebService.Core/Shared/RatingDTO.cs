namespace WebService.Core.Shared
{
    public record CreateRatingDTO
    {
        public CreateRatingDTO(int value)
        {
            Value = value;
        }

        [Range(1,10)]
        public int Value { get; init; }
    }

    public record RatingDTO : CreateRatingDTO
    {
        public RatingDTO(int id, int value) : base(value)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
