// ***********************************************************************
// Assembly         : WebService.Infrastructure.Tests
// Author           : Group BTG
// Created          : 12-10-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="SearchAlgorithmTest.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;

//using ExtensionMethods;
namespace WebService.Infrastructure.Tests.SearchAlgorithmTests;

/// <summary>
///     Class SearchAlgorithmTest.
/// </summary>
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class SearchAlgorithmTest
{
    /// <summary>
    ///     The search algorithm
    /// </summary>
    private readonly SearchAlgorithm _searchAlgorithm;

    /// <summary>
    ///     The tag1011 materials
    /// </summary>
    private readonly List<Material> _tag1011Materials;

    /// <summary>
    ///     The tag1 materials
    /// </summary>
    private readonly List<Material> _tag1Materials;

    /// <summary>
    ///     The tag2 materials
    /// </summary>
    private readonly List<Material> _tag2Materials;

    /// <summary>
    ///     The tag3 materials
    /// </summary>
    private readonly List<Material> _tag3Materials;

    /// <summary>
    ///     The tag4 materials
    /// </summary>
    private readonly List<Material> _tag4Materials;

    /// <summary>
    ///     The tag5 materials
    /// </summary>
    private readonly List<Material> _tag5Materials;

    /// <summary>
    ///     The tag6 materials
    /// </summary>
    private readonly List<Material> _tag6Materials;

    /// <summary>
    ///     The tag7 materials
    /// </summary>
    private readonly List<Material> _tag7Materials;

    /// <summary>
    ///     The tag8 materials
    /// </summary>
    private readonly List<Material> _tag8Materials;

    /// <summary>
    ///     The tag9 materials
    /// </summary>
    private readonly List<Material> _tag9Materials;

    /// <summary>
    ///     The v
    /// </summary>
    private readonly SearchTestVariables _v;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SearchAlgorithmTest" /> class.
    /// </summary>
    public SearchAlgorithmTest()
    {
        _v = new SearchTestVariables();
        var materialRepository = new MaterialRepository(_v.Context);
        var tagRepository = new TagRepository(_v.Context);
        _searchAlgorithm = new SearchAlgorithm(materialRepository, tagRepository);

        _tag1Materials = _v.Tag1Materials;
        _tag2Materials = _v.Tag2Materials;
        _tag3Materials = _v.Tag3Materials;
        _tag4Materials = _v.Tag4Materials;
        _tag5Materials = _v.Tag5Materials;
        _tag6Materials = _v.Tag6Materials;
        _tag7Materials = _v.Tag7Materials;
        _tag8Materials = _v.Tag8Materials;
        _tag9Materials = _v.Tag9Materials;
        _tag1011Materials = _v.Tag1011Materials;
    }


    #region Search

    /// <summary>
    ///     Defines the test method Search_given_nothing_returns_status_found.
    /// </summary>
    [Fact]
    public void Search_given_nothing_returns_status_found()
    {
        //Arrange
        var searchForm = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        const Status expected = Status.Found;

        //Act
        var result = _searchAlgorithm.Search(searchForm).Result;

        //Assert
        Assert.Equal(expected, result.Item1);
    }

    #endregion


    #region Tag1-Weight

    //tag1, varying weight
    /// <summary>
    ///     Defines the test method Search_given_SearchForm_returns_list_of_materials_prioritized_by_tag_weight.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_returns_list_of_materials_prioritized_by_tag_weight()
    {
        //Arrange
        var searchForm = new SearchForm("ELI5: Induction Proofs", new List<TagDTO> { new(1, "Tag1") },
            new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(10);


        //Assert
        Assert.Collection(actual,
            item => Assert.Equal(_tag1Materials[9].Title, item.Title),
            item => Assert.Equal(_tag1Materials[8].Title, item.Title),
            item => Assert.Equal(_tag1Materials[7].Title, item.Title),
            item => Assert.Equal(_tag1Materials[6].Title, item.Title),
            item => Assert.Equal(_tag1Materials[5].Title, item.Title),
            item => Assert.Equal(_tag1Materials[4].Title, item.Title),
            item => Assert.Equal(_tag1Materials[3].Title, item.Title),
            item => Assert.Equal(_tag1Materials[2].Title, item.Title),
            item => Assert.Equal(_tag1Materials[1].Title, item.Title),
            item => Assert.Equal(_tag1Materials[0].Title, item.Title)
        );
    }

    #endregion


    #region Tag4-Language

    //tag4 - varying Language

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_language_returns_list_of_material_only_with_given_language.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_containing_language_returns_list_of_material_only_with_given_language()
    {
        //Arrange
        var searchLanguage = new List<LanguageDTO> { new(1, "Danish") };
        var searchForm = new SearchForm("", new List<TagDTO> { new(4, "Tag4") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), searchLanguage, new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(1);

        //Assert
        Assert.Collection(actual,
           item => Assert.Equal(_tag4Materials[0].Title, item.Title)
       );
    }

    #endregion

    #region Tag8-Timestamp

    //tag8, timestamp
    /// <summary>
    ///     Defines the test method Search_given_SearchForm_returns_Materials_prioritized_by_timestamp.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_returns_Materials_prioritized_by_timestamp()
    {
        //Arrange
        var searchForm = new SearchForm("", new List<TagDTO> { new(8, "Tag8") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(8);

        //Assert
        Assert.Collection(actual,
           item => Assert.Equal(_tag8Materials[7].Title, item.Title),
           item => Assert.Equal(_tag8Materials[6].Title, item.Title),
           item => Assert.Equal(_tag8Materials[5].Title, item.Title),
           item => Assert.Equal(_tag8Materials[4].Title, item.Title),
           item => Assert.Equal(_tag8Materials[3].Title, item.Title),
           item => Assert.Equal(_tag8Materials[2].Title, item.Title),
           item => Assert.Equal(_tag8Materials[1].Title, item.Title),
           item => Assert.Equal(_tag8Materials[0].Title, item.Title)
       );
    }

    #endregion

    #region SearchFormParse

    #endregion

    #region Tag2-Rating

    //tag2, varying rating

    /// <summary>
    ///     Defines the test method Search_given_SearchForm_with_rating_1_returns_list_of_material_with_rating_1_or_higher.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_with_rating_1_returns_list_of_material_with_rating_1_or_higher()
    {
        //Arrange
        const int rating = 1;
        var searchForm = new SearchForm("", new List<TagDTO> { new(2, "Tag2") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);

        //Act

        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(10);

        //Assert
        Assert.All(actual,
         item => Assert.True(rating <= item.AverageRating())
         );

        Assert.Collection(actual,
           item => Assert.Equal(_tag2Materials[10].Title, item.Title),
           item => Assert.Equal(_tag2Materials[9].Title, item.Title),
           item => Assert.Equal(_tag2Materials[8].Title, item.Title),
           item => Assert.Equal(_tag2Materials[7].Title, item.Title),
           item => Assert.Equal(_tag2Materials[6].Title, item.Title),
           item => Assert.Equal(_tag2Materials[5].Title, item.Title),
           item => Assert.Equal(_tag2Materials[4].Title, item.Title),
           item => Assert.Equal(_tag2Materials[3].Title, item.Title),
           item => Assert.Equal(_tag2Materials[2].Title, item.Title),
           item => Assert.Equal(_tag2Materials[1].Title, item.Title)
       );
    }

    /// <summary>
    ///     Defines the test method Search_given_SearchForm_with_rating_3_returns_list_of_material_with_rating_3_or_higher.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_with_rating_3_returns_list_of_material_with_rating_3_or_higher()
    {
        //Arrange
        const int rating = 3;
        var searchForm = new SearchForm("", new List<TagDTO> { new(2, "Tag2") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);

        //Act

        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(8);

        //Assert       
        Assert.All(actual,
          item => Assert.True(rating <= item.AverageRating())
      );
        Assert.Collection(actual,
       item => Assert.Equal(_tag2Materials[10].Title, item.Title),
       item => Assert.Equal(_tag2Materials[9].Title, item.Title),
       item => Assert.Equal(_tag2Materials[8].Title, item.Title),
       item => Assert.Equal(_tag2Materials[7].Title, item.Title),
       item => Assert.Equal(_tag2Materials[6].Title, item.Title),
       item => Assert.Equal(_tag2Materials[5].Title, item.Title),
       item => Assert.Equal(_tag2Materials[4].Title, item.Title),
       item => Assert.Equal(_tag2Materials[3].Title, item.Title)
   );

    }

    /// <summary>
    ///     Defines the test method Search_given_SearchForm_with_rating_5_returns_list_of_material_with_rating_5_or_higher.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_with_rating_5_returns_list_of_material_with_rating_5_or_higher()
    {
        //Arrange
        const int rating = 5;
        var searchForm = new SearchForm("", new List<TagDTO> { new(2, "Tag2") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(6);

        //Assert       
        Assert.All(actual,
          item => Assert.True(rating <= item.AverageRating())
      );
        Assert.Collection(actual,
       item => Assert.Equal(_tag2Materials[10].Title, item.Title),
       item => Assert.Equal(_tag2Materials[9].Title, item.Title),
       item => Assert.Equal(_tag2Materials[8].Title, item.Title),
       item => Assert.Equal(_tag2Materials[7].Title, item.Title),
       item => Assert.Equal(_tag2Materials[6].Title, item.Title),
       item => Assert.Equal(_tag2Materials[5].Title, item.Title)
   );
    }

    /// <summary>
    ///     Defines the test method Search_given_SearchForm_with_rating_7_returns_list_of_material_with_rating_7_or_higher.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_with_rating_7_returns_list_of_material_with_rating_7_or_higher()
    {
        //Arrange
        const int rating = 7;
        var searchForm = new SearchForm("", new List<TagDTO> { new(2, "Tag2") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(4);

        //Assert
        //Assert       
        Assert.All(actual,
          item => Assert.True(rating <= item.AverageRating())
      );
        Assert.Collection(actual,
       item => Assert.Equal(_tag2Materials[10].Title, item.Title),
       item => Assert.Equal(_tag2Materials[9].Title, item.Title),
       item => Assert.Equal(_tag2Materials[8].Title, item.Title),
       item => Assert.Equal(_tag2Materials[7].Title, item.Title)
   );
    }

    /// <summary>
    ///     Defines the test method Search_given_SearchForm_with_rating_9_returns_list_of_material_with_rating_9_or_higher.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_with_rating_9_returns_list_of_material_with_rating_9_or_higher()
    {
        //Arrange
        const int rating = 9;
        var searchForm = new SearchForm("", new List<TagDTO> { new(2, "Tag2") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(2);

        //Assert       
        Assert.All(actual,
          item => Assert.True(rating <= item.AverageRating())
      );
        Assert.Collection(actual,
       item => Assert.Equal(_tag2Materials[10].Title, item.Title),
       item => Assert.Equal(_tag2Materials[9].Title, item.Title)
   );
    }

    #endregion

    #region Tag3-Media

    //tag3, levels


    /// <summary>
    ///     Defines the test method Search_given_SearchForm_containing_bachelor_returns_list_of_material_prioritized_by_level.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_containing_bachelor_returns_list_of_material_prioritized_by_level()
    {
        //Arrange
        var searchLevels = new List<LevelDTO> { new(1, "Bachelor") };
        var searchForm = new SearchForm("", new List<TagDTO> { new(3, "Tag3") }, searchLevels,
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(4);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag3Materials[0].Title, item.Title),
        item => Assert.Equal(_tag3Materials[3].Title, item.Title),
        item => Assert.Equal(_tag3Materials[4].Title, item.Title),
        item => Assert.Equal(_tag3Materials[6].Title, item.Title)
        );
    }

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_bachelor_master_returns_list_of_material_prioritized_by_level.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_containing_bachelor_master_returns_list_of_material_prioritized_by_level()
    {
        //Arrange
        var searchLevels = new List<LevelDTO> { new(1, "Bachelor"), new(2, "Master") };
        var searchForm = new SearchForm("", new List<TagDTO> { new(3, "Tag3") }, searchLevels,
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(6);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag3Materials[3].Title, item.Title),
        item => Assert.Equal(_tag3Materials[6].Title, item.Title),
        item => Assert.Equal(_tag3Materials[0].Title, item.Title),
        item => Assert.Equal(_tag3Materials[1].Title, item.Title),
        item => Assert.Equal(_tag3Materials[4].Title, item.Title),
        item => Assert.Equal(_tag3Materials[5].Title, item.Title)
        );
    }


    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_bachelor_master_phd_returns_list_of_material_prioritized_by_level.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_containing_bachelor_master_phd_returns_list_of_material_prioritized_by_level()
    {
        //Arrange
        var searchLevels = new List<LevelDTO> { new(1, "Bachelor"), new(2, "Master"), new(3, "PHD") };
        var searchForm = new SearchForm("", new List<TagDTO> { new(3, "Tag3") }, searchLevels,
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(7);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag3Materials[6].Title, item.Title),
        item => Assert.Equal(_tag3Materials[3].Title, item.Title),
        item => Assert.Equal(_tag3Materials[4].Title, item.Title),
        item => Assert.Equal(_tag3Materials[5].Title, item.Title),
        item => Assert.Equal(_tag3Materials[0].Title, item.Title),
        item => Assert.Equal(_tag3Materials[1].Title, item.Title),
        item => Assert.Equal(_tag3Materials[2].Title, item.Title)
        );
    }

    #endregion


    #region Tag5-ProgrammingLanguage

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_one_programmingLanguage_returns_list_of_material_prioritized_by_programmingLanguage.
    /// </summary>
    [Fact]
    public void
        Search_given_SearchForm_containing_one_programmingLanguage_returns_list_of_material_prioritized_by_programmingLanguage()
    {
        //Arrange
        var searchProgrammingLanguages = new List<ProgrammingLanguageDTO> { new(1, "C#") };
        var searchForm = new SearchForm("", new List<TagDTO> { new(5, "Tag5") }, new List<LevelDTO>(),
            searchProgrammingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act

        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(4);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag5Materials[0].Title, item.Title),
        item => Assert.Equal(_tag5Materials[3].Title, item.Title),
        item => Assert.Equal(_tag5Materials[4].Title, item.Title),
        item => Assert.Equal(_tag5Materials[6].Title, item.Title)
        );
    }


    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_two_programmingLanguage_returns_list_of_material_prioritized_by_programmingLanguage.
    /// </summary>
    [Fact]
    public void
        Search_given_SearchForm_containing_two_programmingLanguage_returns_list_of_material_prioritized_by_programmingLanguage()
    {
        //Arrange
        var searchProgrammingLanguages = new List<ProgrammingLanguageDTO> { new(1, "C#"), new(3, "F#") };
        var searchForm = new SearchForm("", new List<TagDTO> { new(5, "Tag5") }, new List<LevelDTO>(),
            searchProgrammingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);
      
        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(6);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag5Materials[3].Title, item.Title),
        item => Assert.Equal(_tag5Materials[6].Title, item.Title),
        item => Assert.Equal(_tag5Materials[0].Title, item.Title),
        item => Assert.Equal(_tag5Materials[1].Title, item.Title),
        item => Assert.Equal(_tag5Materials[4].Title, item.Title),
        item => Assert.Equal(_tag5Materials[5].Title, item.Title)
        );
    }


    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_all_programmingLanguage_returns_list_of_material_prioritized_by_programmingLanguage.
    /// </summary>
    [Fact]
    public void
        Search_given_SearchForm_containing_all_programmingLanguage_returns_list_of_material_prioritized_by_programmingLanguage()
    {
        //Arrange
        var searchProgrammingLanguages = new List<ProgrammingLanguageDTO> { new(1, "C#"), new(3, "F#"), new(2, "Java") };
        var searchForm = new SearchForm("", new List<TagDTO> { new(5, "Tag5") }, new List<LevelDTO>(),
            searchProgrammingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);
      
        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(7);
        
        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag5Materials[6].Title, item.Title),
        item => Assert.Equal(_tag5Materials[3].Title, item.Title),
        item => Assert.Equal(_tag5Materials[4].Title, item.Title),
        item => Assert.Equal(_tag5Materials[5].Title, item.Title),
        item => Assert.Equal(_tag5Materials[0].Title, item.Title),
        item => Assert.Equal(_tag5Materials[1].Title, item.Title),
        item => Assert.Equal(_tag5Materials[2].Title, item.Title)
        );
    }

    #endregion


    #region Tag6-Media

    //tag6, varying media


    /// <summary>
    ///     Defines the test method Search_given_SearchForm_containing_media_returns_list_of_material_only_with_given_media.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_containing_media_returns_list_of_material_only_with_given_media()
    {
        //Arrange
        var searchMedia = new List<MediaDTO> { new(3, "Report") };
        var searchForm = new SearchForm("Dockerize your life", new List<TagDTO> { new(6, "Tag6") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), searchMedia, 0);
     
        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(3);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag6Materials[1].Title, item.Title),
        item => Assert.Equal(_tag6Materials[0].Title, item.Title),
        item => Assert.Equal(_tag6Materials[2].Title, item.Title)
        );
    }

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_two_media_returns_list_of_materials_prioritized_by_medias.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_containing_two_media_returns_list_of_materials_prioritized_by_medias()
    {
        //Arrange
        var searchMedia = new List<MediaDTO> { new(1, "Book"), new(2, "Video") };
        var searchForm = new SearchForm("Dockerize your life", new List<TagDTO> { new(6, "Tag6") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), searchMedia, 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(3);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag6Materials[0].Title, item.Title),
        item => Assert.Equal(_tag6Materials[2].Title, item.Title),
        item => Assert.Equal(_tag6Materials[1].Title, item.Title)
        );
    }


    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_three_media_ignores_order_returns_list_of_materials_prioritized_by_medias.
    /// </summary>
    [Fact]
    public void
        Search_given_SearchForm_containing_three_media_ignores_order_returns_list_of_materials_prioritized_by_medias()
    {
        //Arrange
        var searchMedia = new List<MediaDTO> { new(3, "Report"), new(1, "Book"), new(2, "Video") };
        var searchForm = new SearchForm("Dockerize your life", new List<TagDTO> { new(6, "Tag6") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), searchMedia, 0);
 
        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(3);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag6Materials[0].Title, item.Title),
        item => Assert.Equal(_tag6Materials[1].Title, item.Title),
        item => Assert.Equal(_tag6Materials[2].Title, item.Title)
        );
    }

    #endregion


    #region Tag7-Author

    //tag7, varying author

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_author_in_textfield_returns_materials_prioritized_by_author.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_containing_author_in_textfield_returns_materials_prioritized_by_author()
    {
        //Arrange
        var searchForm = new SearchForm("Alfa Alfason .NET Framework intro ", new List<TagDTO> { new(7, "Tag7") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(4);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag7Materials[0].Title, item.Title),
        item => Assert.Equal(_tag7Materials[3].Title, item.Title),
        item => Assert.Equal(_tag7Materials[1].Title, item.Title),
        item => Assert.Equal(_tag7Materials[2].Title, item.Title)
        );
    }

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_author_firstName_in_textfield_returns_materials_prioritized_by_author.
    /// </summary>
    [Fact]
    public void
        Search_given_SearchForm_containing_author_firstName_in_textfield_returns_materials_prioritized_by_author()
    {
        //Arrange

        var searchForm = new SearchForm("Alfa .NET Framework intro ", new List<TagDTO> { new(7, "Tag7") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(4);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag7Materials[0].Title, item.Title),
        item => Assert.Equal(_tag7Materials[1].Title, item.Title),
        item => Assert.Equal(_tag7Materials[3].Title, item.Title),
        item => Assert.Equal(_tag7Materials[2].Title, item.Title)
        );
    }

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_two_authors_in_textfield_returns_materials_prioritized_by_author.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_containing_two_authors_in_textfield_returns_materials_prioritized_by_author()
    {
        //Arrange

        var searchForm = new SearchForm("Alfa Alfason, Bravo Bravoson .NET Framework intro ", new List<TagDTO> { new(7, "Tag7") },
            new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(4);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag7Materials[3].Title, item.Title),
        item => Assert.Equal(_tag7Materials[0].Title, item.Title),
        item => Assert.Equal(_tag7Materials[1].Title, item.Title),
        item => Assert.Equal(_tag7Materials[2].Title, item.Title)
        );
    }

    #endregion

    #region Tag9-Titles

    //tag9, title

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_textInput_lorem_returns_list_of_material_prioritized_by_titles_containing_lorem.
    /// </summary>
    [Fact]
    public void
        Search_given_SearchForm_containing_textInput_lorem_returns_list_of_material_prioritized_by_titles_containing_lorem()
    {
        //Arrange
        var searchForm = new SearchForm("Lorem", new List<TagDTO> { new(9, "Tag9") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(5);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag9Materials[4].Title, item.Title),
        item => Assert.Equal(_tag9Materials[3].Title, item.Title),
        item => Assert.Equal(_tag9Materials[2].Title, item.Title),
        item => Assert.Equal(_tag9Materials[1].Title, item.Title),
        item => Assert.Equal(_tag9Materials[0].Title, item.Title)
        );
    }

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_textInput_lorem_etc_returns_list_of_material_prioritized_by_titles_with_lorem_etc_first.
    /// </summary>
    [Fact]
    public void
        Search_given_SearchForm_containing_textInput_lorem_etc_returns_list_of_material_prioritized_by_titles_with_lorem_etc_first()
    {
        //Arrange
        var searchForm = new SearchForm("Lorem ipsum dolor sit amet", new List<TagDTO> { new(9, "Tag9") },
            new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(5);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag9Materials[0].Title, item.Title),
        item => Assert.Equal(_tag9Materials[1].Title, item.Title),
        item => Assert.Equal(_tag9Materials[2].Title, item.Title),
        item => Assert.Equal(_tag9Materials[3].Title, item.Title),
        item => Assert.Equal(_tag9Materials[4].Title, item.Title)
        );
    }

    #endregion

    #region Tag10Tag11-WeightTwoTags

    //Tag 10 + 11, Varying weight, two tags
    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_twoTags_returns_list_of_material_prioritized_by_tag_weight_sum.
    /// </summary>
    [Fact]
    public void Search_given_SearchForm_containing_twoTags_returns_list_of_material_prioritized_by_tag_weight_sum()
    {
        //Arrange
        var searchForm = new SearchForm("", new List<TagDTO> { new(10, "Tag10"), new(11, "Tag11") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(6);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag1011Materials[1].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[4].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[2].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[3].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[5].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[0].Title, item.Title)
        );
    }

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_Tag10_returns_list_of_material_prioritized_by_tag_weight_sum_of_Tag10_only.
    /// </summary>
    [Fact]
    public void
        Search_given_SearchForm_containing_Tag10_returns_list_of_material_prioritized_by_tag_weight_sum_of_Tag10_only()
    {
        //Arrange
        var searchForm = new SearchForm("", new List<TagDTO> { new(10, "Tag10") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(6);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag1011Materials[1].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[4].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[5].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[3].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[2].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[0].Title, item.Title)
        );
    }

    /// <summary>
    ///     Defines the test method
    ///     Search_given_SearchForm_containing_Tag11_returns_list_of_material_prioritized_by_tag_weight_sum_of_Tag11_only.
    /// </summary>
    [Fact]
    public void
        Search_given_SearchForm_containing_Tag11_returns_list_of_material_prioritized_by_tag_weight_sum_of_Tag11_only()
    {
        //Arrange
        var searchForm = new SearchForm("", new List<TagDTO> { new(11, "Tag11") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        //Act
        var actual = _searchAlgorithm.Search(searchForm).Result.Item2.Take(6);

        //Assert
        Assert.Collection(actual,
        item => Assert.Equal(_tag1011Materials[1].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[2].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[3].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[4].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[5].Title, item.Title),
        item => Assert.Equal(_tag1011Materials[0].Title, item.Title)
        );
    }

    #endregion

    #region UpperLower

    /// <summary>
    ///     Defines the test method Search_with_tag_lower_case_returns_material_with_tag.
    /// </summary>
    [Fact]
    public async Task Search_with_tag_lower_case_returns_material_with_tag()
    {
        var searchForm = new SearchForm("", new List<TagDTO> { new(0, "dotnet") }, new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var materials = response.Item2;
        var actual = materials.First().Title;

        var expected = _v.UpperLowerMaterial.Title;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method Search_with_level_lower_case_returns_material_with_level.
    /// </summary>
    [Fact]
    public async Task Search_with_level_lower_case_returns_material_with_level()
    {
        var searchForm = new SearchForm("", new List<TagDTO>(), new List<LevelDTO> { new(0, "school") },
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var materials = response.Item2;
        var actual = materials.First().Title;

        var expected = _v.UpperLowerMaterial.Title;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method Search_with_programmingLanguage_lower_case_returns_material_with_programmingLanguage.
    /// </summary>
    [Fact]
    public async Task Search_with_programmingLanguage_lower_case_returns_material_with_programmingLanguage()
    {
        var searchForm = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO> { new(0, "go") }, new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var materials = response.Item2;
        var actual = materials.First().Title;

        var expected = _v.UpperLowerMaterial.Title;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method Search_with_media_lower_case_returns_material_with_media.
    /// </summary>
    [Fact]
    public async Task Search_with_media_lower_case_returns_material_with_media()
    {
        var searchForm = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO> { new(0, "youTUBE") }, 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var materials = response.Item2;
        var actual = materials.First().Title;

        var expected = _v.UpperLowerMaterial.Title;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method Search_with_language_lower_case_returns_language_with_media.
    /// </summary>
    [Fact]
    public async Task Search_with_language_lower_case_returns_language_with_media()
    {
        var searchForm = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO> { new(0, "sWeDiSh") }, new List<MediaDTO>(), 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var materials = response.Item2;
        var actual = materials.First().Title;

        var expected = _v.UpperLowerMaterial.Title;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method Search_with_textfield_containing_tag_lower_case_returns_material_with_tag.
    /// </summary>
    [Fact]
    public async Task Search_with_textfield_containing_tag_lower_case_returns_material_with_tag()
    {
        var searchForm = new SearchForm("Something something doTNet test", new List<TagDTO>(), new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var materials = response.Item2;
        var actual = materials.First().Title;

        var expected = _v.UpperLowerMaterial.Title;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method Search_with_textfield_containing_author_lower_case_returns_material_with_author.
    /// </summary>
    [Fact]
    public async Task Search_with_textfield_containing_author_lower_case_returns_material_with_author()
    {
        var searchForm = new SearchForm("Something something some author and or auTHOrSON ", new List<TagDTO>(),
            new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var materials = response.Item2;
        var actual = materials.First().Title;

        var expected = _v.UpperLowerMaterial.Title;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method Search_with_textfield_containing_title_lower_case_returns_material_with_title.
    /// </summary>
    [Fact]
    public async Task Search_with_textfield_containing_title_lower_case_returns_material_with_title()
    {
        var searchForm = new SearchForm("HALLELUJA", new List<TagDTO>(), new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var materials = response.Item2;
        var actual = materials.First().Title;

        var expected = _v.UpperLowerMaterial.Title;

        Assert.Equal(expected, actual);
    }

    #endregion

    #region NotFound

    /// <summary>
    ///     Defines the test method Search_with_language_non_existing_on_any_materials_returns_notFound.
    /// </summary>
    [Fact]
    public async Task Search_with_language_non_existing_on_any_materials_returns_notFound()
    {
        var searchForm = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(),
            new List<ProgrammingLanguageDTO>(), new List<LanguageDTO> { new(0, "German") }, new List<MediaDTO>(), 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var actual = response.Item1;

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method Search_with_parameters_on_no_materials_returns_notFound.
    /// </summary>
    [Fact]
    public async Task Search_with_parameters_on_no_materials_returns_notFound()
    {
        var searchForm = new SearchForm("", new List<TagDTO> { new(0, "ThisDoesNotExists") },
            new List<LevelDTO> { new(0, "ThisDoesNotExists") },
            new List<ProgrammingLanguageDTO> { new(0, "ThisDoesNotExists") },
            new List<LanguageDTO> { new(0, "ThisDoesNotExists") }, new List<MediaDTO> { new(0, "ThisDoesNotExists") }, 0);

        var response = await _searchAlgorithm.Search(searchForm);
        var actual = response.Item1;

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    #endregion
}