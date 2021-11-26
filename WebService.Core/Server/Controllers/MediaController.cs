﻿namespace WebService.Core.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[api/controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaRepository _mediaRespository;

        public MediaController(IMediaRepository mediaRepository)
        {
            _mediaRespository = mediaRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MediaDTO>>> Get()
        {
            var result = await _mediaRespository.ReadAsync();
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
