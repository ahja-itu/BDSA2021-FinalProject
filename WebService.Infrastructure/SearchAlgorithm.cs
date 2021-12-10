using WebService.Core;

namespace WebService.Infrastructure
{
    public class SearchAlgorithm : ISearch
    {
        private readonly IMaterialRepository _repository;
        private readonly ITagRepository _tagRepository;

        private Dictionary<MaterialDTO,double> _map;

        public SearchAlgorithm(IMaterialRepository materialRepository, ITagRepository tagRepository)
        {
            _repository = materialRepository;
            _tagRepository = tagRepository;
            _map = new Dictionary<MaterialDTO, double>();
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

        private void SetScoreWeigthedTags(this MaterialDTO material, SearchForm searchform)
        {
                
        }    
           

        private void SetScoreRating()
        {
            
        }
        
        private void SetScoreLevel()
        {
            
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
