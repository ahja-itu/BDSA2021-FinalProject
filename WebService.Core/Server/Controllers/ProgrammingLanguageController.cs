namespace WebService.Core.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ProgrammingLanguageController : ControllerBase
    {
        private readonly IProgrammingLanguageRespository _programmingLanguageRepository;

        public ProgrammingLanguageController(IProgrammingLanguageRespository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<ProgrammingLanguageDTO>>> Get()
        {
            var result = await _programmingLanguageRepository.ReadAsync();
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
