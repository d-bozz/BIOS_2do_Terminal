using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static IPersistenciaCompanias getPersistenciaCompanias()
        {
            return (PersistenciaCompanias.GetInstancia());
        }

        public static IPersistenciaDestinos getPersistenciaDestinos()
        {
            return (PersistenciaDestinos.GetInstancia());
        }

        public static IPersistenciaEmpleados getPersistenciaEmpleados()
        {
            return (PersistenciaEmpleados.GetInstancia());
        }

        public static IPersistenciaViajesNacionales getIPersistenciaViajesNacionales()
        {
            return (PersistenciaViajesNacionales.GetInstancia());
        }

        public static IPersistenciaViajesInternacionales getIPersistenciaViajesInternacionales()
        {
            return (PersistenciaViajesInternacionales.GetInstancia());
        }
    }
}
