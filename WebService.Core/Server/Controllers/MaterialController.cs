// ***********************************************************************
// Assembly         : WebService.Core.Server
// Author           : Group BTG
// Created          : 12-03-2021
//
// Last Modified By : Group BTG
// Last Modified On : 12-14-2021
// ***********************************************************************
// <copyright file="MaterialController.cs" company="BTG">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace WebService.Core.Server.Controllers;

/// <summary>
///     Class MaterialController.
///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class MaterialController : ControllerBase
{
    /// <summary>
    ///     The material repository
    /// </summary>
    private readonly IMaterialRepository _materialRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MaterialController" /> class.
    /// </summary>
    public MaterialController(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }

    /// <summary>
    ///     Gets all materialDTOs.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<MaterialDTO>>> Get()
    {
        var result = await _materialRepository.ReadAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Gets a specified materialDTO based on id.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaterialDTO>> Get(int id)
    {
        var (response, materialDTO) = await _materialRepository.ReadAsync(id);

        if (response == Status.Found) return Ok(materialDTO);
        return NotFound(materialDTO);
    }

    /// <summary>
    ///     Posts a specified search form and returns a materialDTO matching the search from.
    /// </summary>
    [HttpPost("PostSearchForm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaterialDTO>> Post(SearchForm searchForm)
    {
        var (response, materialDTOs) = await _materialRepository.ReadAsync(searchForm);

        if (response == Status.Found) return Ok(materialDTOs);
        return NotFound();
    }

    /// <summary>
    ///     Posts the specified material.
    /// </summary>
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

    /// <summary>
    ///     Puts the specified, existing material.
    /// </summary>
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

    /// <summary>
    ///     Deletes the specified material based on id.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _materialRepository.DeleteAsync(id);

        if (response == Status.Deleted) return NoContent();
        return NotFound();
    }
}