using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class ViajeNacional:Viaje
    {
        //Atributos
        private int _paradas;

        //Propiedades
        public int Paradas
        {
            get { return _paradas; }
            set
            {
                if (value >= 0)
                {
                    _paradas = value;
                }
                else
                {
                    throw new Exception("El numero de paradas deben ser mayor o igual a 0.");
                }
            }
        }

        //Constructor
        public ViajeNacional(int pNumero, DateTime pFechaSalida, DateTime pFechaArribo, int pCantidadAsientos, Empleado pUsuario, Destino pDestino, Compania pCompania, int pParadas)
            : base(pNumero, pFechaSalida, pFechaArribo, pCantidadAsientos, pUsuario, pDestino, pCompania)
        {
            Paradas = pParadas;
        }

        public ViajeNacional()
            : base()
        {
        }
    }
}
