using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Persistencia;
using EntidadesCompartidas;

namespace Logica
{
    internal class LogicaCompanias : ILogicaCompanias
    {
         //Singleton
        private static LogicaCompanias _instancia = null;

        private LogicaCompanias()
        { 
        }

        public static LogicaCompanias GetInstance()
        {
            if (_instancia == null)
                _instancia = new LogicaCompanias();
            return _instancia;
        }
        //************************************************

        //Operaciones
        public void AgregarCompania(Compania C)
        {
            Persistencia.FabricaPersistencia.getPersistenciaCompanias().AgregarCompania(C);
        }

        public void EliminarCompania(Compania C)
        {
            Persistencia.FabricaPersistencia.getPersistenciaCompanias().EliminarCompania(C);
        }

        public void ModificarCompania(Compania C)
        {
            Persistencia.FabricaPersistencia.getPersistenciaCompanias().ModificarCompania(C); 
        }

        public Compania BuscarCompaniaActiva(string pNombre)
        {
            return (Persistencia.FabricaPersistencia.getPersistenciaCompanias().BuscarCompaniaActiva(pNombre));
        }

        public List<Compania> ListarCompanias()
        { 
            
             return (Persistencia.FabricaPersistencia.getPersistenciaCompanias().ListarCompanias());
        }

       
    }
}
