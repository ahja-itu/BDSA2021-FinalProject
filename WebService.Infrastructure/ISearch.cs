namespace WebService.Infrastructure
{
    public interface ISearch
    {
        Task<ICollection<MaterialDTO>> SearchAsync(SearchForm searchForm);
    }
}
