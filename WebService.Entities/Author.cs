// ***********************************************************************
// Assembly         : WebService.Entities
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-03-2021
// ***********************************************************************
// <copyright file="Author.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Entities;

/// <summary>Class Author.</summary>
[Owned]
public class Author
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Author" /> class.</summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="surName">Name of the sur.</param>
    public Author(string firstName, string surName)
    {
        FirstName = firstName;
        SurName = surName;
    }

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Author" /> class.</summary>
    /// <param name="id">The identifier.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="surName">Name of the sur.</param>
    public Author(int id, string firstName, string surName)
    {
        Id = id;
        FirstName = firstName;
        SurName = surName;
    }

    public int Id { get; set; }

    [StringLength(50)] public string FirstName { get; set; }

    [StringLength(50)] public string SurName { get; set; }
}