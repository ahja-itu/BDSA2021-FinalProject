// ***********************************************************************
// Assembly         : WebService.Infrastructure
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="SearchAlgorithm.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Infrastructure;

/// <summary>
///     Class SearchAlgorithm.
///     Implements the <see cref="WebService.Core.Shared.ISearch" />
/// </summary>
/// <seealso cref="WebService.Core.Shared.ISearch" />
public class SearchAlgorithm : ISearch
{
    /// <summary>
    ///     The weighted tag score
    /// </summary>
    private const float WeightedTagScore = 10;

    /// <summary>
    ///     The rating score
    /// </summary>
    private const float RatingScore = 10;

    /// <summary>
    ///     The level score
    /// </summary>
    private const float LevelScore = 50;

    /// <summary>
    ///     The programming language score
    /// </summary>
    private const float ProgrammingLanguageScore = 100;

    /// <summary>
    ///     The media score
    /// </summary>
    private const float MediaScore = 50;

    /// <summary>
    ///     The title score
    /// </summary>
    private const float TitleScore = 300;

    /// <summary>
    ///     The author score
    /// </summary>
    private const float AuthorScore = 300;

    /// <summary>
    ///     The timestamp score
    /// </summary>
    private const float TimestampScore = -5;

    /// <summary>
    ///     The map
    /// </summary>
    private readonly ConcurrentDictionary<MaterialDTO, float> _map;

    /// <summary>
    ///     The repository
    /// </summary>
    private readonly IMaterialRepository _repository;

    /// <summary>
    ///     The tag repository
    /// </summary>
    private readonly ITagRepository _tagRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SearchAlgorithm" /> class.
    /// </summary>
    /// <param name="materialRepository">The material repository.</param>
    /// <param name="tagRepository">The tag repository.</param>
    public SearchAlgorithm(IMaterialRepository materialRepository, ITagRepository tagRepository)
    {
        _repository = materialRepository;
        _tagRepository = tagRepository;
        _map = new ConcurrentDictionary<MaterialDTO, float>();
    }

    /// <summary>
    ///     Searches the specified search form and gets matching material.
    /// </summary>
    /// <param name="searchForm">The search form.</param>
    /// <returns>Task&lt;System.ValueTuple&lt;Status, ICollection&lt;MaterialDTO&gt;&gt;&gt;.</returns>
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

    /// <summary>
    ///     Adds the tags to search from text field.
    /// </summary>
    /// <param name="searchForm">The search form.</param>
    /// <returns>SearchForm.</returns>
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

    /// <summary>
    ///     Prioritizes the materials.
    /// </summary>
    /// <param name="searchForm">The search form.</param>
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

    /// <summary>
    ///     Filters the language.
    /// </summary>
    /// <param name="materials">The materials.</param>
    /// <param name="searchForm">The search form.</param>
    /// <returns>ICollection&lt;MaterialDTO&gt;.</returns>
    private static ICollection<MaterialDTO> FilterLanguage(ICollection<MaterialDTO> materials, SearchForm searchForm)
    {
        if (!searchForm.Languages.Any()) return materials;

        return materials.Where(m => searchForm.Languages.Select(e => e.Name).ContainsIgnoreCasing(m.Language.Name))
            .ToList();
    }

    /// <summary>
    ///     Sets the score for weighted tags.
    /// </summary>
    /// <param name="searchForm">The search form.</param>
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

    /// <summary>
    ///     Sets the score for rating.
    /// </summary>
    private void SetScoreRating()
    {
        foreach (var material in _map.Keys) UpdateMap(material, material.AverageRating() * RatingScore);
    }

    /// <summary>
    ///     Sets the score for level.
    /// </summary>
    /// <param name="searchForm">The search form.</param>
    private void SetScoreLevel(SearchForm searchForm)
    {
        foreach (var material in _map.Keys)
        {
            var count = material.Levels
                .Count(e => searchForm.Levels.Select(levelDTO => levelDTO.Name).ContainsIgnoreCasing(e.Name));
            UpdateMap(material, count * LevelScore);
        }
    }

    /// <summary>
    ///     Sets the score for programming language.
    /// </summary>
    /// <param name="searchForm">The search form.</param>
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

    /// <summary>
    ///     Sets the score for media.
    /// </summary>
    /// <param name="searchForm">The search form.</param>
    private void SetScoreMedia(SearchForm searchForm)
    {
        foreach (var material in _map.Keys)
        {
            var count = material.Medias
                .Count(e => searchForm.Medias.Select(mediaDTO => mediaDTO.Name).ContainsIgnoreCasing(e.Name));
            UpdateMap(material, count * MediaScore);
        }
    }

    /// <summary>
    ///     Sets the score for title.
    /// </summary>
    /// <param name="searchForm">The search form.</param>
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

    /// <summary>
    ///     Sets the score for author.
    /// </summary>
    /// <param name="searchForm">The search form.</param>
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

    /// <summary>
    ///     Sets the score for timestamp.
    /// </summary>
    private void SetScoreTimestamp()
    {
        foreach (var material in _map.Keys)
        {
            var yearDifference = DateTime.UtcNow.Year - material.TimeStamp.Year;
            UpdateMap(material, yearDifference * TimestampScore);
        }
    }

    /// <summary>
    ///     Updates the map.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="addValue">The add value.</param>
    private void UpdateMap(MaterialDTO key, float addValue)
    {
        _map.AddOrUpdate(key, 0, (_, current) => current + addValue);
    }
}