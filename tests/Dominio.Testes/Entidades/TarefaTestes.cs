using Dominio.Entidades;
using Dominio.Enums;
using FluentAssertions;

namespace Dominio.Testes.Entidades;

public class TarefaTestes
{
    [Fact]
    public void CriarTarefa_ValoresValidos_DeveCriarTarefa()
    {
        // Arrange
        const string titulo = "Estudar";
        const string descricao = "Estudar todos os tópicos do exame";
        var data = DateTime.UtcNow.AddDays(10);

        // Act
        var resultado = Tarefa.CriarTarefa(titulo, descricao, data);

        // Assert
        resultado.Ok.Should().BeTrue();
        resultado.Dados.Titulo.Should().Be(titulo);
        resultado.Dados.Descricao.Should().Be(descricao);
        resultado.MensagemErro.Should().BeEmpty();
    }

    [Fact]
    public void CriarTarefa_ValoresInvalidos_DeveResultarEmErro()
    {
        // Arrange
        var titulo = new string('a', 25);
        var descricao = new string('a', 600);
        var data = DateTime.UtcNow.AddDays(-1);
        var mensagemErroEsperada = new List<string>
        {
            "O título deve ter entre 1 e 20 caracteres.",
            "A descrição deve ter entre 1 e 500 caracteres.",
            "A data deve ser futura."
        };

        // Act
        var resultado = Tarefa.CriarTarefa(titulo, descricao, data);

        // Assert
        resultado.Ok.Should().BeFalse();
        resultado.Dados.Should().Be(default);
        resultado.MensagemErro.Should().ContainAll(mensagemErroEsperada);
    }

    [Fact]
    public void AtualizarTarefa_ValoresValidos_DeveAtualizarTarefa()
    {
        // Arrange
        var tarefa = Tarefa.CriarTarefa
            ("Estudar", "Estudar todos os tópicos do exame", DateTime.UtcNow.AddDays(10)).Dados;
        const string novoTitulo = "Estudar para a prova";
        var novaData = DateTime.UtcNow.AddDays(5);

        // Act
        tarefa = tarefa.AtualizarTarefa(novoTitulo, tarefa.Descricao, tarefa.Status, novaData).Dados;

        // Assert
        tarefa.Titulo.Should().Be(novoTitulo);
        tarefa.Data.Should().Be(novaData);
        tarefa.Descricao.Should().Be("Estudar todos os tópicos do exame");
        tarefa.Status.Should().Be(StatusTarefa.NaoIniciada);
    }

    [Fact]
    public void AtualizarTitulo_ValoresValidos_DeveAtualizarTitulo()
    {
        // Arrange
        var tarefa = Tarefa.CriarTarefa(
                "Estudar",
                "Estudar todos os tópicos do exame",
                DateTime.UtcNow.AddDays(10))
            .Dados;
        const string novoTitulo = "ESTUDAR!!!!!";

        // Act
        tarefa = tarefa.AtualizarTitulo(novoTitulo).Dados;

        // Assert
        tarefa.Titulo.Should().Be(novoTitulo);
    }

    [Fact]
    public void AtualizarDescricao_ValoresValidos_DeveAtualizarDescricao()
    {
        // Arrange
        var tarefa = Tarefa.CriarTarefa(
                "Estudar",
                "Estudar todos os tópicos do exame",
                DateTime.UtcNow.AddDays(10))
            .Dados;
        var novaDescricao = "Reforçar clean architecture";

        // Act
        tarefa = tarefa.AtualizarDescricao(novaDescricao).Dados;

        // Assert
        tarefa.Descricao.Should().Be(novaDescricao);
    }

    [Fact]
    public void AtualizarStatus_ValoresValidos_DeveAtualizarStatus()
    {
        // Arrange
        var tarefa = Tarefa.CriarTarefa(
                "Estudar",
                "Estudar todos os tópicos do exame",
                DateTime.UtcNow.AddDays(10))
            .Dados;
        const StatusTarefa novoStatus = StatusTarefa.EmAndamento;

        // Act
        tarefa = tarefa.AtualizarStatus(novoStatus).Dados;

        // Assert
        tarefa.Status.Should().Be(novoStatus);
    }

    [Fact]
    public void AtualizarData_ValoresValidos_DeveAtualizarData()
    {
        // Arrange
        var tarefa = Tarefa.CriarTarefa(
                "Estudar",
                "Estudar todos os tópicos do exame",
                DateTime.UtcNow.AddDays(10))
            .Dados;
        var novaData = DateTime.UtcNow.AddDays(5);

        // Act
        tarefa = tarefa.AtualizarData(novaData).Dados;

        // Assert
        tarefa.Data.Should().Be(novaData);
    }

    [Fact]
    public void ConcluirTarefa_ValoresValidos_DeveConcluirTarefa()
    {
        // Arrange
        var tarefa = Tarefa.CriarTarefa(
                "Estudar",
                "Estudar todos os tópicos do exame",
                DateTime.UtcNow.AddDays(10))
            .Dados;

        // Act
        tarefa = tarefa.Concluir().Dados;

        // Assert
        tarefa.Status.Should().Be(StatusTarefa.Concluida);
    }
}
