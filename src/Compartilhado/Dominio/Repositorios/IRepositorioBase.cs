namespace Compartilhado.Dominio.Repositorios;

public interface IRepositorioBase<T, TK>
    where T : class
{
    Task<T> AdicionarAsync(T entidade);
    // Task AdicionarVariosAsync(List<T> entidades);
    T Atualizar(T entidade);
    // void AtualizarVarios(List<T> entidades);
    Task<T> BuscarAsync(TK id);
    // ListaPaginada<T> BuscarTodos(Paginacao paginacao);
    bool Remover(T entidade);
}