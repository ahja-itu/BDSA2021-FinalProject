using WebService.Core;

namespace WebService.Infrastructure
{
    public class SearchAlgorithm : ISearch
    {
        private readonly IMaterialRepository _repository;
        private readonly ITagRepository _tagRepository;
        private const float WeightedTagScore = 10;
        private const float RatingScore = 10;
        private const float LevelScore = 50;
        private const float ProgrammingLanguageScore = 100;
        private const float MediaScore = 50;
        private const float TitleScore = 300;
        private const float AuthorScore = 300;
        private const float TimestampScore = -5;


        private ConcurrentDictionary<MaterialDTO, float> _map;

        public SearchAlgorithm(IMaterialRepository materialRepository, ITagRepository tagRepository)
        {
            _repository = materialRepository;
            _tagRepository = tagRepository;
            _map = new ConcurrentDictionary<MaterialDTO, float>();
        }

        public async Task<(Status, ICollection<MaterialDTO>)> Search(SearchForm searchForm)
        {
            searchForm = FixTextFieldInput(searchForm);

            searchForm = await AddTagsToSearchFromTextField(searchForm);
            var response = await _repository.ReadAsync(searchForm);
            var status = response.Item1;
            ICollection<MaterialDTO> materials = new List<MaterialDTO>(response.Item2);

            if (status == Status.NotFound) return (Status.NotFound, materials);

            materials = FilterLanguage(materials, searchForm);

            if (!materials.Any()) return (Status.NotFound, materials);

            foreach (MaterialDTO material in materials)
            {
                _map[material] = 0;
            }

            PrioritizeMaterials(searchForm);

            materials = _map.OrderByDescending(e => e.Value).ThenBy(e => e.Key.Title).Select(e => e.Key).ToList();

            return (Status.Found, materials);
        }

        private SearchForm FixTextFieldInput(SearchForm searchForm)
        {
            string textField = "";
            if (string.IsNullOrEmpty(textField) || string.IsNullOrWhiteSpace(textField)) return searchForm;
            foreach (string word in searchForm.TextField.Split(" "))
            {
                textField += FirstLetterToUpper(word);
            }
            searchForm.TextField = textField;
            return searchForm;
        }

        private string FirstLetterToUpper(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1).ToLower();
        }

        public async Task<SearchForm> AddTagsToSearchFromTextField(SearchForm searchForm)
        {
            var tags = await _tagRepository.ReadAsync();
            var foundWordsToTags = new HashSet<TagDTO>(searchForm.Tags);

            foreach (string word in searchForm.TextField.Split(" "))
            {
                if (tags.Select(e => e.Name).Contains(word)) foundWordsToTags.Add(tags.Where(e => e.Name == word).First());
            }
            searchForm.Tags = foundWordsToTags.ToList();
            return searchForm;
        }

        private void PrioritizeMaterials(SearchForm searchForm)
        {
            Parallel.Invoke(
                () => SetScoreRating(),
                () => SetScoreAuthor(searchForm),
                () => SetScoreLevel(searchForm),
                () => SetScoreMedia(searchForm),
                () => SetScoreProgrammingLanguage(searchForm),
                () => SetScoreTimestamp(),
                () => SetScoreTitle(searchForm),
                () => SetScoreWeigthedTags(searchForm)
            );

        }

        private ICollection<MaterialDTO> FilterLanguage(ICollection<MaterialDTO> materials, SearchForm searchForm)
        {
            if (!searchForm.Languages.Any()) return materials;

            return materials.Where(m => searchForm.Languages.Select(e => e.Name).Contains(m.Language.Name)).ToList();

        }

        private void SetScoreWeigthedTags(SearchForm searchform)
        {
            foreach (var material in _map.Keys)
            {
                var weightSum = material.Tags.Where(materialTag => searchform.Tags.Select(searchformTag => searchformTag.Name).Contains(materialTag.Name)).ToList().Select(tag => tag.Weight).Sum();
                _map[material] += weightSum * WeightedTagScore;
                
            }
        }
        private void SetScoreRating()
        {
            foreach (MaterialDTO material in _map.Keys)
            {
                _map[material] += material.AverageRating() * RatingScore;
            }

        }

        private void SetScoreLevel(SearchForm searchform)
        {
            foreach (var material in _map.Keys)
            {
                var count = material.Levels.Where(e => searchform.Levels.Select(e => e.Name).Contains(e.Name)).Count();
                _map[material] += count * LevelScore;
            }
        }

        private void SetScoreProgrammingLanguage(SearchForm searchform)
        {
            foreach (var material in _map.Keys)
            {
                var count = material.ProgrammingLanguages.Where(e => searchform.ProgrammingLanguages.Select(e => e.Name).Contains(e.Name)).Count();
                _map[material] += count * ProgrammingLanguageScore;
            }
        }

        private void SetScoreMedia(SearchForm searchform)
        {
            foreach (var material in _map.Keys)
            {
                var count = material.Medias.Where(e => searchform.Medias.Select(e => e.Name).Contains(e.Name)).Count();
                _map[material] += count * MediaScore;
            }
        }

        private void SetScoreTitle(SearchForm searchForm)
        {
            foreach (MaterialDTO material in _map.Keys)
            {
                var wordCount = 0;
                var textFieldCount = searchForm.TextField.Split(" ").Count();
                foreach (var word in material.Title.Split(" "))
                {
                    if (searchForm.TextField.Contains(word)) wordCount++;
                }
                _map[material] += wordCount / textFieldCount * TitleScore;
            }
        }

        private void SetScoreAuthor(SearchForm searchForm)
        {
            foreach (MaterialDTO material in _map.Keys)
            {
                var authorNameCount = 0;

                foreach (var author in material.Authors)
                {
                    if (searchForm.TextField.Contains(author.FirstName) || searchForm.TextField.Contains(author.SurName)) authorNameCount++;
                }
                _map[material] += authorNameCount * AuthorScore;
            }
        }

        private void SetScoreTimestamp()
        {
            foreach (var material in _map.Keys)
            {
                var yearDifferece = DateTime.UtcNow.Year - material.TimeStamp.Year;
                _map[material] += yearDifferece * TimestampScore;
            }
        }

    }
}
