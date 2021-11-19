using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Infrastructure
{
    public class TagRepository : ITagRepository
    {
        private readonly IContext _context;
        public TagRepository(IContext context)
        {
            _context = context;
        }

        public Task<CreateTagDTO> CreateAsync(CreateTagDTO tag)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        public Task<TagDTO> ReadAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TagDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
