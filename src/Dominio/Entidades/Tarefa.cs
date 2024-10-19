using Compartilhado.Primitivos;
using Dominio.Enums;

namespace Dominio.Entidades;

public record Tarefa(
    Guid Id,
    string Titulo,
    string Descricao,
    StatusTarefa Status,
    DateTime Data)
{
    public readonly bool EntidadeAuditavel = true;

    public static Resultado<Tarefa> CriarTarefa(string titulo, string descricao, DateTime data)
    {
        var resultadoValidacao = Validar(titulo, descricao, data);
        return resultadoValidacao.Ok
            ? Resultado<Tarefa>.Sucesso(new Tarefa(Guid.NewGuid(), titulo, descricao, StatusTarefa.NaoIniciada, data))
            : Resultado<Tarefa>.Falha(resultadoValidacao.MensagemErro);
    }

    public Resultado<Tarefa> AtualizarStatus(StatusTarefa status) => this with { Status = status };

    public Resultado<Tarefa> AtualizarTitulo(string titulo)
    {
        var resultadoValidacao = ValidarTitulo(titulo);
        return resultadoValidacao.Ok
            ? this with { Titulo = titulo }
            : Resultado<Tarefa>.Falha(resultadoValidacao.MensagemErro);
    }

    public Resultado<Tarefa> AtualizarDescricao(string descricao)
    {
        var resultadoValidacao = ValidarDescricao(descricao);
        return resultadoValidacao.Ok
            ? this with { Descricao = descricao }
            : Resultado<Tarefa>.Falha(resultadoValidacao.MensagemErro);
    }

    public Resultado<Tarefa> AtualizarData(DateTime data)
    {
        var resultadoValidacao = ValidarData(data);
        return resultadoValidacao.Ok
            ? this with { Data = data }
            : Resultado<Tarefa>.Falha(resultadoValidacao.MensagemErro);
    }

    public Resultado<Tarefa> Concluir() => this with { Status = StatusTarefa.Concluida };

    public Resultado<Tarefa> AtualizarTarefa(string titulo, string descricao, StatusTarefa status, DateTime data) =>
        this with { Titulo = titulo, Descricao = descricao, Status = status, Data = data };

    private static Resultado<(string, string, DateTime)> Validar(string titulo, string descricao, DateTime data)
    {
        List<string> erros = [];

        var resultadoTitulo = ValidarTitulo(titulo);
        if (!resultadoTitulo.Ok)
        {
            erros.Add(resultadoTitulo.MensagemErro);
        }

        var resultadoDescricao = ValidarDescricao(descricao);
        if (!resultadoDescricao.Ok)
        {
            erros.Add(resultadoDescricao.MensagemErro);
        }

        var resultadoData = ValidarData(data);
        if (!resultadoData.Ok)
        {
            erros.Add(resultadoData.MensagemErro);
        }

        return erros.Count != 0
            ? Resultado<(string, string, DateTime)>.Falha(string.Join(Environment.NewLine, erros))
            : Resultado<(string, string, DateTime)>.Sucesso((titulo, descricao, data));
    }

    private static Resultado<string> ValidarTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo) || titulo.Length > 20)
        {
            return Resultado<string>.Falha("O título deve ter entre 1 e 20 caracteres.");
        }

        return Resultado<string>.Sucesso(titulo);
    }

    private static Resultado<string> ValidarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao) || descricao.Length > 500)
        {
            return Resultado<string>.Falha("A descrição deve ter entre 1 e 500 caracteres.");
        }

        return Resultado<string>.Sucesso(descricao);
    }

    private static Resultado<DateTime> ValidarData(DateTime data) =>
        data <= DateTime.UtcNow
            ? Resultado<DateTime>.Falha("A data deve ser futura.")
            : Resultado<DateTime>.Sucesso(data);
}
