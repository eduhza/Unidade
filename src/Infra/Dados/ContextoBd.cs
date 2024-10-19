using EntityFramework.Exceptions.PostgreSQL;
using Infra.Dados.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Infra.Dados;

public class ContextoBd(DbContextOptions options) : DbContext(options)
{
    public DbSet<TarefaModelo> Tarefas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder
            .UseExceptionProcessor()
            .EnableSensitiveDataLogging();
}
