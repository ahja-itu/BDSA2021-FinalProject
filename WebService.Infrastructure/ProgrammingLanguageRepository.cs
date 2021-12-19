// ***********************************************************************
// Assembly         : WebService.Infrastructure
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ProgrammingLanguageRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Infrastructure;

/// <summary>
///     Class ProgrammingLanguageRepository.
///     Implements the <see cref="WebService.Core.Shared.IProgrammingLanguageRepository" /> interface
/// </summary>
/// <seealso cref="WebService.Core.Shared.IProgrammingLanguageRepository" />
public class ProgrammingLanguageRepository : IProgrammingLanguageRepository
{
    private readonly IContext _context;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ProgrammingLanguageRepository" /> class.
    /// </summary>
    public ProgrammingLanguageRepository(IContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Creates a new programming language asynchronously.
    /// </summary>
    public async Task<(Status, ProgrammingLanguageDTO)> CreateAsync(CreateProgrammingLanguageDTO programmingLanguage)
    {
        if (InvalidInput(programmingLanguage))
            return (Status.BadRequest, new ProgrammingLanguageDTO(-1, programmingLanguage.Name));

        var existing = await (from l in _context.ProgrammingLanguages
                where l.Name == programmingLanguage.Name
                select new ProgrammingLanguageDTO(l.Id, l.Name))
            .FirstOrDefaultAsync();

        if (existing != null) return (Status.Conflict, existing);

        var entity = new ProgrammingLanguage(programmingLanguage.Name);

        _context.ProgrammingLanguages.Add(entity);

        await _context.SaveChangesAsync();

        return (Status.Created, new ProgrammingLanguageDTO(entity.Id, entity.Name));
    }

    /// <summary>
    ///     Deletes a programming language based on id asynchronously.
    /// </summary>
    public async Task<Status> DeleteAsync(int programmingLanguageId)
    {
        var language = await _context.ProgrammingLanguages.FindAsync(programmingLanguageId);

        if (language == null) return Status.NotFound;

        _context.ProgrammingLanguages.Remove(language);

        await _context.SaveChangesAsync();

        return Status.Deleted;
    }

    /// <summary>
    ///     Reads a programming language based on id asynchronously and returns it with an http status.
    /// </summary>
    public async Task<(Status, ProgrammingLanguageDTO)> ReadAsync(int programmingLanguageId)
    {
        var query = from l in _context.ProgrammingLanguages
            where l.Id == programmingLanguageId
            select new ProgrammingLanguageDTO(l.Id, l.Name);

        var category = await query.FirstOrDefaultAsync();

        return category == null ? (Status.NotFound, new ProgrammingLanguageDTO(-1, "")) : (Status.Found, category);
    }

    /// <summary>
    ///     Reads all programming languages asynchronously.
    /// </summary>
    public async Task<IReadOnlyCollection<ProgrammingLanguageDTO>> ReadAsync()
    {
        return await _context.ProgrammingLanguages.Select(l => new ProgrammingLanguageDTO(l.Id, l.Name)).ToListAsync();
    }

    /// <summary>
    ///     Updates an existing programming language asynchronously.
    /// </summary>
    public async Task<Status> UpdateAsync(ProgrammingLanguageDTO programmingProgrammingLanguageDTO)
    {
        if (InvalidInput(programmingProgrammingLanguageDTO)) return Status.BadRequest;

        var existing = await (from l in _context.ProgrammingLanguages
                where l.Id != programmingProgrammingLanguageDTO.Id
                where l.Name == programmingProgrammingLanguageDTO.Name
                select new ProgrammingLanguageDTO(l.Id, l.Name))
            .AnyAsync();

        if (existing) return Status.Conflict;

        var entity = await _context.ProgrammingLanguages.FindAsync(programmingProgrammingLanguageDTO.Id);

        if (entity == null) return Status.NotFound;

        entity.Name = programmingProgrammingLanguageDTO.Name;

        await _context.SaveChangesAsync();

        return Status.Updated;
    }

    /// <summary>
    ///     Validates the input programming language with regards to validity of its name.
    /// </summary>
    private static bool InvalidInput(CreateProgrammingLanguageDTO programmingLanguage)
    {
        return programmingLanguage.Name.Length is > 50 or > 50
               || string.IsNullOrEmpty(programmingLanguage.Name)
               || string.IsNullOrEmpty(programmingLanguage.Name)
               || string.IsNullOrWhiteSpace(programmingLanguage.Name)
               || string.IsNullOrWhiteSpace(programmingLanguage.Name);
    }
}