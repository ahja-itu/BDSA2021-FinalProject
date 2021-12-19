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
///     Implements the <see cref="IRepository" /> interface
/// </summary>
/// <seealso cref="IRepository" />
public interface ITagRepository : IRepository
{
    /// <summary>
    ///     Creates a new tag asynchronously.
    /// </summary>
    Task<(Status, TagDTO)> CreateAsync(CreateTagDTO tag);

    /// <summary>
    ///     Reads a tag based on id asynchronously.
    /// </summary>
    Task<(Status, TagDTO)> ReadAsync(int tagId);

    /// <summary>
    ///     Reads all tags asynchronously.
    /// </summary>
    Task<IReadOnlyCollection<TagDTO>> ReadAsync();

    /// <summary>
    ///     Deletes a tag based on id asynchronously.
    /// </summary>
    Task<Status> DeleteAsync(int tagId);

    /// <summary>
    ///     Updates a given tag asynchronously.
    /// </summary>
    Task<Status> UpdateAsync(TagDTO tagDTO);
}