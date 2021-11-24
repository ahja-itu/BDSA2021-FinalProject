using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Infrastructure
{
    public class MediaRepository : IMediaRepository
    {
        private readonly IContext _context;
        public MediaRepository(IContext context)
        {
            _context = context;
        }

        public Task<(Status,MediaDTO)> CreateAsync(CreateMediaDTO media)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int mediaId)
        {
            throw new NotImplementedException();
        }

        public Task<(Status,MediaDTO)> ReadAsync(int mediaId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<MediaDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
