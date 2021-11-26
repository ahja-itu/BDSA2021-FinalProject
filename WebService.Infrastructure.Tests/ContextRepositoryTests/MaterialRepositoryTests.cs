namespace WebService.Infrastructure.Tests.ContextRepositoryTests
{
    public class MaterialRepositoryTests
    {
        private readonly TestVariables _v;

        public MaterialRepositoryTests()
        {
            _v = new TestVariables();
        }

        [Fact]
        public async Task Test()
        {
            var material = new CreateMaterialDTO(new List<TagDTO> { new TagDTO(1, "SOLID", 10) }, new List<RatingDTO> { new RatingDTO(1, 2) }, new List<LevelDTO> { new LevelDTO(1, "Bachelor") }, new List<ProgrammingLanguageDTO> { new ProgrammingLanguageDTO(1, "C#") }, new List<MediaDTO> { new MediaDTO(1, "Book") }, new LanguageDTO(1, "Danish"), null, "Title", new List<AuthorDTO>() { new  AuthorDTO(1,"Rasmus","Kristensen")},DateTime.UtcNow);
            var actual = _v.MaterialRepository.CreateAsync(material);

        }
    }
}
