namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class MaterialRepositoryTests
    {
        // Mock Repository
        private readonly TestVariables _v;

        // Create material test variables
        private readonly CreateMaterialDTO _CreateMaterialDTO;
        private readonly CreateMaterialDTO _CreateMaterialDTOConflict;
        private readonly CreateMaterialDTO _CreateMaterialDTOTagNotExisting;
        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongTagName;
        private readonly CreateMaterialDTO _CreateMaterialDTOTagWeightTooHigh;
        private readonly CreateMaterialDTO _CreateMaterialDTODuplicateTag;

        private readonly CreateMaterialDTO _CreateMaterialDTODuplicateMedia;
        private readonly CreateMaterialDTO _CreateMaterialDTOMediaNotExisting;
        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongMediaName;

        private readonly CreateMaterialDTO _CreateMaterialDTORatingWrongWeight;
        private readonly CreateMaterialDTO _CreateMaterialDTOToolongRatingName;
        private readonly CreateMaterialDTO _CreateMaterialDTOMultipleRatingsFromSameUser;

        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongAuthorFirstName;
        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongAuthorSurName;
        private readonly CreateMaterialDTO _CreateMaterialDTODuplicateAuthor;

        private readonly CreateMaterialDTO _CreateMaterialDTOLevelNotExisting;
        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongLevelName;
        private readonly CreateMaterialDTO _CreateMaterialDTODuplicateLevel;

        private readonly CreateMaterialDTO _CreateMaterialDTOLanguageNotExisting;
        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongLanguageName;

        private readonly CreateMaterialDTO _CreateMaterialDTOProgrammingLanguageNotExisting;
        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongProgrammingLanguageName;
        private readonly CreateMaterialDTO _CreateMaterialDTODuplicateProgrammingLanguage;

        private readonly MaterialDTO _UpdateMaterialDTO;
        private readonly MaterialDTO _UpdateMaterialDTONotFound;
        private readonly MaterialDTO _UpdateMaterialDTOConflict;
        private readonly MaterialDTO _UpdateMaterialDTOBadRequest;



        public MaterialRepositoryTests()
        {
            _v = new TestVariables(); // A mock repository

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

            // material testing
            title = "Material 1";
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTOConflict = material;
            title = "Title";

            // tags testing
            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("Tag1", 10) };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTOTagNotExisting = material;

            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("SOLID", 101) };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTagWeightTooHigh = material;

            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("SOLIDSOLIDSOLIDSOLIDSOLIDSOLIDSOLIDSOLIDSOLIDSOLID", 10) };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongTagName = material;

            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("SOLID", 10), new CreateWeightedTagDTO("SOLID", 10) };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTODuplicateTag = material;

            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("API", 10) }; // tags reset

            // medias testing
            medias = new List<CreateMediaDTO> { new CreateMediaDTO("Book"), new CreateMediaDTO("book") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTODuplicateMedia = material;

            medias = new List<CreateMediaDTO> { new CreateMediaDTO("Tv Show") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOMediaNotExisting = material;

            medias = new List<CreateMediaDTO> { new CreateMediaDTO("VideoVideoVideoVideoVideoVideoVideoVideoVideoVideoVideo") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongMediaName = material;

            medias = new List<CreateMediaDTO> { new CreateMediaDTO("Book") }; // medias reset

            // ratings testing
            ratings = new List<CreateRatingDTO> { new CreateRatingDTO(12, "Me") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTORatingWrongWeight = material;

            ratings = new List<CreateRatingDTO> { new CreateRatingDTO(8, "MyssenbergMyssenbergMyssenbergMyssenbergMyssenbergMyssenberg") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOToolongRatingName = material;

            ratings = new List<CreateRatingDTO> { new CreateRatingDTO(10, "Me"), new CreateRatingDTO(7, "Me") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOMultipleRatingsFromSameUser = material;

            ratings = new List<CreateRatingDTO> { new CreateRatingDTO(5, "Me") }; // ratings resest

            // authors testing
            authors = new List<CreateAuthorDTO>() { new CreateAuthorDTO("RasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmus", "Kristensen") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _CreateMaterialDTOTooLongAuthorFirstName = material;

            authors = new List<CreateAuthorDTO>() { new CreateAuthorDTO("Rasmus", "KristensenKristensenKristensenKristensenKristensenKristensen") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongAuthorSurName = material;

            authors = new List<CreateAuthorDTO>() { new CreateAuthorDTO("Rasmus", "Kristensen"), new CreateAuthorDTO("Rasmus", "Kristensen") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTODuplicateAuthor = material;

            authors = new List<CreateAuthorDTO>() { new CreateAuthorDTO("Rasmus", "Kristensen") }; // authors reset

            // levels testing
            levels = new List<CreateLevelDTO> { new CreateLevelDTO("Banana") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOLevelNotExisting = material;

            levels = new List<CreateLevelDTO> { new CreateLevelDTO("BachelorBachelorBachelorBachelorBachelorBachelorBachelor") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongLevelName = material;

            levels = new List<CreateLevelDTO> { new CreateLevelDTO("Bachelor"), new CreateLevelDTO("Bachelor") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongLevelName = material;

            levels = new List<CreateLevelDTO> { new CreateLevelDTO("PHD") }; // levels reset

            // language testing
            language = new CreateLanguageDTO("ChingChong");
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOLanguageNotExisting = material;

            language = new CreateLanguageDTO("DanishDanishDanishDanishDanishDanishDanishDanishDanish");
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongLanguageName = material;

            language = new CreateLanguageDTO("Danish"); // language reset

            // programming language testing
            programmingLanguages = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("Java") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOProgrammingLanguageNotExisting = material;

            programmingLanguages = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("JavaScriptJavaScriptJavaScriptJavaScriptJavaScriptJavaScriptJavaScriptJavaScript") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongProgrammingLanguageName = material;

            programmingLanguages = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("Java"), new CreateProgrammingLanguageDTO("Java") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOProgrammingLanguageNotExisting = material;

            programmingLanguages = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("C#") }; // programming language reset

            // update testing

            title = "New title";
            var updateMaterial = new MaterialDTO(1,tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _UpdateMaterialDTO = updateMaterial;
            title = "Title";

            updateMaterial = new MaterialDTO(10, tags, ratings, levels, programmingLanguages, medias, language, summary,url,content, title, authors, dateTime);
            _UpdateMaterialDTONotFound = updateMaterial;

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

        // Tags

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
        public async Task CreateAsync_material_returns_bad_request_on_duplicate_tag()
        {
            var material = _CreateMaterialDTODuplicateTag;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_too_long_tag_name()
        {
            var material = _CreateMaterialDTOTooLongTagName;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_tag_weight_too_high()
        {
            var material = _CreateMaterialDTOTagWeightTooHigh;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        // Medias

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
        public async Task CreateAsync_material_returns_bad_request_on_media_not_existing()
        {
            var material = _CreateMaterialDTOMediaNotExisting;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_too_long_media_name()
        {
            var material = _CreateMaterialDTOTooLongMediaName;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        // Ratings

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
        public async Task CreateAsync_material_returns_bad_request_on_too_long_rating_name()
        {
            var material = _CreateMaterialDTOToolongRatingName;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_duplicate_user_with_different_ratings()
        {
            var material = _CreateMaterialDTOMultipleRatingsFromSameUser;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        // Authors

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_too_long_author_first_name()
        {
            var material = _CreateMaterialDTOTooLongAuthorFirstName;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_too_long_author_sur_name()
        {
            var material = _CreateMaterialDTOTooLongAuthorSurName;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_duplicate_author()
        {
            var material = _CreateMaterialDTODuplicateAuthor;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        // Levels

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_level_not_existing()
        {
            var material = _CreateMaterialDTOLevelNotExisting;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_too_long_level_name()
        {
            var material = _CreateMaterialDTOTooLongLevelName;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_duplicate_level()
        {
            var material = _CreateMaterialDTODuplicateLevel;

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
        public async Task ReadAsync_material_by_id_returns_material_check_tags_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actualStatus = response.Item1;
            var actuals = response.Item2.Tags;

            var expectedStatus = Status.Found;
            var expected1 = ("API", 90);
            var expected2 = ("RAD", 50);
            var expected3 = ("SOLID", 10);

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, (actual.Name, actual.Weight)),
                actual => Assert.Equal(expected2, (actual.Name, actual.Weight)),
                actual => Assert.Equal(expected3, (actual.Name, actual.Weight)));
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_ratings_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actualStatus = response.Item1;
            var actuals = response.Item2.Ratings;

            var expectedStatus = Status.Found;
            var expected1 = (9, "Poul");
            var expected2 = (5, "Kim");
            var expected3 = (2, "Rasmus");

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, (actual.Value, actual.Reviewer)),
                actual => Assert.Equal(expected2, (actual.Value, actual.Reviewer)),
                actual => Assert.Equal(expected3, (actual.Value, actual.Reviewer)));
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_levels_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actualStatus = response.Item1;
            var actuals = response.Item2.Levels;

            var expectedStatus = Status.Found;
            var expected1 = "PHD";
            var expected2 = "Master";
            var expected3 = "Bachelor";

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual.Name),
                actual => Assert.Equal(expected2, actual.Name),
                actual => Assert.Equal(expected3, actual.Name));
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_programming_language_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actualStatus = response.Item1;
            var actuals = response.Item2.ProgrammingLanguages;

            var expectedStatus = Status.Found;
            var expected1 = "F#";
            var expected2 = "C++";
            var expected3 = "C#";

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual.Name),
                actual => Assert.Equal(expected2, actual.Name),
                actual => Assert.Equal(expected3, actual.Name));
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_media_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actualStatus = response.Item1;
            var actuals = response.Item2.Medias;

            var expectedStatus = Status.Found;
            var expected1 = "Book";

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual.Name));
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_authors_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actualStatus = response.Item1;
            var actuals = response.Item2.Authors;

            var expectedStatus = Status.Found;
            var expected1 = ("Thor", "Lind");
            var expected2 = ("Alex", "Su");
            var expected3 = ("Rasmus", "Kristensen");

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, (actual.FirstName, actual.SurName)),
                actual => Assert.Equal(expected2, (actual.FirstName, actual.SurName)),
                actual => Assert.Equal(expected3, (actual.FirstName, actual.SurName)));
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_language_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(1);

            var actual = (response.Item1, response.Item2.Language.Name);

            var expected = (Status.Found, "Swedish");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_summary_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(1);

            var actual = (response.Item1, response.Item2.Summary);

            var expected = (Status.Found, "I am material 3");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_url_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(1);

            var actual = (response.Item1, response.Item2.URL);

            var expected = (Status.Found, "url3.com");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_content_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(1);

            var actual = (response.Item1, response.Item2.Content);

            var expected = (Status.Found, "null");

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

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_tags()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.Tags.First().Name);

            var expected1 = "SOLID";
            var expected2 = "API";
            var expected3 = "API";

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
            
            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

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
