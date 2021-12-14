// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="AuthorDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Class CreateAuthorDTO.
///     Implements the <see cref="System.IEquatable{CreateAuthorDTO}" />
/// </summary>
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
///     Class AuthorDTO.
///     Implements the <see cref="T:WebService.Core.Shared.CreateAuthorDTO" />
///     Implements the <see cref="System.IEquatable{CreateAuthorDTO}" />
///     Implements the <see cref="System.IEquatable{AuthorDTO}" />
/// </summary>
public record AuthorDTO : CreateAuthorDTO
{
    public AuthorDTO(string firstName, string surName) : base(firstName, surName)
    {
    }
}