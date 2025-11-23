using Revoo.Domain.Exceptions;

namespace Revoo.Domain.Entities;

public class MetaSemanal
{
    public long IdMetaSemanal { get; private set; }
    public long IdColaborador { get; private set; }
    public long IdHabito { get; private set; }
    public DateTime DtSemanaInicio { get; private set; }
    public DateTime DtSemanaFim { get; private set; }
    public decimal QtdMetaSemanal { get; private set; }
    public string IndStatus { get; private set; } = "ATIVA";
    public DateTime DtCriacao { get; private set; }
    public DateTime? DtAtualizacao { get; private set; }

    protected MetaSemanal() { }

    public MetaSemanal(long idColab, long idHabito, DateTime inicio, DateTime fim, decimal qtd, string? status)
    {
        if (idColab <= 0 || idHabito <= 0) throw new DomainException("Colaborador e hábito são obrigatórios.");
        Validate(inicio, fim, qtd);

        IdColaborador = idColab;
        IdHabito = idHabito;
        DtSemanaInicio = inicio.Date;
        DtSemanaFim = fim.Date;
        QtdMetaSemanal = qtd;
        IndStatus = string.IsNullOrWhiteSpace(status) ? "ATIVA" : status!.Trim().ToUpperInvariant();
        DtCriacao = DateTime.UtcNow;
    }

    public void Update(DateTime? inicio, DateTime? fim, decimal? qtd, string? status)
    {
        var newIni = inicio?.Date ?? DtSemanaInicio;
        var newFim = fim?.Date ?? DtSemanaFim;
        var newQtd = qtd ?? QtdMetaSemanal;

        Validate(newIni, newFim, newQtd);
        DtSemanaInicio = newIni;
        DtSemanaFim = newFim;
        QtdMetaSemanal = newQtd;
        if (!string.IsNullOrWhiteSpace(status)) IndStatus = status!.Trim().ToUpperInvariant();
        DtAtualizacao = DateTime.UtcNow;
    }

    private static void Validate(DateTime inicio, DateTime fim, decimal qtd)
    {
        if (fim < inicio) throw new DomainException("DT_SEMANA_FIM não pode ser menor que início.");
        if ((fim - inicio).TotalDays > 6) throw new DomainException("Meta semanal deve ter no máximo 7 dias.");
        if (qtd <= 0) throw new DomainException("Quantidade da meta deve ser > 0.");
    }
}
