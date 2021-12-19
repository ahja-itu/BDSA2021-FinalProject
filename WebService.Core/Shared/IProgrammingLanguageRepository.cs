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
///     Interface IProgrammingLanguageRepository
///     Implements the <see cref="IRepository" /> interface
/// </summary>
/// <seealso cref="IRepository" />
public interface IProgrammingLanguageRepository : IRepository
{
    /// <summary>
    ///     Creates asynchronously.
    /// </summary>
    Task<(Status, ProgrammingLanguageDTO)> CreateAsync(CreateProgrammingLanguageDTO programmingLanguage);

    /// <summary>
    ///     Reads a programming language based on id asynchronously.
    /// </summary>
    Task<(Status, ProgrammingLanguageDTO)> ReadAsync(int programmingLanguageId);

    /// <summary>
    ///     Reads all languages asynchronously.
    /// </summary>
    Task<IReadOnlyCollection<ProgrammingLanguageDTO>> ReadAsync();

    /// <summary>
    ///     Deletes a programming language based on id asynchronously.
    /// </summary>
    Task<Status> DeleteAsync(int programmingLanguageId);

    /// <summary>
    ///     Updates a given programming language asynchronously.
    /// </summary>
    Task<Status> UpdateAsync(ProgrammingLanguageDTO programmingLanguageDTO);
}