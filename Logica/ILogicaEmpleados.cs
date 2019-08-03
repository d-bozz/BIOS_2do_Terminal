using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaEmpleados
    {
        Empleado Login(string pCi, string pPass);
        void AgregarEmpleado(Empleado E);
        void EliminarEmpleado(Empleado E);
        void ModificarEmpleado(Empleado E);
        Empleado BuscarEmpleadoActivo(string pCi);
        //List<Empleado> ListarEmpleados();
    }
}
