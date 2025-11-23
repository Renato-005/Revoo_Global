namespace Revoo.Application.DTOs;

public record UsuarioDto(long Id, string Nome, string Email, DateTime? DtNasc);
public record CreateUsuarioDto(string Nome, string Email, string SenhaHash, DateTime? DtNasc);
public record UpdateUsuarioDto(string? Nome, string? Email, string? SenhaHash, DateTime? DtNasc);
