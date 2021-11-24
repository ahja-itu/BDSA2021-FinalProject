namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class MediaRepositoryTests
    {
        private readonly TestVariables _v;

        public MediaRepositoryTests()
        {
            _v = new TestVariables();
        }
        #region Create
        [Fact]
        public async Task CreateAsync_media_returns_new_media_with_id()
        {
            var media = new CreateMediaDTO("Article");

            var actual = await _v.MediaRepository.CreateAsync(media);

            var expected = (Status.Created, new MediaDTO(4, "Article"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_media_returns_conflict_and_existing_media()
        {
            var media = new CreateMediaDTO("Video");

            var actual = await _v.MediaRepository.CreateAsync(media);

            var expected = (Status.Conflict, new MediaDTO(3, "Video"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_media_returns_count_one_more()
        {
            var media = new CreateMediaDTO("Article");

            await _v.MediaRepository.CreateAsync(media);

            var actual = _v.MediaRepository.ReadAsync().Result.Count;

            var expected = 4;

            Assert.Equal(expected, actual);
        }
        #endregion

        #region Read

        [Fact]
        public async Task ReadAsync_media_by_id_returns_media_and_status_found()
        {
            var actual = await _v.MediaRepository.ReadAsync(1);

            var expected = (Status.Found, new MediaDTO(1, "Book"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_media_by_id_returns_empty_media_and_status_notFound()
        {
            var actual = await _v.MediaRepository.ReadAsync(4);

            var expected = (Status.NotFound, new MediaDTO(-1, ""));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_tags()
        {
            var actuals = await _v.MediaRepository.ReadAsync();

            var expected1 = new MediaDTO(1, "Book");
            var expected2 = new MediaDTO(2, "Report");
            var expected3 = new MediaDTO(3, "Video");

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteAsync_media_by_id_returns_status_deleted()
        {
            var actual = await _v.MediaRepository.DeleteAsync(1);

            var expected = Status.Deleted;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_media_by_id_returns_status_notFound()
        {
            var actual = await _v.MediaRepository.DeleteAsync(4);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_media_by_id_returns_count_one_less()
        {
            await _v.MediaRepository.DeleteAsync(3);

            var actual = _v.MediaRepository.ReadAsync().Result.Count;

            var expected = 2;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Update
        [Fact]
        public async Task UpdateAsync_media_by_id_returns_status_updated()
        {
            var updateMediaDTO = new MediaDTO(3, "Article");

            var actual = await _v.MediaRepository.UpdateAsync(updateMediaDTO);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_media_by_id_read_updated_returns_status_found_and_updated_media()
        {
            var updateMediaDTO = new MediaDTO(3, "Article");

            await _v.MediaRepository.UpdateAsync(updateMediaDTO);

            var actual = await _v.MediaRepository.ReadAsync(3);

            var expected = (Status.Found, updateMediaDTO);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_media_by_id_returns_status_notFound()
        {
            var updateMediaDTO = new MediaDTO(4, "Article");

            var actual = await _v.MediaRepository.UpdateAsync(updateMediaDTO);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_media_by_id_returns_status_conflict()
        {
            var updateMediaDTO = new MediaDTO(3, "Book");

            var actual = await _v.MediaRepository.UpdateAsync(updateMediaDTO);

            var expected = Status.Conflict;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
