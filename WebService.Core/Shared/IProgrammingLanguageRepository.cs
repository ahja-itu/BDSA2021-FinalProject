// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="IProgrammingLanguageRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Core.Shared;

/// <summary>
/// Interface IProgrammingLanguageRepository
/// Implements the <see cref="IRepository" />
/// </summary>
/// <seealso cref="IRepository" />
public interface IProgrammingLanguageRepository : IRepository
{
    /// <summary>
    /// Creates asynchronously.
    /// </summary>
    /// <param name="programmingLanguage">The programming language.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, ProgrammingLanguageDTO&gt;&gt;.</returns>
    Task<(Status, ProgrammingLanguageDTO)> CreateAsync(CreateProgrammingLanguageDTO programmingLanguage);
    /// <summary>
    /// Reads asynchronously.
    /// </summary>
    /// <param name="programmingLanguageId">The programming language identifier.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, ProgrammingLanguageDTO&gt;&gt;.</returns>
    Task<(Status, ProgrammingLanguageDTO)> ReadAsync(int programmingLanguageId);
    /// <summary>
    /// Reads all asynchronously.
    /// </summary>
    /// <returns>Task&lt;IReadOnlyCollection&lt;ProgrammingLanguageDTO&gt;&gt;.</returns>
    Task<IReadOnlyCollection<ProgrammingLanguageDTO>> ReadAsync();
    /// <summary>
    /// Deletes asynchronously.
    /// </summary>
    /// <param name="programmingLanguageId">The programming language identifier.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> DeleteAsync(int programmingLanguageId);
    /// <summary>
    /// Updates asynchronously.
    /// </summary>
    /// <param name="programmingLanguageDTO">The programming language dto.</param>
    /// <returns>Task&lt;Status&gt;.</returns>
    Task<Status> UpdateAsync(ProgrammingLanguageDTO programmingLanguageDTO);
}