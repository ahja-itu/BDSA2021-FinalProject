// ***********************************************************************
// Assembly         : WebService.Core.Server.Tests
// Author           : Group BTG
// Created          : 12-03-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LevelControllerTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Tests.ControllerTests;

/// <summary>
///     Class LevelControllerTests.
/// </summary>
public class LevelControllerTests
{
    /// <summary>
    ///     The level controller
    /// </summary>
    private readonly LevelController _levelController;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LevelControllerTests" /> class.
    /// </summary>
    public LevelControllerTests()
    {
        var v = new TestVariables();
        _levelController = new LevelController(v.LevelRepository);
    }

    #region Post/Create

    /// <summary>
    ///     Defines the test method Post_level_returns_status_created.
    /// </summary>
    [Fact]
    public async Task Post_level_returns_status_created()
    {
        var level = new CreateLevelDTO("High school");

        var actual = await _levelController.Post(level) as CreatedResult;

        Assert.Equal((int) HttpStatusCode.Created, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Post_level_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Post_level_returns_status_conflict()
    {
        var level = new CreateLevelDTO("PHD");

        var actual = await _levelController.Post(level) as ConflictResult;

        Assert.Equal((int) HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Post_level_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Post_level_returns_status_badRequest()
    {
        var level = new CreateLevelDTO("");

        var actual = await _levelController.Post(level) as BadRequestResult;

        Assert.Equal((int) HttpStatusCode.BadRequest, actual?.StatusCode);
    }

    #endregion

    #region Get/Read

    /// <summary>
    ///     Defines the test method Get_all_levels_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_all_levels_returns_status_ok()
    {
        var response = await _levelController.Get();
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int) HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Get_level_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_level_returns_status_ok()
    {
        var response = await _levelController.Get(1);
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int) HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Get_level_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Get_level_returns_status_notFound()
    {
        var response = await _levelController.Get(4);
        var actual = response.Result as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion

    #region Delete

    /// <summary>
    ///     Defines the test method Delete_level_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Delete_level_returns_status_noContent()
    {
        var response = await _levelController.Delete(3);
        var actual = response as NoContentResult;

        Assert.Equal((int) HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Delete_level_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Delete_level_returns_status_notFound()
    {
        var response = await _levelController.Delete(4);
        var actual = response as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion

    #region Put/Update

    /// <summary>
    ///     Defines the test method Put_level_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Put_level_returns_status_noContent()
    {
        var level = new LevelDTO(1, "High school");
        var response = await _levelController.Put(level);
        var actual = response as NoContentResult;

        Assert.Equal((int) HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Put_level_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Put_level_returns_status_conflict()
    {
        var level = new LevelDTO(1, "PHD");
        var response = await _levelController.Put(level);
        var actual = response as ConflictResult;

        Assert.Equal((int) HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Put_level_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Put_level_returns_status_badRequest()
    {
        var level = new LevelDTO(1, "");
        var response = await _levelController.Put(level);
        var actual = response as BadRequestResult;

        Assert.Equal((int) HttpStatusCode.BadRequest, actual?.StatusCode);
    }

    /// <summary>
    ///     Defines the test method Put_level_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Put_level_returns_status_notFound()
    {
        var level = new LevelDTO(4, "High school");
        var response = await _levelController.Put(level);
        var actual = response as NotFoundResult;

        Assert.Equal((int) HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion
}