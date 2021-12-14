namespace WebService.Core.Shared;

public record CreateRatingDTO
{
    public CreateRatingDTO(int value, string reviewer)
    {
        Value = value;
        Reviewer = reviewer;
        TimeStamp = DateTime.UtcNow;
    }

    [Range(1, 10)] public int Value { get; init; }

    public string Reviewer { get; init; }
    public DateTime TimeStamp { get; init; }
}

public record RatingDTO : CreateRatingDTO
{
    public RatingDTO(int id, int value, string reviewer) : base(value, reviewer)
    {
        Id = id;
    }

    public int Id { get; init; }
}