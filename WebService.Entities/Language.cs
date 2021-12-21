// ***********************************************************************
// Assembly         : WebService.Entities
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-08-2021
// ***********************************************************************
// <copyright file="Language.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Entities;

/// <summary>Class Language.</summary>
public class Language
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Language" /> class.</summary>
    public Language(string name)
    {
        Name = name;
    }

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Language" /> class including id.</summary>
    public Language(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public ICollection<Material> Materials { get; set; } = new List<Material>();

    [StringLength(50)] public string Name { get; set; }
}