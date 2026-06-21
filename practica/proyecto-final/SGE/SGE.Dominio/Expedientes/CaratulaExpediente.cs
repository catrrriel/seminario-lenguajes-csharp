using SGE.Dominio.Comun;

namespace SGE.Dominio.Expedientes;

public record class CaratulaExpediente
{
    public string Valor{ get; private init;} = "";

    public CaratulaExpediente( string valor)
    {
        if(string.IsNullOrEmpty(valor))
            throw new DominioException("La caratula no puede estar vacia");
        
        Valor = valor;
    }

    protected CaratulaExpediente() { }

    public override string ToString() => Valor;
}