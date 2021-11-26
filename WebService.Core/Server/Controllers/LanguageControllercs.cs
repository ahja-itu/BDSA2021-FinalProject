namespace WebService.Core.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[api/controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository _languageRespository;

        public LanguageController(ILanguageRepository languageRepository)
        {
            _languageRespository = languageRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LanguageDTO>>> Get()
        {
            var result = await _languageRespository.ReadAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Task<ActionResult<TagDTO>> Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public Task<IActionResult> Post(CreateTagDTO tag)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public void Put([FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
