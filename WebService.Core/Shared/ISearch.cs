namespace WebService.Core.Shared
{
    public interface ISearch
    {
        Task<ICollection<MaterialDTO>> Search(SearchForm searchForm);
    }
}
