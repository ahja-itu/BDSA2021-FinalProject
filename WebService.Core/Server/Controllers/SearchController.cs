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
            //throw new NotImplementedException();
            
            if (searchForm.TextField == "search")
            {
                return new List<MaterialDTO>
                {
                    new(
                        0,
                        new List<CreateWeightedTagDTO>(),
                        new List<CreateRatingDTO>(),
                        new List<CreateLevelDTO>(),
                        new List<CreateProgrammingLanguageDTO>(),
                        new List<CreateMediaDTO>(),
                        new LanguageDTO(0,"English"),
                        "search",
                        "search.com",
                        "search",
                        "search",
                        new List<CreateAuthorDTO>(),
                        DateTime.Now
                    )
                };
            }

            return new List<MaterialDTO>
            {
                new(
                    1,
                    new List<CreateWeightedTagDTO>(),
                    new List<CreateRatingDTO>(),
                    new List<CreateLevelDTO>(),
                    new List<CreateProgrammingLanguageDTO>(),
                    new List<CreateMediaDTO>(),
                    new LanguageDTO(0,"English"),
                    "no-search",
                    "no-search.com",
                    "no-search",
                    "no-search",
                    new List<CreateAuthorDTO>(),
                    DateTime.Now
                )
            };

        }
    }
}
