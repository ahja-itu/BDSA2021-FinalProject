namespace WebService.Infrastructure
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly IContext _context;

        public MaterialRepository(IContext context)
        {
            _context = context;
        }

        public Task<(Status, MaterialDTO)> CreateAsync(CreateMaterialDTO material)
        {
            var entity = new Material()
            {
                Tags = new List<Tag> { tag1 },
                Ratings = new List<Rating> { rating1 },
                Levels = new List<Level> { level1 },
                ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguage1 },
                Medias = new List<Media> { media1 },
                Language = language1,
                Content = content1,
                Title = "Material 1",
                Authors = new List<Author> { author1 },
                TimeStamp = System.DateTime.UtcNow
            }
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int materialId)
        {
            throw new NotImplementedException();
        }

        public Task<(Status, MaterialDTO)> ReadAsync(int materialId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<MaterialDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(Status, IReadOnlyCollection<MaterialDTO>)> ReadAsync(SearchForm searchInput)
        {
            throw new NotImplementedException();
        }

        public Task<Status> UpdateAsync(UpdateMaterialDTO materialId)
        {
            throw new NotImplementedException();
        }
    }
}
