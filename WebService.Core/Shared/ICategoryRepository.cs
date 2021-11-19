namespace WebService.Core.Shared
{
    public interface ICategoryRepository
    {
        Task<CreateCategoryDTO> CreateAsync(CreateCategoryDTO category);
        Task<CategoryDTO> ReadAsync(int categoryId);
        Task<IReadOnlyCollection<CategoryDTO>> ReadAsync();
        Task<Status> DeleteAsync(int categoryId);
    }
}
