namespace WebService.Infrastructure
{
    public class MediaRepository : IMediaRepository
    {
        private readonly IContext _context;
        public MediaRepository(IContext context)
        {
            _context = context;
        }

        public async Task<(Status, MediaDTO)> CreateAsync(CreateMediaDTO media)
        {
            var existing = await (from m in _context.Medias
                                  where m.Name == media.Name
                                  select new MediaDTO(m.Id, m.Name))
                          .FirstOrDefaultAsync();

            if (existing != null) return (Status.Conflict, existing);

            var entity = new Media(media.Name);

            _context.Medias.Add(entity);

            await _context.SaveChangesAsync();

            return (Status.Created, new MediaDTO(entity.Id, entity.Name));
        }

        public async Task<Status> DeleteAsync(int mediaId)
        {
            var media = await _context.Medias.FindAsync(mediaId);

            if (media == null) return Status.NotFound;

            _context.Medias.Remove(media);

            await _context.SaveChangesAsync();

            return Status.Deleted;
        }

        public async Task<(Status, MediaDTO)> ReadAsync(int mediaId)
        {
            var query = from m in _context.Medias
                        where m.Id == mediaId
                        select new MediaDTO(m.Id, m.Name);

            var category = await query.FirstOrDefaultAsync();

            if (category == null) return (Status.NotFound, new MediaDTO(-1, ""));

            return (Status.Found, category);
        }

        public async Task<IReadOnlyCollection<MediaDTO>> ReadAsync()
        {
            return await _context.Medias.Select(m => new MediaDTO(m.Id, m.Name)).ToListAsync();
        }

        public async Task<Status> UpdateAsync(MediaDTO mediaDTO)
        {
            var existing = await (from m in _context.Medias
                                  where m.Id != mediaDTO.Id
                                  where m.Name == mediaDTO.Name
                                  select new MediaDTO(m.Id, m.Name))
                                      .AnyAsync();

            if (existing) return Status.Conflict;

            var entity = await _context.Medias.FindAsync(mediaDTO.Id);

            if (entity == null) return Status.NotFound;

            entity.Name = mediaDTO.Name;

            _context.SaveChanges();

            return Status.Updated;
        }
    }
}
