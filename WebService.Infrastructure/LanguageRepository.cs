// ***********************************************************************
// Assembly         : WebService.Infrastructure
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LanguageRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Infrastructure;

public class LanguageRepository : ILanguageRepository
{
    private readonly IContext _context;

    public LanguageRepository(IContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Create a Language entry in the database provided a CreateLanguageDTO
    /// </summary>
    /// <param name="language">A CreateLanguageDTO containing the name of the language to store</param>
    /// <returns>
    ///     A status describing the result of the operation, and possibly a DTO representing the (maybe) saved database
    ///     entry
    /// </returns>
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

    /// <summary>
    ///     Delete a language entry in the database given its id.
    /// </summary>
    /// <param name="languageId">ID of the given language.</param>
    /// <returns>A status object indicating how the operation went.</returns>
    public async Task<Status> DeleteAsync(int languageId)
    {
        var language = await _context.Languages.FindAsync(languageId);

        if (language == null) return Status.NotFound;

        _context.Languages.Remove(language);

        await _context.SaveChangesAsync();

        return Status.Deleted;
    }

    /// <summary>
    ///     Read an entry from the database given its ID
    /// </summary>
    /// <param name="languageId">The ID of the given entry in the database</param>
    /// <returns>
    ///     A status showing if the entry was found or not, and maybe the language entry as a DTO if there were an entry
    ///     with the given ID
    /// </returns>
    public async Task<(Status, LanguageDTO)> ReadAsync(int languageId)
    {
        var query = from l in _context.Languages
            where l.Id == languageId
            select new LanguageDTO(l.Id, l.Name);

        var category = await query.FirstOrDefaultAsync();

        return category == null ? (Status.NotFound, new LanguageDTO(-1, "")) : (Status.Found, category);
    }

    /// <summary>
    ///     Read all of the language entries from the database
    /// </summary>
    /// <returns>A list of language DTOs that were present in the database</returns>
    public async Task<IReadOnlyCollection<LanguageDTO>> ReadAsync()
    {
        return await _context.Languages.Select(l => new LanguageDTO(l.Id, l.Name)).ToListAsync();
    }

    /// <summary>
    ///     Updates a given language entry, in the database, with new field(s).
    /// </summary>
    /// <param name="languageDTO">A DTO containing updated fields and a valid ID to find the entry in the database</param>
    /// <returns>A status indication of how the update went.</returns>
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

    /// <summary>Validates the input.</summary>
    /// <param name="language">The language.</param>
    /// <returns>
    ///     <c>true</c> if input is valid, <c>false</c> otherwise.
    /// </returns>
    private static bool InvalidInput(CreateLanguageDTO language)
    {
        return language.Name.Length is > 50 or > 50
               || string.IsNullOrEmpty(language.Name)
               || string.IsNullOrEmpty(language.Name)
               || string.IsNullOrWhiteSpace(language.Name)
               || string.IsNullOrWhiteSpace(language.Name);
    }
}