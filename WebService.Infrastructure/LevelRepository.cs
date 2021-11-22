namespace WebService.Infrastructure
{
    public class LevelRepository : ILevelRespository
    {
        private readonly IContext _context;

        public LevelRepository(IContext context)
        {
            _context = context;
        }

        public Task<CreateLevelDTO> CreateAsync(CreateLevelDTO level)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int levelId)
        {
            throw new NotImplementedException();
        }

        public Task<LevelDTO> ReadAsync(int levelId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<LevelDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
