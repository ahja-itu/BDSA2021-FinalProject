// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : thorl
// Created          : 11-29-2021
//
// Last Modified By : thorl
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LanguageDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Core.Shared;

/// <summary>Class CreateLanguageDTO.
/// Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateLanguageDTO}" /></summary>
public record CreateLanguageDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateLanguageDTO" /> class.</summary>
    /// <param name="name">The name.</param>
    public CreateLanguageDTO(string name)
    {
        Name = name;
    }

    [StringLength(50)]
    public string Name { get; }
}

/// <summary>
/// Class LanguageDTO.
/// Implements the <see cref="T:WebService.Core.Shared.CreateLanguageDTO" />
/// Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateLanguageDTO}" />
/// Implements the <see cref="System.IEquatable{WebService.Core.Shared.LanguageDTO}" /></summary>
public record LanguageDTO : CreateLanguageDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.LanguageDTO" /> class.</summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    public LanguageDTO(int id, string name) : base(name)
    {
        Id = id;
    }

    public int Id { get; }
}