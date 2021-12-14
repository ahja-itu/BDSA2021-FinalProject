using WebService.Infrastructure.Tests.SearchAlgorithmTests;
//using ExtensionMethods;
namespace WebService.Infrastructure.Tests
{
    public class SearchAlgorithmTest
    {

        private SearchTestVariables _v;
        private MaterialRepository _materialRepository;
        private TagRepository _tagRepository;
        private SearchAlgorithm _searchAlgorithm;
        private LanguageRepository _languageRepository;
        private List<Material> _tag1Materials;
        private List<Material> _tag2Materials;
        private List<Material> _tag3Materials;
        private List<Material> _tag4Materials;
        private List<Material> _tag6Materials;

        private List<Material> _tag5Materials;
        private List<Material> _tag7Materials;

        public SearchAlgorithmTest()
        {
            _v = new SearchTestVariables();
            _materialRepository = new MaterialRepository(_v.Context);
            _tagRepository = new TagRepository(_v.Context);
            _languageRepository = new LanguageRepository(_v.Context);
            _searchAlgorithm = new SearchAlgorithm(_materialRepository, _tagRepository);

            _tag1Materials = _v.Tag1Materials;
            _tag2Materials = _v.Tag2Materials;
            _tag3Materials = _v.Tag3Materials;
            _tag4Materials = _v.Tag4Materials;
            _tag5Materials = _v.Tag5Materials;
            _tag7Materials = _v.Tag7Materials;
            _tag4Materials = _v.Tag4Materials;
            _tag6Materials = _v.Tag6Materials;
        }


        #region Search
        [Fact]
        public void Search_given_nothing_returns_status_not_found()
        {
            //Arrange
            SearchForm searchform = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0)
            {
            };

            var expected = Status.NotFound;

            //Act
            var result = _searchAlgorithm.Search(searchform).Result;

            //Assert
            Assert.Equal(expected, result.Item1);
        }
        #endregion

        #region SearchFormParse

        #endregion


        #region TextFieldParse
        [Fact]
        public void TextFieldParse_given_SearchForm_with_text_tag_adds_texttag_to_empty_tags()
        {
            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search Tag1", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            SearchForm expected = new SearchForm("I am a text search Tag1", new List<TagDTO>() { new TagDTO(1, "Tag1") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            //Act
            SearchForm actual = _searchAlgorithm.AddTagsToSearchFromTextField(searchForm).Result;

            //Assert
            Assert.Equal(expected.TextField, actual.TextField);
            Assert.Equal(expected.Tags, actual.Tags);
            Assert.Equal(expected.ProgrammingLanguages, actual.ProgrammingLanguages);
            Assert.Equal(expected.Languages, actual.Languages);
            Assert.Equal(expected.Levels, actual.Levels);
            //Assert.Equal(expected, actual); //does not pass even though the above asserts does
        }

        [Fact]
        public void TextFieldParse_given_SearchForm_with_text_tag_adds_texttag_to_tags()
        {
            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search Tag1", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            SearchForm expected = new SearchForm("I am a text search Tag1", new List<TagDTO>() { new TagDTO(1, "Tag1"), new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            //Act
            SearchForm actual = _searchAlgorithm.AddTagsToSearchFromTextField(searchForm).Result;

            //Assert
            Assert.Equal(expected.TextField, actual.TextField);
            Assert.Equal(new HashSet<TagDTO>(expected.Tags), new HashSet<TagDTO>(actual.Tags));
            Assert.Equal(expected.ProgrammingLanguages, actual.ProgrammingLanguages);
            Assert.Equal(expected.Languages, actual.Languages);
            Assert.Equal(expected.Levels, actual.Levels);
            //Assert.Equal(expected, actual);

        }
        #endregion

        #region Tag1
        //tag1, varying weight
        [Fact]
        public void Search_given_SearchForm_returns_list_of_materials_prioritized_by_tag_weight()
        {
            //Arrange

            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { _tagRepository.ReadAsync(1).Result.Item2 }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);


            List<MaterialDTO> expected = new List<MaterialDTO>();
            for (int i = _tag1Materials.Count - 1; i > 0; i--) //check indices
            {
                expected.Add(_tag1Materials[i].ConvertToMaterialDTO());
            }

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;
            foreach (MaterialDTO material in actual)
            {
                foreach (CreateWeightedTagDTO materialTag in material.Tags)
                {
                    if (materialTag.Name != searchForm.Tags.ElementAt(0).Name) actual.Remove(material);
                }

            }
            Assert.Equal(_tag1Materials.Count, actual.Count);



