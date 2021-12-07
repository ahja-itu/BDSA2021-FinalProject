namespace WebService.Core.Server
{
    public class SearchAlgorithm : ISearch
    {
        public List<int> Search(List<string> inputWords, List<int> tagIDs, List<List<int>> filterIDs)
        {
            throw new NotImplementedException();

            //return new List<int>();
        }
        public Task<ICollection<MaterialDTO>> Search(SearchForm searchForm)
        {
            throw new NotImplementedException();
        }
    }
}
