// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ProgrammingLanguageDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Class CreateProgrammingLanguageDTO.
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateProgrammingLanguageDTO}" />
/// </summary>
public record CreateProgrammingLanguageDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateProgrammingLanguageDTO" /> class.</summary>
    /// <param name="name">The name.</param>
    public CreateProgrammingLanguageDTO(string name)
    {
        Name = name;
    }

    [StringLength(50)] public string Name { get; }
}

/// <summary>
///     Class ProgrammingLanguageDTO.
///     Implements the <see cref="T:WebService.Core.Shared.CreateProgrammingLanguageDTO" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateProgrammingLanguageDTO}" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.ProgrammingLanguageDTO}" />
/// </summary>
public record ProgrammingLanguageDTO : CreateProgrammingLanguageDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.ProgrammingLanguageDTO" /> class.</summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    public ProgrammingLanguageDTO(int id, string name) : base(name)
    {
        Id = id;
    }

    public int Id { get; }
}