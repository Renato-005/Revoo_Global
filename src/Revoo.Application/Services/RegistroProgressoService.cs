using Revoo.Application.DTOs;
using Revoo.Application.Interfaces;
using Revoo.Domain.Entities;
using Revoo.Domain.Exceptions;

namespace Revoo.Application.Services;

public class RegistroProgressoService
{
    private readonly IRegistroProgressoRepository _repo;
    public RegistroProgressoService(IRegistroProgressoRepository repo) => _repo = repo;

    public async Task<RegistroProgressoDto> CreateAsync(CreateRegistroProgressoDto dto)
    {
        var reg = new RegistroProgresso(dto.MetaSemanalId, dto.DataRegistro, dto.QtdRealizada, dto.Obs);
        await _repo.AddAsync(reg);
        await _repo.SaveChangesAsync();
        return Map(reg);
    }

    public async Task<RegistroProgressoDto> GetAsync(long id)
    {
        var reg = await _repo.GetByIdAsync(id) ?? throw new DomainException("Registro de progresso não encontrado.");
        return Map(reg);
    }

    public async Task<RegistroProgressoDto> UpdateAsync(long id, UpdateRegistroProgressoDto dto)
    {
        var reg = await _repo.GetByIdAsync(id) ?? throw new DomainException("Registro de progresso não encontrado.");
        reg.Update(dto.QtdRealizada, dto.Obs);
        _repo.Update(reg);
        await _repo.SaveChangesAsync();
        return Map(reg);
    }

    public async Task DeleteAsync(long id)
    {
        var reg = await _repo.GetByIdAsync(id) ?? throw new DomainException("Registro de progresso não encontrado.");
        _repo.Remove(reg);
        await _repo.SaveChangesAsync();
    }

    public async Task<(IReadOnlyList<RegistroProgressoDto> Items, int Total)> SearchAsync(RegistroProgressoSearchQuery q)
    {
        var (items, total) = await _repo.SearchAsync(q.MetaSemanalId, q.DataInicio, q.DataFim,
                                                    q.Page, q.PageSize, q.OrderBy, q.Desc);
        return (items.Select(Map).ToList(), total);
    }

    private static RegistroProgressoDto Map(RegistroProgresso r)
        => new(r.IdRegistroProgresso, r.IdMetaSemanal, r.DataRegistro, r.QtdRealizada, r.Obs);
}
