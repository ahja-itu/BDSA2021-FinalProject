// ***********************************************************************
// Assembly         : WebService.Entities
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-08-2021
// ***********************************************************************
// <copyright file="Level.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Entities;

/// <summary>Class Level.</summary>
public class Level
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Level" /> class.</summary>
    /// <param name="name">The name.</param>
    public Level(string name)
    {
        Name = name;
    }

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Level" /> class.</summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    public Level(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public ICollection<Material> Materials { get; set; } = new List<Material>();

    [StringLength(50)] public string Name { get; set; }
}