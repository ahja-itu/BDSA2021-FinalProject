// ***********************************************************************
// Assembly         : WebService.Infrastructure
// Author           : Group BTG
// Created          : 12-14-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="Extensions.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Infrastructure;

/// <summary>
///     Class Extensions.
/// </summary>
public static class Extensions
{
    /// <summary>
    ///     Determines whether the source is equal to the specified to check ignoring casing
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="toCheck">To check.</param>
    /// <returns><c>true</c> if [is equal ignore casing] [the specified to check]; otherwise, <c>false</c>.</returns>
    public static bool IsEqualIgnoreCasing(this string source, string toCheck)
    {
        return string.Equals(source, toCheck, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Determines whether the source contains the specified to check ignoring casing.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="toCheck">To check.</param>
    /// <returns><c>true</c> if [contains ignore casing] [the specified to check]; otherwise, <c>false</c>.</returns>
    public static bool ContainsIgnoreCasing(this string source, string toCheck)
    {
        return source.Split(" ").Contains(toCheck, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Determines whether the source contains the specified to check ignoring casing.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="toCheck">To check.</param>
    /// <returns><c>true</c> if [contains ignore casing] [the specified to check]; otherwise, <c>false</c>.</returns>
    public static bool ContainsIgnoreCasing(this IEnumerable<string> source, string toCheck)
    {
        return source.Contains(toCheck, StringComparer.OrdinalIgnoreCase);
    }
}