namespace WebService.Core.Server.Tests.ControllerTests
{
    public class SearchControllerTests
    {
        private readonly SearchController _searchController;
        private readonly TestVariables _v;

        public SearchControllerTests()
        {
            _v = new TestVariables();
            _searchController = new SearchController(_v.SearchAlgorithm);
        }

        [Fact]
        public async Task Get_material_returns_from_searchForm_with_search_algo_status_ok()
        {
            var searchForm = new SearchForm("Hello", new TagDTO[] { new TagDTO(1, "SOLID") }, new LevelDTO[] { }, new ProgrammingLanguageDTO[] { }, new LanguageDTO[] { }, new MediaDTO[] { }, 5);
            var response = await _searchController.Post(searchForm);
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }

        [Fact]
        public async Task Get_material_returns_from_searchForm_with_search_algo_status_notFound()
        {
            var searchForm = new SearchForm("Hello", new TagDTO[] { new TagDTO(4, "DOTNET") }, new LevelDTO[] { new LevelDTO(4, "DOTNET") }, new ProgrammingLanguageDTO[] { new ProgrammingLanguageDTO(4, "DOTNET") }, new LanguageDTO[] { new LanguageDTO(4, "DOTNET") }, new MediaDTO[] { new MediaDTO(4, "DOTNET") }, 10);
            var response = await _searchController.Post(searchForm);
            var actual = response.Result as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }

    }
}