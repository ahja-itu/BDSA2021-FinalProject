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
            var existing = await(from l in _context.Levels
                                 where l.EducationLevel == level.EducationLevel
                                 select new LevelDTO(l.Id, l.EducationLevel))
                           .FirstOrDefaultAsync();
            if (existing != null) return (Status.Conflict, existing);

            var entity = new Level(level.EducationLevel);

            _context.Levels.Add(entity);

            await _context.SaveChangesAsync();

            return (Status.Created, new LevelDTO(entity.Id, entity.EducationLevel));
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
                        select new LevelDTO(l.Id, l.EducationLevel);

            var category = await query.FirstOrDefaultAsync();

            if (category == null) return (Status.NotFound, new LevelDTO(-1, ""));

            return (Status.Found, category);
        }

        public async Task<IReadOnlyCollection<LevelDTO>> ReadAsync()
        {
            return await _context.Levels.Select(l => new LevelDTO(l.Id, l.EducationLevel)).ToListAsync();
        }

        public async Task<Status> UpdateAsync(LevelDTO levelDTO)
        {
            var existing = await (from l in _context.Levels
                                  where l.Id != levelDTO.Id
                                  where l.EducationLevel == levelDTO.EducationLevel
                                  select new LevelDTO(l.Id, l.EducationLevel))
                                     .AnyAsync();

            if (existing) return Status.Conflict;

            var entity = await _context.Levels.FindAsync(levelDTO.Id);

            if (entity == null) return Status.NotFound;

            entity.EducationLevel = levelDTO.EducationLevel;

            _context.SaveChanges();

            return Status.Updated;
        }
    }
}
