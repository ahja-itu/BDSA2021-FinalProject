namespace WebService.Core.Shared
{
    public interface IMediaRepository
    {
        Task<CreateMediaDTO> CreateAsync(CreateMediaDTO media);
        Task<MediaDTO> ReadAsync(int mediaId);
        Task<IReadOnlyCollection<MediaDTO>> ReadAsync();
        Task<Status> DeleteAsync(int mediaId);
    }
}
