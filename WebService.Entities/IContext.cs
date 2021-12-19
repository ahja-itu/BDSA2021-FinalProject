// ***********************************************************************
// Assembly         : WebService.Entities
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-03-2021
// ***********************************************************************
// <copyright file="IContext.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Entities;

/// <summary>
///     Interface IContext
///     Implements the <see cref="System.IDisposable" /> interface
/// </summary>
public interface IContext : IDisposable
{
    /// <summary>
    ///     Gets languages.
    /// </summary>
    DbSet<Language> Languages { get; }

    /// <summary>
    ///     Gets levels.
    /// </summary>
    DbSet<Level> Levels { get; }

    /// <summary>
    ///     Gets materials.
    /// </summary>
    DbSet<Material> Materials { get; }

    /// <summary>
    ///     Gets medias.
    /// </summary>
    DbSet<Media> Medias { get; }

    /// <summary>
    ///     Gets programming languages.
    /// </summary>
    DbSet<ProgrammingLanguage> ProgrammingLanguages { get; }

    /// <summary>
    ///     Gets tags.
    /// </summary>
    DbSet<Tag> Tags { get; }

    /// <summary>
    ///     Saves any changes made to the context.
    /// </summary>
    int SaveChanges();

    /// <summary>
    ///     Saves changes asynchronously using the cancellation token that can be used by other 
    ///     objects or threads to receive notice of cancellation.
    /// </summary>   
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}