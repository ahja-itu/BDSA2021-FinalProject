// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LanguageDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Class CreateLanguageDTO.
///     Implements the <see cref="System.IEquatable{CreateLanguageDTO}" />
/// </summary>
public record CreateLanguageDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateLanguageDTO" /> class.</summary>
    public CreateLanguageDTO(string name)
    {
        Name = name;
    }

    [StringLength(50)] public string Name { get; }
}

/// <summary>
///     Class LanguageDTO.
///     Implements the <see cref="T:WebService.Core.Shared.CreateLanguageDTO" /> class with an added id field.
/// </summary>
public record LanguageDTO : CreateLanguageDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.LanguageDTO" /> class with id and name.</summary>
    public LanguageDTO(int id, string name) : base(name)
    {
        Id = id;
    }

    public int Id { get; }
}