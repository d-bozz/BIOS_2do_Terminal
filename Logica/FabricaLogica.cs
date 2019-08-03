using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILogicaCompanias getLogicaCompanias()
        {
            return LogicaCompanias.GetInstance();
        }

        public static ILogicaDestinos getLogicaDestinos()
        {
            return LogicaDestinos.GetInstance();
        }

        public static ILogicaEmpleados getLogicaEmpleados()
        {
            return LogicaEmpleados.GetInstance();
        }

        public static ILogicaViajes getLogicaViajes()
        {
            return LogicaViajes.GetInstance();
        }
    }
}
