using Revoo.Domain.Entities;

namespace Revoo.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(long id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task AddAsync(Usuario entity);
    void Update(Usuario entity);
    void Remove(Usuario entity);
    Task<bool> SaveChangesAsync();
}

public interface IHabitoRepository
{
    Task<Habito?> GetByIdAsync(long id);
    Task AddAsync(Habito entity);
    void Update(Habito entity);
    void Remove(Habito entity);
    Task<(IReadOnlyList<Habito> Items, int Total)> SearchAsync(
        string? term, long? categoriaId, string? ativo,
        int page, int pageSize, string orderBy, bool desc);
    Task<bool> SaveChangesAsync();
}

public interface IMetaSemanalRepository
{
    Task<Revoo.Domain.Entities.MetaSemanal?> GetByIdAsync(long id);
    Task AddAsync(Revoo.Domain.Entities.MetaSemanal entity);
    void Update(Revoo.Domain.Entities.MetaSemanal entity);
    void Remove(Revoo.Domain.Entities.MetaSemanal entity);
    Task<(IReadOnlyList<Revoo.Domain.Entities.MetaSemanal> Items, int Total)> SearchAsync(
        long? colaboradorId, long? habitoId, DateTime? semanaInicio, DateTime? semanaFim,
        int page, int pageSize, string orderBy, bool desc);
    Task<bool> SaveChangesAsync();
}

public interface IRegistroProgressoRepository
{
    Task<Revoo.Domain.Entities.RegistroProgresso?> GetByIdAsync(long id);
    Task AddAsync(Revoo.Domain.Entities.RegistroProgresso entity);
    void Update(Revoo.Domain.Entities.RegistroProgresso entity);
    void Remove(Revoo.Domain.Entities.RegistroProgresso entity);
    Task<(IReadOnlyList<Revoo.Domain.Entities.RegistroProgresso> Items, int Total)> SearchAsync(
        long? metaSemanalId, DateTime? dataInicio, DateTime? dataFim,
        int page, int pageSize, string orderBy, bool desc);
    Task<bool> SaveChangesAsync();
}
