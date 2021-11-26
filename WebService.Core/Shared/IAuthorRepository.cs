namespace WebService.Core.Shared
{
    public interface IAuthorRepository
    {
        Task<(Status, AuthorDTO)> CreateAsync(CreateAuthorDTO language);
        Task<(Status, AuthorDTO)> ReadAsync(int languageId);
        Task<IReadOnlyCollection<AuthorDTO>> ReadAsync();
        Task<Status> DeleteAsync(int languageId);
        Task<Status> UpdateAsync(AuthorDTO languageDTO);
    }
}
