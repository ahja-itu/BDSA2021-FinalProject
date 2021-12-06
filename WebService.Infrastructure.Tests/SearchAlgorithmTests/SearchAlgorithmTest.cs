using WebService.Infrastructure.Tests.SearchAlgorithmTests;

namespace WebService.Infrastructure.Tests
{
    public class SearchAlgorithmTest
    {

        private SearchTestVariables _v;   
        private MaterialRepository _materialRepository;
        private List<Material> _tag1Materials;
        private List<Material> _tag2Materials;

        public SearchAlgorithmTest()
        {
            _v = new SearchTestVariables();
            _materialRepository = new MaterialRepository(_v.Context);

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
        public void Search_given_SearchForm_returns_list_of_materials_prioritized_by_tag_weight(){
            //Arrange
            SearchAlgorithm s = new SearchAlgorithm();   
            SearchForm searhForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(1, "Tag1") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(),0);


            //Act
            var actual = s.Search(searhForm);

            //Assert

            Assert.True(MaterialRepository.ConvertMaterialToMaterialDTOHashSet(_v.Tag1Materials.ElementAt(1))==actual.ElementAt(1));
        }


        //tag2, varying rating
        public static IEnumerable<object[]> Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_tag_weight_data(IEnumerable<Material> _tag2Materials)
        {
            yield return new object[] { 1, new List<Material>
            {
                _tag2Materials.ElementAt(9),
                _tag2Materials.ElementAt(8),
                _tag2Materials.ElementAt(7),
                _tag2Materials.ElementAt(6),
                _tag2Materials.ElementAt(5),
                _tag2Materials.ElementAt(4),
                _tag2Materials.ElementAt(3),
                _tag2Materials.ElementAt(2),
                _tag2Materials.ElementAt(1),
                _tag2Materials.ElementAt(0)
            }
            };
            yield return new object[] { 3, new List<Material>
            {
                _tag2Materials.ElementAt(9),
                _tag2Materials.ElementAt(8),
                _tag2Materials.ElementAt(7),
                _tag2Materials.ElementAt(6),
                _tag2Materials.ElementAt(5),
                _tag2Materials.ElementAt(4),
                _tag2Materials.ElementAt(3),
                _tag2Materials.ElementAt(2)
            }};
            yield return new object[] { 5, new List<Material> { _tag2Materials.ElementAt(9), _tag2Materials.ElementAt(8), _tag2Materials.ElementAt(7), _tag2Materials.ElementAt(6), _tag2Materials.ElementAt(5), _tag2Materials.ElementAt(4) } };
            yield return new object[] { 7, new List<Material> { _tag2Materials.ElementAt(9), _tag2Materials.ElementAt(8), _tag2Materials.ElementAt(7), _tag2Materials.ElementAt(6) } };
            yield return new object[] { 10, new List<Material> { _tag2Materials.ElementAt(9) } };
        }

 
        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_tag_weight_data))]
        public void Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_rating(int rating, List<Material> expected) {

            //Arrange
            SearchAlgorithm algo = new SearchAlgorithm();
            SearchForm searchForm = new SearchForm("I am a text search tag1", new List<TagDTO>() { new TagDTO(1, "Tag1") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>();
            foreach(Material m in expected)
            {
                MaterialDTO temp = MaterialRepository.ConvertMaterialToMaterialDTOHashSet(m);
                expectedDTO.Add(temp);
            }

            //Act
            //search and return list of materialDTO
            var result = algo.Search(searchForm);

            //Assert
            //Compare with DTO converted to Material or vice-versa
            Assert.Equal<MaterialDTO>(expectedDTO, result);
        }

        //tag3




        //tag4



        //tag5




        //tag6




    }
}
