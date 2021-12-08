using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ExtensionMethods;
using WebService.Entities;
using Xunit;

namespace WebService.Core.Shared.Tests;

public class MaterialDTOTests
{
    private Material material;  
    public MaterialDTOTests()
    {
            material = new Material(
            0,
            new List<WeightedTag>
            {
                new (0, "weigthedtag0", 2),
                new (1, "weigthedtag1", 10)
            },
            new List<Rating>
            {
                new (0, 7,"reviewer0"),
                new (1, 6,"reviewer1"),
                new (2, 10,"reviewer2"),
                new (3, 9,"reviewer3"),
                new (4, 7,"reviewer4"),
                new (5, 8,"reviewer5")
            },
            new List<Level>
            {
                new (0, "Level0"),
                new (1, "Level1"),
                new (2, "level2")
            },
            new List<ProgrammingLanguage>
            {
                new (0, "ProgrammingLanguage0"),
                new (1, "ProgrammingLanguage0")
            },
            new List<Media>
            {
                new (0, "Media0"),
                new (1, "Media1")
            },
            new Language(0,"language"),
            "Summary",
            "URL.com",
            "Content",
            "Title",
            new List<Author>
            {
                new (0,"firstname0","surname0"),
                new (0,"firstname1","surname1"),
            },
            DateTime.ParseExact("2011-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
        );
    }

    [Fact]
    public void ConvertToMaterialDTOReturnsIdenticalMaterialDTO()
    {
        var expected = new MaterialDTO(
            0,
            new List<CreateWeightedTagDTO>
            {
                new WeightedTagDTO(0, "weigthedtag0", 2),
                new WeightedTagDTO(1, "weigthedtag1", 10)
            },
            new List<CreateRatingDTO>
            {
                new RatingDTO(0, 7,"reviewer0"),
                new RatingDTO(1, 6,"reviewer1"),
                new RatingDTO(2, 10,"reviewer2"),
                new RatingDTO(3, 9,"reviewer3"),
                new RatingDTO(4, 7,"reviewer4"),
                new RatingDTO(5, 8,"reviewer5")
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
            new LanguageDTO(0,"language"),
            "Summary",
            "URL.com",
            "Content",
            "Title",
            new List<CreateAuthorDTO>
            {
                new AuthorDTO(0,"firstname0","surname0"),
                new AuthorDTO(0,"firstname1","surname1"),
            },
            DateTime.ParseExact("2011-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
        );
        var actual = material.ConvertToMaterialDTO();
        
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Tags,actual.Tags);

        Assert.Equal(expected.Ratings.Count, actual.Ratings.Count);
        var expectedCounter = (from RatingDTO? expectedRating in expected.Ratings from actualRatingDTO1 in actual.Ratings let actualRating = (RatingDTO) actualRatingDTO1 where expectedRating.Id == actualRating.Id && expectedRating.Value == actualRating.Value && expectedRating.Reviewer == actualRating.Reviewer && expectedRating.TimeStamp.ToString(CultureInfo.CurrentCulture) == actualRating.TimeStamp.ToString(CultureInfo.CurrentCulture) select expectedRating).Count();
        Assert.Equal(6,expectedCounter);
        
        Assert.Equal(expected.Levels,actual.Levels);
        Assert.Equal(expected.ProgrammingLanguages,actual.ProgrammingLanguages);
        Assert.Equal(expected.Medias,actual.Medias);
        Assert.Equal(expected.Language,actual.Language);
        Assert.Equal(expected.Summary,actual.Summary);
        Assert.Equal(expected.URL,actual.URL);
        Assert.Equal(expected.Content,actual.Content);
        Assert.Equal(expected.Title,actual.Title);
        Assert.Equal(expected.Authors, actual.Authors);
        Assert.Equal(expected.TimeStamp.ToString(CultureInfo.CurrentCulture),expected.TimeStamp.ToString(CultureInfo.CurrentCulture));
    }
    
    [Fact]
    public void AverageRatingReturns7_8()
    {
        var expected = 7.833333;

        var actual = material.ConvertToMaterialDTO().AverageRating();
        
        Assert.Equal(expected,actual,5);
    }
    [Fact]
    public void LevelsToStringReturnsLevelNamesSeperatedBySpaces()
    {
        var expected = "Level0 Level1 level2";

        var actual = material.ConvertToMaterialDTO().LevelsToString();
        
        Assert.Equal(expected,actual);
    }
    [Fact]
    public void AuthorsToStringReturnFullNamesSeperatedByComma()
    {
        var expected = "Authors: firstname0 surname0, firstname1 surname1";

        var actual = material.ConvertToMaterialDTO().AuthorsToString();
        
        Assert.Equal(expected,actual);
    }
    
}