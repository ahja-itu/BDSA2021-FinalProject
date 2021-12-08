namespace WebService.Core.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class SearchController : ControllerBase
    {
        private readonly ISearch _search;

        public SearchController(ISearch search)
        {
            _search = search;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ICollection<MaterialDTO>>> Post(SearchForm searchForm)
        {
            //var result = await _search.Search(searchForm);
            throw new NotImplementedException();
        }
    }
}
