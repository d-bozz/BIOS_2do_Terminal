using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class Empleado
    {
        //Atributos
        private string _ci;
        private string _contrasena;
        private string _nombre;
       
        //Propiedades

        public string Ci
        {
            get { return _ci; }
            set
            {
                if (value.Trim().Length > 8 || value.Trim().Length < 8)
                    throw new Exception("El largo de la cedula de identidad no es correcto.");

                if (value.Trim().Length == 0)
                    throw new Exception("la cedula de identidad no puede ser vacia.");

                try
                {
                    Convert.ToInt64(value);
                }
                catch
                {
                    throw new Exception("El campo cedula solo puede tener numeros.");
                }

                _ci = value;
            }
        }

        public string Contrasena
        {
            get { return _contrasena; }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length == 6)
                    { _contrasena = value; }
                    else
                    { throw new Exception("La contraseña debe tener 6 caracteres."); }
                }
                else
                {
                    throw new Exception("La contraseña no puede ser vacia.");
                }
            }        
        }

        public string Nombre
        {
            get { return _nombre.ToUpper(); }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 50)
                    { _nombre = value; }
                    else
                    { throw new Exception("El nombre no puede tener mas de 50 caracteres."); }
                }
                else
                {
                    throw new Exception("El nombre no puede ser vacio.");
                }
            }       
        }

        //Constructor
        public Empleado(string pCi, string pContrasena, string pNombre)
        {
            Ci = pCi;
            Contrasena = pContrasena;
            Nombre = pNombre;
        }

        public Empleado()
        {
        }
        
    }
}
