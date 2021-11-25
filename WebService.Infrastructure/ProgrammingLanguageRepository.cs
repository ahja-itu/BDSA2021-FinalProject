﻿namespace WebService.Infrastructure
{
    public class ProgrammingLanguageRepository : IProgrammingLanguageRespository
    {
        private readonly IContext _context;
        public ProgrammingLanguageRepository(IContext context)
        {
            _context = context;
        }

        public async Task<(Status, ProgrammingLanguageDTO)> CreateAsync(CreateProgrammingLanguageDTO programmingLanguage)
        {
            var existing = await(from l in _context.ProgrammingLanguages
                                 where l.Name == programmingLanguage.Name
                                 select new ProgrammingLanguageDTO(l.Id, l.Name))
               .FirstOrDefaultAsync();

            if (existing != null) return (Status.Conflict, existing);

            var entity = new ProgrammingLanguage(programmingLanguage.Name);

            _context.ProgrammingLanguages.Add(entity);

            await _context.SaveChangesAsync();

            return (Status.Created, new ProgrammingLanguageDTO(entity.Id, entity.Name));
        }

        public async Task<Status> DeleteAsync(int programmingLanguageId)
        {
            var language = await _context.ProgrammingLanguages.FindAsync(programmingLanguageId);

            if (language == null) return Status.NotFound;

            _context.ProgrammingLanguages.Remove(language);

            await _context.SaveChangesAsync();

            return Status.Deleted;
        }

        public async Task<(Status, ProgrammingLanguageDTO)> ReadAsync(int programmingLanguageId)
        {
            var query = from l in _context.ProgrammingLanguages
                        where l.Id == programmingLanguageId
                        select new ProgrammingLanguageDTO(l.Id, l.Name);

            var category = await query.FirstOrDefaultAsync();

            if (category == null) return (Status.NotFound, new ProgrammingLanguageDTO(-1, ""));

            return (Status.Found, category);
        }

        public async Task<IReadOnlyCollection<ProgrammingLanguageDTO>> ReadAsync()
        {
            return await _context.ProgrammingLanguages.Select(l => new ProgrammingLanguageDTO(l.Id, l.Name)).ToListAsync();
        }

        public async Task<Status> UpdateAsync(ProgrammingLanguageDTO programmingProgrammingLanguageDTO)
        {
            var existing = await(from l in _context.ProgrammingLanguages
                                 where l.Id != programmingProgrammingLanguageDTO.Id
                                 where l.Name == programmingProgrammingLanguageDTO.Name
                                 select new ProgrammingLanguageDTO(l.Id, l.Name))
                                      .AnyAsync();

            if (existing) return Status.Conflict;

            var entity = await _context.ProgrammingLanguages.FindAsync(programmingProgrammingLanguageDTO.Id);

            if (entity == null) return Status.NotFound;

            entity.Name = programmingProgrammingLanguageDTO.Name;

            _context.SaveChanges();

            return Status.Updated;
        }
    }
}