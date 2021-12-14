// ***********************************************************************
// Assembly         : WebService.Core.Server.Tests
// Author           : Group BTG
// Created          : 12-14-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="ContentGeneratorTests.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using WebService.Core.Server.Model;

namespace WebService.Core.Server.Tests.ModelTests;

/// <summary>
/// Class ContentGeneratorTests.
/// </summary>
public class ContentGeneratorTests
{
    /// <summary>
    /// Defines the test method GenerateTitle_given_no_tags_should_return_success_false.
    /// </summary>
    [Fact]
    public void GenerateTitle_given_no_tags_should_return_success_false()
    {
        var tags = new List<CreateWeightedTagDTO>();

        var (ok, _) = ContentGenerator.GenerateTitle(tags);

        Assert.False(ok);
    }

    /// <summary>
    /// Defines the test method GenerateTitle_given_tag_docker_should_return_title_containing_docker.
    /// </summary>
    [Fact]
    public void GenerateTitle_given_tag_docker_should_return_title_containing_docker()
    {
        const string tagName = "docker";
        var tags = CreateTags(tagName);
        Assert.Single(tags);

        var (ok, title) = ContentGenerator.GenerateTitle(tags);

        Assert.True(ok);
        Assert.Contains(tagName, title);
        Assert.True(tagName.Length < title.Length);
    }

    /// <summary>
    /// Defines the test method GenerateTitle_given_tags_docker_kubernetes_return_title_containing_both.
    /// </summary>
    [Fact]
    public void GenerateTitle_given_tags_docker_kubernetes_return_title_containing_both()
    {
        var tagNames = new[]
        {
            "docker",
            "kubernetes"
        };
        var tags = CreateTags(tagNames);

        var (ok, title) = ContentGenerator.GenerateTitle(tags);

        Assert.True(ok);
        Assert.True(tagNames.Sum(n => n.Length) < title.Length);
        foreach (var tag in tagNames) Assert.Contains(tag, title);

        Assert.Contains("docker and kubernetes", title);
    }

    /// <summary>
    /// Defines the test method GenerateTitle_given_tags_docker_kubernetes_azure_return_title_containing_all_with_comma_and_and_separation.
    /// </summary>
    [Fact]
    public void
        GenerateTitle_given_tags_docker_kubernetes_azure_return_title_containing_all_with_comma_and_and_separation()
    {
        var tagNames = new[]
        {
            "docker",
            "kubernetes",
            "azure"
        };
        var tags = CreateTags(tagNames);

        var (ok, title) = ContentGenerator.GenerateTitle(tags);

        Assert.True(ok);
        Assert.True(tagNames.Sum(n => n.Length) < title.Length);
        Assert.Contains("docker, kubernetes and azure", title);
    }


    /// <summary>
    /// Defines the test method LanguageToString_converts_correctly_from_language_to_string.
    /// </summary>
    /// <param name="lang">The language.</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData(ContentGenerator.Language.English, "english")]
    [InlineData(ContentGenerator.Language.Russian, "russian")]
    [InlineData(ContentGenerator.Language.Danish, "danish")]
    [InlineData(ContentGenerator.Language.Italian, "italian")]
    public void LanguageToString_converts_correctly_from_language_to_string(ContentGenerator.Language lang,
        string expected)
    {
        var (ok, actual) = ContentGenerator.LanguageToString(lang);

        Assert.True(ok);
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Defines the test method StringToLanguage_converts_magical_strings_to_languages.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData("danish", ContentGenerator.Language.Danish)]
    [InlineData("english", ContentGenerator.Language.English)]
    [InlineData("italian", ContentGenerator.Language.Italian)]
    [InlineData("russian", ContentGenerator.Language.Russian)]
    public void StringToLanguage_converts_magical_strings_to_languages(string input, ContentGenerator.Language expected)
    {
        var (ok, actual) = ContentGenerator.StringToLanguage(input);

        Assert.True(ok);
        Assert.Equal(expected, actual);
    }


    /// <summary>
    /// Defines the test method StringToLanguage_does_not_convert_wrong_magical_strings_to_languages.
    /// </summary>
    /// <param name="input">The input.</param>
    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData("Volapük")]
    [InlineData(null)]
    public void StringToLanguage_does_not_convert_wrong_magical_strings_to_languages(string input)
    {
        var (ok, _) = ContentGenerator.StringToLanguage(input);

        Assert.False(ok);
    }

    /// <summary>
    /// Creates the tags.
    /// </summary>
    /// <param name="tagNames">The tag names.</param>
    /// <returns>IList&lt;CreateWeightedTagDTO&gt;.</returns>
    private static IList<CreateWeightedTagDTO> CreateTags(params string[] tagNames)
    {
        return Array.ConvertAll(tagNames, tag => new CreateWeightedTagDTO(tag, 0));
    }
}