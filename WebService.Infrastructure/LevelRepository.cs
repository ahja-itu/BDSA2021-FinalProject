// ***********************************************************************
// Assembly         : WebService.Infrastructure
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LevelRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Infrastructure;

/// <summary>Class LevelRepository.
/// Implements the <see cref="WebService.Core.Shared.ILevelRepository" /></summary>
public class LevelRepository : ILevelRepository
{
    private readonly IContext _context;

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Infrastructure.LevelRepository" /> class.</summary>
    /// <param name="context">The context.</param>
    public LevelRepository(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create a Level entry in the database.
    /// </summary>
    /// <param name="level">A CreateLevelDTO containing correctly valued/formatted fields to create a new Level entry</param>
    /// <returns>A status of how the create operation completed and a levelDTO to represent the created entry in the database.</returns>
    public async Task<(Status, LevelDTO)> CreateAsync(CreateLevelDTO level)
    {
        if (InvalidInput(level)) return (Status.BadRequest, new LevelDTO(-1, level.Name));

        var existing = await (from l in _context.Levels
                where l.Name == level.Name
                select new LevelDTO(l.Id, l.Name))
            .FirstOrDefaultAsync();
        if (existing != null) return (Status.Conflict, existing);

        var entity = new Level(level.Name);

        _context.Levels.Add(entity);

        await _context.SaveChangesAsync();

        return (Status.Created, new LevelDTO(entity.Id, entity.Name));
    }

    /// <summary>
    /// Delete a level entry from the database.
    /// </summary>
    /// <param name="levelId">The ID of a Level entry in the database</param>
    /// <returns>A status code indicating how the operation went.</returns>
    public async Task<Status> DeleteAsync(int levelId)
    {
        var level = await _context.Levels.FindAsync(levelId);

        if (level == null) return Status.NotFound;

        _context.Levels.Remove(level);

        await _context.SaveChangesAsync();

        return Status.Deleted;
    }

    /// <summary>
    /// Read a single Level entry from the database, with a given ID
    /// </summary>
    /// <param name="levelId">The ID of an entry in the database</param>
    /// <returns>A status message of how the operation went and a LevelDTO object to represent the database entry.</returns>
    public async Task<(Status, LevelDTO)> ReadAsync(int levelId)
    {
        var query = from l in _context.Levels
            where l.Id == levelId
            select new LevelDTO(l.Id, l.Name);

        var category = await query.FirstOrDefaultAsync();

        return category == null ? (Status.NotFound, new LevelDTO(-1, "")) : (Status.Found, category);
    }

    /// <summary>
    /// Read all of the Level entries present in the database.
    /// </summary>
    /// <returns>A list of LevelDTOs representing all of the level entries in the database.</returns>
    public async Task<IReadOnlyCollection<LevelDTO>> ReadAsync()
    {
        return await _context.Levels.Select(l => new LevelDTO(l.Id, l.Name)).ToListAsync();
    }

    /// <summary>
    /// Update a level entry in the database, by providing a LevelDTO with updated fields.
    /// </summary>
    /// <param name="levelDTO">A LevelDTO containing properly valued/formatted fields and a valid ID</param>
    /// <returns>A status message indicating how the update operation went.</returns>
    public async Task<Status> UpdateAsync(LevelDTO levelDTO)
    {
        if (InvalidInput(levelDTO)) return Status.BadRequest;

        var existing = await (from l in _context.Levels
                where l.Id != levelDTO.Id
                where l.Name == levelDTO.Name
                select new LevelDTO(l.Id, l.Name))
            .AnyAsync();

        if (existing) return Status.Conflict;

        var entity = await _context.Levels.FindAsync(levelDTO.Id);

        if (entity == null) return Status.NotFound;

        entity.Name = levelDTO.Name;

        await _context.SaveChangesAsync();

        return Status.Updated;
    }

    /// <summary>Valids the input.</summary>
    /// <param name="level">The level.</param>
    /// <returns>
    ///   <c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    private static bool InvalidInput(CreateLevelDTO level)
    {
        return level.Name.Length is > 50 or > 50 
               || string.IsNullOrEmpty(level.Name) 
               || string.IsNullOrEmpty(level.Name) 
               || string.IsNullOrWhiteSpace(level.Name) 
               || string.IsNullOrWhiteSpace(level.Name);
    }
}