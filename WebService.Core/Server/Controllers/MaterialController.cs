namespace WebService.Core.Server.Controllers;

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
        var (response, materialDTO) = await _materialRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(materialDTO);
        return NotFound(materialDTO);
    }

    [HttpPost("PostSearchForm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaterialDTO>> Post(SearchForm searchForm)
    {
        var (response, materialDTOs) = await _materialRepository.ReadAsync(searchForm);

        if (response == Status.Found) return Ok(materialDTOs);
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post(CreateMaterialDTO material)
    {
        var (response, materialDTO) = await _materialRepository.CreateAsync(material);

        return response switch
        {
            Status.Created => Created(nameof(Put), materialDTO),
            Status.Conflict => Conflict(),
            _ => BadRequest()
        };
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put(MaterialDTO material)
    {
        var response = await _materialRepository.UpdateAsync(material);

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
        var response = await _materialRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}