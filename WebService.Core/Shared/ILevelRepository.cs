// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ILevelRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Interface ILevelRepository
///     Implements the <see cref="IRepository" /> interface
/// </summary>
/// <seealso cref="IRepository" />
public interface ILevelRepository : IRepository
{
    /// <summary>
    ///     Creates a new level asynchronously.
    /// </summary>
    Task<(Status, LevelDTO)> CreateAsync(CreateLevelDTO level);

    /// <summary>
    ///     Reads a level based on id asynchronously.
    /// </summary>
    Task<(Status, LevelDTO)> ReadAsync(int levelId);

    /// <summary>
    ///     Reads all asynchronously.
    /// </summary>
    Task<IReadOnlyCollection<LevelDTO>> ReadAsync();

    /// <summary>
    ///     Deletes a given level based on id asynchronously.
    /// </summary>
    Task<Status> DeleteAsync(int levelId);

    /// <summary>
    ///     Updates a given level asynchronously.
    /// </summary>
    Task<Status> UpdateAsync(LevelDTO levelDTO);
}