// ***********************************************************************
// Assembly         : WebService.Infrastructure
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="MediaRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Infrastructure;

/// <summary>
///     Class MediaRepository.
///     Implements the <see cref="WebService.Core.Shared.IMediaRepository" />
/// </summary>
/// <seealso cref="WebService.Core.Shared.IMediaRepository" />
public class MediaRepository : IMediaRepository
{
    /// <summary>
    ///     The context
    /// </summary>
    private readonly IContext _context;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MediaRepository" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public MediaRepository(IContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Create a media asynchronously.
    /// </summary>
    /// <param name="media">The media.</param>
    /// <returns>A Task&lt;System.ValueTuple&gt; representing the asynchronous operation.</returns>
    public async Task<(Status, MediaDTO)> CreateAsync(CreateMediaDTO media)
    {
        if (InvalidInput(media)) return (Status.BadRequest, new MediaDTO(-1, media.Name));

        var existing = await (from m in _context.Medias
                where m.Name == media.Name
                select new MediaDTO(m.Id, m.Name))
            .FirstOrDefaultAsync();

        if (existing != null) return (Status.Conflict, existing);

        var entity = new Media(media.Name);

        _context.Medias.Add(entity);

        await _context.SaveChangesAsync();

        return (Status.Created, new MediaDTO(entity.Id, entity.Name));
    }

    /// <summary>
    ///     Delete a media asynchronously.
    /// </summary>
    /// <param name="mediaId">The media identifier.</param>
    /// <returns>A Task&lt;Status&gt; representing the asynchronous operation.</returns>
    public async Task<Status> DeleteAsync(int mediaId)
    {
        var media = await _context.Medias.FindAsync(mediaId);

        if (media == null) return Status.NotFound;

        _context.Medias.Remove(media);

        await _context.SaveChangesAsync();

        return Status.Deleted;
    }

    /// <summary>
    ///     Read a media asynchronously and returns a http status.
    /// </summary>
    /// <param name="mediaId">The media identifier.</param>
    /// <returns>A Task&lt;System.ValueTuple&gt; representing the asynchronous operation.</returns>
    public async Task<(Status, MediaDTO)> ReadAsync(int mediaId)
    {
        var query = from m in _context.Medias
            where m.Id == mediaId
            select new MediaDTO(m.Id, m.Name);

        var category = await query.FirstOrDefaultAsync();

        return category == null ? (Status.NotFound, new MediaDTO(-1, "")) : (Status.Found, category);
    }

    /// <summary>
    ///     Reads all medias asynchronously.
    /// </summary>
    /// <returns>A Task&lt;IReadOnlyCollection`1&gt; representing the asynchronous operation.</returns>
    public async Task<IReadOnlyCollection<MediaDTO>> ReadAsync()
    {
        return await _context.Medias.Select(m => new MediaDTO(m.Id, m.Name)).ToListAsync();
    }

    /// <summary>
    ///     Update a media asynchronously.
    /// </summary>
    /// <param name="mediaDTO">The media dto.</param>
    /// <returns>A Task&lt;Status&gt; representing the asynchronous operation.</returns>
    public async Task<Status> UpdateAsync(MediaDTO mediaDTO)
    {
        if (InvalidInput(mediaDTO)) return Status.BadRequest;

        var existing = await (from m in _context.Medias
                where m.Id != mediaDTO.Id
                where m.Name == mediaDTO.Name
                select new MediaDTO(m.Id, m.Name))
            .AnyAsync();

        if (existing) return Status.Conflict;

        var entity = await _context.Medias.FindAsync(mediaDTO.Id);

        if (entity == null) return Status.NotFound;

        entity.Name = mediaDTO.Name;

        await _context.SaveChangesAsync();

        return Status.Updated;
    }

    /// <summary>
    ///     Validates the input.
    /// </summary>
    /// <param name="media">The media.</param>
    /// <returns><c>true</c> if the input is valid, <c>false</c> otherwise.</returns>
    private static bool InvalidInput(CreateMediaDTO media)
    {
        return media.Name.Length is > 50 or > 50
               || string.IsNullOrEmpty(media.Name)
               || string.IsNullOrEmpty(media.Name)
               || string.IsNullOrWhiteSpace(media.Name)
               || string.IsNullOrWhiteSpace(media.Name);
    }
}