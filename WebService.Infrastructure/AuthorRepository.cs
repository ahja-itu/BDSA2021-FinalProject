namespace WebService.Infrastructure
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IContext _context;

        public AuthorRepository(IContext context)
        {
            _context = context;
        }
        public async Task<(Status, AuthorDTO)> CreateAsync(CreateAuthorDTO author)
        {                
            if(InvalidInput(author)) return (Status.BadRequest, new AuthorDTO(-1, author.FirstName, author.SurName));

            var existing = await (from a in _context.Authors
                                  where a.FirstName == author.FirstName
                                  where a.SurName == author.SurName
                                  select new AuthorDTO(a.Id, a.FirstName, a.SurName))
                           .FirstOrDefaultAsync();

            if (existing != null) return (Status.Conflict, existing);

            var entity = new Author(author.FirstName, author.SurName);

            _context.Authors.Add(entity);

            await _context.SaveChangesAsync();

            return (Status.Created, new AuthorDTO(entity.Id, entity.FirstName, entity.SurName));
        }

        public async Task<Status> DeleteAsync(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);

            if (author == null) return Status.NotFound;

            _context.Authors.Remove(author);

            await _context.SaveChangesAsync();

            return Status.Deleted;
        }

        public async Task<(Status, AuthorDTO)> ReadAsync(int authorId)
        {
            var query = from a in _context.Authors
                        where a.Id == authorId
                        select new AuthorDTO(a.Id, a.FirstName, a.SurName);

            var category = await query.FirstOrDefaultAsync();

            if (category == null) return (Status.NotFound, new AuthorDTO(-1, "", ""));

            return (Status.Found, category);
        }

        public async Task<IReadOnlyCollection<AuthorDTO>> ReadAsync()
        {
            return await _context.Authors.Select(a => new AuthorDTO(a.Id, a.FirstName, a.SurName)).ToListAsync();

        }

        public async Task<Status> UpdateAsync(AuthorDTO authorDTO)
        {
            if (InvalidInput(authorDTO)) return Status.BadRequest;

            var existing = await (from a in _context.Authors
                                  where a.Id != authorDTO.Id
                                  where a.FirstName == authorDTO.FirstName
                                  where a.SurName == authorDTO.SurName
                                  select new AuthorDTO(a.Id, a.FirstName, a.SurName))
                           .AnyAsync();

            if (existing) return Status.Conflict;

            var entity = await _context.Authors.FindAsync(authorDTO.Id);

            if (entity == null) return Status.NotFound;

            entity.FirstName = authorDTO.FirstName;
            entity.SurName = authorDTO.SurName;

            _context.SaveChanges();

            return Status.Updated;
        }

        private bool InvalidInput(CreateAuthorDTO author)
        {
            return (author.FirstName.Length > 50 || author.SurName.Length > 50 || string.IsNullOrEmpty(author.FirstName) || string.IsNullOrEmpty(author.SurName) || string.IsNullOrWhiteSpace(author.FirstName) || string.IsNullOrWhiteSpace(author.SurName));
        }
    }
}
