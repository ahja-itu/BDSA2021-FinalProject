namespace WebService.Infrastructure
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly IContext _context;

        public MaterialRepository(IContext context)
        {
            _context = context;
        }

        public Task<CreateMaterialDTO> CreateAsync(CreateMaterialDTO material)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int materialId)
        {
            throw new NotImplementedException();
        }

        public Task<MaterialDTO> ReadAsync(int materialId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<MaterialDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> UpdateAsync(int id, UpdateMaterialDTO materialId)
        {
            throw new NotImplementedException();
        }
    }
}
