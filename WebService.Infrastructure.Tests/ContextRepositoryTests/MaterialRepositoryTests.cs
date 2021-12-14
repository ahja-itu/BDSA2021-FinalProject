namespace WebService.Infrastructure.Tests.ContextRepositoryTests;

public class MaterialRepositoryTests
{
    // Create material test variables
    private readonly CreateMaterialDTO _createMaterialDTO;
    private readonly CreateMaterialDTO _createMaterialDTOConflict;
    private readonly CreateMaterialDTO _createMaterialDTODuplicateAuthor;
    private readonly CreateMaterialDTO _createMaterialDTODuplicateLevel;

    private readonly CreateMaterialDTO _createMaterialDTODuplicateMedia;
    private readonly CreateMaterialDTO _createMaterialDTODuplicateProgrammingLanguage;
    private readonly CreateMaterialDTO _createMaterialDTODuplicateTag;

    private readonly CreateMaterialDTO _createMaterialDTOLanguageNotExisting;

    private readonly CreateMaterialDTO _createMaterialDTOLevelNotExisting;
    private readonly CreateMaterialDTO _createMaterialDTOMediaNotExisting;
    private readonly CreateMaterialDTO _createMaterialDTOMultipleRatingsFromSameUser;

    private readonly CreateMaterialDTO _createMaterialDTOProgrammingLanguageNotExisting;

    private readonly CreateMaterialDTO _createMaterialDTORatingWrongWeight;
    private readonly CreateMaterialDTO _createMaterialDTOTagNotExisting;
    private readonly CreateMaterialDTO _createMaterialDTOTagWeightTooHigh;

    private readonly CreateMaterialDTO _createMaterialDTOTooLongAuthorFirstName;
    private readonly CreateMaterialDTO _createMaterialDTOTooLongAuthorSurName;
    private readonly CreateMaterialDTO _createMaterialDTOTooLongLanguageName;
    private readonly CreateMaterialDTO _createMaterialDTOTooLongLevelName;
    private readonly CreateMaterialDTO _createMaterialDTOTooLongMediaName;
    private readonly CreateMaterialDTO _createMaterialDTOTooLongProgrammingLanguageName;
    private readonly CreateMaterialDTO _createMaterialDTOToolongRatingName;

    private readonly CreateMaterialDTO _createMaterialDTOTooLongSummary;
    private readonly CreateMaterialDTO _createMaterialDTOTooLongTagName;
    private readonly CreateMaterialDTO _createMaterialDTOTooLongTitle;

    private readonly MaterialDTO _updateMaterialDTO;
    private readonly MaterialDTO _updateMaterialDTOBadRequest;
    private readonly MaterialDTO _updateMaterialDTOConflict;

    private readonly MaterialDTO _updateMaterialDTONotFound;

    // Mock Repository
    private readonly TestVariables _v;


    public MaterialRepositoryTests()
    {
        _v = new TestVariables(); // A mock repository

        var createWeightedTagDtos = new List<CreateWeightedTagDTO> {new("API", 10)};
        var tags = createWeightedTagDtos;
        var ratings = new List<CreateRatingDTO> {new(5, "Me")};
        var levels = new List<CreateLevelDTO> {new("PHD")};
        var programmingLanguages = new List<CreateProgrammingLanguageDTO> {new("C#")};
        var medias = new List<CreateMediaDTO> {new("Book")};
        var language = new CreateLanguageDTO("Danish");
        var authors = new List<CreateAuthorDTO> {new("Rasmus", "Kristensen")};
        var title = "Title";
        var dateTime = DateTime.UtcNow;
        var content = "null";
        var summary = "i am a material";
        var url = "url.com";

        var material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary,
            url, content, title, authors, dateTime);

        _createMaterialDTO = material;

        // material testing
        title = "Material 1";
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOConflict = material;

        title =
            "MaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterialMaterial";
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTooLongTitle = material;

        title = "Title"; // title reset

        // tags testing
        tags = new List<CreateWeightedTagDTO> {new("Tag1", 10)};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTagNotExisting = material;

        tags = new List<CreateWeightedTagDTO> {new("SOLID", 101)};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTagWeightTooHigh = material;

