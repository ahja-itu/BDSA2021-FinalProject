namespace WebService.Infrastructure;

public class SearchAlgorithm : ISearch
{
    private const float WeightedTagScore = 10;
    private const float RatingScore = 10;
    private const float LevelScore = 50;
    private const float ProgrammingLanguageScore = 100;
    private const float MediaScore = 50;
    private const float TitleScore = 300;
    private const float AuthorScore = 300;
    private const float TimestampScore = -5;

    private readonly ConcurrentDictionary<MaterialDTO, float> _map;
    private readonly IMaterialRepository _repository;
    private readonly ITagRepository _tagRepository;

    public SearchAlgorithm(IMaterialRepository materialRepository, ITagRepository tagRepository)
    {
        _repository = materialRepository;
        _tagRepository = tagRepository;
        _map = new ConcurrentDictionary<MaterialDTO, float>();
    }

    public async Task<(Status, ICollection<MaterialDTO>)> Search(SearchForm searchForm)
    {
        searchForm.TextField = searchForm.TextField.Replace(",", "");
        searchForm = await AddTagsToSearchFromTextField(searchForm);
        var (status, materialDTOs) = await _repository.ReadAsync(searchForm);
        ICollection<MaterialDTO> materials = new List<MaterialDTO>(materialDTOs);

        if (status == Status.NotFound) return (Status.NotFound, materials);

        materials = FilterLanguage(materials, searchForm);

        if (!materials.Any()) return (Status.NotFound, materials);

        foreach (var material in materials) _map[material] = 0;

        PrioritizeMaterials(searchForm);

        materials = _map.OrderByDescending(e => e.Value).ThenBy(e => e.Key.Title).Select(e => e.Key).ToList();

        return (Status.Found, materials);
    }

    private async Task<SearchForm> AddTagsToSearchFromTextField(SearchForm searchForm)
    {
        var tags = await _tagRepository.ReadAsync();
        var foundWordsToTags = new HashSet<TagDTO>(searchForm.Tags);

        foreach (var word in searchForm.TextField.Split(" "))
            if (tags.Select(e => e.Name).ContainsIgnoreCasing(word))
                foundWordsToTags.Add(tags.First(e => e.Name.IsEqualIgnoreCasing(word)));
        searchForm.Tags = foundWordsToTags.ToList();
        return searchForm;
    }

    private void PrioritizeMaterials(SearchForm searchForm)
    {
        Parallel.Invoke(
            SetScoreRating,
            () => SetScoreAuthor(searchForm),
            () => SetScoreLevel(searchForm),
            () => SetScoreMedia(searchForm),
            () => SetScoreProgrammingLanguage(searchForm),
            SetScoreTimestamp,
            () => SetScoreTitle(searchForm),
            () => SetScoreWeightedTags(searchForm)
        );
    }

    private static ICollection<MaterialDTO> FilterLanguage(ICollection<MaterialDTO> materials, SearchForm searchForm)
    {
        if (!searchForm.Languages.Any()) return materials;

        return materials.Where(m => searchForm.Languages.Select(e => e.Name).ContainsIgnoreCasing(m.Language.Name))
            .ToList();
    }

    private void SetScoreWeightedTags(SearchForm searchForm)
    {
        foreach (var material in _map.Keys)
        {
            var weightSum = material.Tags
                .Where(materialTag => searchForm.Tags.Select(searchFormTag => searchFormTag.Name)
                    .ContainsIgnoreCasing(materialTag.Name)).ToList().Select(tag => tag.Weight).Sum();
            var tagCount = material.Tags
                .Count(materialTag => searchForm.Tags.Select(searchFormTag => searchFormTag.Name)
                    .ContainsIgnoreCasing(materialTag.Name));
            UpdateMap(material, weightSum * WeightedTagScore * tagCount);
        }
    }

    private void SetScoreRating()
    {
        foreach (var material in _map.Keys) UpdateMap(material, material.AverageRating() * RatingScore);
    }

    private void SetScoreLevel(SearchForm searchForm)
    {
        foreach (var material in _map.Keys)
        {
            var count = material.Levels
                .Count(e => searchForm.Levels.Select(levelDTO => levelDTO.Name).ContainsIgnoreCasing(e.Name));
            UpdateMap(material, count * LevelScore);
        }
    }

    private void SetScoreProgrammingLanguage(SearchForm searchForm)
    {
        foreach (var material in _map.Keys)
        {
            var count = material.ProgrammingLanguages.Count(e =>
                searchForm.ProgrammingLanguages.Select(programmingLanguageDTO => programmingLanguageDTO.Name)
                    .ContainsIgnoreCasing(e.Name));
            UpdateMap(material, count * ProgrammingLanguageScore);
        }
    }

    private void SetScoreMedia(SearchForm searchForm)
    {
        foreach (var material in _map.Keys)
        {
            var count = material.Medias
                .Count(e => searchForm.Medias.Select(mediaDTO => mediaDTO.Name).ContainsIgnoreCasing(e.Name));
            UpdateMap(material, count * MediaScore);
        }
    }

    private void SetScoreTitle(SearchForm searchForm)
    {
        foreach (var material in _map.Keys)
        {
            float wordCount = 0;
            float textFieldCount = searchForm.TextField.Split(" ").Length;
            foreach (var word in material.Title.Split(" "))
                if (searchForm.TextField.ContainsIgnoreCasing(word))
                    wordCount++;
            UpdateMap(material, wordCount / textFieldCount * TitleScore);
        }
    }

    private void SetScoreAuthor(SearchForm searchForm)
    {
        foreach (var material in _map.Keys)
        {
            var authorNameCount = 0;

            foreach (var author in material.Authors)
            {
                if (searchForm.TextField.ContainsIgnoreCasing(author.FirstName)) authorNameCount++;

                if (searchForm.TextField.ContainsIgnoreCasing(author.SurName)) authorNameCount++;
            }

            UpdateMap(material, authorNameCount * AuthorScore);
        }
    }

    private void SetScoreTimestamp()
    {
        foreach (var material in _map.Keys)
        {
            var yearDifference = DateTime.UtcNow.Year - material.TimeStamp.Year;
            UpdateMap(material, yearDifference * TimestampScore);
        }
    }

    private void UpdateMap(MaterialDTO key, float addValue)
    {
        _map.AddOrUpdate(key, 0, (_, current) => current + addValue);
    }
}