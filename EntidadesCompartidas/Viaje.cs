using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public abstract class Viaje
    {
        //Atributos
        private int _numero;
        private DateTime _fechaSalida;
        private DateTime _fechaArribo;
        private int _cantidadAsientos;
        private Empleado _usuario;
        private Destino _destino;
        private Compania _compania;

        //Propiedades
        public int Numero
        {
            get { return _numero; }
            set
            {
                if (value > 0)
                {
                    _numero = value;
                }
                else
                {
                    throw new Exception("El numero debe ser mayor a 0.");
                }
            }
        }

        public DateTime FechaSalida
        {
            get { return _fechaSalida; }
            set
            {
                _fechaSalida = value;
            }
        }

        public DateTime FechaArribo
        {
            get { return _fechaArribo; }
            set
            {
                if (value > FechaSalida)
                {
                    _fechaArribo = value;
                }
                else
                {
                    throw new Exception("La fecha y hora del arribo debe ser mayor a la fecha y hora de salida.");
                }
            }
        }

        public int CantidadAsientos
        {
            get { return _cantidadAsientos; }
            set
            {
                if (value >= 0 && value <= 60)
                {
                    _cantidadAsientos = value;
                }
                else
                {
                    throw new Exception("El numero de asientos no es valido (0...60).");
                }
            }
        }

        public Empleado Usuario
        {
            get { return _usuario; }
            set
            {
                if (value != null)
                {
                    _usuario = value;
                }
                else
                {
                    throw new Exception("No existe un usuario con esa cedula de identidad.");
                }
            }
        }

        public Destino Destino
        {
            get { return _destino; }
            set
            {
                if (value != null)
                {
                    _destino = value;
                }
                else
                {
                    throw new Exception("El destino ingresado no existe.");
                }
            }
        }

        public Compania Compania
        {
            get { return _compania; }
            set
            {
                if (value != null)
                {
                    _compania = value;
                }
                else
                {
                    throw new Exception("No existe la compañia ingresada.");
                }
            }
        }

        //Constructor
        public Viaje(int pNumero, DateTime pFechaSalida, DateTime pFechaArribo, int pCantidadAsientos, Empleado pUsuario, 
            Destino pDestino, Compania pCompania)
        {
            Numero = pNumero;
            FechaSalida = pFechaSalida;
            FechaArribo = pFechaArribo;
            CantidadAsientos = pCantidadAsientos;
            Usuario = pUsuario;
            Destino = pDestino;
            Compania = pCompania;
        }

        public Viaje()
        {
           
        }
    }
}
