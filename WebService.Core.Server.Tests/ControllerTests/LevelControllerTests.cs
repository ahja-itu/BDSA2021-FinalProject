namespace WebService.Core.Server.Tests.ControllerTests
{
    public class LevelControllerTests
    {
        private readonly LevelController _levelController;
        private readonly TestVariables _v;

        public LevelControllerTests()
        {
            _v = new TestVariables();
            _levelController = new LevelController(_v.LevelRepository);
        }

        #region Post/Create
        [Fact]
        public async Task Post_level_returns_status_created()
        {
            var level = new CreateLevelDTO("High school");

            var actual = await _levelController.Post(level) as CreatedResult;

            Assert.Equal((int)HttpStatusCode.Created, actual?.StatusCode);
        }

        [Fact]
        public async Task Post_level_returns_status_conflict()
        {
            var level = new CreateLevelDTO("PHD");

            var actual = await _levelController.Post(level) as ConflictResult;

            Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
        }

        [Fact]
        public async Task Post_level_returns_status_badRequest()
        {
            var level = new CreateLevelDTO("");

            var actual = await _levelController.Post(level) as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
        }
        #endregion

        #region Get/Read
        [Fact]
        public async Task Get_all_levels_returns_status_ok()
        {
            var response = await _levelController.Get();
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }

        [Fact]
        public async Task Get_level_returns_status_ok()
        {
            var response = await _levelController.Get(1);
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }

        [Fact]
        public async Task Get_level_returns_status_notFound()
        {
            var response = await _levelController.Get(4);
            var actual = response.Result as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_level_returns_status_noContent()
        {
            var response = await _levelController.Delete(3);
            var actual = response as NoContentResult;

            Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
        }

        [Fact]
        public async Task Delete_level_returns_status_notFound()
        {
            var response = await _levelController.Delete(4);
            var actual = response as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }
        #endregion

        #region Put/Update
        [Fact]
        public async Task Put_level_returns_status_noContent()
        {
            var level = new LevelDTO(1, "High school");
            var response = await _levelController.Put(level);
            var actual = response as NoContentResult;

            Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_level_returns_status_conflict()
        {
            var level = new LevelDTO(1, "PHD");
            var response = await _levelController.Put(level);
            var actual = response as ConflictResult;

            Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_level_returns_status_badRequest()
        {
            var level = new LevelDTO(1, "");
            var response = await _levelController.Put(level);
            var actual = response as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_level_returns_status_notFound()
        {
            var level = new LevelDTO(4, "High school");
            var response = await _levelController.Put(level);
            var actual = response as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }

        #endregion

    }
}