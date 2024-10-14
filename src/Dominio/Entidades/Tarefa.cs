using Compartilhado.Primitivos;
using Dominio.Enums;

namespace Dominio.Entidades;

public record Tarefa(
    Guid Id,
    string Titulo,
    string Descricao,
    StatusTarefa Status,
    DateTime Data,
    EntidadeAuditavel<int> EntidadeAuditavel)
{
    public static Resultado<Tarefa> CriarTarefa(string titulo, string descricao, DateTime data,
        EntidadeAuditavel<int> entidadeAuditavel)
    {
        if (string.IsNullOrWhiteSpace(titulo) || titulo.Length > 20)
            return Resultado<Tarefa>.Falha("O título da tarefa deve ter entre 1 e 20 caracteres");

        if (string.IsNullOrWhiteSpace(descricao) || descricao.Length > 500)
            return Resultado<Tarefa>.Falha("A descrição da tarefa deve ter até 500 caracteres");

        if (data < DateTime.UtcNow)
            return Resultado<Tarefa>.Falha("O prazo final da tarefa não pode ser no passado");

        return Resultado<Tarefa>.Sucesso(new Tarefa(Guid.NewGuid(), titulo, descricao, StatusTarefa.NaoIniciada, data,
            entidadeAuditavel));
    }

    public Resultado<Tarefa> AtualizarTitulo(string titulo) => this with { Titulo = titulo };

    public Resultado<Tarefa> AtualizarDescricao(string descricao) => this with { Descricao = descricao };

    public Resultado<Tarefa> AtualizarStatus(StatusTarefa status) => this with { Status = status };

    public Resultado<Tarefa> AtualizarData(DateTime data) => this with { Data = data };
    
    public Resultado<Tarefa> Finalizar() => this with { Status = StatusTarefa.Concluida };

    public Tarefa AtualizarTarefa(Tarefa tarefa) => this with
    {
        Titulo = tarefa.Titulo,
        Descricao = tarefa.Descricao,
        Status = tarefa.Status,
        Data = tarefa.Data,
        EntidadeAuditavel = tarefa.EntidadeAuditavel
    };
}