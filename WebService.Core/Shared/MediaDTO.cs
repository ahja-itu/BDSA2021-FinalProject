// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="MediaDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Class CreateMediaDTO.
///     Implements the <see cref="System.IEquatable{CreateMediaDTO}" />
/// </summary>
public record CreateMediaDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateMediaDTO" /> class.</summary>
    /// <param name="name">The name.</param>
    public CreateMediaDTO(string name)
    {
        Name = name;
    }

    [StringLength(50)] public string Name { get; }
}

/// <summary>
///     Class MediaDTO.
///     Implements the <see cref="T:WebService.Core.Shared.CreateMediaDTO" />
///     Implements the <see cref="System.IEquatable{CreateMediaDTO}" />
///     Implements the <see cref="System.IEquatable{MediaDTO}" />
/// </summary>
public record MediaDTO : CreateMediaDTO
{
    public MediaDTO(int id, string name) : base(name)
    {
        Id = id;
    }

    public int Id { get; }
}