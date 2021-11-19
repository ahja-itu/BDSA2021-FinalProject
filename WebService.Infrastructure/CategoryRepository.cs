namespace WebService.Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IContext _context;

        public CategoryRepository(IContext context)
        {
            _context = context;
        }

        public Task<CreateCategoryDTO> CreateAsync(CreateCategoryDTO category)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO> ReadAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<CategoryDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
