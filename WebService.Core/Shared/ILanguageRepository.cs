// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ILanguageRepository.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Interface ILanguageRepository
///     Implements the <see cref="IRepository" /> interface
/// </summary>
/// <seealso cref="IRepository" />
public interface ILanguageRepository : IRepository
{
    /// <summary>
    ///     Creates a new language asynchronously.
    /// </summary>
    Task<(Status, LanguageDTO)> CreateAsync(CreateLanguageDTO language);

    /// <summary>
    ///     Reads a language based on id asynchronously.
    /// </summary>
    Task<(Status, LanguageDTO)> ReadAsync(int languageId);

    /// <summary>
    ///     Reads all languages asynchronously.
    /// </summary>
    Task<IReadOnlyCollection<LanguageDTO>> ReadAsync();

    /// <summary>
    ///     Deletes a given language based on id asynchronously.
    /// </summary>
    Task<Status> DeleteAsync(int languageId);

    /// <summary>
    ///     Updates a given language asynchronously.
    /// </summary>
    Task<Status> UpdateAsync(LanguageDTO languageDTO);
}