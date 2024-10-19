namespace Compartilhado.Primitivos;

public record Resultado<T>
{
    private Resultado(T dados, bool ok, string mensagemErro)
    {
        Dados = dados;
        Ok = ok;
        MensagemErro = mensagemErro;
    }

    public T Dados { get; }
    public bool Ok { get; }
    public string MensagemErro { get; }

    public static Resultado<T> Sucesso(T dado)
    {
        if (dado is null)
        {
            throw new ArgumentException(null, nameof(dado));
        }

        return new Resultado<T>(dado, true, string.Empty);
    }

    public static Resultado<T> Falha(string mensagemErro)
    {
        if (string.IsNullOrWhiteSpace(mensagemErro))
        {
            throw new ArgumentException("Mensagem de erro não pode ser nula ou vazia.", nameof(mensagemErro));
        }

        return new Resultado<T>(default!, false, mensagemErro);
    }

    /// <summary>
    ///     Se o resultado for comparado com um booleano, ele retornará o valor de EhSucesso
    /// </summary>
    /// <param name="resultado"></param>
    /// <returns>EhSucesso</returns>
    public static implicit operator bool(Resultado<T> resultado) => resultado.Ok;

    /// <summary>
    ///     Se o resultado for comparado com um T, ele retornará o valor de Dado
    /// </summary>
    /// <param name="resultado"></param>
    /// <returns>Dado</returns>
    public static implicit operator T(Resultado<T> resultado) => resultado.Dados;

    /// <summary>
    ///     Se o resultado for comparado com um Resultado, ele retornará Sucesso
    /// </summary>
    /// <param name="dado"></param>
    /// <returns>Sucesso</returns>
    public static implicit operator Resultado<T>(T dado) => Sucesso(dado);

    /// <summary>
    ///     Se o resultado for comparado com um Resultado, ele retornará Falha
    /// </summary>
    /// <param name="mensagemErro"></param>
    /// <returns>Falha</returns>
    public static implicit operator Resultado<T>(string mensagemErro) => Falha(mensagemErro);
}
