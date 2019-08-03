using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Persistencia;
using EntidadesCompartidas;

namespace Logica
{
    internal class LogicaViajes : ILogicaViajes
    {
        //Singleton*************************************
        private static LogicaViajes _instancia = null;
        private LogicaViajes()
        {
        }

        public static LogicaViajes GetInstance()
        {
            if (_instancia == null)
                _instancia = new LogicaViajes();
            return _instancia;
        }
        //***************************************************

        //Operaciones****************************************

        public void AgregarViaje(Viaje V)
        {
            if (V.FechaSalida < DateTime.Now)
                throw new Exception("Error: La fecha del viaje debe estar en el futuro.");

            List<Viaje> lista = Listar();

            TimeSpan doshoras = new TimeSpan(2, 0, 0);
            TimeSpan menosdoshoras = new TimeSpan(-2, 0, 0);
            foreach (Viaje vi in lista) 
            {
                if (V.Destino.Cod == vi.Destino.Cod && doshoras >= V.FechaSalida.Subtract(vi.FechaSalida) && menosdoshoras <= V.FechaSalida.Subtract(vi.FechaSalida))
                    throw new Exception("No pueden salir dos viajes al mismo lugar con menos de 2 horas de diferencia.");
            }

            if (V is ViajeInternacional)
                Persistencia.FabricaPersistencia.getIPersistenciaViajesInternacionales().AgregarViajeInternacional((ViajeInternacional)V);
            else
                Persistencia.FabricaPersistencia.getIPersistenciaViajesNacionales().AgregarViajeNacional((ViajeNacional)V);
        }

        public void EliminarViaje(Viaje V)
        {
            if (V is ViajeInternacional)
                Persistencia.FabricaPersistencia.getIPersistenciaViajesInternacionales().EliminarViajeInternacional((ViajeInternacional)V);
            else
                Persistencia.FabricaPersistencia.getIPersistenciaViajesNacionales().EliminarViajeNacional((ViajeNacional)V);
        }

        public void ModificarViaje(Viaje V)
        {
            if (V.FechaSalida < DateTime.Now)
                throw new Exception("Error: La fecha del viaje debe estar en el futuro.");

            List<Viaje> lista = Listar();

            TimeSpan doshoras = new TimeSpan(2, 0, 0);
            TimeSpan menosdoshoras = new TimeSpan(-2, 0, 0);
            foreach (Viaje vi in lista) 
            {
                if (V.Numero != vi.Numero && V.Destino.Cod == vi.Destino.Cod && doshoras >= V.FechaSalida.Subtract(vi.FechaSalida) && menosdoshoras <= V.FechaSalida.Subtract(vi.FechaSalida))
                    throw new Exception("No pueden salir dos viajes al mismo lugar con menos de 2 horas de diferencia.");
            }

            if (V is ViajeInternacional)
                Persistencia.FabricaPersistencia.getIPersistenciaViajesInternacionales().ModificarViajeInternacional((ViajeInternacional)V);
            else
                Persistencia.FabricaPersistencia.getIPersistenciaViajesNacionales().ModificarViajeNacional((ViajeNacional)V);
        }


        public Viaje BuscarViaje(int pNumero)
        {
            Viaje v = Persistencia.FabricaPersistencia.getIPersistenciaViajesInternacionales().BuscarViajeInter(pNumero);

            if (v == null)
                v = Persistencia.FabricaPersistencia.getIPersistenciaViajesNacionales().BuscarViajeNacional(pNumero);

            return v;
        }

        public List<Viaje> Listar()
        {
            List<Viaje> Lista = new List<Viaje>();

            Lista.AddRange(Persistencia.FabricaPersistencia.getIPersistenciaViajesInternacionales().ListadoInter());
            Lista.AddRange(Persistencia.FabricaPersistencia.getIPersistenciaViajesNacionales().ListadoNacionales());
            return Lista;
        }

         public List<Viaje> ListarSinPartir()
        {
            List<Viaje> Lista = new List<Viaje>();

            Lista.AddRange(Persistencia.FabricaPersistencia.getIPersistenciaViajesInternacionales().ListadoSinPartirInter());
            Lista.AddRange(Persistencia.FabricaPersistencia.getIPersistenciaViajesNacionales().ListadoSinPartirNac());
            return Lista;
        }
    }
}
