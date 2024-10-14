namespace Compartilhado.Primitivos;

public abstract class ObjetoValor : IEquatable<ObjetoValor>
{
    protected abstract IEnumerable<object> PegarValoresAtomicos();


    public bool Equals(ObjetoValor? outroObjeto)
    {
        return outroObjeto is not null && ValoresSaoIguais(outroObjeto);
    }

    public override bool Equals(object? obj)
    {
        return obj is ObjetoValor outroObjeto && ValoresSaoIguais(outroObjeto); 
    }
    
    public override int GetHashCode()
    {
        return PegarValoresAtomicos()
            .Aggregate(
                default(int),
                HashCode.Combine);
    }

    private bool ValoresSaoIguais(ObjetoValor outroObjeto)
    {
        return PegarValoresAtomicos().SequenceEqual(outroObjeto.PegarValoresAtomicos());
    }
}