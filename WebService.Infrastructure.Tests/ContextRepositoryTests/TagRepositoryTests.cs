namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class TagRepositoryTests
    {
        private readonly TestVariables _v;

        public TagRepositoryTests()
        {
            _v = new TestVariables();
        }
        #region Create
        [Fact]
        public async Task CreateAsync_tag_returns_new_tag_with_id()
        {
            var tag = new CreateTagDTO("Database", 30);

            var actual = await _v.TagRepository.CreateAsync(tag);

            var expected = (Status.Created, new TagDTO(4, "Database", 30));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_tag_returns_conflict_and_existing_tag()
        {
            var tag = new CreateTagDTO("API", 90);

            var actual = await _v.TagRepository.CreateAsync(tag);

            var expected = (Status.Conflict, new TagDTO(3, "API", 90));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_tag_returns_count_one_more()
        {
            var tag = new CreateTagDTO("Database", 30);

            await _v.TagRepository.CreateAsync(tag);

            var actual = _v.TagRepository.ReadAsync().Result.Count;

            var expected = 4;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_tag_returns_bad_request_on_name_tooLong()
        {
            var tag = new CreateTagDTO("asseocarnisanguineoviscericartilaginonervomedullary", 1);

            var actual = await _v.TagRepository.CreateAsync(tag);

            var expected = (Status.BadRequest, new TagDTO(-1, "asseocarnisanguineoviscericartilaginonervomedullary", 1));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_tag_returns_bad_request_on_name_empty()
        {
            var tag = new CreateTagDTO("", 1);

            var actual = await _v.TagRepository.CreateAsync(tag);

            var expected = (Status.BadRequest, new TagDTO(-1, "", 1));

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task CreateAsync_tag_returns_bad_request_on_name_whitespace()
        {
            var tag = new CreateTagDTO(" ", 1);

            var actual = await _v.TagRepository.CreateAsync(tag);

            var expected = (Status.BadRequest, new TagDTO(-1, " ", 1));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_tag_with_max_length_returns_new_language_with_id()
        {
            var tag = new CreateTagDTO("asseocarnisanguineoviscericartilaginonervomedullar", 1);

            var actual = await _v.TagRepository.CreateAsync(tag);

            var expected = (Status.Created, new TagDTO(4, "asseocarnisanguineoviscericartilaginonervomedullar", 1));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_tag_by_id_returns_status_badRequest_value_negative()
        {
            var tag = new CreateTagDTO("H", -5);

            var actual = await _v.TagRepository.CreateAsync(tag);

            var expected = (Status.BadRequest, new TagDTO(-1, "H", -5));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_tag_returns_bad_request_with_value_too_low()
        {
            var tag = new CreateTagDTO("H", 0);

            var actual = await _v.TagRepository.CreateAsync(tag);

            var expected = (Status.BadRequest, new TagDTO(-1, "H", 0));

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task CreateAsync_tag_returns_bad_request_with_value_too_high()
        {
            var tag = new CreateTagDTO("H", 101);

            var actual = await _v.TagRepository.CreateAsync(tag);

            var expected = (Status.BadRequest, new TagDTO(-1, "H", 101));

            Assert.Equal(expected, actual);
        }
        #endregion

        #region Read

        [Fact]
        public async Task ReadAsync_tag_by_id_returns_tag_and_status_found()
        {
            var actual = await _v.TagRepository.ReadAsync(1);

            var expected = (Status.Found, new TagDTO(1, "SOLID", 10));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_tag_by_id_returns_empty_tag_and_status_notFound()
        {
            var actual = await _v.TagRepository.ReadAsync(4);

            var expected = (Status.NotFound, new TagDTO(-1, "", 0));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_tags()
        {
            var actuals = await _v.TagRepository.ReadAsync();

            var expected1 = new TagDTO(1, "SOLID", 10);
            var expected2 = new TagDTO(2, "RAD", 50);
            var expected3 = new TagDTO(3, "API", 90);

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteAsync_tag_by_id_returns_status_deleted()
        {
            var actual = await _v.TagRepository.DeleteAsync(1);

            var expected = Status.Deleted;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_tag_by_id_returns_status_notFound()
        {
            var actual = await _v.TagRepository.DeleteAsync(4);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_tag_by_id_returns_count_one_less()
        {
            await _v.TagRepository.DeleteAsync(3);

            var actual = _v.TagRepository.ReadAsync().Result.Count;

            var expected = 2;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Update
        [Fact]
        public async Task UpdateAsync_tag_by_id_returns_status_updated()
        {
            var updateTagDTO = new TagDTO(3, "Database", 30);

            var actual = await _v.TagRepository.UpdateAsync(updateTagDTO);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_tag_by_id_read_updated_returns_status_found_and_updated_tag()
        {
            var updateTagDTO = new TagDTO(3, "Database", 30);

            await _v.TagRepository.UpdateAsync(updateTagDTO);

            var actual = await _v.TagRepository.ReadAsync(3);

            var expected = (Status.Found, updateTagDTO);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_tag_by_id_returns_status_notFound()
        {
            var updateTagDTO = new TagDTO(4, "Database", 30);

            var actual = await _v.TagRepository.UpdateAsync(updateTagDTO);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_tag_by_id_returns_status_conflict()
        {
            var updateTagDTO = new TagDTO(3, "SOLID", 10);

            var actual = await _v.TagRepository.UpdateAsync(updateTagDTO);

            var expected = Status.Conflict;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_tag_returns_bad_request_on_name_tooLong()
        {
            var tag = new TagDTO(1, "asseocarnisanguineoviscericartilaginonervomedullary", 1);

            var actual = await _v.TagRepository.UpdateAsync(tag);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_tag_returns_bad_request_on_name_empty()
        {
            var tag = new TagDTO(1, "", 1);

            var actual = await _v.TagRepository.UpdateAsync(tag);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task UpdateAsync_tag_returns_bad_request_on_name_whitespace()
        {
            var tag = new TagDTO(1, " ", 1);

            var actual = await _v.TagRepository.UpdateAsync(tag);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_tag_with_max_length_returns_updated()
        {
            var tag = new TagDTO(1, "asseocarnisanguineoviscericartilaginonervomedullar", 1);

            var actual = await _v.TagRepository.UpdateAsync(tag);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_tag_by_id_returns_status_badRequest_value_too_low()
        {
            var updateTagDTO = new TagDTO(3, "H", 0);

            var actual = await _v.TagRepository.UpdateAsync(updateTagDTO);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_tag_by_id_returns_status_badRequest_value_too_high()
        {
            var updateTagDTO = new TagDTO(3, "H", 101);

            var actual = await _v.TagRepository.UpdateAsync(updateTagDTO);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_tag_by_id_returns_status_badRequest_value_negative()
        {
            var updateTagDTO = new TagDTO(3, "H", -5);

            var actual = await _v.TagRepository.UpdateAsync(updateTagDTO);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
