namespace WebService.Core.Shared;

public interface ILevelRepository : IRepository
{
    Task<(Status, LevelDTO)> CreateAsync(CreateLevelDTO level);
    Task<(Status, LevelDTO)> ReadAsync(int levelId);
    Task<IReadOnlyCollection<LevelDTO>> ReadAsync();
    Task<Status> DeleteAsync(int levelId);
    Task<Status> UpdateAsync(LevelDTO levelDTO);
}