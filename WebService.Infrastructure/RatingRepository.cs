namespace WebService.Infrastructure
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IContext _context;
        public RatingRepository(IContext context)
        {
            _context = context;
        }

        public async Task<(Status, RatingDTO)> CreateAsync(CreateRatingDTO rating)
        {
            var existing = await (from r in _context.Ratings
                                  where r.Value == rating.Value
                                  select new RatingDTO(r.Id, r.Value))
                           .FirstOrDefaultAsync();

            if (existing != null) return (Status.Conflict, existing);

            var entity = new Rating(rating.Value);

            _context.Ratings.Add(entity);

            await _context.SaveChangesAsync();

            return (Status.Created, new RatingDTO(entity.Id, entity.Value));
        }

        public async Task<Status> DeleteAsync(int ratingId)
        {
            var rating = await _context.Ratings.FindAsync(ratingId);

            if (rating == null) return Status.NotFound;

            _context.Ratings.Remove(rating);

            await _context.SaveChangesAsync();

            return Status.Deleted;
        }

        public async Task<(Status, RatingDTO)> ReadAsync(int ratingId)
        {
            var query = from r in _context.Ratings
                        where r.Id == ratingId
                        select new RatingDTO(r.Id, r.Value);

            var category = await query.FirstOrDefaultAsync();

            if (category == null) return (Status.NotFound, new RatingDTO(-1, 0));

            return (Status.Found, category);
        }

        public async Task<IReadOnlyCollection<RatingDTO>> ReadAsync()
        {
            return await _context.Ratings.Select(r => new RatingDTO(r.Id, r.Value)).ToListAsync();
        }

        public async Task<Status> UpdateAsync(RatingDTO ratingDTO)
        {
            var existing = await (from r in _context.Ratings
                                  where r.Id != ratingDTO.Id
                                  where r.Value == ratingDTO.Value
                                  select new RatingDTO(r.Id, r.Value))
                           .AnyAsync();

            if (existing) return Status.Conflict;

            var entity = await _context.Ratings.FindAsync(ratingDTO.Id);

            if (entity == null) return Status.NotFound;

            entity.Value = ratingDTO.Value;

            _context.SaveChanges();

            return Status.Updated;
        }
    }
}
