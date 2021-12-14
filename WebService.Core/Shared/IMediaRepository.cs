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
///     Implements the <see cref="IRepository" />
/// </summary>
/// <seealso cref="IRepository" />
public interface IMediaRepository : IRepository
{
    /// <summary>
    ///     Creates asynchronously.
    /// </summary>
    /// <param name="media">The media.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, MediaDTO&gt;&gt;.</returns>
    Task<(Status, MediaDTO)> CreateAsync(CreateMediaDTO media);

    /// <summary>
    ///     Reads asynchronously.
    /// </summary>
    /// <param name="mediaId">The media identifier.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, MediaDTO&gt;&gt;.</returns>
    Task<(Status, MediaDTO)> ReadAsync(int mediaId);

    /// <summary>
    ///     Reads all asynchronously.
    /// </summary>
    /// <returns>Task&lt;IReadOnlyCollection&lt;MediaDTO&gt;&gt;.</returns>
    Task<IReadOnlyCollection<MediaDTO>> ReadAsync();

    /// <summary>
    ///     Deletes asynchronously.
    /// </summary>
    /// <param name="mediaId">The media identifier.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> DeleteAsync(int mediaId);

    /// <summary>
    ///     Updates asynchronously.
    /// </summary>
    /// <param name="mediaDTO">The media dto.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> UpdateAsync(MediaDTO mediaDTO);
}