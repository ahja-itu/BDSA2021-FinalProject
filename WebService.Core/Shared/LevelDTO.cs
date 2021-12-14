// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : thorl
// Created          : 11-29-2021
//
// Last Modified By : thorl
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LevelDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Core.Shared;

/// <summary>Class CreateLevelDTO.
/// Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateLevelDTO}" /></summary>
public record CreateLevelDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateLevelDTO" /> class.</summary>
    /// <param name="name">The name.</param>
    public CreateLevelDTO(string name)
    {
        Name = name;
    }

    [StringLength(50)]
    public string Name { get; }
}

/// <summary>
/// Class LevelDTO.
/// Implements the <see cref="T:WebService.Core.Shared.CreateLevelDTO" />
/// Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateLevelDTO}" />
/// Implements the <see cref="System.IEquatable{WebService.Core.Shared.LevelDTO}" /></summary>
public record LevelDTO : CreateLevelDTO
{
    public LevelDTO(int id, string name) : base(name)
    {
        Id = id;
    }

    public int Id { get; }
}