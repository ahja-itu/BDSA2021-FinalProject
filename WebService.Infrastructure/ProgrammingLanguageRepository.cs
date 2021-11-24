using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Infrastructure
{
    public class ProgrammingLanguageRepository : IProgrammingLanguageRespository
    {
        private readonly IContext _context;
        public ProgrammingLanguageRepository(IContext context)
        {
            _context = context;
        }

        public Task<(Status,ProgrammingLanguageDTO)> CreateAsync(CreateProgrammingLanguageDTO programmingLanguage)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int programmingLanguageId)
        {
            throw new NotImplementedException();
        }

        public Task<(Status,ProgrammingLanguageDTO)> ReadAsync(int programmingLanguageId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<ProgrammingLanguageDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
