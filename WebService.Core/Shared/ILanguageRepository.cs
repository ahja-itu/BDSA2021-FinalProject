// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : thorl
// Created          : 11-29-2021
//
// Last Modified By : thorl
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ILanguageRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Core.Shared;

/// <summary>
/// Interface ILanguageRepository
/// Implements the <see cref="IRepository" />
/// </summary>
/// <seealso cref="IRepository" />
public interface ILanguageRepository : IRepository
{
    /// <summary>
    /// Creates asynchronously.
    /// </summary>
    /// <param name="language">The language.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, LanguageDTO&gt;&gt;.</returns>
    Task<(Status, LanguageDTO)> CreateAsync(CreateLanguageDTO language);
    /// <summary>
    /// Reads asynchronously.
    /// </summary>
    /// <param name="languageId">The language identifier.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, LanguageDTO&gt;&gt;.</returns>
    Task<(Status, LanguageDTO)> ReadAsync(int languageId);
    /// <summary>
    /// Reads all asynchronously.
    /// </summary>
    /// <returns>Task&lt;IReadOnlyCollection&lt;LanguageDTO&gt;&gt;.</returns>
    Task<IReadOnlyCollection<LanguageDTO>> ReadAsync();
    /// <summary>
    /// Deletes asynchronously.
    /// </summary>
    /// <param name="languageId">The language identifier.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> DeleteAsync(int languageId);
    /// <summary>
    /// Updates asynchronously.
    /// </summary>
    /// <param name="languageDTO">The language dto.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> UpdateAsync(LanguageDTO languageDTO);
}