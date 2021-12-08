namespace WebService.Core.Server.Tests.ControllerTests
{
    public class MaterialControllerTests
    {
        private readonly MaterialController _materialController;
        private readonly TestVariables _v;

        public MaterialControllerTests()
        {
            _v = new TestVariables();
            _materialController = new MaterialController(_v.MaterialRepository);
        }

        #region Post/Create
        [Fact]
        public async Task Post_material_returns_status_created()
        {
            var material = new CreateMaterialDTO(new CreateWeightedTagDTO[] {new CreateWeightedTagDTO("API",10)},new CreateRatingDTO[] { }, new CreateLevelDTO[] {new CreateLevelDTO("PHD")},new CreateProgrammingLanguageDTO[] {new CreateProgrammingLanguageDTO("C#")},new CreateMediaDTO[] {new CreateMediaDTO("Book")},new CreateLanguageDTO("Danish"),"Summary","Url","Content","Title", new CreateAuthorDTO[] {},System.DateTime.UtcNow);

            var actual = await _materialController.Post(material) as CreatedResult;

            Assert.Equal((int)HttpStatusCode.Created, actual?.StatusCode);
        }

        [Fact]
        public async Task Post_material_returns_status_conflict()
        {
            var material = new CreateMaterialDTO(new CreateWeightedTagDTO[] { new CreateWeightedTagDTO("API", 10) }, new CreateRatingDTO[] { }, new CreateLevelDTO[] { new CreateLevelDTO("PHD") }, new CreateProgrammingLanguageDTO[] { new CreateProgrammingLanguageDTO("C#") }, new CreateMediaDTO[] { new CreateMediaDTO("Book") }, new CreateLanguageDTO("Danish"), "Summary", "Url", "Content", "Material 2", new CreateAuthorDTO[] { }, System.DateTime.UtcNow);

            var actual = await _materialController.Post(material) as ConflictResult;

            Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
        }

        [Fact]
        public async Task Post_material_returns_status_badRequest()
        {
            var material = new CreateMaterialDTO(new CreateWeightedTagDTO[] { new CreateWeightedTagDTO("Some tag", 10) }, new CreateRatingDTO[] { }, new CreateLevelDTO[] { }, new CreateProgrammingLanguageDTO[] { }, new CreateMediaDTO[] { }, new CreateLanguageDTO("Language"), "Summary", "Url", "Content", "Material 2", new CreateAuthorDTO[] { }, System.DateTime.UtcNow);

            var actual = await _materialController.Post(material) as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
        }
        #endregion

        #region Get/Read
        [Fact]
        public async Task Get_all_materials_returns_status_ok()
        {
            var response = await _materialController.Get();
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }

        [Fact]
        public async Task Get_material_returns_status_ok()
        {
            var response = await _materialController.Get(1);
            var actual = response.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, actual?.StatusCode);
        }

        [Fact]
        public async Task Get_material_returns_status_notFound()
        {
            var response = await _materialController.Get(4);
            var actual = response.Result as NotFoundObjectResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_material_returns_status_noContent()
        {
            var response = await _materialController.Delete(3);
            var actual = response as NoContentResult;

            Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
        }

        [Fact]
        public async Task Delete_material_returns_status_notFound()
        {
            var response = await _materialController.Delete(4);
            var actual = response as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }
        #endregion

        #region Put/Update
        [Fact]
        public async Task Put_material_returns_status_noContent()
        {
            var material = new MaterialDTO(1, new CreateWeightedTagDTO[] { new CreateWeightedTagDTO("API", 10) }, new CreateRatingDTO[] { }, new CreateLevelDTO[] { new CreateLevelDTO("PHD") }, new CreateProgrammingLanguageDTO[] { new CreateProgrammingLanguageDTO("C#") }, new CreateMediaDTO[] { new CreateMediaDTO("Book") }, new CreateLanguageDTO("Danish"), "Summary", "Url", "Content", "Title new", new CreateAuthorDTO[] { }, System.DateTime.UtcNow);
            var response = await _materialController.Put(material);
            var actual = response as NoContentResult;

            Assert.Equal((int)HttpStatusCode.NoContent, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_material_returns_status_conflict()
        {
            var material = new MaterialDTO(1, new CreateWeightedTagDTO[] { new CreateWeightedTagDTO("API", 10) }, new CreateRatingDTO[] { }, new CreateLevelDTO[] { new CreateLevelDTO("PHD") }, new CreateProgrammingLanguageDTO[] { new CreateProgrammingLanguageDTO("C#") }, new CreateMediaDTO[] { new CreateMediaDTO("Book") }, new CreateLanguageDTO("Danish"), "Summary", "Url", "Content", "Material 2", new CreateAuthorDTO[] { }, System.DateTime.UtcNow);
            var response = await _materialController.Put(material);
            var actual = response as ConflictResult;

            Assert.Equal((int)HttpStatusCode.Conflict, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_material_returns_status_badRequest()
        {
            var material = new MaterialDTO(1, new CreateWeightedTagDTO[] { new CreateWeightedTagDTO("Some tag", 10) }, new CreateRatingDTO[] { }, new CreateLevelDTO[] { }, new CreateProgrammingLanguageDTO[] { }, new CreateMediaDTO[] { }, new CreateLanguageDTO("Language"), "Summary", "Url", "Content", "Material 2", new CreateAuthorDTO[] { }, System.DateTime.UtcNow);
            var response = await _materialController.Put(material);
            var actual = response as BadRequestResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, actual?.StatusCode);
        }

        [Fact]
        public async Task Put_material_returns_status_notFound()
        {
            var material = new MaterialDTO(4, new CreateWeightedTagDTO[] { new CreateWeightedTagDTO("API", 10) }, new CreateRatingDTO[] { }, new CreateLevelDTO[] { new CreateLevelDTO("PHD") }, new CreateProgrammingLanguageDTO[] { new CreateProgrammingLanguageDTO("C#") }, new CreateMediaDTO[] { new CreateMediaDTO("Book") }, new CreateLanguageDTO("Danish"), "Summary", "Url", "Content", "Title new", new CreateAuthorDTO[] { }, System.DateTime.UtcNow);
            var response = await _materialController.Put(material);
            var actual = response as NotFoundResult;

            Assert.Equal((int)HttpStatusCode.NotFound, actual?.StatusCode);
        }

        #endregion

    }
}