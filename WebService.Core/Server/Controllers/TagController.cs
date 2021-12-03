namespace WebService.Core.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<TagDTO>>> Get()
        {
            var result = await _tagRepository.ReadAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TagDTO>> Get(int id)
        {
            var result = await _tagRepository.ReadAsync(id);
            var response = result.Item1;

            if(response == Status.Found) return Ok(result);
            else return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(CreateTagDTO tag)
        {
            var result = await _tagRepository.CreateAsync(tag);
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
        public async Task<IActionResult> Put(TagDTO tag)
        {
            var response = await _tagRepository.UpdateAsync(tag);

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
            var response = await _tagRepository.DeleteAsync(id);

            if (response == Status.Deleted) return NoContent();
            else return NotFound();
        }
    }
}
