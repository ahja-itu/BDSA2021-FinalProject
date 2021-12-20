// ***********************************************************************
// Assembly         : WebService.Core.Shared
// Author           : Group BTG
// Created          : 12-03-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ISearch.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Shared;

/// <summary>
///     Interface ISearch
/// </summary>
public interface ISearch
{
    /// <summary>
    ///     Searches with the specified search form and gets matching material.
    /// </summary>
    Task<(Status, ICollection<MaterialDTO>)> Search(SearchForm searchForm);
}