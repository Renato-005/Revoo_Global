using Revoo.Domain.Exceptions;

namespace Revoo.Domain.Entities;

public class Habito
{
    public long IdHabito { get; private set; }
    public long IdCategoriaHabito { get; private set; }
    public string NomHabito { get; private set; } = null!;
    public string? DesHabito { get; private set; }
    public decimal? QtdSemanalSugerida { get; private set; }
    public decimal PontosBase { get; private set; }
    public string IndAtivo { get; private set; } = "S";
    public DateTime DtCriacao { get; private set; }
    public DateTime? DtAtualizacao { get; private set; }

    protected Habito() { }

    public Habito(long idCategoria, string nome, string? desc, decimal? qtdSug, decimal pontosBase, string? indAtivo)
    {
        if (idCategoria <= 0) throw new DomainException("Categoria obrigatória.");
        if (string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome do hábito é obrigatório.");

        IdCategoriaHabito = idCategoria;
        NomHabito = nome.Trim();
        DesHabito = desc?.Trim();
        QtdSemanalSugerida = qtdSug;
        PontosBase = pontosBase < 0 ? 0 : pontosBase;
        IndAtivo = (indAtivo is "S" or "N") ? indAtivo! : "S";
        DtCriacao = DateTime.UtcNow;
    }

    public void Update(long? idCategoria, string? nome, string? desc, decimal? qtdSug, decimal? pontosBase, string? indAtivo)
    {
        if (idCategoria.HasValue && idCategoria.Value > 0) IdCategoriaHabito = idCategoria.Value;
        if (!string.IsNullOrWhiteSpace(nome)) NomHabito = nome.Trim();
        DesHabito = desc?.Trim() ?? DesHabito;
        QtdSemanalSugerida = qtdSug ?? QtdSemanalSugerida;
        if (pontosBase.HasValue) PontosBase = pontosBase.Value < 0 ? 0 : pontosBase.Value;
        if (indAtivo is "S" or "N") IndAtivo = indAtivo!;
        DtAtualizacao = DateTime.UtcNow;
    }
}
