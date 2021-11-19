namespace WebService.Core.Shared
{
    public interface IRatingRepository
    {
        Task<CreateRatingDTO> CreateAsync(CreateRatingDTO rating);
        Task<RatingDTO> ReadAsync(int ratingId);
        Task<IReadOnlyCollection<RatingDTO>> ReadAsync();
        Task<RatingDTO> DeleteAsync(int ratingId);
    }
}
