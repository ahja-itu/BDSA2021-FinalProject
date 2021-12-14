// ***********************************************************************
// Assembly         : WebService.Core.Server.Tests
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="TagControllerTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace WebService.Core.Server.Tests.ControllerTests;

/// <summary>
/// Class TagControllerTests.
/// </summary>
public class TagControllerTests
{
    /// <summary>
    /// The tag controller
    /// </summary>
    private readonly TagController _tagController;

    /// <summary>
    /// Initializes a new instance of the <see cref="TagControllerTests"/> class.
    /// </summary>
    public TagControllerTests()
    {
        var v = new TestVariables();
        _tagController = new TagController(v.TagRepository);
    }

    #region Post/Create
    /// <summary>
    /// Defines the test method Post_tag_returns_status_created.
    /// </summary>
    [Fact]
    public async Task Post_tag_returns_status_created()
    {
        var tag = new CreateTagDTO("Database");

        var actual = await _tagController.Post(tag) as CreatedResult;

        Assert.Equal((int)HttpStatusCode.Created, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Post_tag_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Post_tag_returns_status_conflict()
    {
        var tag = new CreateTagDTO("SOLID");

        var actual = await _tagController.Post(tag) as ConflictResult;

        Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Post_tag_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Post_tag_returns_status_badRequest()
    {
        var tag = new CreateTagDTO("");

        var actual = await _tagController.Post(tag) as BadRequestResult;

        Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
    }
    #endregion

    #region Get/Read
    /// <summary>
    /// Defines the test method Get_all_tags_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_all_tags_returns_status_ok()
    {
        var response = await _tagController.Get();
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Get_tag_returns_status_ok.
    /// </summary>
    [Fact]
    public async Task Get_tag_returns_status_ok()
    {
        var response = await _tagController.Get(1);
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Get_tag_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Get_tag_returns_status_notFound()
    {
        var response = await _tagController.Get(4);
        var actual = response.Result as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }
    #endregion

    #region Delete
    /// <summary>
    /// Defines the test method Delete_tag_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Delete_tag_returns_status_noContent()
    {
        var response = await _tagController.Delete(3);
        var actual = response as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Delete_tag_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Delete_tag_returns_status_notFound()
    {
        var response = await _tagController.Delete(4);
        var actual = response as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }
    #endregion

    #region Put/Update
    /// <summary>
    /// Defines the test method Put_tag_returns_status_noContent.
    /// </summary>
    [Fact]
    public async Task Put_tag_returns_status_noContent()
    {
        var tag = new TagDTO(1, "Database");
        var response = await _tagController.Put(tag);
        var actual = response as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Put_tag_returns_status_conflict.
    /// </summary>
    [Fact]
    public async Task Put_tag_returns_status_conflict()
    {
        var tag = new TagDTO(1, "API");
        var response = await _tagController.Put(tag);
        var actual = response as ConflictResult;

        Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Put_tag_returns_status_badRequest.
    /// </summary>
    [Fact]
    public async Task Put_tag_returns_status_badRequest()
    {
        var tag = new TagDTO(1, "");
        var response = await _tagController.Put(tag);
        var actual = response as BadRequestResult;

        Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
    }

    /// <summary>
    /// Defines the test method Put_tag_returns_status_notFound.
    /// </summary>
    [Fact]
    public async Task Put_tag_returns_status_notFound()
    {
        var tag = new TagDTO(4, "Database");
        var response = await _tagController.Put(tag);
        var actual = response as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion

}