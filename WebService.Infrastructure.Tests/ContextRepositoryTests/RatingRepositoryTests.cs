namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class RatingRepositoryTests
    {
        private readonly TestVariables _v;
        public RatingRepositoryTests()
        {
            _v = new TestVariables();
        }
        #region Create
        [Fact]
        public async Task CreateAsync_rating_returns_new_rating_with_id()
        {
            var rating = new CreateRatingDTO(6);

            var actual = await _v.RatingRepository.CreateAsync(rating);

            var expected = (Status.Created, new RatingDTO(4, 6));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_rating_returns_conflict_and_existing_rating()
        {
            var rating = new CreateRatingDTO(9);

            var actual = await _v.RatingRepository.CreateAsync(rating);

            var expected = (Status.Conflict, new RatingDTO(3, 9));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_rating_returns_count_one_more()
        {
            var rating = new CreateRatingDTO(6);

            await _v.RatingRepository.CreateAsync(rating);

            var actual = _v.RatingRepository.ReadAsync().Result.Count;

            var expected = 4;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_rating_by_id_returns_status_badRequest_value_negative()
        {
            var rating = new CreateRatingDTO(-5);

            var actual = await _v.RatingRepository.CreateAsync(rating);

            var expected = (Status.BadRequest, new RatingDTO(-1, 0));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_rating_returns_bad_request_with_value_too_low()
        {
            var rating = new CreateRatingDTO(11);

            var actual = await _v.RatingRepository.CreateAsync(rating);

            var expected = (Status.BadRequest, new RatingDTO(-1, 0));

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task CreateAsync_rating_returns_bad_request_with_value_too_high()
        {
            var rating = new CreateRatingDTO(0);

            var actual = await _v.RatingRepository.CreateAsync(rating);

            var expected = (Status.BadRequest, new RatingDTO(-1, 0));

            Assert.Equal(expected, actual);
        }
        #endregion

        #region Read

        [Fact]
        public async Task ReadAsync_rating_by_id_returns_rating_and_status_found()
        {
            var actual = await _v.RatingRepository.ReadAsync(1);

            var expected = (Status.Found, new RatingDTO(1, 2));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_rating_by_id_returns_empty_rating_and_status_notFound()
        {
            var actual = await _v.RatingRepository.ReadAsync(4);

            var expected = (Status.NotFound, new RatingDTO(-1, 0));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_ratings()
        {
            var actuals = await _v.RatingRepository.ReadAsync();

            var expected1 = new RatingDTO(1, 2);
            var expected2 = new RatingDTO(2, 5);
            var expected3 = new RatingDTO(3, 9);

            Assert.Collection(actuals,
                actual => Assert.Equal(expected1, actual),
                actual => Assert.Equal(expected2, actual),
                actual => Assert.Equal(expected3, actual));
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteAsync_rating_by_id_returns_status_deleted()
        {
            var actual = await _v.RatingRepository.DeleteAsync(1);

            var expected = Status.Deleted;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_rating_by_id_returns_status_notFound()
        {
            var actual = await _v.RatingRepository.DeleteAsync(4);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_rating_by_id_returns_count_one_less()
        {
            await _v.RatingRepository.DeleteAsync(3);

            var actual = _v.RatingRepository.ReadAsync().Result.Count;

            var expected = 2;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Update
        [Fact]
        public async Task UpdateAsync_rating_by_id_returns_status_updated()
        {
            var updateRatingDTO = new RatingDTO(3, 6);

            var actual = await _v.RatingRepository.UpdateAsync(updateRatingDTO);

            var expected = Status.Updated;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_rating_by_id_read_updated_returns_status_found_and_updated_rating()
        {
            var updateRatingDTO = new RatingDTO(3, 6);

            await _v.RatingRepository.UpdateAsync(updateRatingDTO);

            var actual = await _v.RatingRepository.ReadAsync(3);

            var expected = (Status.Found, updateRatingDTO);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_rating_by_id_returns_status_notFound()
        {
            var updateRatingDTO = new RatingDTO(4, 6);

            var actual = await _v.RatingRepository.UpdateAsync(updateRatingDTO);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_rating_by_id_returns_status_conflict()
        {
            var updateRatingDTO = new RatingDTO(3, 2);

            var actual = await _v.RatingRepository.UpdateAsync(updateRatingDTO);

            var expected = Status.Conflict;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_rating_by_id_returns_status_badRequest_value_too_low()
        {
            var updateRatingDTO = new RatingDTO(3, 0);

            var actual = await _v.RatingRepository.UpdateAsync(updateRatingDTO);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_rating_by_id_returns_status_badRequest_value_too_high()
        {
            var updateRatingDTO = new RatingDTO(3, 11);

            var actual = await _v.RatingRepository.UpdateAsync(updateRatingDTO);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateAsync_rating_by_id_returns_status_badRequest_value_negative()
        {
            var updateRatingDTO = new RatingDTO(3, -5);

            var actual = await _v.RatingRepository.UpdateAsync(updateRatingDTO);

            var expected = Status.BadRequest;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
