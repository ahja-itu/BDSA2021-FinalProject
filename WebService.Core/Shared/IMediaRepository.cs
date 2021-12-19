// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="IMediaRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Interface IMediaRepository
///     Implements the <see cref="IRepository" /> interface
/// </summary>
/// <seealso cref="IRepository" />
public interface IMediaRepository : IRepository
{
    /// <summary>
    ///     Creates asynchronously.
    /// </summary>
    Task<(Status, MediaDTO)> CreateAsync(CreateMediaDTO media);

    /// <summary>
    ///     Reads a media type based on id asynchronously.
    /// </summary>
    Task<(Status, MediaDTO)> ReadAsync(int mediaId);

    /// <summary>
    ///     Reads all media types asynchronously.
    /// </summary>
    Task<IReadOnlyCollection<MediaDTO>> ReadAsync();

    /// <summary>
    ///     Deletes a media type based on id asynchronously.
    /// </summary>
    Task<Status> DeleteAsync(int mediaId);

    /// <summary>
    ///     Updates a given media type asynchronously.
    /// </summary>
    Task<Status> UpdateAsync(MediaDTO mediaDTO);
}