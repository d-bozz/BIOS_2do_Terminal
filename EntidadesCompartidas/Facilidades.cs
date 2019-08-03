using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class Facilidades
    {
        //Atributos
        private string _facilidad;


        //Propiedades
        public string Facilidad
        {
            get 
            { return _facilidad; }
            set
            {
                if (value.Length <= 50 && value.Length > 0)
                    _facilidad = value;
                else
                    throw new Exception("La Facilidad debe tener como maximo 50 caracteres y no ser vacia.");
            }
        }


        //Constructor
        public Facilidades(string pFacilidad)
        {
            Facilidad = pFacilidad;
        }

        public Facilidades()
        { }
    }
}
