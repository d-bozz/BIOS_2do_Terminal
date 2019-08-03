using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaViajesNacionales
    {
        void AgregarViajeNacional(ViajeNacional Vn);
        void EliminarViajeNacional(ViajeNacional Vn);
        void ModificarViajeNacional(ViajeNacional Vn);
        ViajeNacional BuscarViajeNacional(int pNumero);
        List<ViajeNacional> ListadoNacionales();
        List<ViajeNacional> ListadoSinPartirNac();
    }
}
