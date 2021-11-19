using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Core.Shared
{
    public interface IProgrammingLanguageRespository
    {
        Task<CreateProgrammingLanguageDTO> CreateAsync(CreateProgrammingLanguageDTO programmingLanguage);
        Task<ProgrammingLanguageDTO> ReadAsync(int programmingLanguageId);
        Task<IReadOnlyCollection<ProgrammingLanguageDTO>> ReadAsync();
        Task<Status> DeleteAsync(int programmingLanguageId);
    }
}
