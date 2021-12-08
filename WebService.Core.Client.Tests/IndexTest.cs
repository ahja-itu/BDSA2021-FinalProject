using System;
using System.Collections.Generic;
using Xunit;
using Bunit;
using AngleSharp.Dom;
using WebService.Core.Shared;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Index = WebService.Core.Client.Pages.Index;

namespace WebService.Core.Client.Tests;

#region MockHttpClient

public static class MockHttpClientBunitHelpers
{
    public static MockHttpMessageHandler AddMockHttpClient(this TestServiceProvider services)
    {
        var mockHttpHandler = new MockHttpMessageHandler();
        var httpClient = mockHttpHandler.ToHttpClient();
        httpClient.BaseAddress = new System.Uri("http://localhost:44355/");
        services.AddSingleton<HttpClient>(httpClient);
        return mockHttpHandler;
    }

    public static MockedRequest RespondJson<T>(this MockedRequest request, T content)
    {
        request.Respond(req =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonSerializer.Serialize(content));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        });
        return request;
    }

    public static MockedRequest RespondJson<T>(this MockedRequest request, System.Func<T> contentProvider)
    {
        request.Respond(req =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonSerializer.Serialize(contentProvider()));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        });
        return request;
    }
}

#endregion

public class IndexTest : IDisposable
{
    private TestContext _ctx;
    private MockHttpMessageHandler _mock;
    public IndexTest() 
    {
        var languages = new List<LanguageDTO>
        {
            new (0,"english"),
            new (1,"danish")
        };
        var levels = new List<LevelDTO>
        {
            new (0,"High School"),
            new (1, "Middle School"),
            new (2, "P.H.D")
        };
        var medias = new List<MediaDTO>
        {
            new (0,"Book"),
            new (1,"video"),
            new(2,"quiz")
        };
        var programmingLanguages = new List<ProgrammingLanguageDTO>
        {
            new (0,"c#"),
            new (1,"java"),
            new (2, "c++"),
            new (3,"f#")
        };
        var tags = new List<TagDTO>
        {
            new (0,"c#"),
            new (1,"GOF"),
            new (2, "Docker")
        };
        
        _ctx = new TestContext();
        _mock = _ctx.Services.AddMockHttpClient();
        _mock.When("/Tag").RespondJson(tags);
        _mock.When("/Level").RespondJson(levels);
        _mock.When("/ProgrammingLanguage").RespondJson(programmingLanguages);
        _mock.When("/Language").RespondJson(languages);
        _mock.When("/Media").RespondJson(medias);
        
    }

    [Fact]
    public void IndexFilterButtonShowsFilterOptions()
    {
        // Arrange
        var cut = _ctx.RenderComponent<Index>();
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
        Assert.Equal(6, countOfFilterOptions);
    }

    [Fact]
    public void IndexFilterButtonAgainHidesFilterOptions()
    {
        // Arrange
        var cut = _ctx.RenderComponent<Index>();
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
        var cut = _ctx.RenderComponent<Index>();
        var expected = 12;

        // Act
        var filterButton = cut.FindAll("button").GetElementById("filterButton");
        filterButton.Click();
        cut.FindAll("button").GetElementById("tags").Click();
        var actual = cut.FindAll("button").Count;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IndexHideSearchFieldForFilterWhenSelectingOptionRatingsAndShowRangeSlider()
    {
        // Arrange
        _ctx.JSInterop.SetupVoid("Radzen.createSlider", _ => true);
        var cut = _ctx.RenderComponent<Index>();
        var filterButton = cut.FindAll("button").GetElementById("filterButton");
        var searchFieldIsGone = true;
        var sliderIsGone = false;

        // Act
        filterButton.Click();
        cut.FindAll("button").GetElementById("ratings").Click();
        if (cut.FindAll("input").GetElementById("searchFitler") != null) searchFieldIsGone = false;
        if (cut.FindAll("div").GetElementById("slider") == null) sliderIsGone = true;

        // Assert
        Assert.True(searchFieldIsGone);
        Assert.False(sliderIsGone);
    }

    [Fact]
    public void IndexChangeSearchFieldForFilterWhenSelectingOptionLevels()
    {
        // Arrange
        
        var cut = _ctx.RenderComponent<Index>();
        var expected = 12;

        // Act
        var filterButton = cut.FindAll("button").GetElementById("filterButton");
        filterButton.Click();
        cut.FindAll("button").GetElementById("levels").Click();
        var actual = cut.FindAll("button").Count;
        
        // Assert
        Assert.Equal(expected,actual);
    }

    [Fact]
    public void IndexChangeSearchFieldForFilterWhenSelectingOptionProgramingLanguages()
    {
        // Arrange
        
        var cut = _ctx.RenderComponent<Index>();
        var expected = 13;

        // Act
        var filterButton = cut.FindAll("button").GetElementById("filterButton");
        filterButton.Click();
        cut.FindAll("button").GetElementById("programming Languages").Click();
        var actual = cut.FindAll("button").Count;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IndexChangeSearchFieldForFilterWhenSelectingOptionLanguagess()
    {
        // Arrange
        var cut = _ctx.RenderComponent<Index>();
        var expected = 11;

        // Act
        var filterButton = cut.FindAll("button").GetElementById("filterButton");
        filterButton.Click();
        cut.FindAll("button").GetElementById("languages").Click();
        var actual = cut.FindAll("button").Count;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IndexChangeSearchFieldForFilterWhenSelectingOptionMedias()
    {
        // Arrange
        var cut = _ctx.RenderComponent<Index>();
        var expected = 12;

        // Act
        var filterButton = cut.FindAll("button").GetElementById("filterButton");
        filterButton.Click();
        cut.FindAll("button").GetElementById("medias").Click();
        var actual = cut.FindAll("button").Count;

        // Assert
        Assert.Equal(expected, actual);
    }

    public void Dispose()
    {
        _ctx.Dispose();
    }
}
