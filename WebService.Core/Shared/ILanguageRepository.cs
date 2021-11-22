namespace WebService.Core.Shared
{
    public interface ILanguageRepository
    {
        Task<CreateLanguageDTO> CreateAsync(CreateLanguageDTO language);
        Task<LanguageDTO> ReadAsync(int languageId);
        Task<IReadOnlyCollection<LanguageDTO>> ReadAsync();
        Task<Status> DeleteAsync(int languageId);
    }
}
