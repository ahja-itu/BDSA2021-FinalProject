namespace WebService.Core.Shared;

public interface IProgrammingLanguageRepository : IRepository
{
    Task<(Status, ProgrammingLanguageDTO)> CreateAsync(CreateProgrammingLanguageDTO programmingLanguage);
    Task<(Status, ProgrammingLanguageDTO)> ReadAsync(int programmingLanguageId);
    Task<IReadOnlyCollection<ProgrammingLanguageDTO>> ReadAsync();
    Task<Status> DeleteAsync(int programmingLanguageId);
    Task<Status> UpdateAsync(ProgrammingLanguageDTO programmingLanguageDTO);
}