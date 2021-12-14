// ***********************************************************************
// Assembly         : WebService.Entities
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="Context.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Entities;

/// <summary>
///     Class Context.
///     Implements the <see cref="Microsoft.EntityFrameworkCore.DbContext" />
///     Implements the <see cref="WebService.Entities.IContext" />
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
/// <seealso cref="WebService.Entities.IContext" />
public class Context : DbContext, IContext
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Context" /> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    ///     and
    ///     <see href="https://aka.ms/efcore-docs-dbcontext-options">Using DbContextOptions</see> for more information.
    /// </remarks>
    public Context(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    ///     Gets languages.
    /// </summary>
    /// <value>The languages.</value>
    public DbSet<Language> Languages => Set<Language>();

    /// <summary>
    ///     Gets levels.
    /// </summary>
    /// <value>The levels.</value>
    public DbSet<Level> Levels => Set<Level>();

    /// <summary>
    ///     Gets materials.
    /// </summary>
    /// <value>The materials.</value>
    public DbSet<Material> Materials => Set<Material>();

    /// <summary>
    ///     Gets medias.
    /// </summary>
    /// <value>The medias.</value>
    public DbSet<Media> Medias => Set<Media>();

    /// <summary>
    ///     Gets programming languages.
    /// </summary>
    /// <value>The programming languages.</value>
    public DbSet<ProgrammingLanguage> ProgrammingLanguages => Set<ProgrammingLanguage>();

    /// <summary>
    ///     Gets tags.
    /// </summary>
    /// <value>The tags.</value>
    public DbSet<Tag> Tags => Set<Tag>();

    /// <summary>
    ///     Override this method to further configure the model that was discovered by convention from the entity types
    ///     exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting
    ///     model may be cached
    ///     and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <param name="modelBuilder">
    ///     The builder being used to construct the model for this context. Databases (and other extensions) typically
    ///     define extension methods on this object that allow you to configure aspects of the model that are specific
    ///     to a given database.
    /// </param>
    /// <remarks>
    ///     <para>
    ///         If a model is explicitly set on the options for this context (via
    ///         <see
    ///             cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />
    ///         )
    ///         then this method will not be run.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more
    ///         information.
    ///     </para>
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<Level>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<Material>()
            .HasIndex(s => s.Title)
            .IsUnique();

        modelBuilder.Entity<Material>()
            .OwnsMany(e => e.WeightedTags, a =>
            {
                a.WithOwner().HasForeignKey("MaterialId");
                a.Property<int>("MaterialId");
                a.Property<string>("Name");
                a.HasKey("MaterialId", "Name");
            });

        modelBuilder.Entity<Material>()
            .OwnsMany(e => e.Ratings, a =>
            {
                a.WithOwner().HasForeignKey("MaterialId");
                a.Property<int>("MaterialId");
                a.Property<string>("Reviewer");
                a.HasKey("MaterialId", "Reviewer");
            });

        modelBuilder.Entity<Material>()
            .OwnsMany(e => e.Authors, a =>
            {
                a.WithOwner().HasForeignKey("MaterialId");
                a.Property<int>("MaterialId");
                a.Property<string>("FirstName");
                a.Property<string>("SurName");
                a.HasKey("MaterialId", "FirstName", "SurName");
            });

        modelBuilder.Entity<Media>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<ProgrammingLanguage>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<Tag>()
            .HasIndex(s => s.Name)
            .IsUnique();
    }
}