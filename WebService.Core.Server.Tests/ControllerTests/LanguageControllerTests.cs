// ***********************************************************************
// Assembly         : WebService.Core.Server.Tests
// Author           : Group BTG
// Created          : 12-03-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LanguageControllerTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Tests.ControllerTests;

/// <summary>
///     Class LanguageControllerTests.
///     Contains tests grouped into regions based on the type  of method tested.
/// </summary>
public class LanguageControllerTests
{    private readonly LanguageController _languageController;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LanguageControllerTests" /> class.
    /// </summary>
    public LanguageControllerTests()
    {
        var v = new TestVariables();
        _languageController = new LanguageController(v.LanguageRepository);
    }

    #region Post/Create

    /// <summary>
    ///     Defines the test method Post_language_returns_status_created.
    /// </summary>
    [Fact]
    public async Task Post_language_returns_status_created()
    {
        var language = new CreateLanguageDTO("French");

        var actual = await _languageController.Post(language) as CreatedResult;

        Assert.Equal((int) HttpStatusCode.Created, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Post_language_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Post_language_returns_status_conflict()
    {
        var language = new CreateLanguageDTO("Danish");

        var actual = await _languageController.Post(language) as ConflictResult;

        Assert.Equal((int) HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Post_language_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Post_language_returns_status_badRequest()
    {
        var language = new CreateLanguageDTO("");

        var actual = await _languageController.Post(language) as BadRequestResult;

        Assert.Equal((int) HttpStatusCode.BadRequest, actual?.StatusCode);
    }

    #endregion

    #region Get/Read

    /// <summary>
    ///     Defines the test method Get_all_languages_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_all_languages_returns_status_ok()
    {
        var response = await _languageController.Get();
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int) HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Get_language_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_language_returns_status_ok()
    {
        var response = await _languageController.Get(1);
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int) HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Get_language_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Get_language_returns_status_notFound()
    {
        var response = await _languageController.Get(4);
        var actual = response.Result as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion

    #region Delete

    /// <summary>
    ///     Defines the test method Delete_language_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Delete_language_returns_status_noContent()
    {
        var response = await _languageController.Delete(3);
        var actual = response as NoContentResult;

        Assert.Equal((int) HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Delete_language_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Delete_language_returns_status_notFound()
    {
        var response = await _languageController.Delete(4);
        var actual = response as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion

    #region Put/Update

    /// <summary>
    ///     Defines the test method Put_language_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Put_language_returns_status_noContent()
    {
        var language = new LanguageDTO(1, "French");
        var response = await _languageController.Put(language);
        var actual = response as NoContentResult;

        Assert.Equal((int) HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Put_language_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Put_language_returns_status_conflict()
    {
        var language = new LanguageDTO(1, "Swedish");
        var response = await _languageController.Put(language);
        var actual = response as ConflictResult;

        Assert.Equal((int) HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Put_language_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Put_language_returns_status_badRequest()
    {
        var language = new LanguageDTO(1, "");
        var response = await _languageController.Put(language);
        var actual = response as BadRequestResult;

        Assert.Equal((int) HttpStatusCode.BadRequest, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Put_language_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Put_language_returns_status_notFound()
    {
        var language = new LanguageDTO(4, "French");
        var response = await _languageController.Put(language);
        var actual = response as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion
}