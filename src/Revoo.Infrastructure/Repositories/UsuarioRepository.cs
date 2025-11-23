using Microsoft.EntityFrameworkCore;
using Revoo.Application.Interfaces;
using Revoo.Domain.Entities;
using Revoo.Infrastructure.Data;

namespace Revoo.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly RevooDbContext _ctx;
    public UsuarioRepository(RevooDbContext ctx) => _ctx = ctx;

    public Task<Usuario?> GetByIdAsync(long id)
        => _ctx.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

    public Task<Usuario?> GetByEmailAsync(string email)
        => _ctx.Usuarios.FirstOrDefaultAsync(x => x.Email == email.ToLower());

    public Task AddAsync(Usuario entity) => _ctx.Usuarios.AddAsync(entity).AsTask();
    public void Update(Usuario entity) => _ctx.Usuarios.Update(entity);
    public void Remove(Usuario entity) => _ctx.Usuarios.Remove(entity);
    public async Task<bool> SaveChangesAsync() => await _ctx.SaveChangesAsync() > 0;
}
