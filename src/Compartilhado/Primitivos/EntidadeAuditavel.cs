namespace Compartilhado.Primitivos;

public abstract class EntidadeAuditavel<T> : Entidade<T>
{
    public string CriadoPor { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public string AtualizadoPor { get; set; } = string.Empty;
    public DateTime? AtualizadoEm { get; set; }
}