using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class Compania
    {
        //Atributos
        private string _nombre;
        private string _direccion;
        private int _telefono;


        //Propiedades
        public string Direccion
        {
            get { return _direccion.ToUpper(); }

           set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 50)
                    { _direccion = value; }
                    else
                    { throw new Exception("La direccion no puede tener mas de 50 caracteres."); }
                }
                else
                {
                    throw new Exception("La direccion no puede ser vacia.");
                }
            }        
         }
        
        public string Nombre
        {
            get { return _nombre.ToUpper(); }
            set
            {
                if (value.Length != 0)
                    if (value.Length <= 50)
                        _nombre = value;
                    else
                        throw new Exception("El nombre no puede tener mas de 50 caracteres.");
                else
                {
                    throw new Exception("El nombre de la compañia no puede ser vacio.");
                }
            }
        }

        public int Telefono
        {
            get { return _telefono; }
            set
            {
                if (value > 0)
                {
                    _telefono = value;
                }
                else
                {
                    throw new Exception("El numero de telefono no puede ser negativo.");
                }
            }
        }


        //constructor
        public Compania(string pNombre, string pDireccion, int pTelefono)
        {
            Nombre = pNombre;
            Direccion = pDireccion;
            Telefono = pTelefono;      
        }

        public Compania()
        { }


    }
}
