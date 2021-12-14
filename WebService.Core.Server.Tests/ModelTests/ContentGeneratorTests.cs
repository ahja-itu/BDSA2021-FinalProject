using System;
using System.Linq;

namespace WebService.Core.Server.Tests.ModelTests;
public class ContentGeneratorTests
{
	private ContentGenerator _gen = new ContentGenerator();
	public ContentGeneratorTests()
	{
	}


	[Fact]
	public void GenerateTitle_given_no_tags_should_return_success_false()
    {
		var tags = new List<CreateWeightedTagDTO>();

		var (ok, _) = _gen.GenerateTitle(tags);

		Assert.False(ok);
    }

	[Fact]
	public void GenerateTitle_given_tag_docker_should_return_title_containing_docker()
    {
		var tagName = "docker";
		var tags = CreateTags(tagName);
		Assert.Single(tags);

		var (ok, title) = _gen.GenerateTitle(tags);

		Assert.True(ok);
		Assert.Contains(tagName, title);
		Assert.True(tagName.Length < title.Length);
    }

	[Fact]
	public void GenerateTitle_given_tags_docker_kubernetes_return_title_containing_both()
    {
		var tagNames = new string[]
		{
			"docker",
			"kubernetes"
		};
		var tags = CreateTags(tagNames);

		var (ok, title) = _gen.GenerateTitle(tags);

		Assert.True(ok);
		Assert.True(tagNames.Sum(n => n.Length) < title.Length);
		foreach (var tag in tagNames)
        {
			Assert.Contains(tag, title);
        }

		Assert.Contains("docker and kubernetes", title);
    }

	[Fact]
	public void GenerateTitle_given_tags_docker_kubernetes_azure_return_title_containing_all_with_comma_and_and_separation()
    {
		var tagNames = new string[]
		{
			"docker",
			"kubernetes",
			"azure"
		};
		var tags = CreateTags(tagNames);

		var (ok, title) = _gen.GenerateTitle(tags);

		Assert.True(ok);
		Assert.True(tagNames.Sum(n => n.Length) < title.Length);
		Assert.Contains("docker, kubernetes and azure", title);
    }


	[Theory]
	[InlineData(ContentGenerator.Language.ENGLISH, "english")]
	[InlineData(ContentGenerator.Language.RUSSIAN, "russian")]
	[InlineData(ContentGenerator.Language.DANISH, "danish")]
	[InlineData(ContentGenerator.Language.ITALIAN, "italian")]
	public void LanguageToString_converts_correctly_from_language_to_string(ContentGenerator.Language lang, string expected)
    {
		var (ok, actual) = _gen.LanguageToString(lang);

		Assert.True(ok);
		Assert.Equal(expected, actual);
    }

	[Theory]
	[InlineData("danish", ContentGenerator.Language.DANISH)]
	[InlineData("english", ContentGenerator.Language.ENGLISH)]
	[InlineData("italian", ContentGenerator.Language.ITALIAN)]
	[InlineData("russian", ContentGenerator.Language.RUSSIAN)]
	public void StringToLanguage_converts_magical_strings_to_languages(string input, ContentGenerator.Language expected)
    {
		var (ok, actual) = _gen.StringToLanguage(input);

		Assert.True(ok);
		Assert.Equal(expected, actual);
    }


	[Theory]
	[InlineData("")]
	[InlineData("     ")]
	[InlineData("Volapyk")]
	[InlineData(null)]
	public void StringToLanguage_does_not_convert_wrong_magical_strings_to_languages(string input)
    {
		var (ok, _) = _gen.StringToLanguage(input);

		Assert.False(ok);
    }

	private static IList<CreateWeightedTagDTO> CreateTags(params string[] tagNames)
		=> Array.ConvertAll<string, CreateWeightedTagDTO>(tagNames, tag => new CreateWeightedTagDTO(tag, 0));
    
}
