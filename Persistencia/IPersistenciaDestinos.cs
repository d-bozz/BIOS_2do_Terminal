using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaDestinos
    {
        void AgregarDestino( Destino D);
        void EliminarDestino(Destino D);
        void ModificarDestino(Destino D);
        Destino BuscarDestinoActivo(string pCod);
        List<Destino> ListarDestinos();
    }
}
