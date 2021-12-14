namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class MaterialRepositoryTests
    {
        // Mock Repository
        private readonly TestVariables _v;

        // Create material test variables
        private readonly CreateMaterialDTO _CreateMaterialDTO;
        private readonly CreateMaterialDTO _CreateMaterialDTOConflict;
        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongTitle;
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

        private readonly CreateMaterialDTO _CreateMaterialDTOTooLongSummary;

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

            var material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);

            _CreateMaterialDTO = material;

            // material testing
            title = "Material 1";
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOConflict = material;

            title = "MaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterial";
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongTitle = material;

            title = "Title"; // title reset

            // tags testing
            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("Tag1", 10) };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
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
            medias = new List<CreateMediaDTO> { new CreateMediaDTO("Book"), new CreateMediaDTO("Book") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
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
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
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
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
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

            levels = new List<CreateLevelDTO> { new CreateLevelDTO("BachelorBachelorBachelorBachelorBachelorBachelorBachelorBachelorBachelorBachelorBachelor") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongLevelName = material;

            levels = new List<CreateLevelDTO> { new CreateLevelDTO("Bachelor"), new CreateLevelDTO("Bachelor") };
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTODuplicateLevel = material;

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
            _CreateMaterialDTODuplicateProgrammingLanguage = material;

            programmingLanguages = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("C#") }; // programming language reset

            // Summary

            summary = "TENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERS";
            material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _CreateMaterialDTOTooLongSummary = material;

            summary = "i am a material"; // summary reset

            // update testing

            title = "New title";
            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("API", 90), new CreateWeightedTagDTO("RAD", 50) };
            ratings = new List<CreateRatingDTO> { new CreateRatingDTO(5, "Kim"), new CreateRatingDTO(9, "Poul") };
            levels = new List<CreateLevelDTO> { new CreateLevelDTO("PHD"), new CreateLevelDTO("Bachelor") };
            programmingLanguages = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("C#"), new CreateProgrammingLanguageDTO("F#") };
            medias = new List<CreateMediaDTO> { new CreateMediaDTO("Video") };
            language = new CreateLanguageDTO("English");
            content = "Banana Phone";
            summary = "i am materialized";
            url = "anotherUrl.com";

            var updateMaterial = new MaterialDTO(1, tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _UpdateMaterialDTO = updateMaterial;

            title = "Title";
            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("API", 10) };
            ratings = new List<CreateRatingDTO> { new CreateRatingDTO(5, "Me") };
            levels = new List<CreateLevelDTO> { new CreateLevelDTO("PHD") };
            programmingLanguages = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("C#") };
            medias = new List<CreateMediaDTO> { new CreateMediaDTO("Book") };
            language = new CreateLanguageDTO("Danish");
            authors = new List<CreateAuthorDTO>() { new CreateAuthorDTO("Peter", "Petersen") };
            content = "null";
            summary = "i am a material";
            url = "url.com";

            updateMaterial = new MaterialDTO(10, tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _UpdateMaterialDTONotFound = updateMaterial;

            title = "Material 2";
            updateMaterial = new MaterialDTO(1, tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
            _UpdateMaterialDTOConflict = updateMaterial;
            title = "Title";

            tags = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("Tag1", 10), new CreateWeightedTagDTO("RasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmus", 1000) };
            updateMaterial = new MaterialDTO(10, tags, ratings, levels, programmingLanguages, medias, language, summary, url, content, title, authors, dateTime);
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

            Assert.Equal(expected, actual);
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
        public async Task CreateAsync_material_returns_bad_request_on_too_long_title()
        {
            var material = _CreateMaterialDTOTooLongTitle;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

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
        public async Task CreateAsync_material_returns_bad_request_on_wrong_tag_weight()
        {
            var material = _CreateMaterialDTOTagWeightTooHigh;

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
        public async Task CreateAsync_material_returns_bad_request_on_duplicate_tag()
        {
            var material = _CreateMaterialDTODuplicateTag;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        // media

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
        public async Task CreateAsync_material_returns_created_on_duplicate_level()
        {
            var material = _CreateMaterialDTODuplicateLevel;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        // Language

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_language_not_existing()
        {
            var material = _CreateMaterialDTOLanguageNotExisting;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_too_long_language_name()
        {
            var material = _CreateMaterialDTOTooLongLanguageName;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        // Programming Language

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_programming_language_not_existing()
        {
            var material = _CreateMaterialDTOProgrammingLanguageNotExisting;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_too_long_programming_language_name()
        {
            var material = _CreateMaterialDTOTooLongProgrammingLanguageName;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_material_returns_created_on_duplicate_programming_language()
        {
            var material = _CreateMaterialDTODuplicateProgrammingLanguage;

            var response = await _v.MaterialRepository.CreateAsync(material);

            var actual = (response.Item1, response.Item2.Id);

            var expected = (Status.BadRequest, -1);

            Assert.Equal(expected, actual);
        }

        // Summary

        [Fact]
        public async Task CreateAsync_material_returns_bad_request_on_too_long_summary()
        {
            var material = _CreateMaterialDTOTooLongSummary;

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
            var expected1 = (2, "Rasmus");
            var expected2 = (5, "Kim");
            var expected3 = (9, "Poul");

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
            var expected1 = "C++";

            Assert.Equal(expectedStatus, actualStatus);
            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual.Name));
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_media_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actualStatus = response.Item1;
            var actuals = response.Item2.Medias;

            var expectedStatus = Status.Found;
            var expected1 = "Video";

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
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actual = (response.Item1, response.Item2.Language.Name);

            var expected = (Status.Found, "Swedish");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_summary_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actual = (response.Item1, response.Item2.Summary);

            var expected = (Status.Found, "I am material 3");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_url_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actual = (response.Item1, response.Item2.URL);

            var expected = (Status.Found, "url3.com");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_material_by_id_returns_material_check_content_and_status_found()
        {
            var response = await _v.MaterialRepository.ReadAsync(3);

            var actual = (response.Item1, response.Item2.Content);

            var expected = (Status.Found, "Content 3");

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
            var actuals = response.Select(e => e.Tags);

            var expected1 = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("SOLID", 10) };
            var expected2 = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("API", 90), new CreateWeightedTagDTO("SOLID", 10) };
            var expected3 = new List<CreateWeightedTagDTO> { new CreateWeightedTagDTO("API", 90), new CreateWeightedTagDTO("RAD", 50), new CreateWeightedTagDTO("SOLID", 10) };

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_ratings()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e);

            var expected1 = new List<CreateRatingDTO> { new CreateRatingDTO(2, "Rasmus") };
            var expected2 = new List<CreateRatingDTO> { new CreateRatingDTO(2, "Rasmus"), new CreateRatingDTO(5, "Kim") };
            var expected3 = new List<CreateRatingDTO> { new CreateRatingDTO(2, "Rasmus"), new CreateRatingDTO(5, "Kim"), new CreateRatingDTO(9, "Poul") };

            var expectedCounter1 = 0;
            var expectedCounter2 = 0;
            var expectedCounter3 = 0;

            foreach (var expectedCreateRating in expected1)
            {
                var expectedRating = expectedCreateRating;
                foreach (var actualRatingDTO1 in actuals.ElementAt(0).Ratings)
                {
                    var actualRating = actualRatingDTO1;
                    if (
                        expectedRating.Value == actualRating.Value &&
                        expectedRating.Reviewer == actualRating.Reviewer &&
                        expectedRating.TimeStamp.ToString() == actualRating.TimeStamp.ToString()
                    )
                    {
                        expectedCounter1++;
                    }
                }
            }

            foreach (var expectedCreateRating in expected2)
            {
                var expectedRating = expectedCreateRating;
                foreach (var actualRatingDTO1 in actuals.ElementAt(1).Ratings)
                {
                    var actualRating = actualRatingDTO1;
                    if (
                        expectedRating.Value == actualRating.Value &&
                        expectedRating.Reviewer == actualRating.Reviewer &&
                        expectedRating.TimeStamp.ToString() == actualRating.TimeStamp.ToString()
                    )
                    {
                        expectedCounter2++;
                    }
                }
            }

            foreach (var expectedCreateRating in expected3)
            {
                var expectedRating = expectedCreateRating;
                foreach (var actualRatingDTO1 in actuals.ElementAt(2).Ratings)
                {
                    var actualRating = actualRatingDTO1;
                    if (
                        expectedRating.Value == actualRating.Value &&
                        expectedRating.Reviewer == actualRating.Reviewer &&
                        expectedRating.TimeStamp.ToString() == actualRating.TimeStamp.ToString()
                    )
                    {
                        expectedCounter3++;
                    }
                }
            }

            Assert.Equal(1, expectedCounter1);
            Assert.Equal(2, expectedCounter2);
            Assert.Equal(3, expectedCounter3);

        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_levels()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.Levels);

            var expected1 = new List<CreateLevelDTO> { new CreateLevelDTO("Bachelor"), new CreateLevelDTO("Master") };
            var expected2 = new List<CreateLevelDTO> { new CreateLevelDTO("PHD"), new CreateLevelDTO("Bachelor") };
            var expected3 = new List<CreateLevelDTO> { new CreateLevelDTO("PHD"), new CreateLevelDTO("Master"), new CreateLevelDTO("Bachelor") };

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_authors()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.Authors);

            var expected1 = new List<CreateAuthorDTO>() { new CreateAuthorDTO("Rasmus", "Kristensen") };
            var expected2 = new List<CreateAuthorDTO>() { new CreateAuthorDTO("Alex", "Su"), new CreateAuthorDTO("Rasmus", "Kristensen") };
            var expected3 = new List<CreateAuthorDTO>() { new CreateAuthorDTO("Thor", "Lind"), new CreateAuthorDTO("Alex", "Su"), new CreateAuthorDTO("Rasmus", "Kristensen") };

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_programming_languages()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.ProgrammingLanguages);

            var expected1 = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("C#") };
            var expected2 = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("F#") };
            var expected3 = new List<CreateProgrammingLanguageDTO> { new CreateProgrammingLanguageDTO("C++") };

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_medias()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.Medias);

            var expected1 = new List<CreateMediaDTO> { new CreateMediaDTO("Book"), new CreateMediaDTO("Report") };
            var expected2 = new List<CreateMediaDTO> { new CreateMediaDTO("Report") };
            var expected3 = new List<CreateMediaDTO> { new CreateMediaDTO("Video") };

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_language()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.Language);

            var expected1 = new CreateLanguageDTO("Danish");
            var expected2 = new CreateLanguageDTO("English");
            var expected3 = new CreateLanguageDTO("Swedish");

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_summary()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.Summary);

            var expected1 = "I am material 1";
            var expected2 = "I am material 2";
            var expected3 = "I am material 3";

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_url()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.URL);

            var expected1 = "url1.com";
            var expected2 = "url2.com";
            var expected3 = "url3.com";

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_material_check_content()
        {
            var response = await _v.MaterialRepository.ReadAsync();
            var actuals = response.Select(e => e.Content);

            var expected1 = "Content 1";
            var expected2 = "Content 2";
            var expected3 = "Content 3";

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        [Fact]
        public async Task ReadAsync_given_search_form_input_with_rating_above_avergage_of_10_should_return_material()
        {
            SearchForm input = new SearchForm("",
                new TagDTO[0],
                new LevelDTO[0],
                new ProgrammingLanguageDTO[0],
                new LanguageDTO[0],
                new MediaDTO[0],
                10);

            (var status, var response) = await _v.MaterialRepository.ReadAsync(input);

            Assert.Equal(Status.NotFound, status);
        }

        [Fact]
        public async Task ReadAsync_given_search_material_form_input_with_rating_above_average_of_0_should_return_all_materials()
        {
            SearchForm input = new SearchForm("",
                new TagDTO[0],
                new LevelDTO[0],
                new ProgrammingLanguageDTO[0],
                new LanguageDTO[0],
                new MediaDTO[0],
                0);

            (var status, var response) = await _v.MaterialRepository.ReadAsync(input);
            var actualCount = _v.Context.Materials.Select(m => m).Count();

            Assert.Equal(Status.Found, status);
            Assert.Equal(actualCount, response.Count());
        }

        [Fact]
        public async Task ReadAsync_given_search_material_form_input_with_rating_above_average_of_3_should_return_two_materials()
        {
            SearchForm input = new SearchForm("",
                new TagDTO[0],
                new LevelDTO[0],
                new ProgrammingLanguageDTO[0],
                new LanguageDTO[0],
                new MediaDTO[0],
                3);


            (var status, var response) = await _v.MaterialRepository.ReadAsync(input);

            Assert.Equal(Status.Found, status);
            Assert.Equal(2, response.Count());
        }

        [Fact]
        public async Task ReadAsync_given_programming_language_filter_c_sharp_should_only_return_materials_with_csharp()
        {
            SearchForm input = new SearchForm("",
                new TagDTO[] { new TagDTO(1, "hathathat") },
                new LevelDTO[] { new LevelDTO(1, "hathathat") },
                new ProgrammingLanguageDTO[] { new ProgrammingLanguageDTO(1, "C#") },
                new LanguageDTO[] { new LanguageDTO(1, "hathathat") },
                new MediaDTO[] { new MediaDTO(1, "hathathat") },
                1);

            (var status, var response) = await _v.MaterialRepository.ReadAsync(input);

            Assert.Equal(Status.Found, status);
            Assert.Equal(1, response.Count);
        }

        [Fact]
        public async Task ReadAsync_given_filter_than_doesnt_exist_return_no_materials()
        {
            SearchForm input = new SearchForm("",
                new TagDTO[] { new TagDTO(1, "hathathat")},
                new LevelDTO[] { new LevelDTO(1, "hathathat") },
                new ProgrammingLanguageDTO[] { new ProgrammingLanguageDTO(1, "Lisp") },
                new LanguageDTO[] { new LanguageDTO(1, "hathathat") },
                new MediaDTO[] { new MediaDTO(1, "hathathat") },
                1); 

            (var status, var response) = await _v.MaterialRepository.ReadAsync(input);

            Assert.Equal(Status.NotFound, status);
            Assert.Equal(0, response.Count);
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
        public async Task UpdateAsync_material_by_id_returns_material_returns_new_tags()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actuals = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.Tags;

            var expected1 = ("API", 90);
            var expected2 = ("RAD", 50);

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, (actual.Name, actual.Weight)),
                actual => Assert.Equal(expected2, (actual.Name, actual.Weight)));
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_material_returns_new_ratings()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actuals = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.Ratings;

            var expected1 = (5, "Kim");
            var expected2 = (9, "Poul");

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, (actual.Value, actual.Reviewer)),
                actual => Assert.Equal(expected2, (actual.Value, actual.Reviewer)));
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_material_returns_new_levels()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actuals = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.Levels;

            var expected1 = "Bachelor";
            var expected2 = "PHD";

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual.Name),
                actual => Assert.Equal(expected2, actual.Name));
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_material_returns_new_programming_languages()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actuals = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.ProgrammingLanguages;

            var expected1 = "C#";
            var expected2 = "F#";

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual.Name),
                actual => Assert.Equal(expected2, actual.Name));
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_material_returns_new_medias()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actuals = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.Medias;

            var expected1 = "Video";

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual.Name));
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_new_language()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actual = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.Language.Name;

            var expected = "English";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_new_content()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actual = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.Content;

            var expected = "Banana Phone";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_new_summary()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actual = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.Summary;

            var expected = "i am materialized";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_material_by_id_returns_new_url()
        {
            var updateMaterialDTO = _UpdateMaterialDTO;

            await _v.MaterialRepository.UpdateAsync(updateMaterialDTO);

            var actual = _v.MaterialRepository.ReadAsync(updateMaterialDTO.Id).Result.Item2.URL;

            var expected = "anotherUrl.com";

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

        #region Helpers

        [Fact]
        public async Task MayContainProgrammingLanguage_given_materials_with_c_sharp_pl_should_find_materials()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                new ProgrammingLanguageDTO[] { new ProgrammingLanguageDTO(1, "C#") },
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainProgrammingLanguage(input);
            var material = await _v.Context.Materials.FirstAsync();

            var actual = func.Invoke(material);

            Assert.True(actual);
        }

        [Fact]
        public async Task MayContainProgrammingLanguage_given_materials_with_no_pls_should_find_materials()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainProgrammingLanguage(input);
            var material = await _v.Context.Materials.FirstAsync();

            var actual = func.Invoke(material);

            Assert.True(actual);
        }


        [Fact]
        public async Task MayContainProgrammingLanguage_given_search_input_with_programming_language_clojure_should_not_find_materials()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                new ProgrammingLanguageDTO[] { new ProgrammingLanguageDTO(1, "Clojure") },
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainProgrammingLanguage(input);

            await _v.Context.Materials.ForEachAsync(material => Assert.False(func.Invoke(material)));
        }

        [Fact]
        public async Task MayContainLanguage_given_material_with_language_danish_should_find_material()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                new LanguageDTO[] { new LanguageDTO(1, "Danish") },
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainLanguage(input);
            var material = await _v.Context.Materials.FirstAsync();

            var actual = func.Invoke(material);

            Assert.True(actual);
        }

        [Fact]
        public async Task MayContainLanguage_given_material_with_no_language_given_should_find_materials()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainLanguage(input);
            var material = await _v.Context.Materials.FirstAsync();

            var actual = func.Invoke(material);

            Assert.True(actual);
        }

        [Fact]
        public async Task MayContainLanguage_given_material_with_language_volapyk_should_not_find_material()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                new LanguageDTO[] { new LanguageDTO(1, "Volapyk") },
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainLanguage(input);
            var material = await _v.Context.Materials.FirstAsync();

            var actual = func.Invoke(material);

            Assert.False(actual);
        }

        [Fact]
        public async Task MayContainMedia_search_with_no_given_media_should_find_material()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainMedia(input);
            var material = await _v.Context.Materials.ToListAsync();

            var found = material.Where(func.Invoke).Count();

            Assert.Equal(3, found);
        }

        [Fact]
        public async Task MayContainMedia_search_with_media_book_should_return_1_material()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                new MediaDTO[] { new MediaDTO(1, "Book") },
                1);

            var func = MaterialRepository.MayContainMedia(input);
            var materials = await _v.Context.Materials.ToListAsync();

            var found = materials.Where(material => func.Invoke(material)).Count();

            Assert.Equal(1, found);
        }

        [Fact]
        public async Task MayContainMedia_search_with_media_mysterious_format_should_return_no_materials()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                new MediaDTO[] { new MediaDTO(1, "Mysterious Format") },
                1);

            var func = MaterialRepository.MayContainMedia(input);
            var materials = await _v.Context.Materials.ToListAsync();

            var found = materials.Where(material => func.Invoke(material)).Count();

            Assert.Equal(0, found);
        }

        [Fact]
        public async Task MayContainTag_search_with_no_tags_should_return_all_materials()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainTag(input);
            var materials = await _v.Context.Materials.ToListAsync();

            var found = materials.Where(material => func.Invoke(material)).Count();

            Assert.Equal(3, found);
        }

        [Fact]
        public async Task MayContainTag_search_with_tag_solid_should_return_1_element()
        {
            SearchForm input = new SearchForm("",
                new TagDTO[] { new TagDTO(1, "SOLID") },
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainTag(input);
            var materials = await _v.Context.Materials.ToListAsync();

            var found = materials.Where(material => func.Invoke(material)).Count();

            Assert.Equal(3, found);
        }

        [Fact]
        public async Task MayContainTag_search_with_tag_mystery_tag_should_return_no_element()
        {
            SearchForm input = new SearchForm("",
                new TagDTO[] { new TagDTO(1, "MYSTERIOUS TAG") },
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainTag(input);
            var materials = await _v.Context.Materials.ToListAsync();

            var found = materials.Where(material => func.Invoke(material)).Count();

            Assert.Equal(0, found);
        }

        [Fact]
        public async Task MayContainLevel_search_with_no_level_returns_all_matrials()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                Array.Empty<LevelDTO>(),
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainLevel(input);
            var materials = await _v.Context.Materials.ToListAsync();

            var found = materials.Where(material => func.Invoke(material)).Count();

            Assert.Equal(3, found);
        }

        [Fact]
        public async Task MayContainLevel_search_with_level_phd_returns_1_matrials()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                new LevelDTO[] { new LevelDTO(1, "PHD") },
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);

            var func = MaterialRepository.MayContainLevel(input);
            var materials = await _v.Context.Materials.ToListAsync();

            var found = materials.Where(func.Invoke).Count();

            Assert.Equal(2, found);
        }

        [Fact]
        public async Task MayContainLevel_search_with_level_mysterious_degree_should_return_no_materials()
        {
            SearchForm input = new SearchForm("",
                Array.Empty<TagDTO>(),
                new LevelDTO[] { new LevelDTO(1, "mysterious degree") },
                Array.Empty<ProgrammingLanguageDTO>(),
                Array.Empty<LanguageDTO>(),
                Array.Empty<MediaDTO>(),
                1);


            var repo = new MaterialRepository(_v.Context);

            var func = MaterialRepository.MayContainLevel(input);
            var materials = await _v.Context.Materials.ToListAsync();

            var found = materials.Where(material => func.Invoke(material)).Count();

            Assert.Equal(0, found);
        }

        #endregion
    }
}
