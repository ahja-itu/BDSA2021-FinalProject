// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="IMaterialRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Interface IMaterialRepository
///     Implements the <see cref="IRepository" /> interface
/// </summary>
/// <seealso cref="IRepository" />
public interface IMaterialRepository : IRepository
{
    /// <summary>
    ///     Creates a new material asynchronously.
    /// </summary>
    Task<(Status, MaterialDTO)> CreateAsync(CreateMaterialDTO material);

    /// <summary>
    ///     Reads a material based on id asynchronously.
    /// </summary>
    Task<(Status, MaterialDTO)> ReadAsync(int materialId);

    /// <summary>
    ///     Reads materials based on a given search form asynchronously.
    /// </summary>
    Task<(Status, IReadOnlyCollection<MaterialDTO>)> ReadAsync(SearchForm searchInput);

    /// <summary>
    ///     Reads all materials asynchronously.
    /// </summary>
    Task<IReadOnlyCollection<MaterialDTO>> ReadAsync();

    /// <summary>
    ///     Updates a given material asynchronously.
    /// </summary>
    Task<Status> UpdateAsync(MaterialDTO materialId);

    /// <summary>
    ///     Deletes a given material asynchronously.
    /// </summary>
    Task<Status> DeleteAsync(int materialId);
}