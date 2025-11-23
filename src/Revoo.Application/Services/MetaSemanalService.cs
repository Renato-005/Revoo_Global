using Revoo.Application.DTOs;
using Revoo.Application.Interfaces;
using Revoo.Domain.Entities;
using Revoo.Domain.Exceptions;

namespace Revoo.Application.Services;

public class MetaSemanalService
{
    private readonly IMetaSemanalRepository _repo;
    public MetaSemanalService(IMetaSemanalRepository repo) => _repo = repo;

    public async Task<MetaSemanalDto> CreateAsync(CreateMetaSemanalDto dto)
    {
        var meta = new MetaSemanal(dto.ColaboradorId, dto.HabitoId, dto.SemanaInicio, dto.SemanaFim,
                                   dto.QtdMetaSemanal, dto.IndStatus);
        await _repo.AddAsync(meta);
        await _repo.SaveChangesAsync();
        return Map(meta);
    }

    public async Task<MetaSemanalDto> GetAsync(long id)
    {
        var meta = await _repo.GetByIdAsync(id) ?? throw new DomainException("Meta semanal não encontrada.");
        return Map(meta);
    }

    public async Task<MetaSemanalDto> UpdateAsync(long id, UpdateMetaSemanalDto dto)
    {
        var meta = await _repo.GetByIdAsync(id) ?? throw new DomainException("Meta semanal não encontrada.");
        meta.Update(dto.SemanaInicio, dto.SemanaFim, dto.QtdMetaSemanal, dto.IndStatus);
        _repo.Update(meta);
        await _repo.SaveChangesAsync();
        return Map(meta);
    }

    public async Task DeleteAsync(long id)
    {
        var meta = await _repo.GetByIdAsync(id) ?? throw new DomainException("Meta semanal não encontrada.");
        _repo.Remove(meta);
        await _repo.SaveChangesAsync();
    }

    public async Task<(IReadOnlyList<MetaSemanalDto> Items, int Total)> SearchAsync(MetaSemanalSearchQuery q)
    {
        var (items, total) = await _repo.SearchAsync(q.ColaboradorId, q.HabitoId, q.SemanaInicio, q.SemanaFim,
                                                    q.Page, q.PageSize, q.OrderBy, q.Desc);
        return (items.Select(Map).ToList(), total);
    }

    private static MetaSemanalDto Map(MetaSemanal m)
        => new(m.IdMetaSemanal, m.IdColaborador, m.IdHabito,
               m.DtSemanaInicio, m.DtSemanaFim, m.QtdMetaSemanal, m.IndStatus);
}
