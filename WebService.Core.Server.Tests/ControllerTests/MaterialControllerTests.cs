// ***********************************************************************
// Assembly         : WebService.Core.Server.Tests
// Author           : Group BTG
// Created          : 12-03-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="MaterialControllerTests .cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Diagnostics.CodeAnalysis;

namespace WebService.Core.Server.Tests.ControllerTests;

/// <summary>
///     Class MaterialControllerTests.
///     Contains tests grouped into regions based on the type  of method tested.
/// </summary>
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class MaterialControllerTests
{
    private readonly MaterialController _materialController;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MaterialControllerTests" /> class.
    /// </summary>
    public MaterialControllerTests()
    {
        var v = new TestVariables();
        _materialController = new MaterialController(v.MaterialRepository);
    }

    #region Post/Create

    /// <summary>
    ///     Defines the test method Post_material_returns_status_created.
    /// </summary>
    [Fact]
    public async Task Post_material_returns_status_created()
    {
        var material = new CreateMaterialDTO(new CreateWeightedTagDTO[] {new("API", 10)}, new CreateRatingDTO[] { },
            new CreateLevelDTO[] {new("PHD")}, new CreateProgrammingLanguageDTO[] {new("C#")},
            new CreateMediaDTO[] {new("Book")}, new CreateLanguageDTO("Danish"), "Summary", "Url", "Content", "Title",
            new CreateAuthorDTO[] { }, DateTime.UtcNow);

        var actual = await _materialController.Post(material) as CreatedResult;

        Assert.Equal((int) HttpStatusCode.Created, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Post_material_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Post_material_returns_status_conflict()
    {
        var material = new CreateMaterialDTO(new CreateWeightedTagDTO[] {new("API", 10)}, new CreateRatingDTO[] { },
            new CreateLevelDTO[] {new("PHD")}, new CreateProgrammingLanguageDTO[] {new("C#")},
            new CreateMediaDTO[] {new("Book")}, new CreateLanguageDTO("Danish"), "Summary", "Url", "Content",
            "Material 2", new CreateAuthorDTO[] { }, DateTime.UtcNow);

        var actual = await _materialController.Post(material) as ConflictResult;

        Assert.Equal((int) HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Post_material_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Post_material_returns_status_badRequest()
    {
        var material = new CreateMaterialDTO(new CreateWeightedTagDTO[] {new("Some tag", 10)},
            new CreateRatingDTO[] { }, new CreateLevelDTO[] { }, new CreateProgrammingLanguageDTO[] { },
            new CreateMediaDTO[] { }, new CreateLanguageDTO("Language"), "Summary", "Url", "Content", "Material 2",
            new CreateAuthorDTO[] { }, DateTime.UtcNow);

        var actual = await _materialController.Post(material) as BadRequestResult;

        Assert.Equal((int) HttpStatusCode.BadRequest, actual?.StatusCode);
    }

    #endregion

    #region Get/Read

    /// <summary>
    ///     Defines the test method Get_all_materials_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_all_materials_returns_status_ok()
    {
        var response = await _materialController.Get();
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int) HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Get_material_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_material_returns_status_ok()
    {
        var response = await _materialController.Get(1);
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int) HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Get_material_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Get_material_returns_status_notFound()
    {
        var response = await _materialController.Get(4);
        var actual = response.Result as NotFoundObjectResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Get_material_returns_from_searchForm_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_material_returns_from_searchForm_status_ok()
    {
        var searchForm = new SearchForm("Hello", new TagDTO[] {new(1, "SOLID")}, new LevelDTO[] { },
            new ProgrammingLanguageDTO[] { }, new LanguageDTO[] { }, new MediaDTO[] { }, 5);
        var response = await _materialController.Post(searchForm);
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int) HttpStatusCode.OK, actual?.StatusCode);
    }

    [Fact]
    public async Task Get_material_returns_from_searchForm_status_notFound()
    {
        var searchForm = new SearchForm("Hello", new[] {new TagDTO(4, "DOTNET")},
            new[] {new LevelDTO(1, "Kindergarden")},
            new[] {new ProgrammingLanguageDTO(1, "Cow")}, new[] {new LanguageDTO(1, "ASL")},
            new[] {new MediaDTO(1, "Comicbook")}, 10);
        var response = await _materialController.Post(searchForm);
        var actual = response.Result as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion

    #region Delete

    /// <summary>
    ///     Defines the test method Delete_material_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Delete_material_returns_status_noContent()
    {
        var response = await _materialController.Delete(3);
        var actual = response as NoContentResult;

        Assert.Equal((int) HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Delete_material_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Delete_material_returns_status_notFound()
    {
        var response = await _materialController.Delete(4);
        var actual = response as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion

    #region Put/Update

    /// <summary>
    ///     Defines the test method Put_material_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Put_material_returns_status_noContent()
    {
        var material = new MaterialDTO(1, new CreateWeightedTagDTO[] {new("API", 10)}, new CreateRatingDTO[] { },
            new CreateLevelDTO[] {new("PHD")}, new CreateProgrammingLanguageDTO[] {new("C#")},
            new CreateMediaDTO[] {new("Book")}, new CreateLanguageDTO("Danish"), "Summary", "Url", "Content",
            "Title new", new CreateAuthorDTO[] { }, DateTime.UtcNow);
        var response = await _materialController.Put(material);
        var actual = response as NoContentResult;

        Assert.Equal((int) HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Put_material_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Put_material_returns_status_conflict()
    {
        var material = new MaterialDTO(1, new CreateWeightedTagDTO[] {new("API", 10)}, new CreateRatingDTO[] { },
            new CreateLevelDTO[] {new("PHD")}, new CreateProgrammingLanguageDTO[] {new("C#")},
            new CreateMediaDTO[] {new("Book")}, new CreateLanguageDTO("Danish"), "Summary", "Url", "Content",
            "Material 2", new CreateAuthorDTO[] { }, DateTime.UtcNow);
        var response = await _materialController.Put(material);
        var actual = response as ConflictResult;

        Assert.Equal((int) HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Put_material_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Put_material_returns_status_badRequest()
    {
        var material = new MaterialDTO(1, new CreateWeightedTagDTO[] {new("Some tag", 10)}, new CreateRatingDTO[] { },
            new CreateLevelDTO[] { }, new CreateProgrammingLanguageDTO[] { }, new CreateMediaDTO[] { },
            new CreateLanguageDTO("Language"), "Summary", "Url", "Content", "Material 2", new CreateAuthorDTO[] { },
            DateTime.UtcNow);
        var response = await _materialController.Put(material);
        var actual = response as BadRequestResult;

        Assert.Equal((int) HttpStatusCode.BadRequest, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Put_material_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Put_material_returns_status_notFound()
    {
        var material = new MaterialDTO(4, new CreateWeightedTagDTO[] {new("API", 10)}, new CreateRatingDTO[] { },
            new CreateLevelDTO[] {new("PHD")}, new CreateProgrammingLanguageDTO[] {new("C#")},
            new CreateMediaDTO[] {new("Book")}, new CreateLanguageDTO("Danish"), "Summary", "Url", "Content",
            "Title new", new CreateAuthorDTO[] { }, DateTime.UtcNow);
        var response = await _materialController.Put(material);
        var actual = response as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion
}