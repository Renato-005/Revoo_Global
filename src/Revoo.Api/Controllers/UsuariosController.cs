using Microsoft.AspNetCore.Mvc;
using Revoo.Application.DTOs;
using Revoo.Application.Services;

namespace Revoo.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;
    public UsuariosController(UsuarioService service) => _service = service;

    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Create(CreateUsuarioDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<UsuarioDto>> GetById(long id)
        => Ok(await _service.GetAsync(id));

    [HttpPut("{id:long}")]
    public async Task<ActionResult<UsuarioDto>> Update(long id, UpdateUsuarioDto dto)
        => Ok(await _service.UpdateAsync(id, dto));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
