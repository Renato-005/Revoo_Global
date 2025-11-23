namespace Revoo.Application.DTOs;

public record RegistroProgressoDto(
    long Id,
    long MetaSemanalId,
    DateTime DataRegistro,
    decimal QtdRealizada,
    string? Obs
);

public record CreateRegistroProgressoDto(
    long MetaSemanalId,
    DateTime DataRegistro,
    decimal QtdRealizada,
    string? Obs
);

public record UpdateRegistroProgressoDto(
    decimal? QtdRealizada,
    string? Obs
);

public record RegistroProgressoSearchQuery(
    long? MetaSemanalId,
    DateTime? DataInicio,
    DateTime? DataFim,
    int Page = 1,
    int PageSize = 10,
    string OrderBy = "data",
    bool Desc = false
);
