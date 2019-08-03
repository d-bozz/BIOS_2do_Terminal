using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Administracion.Servicio;

namespace Administracion
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //usar este para entregarlo
            Application.Run(new Login());

            //usar este para probar sin tener que loguerse siempre
            
            /*Empleado emp = new Empleado();
            emp.Nombre = "Login_Workaround";
            emp.Ci = "12345678";
            emp.Contrasena = "123456";
            Application.Run(new Menu(emp,new Login()));
            */
        }
    }
}
