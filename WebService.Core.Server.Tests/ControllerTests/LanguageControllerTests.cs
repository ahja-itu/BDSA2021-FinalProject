namespace WebService.Core.Server.Tests.ControllerTests;

public class LanguageControllerTests
{
    private readonly LanguageController _languageController;

    public LanguageControllerTests()
    {
        var v = new TestVariables();
        _languageController = new LanguageController(v.LanguageRepository);
    }

    #region Post/Create
    [Fact]
    public async Task Post_language_returns_status_created()
    {
        var language = new CreateLanguageDTO("French");

        var actual = await _languageController.Post(language) as CreatedResult;

        Assert.Equal((int)HttpStatusCode.Created, actual?.StatusCode);
    }

    [Fact]
    public async Task Post_language_returns_status_conflict()
    {
        var language = new CreateLanguageDTO("Danish");

        var actual = await _languageController.Post(language) as ConflictResult;

        Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
    }

    [Fact]
    public async Task Post_language_returns_status_badRequest()
    {
        var language = new CreateLanguageDTO("");

        var actual = await _languageController.Post(language) as BadRequestResult;

        Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
    }
    #endregion

    #region Get/Read
    [Fact]
    public async Task Get_all_languages_returns_status_ok()
    {
        var response = await _languageController.Get();
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
    }

    [Fact]
    public async Task Get_language_returns_status_ok()
    {
        var response = await _languageController.Get(1);
        var actual = response.Result as OkObjectResult;

        Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
    }

    [Fact]
    public async Task Get_language_returns_status_notFound()
    {
        var response = await _languageController.Get(4);
        var actual = response.Result as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }
    #endregion

    #region Delete
    [Fact]
    public async Task Delete_language_returns_status_noContent()
    {
        var response = await _languageController.Delete(3);
        var actual = response as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
    }

    [Fact]
    public async Task Delete_language_returns_status_notFound()
    {
        var response = await _languageController.Delete(4);
        var actual = response as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }
    #endregion

    #region Put/Update
    [Fact]
    public async Task Put_language_returns_status_noContent()
    {
        var language = new LanguageDTO(1, "French");
        var response = await _languageController.Put(language);
        var actual = response as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
    }

    [Fact]
    public async Task Put_language_returns_status_conflict()
    {
        var language = new LanguageDTO(1, "Swedish");
        var response = await _languageController.Put(language);
        var actual = response as ConflictResult;

        Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
    }

    [Fact]
    public async Task Put_language_returns_status_badRequest()
    {
        var language = new LanguageDTO(1, "");
        var response = await _languageController.Put(language);
        var actual = response as BadRequestResult;

        Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
    }

    [Fact]
    public async Task Put_language_returns_status_notFound()
    {
        var language = new LanguageDTO(4, "French");
        var response = await _languageController.Put(language);
        var actual = response as NotFoundResult;

        Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
    }

    #endregion

}