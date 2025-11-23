namespace Revoo.Application.DTOs;

public record HabitoDto(long Id, long CategoriaId, string Nome, string? Descricao,
                         decimal? QtdSemanalSugerida, decimal PontosBase, string IndAtivo);

public record CreateHabitoDto(long CategoriaId, string Nome, string? Descricao,
                              decimal? QtdSemanalSugerida, decimal? PontosBase, string? IndAtivo);

public record UpdateHabitoDto(long? CategoriaId, string? Nome, string? Descricao,
                              decimal? QtdSemanalSugerida, decimal? PontosBase, string? IndAtivo);

public record HabitoSearchQuery(string? Term, long? CategoriaId, string? Ativo,
                                int Page = 1, int PageSize = 10,
                                string OrderBy = "nome", bool Desc = false);
