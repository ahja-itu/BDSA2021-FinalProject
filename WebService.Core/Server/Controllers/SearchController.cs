// ***********************************************************************
// Assembly         : WebService.Core.Server
// Author           : Group BTG
// Created          : 12-03-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="SearchController.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Controllers;

/// <summary>
///     Class SearchController.
///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class SearchController : ControllerBase
{
    /// <summary>
    ///     The search
    /// </summary>
    private readonly ISearch _search;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SearchController" /> class.
    /// </summary>
    /// <param name="search">The search.</param>
    public SearchController(ISearch search)
    {
        _search = search;
    }

    /// <summary>
    ///     Posts the specified search form and gets all material matching it using the search algorithem
    /// </summary>
    /// <param name="searchForm">The search form.</param>
    /// <returns>ActionResult&lt;ICollection&lt;MaterialDTO&gt;&gt;.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICollection<MaterialDTO>>> Post(SearchForm searchForm)
    {
        var (response, materialDTOs) = await _search.Search(searchForm);

        if (response == Status.Found) return Ok(materialDTOs);
        return NotFound();
    }
}