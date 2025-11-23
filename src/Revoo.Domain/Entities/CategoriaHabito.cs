using Revoo.Domain.Exceptions;

namespace Revoo.Domain.Entities;

public class CategoriaHabito
{
    public long IdCategoriaHabito { get; private set; }
    public string NomCategoria { get; private set; } = null!;
    public string? DesCategoria { get; private set; }
    public DateTime DtCriacao { get; private set; }
    public DateTime? DtAtualizacao { get; private set; }

    protected CategoriaHabito() { }

    public CategoriaHabito(string nome, string? desc)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome da categoria é obrigatório.");
        NomCategoria = nome.Trim().ToUpperInvariant();
        DesCategoria = desc?.Trim();
        DtCriacao = DateTime.UtcNow;
    }

    public void Update(string? nome, string? desc)
    {
        if (!string.IsNullOrWhiteSpace(nome))
            NomCategoria = nome.Trim().ToUpperInvariant();
        DesCategoria = desc?.Trim() ?? DesCategoria;
        DtAtualizacao = DateTime.UtcNow;
    }
}
