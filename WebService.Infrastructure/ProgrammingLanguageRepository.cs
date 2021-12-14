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
///     Implements the <see cref="WebService.Core.Shared.IProgrammingLanguageRepository" />
/// </summary>
/// <seealso cref="WebService.Core.Shared.IProgrammingLanguageRepository" />
public class ProgrammingLanguageRepository : IProgrammingLanguageRepository
{
    /// <summary>
    ///     The context
    /// </summary>
    private readonly IContext _context;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ProgrammingLanguageRepository" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public ProgrammingLanguageRepository(IContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Creates a programming language asynchronously.
    /// </summary>
    /// <param name="programmingLanguage">The programming language.</param>
    /// <returns>A Task&lt;System.ValueTuple&gt; representing the asynchronous operation.</returns>
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
    ///     Deletes a programming language asynchronously.
    /// </summary>
    /// <param name="programmingLanguageId">The programming language identifier.</param>
    /// <returns>A Task&lt;Status&gt; representing the asynchronous operation.</returns>
    public async Task<Status> DeleteAsync(int programmingLanguageId)
    {
        var language = await _context.ProgrammingLanguages.FindAsync(programmingLanguageId);

        if (language == null) return Status.NotFound;

        _context.ProgrammingLanguages.Remove(language);

        await _context.SaveChangesAsync();

        return Status.Deleted;
    }

    /// <summary>
    ///     Reads a programming language asynchronously and a http status.
    /// </summary>
    /// <param name="programmingLanguageId">The programming language identifier.</param>
    /// <returns>A Task&lt;System.ValueTuple&gt; representing the asynchronous operation.</returns>
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
    /// <returns>A Task&lt;IReadOnlyCollection`1&gt; representing the asynchronous operation.</returns>
    public async Task<IReadOnlyCollection<ProgrammingLanguageDTO>> ReadAsync()
    {
        return await _context.ProgrammingLanguages.Select(l => new ProgrammingLanguageDTO(l.Id, l.Name)).ToListAsync();
    }

    /// <summary>
    ///     Update a programming language asynchronously.
    /// </summary>
    /// <param name="programmingProgrammingLanguageDTO">The programming programming language dto.</param>
    /// <returns>A Task&lt;Status&gt; representing the asynchronous operation.</returns>
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
    ///     Valids the input.
    /// </summary>
    /// <param name="programmingLanguage">The programming language.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private static bool InvalidInput(CreateProgrammingLanguageDTO programmingLanguage)
    {
        return programmingLanguage.Name.Length is > 50 or > 50
               || string.IsNullOrEmpty(programmingLanguage.Name)
               || string.IsNullOrEmpty(programmingLanguage.Name)
               || string.IsNullOrWhiteSpace(programmingLanguage.Name)
               || string.IsNullOrWhiteSpace(programmingLanguage.Name);
    }
}