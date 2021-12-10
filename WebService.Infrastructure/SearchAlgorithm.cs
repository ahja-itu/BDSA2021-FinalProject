using WebService.Core;

namespace WebService.Infrastructure
{
    public class SearchAlgorithm : ISearch
    {
        private readonly IMaterialRepository _repository;
        private SearchForm _searchForm; //SPØRG

        public SearchAlgorithm(IMaterialRepository materialRepository)
        {
            _repository = materialRepository;
        }

        public async Task<(Status, ICollection<MaterialDTO>)> Search(SearchForm searchForm)
        {
            _searchForm = searchForm;
            var databaseMaterials = FindMaterials(SearchFormParse(_searchForm));
            if (databaseMaterials.Result.Item1 == Status.NotFound)
            {
                return databaseMaterials.Result;
            };

            var materialDTOs = FilterMaterials(databaseMaterials.Result.Item2);
            return (Status.Found, PrioritizeMaterials(materialDTOs));
        }

        public SearchForm SearchFormParse(SearchForm searchForm)
        {
            return TextFieldParse(searchForm);
        }

        public SearchForm TextFieldParse(SearchForm searchForm)
        {
            return searchForm;
            //string[] text = searchForm.TextField.Split(' ');
        }

        private async Task<(Status, ICollection<MaterialDTO>)> FindMaterials(SearchForm searchForm)
        {
            var result = _repository.ReadAsync(searchForm).Result;

            return (result.Item1, new List<MaterialDTO>(result.Item2));
        }

        private ICollection<MaterialDTO> FilterMaterials(ICollection<MaterialDTO> materials)
        {
            materials = FilterLanguage(materials);
            materials = FilterTags(materials);
            return materials;
        }


        private ICollection<MaterialDTO> PrioritizeMaterials(ICollection<MaterialDTO> materials)
        {
            //TODO this
            return materials;
        }

        private ICollection<MaterialDTO> FilterLanguage(ICollection<MaterialDTO> materials)
        {
           
            if (!_searchForm.Languages.Any()) { return materials;};

            List<MaterialDTO> temp = new List<MaterialDTO>();
            
            foreach (MaterialDTO m in materials)
            {
                if (_searchForm.Languages.Contains(m.Language))
                {
                    temp.Add(m);
                }
            }

            return temp;
        }

        private ICollection<MaterialDTO> FilterTags(ICollection<MaterialDTO> materials)
        {
            if (!_searchForm.Tags.Any()) { return materials;};

            List<MaterialDTO> temp = new List<MaterialDTO>();
            materialLoop:
            foreach (MaterialDTO m in materials)
            {  
                foreach (WeightedTagDTO wt in m.Tags)
                {
                    foreach (TagDTO t in _searchForm.Tags)
                    {
                        if (wt.Name == t.Name)
                        {
                            temp.Add(m);
                            goto materialLoop;
                        }
                    }
                }
  
            }                
            return temp;
        }
    }
}
