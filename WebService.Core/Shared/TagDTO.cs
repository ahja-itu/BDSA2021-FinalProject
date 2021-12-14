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
///     Implements the
///     <see>
///         <cref>System.IEquatable{WebService.Core.Shared.CreateTagDTO}</cref>
///     </see>
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
///     Implements the
///     <see>
///         <cref>System.IEquatable{WebService.Core.Shared.CreateTagDTO}</cref>
///     </see>
///     Implements the
///     <see>
///         <cref>System.IEquatable{WebService.Core.Shared.TagDTO}</cref>
///     </see>
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
///     Implements the
///     <see>
///         <cref>System.IEquatable{WebService.Core.Shared.CreateTagDTO}</cref>
///     </see>
///     Implements the
///     <see>
///         <cref>System.IEquatable{WebService.Core.Shared.CreateWeightedTagDTO}</cref>
///     </see>
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
///     <para>
///         Class WeightedTagDTO.
///     </para>
///     <para>     Implements the <see cref="T:WebService.Core.Shared.CreateWeightedTagDTO" /></para>
///     <para>     Implements the <see cref="System.IEquatable{CreateTagDTO}" /></para>
///     <para>     Implements the <see cref="System.IEquatable{CreateWeightedTagDTO}" /></para>
///     <para>     Implements the <see cref="System.IEquatable{WeightedTagDTO}" /></para>
/// </summary>
public record WeightedTagDTO : CreateWeightedTagDTO
{
    /// <summary>Initializes a new instance of the <see cref="T:WebService.Core.Shared.WeightedTagDTO" /> class.</summary>
    /// <param name="name">The name.</param>
    /// <param name="weight">The weight.</param>
    public WeightedTagDTO(string name, int weight) : base(name, weight)
    {
    }
}