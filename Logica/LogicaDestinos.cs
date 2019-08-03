using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Persistencia;
using EntidadesCompartidas;

namespace Logica
{
    internal class LogicaDestinos : ILogicaDestinos
    {
         //Singleton*************************************
        private static LogicaDestinos _instancia = null;

        private LogicaDestinos()
        { 
        }

        public static LogicaDestinos GetInstance()
        {
            if (_instancia == null)
                _instancia = new LogicaDestinos();
            return _instancia;
        }
        //************************************************

        //Operaciones
        public void AgregarDestino(Destino D)
        {
            if (D.Pais.ToLower() == "argentina" || D.Pais.ToLower() == "brasil" || D.Pais.ToLower() == "paraguay" ||
                    D.Pais.ToLower() == "uruguay")
            {
                Persistencia.FabricaPersistencia.getPersistenciaDestinos().AgregarDestino(D);
            }
            else
            {
                throw new Exception("Se debe ingresar un pais del mercosur.");
            }            
        }

        public void EliminarDestino(Destino D)
        {
            Persistencia.FabricaPersistencia.getPersistenciaDestinos().EliminarDestino(D);
        }

        public void ModificarDestino(Destino D)
        {
            Persistencia.FabricaPersistencia.getPersistenciaDestinos().ModificarDestino(D);
        }

        public Destino BuscarDestino(string pCod)
        {
            return (Persistencia.FabricaPersistencia.getPersistenciaDestinos().BuscarDestinoActivo(pCod));
        }


        public List<Destino> ListarDestinos()
        {
            return (Persistencia.FabricaPersistencia.getPersistenciaDestinos().ListarDestinos());
        }
    }
}
