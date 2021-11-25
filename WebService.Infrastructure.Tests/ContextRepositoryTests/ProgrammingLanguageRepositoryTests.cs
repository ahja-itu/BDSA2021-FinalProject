namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class ProgrammingProgrammingLanguageRepositoryTests
    {
        private readonly TestVariables _v;

        public ProgrammingProgrammingLanguageRepositoryTests()
        {
            _v = new TestVariables();
        }
        #region Create
        [Fact]
        public async Task CreateAsync_programmingLanguage_returns_new_programmingLanguage_with_id()
        {
            var programmingLanguage = new CreateProgrammingLanguageDTO("Java");

            var actual = await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

            var expected = (Status.Created, new ProgrammingLanguageDTO(4, "Java"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_programmingLanguage_returns_conflict_and_existing_programmingLanguage()
        {
            var programmingLanguage = new CreateProgrammingLanguageDTO("F#");

            var actual = await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

            var expected = (Status.Conflict, new ProgrammingLanguageDTO(3, "F#"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_programmingLanguage_returns_count_one_more()
        {
            var programmingLanguage = new CreateProgrammingLanguageDTO("Java");

            await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

            var actual = _v.ProgrammingLanguageRepository.ReadAsync().Result.Count;

            var expected = 4;

            Assert.Equal(expected, actual);
        }
        #endregion

        #region Read

        [Fact]
        public async Task ReadAsync_programmingLanguage_by_id_returns_programmingLanguage_and_status_found()
        {
            var actual = await _v.ProgrammingLanguageRepository.ReadAsync(1);

            var expected = (Status.Found, new ProgrammingLanguageDTO(1, "C#"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_programmingLanguage_by_id_returns_empty_programmingLanguage_and_status_notFound()
        {
            var actual = await _v.ProgrammingLanguageRepository.ReadAsync(4);

            var expected = (Status.NotFound, new ProgrammingLanguageDTO(-1, ""));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_programmingLanguages()
        {
            var actuals = await _v.ProgrammingLanguageRepository.ReadAsync();

            var expected1 = new ProgrammingLanguageDTO(1, "C#");
            var expected2 = new ProgrammingLanguageDTO(2, "C++");
            var expected3 = new ProgrammingLanguageDTO(3, "F#");

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteAsync_programmingLanguage_by_id_returns_status_deleted()
        {
            var actual = await _v.ProgrammingLanguageRepository.DeleteAsync(1);

            var expected = Status.Deleted;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_programmingLanguage_by_id_returns_status_notFound()
        {
            var actual = await _v.ProgrammingLanguageRepository.DeleteAsync(4);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_programmingLanguage_by_id_returns_count_one_less()
        {
            await _v.ProgrammingLanguageRepository.DeleteAsync(3);

            var actual = _v.ProgrammingLanguageRepository.ReadAsync().Result.Count;

            var expected = 2;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Update
        [Fact]
        public async Task UpdateAsync_programmingLanguage_by_id_returns_status_updated()
        {
            var updateProgrammingLanguageDTO = new ProgrammingLanguageDTO(3, "Java");

            var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(updateProgrammingLanguageDTO);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_programmingLanguage_by_id_read_updated_returns_status_found_and_updated_programmingLanguage()
        {
            var updateProgrammingLanguageDTO = new ProgrammingLanguageDTO(3, "Java");

            await _v.ProgrammingLanguageRepository.UpdateAsync(updateProgrammingLanguageDTO);

            var actual = await _v.ProgrammingLanguageRepository.ReadAsync(3);

            var expected = (Status.Found, updateProgrammingLanguageDTO);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_programmingLanguage_by_id_returns_status_notFound()
        {
            var updateProgrammingLanguageDTO = new ProgrammingLanguageDTO(4, "Java");

            var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(updateProgrammingLanguageDTO);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_programmingLanguage_by_id_returns_status_conflict()
        {
            var updateProgrammingLanguageDTO = new ProgrammingLanguageDTO(3, "C#");

            var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(updateProgrammingLanguageDTO);

            var expected = Status.Conflict;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
