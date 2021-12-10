using System;
using System.Linq;

namespace WebService.Core.Server.Tests.ModelTests;
public class ContentGeneratorTests
{
	public ContentGeneratorTests()
	{
	}


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
		var tagName = "docker";
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
		var tagNames = new string[]
		{
			"docker",
			"kubernetes"
		};
		var tags = CreateTags(tagNames);

		var (ok, title) = ContentGenerator.GenerateTitle(tags);

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

		var (ok, title) = ContentGenerator.GenerateTitle(tags);

		Assert.True(ok);
		Assert.True(tagNames.Sum(n => n.Length) < title.Length);
		Assert.Contains("docker, kubernetes and azure", title);
    }

	private static IList<CreateWeightedTagDTO> CreateTags(params string[] tagNames)
		=> Array.ConvertAll<string, CreateWeightedTagDTO>(tagNames, tag => new CreateWeightedTagDTO(tag, 0));
    
}
