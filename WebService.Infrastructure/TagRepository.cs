namespace WebService.Infrastructure
{
    public class TagRepository : ITagRepository
    {
        private readonly IContext _context;
        public TagRepository(IContext context)
        {
            _context = context;
        }

        public Task<(Status, TagDTO)> CreateAsync(CreateTagDTO tag)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        public Task<(Status, TagDTO)> ReadAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TagDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> UpdateAsync(TagDTO tagDTO)
        {
            throw new NotImplementedException();
        }
    }
}
