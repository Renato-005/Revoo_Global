using Revoo.Domain.Exceptions;

namespace Revoo.Domain.Entities;

public class RegistroProgresso
{
    public long IdRegistroProgresso { get; private set; }
    public long IdMetaSemanal { get; private set; }
    public DateTime DataRegistro { get; private set; }
    public decimal QtdRealizada { get; private set; }
    public string? Obs { get; private set; }
    public DateTime DtCriacao { get; private set; }
    public DateTime? DtAtualizacao { get; private set; }

    protected RegistroProgresso() { }

    public RegistroProgresso(long idMeta, DateTime data, decimal qtd, string? obs)
    {
        if (idMeta <= 0) throw new DomainException("Meta semanal obrigatória.");
        if (data == default) throw new DomainException("Data obrigatória.");
        if (qtd <= 0) throw new DomainException("Qtd realizada inválida.");

        IdMetaSemanal = idMeta;
        DataRegistro = data.Date;
        QtdRealizada = qtd;
        Obs = obs?.Trim();
        DtCriacao = DateTime.UtcNow;
    }

    public void Update(decimal? qtd, string? obs)
    {
        if (qtd.HasValue && qtd.Value <= 0) throw new DomainException("Qtd realizada inválida.");
        QtdRealizada = qtd ?? QtdRealizada;
        Obs = obs?.Trim() ?? Obs;
        DtAtualizacao = DateTime.UtcNow;
    }
}
