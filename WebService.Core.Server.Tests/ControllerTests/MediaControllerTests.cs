namespace WebService.Core.Server.Tests.ControllerTests
{
    public class MediaControllerTests
    {
        private readonly MediaController _mediaController;
        private readonly TestVariables _v;

        public MediaControllerTests()
        {
            _v = new TestVariables();
            _mediaController = new MediaController(_v.MediaRepository);
        }

        #region Post/Create
        [Fact]
        public async Task Post_media_returns_status_created()
        {
            var media = new CreateMediaDTO("Article");

            var actual = await _mediaController.Post(media) as CreatedResult;

            Assert.Equal((int)HttpStatusCode.Created, actual?.StatusCode);
        }

        [Fact]
        public async Task Post_media_returns_status_conflict()
        {
            var media = new CreateMediaDTO("Book");

            var actual = await _mediaController.Post(media) as ConflictResult;

            Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
        }

        [Fact]
        public async Task Post_media_returns_status_badRequest()
        {
            var media = new CreateMediaDTO("");

            var actual = await _mediaController.Post(media) as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
        }
        #endregion

        #region Get/Read
        [Fact]
        public async Task Get_all_medias_returns_status_ok()
        {
            var response = await _mediaController.Get();
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }

        [Fact]
        public async Task Get_media_returns_status_ok()
        {
            var response = await _mediaController.Get(1);
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }

        [Fact]
        public async Task Get_media_returns_status_notFound()
        {
            var response = await _mediaController.Get(4);
            var actual = response.Result as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_media_returns_status_noContent()
        {
            var response = await _mediaController.Delete(3);
            var actual = response as NoContentResult;

            Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
        }

        [Fact]
        public async Task Delete_media_returns_status_notFound()
        {
            var response = await _mediaController.Delete(4);
            var actual = response as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }
        #endregion

        #region Put/Update
        [Fact]
        public async Task Put_media_returns_status_noContent()
        {
            var media = new MediaDTO(1, "Article");
            var response = await _mediaController.Put(media);
            var actual = response as NoContentResult;

            Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_media_returns_status_conflict()
        {
            var media = new MediaDTO(1, "Report");
            var response = await _mediaController.Put(media);
            var actual = response as ConflictResult;

            Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_media_returns_status_badRequest()
        {
            var media = new MediaDTO(1, "");
            var response = await _mediaController.Put(media);
            var actual = response as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_media_returns_status_notFound()
        {
            var media = new MediaDTO(4, "Article");
            var response = await _mediaController.Put(media);
            var actual = response as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }

        #endregion

    }
}