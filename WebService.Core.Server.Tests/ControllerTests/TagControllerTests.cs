namespace WebService.Core.Server.Tests.ControllerTests;

public class TagControllerTests
{
    private readonly TagController _tagController;

    public TagControllerTests()
    {
        var v = new TestVariables();
        _tagController = new TagController(v.TagRepository);
    }

    #region Post/Create
    [Fact]
    public async Task Post_tag_returns_status_created()
    {
        var tag = new CreateTagDTO("Database");

        var actual = await _tagController.Post(tag) as CreatedResult;

        Assert.Equal((int)HttpStatusCode.Created, actual?.StatusCode);
    }

    [Fact]
    public async Task Post_tag_returns_status_conflict()
    {
        var tag = new CreateTagDTO("SOLID");

        var actual = await _tagController.Post(tag) as ConflictResult;

        Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
    }

    [Fact]
    public async Task Post_tag_returns_status_badRequest()
    {
        var tag = new CreateTagDTO("");

        var actual = await _tagController.Post(tag) as BadRequestResult;

        Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
    }
    #endregion

    #region Get/Read
    [Fact]
    public async Task Get_all_tags_returns_status_ok()
    {
        var response = await _tagController.Get();
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
    }

    [Fact]
    public async Task Get_tag_returns_status_ok()
    {
        var response = await _tagController.Get(1);
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
    }

    [Fact]
    public async Task Get_tag_returns_status_notFound()
    {
        var response = await _tagController.Get(4);
        var actual = response.Result as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }
    #endregion

    #region Delete
    [Fact]
    public async Task Delete_tag_returns_status_noContent()
    {
        var response = await _tagController.Delete(3);
        var actual = response as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
    }

    [Fact]
    public async Task Delete_tag_returns_status_notFound()
    {
        var response = await _tagController.Delete(4);
        var actual = response as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }
    #endregion

    #region Put/Update
    [Fact]
    public async Task Put_tag_returns_status_noContent()
    {
        var tag = new TagDTO(1, "Database");
        var response = await _tagController.Put(tag);
        var actual = response as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
    }

    [Fact]
    public async Task Put_tag_returns_status_conflict()
    {
        var tag = new TagDTO(1, "API");
        var response = await _tagController.Put(tag);
        var actual = response as ConflictResult;

        Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
    }

    [Fact]
    public async Task Put_tag_returns_status_badRequest()
    {
        var tag = new TagDTO(1, "");
        var response = await _tagController.Put(tag);
        var actual = response as BadRequestResult;

        Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
    }

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