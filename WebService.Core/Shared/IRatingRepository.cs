namespace WebService.Core.Shared
{
    public interface IRatingRepository
    {
        Task<(Status, RatingDTO)> CreateAsync(CreateRatingDTO rating);
        Task<(Status, RatingDTO)> ReadAsync(int ratingId);
        Task<IReadOnlyCollection<RatingDTO>> ReadAsync();
        Task<Status> DeleteAsync(int ratingId);
        Task<Status> UpdateAsync(RatingDTO ratingDTO);
    }
}
