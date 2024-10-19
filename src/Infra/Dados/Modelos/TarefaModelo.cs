using Compartilhado.Primitivos;
using Dominio.Enums;
using System.ComponentModel.DataAnnotations;

namespace Infra.Dados.Modelos;

public class TarefaModelo : EntidadeAuditavel<Guid>
{
    [Key]
    public override Guid Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Descricao { get; set; } = string.Empty;

    public StatusTarefa Status { get; set; } = StatusTarefa.NaoIniciada;

    public DateTime DataHora { get; set; }
}