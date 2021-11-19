namespace WebService.Infrastructure
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly IContext _context;

        public LanguageRepository(IContext context)
        {
            _context = context;
        }

        public Task<CreateLanguageDTO> CreateAsync(CreateLanguageDTO language)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int languageId)
        {
            throw new NotImplementedException();
        }

        public Task<LanguageDTO> ReadAsync(int languageId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<LanguageDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
