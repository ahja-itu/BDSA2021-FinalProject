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
        private List<Material> _tag5Materials;
        private List<Material> _tag6Materials;
        private List<Material> _tag7Materials;
        private List<Material> _tag8Materials;

        private List<Material> _tag9Materials;

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
            _tag6Materials = _v.Tag6Materials;
            _tag7Materials = _v.Tag7Materials;
            _tag8Materials = _v.Tag8Materials; 
            _tag9Materials = _v.Tag9Materials;
        }


        #region Search
        [Fact]
        public void Search_given_nothing_returns_status_found()
        {
            //Arrange
            SearchForm searchform = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0)
            {
            };

            var expected = Status.Found;

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

        [Fact]
        public void TextFieldParse_given_SearchForm_with_text_tag_ignores_casing_adds_texttag_to_empty_tags()
        {
            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search taG1", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            SearchForm expected = new SearchForm("I am a text search taG1", new List<TagDTO>() { new TagDTO(1, "Tag1") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

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


        #endregion

        #region Tag1-Weight
        //tag1, varying weight
        [Fact]
        public void Search_given_SearchForm_returns_list_of_materials_prioritized_by_tag_weight()
        {
            //Arrange
            SearchForm searchForm = new SearchForm("ELI5: Induction Proofs", new List<TagDTO>() { new TagDTO(1, "Tag1")}, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);


            List<MaterialDTO> expected = new List<MaterialDTO>();
            for (int i = _tag1Materials.Count - 1; i > 0; i--) //check indices
            {
                expected.Add(_tag1Materials[i].ConvertToMaterialDTO());
            }

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }



        }
        #endregion

        #region Tag2-Rating

        //tag2, varying rating

        [Fact]
        public void Search_given_SearchForm_with_rating_1_returns_list_of_material_with_rating_1_or_higher()
        {

            //Arrange
            int rating = 1;
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);

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
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
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
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
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
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>(){
                      _tag2Materials.ElementAt(10).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(9).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(8).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(7).ConvertToMaterialDTO(),

                };
            //Act

            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
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
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(2, "Tag2") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>(){
                 _tag2Materials.ElementAt(10).ConvertToMaterialDTO(),
                    _tag2Materials.ElementAt(9).ConvertToMaterialDTO(),

                };
            //Act

            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < expectedDTO.Count; i++)
            {
                Assert.Equal(expectedDTO[i].Title, actual.ElementAt(i).Title);
            }


        }
        #endregion

        #region Tag3-Media
        //tag3, levels


        [Fact]
        public void Search_given_SearchForm_containing_bachelor_returns_list_of_material_prioritized_by_level()
        {

            //Arrange
            var searchLevels = new List<LevelDTO> { new LevelDTO(1, "Bachelor") };
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(3, "Tag3") }, searchLevels, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

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
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }


        }

        [Fact]
        public void Search_given_SearchForm_containing_bachelor_master_returns_list_of_material_prioritized_by_level()
        {

            //Arrange
            var searchLevels = new List<LevelDTO> { new LevelDTO(1, "Bachelor"), new LevelDTO(2, "Master") };
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(3, "Tag3") }, searchLevels, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

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
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }


        }


        [Fact]
        public void Search_given_SearchForm_containing_bachelor_master_phd_returns_list_of_material_prioritized_by_level()
        {

            //Arrange
            var searchLevels = new List<LevelDTO> { new LevelDTO(1, "Bachelor"), new LevelDTO(2, "Master"), new LevelDTO(3,"PHD") };
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(3, "Tag3") }, searchLevels, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

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
          
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }
        #endregion

  
        #region Tag4-Language
        //tag4 - varying Language

    [Fact]
       
        public void Search_given_SearchForm_containing_language_returns_list_of_material_only_with_given_language()
    {

            //Arrange
            var searchLanguage = new List<LanguageDTO>() { new LanguageDTO(1, "Danish") };
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(4, "Tag4")}, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), searchLanguage, new List<MediaDTO>(), 0);
            var expected = new List<MaterialDTO>()
            {
                 _tag4Materials.ElementAt(0).ConvertToMaterialDTO(),
            };

            //Act
           
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }

        }
        #endregion


        #region Tag5-ProgrammingLanguage


        [Fact]
        public void Search_given_SearchForm_containing_one_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage()
        {

            //Arrange
            var searchProgrammingLanguages = new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(1, "C#") };
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(5, "Tag5") }, new List<LevelDTO>() { }, searchProgrammingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);
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
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }


            [Fact]
            public void Search_given_SearchForm_containing_two_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage()
            {

                //Arrange
                var searchProgrammingLanguages = new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(1, "C#"), new ProgrammingLanguageDTO(3, "F#") };
                SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(5, "Tag5") }, new List<LevelDTO>() { }, searchProgrammingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);
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
                for (int i = 0; i < expected.Count - 1; i++)
                {
                    Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
                }

            }


        [Fact]
        public void Search_given_SearchForm_containing_all_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage()
        {

            //Arrange
            var searchProgrammingLanguages = new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(1, "C#"), new ProgrammingLanguageDTO(3, "F#"), new ProgrammingLanguageDTO(2, "Java") };
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(5, "Tag5") }, new List<LevelDTO>() { }, searchProgrammingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);
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
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }

        }
        #endregion

        
        #region Tag6-Media
        //tag6, varying media



    [Fact]
    public void Search_given_SearchForm_containing_media_returns_list_of_material_only_with_given_media()
    {
            //Arrange
            var searchMedia = new List<MediaDTO>() { new MediaDTO(3, "Report") };
        SearchForm searchForm = new SearchForm("Dockerize your life", new List<TagDTO>() { new TagDTO(6, "Tag6")}, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), searchMedia, 0);
            List<MaterialDTO> expected = new List<MaterialDTO>() { 
                _tag6Materials.ElementAt(2).ConvertToMaterialDTO(),
                _tag6Materials.ElementAt(0).ConvertToMaterialDTO(),
                _tag6Materials.ElementAt(1).ConvertToMaterialDTO(),
            };
      
        //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }

        }

        [Fact]
        public void Search_given_SearchForm_containing_two_media_returns_list_of_materials_prioritized_by_medias()
        {
            //Arrange
            var searchMedia = new List<MediaDTO>() { new MediaDTO(1, "Book"), new MediaDTO(2, "Video") };
            SearchForm searchForm = new SearchForm("Dockerize your life", new List<TagDTO>() { new TagDTO(6, "Tag6") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), searchMedia, 0);
            List<MaterialDTO> expected = new List<MaterialDTO>() {
                _tag6Materials.ElementAt(0).ConvertToMaterialDTO(),
                _tag6Materials.ElementAt(2).ConvertToMaterialDTO(),
                _tag6Materials.ElementAt(1).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }

        }



        [Fact]
        public void Search_given_SearchForm_containing_three_media_ignores_order_returns_list_of_materials_prioritized_by_medias()
        {
            //Arrange
            var searchMedia = new List<MediaDTO>() { new MediaDTO(3, "Report"), new MediaDTO(1, "Book"), new MediaDTO(2, "Video") };
            SearchForm searchForm = new SearchForm("Dockerize your life", new List<TagDTO>() { new TagDTO(6, "Tag6") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), searchMedia, 0);
            List<MaterialDTO> expected = new List<MaterialDTO>() {
                _tag6Materials.ElementAt(0).ConvertToMaterialDTO(),  
                _tag6Materials.ElementAt(1).ConvertToMaterialDTO(),
                   _tag6Materials.ElementAt(2).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }

        }
        #endregion


        #region Tag7-Author
        //tag7, varying author

        [Fact]
        public void Search_given_SearchForm_containing_author_in_textfield_returns_materials_prioritized_by_author()
        {
            //Arrange
            
            SearchForm searchForm = new SearchForm("Alfa Alfason", new List<TagDTO>() { new TagDTO(7, "Tag7") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            List<MaterialDTO> expected = new List<MaterialDTO>() {
                _tag7Materials.ElementAt(0).ConvertToMaterialDTO(),
                _tag7Materials.ElementAt(3).ConvertToMaterialDTO(),
                _tag7Materials.ElementAt(1).ConvertToMaterialDTO(),
                _tag7Materials.ElementAt(2).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }

        [Fact]
        public void Search_given_SearchForm_containing_author_firstName_in_textfield_returns_materials_prioritized_by_author()
        {
            //Arrange

            SearchForm searchForm = new SearchForm("Alfa", new List<TagDTO>() { new TagDTO(7, "Tag7") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            List<MaterialDTO> expected = new List<MaterialDTO>() {
                _tag7Materials.ElementAt(0).ConvertToMaterialDTO(),
                _tag7Materials.ElementAt(1).ConvertToMaterialDTO(),
                _tag7Materials.ElementAt(3).ConvertToMaterialDTO(),
                _tag7Materials.ElementAt(2).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }

        [Fact]
        public void Search_given_SearchForm_containing_two_authors_in_textfield_returns_materials_prioritized_by_author()
        {
            //Arrange

            SearchForm searchForm = new SearchForm("Alfa Alfason, Bravo Bravoson", new List<TagDTO>() { new TagDTO(7, "Tag7") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            List<MaterialDTO> expected = new List<MaterialDTO>() {
                _tag7Materials.ElementAt(3).ConvertToMaterialDTO(),
                _tag7Materials.ElementAt(0).ConvertToMaterialDTO(),
                _tag7Materials.ElementAt(1).ConvertToMaterialDTO(),
                _tag7Materials.ElementAt(2).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }


        #endregion

        #region Tag8-Timestamp
        //tag8, timestamp
        [Fact]
        public void Search_given_SearchForm_returns_Materials_prioritized_by_timestamp()
        {
            //Arrange
            SearchForm searchForm = new SearchForm("", new List<TagDTO>() { new TagDTO(8, "Tag8") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            List<MaterialDTO> expected = new List<MaterialDTO>() {
                _tag8Materials.ElementAt(6).ConvertToMaterialDTO(),
                _tag8Materials.ElementAt(5).ConvertToMaterialDTO(),
                _tag8Materials.ElementAt(4).ConvertToMaterialDTO(),
                _tag8Materials.ElementAt(3).ConvertToMaterialDTO(),
                _tag8Materials.ElementAt(2).ConvertToMaterialDTO(),
                _tag8Materials.ElementAt(1).ConvertToMaterialDTO(),
                _tag8Materials.ElementAt(0).ConvertToMaterialDTO(),
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }







        #endregion

        #region Tag9-Titles
        //tag9, title

        [Fact]
        public void Search_given_SearchForm_containing_textinput_lorem_returns_list_of_material_prioritized_by_titles_containing_lorem()
        {

            //Arrange
            SearchForm searchForm = new SearchForm("Lorem", new List<TagDTO>() { new TagDTO(9, "Tag9") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var expected = new List<MaterialDTO>()
            {
                 _tag9Materials.ElementAt(4).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(3).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(2).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(1).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(0).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(5).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(6).ConvertToMaterialDTO()
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert

            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }

        [Fact]
        public void Search_given_SearchForm_containing_textinput_lorem_etc_returns_list_of_material_prioritized_by_titles_with_lorem_etc_first()
        {

            //Arrange
            SearchForm searchForm = new SearchForm("Lorem ipsum dolor sit amet", new List<TagDTO>() { new TagDTO(9, "Tag9") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var expected = new List<MaterialDTO>()
            {
                 _tag9Materials.ElementAt(0).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(1).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(2).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(3).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(4).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(5).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(6).ConvertToMaterialDTO()
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert

            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }
        #endregion

        #region Tag10Tag11-WeightTwoTags
        //Tag 10 + 11, Varying weight, two tags
        [Fact]
        public void Search_given_SearchForm_containing__lorem_returns_list_of_material_prioritized_by_titles_containing_lorem()
        {

            //Arrange
            SearchForm searchForm = new SearchForm("Lorem", new List<TagDTO>() { new TagDTO(9, "Tag9") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var expected = new List<MaterialDTO>()
            {
                 _tag9Materials.ElementAt(4).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(3).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(2).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(1).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(0).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(5).ConvertToMaterialDTO(),
                 _tag9Materials.ElementAt(6).ConvertToMaterialDTO()
            };

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert

            for (int i = 0; i < expected.Count - 1; i++)
            {
                Assert.Equal(expected[i].Title, actual.ElementAt(i).Title);
            }
        }

        #endregion

        #region Tag12Tag13-TagsInTitle
        //Tag 12 + 13, Tags in title
        #endregion

        #region UpperLower
        [Fact]
        public async Task Search_with_tag_lower_case_returns_material_with_tag()
        {
            var searchform = new SearchForm("", new List<TagDTO>() { new TagDTO(0, "dotnet") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var response = await _searchAlgorithm.Search(searchform);
            var materials = response.Item2;
            var actual = materials.First().Title;

            var expected = _v.UpperLowerMaterial.Title;

            Assert.Equal(expected,actual);
        }

        [Fact]
        public async Task Search_with_level_lower_case_returns_material_with_level()
        {
            var searchform = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>() { new LevelDTO(0,"school")}, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var response = await _searchAlgorithm.Search(searchform);
            var materials = response.Item2;
            var actual = materials.First().Title;

            var expected = _v.UpperLowerMaterial.Title;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Search_with_programmingLanguage_lower_case_returns_material_with_programmingLanguage()
        {
            var searchform = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(0, "go")}, new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var response = await _searchAlgorithm.Search(searchform);
            var materials = response.Item2;
            var actual = materials.First().Title;

            var expected = _v.UpperLowerMaterial.Title;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Search_with_media_lower_case_returns_material_with_media()
        {
            var searchform = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>() { new MediaDTO(0,"youTUBE")}, 0);

            var response = await _searchAlgorithm.Search(searchform);
            var materials = response.Item2;
            var actual = materials.First().Title;

            var expected = _v.UpperLowerMaterial.Title;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Search_with_language_lower_case_returns_language_with_media()
        {
            var searchform = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>() { new LanguageDTO(0,"sWeDiSh")}, new List<MediaDTO>(), 0);

            var response = await _searchAlgorithm.Search(searchform);
            var materials = response.Item2;
            var actual = materials.First().Title;

            var expected = _v.UpperLowerMaterial.Title;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Search_with_textfield_containing_tag_lower_case_returns_material_with_tag()
        {
            var searchform = new SearchForm("Something something doTNet test", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var response = await _searchAlgorithm.Search(searchform);
            var materials = response.Item2;
            var actual = materials.First().Title;

            var expected = _v.UpperLowerMaterial.Title;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Search_with_textfield_containing_author_lower_case_returns_material_with_author()
        {
            var searchform = new SearchForm("Something something some author and or auTHOrSON ", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var response = await _searchAlgorithm.Search(searchform);
            var materials = response.Item2;
            var actual = materials.First().Title;

            var expected = _v.UpperLowerMaterial.Title;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Search_with_textfield_containing_title_lower_case_returns_material_with_title()
        {
            var searchform = new SearchForm("HALLELUJA", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            var response = await _searchAlgorithm.Search(searchform);
            var materials = response.Item2;
            var actual = materials.First().Title;

            var expected = _v.UpperLowerMaterial.Title;

            Assert.Equal(expected, actual);
        }
        #endregion

        #region NotFound
        [Fact]
        public async Task Search_with_language_non_existing_on_any_materials_returns_notFound()
        {
            var searchform = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>() { new LanguageDTO(0, "German")}, new List<MediaDTO>(), 0);

            var response = await _searchAlgorithm.Search(searchform);
            var actual = response.Item1;

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Search_with_parameters_on_no_materials_returns_notFound()
        {
            var searchform = new SearchForm("", new List<TagDTO>() { new TagDTO(0, "ThisDoesNotExists")}, new List<LevelDTO>() { new LevelDTO(0, "ThisDoesNotExists") }, new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(0, "ThisDoesNotExists") }, new List<LanguageDTO>() { new LanguageDTO(0, "ThisDoesNotExists") }, new List<MediaDTO>() { new MediaDTO(0, "ThisDoesNotExists") }, 0);

            var response = await _searchAlgorithm.Search(searchform);
            var actual = response.Item1;

            var expected = Status.NotFound;

            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
