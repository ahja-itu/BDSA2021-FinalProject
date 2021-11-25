namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class LanguageRepositoryTests
    {
        private readonly TestVariables _v;

        public LanguageRepositoryTests()
        {
            _v = new TestVariables();
        }
        #region Create
        [Fact]
        public async Task CreateAsync_language_returns_new_language_with_id()
        {
            var language = new CreateLanguageDTO("Deutsch");

            var actual = await _v.LanguageRepository.CreateAsync(language);

            var expected = (Status.Created, new LanguageDTO(4, "Deutsch"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_language_returns_conflict_and_existing_language()
        {
            var language = new CreateLanguageDTO("Swedish");

            var actual = await _v.LanguageRepository.CreateAsync(language);

            var expected = (Status.Conflict, new LanguageDTO(3, "Swedish"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_language_returns_count_one_more()
        {
            var language = new CreateLanguageDTO("Deutsch");

            await _v.LanguageRepository.CreateAsync(language);

            var actual = _v.LanguageRepository.ReadAsync().Result.Count;

            var expected = 4;

            Assert.Equal(expected, actual);
        }
        #endregion

        #region Read

        [Fact]
        public async Task ReadAsync_language_by_id_returns_language_and_status_found()
        {
            var actual = await _v.LanguageRepository.ReadAsync(1);

            var expected = (Status.Found, new LanguageDTO(1, "Danish"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_language_by_id_returns_empty_language_and_status_notFound()
        {
            var actual = await _v.LanguageRepository.ReadAsync(4);

            var expected = (Status.NotFound, new LanguageDTO(-1, ""));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_languages()
        {
            var actuals = await _v.LanguageRepository.ReadAsync();

            var expected1 = new LanguageDTO(1, "Danish");
            var expected2 = new LanguageDTO(2, "English");
            var expected3 = new LanguageDTO(3, "Swedish");

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteAsync_language_by_id_returns_status_deleted()
        {
            var actual = await _v.LanguageRepository.DeleteAsync(1);

            var expected = Status.Deleted;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_language_by_id_returns_status_notFound()
        {
            var actual = await _v.LanguageRepository.DeleteAsync(4);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_language_by_id_returns_count_one_less()
        {
            await _v.LanguageRepository.DeleteAsync(3);

            var actual = _v.LanguageRepository.ReadAsync().Result.Count;

            var expected = 2;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Update
        [Fact]
        public async Task UpdateAsync_language_by_id_returns_status_updated()
        {
            var updateLanguageDTO = new LanguageDTO(3, "German");

            var actual = await _v.LanguageRepository.UpdateAsync(updateLanguageDTO);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_language_by_id_read_updated_returns_status_found_and_updated_language()
        {
            var updateLanguageDTO = new LanguageDTO(3, "German");

            await _v.LanguageRepository.UpdateAsync(updateLanguageDTO);

            var actual = await _v.LanguageRepository.ReadAsync(3);

            var expected = (Status.Found, updateLanguageDTO);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_language_by_id_returns_status_notFound()
        {
            var updateLanguageDTO = new LanguageDTO(4, "German");

            var actual = await _v.LanguageRepository.UpdateAsync(updateLanguageDTO);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_language_by_id_returns_status_conflict()
        {
            var updateLanguageDTO = new LanguageDTO(3, "Danish");

            var actual = await _v.LanguageRepository.UpdateAsync(updateLanguageDTO);

            var expected = Status.Conflict;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
