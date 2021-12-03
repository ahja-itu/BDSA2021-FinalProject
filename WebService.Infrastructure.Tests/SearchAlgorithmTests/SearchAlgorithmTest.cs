using WebService.Infrastructure.Tests.SearchAlgorithmTests;

namespace WebService.Infrastructure.Tests
{
    public class SearchAlgorithmTest
    {

        private readonly SearchTestVariables _v;
            public SearchAlgorithmTest()
        {
            _v = new SearchTestVariables();
        }
        
        [Fact]
        public void Search_given_nothing_returns_empty_list()
        {
            //Arrange
             SearchForm searchform = new SearchForm()
            {
            };

            List<MaterialDTO> expected = new List<MaterialDTO>();
            
            SearchAlgorithm s = new SearchAlgorithm();

            //Act
            List<MaterialDTO> result = s.Search(searchform);

            //Assert
            Assert.Equal(expected, result);
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

        /*[Fact]
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
        }*/

        public static IEnumerable<object[]> TextFieldParse_given_SearchForm_parses_SearchForm_data()
        {
            yield return new object[]{  new SearchForm()  {TextField = "I am a text search tag1"  },
            new SearchForm()  {TextField = "I am a text search tag1", Tags = new HashSet<TagDTO>{new TagDTO(1, "Tag1")}}};

            yield return new object[]{  new SearchForm()  {TextField = "I am a text search TAG2"  },
            new SearchForm()  {TextField = "I am a text search TAG2", Tags = new HashSet<TagDTO>{new TagDTO(1, "Tag2")}}};

            yield return new object[]{  new SearchForm()  {TextField = "I am a text TAG3 search"  },
            new SearchForm()  {TextField = "I am a text TAG3 search", Tags = new HashSet<TagDTO>{new TagDTO(1, "Tag3")}}};

            yield return new object[]{  new SearchForm()  {TextField = "I am a text tag4 search tag5"  },
            new SearchForm()  {TextField = "I am a text tag4 search tag5", Tags = new HashSet<TagDTO>{new TagDTO(1, "Tag4"), new TagDTO(5, "tag5")}}};

            yield return new object[]{  new SearchForm()  {TextField = "I am a text search tag1", Tags = new HashSet<TagDTO>{new TagDTO(1, "Tag2")}},
            new SearchForm()  {TextField = "I am a text search tag1", Tags = new HashSet<TagDTO>{new TagDTO(1, "Tag2"), new TagDTO(1, "Tag1")}}};
        }

        [Theory]
        [MemberData(nameof(TextFieldParse_given_SearchForm_parses_SearchForm_data))]
        public void TextFieldParse_given_SearchForm_parses_SearchForm(SearchForm searchForm, SearchForm expected)
        {
            //Arrange
            SearchAlgorithm s = new SearchAlgorithm();

            //Act
            SearchForm result = s.TextFieldParse(searchForm);

            //Assert
            Assert.Equal(expected, result);
        }




    }
}
