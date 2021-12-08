namespace WebService.Core.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialController(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<MaterialDTO>>> Get()
        {
            var result = await _materialRepository.ReadAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MaterialDTO>> Get(int id)
        {
            var result = await _materialRepository.ReadAsync(id);
            var response = result.Item1;

            if (response == Status.Found) return Ok(result);
            else return NotFound(result);
        }

        [HttpGet("{SearchForm}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MaterialDTO>> Get(SearchForm searchForm)
        {
            throw new NotImplementedException();
            //var result = await _materialRepository.ReadAsync(searchForm);
            //var response = result.Item1;

            //if (response == Status.Found) return Ok(result);
            //else return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(CreateMaterialDTO material)
        {
            var result = await _materialRepository.CreateAsync(material);
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
        public async Task<IActionResult> Put(MaterialDTO material)
        {
            var response = await _materialRepository.UpdateAsync(material);

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
            var response = await _materialRepository.DeleteAsync(id);

            if (response == Status.Deleted) return NoContent();
            else return NotFound();
        }
    }
}
