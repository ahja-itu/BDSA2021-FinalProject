using WebService.Infrastructure.Tests.SearchAlgorithmTests;

namespace WebService.Infrastructure.Tests
{
    public class SearchAlgorithmTest
    {

        private SearchTestVariables _v;
        private MaterialRepository _materialRepository;
        private List<Material> _tag1Materials;
        private List<Material> _tag2Materials;
        private List<Material> _tag3Materials;

        private List<Material> _tag5Materials;
        private List<Material> _tag7Materials;

        public SearchAlgorithmTest()
        {
            _v = new SearchTestVariables();
            _materialRepository = new MaterialRepository(_v.Context);

            _tag1Materials = _v.Tag1Materials;
            _tag2Materials = _v.Tag2Materials;
            _tag3Materials = _v.Tag3Materials;
            _tag5Materials = _v.Tag5Materials;
            _tag7Materials = _v.Tag7Materials;


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

        //tag1, varying weight
        [Fact]
        public void Search_given_SearchForm_returns_list_of_materials_prioritized_by_tag_weight()
        {
            //Arrange
            SearchAlgorithm s = new SearchAlgorithm();
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(1, "Tag1") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            List<MaterialDTO> expected = new List<MaterialDTO>();
            for (int i = _v.Tag1Materials.Count; i > 0; i--)
            {
                expected.Add(MaterialRepository.ConvertMaterialToMaterialDTOHashSet(_v.Tag1Materials[i - 1]));
            }

            //Act
            var actual = s.Search(searchForm);

            //Assert
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }



        //tag2, varying rating
        public static IEnumerable<object[]> Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_tag_weight_data(IEnumerable<Material> _tag2Materials)
        {
            yield return new object[] { 5, new List<Material> { _tag2Materials.ElementAt(1), _tag2Materials.ElementAt(2) } };
            yield return new object[] { 10, new List<Material> { _tag2Materials.ElementAt(1), _tag2Materials.ElementAt(2) } };
        }


        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_tag_weight_data))]
        public void Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_rating(int rating, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search tag1", new List<TagDTO>() { new TagDTO(1, "Tag1") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);

            //Act
            //search and return list of materialDTO


            //Assert
            //COmpare with DTO converted to Material or vice-versa

        }

        //tag3, levels
        public static IEnumerable<object[]> Search_given_SearchForm_containing_level_returns_list_of_material_prioritized_by_level_data(IEnumerable<Material> _tag3Materials)
        {
            yield return new object[] { new List<LevelDTO>() { new LevelDTO(1, "Bachelor") }, new List<Material>() { _tag3Materials.ElementAt(0), _tag3Materials.ElementAt(3), _tag3Materials.ElementAt(4), _tag3Materials.ElementAt(6) } }; //all materials with level, what order?
            yield return new object[] { new List<LevelDTO>() { new LevelDTO(2, "Masters") }, new List<Material>() { _tag3Materials.ElementAt(1), _tag3Materials.ElementAt(2) } };
            yield return new object[] { new List<LevelDTO>() { new LevelDTO(3, "PhD") }, new List<Material>() { _tag3Materials.ElementAt(1), _tag3Materials.ElementAt(2) } };

        }

        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_level_returns_list_of_material_prioritized_by_level_data))]
        public void Search_given_SearchForm_containing_level_returns_list_of_material_prioritized_by_level(List<LevelDTO> levels, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(3, "Tag3") }, levels, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            SearchAlgorithm s = new SearchAlgorithm();
            //Act

            var actual = s.Search(searchForm);

            //Assert
            //COmpare with DTO converted to Material or vice-versa

        }


        //tag4



        //tag5, programmingLanguages
        public static IEnumerable<object[]> Search_given_SearchForm_containing_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage_data(IEnumerable<Material> _tag5Materials)
        {
            yield return new object[] { new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(1, "C#") }, new List<Material>() { _tag5Materials.ElementAt(0), _tag5Materials.ElementAt(3), _tag5Materials.ElementAt(4), _tag5Materials.ElementAt(6) } }; //all materials with ProLanguage, what order?

        }

        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage_data))]
        public void Search_given_SearchForm_containing_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage(List<ProgrammingLanguageDTO> programmingLanguages, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(5, "Tag5") }, new List<LevelDTO>() { }, programmingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            SearchAlgorithm s = new SearchAlgorithm();
            //Act
            var actual = s.Search(searchForm);


            //Assert
            //COmpare with DTO converted to Material or vice-versa

        }




        //tag6


        //tag7, title
        public static IEnumerable<object[]> Search_given_SearchForm_containing_title_returns_list_of_material_prioritized_by_title_data(IEnumerable<Material> _tag7Materials)
        {
            yield return new object[] { "Lorem", new List<Material>() { _tag7Materials.ElementAt(0), _tag7Materials.ElementAt(1), _tag7Materials.ElementAt(2), _tag7Materials.ElementAt(3), _tag7Materials.ElementAt(4) } }; //all materials searchword in title, what order?
            yield return new object[] { "lorem", new List<Material>() { _tag7Materials.ElementAt(0), _tag7Materials.ElementAt(1), _tag7Materials.ElementAt(2), _tag7Materials.ElementAt(3), _tag7Materials.ElementAt(4) } };
            yield return new object[] { "LOREM", new List<Material>() { _tag7Materials.ElementAt(0), _tag7Materials.ElementAt(1), _tag7Materials.ElementAt(2), _tag7Materials.ElementAt(3), _tag7Materials.ElementAt(4) } };
            yield return new object[] { "Lorem ipsum dolor sit amet", new List<Material>() { _tag7Materials.ElementAt(0), _tag7Materials.ElementAt(1), _tag7Materials.ElementAt(2), _tag7Materials.ElementAt(3), _tag7Materials.ElementAt(4) } }; //Prioritised by number of matches with words in title

        }

        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_title_returns_list_of_material_prioritized_by_title_data))]
        public void Search_given_SearchForm_containing_title_returns_list_of_material_prioritized_by_title(string title, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(7, "Tag7") }, new List<LevelDTO>() { }, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            SearchAlgorithm s = new SearchAlgorithm();
            //Act
            var actual = s.Search(searchForm);


            //Assert
            for(int i = 0; i<expected.Count;i++){
                Assert.Equal(MaterialRepository.ConvertMaterialToMaterialDTOHashSet(expected.ElementAt(i)), actual.ElementAt(i));
            }
        }



    }
}
