using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{   
    [Serializable]
    public class Destino
    {
        //Atributos
        private string _cod;
        private string _ciudad;
        private string _pais;
        private List<Facilidades> _LasFacilidades;
      

        //Propiedades
        public string Ciudad
        {
            get { return _ciudad.ToUpper(); }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 50 && !value.All(char.IsNumber))
                    { _ciudad = value; }
                    else
                    { throw new Exception("La ciudad no puede tener mas de 50 caracteres."); }
                }
                else
                {
                    throw new Exception("La ciudad no puede ser vacia.");
                }
            }        
        }

        public string Pais
        {
            get { return _pais.ToUpper(); }
            set
            {
                if (value.ToLower() == "argentina" || value.ToLower() == "brasil" || value.ToLower() == "paraguay" ||
                    value.ToLower() == "uruguay")
                {
                    _pais = value.ToLower();
                }
                else
                {
                    throw new Exception("Se debe ingresar un pais del mercosur.");
                }

            }
        }

        public string Cod
        {
            get { return _cod.ToUpper(); }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length == 3 && value.All(char.IsLetter))
                    { 
                        _cod = value; 
                    }
                    else
                    { throw new Exception("El codigo debe tener 3 letras."); }
                }
                else
                {
                    throw new Exception("El codigo no puede ser vacio.");
                }
            }        
        }

        public List<Facilidades> LasFacilidades
        {
            get { return _LasFacilidades; }
            set { _LasFacilidades = value; }
        }
       

        //Constructor
        public Destino (string pCod, string pCiudad, string pPais, List<Facilidades> pFacilidades)
        {
            Cod = pCod;
            Ciudad = pCiudad;
            Pais = pPais;
            LasFacilidades = pFacilidades;
        }
        
        public Destino()
        {
        }
        
      
        //Operaciones
        public void AgregarFacilidad(Facilidades pFacilidad) 
        {
            int i = 0;
            while (i < _LasFacilidades.Count)
            {
                if (_LasFacilidades[i].Facilidad == pFacilidad.Facilidad)
                    throw new Exception("La facilidad "+ pFacilidad+ "ya se encuentra en el destino.");
                i++;
            }
            _LasFacilidades.Add(new Facilidades(pFacilidad.Facilidad)); 
        }

        public void EliminarTodasFacilidades()
        {
            _LasFacilidades.Clear();
        }

    }
}
