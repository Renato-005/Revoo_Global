using Microsoft.AspNetCore.Mvc;
using Revoo.Application.DTOs;
using Revoo.Application.Services;

namespace Revoo.Api.Controllers;

[ApiController]
[Route("api/metas-semanais")]
public class MetasSemanaisController : ControllerBase
{
    private readonly MetaSemanalService _service;
    public MetasSemanaisController(MetaSemanalService service) => _service = service;

    [HttpPost]
    public async Task<ActionResult<object>> Create(CreateMetaSemanalDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, WithLinks(created));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<object>> GetById(long id)
        => Ok(WithLinks(await _service.GetAsync(id)));

    [HttpPut("{id:long}")]
    public async Task<ActionResult<object>> Update(long id, UpdateMetaSemanalDto dto)
        => Ok(WithLinks(await _service.UpdateAsync(id, dto)));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<object>> Search([FromQuery] MetaSemanalSearchQuery q)
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

    private object WithLinks(MetaSemanalDto m) => new
    {
        m.Id, m.ColaboradorId, m.HabitoId, m.SemanaInicio, m.SemanaFim, m.QtdMetaSemanal, m.IndStatus,
        links = new[]
        {
            new { rel = "self", href = Url.ActionLink(nameof(GetById), values: new { id = m.Id }), method = "GET" },
            new { rel = "update", href = Url.ActionLink(nameof(Update), values: new { id = m.Id }), method = "PUT" },
            new { rel = "delete", href = Url.ActionLink(nameof(Delete), values: new { id = m.Id }), method = "DELETE" },
            new { rel = "search", href = Url.ActionLink(nameof(Search)), method = "GET" }
        }
    };
}
