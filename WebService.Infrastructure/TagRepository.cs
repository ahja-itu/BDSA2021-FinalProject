namespace WebService.Infrastructure
{
    public class TagRepository : ITagRepository
    {
        private readonly IContext _context;
        public TagRepository(IContext context)
        {
            _context = context;
        }

        public async Task<(Status, TagDTO)> CreateAsync(CreateTagDTO tag)
        {
            if (InvalidInput(tag)) return (Status.BadRequest, new TagDTO(-1, tag.Name));

            var existing = await (from t in _context.Tags
                                  where t.Name == tag.Name
                                  select new TagDTO(t.Id, t.Name))
                           .FirstOrDefaultAsync();

            if (existing != null) return (Status.Conflict, existing);

            var entity = new Tag(tag.Name);

            _context.Tags.Add(entity);

            await _context.SaveChangesAsync();

            return (Status.Created, new TagDTO(entity.Id, entity.Name));
        }

        public async Task<Status> DeleteAsync(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);

            if (tag == null) return Status.NotFound;

            _context.Tags.Remove(tag);

            await _context.SaveChangesAsync();

            return Status.Deleted;
        }

        public async Task<(Status, TagDTO)> ReadAsync(int tagId)
        {
            var query = from t in _context.Tags
                        where t.Id == tagId
                        select new TagDTO(t.Id, t.Name);

            var category = await query.FirstOrDefaultAsync();

            if (category == null) return (Status.NotFound, new TagDTO(-1, ""));

            return (Status.Found, category);
        }

        public async Task<IReadOnlyCollection<TagDTO>> ReadAsync()
        {
            return await _context.Tags.Select(t => new TagDTO(t.Id, t.Name)).ToListAsync();

        }

        public async Task<Status> UpdateAsync(TagDTO tagDTO)
        {
            if (InvalidInput(tagDTO)) return Status.BadRequest;

            var existing = await (from t in _context.Tags
                                  where t.Id != tagDTO.Id
                                  where t.Name == tagDTO.Name
                                  select new TagDTO(t.Id, t.Name))
                          .AnyAsync();

            if (existing) return Status.Conflict;

            var entity = await _context.Tags.FindAsync(tagDTO.Id);

            if (entity == null) return Status.NotFound;

            entity.Name = tagDTO.Name;

            await _context.SaveChangesAsync();

            return Status.Updated;
        }

        private bool InvalidInput(CreateTagDTO tag)
        {
            return (tag.Name.Length > 50 || tag.Name.Length > 50 || string.IsNullOrEmpty(tag.Name) || string.IsNullOrEmpty(tag.Name) || string.IsNullOrWhiteSpace(tag.Name) || string.IsNullOrWhiteSpace(tag.Name));
        }
    }
}
