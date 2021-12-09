namespace WebService.Core.Shared
{
    public interface IMaterialRepository : IRepository
    {
        Task<(Status, MaterialDTO)> CreateAsync(CreateMaterialDTO material);
        Task<(Status, MaterialDTO)> ReadAsync(int materialId);
        Task<(Status, IReadOnlyCollection<MaterialDTO>)> ReadAsync(SearchForm searchInput);
        Task<IReadOnlyCollection<MaterialDTO>> ReadAsync();
        Task<Status> UpdateAsync(MaterialDTO materialId);
        Task<Status> DeleteAsync(int materialId);
    }
}
