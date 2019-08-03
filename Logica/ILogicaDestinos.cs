using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaDestinos
    {
        void AgregarDestino(Destino D);
        void EliminarDestino(Destino D);
        void ModificarDestino(Destino D);
        Destino BuscarDestino(string pCod);
        List<Destino> ListarDestinos();
    }
}
