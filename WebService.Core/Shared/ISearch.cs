namespace WebService.Core.Shared
{
    public interface ISearch
    {
        Task<(Status,ICollection<MaterialDTO>)> Search(SearchForm searchForm);
    }
}
