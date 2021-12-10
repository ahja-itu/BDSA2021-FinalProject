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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<ProgrammingLanguageDTO>>> Get()
        {
            var result = await _programmingLanguageRepository.ReadAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProgrammingLanguageDTO>> Get(int id)
        {
            var result = await _programmingLanguageRepository.ReadAsync(id);
            var response = result.Item1;

            if (response == Status.Found) return Ok(result.Item2);
            else return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(CreateProgrammingLanguageDTO programmingLanguage)
        {
            var result = await _programmingLanguageRepository.CreateAsync(programmingLanguage);
            var response = result.Item1;

            if (response == Status.Created) return Created(nameof(Put), result.Item2);
            else if (response == Status.Conflict) return Conflict();
            else return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Put(ProgrammingLanguageDTO programmingLanguage)
        {
            var response = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);

            if (response == Status.Updated) return NoContent();
            else if (response == Status.Conflict) return Conflict();
            else if (response == Status.BadRequest) return BadRequest();
            else return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _programmingLanguageRepository.DeleteAsync(id);

            if (response == Status.Deleted) return NoContent();
            else return NotFound();
        }
    }
}
