// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : thorl
// Created          : 11-29-2021
//
// Last Modified By : thorl
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="AuthorDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Core.Shared;

/// <summary>Class CreateAuthorDTO.
/// Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateAuthorDTO}" /></summary>
public record CreateAuthorDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateAuthorDTO" /> class.</summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="surName">Name of the sur.</param>
    public CreateAuthorDTO(string firstName, string surName)
    {
        FirstName = firstName;
        SurName = surName;
    }

    [StringLength(50)] public string FirstName { get; }

    [StringLength(50)] public string SurName { get; }
}

/// <summary>
/// Class AuthorDTO.
/// Implements the <see cref="T:WebService.Core.Shared.CreateAuthorDTO" />
/// Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateAuthorDTO}" />
/// Implements the <see cref="System.IEquatable{WebService.Core.Shared.AuthorDTO}" /></summary>
public record AuthorDTO : CreateAuthorDTO
{
    public AuthorDTO(int id, string firstName, string surName) : base(firstName, surName)
    {
        Id = id;
    }

    public int Id { get; }
}