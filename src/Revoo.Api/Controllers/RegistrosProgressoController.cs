using Microsoft.AspNetCore.Mvc;
using Revoo.Application.DTOs;
using Revoo.Application.Services;

namespace Revoo.Api.Controllers;

[ApiController]
[Route("api/registros-progresso")]
public class RegistrosProgressoController : ControllerBase
{
    private readonly RegistroProgressoService _service;
    public RegistrosProgressoController(RegistroProgressoService service) => _service = service;

    [HttpPost]
    public async Task<ActionResult<object>> Create(CreateRegistroProgressoDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, WithLinks(created));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<object>> GetById(long id)
        => Ok(WithLinks(await _service.GetAsync(id)));

    [HttpPut("{id:long}")]
    public async Task<ActionResult<object>> Update(long id, UpdateRegistroProgressoDto dto)
        => Ok(WithLinks(await _service.UpdateAsync(id, dto)));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<object>> Search([FromQuery] RegistroProgressoSearchQuery q)
    {
        var (items, total) = await _service.SearchAsync(q);
        var result = new
        {
            q.Page,
            q.PageSize,
            Total = total,
            Items = items.Select(WithLinks)
        };
        return Ok(result);
    }

    private object WithLinks(RegistroProgressoDto r) => new
    {
        r.Id, r.MetaSemanalId, r.DataRegistro, r.QtdRealizada, r.Obs,
        links = new[]
        {
            new { rel = "self", href = Url.ActionLink(nameof(GetById), values: new { id = r.Id }), method = "GET" },
            new { rel = "update", href = Url.ActionLink(nameof(Update), values: new { id = r.Id }), method = "PUT" },
            new { rel = "delete", href = Url.ActionLink(nameof(Delete), values: new { id = r.Id }), method = "DELETE" },
            new { rel = "search", href = Url.ActionLink(nameof(Search)), method = "GET" }
        }
    };
}
