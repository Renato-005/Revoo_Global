using Revoo.Application.DTOs;
using Revoo.Application.Interfaces;
using Revoo.Domain.Entities;
using Revoo.Domain.Exceptions;

namespace Revoo.Application.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _repo;
    public UsuarioService(IUsuarioRepository repo) => _repo = repo;

    public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
    {
        if (await _repo.GetByEmailAsync(dto.Email) != null)
            throw new DomainException("Email já cadastrado.");

        var user = new Usuario(dto.Nome, dto.Email, dto.SenhaHash, dto.DtNasc);
        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();

        return new UsuarioDto(user.IdUsuario, user.NomUsuario, user.Email, user.DtNasc);
    }

    public async Task<UsuarioDto> GetAsync(long id)
    {
        var user = await _repo.GetByIdAsync(id) 
                   ?? throw new DomainException("Usuário não encontrado.");
        return new UsuarioDto(user.IdUsuario, user.NomUsuario, user.Email, user.DtNasc);
    }

    public async Task<UsuarioDto> UpdateAsync(long id, UpdateUsuarioDto dto)
    {
        var user = await _repo.GetByIdAsync(id)
                   ?? throw new DomainException("Usuário não encontrado.");

        user.Update(dto.Nome, dto.Email, dto.SenhaHash, dto.DtNasc);
        _repo.Update(user);
        await _repo.SaveChangesAsync();

        return new UsuarioDto(user.IdUsuario, user.NomUsuario, user.Email, user.DtNasc);
    }

    public async Task DeleteAsync(long id)
    {
        var user = await _repo.GetByIdAsync(id)
                   ?? throw new DomainException("Usuário não encontrado.");
        _repo.Remove(user);
        await _repo.SaveChangesAsync();
    }
}
