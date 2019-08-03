using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaViajes
    {
        
        void AgregarViaje(Viaje V);
        void EliminarViaje(Viaje V);
        void ModificarViaje(Viaje V);

        List<Viaje> Listar();
        List<Viaje> ListarSinPartir();
        Viaje BuscarViaje(int pNumero);


    }
}
