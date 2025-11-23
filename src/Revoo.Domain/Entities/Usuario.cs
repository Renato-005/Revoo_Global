using Revoo.Domain.Exceptions;

namespace Revoo.Domain.Entities;

public class Usuario
{
    public long IdUsuario { get; private set; }
    public string NomUsuario { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string SenhaHash { get; private set; } = null!;
    public DateTime? DtNasc { get; private set; }
    public DateTime DtCriacao { get; private set; }
    public DateTime? DtAtualizacao { get; private set; }

    protected Usuario() { }

    public Usuario(string nome, string email, string senhaHash, DateTime? dtNasc)
    {
        SetNome(nome);
        SetEmail(email);
        SetSenhaHash(senhaHash);
        DtNasc = dtNasc;
        DtCriacao = DateTime.UtcNow;
    }

    public void Update(string? nome, string? email, string? senhaHash, DateTime? dtNasc)
    {
        if (!string.IsNullOrWhiteSpace(nome)) SetNome(nome);
        if (!string.IsNullOrWhiteSpace(email)) SetEmail(email);
        if (!string.IsNullOrWhiteSpace(senhaHash)) SetSenhaHash(senhaHash);
        DtNasc = dtNasc ?? DtNasc;
        DtAtualizacao = DateTime.UtcNow;
    }

    private void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome é obrigatório.");
        NomUsuario = nome.Trim();
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email é obrigatório.");
        try { _ = new System.Net.Mail.MailAddress(email); }
        catch { throw new DomainException("Email inválido."); }
        Email = email.Trim().ToLowerInvariant();
    }

    private void SetSenhaHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new DomainException("SenhaHash é obrigatório.");
        SenhaHash = hash.Trim();
    }
}
