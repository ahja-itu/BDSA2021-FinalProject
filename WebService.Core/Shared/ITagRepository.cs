// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ITagRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Interface ITagRepository
///     Implements the <see cref="IRepository" />
/// </summary>
/// <seealso cref="IRepository" />
public interface ITagRepository : IRepository
{
    /// <summary>
    ///     Creates asynchronously.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, TagDTO&gt;&gt;.</returns>
    Task<(Status, TagDTO)> CreateAsync(CreateTagDTO tag);

    /// <summary>
    ///     Reads asynchronously.
    /// </summary>
    /// <param name="tagId">The tag identifier.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, TagDTO&gt;&gt;.</returns>
    Task<(Status, TagDTO)> ReadAsync(int tagId);

    /// <summary>
    ///     Reads all asynchronously.
    /// </summary>
    /// <returns>Task&lt;IReadOnlyCollection&lt;TagDTO&gt;&gt;.</returns>
    Task<IReadOnlyCollection<TagDTO>> ReadAsync();

    /// <summary>
    ///     Deletes asynchronously.
    /// </summary>
    /// <param name="tagId">The tag identifier.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> DeleteAsync(int tagId);

    /// <summary>
    ///     Updates asynchronously.
    /// </summary>
    /// <param name="tagDTO">The tag dto.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> UpdateAsync(TagDTO tagDTO);
}