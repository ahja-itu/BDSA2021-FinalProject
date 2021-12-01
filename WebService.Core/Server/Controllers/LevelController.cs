﻿namespace WebService.Core.Server.Controllers
{
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LevelDTO>>> Get()
        {
            var result = await _levelRepository.ReadAsync();
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
