using System.Net;

namespace WebService.Core.Server.Tests.ControllerTests
{
    public class TagControllerTests
    {
        private TagController _tagController;
        private TestVariables _v;

        public TagControllerTests()
        {
            _v = new TestVariables();
            _tagController = new TagController(_v.TagRepository);
        }

        [Fact]
        public async Task Create_returns_status_created()
        {
            var tag = new CreateTagDTO("Database",1);

            var actual = await _tagController.Post(tag) as CreatedResult;

            Assert.Equal((int)HttpStatusCode.Created, actual?.StatusCode);
        }

        [Fact]
        public async Task Create_returns_status_conflict()
        {
            var tag = new CreateTagDTO("SOLID", 1);

            var actual = await _tagController.Post(tag) as ConflictResult;

            Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
        }

        [Fact]
        public async Task Create_returns_status_badRequest()
        {
            var tag = new CreateTagDTO("", 1);

            var actual = await _tagController.Post(tag) as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
        }

        [Fact]
        public async Task Read_all_returns_status_ok()
        {
            var response = await _tagController.Get();
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }
    }
}
