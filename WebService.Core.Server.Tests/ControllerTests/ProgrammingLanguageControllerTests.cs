namespace WebService.Core.Server.Tests.ControllerTests
{
    public class ProgrammingLanguageControllerTests
    {
        private readonly ProgrammingLanguageController _programmingLanguageController;
        private readonly TestVariables _v;

        public ProgrammingLanguageControllerTests()
        {
            _v = new TestVariables();
            _programmingLanguageController = new ProgrammingLanguageController(_v.ProgrammingLanguageRepository);
        }

        #region Post/Create
        [Fact]
        public async Task Post_programmingLanguage_returns_status_created()
        {
            var programmingLanguage = new CreateProgrammingLanguageDTO("Go");

            var actual = await _programmingLanguageController.Post(programmingLanguage) as CreatedResult;

            Assert.Equal((int)HttpStatusCode.Created, actual?.StatusCode);
        }

        [Fact]
        public async Task Post_programmingLanguage_returns_status_conflict()
        {
            var programmingLanguage = new CreateProgrammingLanguageDTO("C++");

            var actual = await _programmingLanguageController.Post(programmingLanguage) as ConflictResult;

            Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
        }

        [Fact]
        public async Task Post_programmingLanguage_returns_status_badRequest()
        {
            var programmingLanguage = new CreateProgrammingLanguageDTO("");

            var actual = await _programmingLanguageController.Post(programmingLanguage) as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
        }
        #endregion

        #region Get/Read
        [Fact]
        public async Task Get_all_programmingLanguages_returns_status_ok()
        {
            var response = await _programmingLanguageController.Get();
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }

        [Fact]
        public async Task Get_programmingLanguage_returns_status_ok()
        {
            var response = await _programmingLanguageController.Get(1);
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }

        [Fact]
        public async Task Get_programmingLanguage_returns_status_notFound()
        {
            var response = await _programmingLanguageController.Get(4);
            var actual = response.Result as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_programmingLanguage_returns_status_noContent()
        {
            var response = await _programmingLanguageController.Delete(3);
            var actual = response as NoContentResult;

            Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
        }

        [Fact]
        public async Task Delete_programmingLanguage_returns_status_notFound()
        {
            var response = await _programmingLanguageController.Delete(4);
            var actual = response as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }
        #endregion

        #region Put/Update
        [Fact]
        public async Task Put_programmingLanguage_returns_status_noContent()
        {
            var programmingLanguage = new ProgrammingLanguageDTO(1, "Go");
            var response = await _programmingLanguageController.Put(programmingLanguage);
            var actual = response as NoContentResult;

            Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_programmingLanguage_returns_status_conflict()
        {
            var programmingLanguage = new ProgrammingLanguageDTO(1, "C++");
            var response = await _programmingLanguageController.Put(programmingLanguage);
            var actual = response as ConflictResult;

            Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_programmingLanguage_returns_status_badRequest()
        {
            var programmingLanguage = new ProgrammingLanguageDTO(1, "");
            var response = await _programmingLanguageController.Put(programmingLanguage);
            var actual = response as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
        }

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
}