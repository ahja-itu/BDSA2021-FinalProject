namespace WebService.Core.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class LevelController : ControllerBase
{
    private readonly ILevelRespository _levelRepository;

    public LevelController(ILevelRespository levelRepository)
    {
        _levelRepository = levelRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<LevelDTO>>> Get()
    {
        var result = await _levelRepository.ReadAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LevelDTO>> Get(int id)
    {
        var (response, levelDTO) = await _levelRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(levelDTO);
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post(CreateLevelDTO level)
    {
        var (response, levelDTO) = await _levelRepository.CreateAsync(level);

        return response switch
        {
            Status.Created => Created(nameof(Put), levelDTO),
            Status.Conflict => Conflict(),
            _ => BadRequest()
        };
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put(LevelDTO level)
    {
        var response = await _levelRepository.UpdateAsync(level);

        return response switch
        {
            Status.Updated => NoContent(),
            Status.Conflict => Conflict(),
            Status.BadRequest => BadRequest(),
            _ => NotFound()
        };
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _levelRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}