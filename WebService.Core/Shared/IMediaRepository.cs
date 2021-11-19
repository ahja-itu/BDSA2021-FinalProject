using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Core.Shared
{
    public interface IMediaRepository
    {
        Task<CreateMediaDTO> CreateAsync(CreateMediaDTO media);
        Task<MediaDTO> ReadAsync(int mediaId);
        Task<IReadOnlyCollection<MediaDTO>> ReadAsync();
        Task<Status> DeleteAsync(int mediaId);
    }
}
