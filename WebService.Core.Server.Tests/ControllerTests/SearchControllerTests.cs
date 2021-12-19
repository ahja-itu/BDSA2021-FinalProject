// ***********************************************************************
// Assembly         : WebService.Core.Server.Tests
// Author           : Group BTG
// Created          : 12-14-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="SearchControllerTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Tests.ControllerTests;

/// <summary>
///     Class SearchControllerTests.
///     Contains tests grouped into regions based on the type  of method tested.
/// </summary>
public class SearchControllerTests
{
    private readonly SearchController _searchController;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SearchControllerTests" /> class.
    /// </summary>
    public SearchControllerTests()
    {
        var v = new TestVariables();
        _searchController = new SearchController(v.SearchAlgorithm);
    }

    /// <summary>
    ///     Defines the test method Get_material_returns_from_searchForm_with_search_algo_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_material_returns_from_searchForm_with_search_algo_status_ok()
    {
        var searchForm = new SearchForm("Hello", new[] {new TagDTO(1, "SOLID")}, new LevelDTO[] { },
            new ProgrammingLanguageDTO[] { }, new LanguageDTO[] { }, new MediaDTO[] { }, 5);
        var response = await _searchController.Post(searchForm);
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int) HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Get_material_returns_from_searchForm_with_search_algo_status_notFound.
    /// </summary>
    [Fact]
    public async Task Get_material_returns_from_searchForm_with_search_algo_status_notFound()
    {
        var searchForm = new SearchForm("Hello", new[] {new TagDTO(4, "DOTNET")}, new[] {new LevelDTO(4, "DOTNET")},
            new[] {new ProgrammingLanguageDTO(4, "DOTNET")}, new[] {new LanguageDTO(4, "DOTNET")},
            new[] {new MediaDTO(4, "DOTNET")}, 10);
        var response = await _searchController.Post(searchForm);
        var actual = response.Result as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }
}