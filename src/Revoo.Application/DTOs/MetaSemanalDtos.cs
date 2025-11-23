namespace Revoo.Application.DTOs;

public record MetaSemanalDto(
    long Id,
    long ColaboradorId,
    long HabitoId,
    DateTime SemanaInicio,
    DateTime SemanaFim,
    decimal QtdMetaSemanal,
    string IndStatus
);

public record CreateMetaSemanalDto(
    long ColaboradorId,
    long HabitoId,
    DateTime SemanaInicio,
    DateTime SemanaFim,
    decimal QtdMetaSemanal,
    string? IndStatus
);

public record UpdateMetaSemanalDto(
    DateTime? SemanaInicio,
    DateTime? SemanaFim,
    decimal? QtdMetaSemanal,
    string? IndStatus
);

public record MetaSemanalSearchQuery(
    long? ColaboradorId,
    long? HabitoId,
    DateTime? SemanaInicio,
    DateTime? SemanaFim,
    int Page = 1,
    int PageSize = 10,
    string OrderBy = "inicio",
    bool Desc = false
);
