namespace WebService.Core.Shared
{
    public interface ICategoryRepository
    {
        Task<(Status,CategoryDTO)> CreateAsync(CreateCategoryDTO category);
        Task<(Status,CategoryDTO)> ReadAsync(int categoryId);
        Task<(Status, CategoryDTO)> ReadAsyncByName(string name);
        Task<IReadOnlyCollection<CategoryDTO>> ReadAsync();
        Task<Status> DeleteAsync(int categoryId);
    }
}
