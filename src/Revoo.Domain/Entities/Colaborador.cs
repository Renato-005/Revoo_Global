using Revoo.Domain.Exceptions;

namespace Revoo.Domain.Entities;

public class Colaborador
{
    public long IdColaborador { get; private set; }
    public long IdUsuario { get; private set; }
    public string? Matricula { get; private set; }
    public string? Cargo { get; private set; }
    public string? Setor { get; private set; }
    public DateTime? DtAdmissao { get; private set; }
    public DateTime? DtDesligamento { get; private set; }
    public DateTime DtCriacao { get; private set; }
    public DateTime? DtAtualizacao { get; private set; }

    protected Colaborador() { }

    public Colaborador(long idUsuario, string? matricula, string? cargo, string? setor, DateTime? dtAdmissao)
    {
        if (idUsuario <= 0) throw new DomainException("Usuário obrigatório.");
        IdUsuario = idUsuario;
        Matricula = matricula?.Trim();
        Cargo = cargo?.Trim();
        Setor = setor?.Trim();
        DtAdmissao = dtAdmissao;
        DtCriacao = DateTime.UtcNow;
    }

    public void Update(string? matricula, string? cargo, string? setor, DateTime? dtAdmissao, DateTime? dtDesligamento)
    {
        Matricula = matricula?.Trim() ?? Matricula;
        Cargo = cargo?.Trim() ?? Cargo;
        Setor = setor?.Trim() ?? Setor;
        DtAdmissao = dtAdmissao ?? DtAdmissao;
        DtDesligamento = dtDesligamento ?? DtDesligamento;
        DtAtualizacao = DateTime.UtcNow;
    }
}
