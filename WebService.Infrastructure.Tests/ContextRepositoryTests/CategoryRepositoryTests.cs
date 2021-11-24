namespace WebService.Infrastructure.Tests.ContextRepositoryTests 
{
    public class CategoryRepositoryTests
    {
        private readonly TestVariables _v;

        public CategoryRepositoryTests()
        {
            _v = new TestVariables();
        }
        #region Create
        [Fact]
        public async Task CreateAsync_category_returns_new_category_with_id()
        {
            var category = new CreateCategoryDTO("UX");

            var actual = await _v.CategoryRepository.CreateAsync(category);

            var expected = (Status.Created, new CategoryDTO(4, "UX"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateAsync_category_returns_conflict_and_existing_category()
        {
            var category = new CreateCategoryDTO("UML");

            var actual = await _v.CategoryRepository.CreateAsync(category);

            var expected = (Status.Conflict, new CategoryDTO(3, "UML")); 

            Assert.Equal(expected, actual);
        }
        #endregion

        #region Read

        [Fact]
        public async Task ReadAsync_category_by_id_returns_category_and_status_found()
        {
            var actual = await _v.CategoryRepository.ReadAsync(1);

            var expected = (Status.Found, new CategoryDTO(1, "Algorithms"));

            Assert.Equal(expected,actual); 
        }

        [Fact]
        public async Task ReadAsync_category_by_id_returns_empty_category_and_status_notFound()
        {
            var actual = await _v.CategoryRepository.ReadAsync(4);

            var expected = (Status.NotFound, new CategoryDTO(-1,""));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_category_by_name_returns_category_and_status_found()
        {
            var actual = await _v.CategoryRepository.ReadAsyncByName("UML");

            var expected = (Status.Found, new CategoryDTO(3, "UML"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAsync_category_by_name_returns_empty_category_and_status_notFound()
        {
            var actual = await _v.CategoryRepository.ReadAsyncByName("Name");

            var expected = (Status.NotFound, new CategoryDTO(-1, ""));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReadAllAsync_returns_all_tags()
        {
            var actuals = await _v.CategoryRepository.ReadAsync();

            var expected1 = new CategoryDTO(1, "Algorithms");
            var expected2 = new CategoryDTO(2, "Database");
            var expected3 = new CategoryDTO(3, "UML");

            Assert.Collection(actuals, 
                actual => Assert.Equal(expected1,actual),
                actual => Assert.Equal(expected2,actual),
                actual => Assert.Equal(expected3,actual));
        }

        #endregion

        #region Delete

        [Fact]
        public async Task DeleteAsync_category_by_id_returns_status_deleted()
        {
            var actual = await _v.CategoryRepository.DeleteAsync(1);

            var expected = Status.Deleted;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_category_by_id_returns_status_notFound()
        {
            var actual = await _v.CategoryRepository.DeleteAsync(4);

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAsync_category_by_id_returns_count_one_less()
        {
            await _v.CategoryRepository.DeleteAsync(3);

            var actual = _v.CategoryRepository.ReadAsync().Result.Count;

            var expected = 2;

            Assert.Equal(expected, actual);
        }

        #endregion






    }
}
