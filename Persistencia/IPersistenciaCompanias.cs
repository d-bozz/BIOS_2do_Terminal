using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaCompanias
    {
        void AgregarCompania(Compania C);
        void EliminarCompania(Compania C);
        void ModificarCompania(Compania C);
        Compania BuscarCompaniaActiva(string pNombre);
        List<Compania> ListarCompanias();
    }
}
