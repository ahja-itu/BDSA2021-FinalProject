namespace WebService.Core.Server
{
    public class SearchAlgorithm
    {
        public List<MaterialDTO> Search(SearchForm searchform)
        {
            throw new NotImplementedException();

            //return new List<int>();
        }

        private SearchForm SearchFormParse(SearchForm searchForm)
        {
            //TextFieldParse(searchForm);
            throw new NotImplementedException();
        }
        
        private SearchForm TextFieldParse(SearchForm searchForm)
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
