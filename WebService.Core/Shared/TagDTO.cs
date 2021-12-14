// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="TagDTO.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Class CreateTagDTO.
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateTagDTO}" />
/// </summary>
public record CreateTagDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateTagDTO" /> class.</summary>
    /// <param name="name">The name.</param>
    public CreateTagDTO(string name)
    {
        Name = name;
    }

    [StringLength(50)] public string Name { get; }
}

/// <summary>
///     Class TagDTO.
///     Implements the <see cref="T:WebService.Core.Shared.CreateTagDTO" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateTagDTO}" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.TagDTO}" />
/// </summary>
public record TagDTO : CreateTagDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.TagDTO" /> class.</summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    public TagDTO(int id, string name) : base(name)
    {
        Id = id;
    }

    public int Id { get; }
}

/// <summary>
///     Class CreateWeightedTagDTO.
///     Implements the <see cref="T:WebService.Core.Shared.CreateTagDTO" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateTagDTO}" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateWeightedTagDTO}" />
/// </summary>
public record CreateWeightedTagDTO : CreateTagDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.CreateWeightedTagDTO" /> class.</summary>
    /// <param name="name">The name.</param>
    /// <param name="weight">The weight.</param>
    public CreateWeightedTagDTO(string name, int weight) : base(name)
    {
        Weight = weight;
    }

    [Range(1, 100)] public int Weight { get; }
}

/// <summary>
///     Class WeightedTagDTO.
///     Implements the <see cref="T:WebService.Core.Shared.CreateWeightedTagDTO" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateTagDTO}" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.CreateWeightedTagDTO}" />
///     Implements the <see cref="System.IEquatable{WebService.Core.Shared.WeightedTagDTO}" />
/// </summary>
public record WeightedTagDTO : CreateWeightedTagDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.WeightedTagDTO" /> class.</summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    /// <param name="weight">The weight.</param>
    public WeightedTagDTO(int id, string name, int weight) : base(name, weight)
    {
        Id = id;
    }

    private int Id { get; }
}