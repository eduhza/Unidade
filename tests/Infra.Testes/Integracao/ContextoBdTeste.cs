using System.Data.Common;
using Compartilhado.Primitivos;
using Infra.Dados;
using Microsoft.EntityFrameworkCore;

namespace Infra.Testes.Integracao;

public class ContextoBdTeste(DbConnection connection)
    : ContextoBd(new DbContextOptionsBuilder().UseNpgsql(connection).Options)
{
    public DbSet<EntidadeTeste> Entidades { get; set; }
}

public class EntidadeTeste : Entidade<int>
{
    public override int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;
}
