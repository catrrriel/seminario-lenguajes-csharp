using SGE.Dominio.Comun;

namespace SGE.Dominio.Tramites;

public record class ContenidoTramite
{
    public string Valor{ get;}

    public ContenidoTramite( string valor)
    {
        if(string.IsNullOrEmpty(valor))
            throw new DominioException("El contenido del tramite no puede estar vacio");
        
        Valor = valor;
    }

    public override string ToString() => Valor;
}