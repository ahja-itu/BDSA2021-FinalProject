// ***********************************************************************
// Assembly         : WebService.Entities
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-03-2021
// ***********************************************************************
// <copyright file="Rating.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Entities;

/// <summary>Class Rating.</summary>
[Owned]
public class Rating
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Rating" /> class.</summary>
    /// <param name="value">The value.</param>
    /// <param name="reviewer">The reviewer.</param>
    public Rating(int value, string reviewer)
    {
        Value = value;
        Reviewer = reviewer;
        TimeStamp = DateTime.UtcNow;
    }

    /// <summary>Initializes a new instance of the <see cref="T:WebService.Entities.Rating" /> class.</summary>
    /// <param name="id">The identifier.</param>
    /// <param name="value">The value.</param>
    /// <param name="reviewer">The reviewer.</param>
    public Rating(int id, int value, string reviewer)
    {
        Id = id;
        Value = value;
        Reviewer = reviewer;
        TimeStamp = DateTime.UtcNow;
    }

    public int Id { get; set; }

    [Range(1, 10)] public int Value { get; init; }

    [StringLength(50)] public string Reviewer { get; init; }

    public DateTime TimeStamp { get; init; }
}