namespace WebService.Core.Shared
{
    public interface IMaterialRepository
    {
        Task<CreateMaterialDTO> CreateAsync(CreateMaterialDTO material);
        Task<MaterialDTO> ReadAsync(int materialId);
        Task<IReadOnlyCollection<MaterialDTO>> ReadAsync();
        Task<Status> UpdateAsync(int id, UpdateMaterialDTO materialId);
        Task<Status> DeleteAsync(int materialId);
    }
}
