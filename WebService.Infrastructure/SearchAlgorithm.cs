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
            var databaseMaterials = await FindMaterials(SearchFormParse(searchForm));

            if (databaseMaterials.Item1 == Status.NotFound)
            {
                return databaseMaterials;
            };
            
            ICollection<MaterialDTO> materialDTOs = new List<MaterialDTO>(FilterLanguage(databaseMaterials.Item2, searchForm));

            return (Status.Found, PrioritizeMaterials(materialDTOs));
        }

        public SearchForm SearchFormParse(SearchForm searchForm)
        {
            return TextFieldParse(searchForm);
        }

        public async Task<SearchForm> TextFieldParse(SearchForm searchForm)
        {
            var tags = await _tagRepository.ReadAsync();
            var foundWordsToTags = new List<TagDTO>(searchForm.Tags);

            foreach(string word in searchForm.TextField.Split(" "))
            {
                if (tags.Select(e => e.Name).Contains(word)) foundWordsToTags.Add(tags.Where(e => e.Name == word).First());
            }
            searchForm.Tags.Join(foundWordsToTags);
            return searchForm;
        }

        private async Task<(Status, ICollection<MaterialDTO>)> FindMaterials(SearchForm searchForm)
        {
            var result = _repository.ReadAsync(searchForm).Result;

            return (result.Item1, new List<MaterialDTO>(result.Item2));
        }

        private IEnumerable<MaterialDTO> PrioritizeMaterials(IEnumerable<MaterialDTO> materials)
        {
            //TODO this
            return materials;
        }

        private IEnumerable<MaterialDTO> FilterLanguage(ICollection<MaterialDTO> materials, SearchForm searchForm)
        {
           
            if (!searchForm.Languages.Any()) return materials;

            IEnumerable<MaterialDTO> filtered = materials.Where(m => searchForm.Languages.Contains(m.Language));

            return filtered;
        }

        private void SetScoreWeigthedTags(this KeyValuePair<MaterialDTO, float> entry, SearchForm searchform)
        {
            foreach(CreateWeightedTagDTO tag in entry.Key.Tags){
                foreach(TagDTO searchTag in searchform.Tags){
                    if(tag.Name == searchTag.Name){
                        _map[entry.Key]+= tag.Weight*WeightedTagScore;
                    }
                }
            }       
        }    
           

        private void SetScoreRating(this KeyValuePair<MaterialDTO, float> entry, SearchForm searchform)
        {
            float averageRating = entry.Key.AverageRating;
            _map[entry.Key] = entry.Value + averageRating * RatingScore;
        }
        
        private void SetScoreLevel(this KeyValuePair<MaterialDTO, float> entry, SearchForm searchform)
        {
            foreach(CreateLevelDTO level in entry.Key.Levels){
                foreach(LevelDTO searchLevel in searchform.Levels){
                    if(level == searchLevel){
                        _map[entry.Key]+= LevelScore;
                    }
                }
            } 
        }

        private void SetScoreProgrammingLanguage()
        {
            
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
