namespace WebService.Infrastructure
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IContext _context;

        public AuthorRepository(IContext context)
        {
            _context = context;
        }
        public Task<(Status, AuthorDTO)> CreateAsync(CreateAuthorDTO language)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int languageId)
        {
            throw new NotImplementedException();
        }

        public Task<(Status, AuthorDTO)> ReadAsync(int languageId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<AuthorDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> UpdateAsync(AuthorDTO languageDTO)
        {
            throw new NotImplementedException();
        }
    }
}
