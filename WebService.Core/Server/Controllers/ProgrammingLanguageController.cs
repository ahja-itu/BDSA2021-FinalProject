namespace WebService.Core.Server.Controllers;

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
        var (response, programmingLanguageDTO) = await _programmingLanguageRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(programmingLanguageDTO);
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post(CreateProgrammingLanguageDTO programmingLanguage)
    {
        var (response, programmingLanguageDTO) = await _programmingLanguageRepository.CreateAsync(programmingLanguage);

        return response switch
        {
            Status.Created => Created(nameof(Put), programmingLanguageDTO),
            Status.Conflict => Conflict(),
            _ => BadRequest()
        };
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put(ProgrammingLanguageDTO programmingLanguage)
    {
        var response = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);

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
        var response = await _programmingLanguageRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}