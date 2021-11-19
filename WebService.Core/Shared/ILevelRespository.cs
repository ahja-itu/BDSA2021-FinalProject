using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Core.Shared
{
    public interface ILevelRespository
    {
        Task<CreateLevelDTO> CreateAsync(CreateLevelDTO level);
        Task<LevelDTO> ReadAsync(int levelId);
        Task<IReadOnlyCollection<LevelDTO>> ReadAsync();
        Task<Status> DeleteAsync(int levelId);
    }
}
