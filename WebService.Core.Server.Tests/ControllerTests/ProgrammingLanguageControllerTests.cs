// ***********************************************************************
// Assembly         : WebService.Core.Server.Tests
// Author           : Group BTG
// Created          : 12-03-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ProgrammingLanguageControllerTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Core.Server.Tests.ControllerTests;

/// <summary>
/// Class ProgrammingLanguageControllerTests.
/// </summary>
public class ProgrammingLanguageControllerTests
{
    /// <summary>
    /// The programming language controller
    /// </summary>
    private readonly ProgrammingLanguageController _programmingLanguageController;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProgrammingLanguageControllerTests"/> class.
    /// </summary>
    public ProgrammingLanguageControllerTests()
    {
        var v = new TestVariables();
        _programmingLanguageController = new ProgrammingLanguageController(v.ProgrammingLanguageRepository);
    }

    #region Post/Create
    /// <summary>
    /// Defines the test method Post_programmingLanguage_returns_status_created.
    /// </summary>
    [Fact]
    public async Task Post_programmingLanguage_returns_status_created()
    {
        var programmingLanguage = new CreateProgrammingLanguageDTO("Go");

        var actual = await _programmingLanguageController.Post(programmingLanguage) as CreatedResult;

        Assert.Equal((int)HttpStatusCode.Created, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Post_programmingLanguage_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Post_programmingLanguage_returns_status_conflict()
    {
        var programmingLanguage = new CreateProgrammingLanguageDTO("C++");

        var actual = await _programmingLanguageController.Post(programmingLanguage) as ConflictResult;

        Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Post_programmingLanguage_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Post_programmingLanguage_returns_status_badRequest()
    {
        var programmingLanguage = new CreateProgrammingLanguageDTO("");

        var actual = await _programmingLanguageController.Post(programmingLanguage) as BadRequestResult;

        Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
    }
    #endregion

    #region Get/Read
    /// <summary>
    /// Defines the test method Get_all_programmingLanguages_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_all_programmingLanguages_returns_status_ok()
    {
        var response = await _programmingLanguageController.Get();
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Get_programmingLanguage_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_programmingLanguage_returns_status_ok()
    {
        var response = await _programmingLanguageController.Get(1);
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Get_programmingLanguage_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Get_programmingLanguage_returns_status_notFound()
    {
        var response = await _programmingLanguageController.Get(4);
        var actual = response.Result as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }
    #endregion

    #region Delete
    /// <summary>
    /// Defines the test method Delete_programmingLanguage_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Delete_programmingLanguage_returns_status_noContent()
    {
        var response = await _programmingLanguageController.Delete(3);
        var actual = response as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Delete_programmingLanguage_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Delete_programmingLanguage_returns_status_notFound()
    {
        var response = await _programmingLanguageController.Delete(4);
        var actual = response as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }
    #endregion

    #region Put/Update
    /// <summary>
    /// Defines the test method Put_programmingLanguage_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Put_programmingLanguage_returns_status_noContent()
    {
        var programmingLanguage = new ProgrammingLanguageDTO(1, "Go");
        var response = await _programmingLanguageController.Put(programmingLanguage);
        var actual = response as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Put_programmingLanguage_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Put_programmingLanguage_returns_status_conflict()
    {
        var programmingLanguage = new ProgrammingLanguageDTO(1, "C++");
        var response = await _programmingLanguageController.Put(programmingLanguage);
        var actual = response as ConflictResult;

        Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Put_programmingLanguage_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Put_programmingLanguage_returns_status_badRequest()
    {
        var programmingLanguage = new ProgrammingLanguageDTO(1, "");
        var response = await _programmingLanguageController.Put(programmingLanguage);
        var actual = response as BadRequestResult;

        Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Put_programmingLanguage_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Put_programmingLanguage_returns_status_notFound()
    {
        var programmingLanguage = new ProgrammingLanguageDTO(4, "Go");
        var response = await _programmingLanguageController.Put(programmingLanguage);
        var actual = response as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion

}