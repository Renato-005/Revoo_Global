using Revoo.Application.DTOs;
using Revoo.Application.Interfaces;
using Revoo.Domain.Entities;
using Revoo.Domain.Exceptions;

namespace Revoo.Application.Services;

public class HabitoService
{
    private readonly IHabitoRepository _repo;
    public HabitoService(IHabitoRepository repo) => _repo = repo;

    public async Task<HabitoDto> CreateAsync(CreateHabitoDto dto)
    {
        var hab = new Habito(dto.CategoriaId, dto.Nome, dto.Descricao, dto.QtdSemanalSugerida,
                             dto.PontosBase ?? 0, dto.IndAtivo);
        await _repo.AddAsync(hab);
        await _repo.SaveChangesAsync();

        return Map(hab);
    }

    public async Task<HabitoDto> GetAsync(long id)
    {
        var hab = await _repo.GetByIdAsync(id) ?? throw new DomainException("Hábito não encontrado.");
        return Map(hab);
    }

    public async Task<HabitoDto> UpdateAsync(long id, UpdateHabitoDto dto)
    {
        var hab = await _repo.GetByIdAsync(id) ?? throw new DomainException("Hábito não encontrado.");
        hab.Update(dto.CategoriaId, dto.Nome, dto.Descricao, dto.QtdSemanalSugerida, dto.PontosBase, dto.IndAtivo);
        _repo.Update(hab);
        await _repo.SaveChangesAsync();
        return Map(hab);
    }

    public async Task DeleteAsync(long id)
    {
        var hab = await _repo.GetByIdAsync(id) ?? throw new DomainException("Hábito não encontrado.");
        _repo.Remove(hab);
        await _repo.SaveChangesAsync();
    }

    public Task<(IReadOnlyList<HabitoDto> Items, int Total)> SearchAsync(HabitoSearchQuery q)
        => SearchInternal(q);

    private async Task<(IReadOnlyList<HabitoDto> Items, int Total)> SearchInternal(HabitoSearchQuery q)
    {
        var (items, total) = await _repo.SearchAsync(q.Term, q.CategoriaId, q.Ativo,
                                                    q.Page, q.PageSize, q.OrderBy, q.Desc);
        return (items.Select(Map).ToList(), total);
    }

    private static HabitoDto Map(Habito h)
        => new(h.IdHabito, h.IdCategoriaHabito, h.NomHabito, h.DesHabito,
               h.QtdSemanalSugerida, h.PontosBase, h.IndAtivo);
}
