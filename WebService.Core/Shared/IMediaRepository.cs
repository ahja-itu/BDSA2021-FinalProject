namespace WebService.Core.Shared
{
    public interface IMediaRepository
    {
        Task<(Status,MediaDTO)> CreateAsync(CreateMediaDTO media);
        Task<(Status,MediaDTO)> ReadAsync(int mediaId);
        Task<IReadOnlyCollection<MediaDTO>> ReadAsync();
        Task<Status> DeleteAsync(int mediaId);
    }
}
