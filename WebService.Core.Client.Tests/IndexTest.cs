using System.Collections;
using System.Collections.Generic;
using Xunit;
using Bunit;
using WebService.Core.Client.Pages;
using AngleSharp.Dom;


namespace WebService.Core.Client.Tests;

public class IndexTest
{
    [Fact]
    public void IndexContainsWebsiteName()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Index>();

        // Act
        var name = cut.Find("h1");

        // Assert
        Assert.Equal("Bridge The Gap!", name.GetInnerText());
    }

    [Fact]
    public void IndexFilterButtonShowsFilterOptions()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Index>();
        var buttons = cut.FindAll("button");
        var filterButton = buttons.GetElementById("filterButton");
    
        // Act
        filterButton.Click();
        var filterOptions = new List<IElement>();
        var buttonsAfterClick = cut.FindAll("button");
        for (var i = 0; i < 1000; i++)
        {
            var filterOption = buttonsAfterClick.GetElementById("FilterOption " + i);
            if (filterOption != null) filterOptions.Add(filterOption);
        }
        var countOfFilterOptions = cut.FindAll("button").Count - buttons.Count - filterOptions.Count;
        
        // Assert
        Assert.Equal(7, countOfFilterOptions);
    }

    [Fact]
    public void IndexFilterButtonAgainHidesFilterOptions()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Index>();
        var buttonsBefore = cut.FindAll("button");
        var filterButton = buttonsBefore.GetElementById("filterButton");

        // Act
        filterButton.Click();
        filterButton.Click();
        var buttonsAfter = cut.FindAll("button");

        // Assert
        Assert.Equal(buttonsBefore.Count,buttonsAfter.Count);
    }

    [Fact]
    public void IndexChangeSearchFieldForFilterWhenSelectingOptionTags()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Index>();
        var filterButton = cut.FindAll("button").GetElementById("filterButton");

        // Act
        filterButton.Click();
        cut.FindAll("button").GetElementById("tags").Click();
        var searchField = cut.FindAll("input").GetElementById("searchFilter");

        // Assert
        Assert.Equal("Search Tags",searchField.GetAttribute("Placeholder"));
    }

    [Fact]
    public void IndexHideSearchFieldForFilterWhenSelectingOptionRatingsAndShowRangeSlider()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Index>();
        var filterButton = cut.FindAll("button").GetElementById("filterButton");

        // Act
        filterButton.Click();
        cut.FindAll("button").GetElementById("tags").Click();
        var searchField = cut.FindAll("input").GetElementById("searchFilter");
        //var 

        // Assert

    }

    [Fact]
    public void IndexChangeSearchFieldForFilterWhenSelectingOptionLevels()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Index>();
        var filterButton = cut.FindAll("button").GetElementById("filterButton");

        // Act
        filterButton.Click();
        cut.FindAll("button").GetElementById("levels").Click();
        var searchField = cut.FindAll("input").GetElementById("searchFilter");

        // Assert
        Assert.Equal("Search Levels", searchField.GetAttribute("Placeholder"));
    }

    [Fact]
    public void IndexChangeSearchFieldForFilterWhenSelectingOptionProgramingLanguages()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Index>();
        var filterButton = cut.FindAll("button").GetElementById("filterButton");

        // Act
        filterButton.Click();
        cut.FindAll("button").GetElementById("programming Languages").Click();
        var searchField = cut.FindAll("input").GetElementById("searchFilter");

        // Assert
        Assert.Equal("Search Programing Languages", searchField.GetAttribute("Placeholder"));
    }

    [Fact]
    public void IndexChangeSearchFieldForFilterWhenSelectingOptionLanguagess()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Index>();
        var filterButton = cut.FindAll("button").GetElementById("filterButton");

        // Act
        filterButton.Click();
        cut.FindAll("button").GetElementById("languages").Click();
        var searchField = cut.FindAll("input").GetElementById("searchFilter");

        // Assert
        Assert.Equal("Search Languages", searchField.GetAttribute("Placeholder"));
    }

    [Fact]
    public void IndexChangeSearchFieldForFilterWhenSelectingOptionMedias()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Index>();
        var filterButton = cut.FindAll("button").GetElementById("filterButton");

        // Act
        filterButton.Click();
        cut.FindAll("button").GetElementById("medias").Click();
        var searchField = cut.FindAll("input").GetElementById("searchFilter");

        // Assert
        Assert.Equal("Search Medias", searchField.GetAttribute("Placeholder"));
    }
}