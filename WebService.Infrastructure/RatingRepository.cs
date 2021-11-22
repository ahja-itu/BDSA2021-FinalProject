using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Infrastructure
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IContext _context;
        public RatingRepository(IContext context)
        {
            _context = context;
        }

        public Task<CreateRatingDTO> CreateAsync(CreateRatingDTO rating)
        {
            throw new NotImplementedException();
        }

        public Task<RatingDTO> DeleteAsync(int ratingId)
        {
            throw new NotImplementedException();
        }

        public Task<RatingDTO> ReadAsync(int ratingId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<RatingDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
