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
/// Interface IContext
/// Implements the <see cref="System.IDisposable" />
/// </summary>
/// <seealso cref="System.IDisposable" />
public interface IContext : IDisposable
{
    /// <summary>
    /// Gets languages.
    /// </summary>
    /// <value>The languages.</value>
    DbSet<Language> Languages { get; }
    /// <summary>
    /// Gets levels.
    /// </summary>
    /// <value>The levels.</value>
    DbSet<Level> Levels { get; }
    /// <summary>
    /// Gets materials.
    /// </summary>
    /// <value>The materials.</value>
    DbSet<Material> Materials { get; }
    /// <summary>
    /// Gets medias.
    /// </summary>
    /// <value>The medias.</value>
    DbSet<Media> Medias { get; }
    /// <summary>
    /// Gets programming languages.
    /// </summary>
    /// <value>The programming languages.</value>
    DbSet<ProgrammingLanguage> ProgrammingLanguages { get; }
    /// <summary>
    /// Gets tags.
    /// </summary>
    /// <value>The tags.</value>
    DbSet<Tag> Tags { get; }
    /// <summary>
    /// Saves changes.
    /// </summary>
    /// <returns>System.Int32.</returns>
    int SaveChanges();
    /// <summary>
    /// Saves changes asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Task&lt;System.Int32&gt;.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}