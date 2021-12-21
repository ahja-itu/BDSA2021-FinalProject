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
    ///     Determines whether the string source is equal to the string toCheck when ignoring casing
    /// </summary>
    public static bool IsEqualIgnoreCasing(this string source, string toCheck)
    {
        return string.Equals(source, toCheck, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Determines whether the string source contains the string toCheck when ignoring casing.
    /// </summary>
    public static bool ContainsIgnoreCasing(this string source, string toCheck)
    {
        return source.Split(" ").Contains(toCheck, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Determines whether the collection of strings source contains the specified string 
    ///     toCheck when ignoring casing.
    /// </summary>
    public static bool ContainsIgnoreCasing(this IEnumerable<string> source, string toCheck)
    {
        return source.Contains(toCheck, StringComparer.OrdinalIgnoreCase);
    }
}