using Microsoft.EntityFrameworkCore;
using Revoo.Application.Interfaces;
using Revoo.Domain.Entities;
using Revoo.Infrastructure.Data;

namespace Revoo.Infrastructure.Repositories;

public class MetaSemanalRepository : IMetaSemanalRepository
{
    private readonly RevooDbContext _ctx;
    public MetaSemanalRepository(RevooDbContext ctx) => _ctx = ctx;

    public Task<MetaSemanal?> GetByIdAsync(long id)
        => _ctx.MetasSemanais.FirstOrDefaultAsync(x => x.IdMetaSemanal == id);

    public Task AddAsync(MetaSemanal entity) => _ctx.MetasSemanais.AddAsync(entity).AsTask();
    public void Update(MetaSemanal entity) => _ctx.MetasSemanais.Update(entity);
    public void Remove(MetaSemanal entity) => _ctx.MetasSemanais.Remove(entity);
    public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() > 0;

    public async Task<(IReadOnlyList<MetaSemanal> Items, int Total)> SearchAsync(
        long? colaboradorId, long? habitoId, DateTime? semanaInicio, DateTime? semanaFim,
        int page, int pageSize, string orderBy, bool desc)
    {
        var q = _ctx.MetasSemanais.AsNoTracking().AsQueryable();

        if (colaboradorId.HasValue) q = q.Where(x => x.IdColaborador == colaboradorId.Value);
        if (habitoId.HasValue) q = q.Where(x => x.IdHabito == habitoId.Value);
        if (semanaInicio.HasValue) q = q.Where(x => x.DtSemanaInicio >= semanaInicio.Value.Date);
        if (semanaFim.HasValue) q = q.Where(x => x.DtSemanaFim <= semanaFim.Value.Date);

        q = orderBy.ToLower() switch
        {
            "fim" => desc ? q.OrderByDescending(x => x.DtSemanaFim) : q.OrderBy(x => x.DtSemanaFim),
            "qtd" => desc ? q.OrderByDescending(x => x.QtdMetaSemanal) : q.OrderBy(x => x.QtdMetaSemanal),
            _ => desc ? q.OrderByDescending(x => x.DtSemanaInicio) : q.OrderBy(x => x.DtSemanaInicio),
        };

        var total = await q.CountAsync();
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }
}
