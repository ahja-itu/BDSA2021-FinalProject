using System;
using System.Linq;
using WebService.Core.Server.Model;

namespace WebService.Core.Server.Tests.ModelTests;

public class ContentGeneratorTests
{
    [Fact]
    public void GenerateTitle_given_no_tags_should_return_success_false()
    {
        var tags = new List<CreateWeightedTagDTO>();

        var (ok, _) = ContentGenerator.GenerateTitle(tags);

        Assert.False(ok);
    }

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

    private static IList<CreateWeightedTagDTO> CreateTags(params string[] tagNames)
    {
        return Array.ConvertAll(tagNames, tag => new CreateWeightedTagDTO(tag, 0));
    }
}