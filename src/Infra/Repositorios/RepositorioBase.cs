using Compartilhado.Dominio.Repositorios;
using Compartilhado.Primitivos;
using Infra.Dados;
using Microsoft.Extensions.Logging;

namespace Infra.Repositorios;

public abstract class RepositorioBase<T, TK>(
    ContextoBd contextoBd,
    ILogger<RepositorioBase<T, TK>> logger)
    : IRepositorioBase<T, TK> where T : class
{
    private readonly ContextoBd _contextoBd = contextoBd;
    private readonly ILogger<RepositorioBase<T, TK>> _logger = logger;

    public async Task<T> AdicionarAsync(T entidade)
    {
        try
        {
            if (entidade is EntidadeAuditavel<TK> entidadeAuditavel)
            {
                //TODO: Implementar CriadoPor
                entidadeAuditavel.CriadoEm = DateTime.UtcNow;
            }

            await _contextoBd.Set<T>().AddAsync(entidade);
            return entidade;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao adicionar entidade: {Erro}", e.Message);
            throw;
        }
    }

    public T Atualizar(T entidade)
    {
        try
        {
            if (entidade is EntidadeAuditavel<TK> entidadeAuditavel)
            {
                //TODO: Implementar AtualizadoPor
                entidadeAuditavel.AtualizadoEm = DateTime.UtcNow;
            }

            _contextoBd.Set<T>().Update(entidade);
            return entidade;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao atualizar entidade: {Erro}", e.Message);
            throw;
        }
    }

    public async Task<T> BuscarAsync(TK id)
    {
        try
        {
            var entidade = await _contextoBd.Set<T>().FindAsync(id);
            //TODO: Criar Exceções Personalizadas
            return entidade ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao buscar entidade: {Erro}", e.Message);
            throw;
        }
    }

    public bool Remover(T entidade)
    {
        try
        {
            _contextoBd.Set<T>().Remove(entidade);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao remover entidade: {Erro}", e.Message);
            throw;
        }
    }
}
