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
        

        private Dictionary<MaterialDTO,float> _map;

        public SearchAlgorithm(IMaterialRepository materialRepository, ITagRepository tagRepository)
        {
            _repository = materialRepository;
            _tagRepository = tagRepository;
            _map = new Dictionary<MaterialDTO, float>();
        }

        public async Task<(Status, ICollection<MaterialDTO>)> Search(SearchForm searchForm)
        {
            searchForm = await AddTagsToSearchFromTextField(searchForm);
            var response = await _repository.ReadAsync(searchForm);
            var status = response.Item1;
            ICollection<MaterialDTO> materials = new List<MaterialDTO>(response.Item2);

            if (status == Status.NotFound) return (Status.NotFound,materials);

            materials = FilterLanguage(materials, searchForm);

            materials = PrioritizeMaterials(materials);

            return (Status.Found, materials);
        }
        public async Task<SearchForm> AddTagsToSearchFromTextField(SearchForm searchForm)
        {
            var tags = await _tagRepository.ReadAsync();
            var foundWordsToTags = new List<TagDTO>(searchForm.Tags);

            foreach(string word in searchForm.TextField.Split(" "))
            {
                if (tags.Select(e => e.Name).Contains(word)) foundWordsToTags.Add(tags.Where(e => e.Name == word).First());
            }
            searchForm.Tags = foundWordsToTags;
            return searchForm;
        }

        private ICollection<MaterialDTO> PrioritizeMaterials(ICollection<MaterialDTO> materials)
        {
            //TODO this
            return materials;
        }

        private ICollection<MaterialDTO> FilterLanguage(ICollection<MaterialDTO> materials, SearchForm searchForm)
        {          
            if (!searchForm.Languages.Any()) return materials;

            return materials.Where(m => searchForm.Languages.Contains(m.Language)).ToList();
        }

        private void SetScoreWeigthedTags( SearchForm searchform)
        {
            foreach (MaterialDTO material in _map.Keys){
            foreach(CreateWeightedTagDTO tag in material.Tags){
                foreach(TagDTO searchTag in searchform.Tags){
                    if(tag.Name == searchTag.Name){
                        _map[material]+= tag.Weight*WeightedTagScore;
                    }
                }
            }       
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
            foreach (MaterialDTO material in _map.Keys)
            {
            foreach(CreateLevelDTO level in material.Levels){
                foreach(LevelDTO searchLevel in searchform.Levels){
                    if(level == searchLevel){
                        _map[material]+= LevelScore;
                    }
                }
            } 
        }

        private void SetScoreProgrammingLanguage(SearchForm searchform)
        {
              foreach(CreateProgrammingLanguageDTO level in entry.Key.ProgrammingLanguages){
                foreach(LevelDTO searchLevel in searchform.Levels){
                    if(level == searchLevel){
                        _map[entry.Key]+= LevelScore;
                    }
                }
            } 

            
        }
        
        private void SetScoreMedia()
        {
            
        }

        private void SetScoreTitle()
        {
            
        }
        
        private void SetScoreAuthor()
        {
            
        }

        private void SetScoreTimestamp()
        {

        }
     
    }
}
