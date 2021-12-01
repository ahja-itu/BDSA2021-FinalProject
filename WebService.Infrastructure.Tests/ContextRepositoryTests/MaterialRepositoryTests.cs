namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class MaterialRepositoryTests
    {
        private readonly TestVariables _v;
        private readonly CreateMaterialDTO _CreateMaterialDTO;
        private readonly CreateMaterialDTO _CreateMaterialDTOConflict;
        private readonly CreateMaterialDTO _CreateMaterialDTOTagNotExisting;
        private readonly CreateMaterialDTO _CreateMaterialDTODuplicateMedia;
        private readonly CreateMaterialDTO _CreateMaterialDTORatingWrongWeight;
        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongAuthorName;
        private readonly MaterialDTO _UpdateMaterialDTO;
        private readonly MaterialDTO _UpdateMaterialDTONotFound;
        private readonly MaterialDTO _UpdateMaterialDTOConflict;
        private readonly MaterialDTO _UpdateMaterialDTOBadRequest;



        public MaterialRepositoryTests()
        {
            _v = new TestVariables();

            var tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("API", 10) };
            var ratings = new List<CreateRatingDTO> { new CreateRatingDTO(5, "Me") };
            var levels = new List<CreateLevelDTO> { new CreateLevelDTO("PHD") };
            var programmingLanguages = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("C#") };
            var medias = new List<CreateMediaDTO> { new CreateMediaDTO("Book") };
            var language = new CreateLanguageDTO("Danish");
            var authors = new List<CreateAuthorDTO>() { new CreateAuthorDTO("Rasmus", "Kristensen") };
            var title = "Title";
            var dateTime = DateTime.UtcNow;
            var content = "null";
            var summary = "i am a material";
            var url = "url.com";

            var material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);

            _CreateMaterialDTO = material;

            title = "Material 1";
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTOConflict = material;
            title = "Title";

            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("Tag1", 10) };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTOTagNotExisting = material;
            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("API", 10) };

            medias = new List<CreateMediaDTO> { new CreateMediaDTO("Book"), new CreateMediaDTO("book") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTODuplicateMedia = material;
            medias = new List<CreateMediaDTO> { new CreateMediaDTO("Book") };

            ratings = new List<CreateRatingDTO> { new CreateRatingDTO(12, "Me") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTORatingWrongWeight = material;
            ratings = new List<CreateRatingDTO> { new CreateRatingDTO(5, "Me") };

            authors = new List<CreateAuthorDTO>() { new CreateAuthorDTO("RasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmus", "Kristensen") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTOTooLongAuthorName = material;
            authors = new List<CreateAuthorDTO>() { new CreateAuthorDTO("Rasmus", "Kristensen") };

            title = "New title";
            var updateMaterial = new MaterialDTO(1,tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _UpdateMaterialDTO = updateMaterial;
            title = "Title";

            updateMaterial = new MaterialDTO(10, tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _UpdateMaterialDTONotFound = updateMaterial;
            updateMaterial = new MaterialDTO(1, tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);

            title = "Material 2";
            updateMaterial = new MaterialDTO(1, tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _UpdateMaterialDTOConflict = updateMaterial;
            title = "Title";

            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("Tag1", 10), new CreateWeightedTagDTO("RasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmus",1000) };
            updateMaterial = new MaterialDTO(10, tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _UpdateMaterialDTOBadRequest = updateMaterial;
            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("API", 10) };

        }

        #region Create

        [Fact]
        public async Task CreateAsync_material_returns_new_material_with_id()
        {
            var material = _CreateMaterialDTO;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id); 

            var expected = (Status.Created, 4);

            Assert.Equal(expected,actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_conflict_with_exisiting_id()
        {
            var material = _CreateMaterialDTOConflict;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.Conflict, 1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_tag_not_existing()
        {
            var material = _CreateMaterialDTOTagNotExisting;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_duplicate_media()
        {
            var material = _CreateMaterialDTODuplicateMedia;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_wrong_rating_weight()
        {
            var material = _CreateMaterialDTORatingWrongWeight;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_too_long_author_name()
        {
            var material = _CreateMaterialDTOTooLongAuthorName;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }
        #endregion

        #region Read
        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_title_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(1);

            var actual = (response.Item1, response.Item2.Title);

            var expected = (Status.Found, "Material 1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_empty_material_check_titel_and_status_notFound()
        {
            var response = await _v.MaterialRepository.ReadAsync(4);

            var actual = (response.Item1, response.Item2.Title);

            var expected = (Status.NotFound, "");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_titles()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.Title);

            var expected1 = "Material 1";
            var expected2 = "Material 2";
            var expected3 = "Material 3";

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }
        #endregion

        #region Delete
        [Fact]
        public async Task DeleteAsync_material_by_id_returns_status_deleted()
        {
            var actual = await _v.MaterialRepository.DeleteAsync(1);

            var expected = Status.Deleted;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_material_by_id_returns_status_notFound()
        {
            var actual = await _v.MaterialRepository.DeleteAsync(4);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_material_by_id_returns_count_one_less()
        {
            await _v.MaterialRepository.DeleteAsync(3);

            var actual = _v.MaterialRepository.ReadAsync().Result.Count;

            var expected = 2;

            Assert.Equal(expected, actual);
        }
        #endregion

        #region Update
        [Fact]
        public async Task UpdateAsync_material_by_id_returns_status_updated()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            var actual = await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_new_title()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            var response = await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actual = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.Title;

            var expected = "New title";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_status_notFound()
        {
            var updateMaterialDTO = _UpdateMaterialDTONotFound;
    
            var actual = await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_status_Conflict()
        {
            var updateMaterialDTO = _UpdateMaterialDTOConflict;

            var actual = await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var expected = Status.Conflict;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_status_BadRequest()
        {
            var updateMaterialDTO = _UpdateMaterialDTOBadRequest;

            var actual = await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
