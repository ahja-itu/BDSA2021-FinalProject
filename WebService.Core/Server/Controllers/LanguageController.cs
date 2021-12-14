namespace WebService.Core.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class LanguageController : ControllerBase
{
    private readonly ILanguageRepository _languageRepository;

    public LanguageController(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<LanguageDTO>>> Get()
    {
        var result = await _languageRepository.ReadAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LanguageDTO>> Get(int id)
    {
        var (response, languageDTO) = await _languageRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(languageDTO);
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post(CreateLanguageDTO language)
    {
        var (response, languageDTO) = await _languageRepository.CreateAsync(language);

        return response switch
        {
            Status.Created => Created(nameof(Put), languageDTO),
            Status.Conflict => Conflict(),
            _ => BadRequest()
        };
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put(LanguageDTO language)
    {
        var response = await _languageRepository.UpdateAsync(language);

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
        var response = await _languageRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}