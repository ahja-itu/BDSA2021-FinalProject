namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class AuthorRepositoryTest
    {
        private readonly TestVariables _v;

        public AuthorRepositoryTest()
        {
            _v = new TestVariables();
        }
        #region Create
        [Fact]
        public async Task CreateAsync_author_returns_new_author_with_id()
        {
            var author = new CreateAuthorDTO("Andreas", "Wachs");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.Created, new AuthorDTO(4, "Andreas", "Wachs"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_conflict_and_existing_author()
        {
            var author = new CreateAuthorDTO("Thor", "Lind");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.Conflict, new AuthorDTO(3, "Thor", "Lind"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_count_one_more()
        {
            var author = new CreateAuthorDTO("Andreas", "Wachs");

            await _v.AuthorRepository.CreateAsync(author);

            var actual = _v.AuthorRepository.ReadAsync().Result.Count;

            var expected = 4;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_bad_request_on_firstName_tooLong()
        {
            var author = new CreateAuthorDTO("asseocarnisanguineoviscericartilaginonervomedullary", "Wachs");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.BadRequest, new AuthorDTO(-1, "asseocarnisanguineoviscericartilaginonervomedullary", "Wachs"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_bad_request_on_surName_tooLong()
        {
            var author = new CreateAuthorDTO("Andreas", "asseocarnisanguineoviscericartilaginonervomedullary");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.BadRequest, new AuthorDTO(-1, "Andreas", "asseocarnisanguineoviscericartilaginonervomedullary"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_bad_request_on_names_tooLong()
        {
            var author = new CreateAuthorDTO("asseocarnisanguineoviscericartilaginonervomedullary", "asseocarnisanguineoviscericartilaginonervomedullary");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.BadRequest, new AuthorDTO(-1, "asseocarnisanguineoviscericartilaginonervomedullary", "asseocarnisanguineoviscericartilaginonervomedullary"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_bad_request_on_firstName_empty()
        {
            var author = new CreateAuthorDTO("", "Wachs");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.BadRequest, new AuthorDTO(-1, "", "Wachs"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_bad_request_on_surName_empty()
        {
            var author = new CreateAuthorDTO("Andreas", "");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.BadRequest, new AuthorDTO(-1, "Andreas", ""));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_bad_request_on_names_empty()
        {
            var author = new CreateAuthorDTO("", "");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.BadRequest, new AuthorDTO(-1, "", ""));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_bad_request_on_firstName_whitespace()
        {
            var author = new CreateAuthorDTO(" ", "Wachs");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.BadRequest, new AuthorDTO(-1, " ", "Wachs"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_bad_request_on_surName_whitespace()
        {
            var author = new CreateAuthorDTO("Andreas", " ");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.BadRequest, new AuthorDTO(-1, "Andreas", " "));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_returns_bad_request_on_names_whitespace()
        {
            var author = new CreateAuthorDTO(" ", " ");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.BadRequest, new AuthorDTO(-1, " ", " "));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_with_max_length_firstName_returns_new_author_with_id()
        {
            var author = new CreateAuthorDTO("asseocarnisanguineoviscericartilaginonervomedullar", "Wachs");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.Created, new AuthorDTO(4, "asseocarnisanguineoviscericartilaginonervomedullar", "Wachs"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_with_max_length_surName_returns_new_author_with_id()
        {
            var author = new CreateAuthorDTO("Andreas", "asseocarnisanguineoviscericartilaginonervomedullar");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.Created, new AuthorDTO(4, "Andreas", "asseocarnisanguineoviscericartilaginonervomedullar"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_author_with_max_length_both_names_returns_new_author_with_id()
        {
            var author = new CreateAuthorDTO("asseocarnisanguineoviscericartilaginonervomedullar", "asseocarnisanguineoviscericartilaginonervomedullar");

            var actual = await _v.AuthorRepository.CreateAsync(author);

            var expected = (Status.Created, new AuthorDTO(4, "asseocarnisanguineoviscericartilaginonervomedullar", "asseocarnisanguineoviscericartilaginonervomedullar"));

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Read

        [Fact]
        public async Task ReadAsync_author_by_id_returns_author_and_status_found()
        {
            var actual = await _v.AuthorRepository.ReadAsync(1);

            var expected = (Status.Found, new AuthorDTO(1, "Rasmus", "Kristensen"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_author_by_id_returns_empty_author_and_status_notFound()
        {
            var actual = await _v.AuthorRepository.ReadAsync(4);

            var expected = (Status.NotFound, new AuthorDTO(-1, "", ""));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_authors()
        {
            var actuals = await _v.AuthorRepository.ReadAsync();

            var expected1 = new AuthorDTO(2, "Alex", "Su");
            var expected2 = new AuthorDTO(1, "Rasmus", "Kristensen");         
            var expected3 = new AuthorDTO(3, "Thor", "Lind");

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteAsync_author_by_id_returns_status_deleted()
        {
            var actual = await _v.AuthorRepository.DeleteAsync(1);

            var expected = Status.Deleted;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_author_by_id_returns_status_notFound()
        {
            var actual = await _v.AuthorRepository.DeleteAsync(4);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_author_by_id_returns_count_one_less()
        {
            await _v.AuthorRepository.DeleteAsync(3);

            var actual = _v.AuthorRepository.ReadAsync().Result.Count;

            var expected = 2;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Update
        [Fact]
        public async Task UpdateAsync_author_by_id_returns_status_updated()
        {
            var updateAuthorDTO = new AuthorDTO(3, "Andreas", "Wachs");

            var actual = await _v.AuthorRepository.UpdateAsync(updateAuthorDTO);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_by_id_read_updated_returns_status_found_and_updated_author()
        {
            var updateAuthorDTO = new AuthorDTO(3, "Andreas", "Wachs");

            await _v.AuthorRepository.UpdateAsync(updateAuthorDTO);

            var actual = await _v.AuthorRepository.ReadAsync(3);

            var expected = (Status.Found, updateAuthorDTO);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_by_id_returns_status_notFound()
        {
            var updateAuthorDTO = new AuthorDTO(4, "Andreas", "Wachs");

            var actual = await _v.AuthorRepository.UpdateAsync(updateAuthorDTO);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_by_id_returns_status_conflict()
        {
            var updateAuthorDTO = new AuthorDTO(3, "Rasmus", "Kristensen");

            var actual = await _v.AuthorRepository.UpdateAsync(updateAuthorDTO);

            var expected = Status.Conflict;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_returns_bad_request_on_firstName()
        {
            var author = new AuthorDTO(1,"asseocarnisanguineoviscericartilaginonervomedullary", "Wachs");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_returns_bad_request_on_surName()
        {
            var author = new AuthorDTO(1,"Andreas", "asseocarnisanguineoviscericartilaginonervomedullary");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_returns_bad_request_on_names()
        {
            var author = new AuthorDTO(1,"asseocarnisanguineoviscericartilaginonervomedullary", "asseocarnisanguineoviscericartilaginonervomedullary");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_returns_bad_request_on_firstName_empty()
        {
            var author = new AuthorDTO(1,"", "Wachs");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_returns_bad_request_on_surName_empty()
        {
            var author = new AuthorDTO(1,"Andreas", "");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_returns_bad_request_on_names_empty()
        {
            var author = new AuthorDTO(1,"", "");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_returns_bad_request_on_firstName_whitespace()
        {
            var author = new AuthorDTO(1," ", "Wachs");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_returns_bad_request_on_surName_whitespace()
        {
            var author = new AuthorDTO(1,"Andreas", " ");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_returns_bad_request_on_names_whitespace()
        {
            var author = new AuthorDTO(1," ", " ");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_with_max_length_firstName_returns_updated()
        {
            var author = new AuthorDTO(1,"asseocarnisanguineoviscericartilaginonervomedullar", "Wachs");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_with_max_length_surName_returns_updated()
        {
            var author = new AuthorDTO(1,"Andreas", "asseocarnisanguineoviscericartilaginonervomedullar");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_author_with_max_length_both_names_returns_updated()
        {
            var author = new AuthorDTO(1,"asseocarnisanguineoviscericartilaginonervomedullar", "asseocarnisanguineoviscericartilaginonervomedullar");

            var actual = await _v.AuthorRepository.UpdateAsync(author);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
