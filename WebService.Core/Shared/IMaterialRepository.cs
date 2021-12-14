// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : thorl
// Created          : 11-29-2021
//
// Last Modified By : thorl
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="IMaterialRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Core.Shared;

/// <summary>
/// Interface IMaterialRepository
/// Implements the <see cref="IRepository" />
/// </summary>
/// <seealso cref="IRepository" />
public interface IMaterialRepository : IRepository
{
    /// <summary>
    /// Creates asynchronously.
    /// </summary>
    /// <param name="material">The material.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, MaterialDTO&gt;&gt;.</returns>
    Task<(Status, MaterialDTO)> CreateAsync(CreateMaterialDTO material);
    /// <summary>
    /// Reads asynchronously.
    /// </summary>
    /// <param name="materialId">The material identifier.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, MaterialDTO&gt;&gt;.</returns>
    Task<(Status, MaterialDTO)> ReadAsync(int materialId);
    /// <summary>
    /// Reads asynchronously.
    /// </summary>
    /// <param name="searchInput">The search input.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, IReadOnlyCollection&lt;MaterialDTO&gt;&gt;&gt;.</returns>
    Task<(Status, IReadOnlyCollection<MaterialDTO>)> ReadAsync(SearchForm searchInput);
    /// <summary>
    /// Reads all asynchronously.
    /// </summary>
    /// <returns>Task&lt;IReadOnlyCollection&lt;MaterialDTO&gt;&gt;.</returns>
    Task<IReadOnlyCollection<MaterialDTO>> ReadAsync();
    /// <summary>
    /// Updates asynchronously.
    /// </summary>
    /// <param name="materialId">The material identifier.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> UpdateAsync(MaterialDTO materialId);
    /// <summary>
    /// Deletes asynchronously.
    /// </summary>
    /// <param name="materialId">The material identifier.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> DeleteAsync(int materialId);
}