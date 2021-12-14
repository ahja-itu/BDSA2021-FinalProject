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
///     Implements the <see cref="IRepository" />
/// </summary>
/// <seealso cref="IRepository" />
public interface ILevelRepository : IRepository
{
    /// <summary>
    ///     Creates asynchronously.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, LevelDTO&gt;&gt;.</returns>
    Task<(Status, LevelDTO)> CreateAsync(CreateLevelDTO level);

    /// <summary>
    ///     Reads asynchronously.
    /// </summary>
    /// <param name="levelId">The level identifier.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, LevelDTO&gt;&gt;.</returns>
    Task<(Status, LevelDTO)> ReadAsync(int levelId);

    /// <summary>
    ///     Reads all asynchronously.
    /// </summary>
    /// <returns>Task&lt;IReadOnlyCollection&lt;LevelDTO&gt;&gt;.</returns>
    Task<IReadOnlyCollection<LevelDTO>> ReadAsync();

    /// <summary>
    ///     Deletes asynchronously.
    /// </summary>
    /// <param name="levelId">The level identifier.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> DeleteAsync(int levelId);

    /// <summary>
    ///     Updates asynchronously.
    /// </summary>
    /// <param name="levelDTO">The level dto.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> UpdateAsync(LevelDTO levelDTO);
}