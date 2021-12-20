// ***********************************************************************
// Assembly         : WebService.Infrastructure
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="TagRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Infrastructure;

/// <summary>
///     Class TagRepository.
///     Implements the <see cref="WebService.Core.Shared.ITagRepository" /> interface
/// </summary>
/// <seealso cref="WebService.Core.Shared.ITagRepository" />
public class TagRepository : ITagRepository
{
    private readonly IContext _context;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TagRepository" /> class.
    /// </summary>
    public TagRepository(IContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Creates a new tag asynchronously.
    /// </summary>
    public async Task<(Status, TagDTO)> CreateAsync(CreateTagDTO tag)
    {
        if (InvalidInput(tag)) return (Status.BadRequest, new TagDTO(-1, tag.Name));

        var existing = await (from t in _context.Tags
                where t.Name == tag.Name
                select new TagDTO(t.Id, t.Name))
            .FirstOrDefaultAsync();

        if (existing != null) return (Status.Conflict, existing);

        var entity = new Tag(tag.Name);

        _context.Tags.Add(entity);

        await _context.SaveChangesAsync();

        return (Status.Created, new TagDTO(entity.Id, entity.Name));
    }

    /// <summary>
    ///     Deletes a tag based on id asynchronously.
    /// </summary>
    public async Task<Status> DeleteAsync(int tagId)
    {
        var tag = await _context.Tags.FindAsync(tagId);

        if (tag == null) return Status.NotFound;

        _context.Tags.Remove(tag);

        await _context.SaveChangesAsync();

        return Status.Deleted;
    }

    /// <summary>
    ///     Reads a tag asynchronously based on id and returns it along with an http status.
    /// </summary>
    public async Task<(Status, TagDTO)> ReadAsync(int tagId)
    {
        var query = from t in _context.Tags
            where t.Id == tagId
            select new TagDTO(t.Id, t.Name);

        var category = await query.FirstOrDefaultAsync();

        return category == null ? (Status.NotFound, new TagDTO(-1, "")) : (Status.Found, category);
    }

    /// <summary>
    ///     Reads all tags asynchronously.
    /// </summary>
    public async Task<IReadOnlyCollection<TagDTO>> ReadAsync()
    {
        return await _context.Tags.Select(t => new TagDTO(t.Id, t.Name)).ToListAsync();
    }

    /// <summary>
    ///     Update an existing tag asynchronously.
    /// </summary>
    public async Task<Status> UpdateAsync(TagDTO tagDTO)
    {
        if (InvalidInput(tagDTO)) return Status.BadRequest;

        var existing = await (from t in _context.Tags
                where t.Id != tagDTO.Id
                where t.Name == tagDTO.Name
                select new TagDTO(t.Id, t.Name))
            .AnyAsync();

        if (existing) return Status.Conflict;

        var entity = await _context.Tags.FindAsync(tagDTO.Id);

        if (entity == null) return Status.NotFound;

        entity.Name = tagDTO.Name;

        await _context.SaveChangesAsync();

        return Status.Updated;
    }

    /// <summary>
    ///     Validates the input tag with regards to validity of its name.
    /// </summary>
    private static bool InvalidInput(CreateTagDTO tag)
    {
        return tag.Name.Length is > 50 or > 50
               || string.IsNullOrEmpty(tag.Name)
               || string.IsNullOrEmpty(tag.Name)
               || string.IsNullOrWhiteSpace(tag.Name)
               || string.IsNullOrWhiteSpace(tag.Name);
    }
}