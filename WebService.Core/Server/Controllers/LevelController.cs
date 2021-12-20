// ***********************************************************************
// Assembly         : WebService.Core.Server
// Author           : Group BTG
// Created          : 11-29-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="LevelController.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Controllers;

/// <summary>
///     Class LevelController.
///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class LevelController : ControllerBase
{
    /// <summary>
    ///     The level repository
    /// </summary>
    private readonly ILevelRepository _levelRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LevelController" /> class.
    /// </summary>
    public LevelController(ILevelRepository levelRepository)
    {
        _levelRepository = levelRepository;
    }

    /// <summary>
    ///     Gets all LevelDTOs
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<LevelDTO>>> Get()
    {
        var result = await _levelRepository.ReadAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Gets a specified levelDTO based on id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LevelDTO>> Get(int id)
    {
        var (response, levelDTO) = await _levelRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(levelDTO);
        return NotFound();
    }

    /// <summary>
    ///     Posts a new level.
    /// </summary>
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

    /// <summary>
    ///     Puts a specified, existing level.
    /// </summary>
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

    /// <summary>
    ///     Deletes a specified level based on id.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _levelRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}