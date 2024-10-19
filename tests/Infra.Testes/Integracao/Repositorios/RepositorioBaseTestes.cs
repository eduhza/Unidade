using FluentAssertions;
using Infra.Repositorios;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infra.Testes.Integracao.Repositorios;

[Collection("InfraCollection")]
public class RepositorioBaseTestes(InfraFixture fixture) : IAsyncLifetime
{
    private readonly ContextoBdTeste _contextoBd = fixture.Contexto;

    private readonly RepositorioBaseFilho _repositorio =
        new(fixture.Contexto, new Mock<ILogger<RepositorioBase<EntidadeTeste, int>>>().Object);

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task AdicionarAsync_DeveAdicionarEntidade()
    {
        // Arrange
        var entidade = new EntidadeTeste { Nome = "Teste" };

        // Act
        var resultado = await _repositorio.AdicionarAsync(entidade);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Teste", resultado.Nome);
    }

    [Fact]
    public void Atualizar_DeveAtualizarEntidade()
    {
        // Arrange
        var entidade = new EntidadeTeste { Nome = "Teste" };
        _contextoBd.Entidades.Add(entidade);
        _contextoBd.SaveChanges();

        entidade.Nome = "Teste Atualizado";

        // Act
        var resultado = _repositorio.Atualizar(entidade);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Teste Atualizado", resultado.Nome);
    }

    [Fact]
    public async Task BuscarAsync_DeveRetornarEntidade()
    {
        // Arrange
        var entidade = new EntidadeTeste { Nome = "Teste" };
        _contextoBd.Entidades.Add(entidade);
        await _contextoBd.SaveChangesAsync();

        // Act
        var resultado = await _repositorio.BuscarAsync(entidade.Id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Teste", resultado.Nome);
    }

    [Fact]
    public async Task Remover_DeveRemoverEntidade()
    {
        // Arrange
        var entidade = new EntidadeTeste { Nome = "Teste" };
        _contextoBd.Entidades.Add(entidade);
        await _contextoBd.SaveChangesAsync();

        // Act
        var resultado = _repositorio.Remover(entidade);
        await _contextoBd.SaveChangesAsync();
        Func<Task> act = async () => await _repositorio.BuscarAsync(entidade.Id);

        // Assert
        resultado.Should().BeTrue();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    private class RepositorioBaseFilho(
        ContextoBdTeste contextoBd,
        ILogger<RepositorioBase<EntidadeTeste, int>> logger)
        : RepositorioBase<EntidadeTeste, int>(contextoBd, logger);
}
