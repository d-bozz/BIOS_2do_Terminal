using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Persistencia;
using EntidadesCompartidas;

namespace Logica
{
    internal class LogicaEmpleados : ILogicaEmpleados
    {
        //Singleton*************************************
        private static LogicaEmpleados _instancia = null;

        private LogicaEmpleados()
        { 
        }

        public static LogicaEmpleados GetInstance()
        {
            if (_instancia == null)
                _instancia = new LogicaEmpleados();
            return _instancia;
        }
        //************************************************

        //Operaciones
        public Empleado Login(string pCi, string pPass)
        {
            return (Persistencia.FabricaPersistencia.getPersistenciaEmpleados().Login(pCi, pPass));
        }

        public void AgregarEmpleado(Empleado E)
        {
            Persistencia.FabricaPersistencia.getPersistenciaEmpleados().AgregarEmpleado(E);
        }

        public void EliminarEmpleado(Empleado E)
        {
            Persistencia.FabricaPersistencia.getPersistenciaEmpleados().EliminarEmpleado(E);
        }

        public void ModificarEmpleado(Empleado E)
        {
            Persistencia.FabricaPersistencia.getPersistenciaEmpleados().ModificarEmpleado(E);
        }

        public Empleado BuscarEmpleadoActivo(string pCi)
        {
            return (Persistencia.FabricaPersistencia.getPersistenciaEmpleados().BuscarEmpleadoActivo(pCi));
        }

        /*public List<Empleado> ListarEmpleados()
        {
            return (Persistencia.FabricaPersistencia.getPersistenciaEmpleados().ListarEmpleados());
        }
        */

    }
}
