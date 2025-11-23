using Microsoft.EntityFrameworkCore;
using Revoo.Application.Interfaces;
using Revoo.Domain.Entities;
using Revoo.Infrastructure.Data;

namespace Revoo.Infrastructure.Repositories;

public class RegistroProgressoRepository : IRegistroProgressoRepository
{
    private readonly RevooDbContext _ctx;
    public RegistroProgressoRepository(RevooDbContext ctx) => _ctx = ctx;

    public Task<RegistroProgresso?> GetByIdAsync(long id)
        => _ctx.RegistrosProgresso.FirstOrDefaultAsync(x => x.IdRegistroProgresso == id);

    public Task AddAsync(RegistroProgresso entity) => _ctx.RegistrosProgresso.AddAsync(entity).AsTask();
    public void Update(RegistroProgresso entity) => _ctx.RegistrosProgresso.Update(entity);
    public void Remove(RegistroProgresso entity) => _ctx.RegistrosProgresso.Remove(entity);
    public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() > 0;

    public async Task<(IReadOnlyList<RegistroProgresso> Items, int Total)> SearchAsync(
        long? metaSemanalId, DateTime? dataInicio, DateTime? dataFim,
        int page, int pageSize, string orderBy, bool desc)
    {
        var q = _ctx.RegistrosProgresso.AsNoTracking().AsQueryable();

        if (metaSemanalId.HasValue)
            q = q.Where(x => x.IdMetaSemanal == metaSemanalId.Value);
        if (dataInicio.HasValue)
            q = q.Where(x => x.DataRegistro >= dataInicio.Value.Date);
        if (dataFim.HasValue)
            q = q.Where(x => x.DataRegistro <= dataFim.Value.Date);

        q = orderBy.ToLower() switch
        {
            "qtd" => desc ? q.OrderByDescending(x => x.QtdRealizada) : q.OrderBy(x => x.QtdRealizada),
            _ => desc ? q.OrderByDescending(x => x.DataRegistro) : q.OrderBy(x => x.DataRegistro),
        };

        var total = await q.CountAsync();
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }
}
