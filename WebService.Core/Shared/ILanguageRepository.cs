namespace WebService.Core.Shared
{
    public interface ILanguageRepository
    {
        Task<(Status, LanguageDTO)> CreateAsync(CreateLanguageDTO language);
        Task<(Status, LanguageDTO)> ReadAsync(int languageId);
        Task<IReadOnlyCollection<LanguageDTO>> ReadAsync();
        Task<Status> DeleteAsync(int languageId);
    }
}
