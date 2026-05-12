using SGE.Dominio.Comunes;

namespace SGE.Dominio.Expedientes;

public record class CaratulaExpediente
{
    public string Valor{ get;}

    public CaratulaExpediente( string valor)
    {
        if(string.IsNullOrEmpty(valor))
            throw new DominioException("La caratula no puede estar vacia");
        
        Valor = valor;
    }

    public override string ToString() => Valor;
}