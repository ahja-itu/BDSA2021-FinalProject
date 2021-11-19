using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Core.Shared
{
    public interface ITagRepository
    {
        Task<CreateTagDTO> CreateAsync(CreateTagDTO tag);
        Task<TagDTO> ReadAsync(int tagId);
        Task<IReadOnlyCollection<TagDTO>> ReadAsync();
        Task<Status> DeleteAsync(int tagId);
    }
}
