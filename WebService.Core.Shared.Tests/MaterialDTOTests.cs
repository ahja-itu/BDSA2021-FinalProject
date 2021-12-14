// ***********************************************************************
// Assembly         : WebService.Core.Shared.Tests
// Author           : Group BTG
// Created          : 12-08-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="MaterialDTOTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebService.Entities;
using Xunit;

namespace WebService.Core.Shared.Tests;

/// <summary>
///     Class MaterialDTOTests.
/// </summary>
public class MaterialDTOTests
{
    /// <summary>
    ///     The material
    /// </summary>
    private readonly Material _material;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MaterialDTOTests" /> class.
    /// </summary>
    public MaterialDTOTests()
    {
        _material = new Material(
            0,
            new List<WeightedTag>
            {
                new(0, "weigthedtag0", 2),
                new(1, "weigthedtag1", 10)
            },
            new List<Rating>
            {
                new(0, 7, "reviewer0"),
                new(1, 6, "reviewer1"),
                new(2, 10, "reviewer2"),
                new(3, 9, "reviewer3"),
                new(4, 7, "reviewer4"),
                new(5, 8, "reviewer5")
            },
            new List<Level>
            {
                new(0, "Level0"),
                new(1, "Level1"),
                new(2, "level2")
            },
            new List<ProgrammingLanguage>
            {
                new(0, "ProgrammingLanguage0"),
                new(1, "ProgrammingLanguage0")
            },
            new List<Media>
            {
                new(0, "Media0"),
                new(1, "Media1")
            },
            new Language(0, "language"),
            "Summary",
            "URL.com",
            "Content",
            "Title",
            new List<Author>
            {
                new(0, "firstname0", "surname0"),
                new(0, "firstname1", "surname1")
            },
            DateTime.ParseExact("2011-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
        );
    }

    /// <summary>
    ///     Defines the test method ConvertToMaterialDTOReturnsIdenticalMaterialDTO.
    /// </summary>
    [Fact]
    public void ConvertToMaterialDTOReturnsIdenticalMaterialDTO()
    {
        var expected = new MaterialDTO(
            0,
            new List<CreateWeightedTagDTO>
            {
                new WeightedTagDTO("weigthedtag0", 2),
                new WeightedTagDTO("weigthedtag1", 10)
            },
            new List<CreateRatingDTO>
            {
                new RatingDTO(0, 7, "reviewer0"),
                new RatingDTO(1, 6, "reviewer1"),
                new RatingDTO(2, 10, "reviewer2"),
                new RatingDTO(3, 9, "reviewer3"),
                new RatingDTO(4, 7, "reviewer4"),
                new RatingDTO(5, 8, "reviewer5")
            },
            new List<CreateLevelDTO>
            {
                new LevelDTO(0, "Level0"),
                new LevelDTO(1, "Level1"),
                new LevelDTO(2, "level2")
            },
            new List<CreateProgrammingLanguageDTO>
            {
                new ProgrammingLanguageDTO(0, "ProgrammingLanguage0"),
                new ProgrammingLanguageDTO(1, "ProgrammingLanguage0")
            },
            new List<CreateMediaDTO>
            {
                new MediaDTO(0, "Media0"),
                new MediaDTO(1, "Media1")
            },
            new CreateLanguageDTO("language"),
            "Summary",
            "URL.com",
            "Content",
            "Title",
            new List<CreateAuthorDTO>
            {
                new AuthorDTO("firstname0", "surname0"),
                new AuthorDTO("firstname1", "surname1")
            },
            DateTime.ParseExact("2011-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
        );
        var actual = _material.ConvertToMaterialDTO();

        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Tags, actual.Tags);

        Assert.Equal(expected.Ratings.Count, actual.Ratings.Count);
        var expectedCounter = (from RatingDTO? expectedRating in expected.Ratings
            from actualRatingDTO1 in actual.Ratings
            let actualRating = (RatingDTO) actualRatingDTO1
            where expectedRating.Id == actualRating.Id && expectedRating.Value == actualRating.Value &&
                  expectedRating.Reviewer == actualRating.Reviewer &&
                  expectedRating.TimeStamp.ToString(CultureInfo.CurrentCulture) ==
                  actualRating.TimeStamp.ToString(CultureInfo.CurrentCulture)
            select expectedRating).Count();
        Assert.Equal(6, expectedCounter);

        Assert.Equal(expected.Levels, actual.Levels);
        Assert.Equal(expected.ProgrammingLanguages, actual.ProgrammingLanguages);
        Assert.Equal(expected.Medias, actual.Medias);
        Assert.Equal(expected.Language, actual.Language);
        Assert.Equal(expected.Summary, actual.Summary);
        Assert.Equal(expected.Url, actual.Url);
        Assert.Equal(expected.Content, actual.Content);
        Assert.Equal(expected.Title, actual.Title);
        Assert.Equal(expected.Authors, actual.Authors);
        Assert.Equal(expected.TimeStamp.ToString(CultureInfo.CurrentCulture),
            expected.TimeStamp.ToString(CultureInfo.CurrentCulture));
    }

    /// <summary>
    ///     Defines the test method AverageRatingReturns7_8.
    /// </summary>
    [Fact]
    public void AverageRatingReturns7_8()
    {
        const double expected = 7.833333;

        var actual = _material.ConvertToMaterialDTO().AverageRating();

        Assert.Equal(expected, actual, 5);
    }

    /// <summary>
    ///     Defines the test method LevelsToStringReturnsLevelNamesSeperatedBySpaces.
    /// </summary>
    [Fact]
    public void LevelsToStringReturnsLevelNamesSeperatedBySpaces()
    {
        const string expected = "Level0 Level1 level2";

        var actual = _material.ConvertToMaterialDTO().LevelsToString();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    ///     Defines the test method AuthorsToStringReturnFullNamesSeperatedByComma.
    /// </summary>
    [Fact]
    public void AuthorsToStringReturnFullNamesSeperatedByComma()
    {
        const string expected = "Authors: firstname0 surname0, firstname1 surname1";

        var actual = _material.ConvertToMaterialDTO().AuthorsToString();

        Assert.Equal(expected, actual);
    }
}