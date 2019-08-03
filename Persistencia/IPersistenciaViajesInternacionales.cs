using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
namespace Persistencia
{
    public interface IPersistenciaViajesInternacionales
    {
        void AgregarViajeInternacional(ViajeInternacional VI);
        void EliminarViajeInternacional(ViajeInternacional VI);
        void ModificarViajeInternacional(ViajeInternacional VI);
        ViajeInternacional BuscarViajeInter(int pNumero);
        List<ViajeInternacional> ListadoInter();
        List<ViajeInternacional> ListadoSinPartirInter();
    }
}
