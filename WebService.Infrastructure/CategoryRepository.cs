namespace WebService.Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IContext _context;

        public CategoryRepository(IContext context)
        {
            _context = context;
        }

        public async Task<(Status,CategoryDTO)> CreateAsync(CreateCategoryDTO category)
        {
            try
            {
                var entity = new Category()
                {
                    Name = category.Name
                };

                _context.Categories.Add(entity);

                await _context.SaveChangesAsync();

                return (Status.Created, new CategoryDTO(entity.Id, category.Name));
            }
            catch (Exception)
            {
                var entity = ReadAsyncByName(category.Name).Result.Item2;

                return (Status.Conflict,entity);
            }
         
        }

        public async Task<Status> DeleteAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category == null) return Status.NotFound;

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return Status.Deleted;
        }

        public async Task<(Status,CategoryDTO)> ReadAsync(int categoryId)
        {
            try
            {
                var query = from c in _context.Categories
                            where c.Id == categoryId
                            select new CategoryDTO(c.Id, c.Name);
                var category = await query.FirstAsync();
                
                return (Status.Found,category);
            }
            catch 
            { 
                return (Status.NotFound,new CategoryDTO(-1,""));
            }
        }

        public async Task<IReadOnlyCollection<CategoryDTO>> ReadAsync()
        {
            return await _context.Categories.Select(c => new CategoryDTO(c.Id, c.Name)).ToListAsync();
        }

        public async Task<(Status, CategoryDTO)> ReadAsyncByName(string name)
        {
            try
            {
                var query = from c in _context.Categories
                            where c.Name == name
                            select new CategoryDTO(c.Id, c.Name);
                var category = await query.FirstAsync();
                return (Status.Found, category);
            }
            catch
            {
                return (Status.NotFound, new CategoryDTO(-1, ""));
            }
        }
    }
}
