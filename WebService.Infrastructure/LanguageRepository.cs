namespace WebService.Infrastructure;

public class LanguageRepository : ILanguageRepository
{
    private readonly IContext _context;

    public LanguageRepository(IContext context)
    {
        _context = context;
    }

    public async Task<(Status, LanguageDTO)> CreateAsync(CreateLanguageDTO language)
    {
        if (InvalidInput(language)) return (Status.BadRequest, new LanguageDTO(-1, language.Name));

        var existing = await (from l in _context.Languages
                where l.Name == language.Name
                select new LanguageDTO(l.Id, l.Name))
            .FirstOrDefaultAsync();

        if (existing != null) return (Status.Conflict, existing);

        var entity = new Language(language.Name);

        _context.Languages.Add(entity);

        await _context.SaveChangesAsync();

        return (Status.Created, new LanguageDTO(entity.Id, entity.Name));
    }

    public async Task<Status> DeleteAsync(int languageId)
    {
        var language = await _context.Languages.FindAsync(languageId);

        if (language == null) return Status.NotFound;

        _context.Languages.Remove(language);

        await _context.SaveChangesAsync();

        return Status.Deleted;
    }

    public async Task<(Status, LanguageDTO)> ReadAsync(int languageId)
    {
        var query = from l in _context.Languages
            where l.Id == languageId
            select new LanguageDTO(l.Id, l.Name);

        var category = await query.FirstOrDefaultAsync();

        return category == null ? (Status.NotFound, new LanguageDTO(-1, "")) : (Status.Found, category);
    }

    public async Task<IReadOnlyCollection<LanguageDTO>> ReadAsync()
    {
        return await _context.Languages.Select(l => new LanguageDTO(l.Id, l.Name)).ToListAsync();
    }

    public async Task<Status> UpdateAsync(LanguageDTO languageDTO)
    {
        if (InvalidInput(languageDTO)) return Status.BadRequest;

        var existing = await (from l in _context.Languages
                where l.Id != languageDTO.Id
                where l.Name == languageDTO.Name
                select new LanguageDTO(l.Id, l.Name))
            .AnyAsync();

        if (existing) return Status.Conflict;

        var entity = await _context.Languages.FindAsync(languageDTO.Id);

        if (entity == null) return Status.NotFound;

        entity.Name = languageDTO.Name;

        await _context.SaveChangesAsync();

        return Status.Updated;
    }

    private static bool InvalidInput(CreateLanguageDTO language)
    {
        return language.Name.Length is > 50 or > 50 
               || string.IsNullOrEmpty(language.Name) 
               || string.IsNullOrEmpty(language.Name) 
               || string.IsNullOrWhiteSpace(language.Name) 
               || string.IsNullOrWhiteSpace(language.Name);
    }
}