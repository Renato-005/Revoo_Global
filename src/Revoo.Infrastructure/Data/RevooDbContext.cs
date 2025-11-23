using Microsoft.EntityFrameworkCore;
using Revoo.Domain.Entities;

namespace Revoo.Infrastructure.Data;

public class RevooDbContext : DbContext
{
    public RevooDbContext(DbContextOptions<RevooDbContext> options) : base(options) {}

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Colaborador> Colaboradores => Set<Colaborador>();
    public DbSet<CategoriaHabito> CategoriasHabito => Set<CategoriaHabito>();
    public DbSet<Habito> Habitos => Set<Habito>();
    public DbSet<MetaSemanal> MetasSemanais => Set<MetaSemanal>();
    public DbSet<RegistroProgresso> RegistrosProgresso => Set<RegistroProgresso>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.HasDefaultSchema("SEU_SCHEMA"); 

        mb.Entity<Usuario>(e =>
        {
            e.ToTable("USUARIO");
            e.HasKey(x => x.IdUsuario);
            e.Property(x => x.IdUsuario).HasColumnName("ID_USUARIO");
            e.Property(x => x.NomUsuario).HasColumnName("NOM_USUARIO").HasMaxLength(100).IsRequired();
            e.Property(x => x.Email).HasColumnName("EMAIL").HasMaxLength(120).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.SenhaHash).HasColumnName("SENHA_HASH").HasMaxLength(200).IsRequired();
            e.Property(x => x.DtNasc).HasColumnName("DT_NASC");
            e.Property(x => x.DtCriacao).HasColumnName("DT_CRIACAO").IsRequired();
            e.Property(x => x.DtAtualizacao).HasColumnName("DT_ATUALIZACAO");
        });

        mb.Entity<Colaborador>(e =>
        {
            e.ToTable("COLABORADOR");
            e.HasKey(x => x.IdColaborador);
            e.Property(x => x.IdColaborador).HasColumnName("ID_COLABORADOR");
            e.Property(x => x.IdUsuario).HasColumnName("ID_USUARIO").IsRequired();
            e.HasIndex(x => x.IdUsuario).IsUnique();
            e.Property(x => x.Matricula).HasColumnName("MATRICULA").HasMaxLength(30);
            e.HasIndex(x => x.Matricula).IsUnique();
            e.Property(x => x.Cargo).HasColumnName("CARGO").HasMaxLength(60);
            e.Property(x => x.Setor).HasColumnName("SETOR").HasMaxLength(60);
            e.Property(x => x.DtAdmissao).HasColumnName("DT_ADMISSAO");
            e.Property(x => x.DtDesligamento).HasColumnName("DT_DESLIGAMENTO");
            e.Property(x => x.DtCriacao).HasColumnName("DT_CRIACAO").IsRequired();
            e.Property(x => x.DtAtualizacao).HasColumnName("DT_ATUALIZACAO");

            e.HasOne<Usuario>()
             .WithMany()
             .HasForeignKey(x => x.IdUsuario);
        });

        mb.Entity<CategoriaHabito>(e =>
        {
            e.ToTable("CATEGORIA_HABITO");
            e.HasKey(x => x.IdCategoriaHabito);
            e.Property(x => x.IdCategoriaHabito).HasColumnName("ID_CATEGORIA_HABITO");
            e.Property(x => x.NomCategoria).HasColumnName("NOM_CATEGORIA").HasMaxLength(80).IsRequired();
            e.HasIndex(x => x.NomCategoria).IsUnique();
            e.Property(x => x.DesCategoria).HasColumnName("DES_CATEGORIA").HasMaxLength(200);
            e.Property(x => x.DtCriacao).HasColumnName("DT_CRIACAO").IsRequired();
            e.Property(x => x.DtAtualizacao).HasColumnName("DT_ATUALIZACAO");
        });

        mb.Entity<Habito>(e =>
        {
            e.ToTable("HABITO");
            e.HasKey(x => x.IdHabito);
            e.Property(x => x.IdHabito).HasColumnName("ID_HABITO");
            e.Property(x => x.IdCategoriaHabito).HasColumnName("ID_CATEGORIA_HABITO").IsRequired();
            e.Property(x => x.NomHabito).HasColumnName("NOM_HABITO").HasMaxLength(120).IsRequired();
            e.HasIndex(x => x.NomHabito).IsUnique();
            e.Property(x => x.DesHabito).HasColumnName("DES_HABITO").HasMaxLength(400);
            e.Property(x => x.QtdSemanalSugerida).HasColumnName("QTD_SEMANAL_SUGERIDA");
            e.Property(x => x.PontosBase).HasColumnName("PONTOS_BASE").HasDefaultValue(0);
            e.Property(x => x.IndAtivo).HasColumnName("IND_ATIVO").HasMaxLength(1).HasDefaultValue("S");
            e.Property(x => x.DtCriacao).HasColumnName("DT_CRIACAO").IsRequired();
            e.Property(x => x.DtAtualizacao).HasColumnName("DT_ATUALIZACAO");

            e.HasOne<CategoriaHabito>()
             .WithMany()
             .HasForeignKey(x => x.IdCategoriaHabito);
        });

        mb.Entity<MetaSemanal>(e =>
        {
            e.ToTable("META_SEMANAL");
            e.HasKey(x => x.IdMetaSemanal);
            e.Property(x => x.IdMetaSemanal).HasColumnName("ID_META_SEMANAL");
            e.Property(x => x.IdColaborador).HasColumnName("ID_COLABORADOR").IsRequired();
            e.Property(x => x.IdHabito).HasColumnName("ID_HABITO").IsRequired();
            e.Property(x => x.DtSemanaInicio).HasColumnName("DT_SEMANA_INICIO").IsRequired();
            e.Property(x => x.DtSemanaFim).HasColumnName("DT_SEMANA_FIM").IsRequired();
            e.Property(x => x.QtdMetaSemanal).HasColumnName("QTD_META_SEMANAL").IsRequired();
            e.Property(x => x.IndStatus).HasColumnName("IND_STATUS").HasMaxLength(20).HasDefaultValue("ATIVA");
            e.Property(x => x.DtCriacao).HasColumnName("DT_CRIACAO").IsRequired();
            e.Property(x => x.DtAtualizacao).HasColumnName("DT_ATUALIZACAO");

            e.HasIndex(x => new { x.IdColaborador, x.IdHabito, x.DtSemanaInicio }).IsUnique();
            e.HasOne<Colaborador>().WithMany().HasForeignKey(x => x.IdColaborador);
            e.HasOne<Habito>().WithMany().HasForeignKey(x => x.IdHabito);
        });

        mb.Entity<RegistroProgresso>(e =>
        {
            e.ToTable("REGISTRO_PROGRESSO");
            e.HasKey(x => x.IdRegistroProgresso);
            e.Property(x => x.IdRegistroProgresso).HasColumnName("ID_REGISTRO_PROGRESSO");
            e.Property(x => x.IdMetaSemanal).HasColumnName("ID_META_SEMANAL").IsRequired();
            e.Property(x => x.DataRegistro).HasColumnName("DATA_REGISTRO").IsRequired();
            e.Property(x => x.QtdRealizada).HasColumnName("QTD_REALIZADA").IsRequired();
            e.Property(x => x.Obs).HasColumnName("OBS").HasMaxLength(400);
            e.Property(x => x.DtCriacao).HasColumnName("DT_CRIACAO").IsRequired();
            e.Property(x => x.DtAtualizacao).HasColumnName("DT_ATUALIZACAO");

            e.HasIndex(x => new { x.IdMetaSemanal, x.DataRegistro }).IsUnique();
            e.HasOne<MetaSemanal>().WithMany().HasForeignKey(x => x.IdMetaSemanal);
        });
    }
}
