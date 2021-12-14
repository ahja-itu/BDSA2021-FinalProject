// ***********************************************************************
// Assembly         : WebService.Core.Server
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="MediaController.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Controllers;

/// <summary>
///     Class MediaController.
///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class MediaController : ControllerBase
{
    /// <summary>
    ///     The media repository
    /// </summary>
    private readonly IMediaRepository _mediaRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MediaController" /> class.
    /// </summary>
    /// <param name="mediaRepository">The media repository.</param>
    public MediaController(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }

    /// <summary>
    ///     Gets all mediaDTOs.
    /// </summary>
    /// <returns>ActionResult&lt;ICollection&lt;MediaDTO&gt;&gt;.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<MediaDTO>>> Get()
    {
        var result = await _mediaRepository.ReadAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Gets a specified media.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>ActionResult&lt;MediaDTO&gt;.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MediaDTO>> Get(int id)
    {
        var (response, mediaDTO) = await _mediaRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(mediaDTO);
        return NotFound();
    }

    /// <summary>
    ///     Posts the specified media.
    /// </summary>
    /// <param name="media">The media.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post(CreateMediaDTO media)
    {
        var (response, mediaDTO) = await _mediaRepository.CreateAsync(media);

        return response switch
        {
            Status.Created => Created(nameof(Put), mediaDTO),
            Status.Conflict => Conflict(),
            _ => BadRequest()
        };
    }

    /// <summary>
    ///     Puts the specified media.
    /// </summary>
    /// <param name="media">The media.</param>
    /// <returns>IActionResult.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put(MediaDTO media)
    {
        var response = await _mediaRepository.UpdateAsync(media);

        return response switch
        {
            Status.Updated => NoContent(),
            Status.Conflict => Conflict(),
            Status.BadRequest => BadRequest(),
            _ => NotFound()
        };
    }

    /// <summary>
    ///     Deletes the specified media.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>IActionResult.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediaRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}