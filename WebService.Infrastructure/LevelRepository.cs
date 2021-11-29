namespace WebService.Infrastructure
{
    public class LevelRepository : ILevelRespository
    {
        private readonly IContext _context;

        public LevelRepository(IContext context)
        {
            _context = context;
        }

        public async Task<(Status, LevelDTO)> CreateAsync(CreateLevelDTO level)
        {
            if (InvalidInput(level)) return (Status.BadRequest, new LevelDTO(-1, level.Name));

            var existing = await (from l in _context.Levels
                                  where l.Name == level.Name
                                  select new LevelDTO(l.Id, l.Name))
                           .FirstOrDefaultAsync();
            if (existing != null) return (Status.Conflict, existing);

            var entity = new Level(level.Name);

            _context.Levels.Add(entity);

            await _context.SaveChangesAsync();

            return (Status.Created, new LevelDTO(entity.Id, entity.Name));
        }

        public async Task<Status> DeleteAsync(int levelId)
        {
            var level = await _context.Levels.FindAsync(levelId);

            if (level == null) return Status.NotFound;

            _context.Levels.Remove(level);

            await _context.SaveChangesAsync();

            return Status.Deleted;
        }

        public async Task<(Status, LevelDTO)> ReadAsync(int levelId)
        {
            var query = from l in _context.Levels
                        where l.Id == levelId
                        select new LevelDTO(l.Id, l.Name);

            var category = await query.FirstOrDefaultAsync();

            if (category == null) return (Status.NotFound, new LevelDTO(-1, ""));

            return (Status.Found, category);
        }

        public async Task<IReadOnlyCollection<LevelDTO>> ReadAsync()
        {
            return await _context.Levels.Select(l => new LevelDTO(l.Id, l.Name)).ToListAsync();
        }

        public async Task<Status> UpdateAsync(LevelDTO levelDTO)
        {
            if (InvalidInput(levelDTO)) return Status.BadRequest;

            var existing = await (from l in _context.Levels
                                  where l.Id != levelDTO.Id
                                  where l.Name == levelDTO.Name
                                  select new LevelDTO(l.Id, l.Name))
                                     .AnyAsync();

            if (existing) return Status.Conflict;

            var entity = await _context.Levels.FindAsync(levelDTO.Id);

            if (entity == null) return Status.NotFound;

            entity.Name = levelDTO.Name;

            await _context.SaveChangesAsync();

            return Status.Updated;
        }

        private bool InvalidInput(CreateLevelDTO level)
        {
            return (level.Name.Length > 50 || level.Name.Length > 50 || string.IsNullOrEmpty(level.Name) || string.IsNullOrEmpty(level.Name) || string.IsNullOrWhiteSpace(level.Name) || string.IsNullOrWhiteSpace(level.Name));
        }
    }
}
