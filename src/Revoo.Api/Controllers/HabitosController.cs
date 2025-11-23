using Microsoft.AspNetCore.Mvc;
using Revoo.Application.DTOs;
using Revoo.Application.Services;

namespace Revoo.Api.Controllers;

[ApiController]
[Route("api/habitos")]
public class HabitosController : ControllerBase
{
    private readonly HabitoService _service;
    public HabitosController(HabitoService service) => _service = service;

    [HttpPost]
    public async Task<ActionResult<object>> Create(CreateHabitoDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, WithLinks(created));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<object>> GetById(long id)
    {
        var hab = await _service.GetAsync(id);
        return Ok(WithLinks(hab));
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<object>> Update(long id, UpdateHabitoDto dto)
    {
        var hab = await _service.UpdateAsync(id, dto);
        return Ok(WithLinks(hab));
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<object>> Search([FromQuery] HabitoSearchQuery q)
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

    private object WithLinks(HabitoDto h) => new
    {
        h.Id, h.CategoriaId, h.Nome, h.Descricao, h.QtdSemanalSugerida, h.PontosBase, h.IndAtivo,
        links = new[]
        {
            new { rel = "self", href = Url.ActionLink(nameof(GetById), values: new { id = h.Id }), method = "GET" },
            new { rel = "update", href = Url.ActionLink(nameof(Update), values: new { id = h.Id }), method = "PUT" },
            new { rel = "delete", href = Url.ActionLink(nameof(Delete), values: new { id = h.Id }), method = "DELETE" },
            new { rel = "search", href = Url.ActionLink(nameof(Search)), method = "GET" }
        }
    };
}
