// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="Status.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Enum Status
/// </summary>
public enum Status
{
    /// <summary>
    ///     The created http status
    /// </summary>
    Created,

    /// <summary>
    ///     The updated http status
    /// </summary>
    Updated,

    /// <summary>
    ///     The deleted http status
    /// </summary>
    Deleted,

    /// <summary>
    ///     The not found http status
    /// </summary>
    NotFound,

    /// <summary>
    ///     The bad request http status
    /// </summary>
    BadRequest,

    /// <summary>
    ///     The conflict http status
    /// </summary>
    Conflict,

    /// <summary>
    ///     The found http status
    /// </summary>
    Found
}