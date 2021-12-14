// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : thorl
// Created          : 11-29-2021
//
// Last Modified By : thorl
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="RatingDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Class CreateRatingDTO.
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateRatingDTO}" />
/// </summary>
public record CreateRatingDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateRatingDTO" /> class.</summary>
    /// <param name="value">The value.</param>
    /// <param name="reviewer">The reviewer.</param>
    public CreateRatingDTO(int value, string reviewer)
    {
        Value = value;
        Reviewer = reviewer;
        TimeStamp = DateTime.UtcNow;
    }

    [Range(1, 10)] public int Value { get; }

    public string Reviewer { get; }
    public DateTime TimeStamp { get; }
}

/// <summary>
///     Class RatingDTO.
///     Implements the <see cref="T:WebService.Core.Shared.CreateRatingDTO" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateRatingDTO}" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.RatingDTO}" />
/// </summary>
public record RatingDTO : CreateRatingDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.RatingDTO" /> class.</summary>
    /// <param name="id">The identifier.</param>
    /// <param name="value">The value.</param>
    /// <param name="reviewer">The reviewer.</param>
    public RatingDTO(int id, int value, string reviewer) : base(value, reviewer)
    {
        Id = id;
    }

    public int Id { get; }
}