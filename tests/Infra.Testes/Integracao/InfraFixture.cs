using System.Data.Common;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Infra.Testes.Integracao;

public class InfraFixture : IAsyncLifetime
{
    /// <summary>
    ///     Define o container de banco de dados PostgreSql.
    ///     WithPortBinding pode ser comentado, fixei a porta para poder acessar o banco de dados com o Dbeaver.
    ///     Se comentado, a porta será sempre aleatória.
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("ErpCore")
        .WithUsername("admin")
        .WithPassword("123123")
        //.WithPortBinding(64111, 5432)
        .Build();

    private DbConnection _dbConnection = default!;
    public ContextoBdTeste Contexto { get; private set; }

    public async Task InitializeAsync()
    {
        try
        {
            await _dbContainer.StartAsync();
            var connectionString = $"{_dbContainer.GetConnectionString()};Include Error Detail=true;";
            _dbConnection = new NpgsqlConnection(connectionString);

            Contexto = new ContextoBdTeste(_dbConnection);
            await Contexto.Database.EnsureCreatedAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DisposeAsync() => await Contexto.DisposeAsync();
}
