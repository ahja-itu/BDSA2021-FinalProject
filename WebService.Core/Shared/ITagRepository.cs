namespace WebService.Core.Shared
{
    public interface ITagRepository
    {
        Task<(Status, TagDTO)> CreateAsync(CreateTagDTO tag);
        Task<(Status, TagDTO)> ReadAsync(int tagId);
        Task<IReadOnlyCollection<TagDTO>> ReadAsync();
        Task<Status> DeleteAsync(int tagId);
    }
}