            //Assert
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }



        }
        #endregion

        #region Tag2

        //tag2, varying rating

        [Fact]
        public void Search_given_SearchForm_with_rating_1_returns_list_of_material_with_rating_1_or_higher()
        {

            //Arrange
            int rating = 1;
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);

            List<MaterialDTO> expectedDTO = new List<MaterialDTO>(){
                _tag2Materials.ElementAt(10).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(9).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(8).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(7).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(6).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(5).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(4).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(3).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(2).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(1).ConvertToMaterialDTO(),


                };
            //Act

            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert




            Assert.Equal(expectedDTO.Count, actual.Count);
            for (int i = 0; i < expectedDTO.Count; i++)
            {
                Assert.Equal(expectedDTO[i].Title, actual.ElementAt(i).Title);
            }

        }

        [Fact]
        public void Search_given_SearchForm_with_rating_3_returns_list_of_material_with_rating_3_or_higher()
        {
            //Arrange
            int rating = 3;
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>(){
                     _tag2Materials.ElementAt(10).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(9).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(8).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(7).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(6).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(5).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(4).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(3).ConvertToMaterialDTO(),

                };
            //Act

            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert


            Assert.Equal(expectedDTO.Count, actual.Count);
            for (int i = 0; i < expectedDTO.Count; i++)
            {
                Assert.Equal(expectedDTO[i].Title, actual.ElementAt(i).Title);
            }

        }

        [Fact]
        public void Search_given_SearchForm_with_rating_5_returns_list_of_material_with_rating_5_or_higher()
        {
            //Arrange
            int rating = 5;
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>(){
                 _tag2Materials.ElementAt(10).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(9).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(8).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(7).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(6).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(5).ConvertToMaterialDTO(),

                };
            //Act

            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert

            Assert.Equal(expectedDTO.Count, actual.Count);
            for (int i = 0; i < expectedDTO.Count; i++)
            {
                Assert.Equal(expectedDTO[i].Title, actual.ElementAt(i).Title);
            }

        }

        [Fact]
        public void Search_given_SearchForm_with_rating_7_returns_list_of_material_with_rating_7_or_higher()
        {
            //Arrange
            int rating = 7;
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>(){
                      _tag2Materials.ElementAt(10).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(9).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(8).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(7).ConvertToMaterialDTO(),

                };
            //Act

            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert

            Assert.Equal(expectedDTO.Count, actual.Count);
            for (int i = 0; i < expectedDTO.Count; i++)
            {
                Assert.Equal(expectedDTO[i].Title, actual.ElementAt(i).Title);
            }

        }

        [Fact]
        public void Search_given_SearchForm_with_rating_9_returns_list_of_material_with_rating_9_or_higher()
        {
            //Arrange
            int rating = 9;
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>(){
                 _tag2Materials.ElementAt(10).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(9).ConvertToMaterialDTO(),

                };
            //Act

            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert

            Assert.Equal(expectedDTO.Count, actual.Count);
            for (int i = 0; i < expectedDTO.Count; i++)
            {
                Assert.Equal(expectedDTO[i].Title, actual.ElementAt(i).Title);
            }


        }
        #endregion

        #region Tag3
        //tag3, levels


        [Fact]
        public void Search_given_SearchForm_containing_bachelor_returns_list_of_material_prioritized_by_level()
        {

            //Arrange
            var searchLevels = new List<LevelDTO> { new LevelDTO(1, "Bachelor") };
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(3, "Tag3") }, searchLevels, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var expected = new List<MaterialDTO>()
            {
                 _tag3Materials.ElementAt(0).ConvertToMaterialDTO(),
                 _tag3Materials.ElementAt(3).ConvertToMaterialDTO(),
                 _tag3Materials.ElementAt(4).ConvertToMaterialDTO(),
                 _tag3Materials.ElementAt(6).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }


        }

        [Fact]
        public void Search_given_SearchForm_containing_bachelor_master_returns_list_of_material_prioritized_by_level()
        {

            //Arrange
            var searchLevels = new List<LevelDTO> { new LevelDTO(1, "bachelor"), new LevelDTO(2, "master") };
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(3, "Tag3") }, searchLevels, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var expected = new List<MaterialDTO>()
            {
                 _tag3Materials.ElementAt(3).ConvertToMaterialDTO(),
                  _tag3Materials.ElementAt(6).ConvertToMaterialDTO(),
                 _tag3Materials.ElementAt(0).ConvertToMaterialDTO(),
                 _tag3Materials.ElementAt(1).ConvertToMaterialDTO(),
                  _tag3Materials.ElementAt(4).ConvertToMaterialDTO(),
                 _tag3Materials.ElementAt(5).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }


        }


        [Fact]
        public void Search_given_SearchForm_containing_bachelor_master_phd_returns_list_of_material_prioritized_by_level()
        {

            //Arrange
            var searchLevels = new List<LevelDTO> { new LevelDTO(1, "bachelor"), new LevelDTO(2, "master"), new LevelDTO(3,"phd") };
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(3, "Tag3") }, searchLevels, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var expected = new List<MaterialDTO>()
            {
                 _tag3Materials.ElementAt(6).ConvertToMaterialDTO(),
                  _tag3Materials.ElementAt(3).ConvertToMaterialDTO(),
                 _tag3Materials.ElementAt(4).ConvertToMaterialDTO(),
                 _tag3Materials.ElementAt(5).ConvertToMaterialDTO(),
                  _tag3Materials.ElementAt(0).ConvertToMaterialDTO(),
                 _tag3Materials.ElementAt(1).ConvertToMaterialDTO(),
                  _tag3Materials.ElementAt(2).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }
        #endregion

  
        #region Tag4
        //tag4 - varying Language

    [Fact]
       
        public void Search_given_SearchForm_containing_language_returns_list_of_material_only_with_given_language()
    {

            //Arrange
            var searchLanguage = new List<LanguageDTO>() { new LanguageDTO(1, "Danish") };
            SearchForm searchForm = new SearchForm("bla bla bla", new List<TagDTO>() { new TagDTO(4, "Tag4")}, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), searchLanguage, new List<MediaDTO>(), 0);
            var expected = new List<MaterialDTO>()
            {
                 _tag4Materials.ElementAt(0).ConvertToMaterialDTO(),
            };

            Assert.Equal(null, _searchAlgorithm.Search(searchForm).Result.Item2);

            Assert.Equal(null, _materialRepository.ReadAsync(searchForm).Result.Item2);

            //Act
           
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }

        }
        #endregion


        #region Tag5


        [Fact]
        public void Search_given_SearchForm_containing_one_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage()
        {

            //Arrange
            var searchProgrammingLanguages = new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(1, "C#") };
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(5, "Tag5") }, new List<LevelDTO>() { }, searchProgrammingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            var expected = new List<MaterialDTO>()
            {
                 _tag5Materials.ElementAt(0).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(3).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(4).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(6).ConvertToMaterialDTO(),
            };

            //Act

            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }


            [Fact]
            public void Search_given_SearchForm_containing_two_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage()
            {

                //Arrange
                var searchProgrammingLanguages = new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(1, "C#"), new ProgrammingLanguageDTO(3, "F#") };
                SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(5, "Tag5") }, new List<LevelDTO>() { }, searchProgrammingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);
                var expected = new List<MaterialDTO>()
            {
                 _tag5Materials.ElementAt(3).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(6).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(0).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(1).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(4).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(5).ConvertToMaterialDTO(),
            };

                //Act

                var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

                //Assert
                Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < actual.Count - 1; i++)
                {
                    Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
                }

            }


        [Fact]
        public void Search_given_SearchForm_containing_all_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage()
        {

            //Arrange
            var searchProgrammingLanguages = new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(1, "C#"), new ProgrammingLanguageDTO(3, "F#"), new ProgrammingLanguageDTO(2, "JAVA") };
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(5, "Tag5") }, new List<LevelDTO>() { }, searchProgrammingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            var expected = new List<MaterialDTO>()
            {
                 _tag5Materials.ElementAt(6).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(3).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(4).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(5).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(0).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(1).ConvertToMaterialDTO(),
                 _tag5Materials.ElementAt(2).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }

        }
        #endregion

        
        #region Tag6
        //tag6



    [Fact]
    public void Search_given_SearchForm_containing_media_returns_list_of_material_only_with_given_media()
    {

            //Arrange
            var searchMedia = new List<MediaDTO>() { new MediaDTO(1, "Book") };
        SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(6, "Tag6")}, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), searchMedia, 0);
            List<MaterialDTO> expected = new List<MaterialDTO>() { 
                _tag6Materials.ElementAt(0).ConvertToMaterialDTO(),
                _tag6Materials.ElementAt(1).ConvertToMaterialDTO(),
                _tag6Materials.ElementAt(2).ConvertToMaterialDTO(),
            };
      
        //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }

        }
        #endregion

        /*
        #region Tag7
    //tag7, title
        public static IEnumerable<object[]> Search_given_SearchForm_containing_title_returns_list_of_material_prioritized_by_title_data()
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
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;


        //Assert
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected.ElementAt(i).ConvertToMaterialDTO(), actual.ElementAt(i));
        }
    }
        #endregion
        */






    }
}
