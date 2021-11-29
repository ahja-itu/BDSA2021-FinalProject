using System.Reflection;

namespace WebService.Core.Server.Tests
{
    public class SearchAlgorithmTests
    {
        
        [Fact]
        public void Search_given_nothing_returns_empty_list()
        {
            //Arrange
           
             SearchForm searchform = new SearchForm()
            {
            };

            SearchAlgorithm s = new SearchAlgorithm();

            //Act
            List<MaterialDTO> result = s.Search(searchform);

            //Assert
            Assert.Equal<List<MaterialDTO>>(new List<MaterialDTO>(), result);
        }


        [Fact]
        public void Search_given_certain_inputs_returns_relevant_list_of_materialIDs()
        {
            //Arrange
             SearchForm searchform = new SearchForm()
            {
                //Write searchinput
            };

            SearchAlgorithm s = new SearchAlgorithm();

            //Act
            List<MaterialDTO> result = s.Search(searchform);

            //Assert
            Assert.Equal<List<MaterialDTO>>(new List<MaterialDTO>(){}, result);
        }

          [Fact]
        public void PrivateSearchFormParser_given_Searchform_returns_parsed_Searchform()
        {
            SearchForm expected = new SearchForm()
            {
                TextField = "Hat hat hat",
                Tags = new List<TagDTO> { new TagDTO(1, "tag1"), new TagDTO(2, "tag2") }
            };

            SearchForm searchForm = new SearchForm() //input for the test
            {
                TextField = "Hat hat hat tag2",
                Tags = new List<TagDTO> { new TagDTO(1, "tag1") }
            };

            Type type = typeof(SearchAlgorithm);
            var hello = Activator.CreateInstance(type, searchForm);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "SearchFormParser" && x.IsPrivate)
          .First();

            //act
            var actual = (SearchForm)method.Invoke(hello, new object[] { searchForm });

            //assert
           Assert.IsType<SearchForm>(actual);
        }


        public static IEnumerable<object[]> Search_given_nothing_returns_empty_list_data => new List<object[]>{
            new object[]{  new SearchForm()  {TextField = "I am a text search"  }}
        };

        [Theory]
        [MemberData(nameof(Search_given_nothing_returns_empty_list_data))]
        public void Search_given_certain_inputs_returns_relevant_list_of_materialIDs2(SearchForm searchForm)
        {
            //Arrange
            SearchAlgorithm s = new SearchAlgorithm();
            List<MaterialDTO> expected = new List<MaterialDTO>();

            //Act
            List<MaterialDTO> result = s.Search(searchForm);

            //Assert
            Assert.Equal<List<MaterialDTO>>(expected, result);
        }

    }
}
