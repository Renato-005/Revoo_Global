using Microsoft.EntityFrameworkCore;
using Revoo.Application.Interfaces;
using Revoo.Domain.Entities;
using Revoo.Infrastructure.Data;

namespace Revoo.Infrastructure.Repositories;

public class HabitoRepository : IHabitoRepository
{
    private readonly RevooDbContext _ctx;
    public HabitoRepository(RevooDbContext ctx) => _ctx = ctx;

    public Task<Habito?> GetByIdAsync(long id)
        => _ctx.Habitos.FirstOrDefaultAsync(x => x.IdHabito == id);

    public Task AddAsync(Habito entity) => _ctx.Habitos.AddAsync(entity).AsTask();
    public void Update(Habito entity) => _ctx.Habitos.Update(entity);
    public void Remove(Habito entity) => _ctx.Habitos.Remove(entity);
    public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() > 0;

    public async Task<(IReadOnlyList<Habito> Items, int Total)> SearchAsync(
        string? term, long? categoriaId, string? ativo,
        int page, int pageSize, string orderBy, bool desc)
    {
        var q = _ctx.Habitos.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(term))
            q = q.Where(x => x.NomHabito.Contains(term) || (x.DesHabito ?? "").Contains(term));

        if (categoriaId.HasValue)
            q = q.Where(x => x.IdCategoriaHabito == categoriaId);

        if (ativo is "S" or "N")
            q = q.Where(x => x.IndAtivo == ativo);

        q = orderBy.ToLower() switch
        {
            "pontos" => desc ? q.OrderByDescending(x => x.PontosBase) : q.OrderBy(x => x.PontosBase),
            "categoria" => desc ? q.OrderByDescending(x => x.IdCategoriaHabito) : q.OrderBy(x => x.IdCategoriaHabito),
            _ => desc ? q.OrderByDescending(x => x.NomHabito) : q.OrderBy(x => x.NomHabito),
        };

        var total = await q.CountAsync();
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }
}