        tags = new List<CreateWeightedTagDTO> {new("SOLIDSOLIDSOLIDSOLIDSOLIDSOLIDSOLIDSOLIDSOLIDSOLID", 10)};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTooLongTagName = material;

        tags = new List<CreateWeightedTagDTO> {new("SOLID", 10), new("SOLID", 10)};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTODuplicateTag = material;

        tags = createWeightedTagDtos; // tags reset

        // medias testing
        medias = new List<CreateMediaDTO> {new("Book"), new("Book")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTODuplicateMedia = material;

        medias = new List<CreateMediaDTO> {new("Tv Show")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOMediaNotExisting = material;

        medias = new List<CreateMediaDTO> {new("VideoVideoVideoVideoVideoVideoVideoVideoVideoVideoVideo")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTooLongMediaName = material;

        medias = new List<CreateMediaDTO> {new("Book")}; // medias reset

        // ratings testing
        ratings = new List<CreateRatingDTO> {new(12, "Me")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTORatingWrongWeight = material;

        ratings = new List<CreateRatingDTO> {new(8, "MyssenbergMyssenbergMyssenbergMyssenbergMyssenbergMyssenberg")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOToolongRatingName = material;

        ratings = new List<CreateRatingDTO> {new(10, "Me"), new(7, "Me")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOMultipleRatingsFromSameUser = material;

        ratings = new List<CreateRatingDTO> {new(5, "Me")}; // ratings resest

        // authors testing
        authors = new List<CreateAuthorDTO>
            {new("RasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmus", "Kristensen")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTooLongAuthorFirstName = material;

        authors = new List<CreateAuthorDTO>
            {new("Rasmus", "KristensenKristensenKristensenKristensenKristensenKristensen")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTooLongAuthorSurName = material;

        authors = new List<CreateAuthorDTO> {new("Rasmus", "Kristensen"), new("Rasmus", "Kristensen")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTODuplicateAuthor = material;

        authors = new List<CreateAuthorDTO> {new("Rasmus", "Kristensen")}; // authors reset

        // levels testing
        levels = new List<CreateLevelDTO> {new("Banana")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOLevelNotExisting = material;

        levels = new List<CreateLevelDTO>
            {new("BachelorBachelorBachelorBachelorBachelorBachelorBachelorBachelorBachelorBachelorBachelor")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTooLongLevelName = material;

        levels = new List<CreateLevelDTO> {new("Bachelor"), new("Bachelor")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTODuplicateLevel = material;

        levels = new List<CreateLevelDTO> {new("PHD")}; // levels reset

        // language testing
        language = new CreateLanguageDTO("ChingChong");
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOLanguageNotExisting = material;

        language = new CreateLanguageDTO("DanishDanishDanishDanishDanishDanishDanishDanishDanish");
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTooLongLanguageName = material;

        language = new CreateLanguageDTO("Danish"); // language reset

        // programming language testing
        programmingLanguages = new List<CreateProgrammingLanguageDTO> {new("Java")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOProgrammingLanguageNotExisting = material;

        programmingLanguages = new List<CreateProgrammingLanguageDTO>
            {new("JavaScriptJavaScriptJavaScriptJavaScriptJavaScriptJavaScriptJavaScriptJavaScript")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTooLongProgrammingLanguageName = material;

        programmingLanguages = new List<CreateProgrammingLanguageDTO> {new("Java"), new("Java")};
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTODuplicateProgrammingLanguage = material;

        programmingLanguages = new List<CreateProgrammingLanguageDTO> {new("C#")}; // programming language reset

        // Summary

        summary =
            "TENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERSTENLETTERS";
        material = new CreateMaterialDTO(tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _createMaterialDTOTooLongSummary = material;

        // update testing

        title = "New title";
        tags = new List<CreateWeightedTagDTO> {new("API", 90), new("RAD", 50)};
        ratings = new List<CreateRatingDTO> {new(5, "Kim"), new(9, "Poul")};
        levels = new List<CreateLevelDTO> {new("PHD"), new("Bachelor")};
        programmingLanguages = new List<CreateProgrammingLanguageDTO> {new("C#"), new("F#")};
        medias = new List<CreateMediaDTO> {new("Video")};
        language = new CreateLanguageDTO("English");
        content = "Banana Phone";
        summary = "i am materialized";
        url = "anotherUrl.com";

        var updateMaterial = new MaterialDTO(1, tags, ratings, levels, programmingLanguages, medias, language, summary,
            url, content, title, authors, dateTime);
        _updateMaterialDTO = updateMaterial;

        title = "Title";
        tags = createWeightedTagDtos;
        ratings = new List<CreateRatingDTO> {new(5, "Me")};
        levels = new List<CreateLevelDTO> {new("PHD")};
        programmingLanguages = new List<CreateProgrammingLanguageDTO> {new("C#")};
        medias = new List<CreateMediaDTO> {new("Book")};
        language = new CreateLanguageDTO("Danish");
        authors = new List<CreateAuthorDTO> {new("Peter", "Petersen")};
        content = "null";
        summary = "i am a material";
        url = "url.com";

        updateMaterial = new MaterialDTO(10, tags, ratings, levels, programmingLanguages, medias, language, summary,
            url, content, title, authors, dateTime);
        _updateMaterialDTONotFound = updateMaterial;

        title = "Material 2";
        updateMaterial = new MaterialDTO(1, tags, ratings, levels, programmingLanguages, medias, language, summary, url,
            content, title, authors, dateTime);
        _updateMaterialDTOConflict = updateMaterial;
        title = "Title";

        tags = new List<CreateWeightedTagDTO>
            {new("Tag1", 10), new("RasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmusRasmus", 1000)};
        updateMaterial = new MaterialDTO(10, tags, ratings, levels, programmingLanguages, medias, language, summary,
            url, content, title, authors, dateTime);
        _updateMaterialDTOBadRequest = updateMaterial;
    }

    #region Create

    [Fact]
    public async Task CreateAsync_material_returns_new_material_with_id()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTO);

        var actual = (status, materialDTO.Id);

        var expected = (Status.Created, 4);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_conflict_with_exisiting_id()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOConflict);

        var actual = (status, materialDTO.Id);

        var expected = (Status.Conflict, 1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_title()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTooLongTitle);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    // Tags

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_tag_not_existing()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTagNotExisting);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_wrong_tag_weight()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTagWeightTooHigh);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_tag_name()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTooLongTagName);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_duplicate_tag()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTODuplicateTag);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    // media

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_duplicate_media()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTODuplicateMedia);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_media_not_existing()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOMediaNotExisting);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_media_name()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTooLongMediaName);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    // Ratings

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_wrong_rating_weight()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTORatingWrongWeight);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_rating_name()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOToolongRatingName);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_duplicate_user_with_different_ratings()
    {
        var (status, materialDTO) =
            await _v.MaterialRepository.CreateAsync(_createMaterialDTOMultipleRatingsFromSameUser);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    // Authors

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_author_first_name()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTooLongAuthorFirstName);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_author_sur_name()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTooLongAuthorSurName);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_duplicate_author()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTODuplicateAuthor);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    // Levels

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_level_not_existing()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOLevelNotExisting);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_level_name()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTooLongLevelName);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_created_on_duplicate_level()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTODuplicateLevel);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    // Language

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_language_not_existing()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOLanguageNotExisting);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_language_name()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTooLongLanguageName);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    // Programming Language

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_programming_language_not_existing()
    {
        var (status, materialDTO) =
            await _v.MaterialRepository.CreateAsync(_createMaterialDTOProgrammingLanguageNotExisting);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_programming_language_name()
    {
        var (status, materialDTO) =
            await _v.MaterialRepository.CreateAsync(_createMaterialDTOTooLongProgrammingLanguageName);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_material_returns_created_on_duplicate_programming_language()
    {
        var (status, materialDTO) =
            await _v.MaterialRepository.CreateAsync(_createMaterialDTODuplicateProgrammingLanguage);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    // Summary

    [Fact]
    public async Task CreateAsync_material_returns_bad_request_on_too_long_summary()
    {
        var (status, materialDTO) = await _v.MaterialRepository.CreateAsync(_createMaterialDTOTooLongSummary);

        var actual = (status, materialDTO.Id);

        var expected = (Status.BadRequest, -1);

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Read

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_title_and_status_found()
    {
        var (status, materialDTO) = await _v.MaterialRepository.ReadAsync(1);

        var actual = (status, materialDTO.Title);

        var expected = (Status.Found, "Material 1");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_empty_material_check_titel_and_status_notFound()
    {
        var (status, materialDTO) = await _v.MaterialRepository.ReadAsync(4);

        var actual = (status, materialDTO.Title);

        var expected = (Status.NotFound, "");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_tags_and_status_found()
    {
        var (actualStatus, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = materialDTO.Tags;

        const Status expectedStatus = Status.Found;
        var expected1 = ("API", 90);
        var expected2 = ("RAD", 50);
        var expected3 = ("SOLID", 10);

        Assert.Equal(expectedStatus, actualStatus);
        Assert.Collection(actual,
            actual => Assert.Equal(expected1, (actual.Name, actual.Weight)),
            actual => Assert.Equal(expected2, (actual.Name, actual.Weight)),
            actual => Assert.Equal(expected3, (actual.Name, actual.Weight)));
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_ratings_and_status_found()
    {
        var (actualStatus, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = materialDTO.Ratings;

        const Status expectedStatus = Status.Found;
        var expected1 = (2, "Rasmus");
        var expected2 = (5, "Kim");
        var expected3 = (9, "Poul");

        Assert.Equal(expectedStatus, actualStatus);
        Assert.Collection(actual,
            actual => Assert.Equal(expected1, (actual.Value, actual.Reviewer)),
            actual => Assert.Equal(expected2, (actual.Value, actual.Reviewer)),
            actual => Assert.Equal(expected3, (actual.Value, actual.Reviewer)));
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_levels_and_status_found()
    {
        var (actualStatus, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = materialDTO.Levels;

        const Status expectedStatus = Status.Found;
        const string expected1 = "PHD";
        const string expected2 = "Master";
        const string expected3 = "Bachelor";

        Assert.Equal(expectedStatus, actualStatus);
        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual.Name),
            actual => Assert.Equal(expected2, actual.Name),
            actual => Assert.Equal(expected3, actual.Name));
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_programming_language_and_status_found()
    {
        var (actualStatus, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = materialDTO.ProgrammingLanguages;

        const Status expectedStatus = Status.Found;
        const string expected1 = "C++";

        Assert.Equal(expectedStatus, actualStatus);
        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual.Name));
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_media_and_status_found()
    {
        var (actualStatus, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = materialDTO.Medias;

        const Status expectedStatus = Status.Found;
        const string expected1 = "Video";

        Assert.Equal(expectedStatus, actualStatus);
        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual.Name));
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_authors_and_status_found()
    {
        var (actualStatus, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = materialDTO.Authors;

        var expectedStatus = Status.Found;
        var expected1 = ("Thor", "Lind");
        var expected2 = ("Alex", "Su");
        var expected3 = ("Rasmus", "Kristensen");

        Assert.Equal(expectedStatus, actualStatus);
        Assert.Collection(actual,
            actual => Assert.Equal(expected1, (actual.FirstName, actual.SurName)),
            actual => Assert.Equal(expected2, (actual.FirstName, actual.SurName)),
            actual => Assert.Equal(expected3, (actual.FirstName, actual.SurName)));
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_language_and_status_found()
    {
        var (status, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = (status, materialDTO.Language.Name);

        var expected = (Status.Found, "Swedish");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_summary_and_status_found()
    {
        var (status, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = (status, materialDTO.Summary);

        var expected = (Status.Found, "I am material 3");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_url_and_status_found()
    {
        var (status, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = (status, materialDTO.URL);

        var expected = (Status.Found, "url3.com");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAsync_material_by_id_returns_material_check_content_and_status_found()
    {
        var (status, materialDTO) = await _v.MaterialRepository.ReadAsync(3);

        var actual = (status, materialDTO.Content);

        var expected = (Status.Found, "Content 3");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_titles()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.Title);

        const string expected1 = "Material 1";
        const string expected2 = "Material 2";
        const string expected3 = "Material 3";

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_tags()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.Tags);

        var expected1 = new List<CreateWeightedTagDTO> {new("SOLID", 10)};
        var expected2 = new List<CreateWeightedTagDTO> {new("API", 90), new("SOLID", 10)};
        var expected3 = new List<CreateWeightedTagDTO> {new("API", 90), new("RAD", 50), new("SOLID", 10)};

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_ratings()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e);

        var expected1 = new List<CreateRatingDTO> {new(2, "Rasmus")};
        var expected2 = new List<CreateRatingDTO> {new(2, "Rasmus"), new(5, "Kim")};
        var expected3 = new List<CreateRatingDTO> {new(2, "Rasmus"), new(5, "Kim"), new(9, "Poul")};

        var expectedCounter1 = 0;
        var expectedCounter2 = 0;
        var expectedCounter3 = 0;

        foreach (var expectedCreateRating in expected1)
        foreach (var actualRatingDTO1 in actual.ElementAt(0).Ratings)
            if (
                expectedCreateRating.Value == actualRatingDTO1.Value &&
                expectedCreateRating.Reviewer == actualRatingDTO1.Reviewer &&
                expectedCreateRating.TimeStamp.ToString() == actualRatingDTO1.TimeStamp.ToString()
            )
                expectedCounter1++;

        foreach (var expectedCreateRating in expected2)
        foreach (var actualRatingDTO1 in actual.ElementAt(1).Ratings)
            if (
                expectedCreateRating.Value == actualRatingDTO1.Value &&
                expectedCreateRating.Reviewer == actualRatingDTO1.Reviewer &&
                expectedCreateRating.TimeStamp.ToString() == actualRatingDTO1.TimeStamp.ToString()
            )
                expectedCounter2++;

        foreach (var expectedCreateRating in expected3)
        foreach (var actualRatingDTO1 in actual.ElementAt(2).Ratings)
            if (
                expectedCreateRating.Value == actualRatingDTO1.Value &&
                expectedCreateRating.Reviewer == actualRatingDTO1.Reviewer &&
                expectedCreateRating.TimeStamp.ToString() == actualRatingDTO1.TimeStamp.ToString()
            )
                expectedCounter3++;

        Assert.Equal(1, expectedCounter1);
        Assert.Equal(2, expectedCounter2);
        Assert.Equal(3, expectedCounter3);
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_levels()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.Levels);

        var expected1 = new List<CreateLevelDTO> {new("Bachelor"), new("Master")};
        var expected2 = new List<CreateLevelDTO> {new("PHD"), new("Bachelor")};
        var expected3 = new List<CreateLevelDTO> {new("PHD"), new("Master"), new("Bachelor")};

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_authors()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.Authors);

        var expected1 = new List<CreateAuthorDTO> {new("Rasmus", "Kristensen")};
        var expected2 = new List<CreateAuthorDTO> {new("Alex", "Su"), new("Rasmus", "Kristensen")};
        var expected3 = new List<CreateAuthorDTO> {new("Thor", "Lind"), new("Alex", "Su"), new("Rasmus", "Kristensen")};

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_programming_languages()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.ProgrammingLanguages);

        var expected1 = new List<CreateProgrammingLanguageDTO> {new("C#")};
        var expected2 = new List<CreateProgrammingLanguageDTO> {new("F#")};
        var expected3 = new List<CreateProgrammingLanguageDTO> {new("C++")};

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_medias()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.Medias);

        var expected1 = new List<CreateMediaDTO> {new("Book"), new("Report")};
        var expected2 = new List<CreateMediaDTO> {new("Report")};
        var expected3 = new List<CreateMediaDTO> {new("Video")};

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_language()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.Language);

        var expected1 = new CreateLanguageDTO("Danish");
        var expected2 = new CreateLanguageDTO("English");
        var expected3 = new CreateLanguageDTO("Swedish");

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_summary()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.Summary);

        const string expected1 = "I am material 1";
        const string expected2 = "I am material 2";
        const string expected3 = "I am material 3";

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_url()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.URL);

        const string expected1 = "url1.com";
        const string expected2 = "url2.com";
        const string expected3 = "url3.com";

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_material_check_content()
    {
        var response = await _v.MaterialRepository.ReadAsync();
        var actual = response.Select(e => e.Content);

        const string expected1 = "Content 1";
        const string expected2 = "Content 2";
        const string expected3 = "Content 3";

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual),
            actual => Assert.Equal(expected2, actual),
            actual => Assert.Equal(expected3, actual));
    }

    [Fact]
    public async Task ReadAsync_given_search_form_input_with_rating_above_avergage_of_10_should_return_material()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            10);

        var (status, _) = await _v.MaterialRepository.ReadAsync(input);

        Assert.Equal(Status.NotFound, status);
    }

    [Fact]
    public async Task
        ReadAsync_given_search_material_form_input_with_rating_above_average_of_0_should_return_all_materials()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            0);

        var (status, response) = await _v.MaterialRepository.ReadAsync(input);
        var actualCount = _v.Context.Materials.Select(m => m).Count();

        Assert.Equal(Status.Found, status);
        Assert.Equal(actualCount, response.Count());
    }

    [Fact]
    public async Task
        ReadAsync_given_search_material_form_input_with_rating_above_average_of_3_should_return_two_materials()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            3);


        var (status, response) = await _v.MaterialRepository.ReadAsync(input);

        Assert.Equal(Status.Found, status);
        Assert.Equal(2, response.Count());
    }

    [Fact]
    public async Task ReadAsync_given_programming_language_filter_c_sharp_should_only_return_materials_with_csharp()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            new ProgrammingLanguageDTO[] {new(1, "F#")},
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            1);

        var (status, response) = await _v.MaterialRepository.ReadAsync(input);

        Assert.Equal(Status.Found, status);
        Assert.Equal(1, response.Count);
    }

    [Fact]
    public async Task ReadAsync_given_programming_language_filter_than_doesnt_exist_return_no_materials()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            new ProgrammingLanguageDTO[] {new(1, "Lisp")},
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            1);

        var (status, response) = await _v.MaterialRepository.ReadAsync(input);

        Assert.Equal(Status.NotFound, status);
        Assert.Equal(0, response.Count);
    }

    #endregion

    #region Delete

    [Fact]
    public async Task DeleteAsync_material_by_id_returns_status_deleted()
    {
        var actual = await _v.MaterialRepository.DeleteAsync(1);

        const Status expected = Status.Deleted;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task DeleteAsync_material_by_id_returns_status_notFound()
    {
        var actual = await _v.MaterialRepository.DeleteAsync(4);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task DeleteAsync_material_by_id_returns_count_one_less()
    {
        await _v.MaterialRepository.DeleteAsync(3);

        var actual = _v.MaterialRepository.ReadAsync().Result.Count;

        const int expected = 2;

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Update

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_status_updated()
    {
        var actual = await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_new_title()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.Title;

        const string expected = "New title";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_material_returns_new_tags()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.Tags;

        var expected1 = ("API", 90);
        var expected2 = ("RAD", 50);

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, (actual.Name, actual.Weight)),
            actual => Assert.Equal(expected2, (actual.Name, actual.Weight)));
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_material_returns_new_ratings()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.Ratings;

        var expected1 = (5, "Kim");
        var expected2 = (9, "Poul");

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, (actual.Value, actual.Reviewer)),
            actual => Assert.Equal(expected2, (actual.Value, actual.Reviewer)));
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_material_returns_new_levels()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.Levels;

        const string expected1 = "Bachelor";
        const string expected2 = "PHD";

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual.Name),
            actual => Assert.Equal(expected2, actual.Name));
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_material_returns_new_programming_languages()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.ProgrammingLanguages;

        const string expected1 = "C#";
        const string expected2 = "F#";

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual.Name),
            actual => Assert.Equal(expected2, actual.Name));
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_material_returns_new_medias()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.Medias;

        const string expected1 = "Video";

        Assert.Collection(actual,
            actual => Assert.Equal(expected1, actual.Name));
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_new_language()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.Language.Name;

        const string expected = "English";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_new_content()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.Content;

        const string expected = "Banana Phone";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_new_summary()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.Summary;

        const string expected = "i am materialized";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_new_url()
    {
        await _v.MaterialRepository.UpdateAsync(_updateMaterialDTO);

        var actual = _v.MaterialRepository.ReadAsync(_updateMaterialDTO.Id).Result.Item2.URL;

        const string expected = "anotherUrl.com";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_status_notFound()
    {
        var actual = await _v.MaterialRepository.UpdateAsync(_updateMaterialDTONotFound);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_status_Conflict()
    {
        var actual = await _v.MaterialRepository.UpdateAsync(_updateMaterialDTOConflict);

        const Status expected = Status.Conflict;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_material_by_id_returns_status_BadRequest()
    {
        var actual = await _v.MaterialRepository.UpdateAsync(_updateMaterialDTOBadRequest);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Helpers

    [Fact]
    public async Task MayContainProgrammingLanguage_given_materials_with_c_sharp_pl_should_find_materials()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            new ProgrammingLanguageDTO[] {new(1, "C#")},
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
        var input = new SearchForm("",
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
    public async Task
        MayContainProgrammingLanguage_given_search_input_with_programming_language_clojure_should_not_find_materials()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            new ProgrammingLanguageDTO[] {new(1, "Clojure")},
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            1);

        var func = MaterialRepository.MayContainProgrammingLanguage(input);

        await _v.Context.Materials.ForEachAsync(material => Assert.False(func.Invoke(material)));
    }

    [Fact]
    public async Task MayContainLanguage_given_material_with_language_danish_should_find_material()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            new LanguageDTO[] {new(1, "Danish")},
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
        var input = new SearchForm("",
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
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            new LanguageDTO[] {new(1, "Volapyk")},
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
        var input = new SearchForm("",
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
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            new MediaDTO[] {new(1, "Book")},
            1);

        var func = MaterialRepository.MayContainMedia(input);
        var materials = await _v.Context.Materials.ToListAsync();

        var found = materials.Count(material => func.Invoke(material));

        Assert.Equal(1, found);
    }

    [Fact]
    public async Task MayContainMedia_search_with_media_mysterious_format_should_return_no_materials()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            new MediaDTO[] {new(1, "Mysterious Format")},
            1);

        var func = MaterialRepository.MayContainMedia(input);
        var materials = await _v.Context.Materials.ToListAsync();

        var found = materials.Count(material => func.Invoke(material));

        Assert.Equal(0, found);
    }

    [Fact]
    public async Task MayContainTag_search_with_no_tags_should_return_all_materials()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            1);

        var func = MaterialRepository.MayContainTag(input);
        var materials = await _v.Context.Materials.ToListAsync();

        var found = materials.Count(material => func.Invoke(material));

        Assert.Equal(3, found);
    }

    [Fact]
    public async Task MayContainTag_search_with_tag_solid_should_return_1_element()
    {
        var input = new SearchForm("",
            new TagDTO[] {new(1, "SOLID")},
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            1);

        var func = MaterialRepository.MayContainTag(input);
        var materials = await _v.Context.Materials.ToListAsync();

        var found = materials.Count(material => func.Invoke(material));

        Assert.Equal(3, found);
    }

    [Fact]
    public async Task MayContainTag_search_with_tag_mystery_tag_should_return_no_element()
    {
        var input = new SearchForm("",
            new TagDTO[] {new(1, "MYSTERIOUS TAG")},
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            1);

        var func = MaterialRepository.MayContainTag(input);
        var materials = await _v.Context.Materials.ToListAsync();

        var found = materials.Count(material => func.Invoke(material));

        Assert.Equal(0, found);
    }

    [Fact]
    public async Task MayContainLevel_search_with_no_level_returns_all_matrials()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            Array.Empty<LevelDTO>(),
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            1);

        var func = MaterialRepository.MayContainLevel(input);
        var materials = await _v.Context.Materials.ToListAsync();

        var found = materials.Count(material => func.Invoke(material));

        Assert.Equal(3, found);
    }

    [Fact]
    public async Task MayContainLevel_search_with_level_phd_returns_1_matrials()
    {
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            new LevelDTO[] {new(1, "PHD")},
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
        var input = new SearchForm("",
            Array.Empty<TagDTO>(),
            new LevelDTO[] {new(1, "mysterious degree")},
            Array.Empty<ProgrammingLanguageDTO>(),
            Array.Empty<LanguageDTO>(),
            Array.Empty<MediaDTO>(),
            1);


        var func = MaterialRepository.MayContainLevel(input);
        var materials = await _v.Context.Materials.ToListAsync();

        var found = materials.Count(material => func.Invoke(material));

        Assert.Equal(0, found);
    }

    #endregion
}