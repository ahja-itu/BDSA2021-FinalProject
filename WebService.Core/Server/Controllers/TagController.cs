// ***********************************************************************
// Assembly         : WebService.Core.Server
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="TagController.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Controllers;

/// <summary>
///     Class TagController.
///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class TagController : ControllerBase
{
    /// <summary>
    ///     The tag repository
    /// </summary>
    private readonly ITagRepository _tagRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TagController" /> class.
    /// </summary>
    /// <param name="tagRepository">The tag repository.</param>
    public TagController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    /// <summary>
    ///     Gets all tagDTOs.
    /// </summary>
    /// <returns>ActionResult&lt;ICollection&lt;TagDTO&gt;&gt;.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<TagDTO>>> Get()
    {
        var result = await _tagRepository.ReadAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Gets the specified tagDTO.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>ActionResult&lt;TagDTO&gt;.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TagDTO>> Get(int id)
    {
        var (response, tagDTO) = await _tagRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(tagDTO);
        return NotFound();
    }

    /// <summary>
    ///     Posts the specified tag.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <returns>IActionResult.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post(CreateTagDTO tag)
    {
        var (response, tagDTO) = await _tagRepository.CreateAsync(tag);

        return response switch
        {
            Status.Created => Created(nameof(Put), tagDTO),
            Status.Conflict => Conflict(),
            _ => BadRequest()
        };
    }

    /// <summary>
    ///     Puts the specified tag.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <returns>IActionResult.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Put(TagDTO tag)
    {
        var response = await _tagRepository.UpdateAsync(tag);

        return response switch
        {
            Status.Updated => NoContent(),
            Status.Conflict => Conflict(),
            Status.BadRequest => BadRequest(),
            _ => NotFound()
        };
    }

    /// <summary>
    ///     Deletes the specified tagDTO.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>IActionResult.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _tagRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}