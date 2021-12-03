

namespace WebService.Infrastructure
{
    public class SearchAlgorithm
    {
        public List<MaterialDTO> Search(SearchForm searchform)
        {
            throw new NotImplementedException();

            //return new List<int>();
        }

        public SearchForm SearchFormParse(SearchForm searchForm)
        {
            //TextFieldParse(searchForm);
            throw new NotImplementedException();
        }
        
        public SearchForm TextFieldParse(SearchForm searchForm)
        {
            throw new NotImplementedException();
            string[] text = searchForm.TextField.Split(' ');
        }

        private List<MaterialDTO> FindMaterials(SearchForm searchForm)
        {
            throw new NotImplementedException();
        }

        private List<MaterialDTO> PrioritizeMaterials(List<MaterialDTO> materials)
        {
            throw new NotImplementedException();
        }
    }
}
