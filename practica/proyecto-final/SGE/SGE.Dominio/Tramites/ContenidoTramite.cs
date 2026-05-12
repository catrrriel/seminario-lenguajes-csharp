using SGE.Dominio.Comunes;

namespace SGE.Dominio.Tramites;

public record class ContenidoTramite
{
    public string Valor{ get;}

    public ContenidoTramite( string valor)
    {
        if(valor.Equals(""))
            throw new DominioException("El contenido del tramite no puede estar vacio");
        
        Valor = valor;
    }

    public override string ToString() => Valor;
}