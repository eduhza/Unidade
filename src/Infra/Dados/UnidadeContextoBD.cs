using Infra.Dados.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Infra.Dados;

public class UnidadeContextoBd(DbContextOptions<UnidadeContextoBd> options) : DbContext(options)
{
    public DbSet<TarefaModelo> Tarefas { get; set; }
}