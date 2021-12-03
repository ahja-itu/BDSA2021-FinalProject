using WebService.Infrastructure.Tests.SearchAlgorithmTests;

namespace WebService.Infrastructure.Tests
{
    public class SearchAlgorithmTest
    {

        private SearchTestVariables _v;
        
        private List<Material> _tag1Materials;
        private List<Material> _tag2Materials;

        public SearchAlgorithmTest()
        {
            _v = new SearchTestVariables();

            _tag1Materials = _v.Tag1Materials;
            _tag2Materials = _v.Tag2Materials;

        }

        [Fact]
        public void Search_given_nothing_returns_empty_list()
        {
            //Arrange
            SearchForm searchform = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0)
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
            Assert.Equal<List<MaterialDTO>>(new List<MaterialDTO>() { }, result);
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
            yield return new object[]{  new SearchForm("I am a text search tag1", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0),
            new SearchForm("I am a text search tag1", new List<TagDTO>(){ new TagDTO(1,"Tag1" ) }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0)};
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

     

        //tag2, varying rating
        public static IEnumerable<object[]> Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_tag_weight_data(IEnumerable<Material> _tag2Materials) 
        {
            yield return new object[] { 5, new List<Material> { _tag2Materials.ElementAt(1), _tag2Materials.ElementAt(2) } };
            yield return new object[] { 10, new List<Material> { _tag2Materials.ElementAt(1), _tag2Materials.ElementAt(2) } };
        }

 
        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_tag_weight_data))]
        public void Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_rating(int rating, List<Material> expected) {

            //Arrange
            SearchForm searhForm = new SearchForm("I am a text search tag1", new List<TagDTO>() { new TagDTO(1, "Tag1") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);

            //Act
            //search and return list of materialDTO


            //Assert
            //COmpare with DTO converted to Material or vice-versa
        
        }

        //tag3




        //tag4



        //tag5




        //tag6




    }
}
