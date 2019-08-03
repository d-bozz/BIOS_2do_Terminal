using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    [Serializable]
    public class ViajeInternacional : Viaje
    {
        //Atributos
        private bool _servicioABordo;
        private string _documentos;


        //Propiedades
        public bool ServicioABordo
        {
            get { return _servicioABordo; }
            set { _servicioABordo = value; }
        }

        public string Documentos
        {
            get { return _documentos; }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 50)
                    { _documentos = value; }
                    else
                    { throw new Exception("El documento no puede tener mas de 50 caracteres."); }
                }
                else
                {
                    throw new Exception("El documento no puede ser vacio.");
                }
            }
        }

        //Constructor
        public ViajeInternacional(int pNumero, DateTime pFechaSalida, DateTime pFechaArribo, int pCantidadAsientos, Empleado pUsuario, Destino pDestino, Compania pCompania, bool pServicioABordo, string pDocumentos)
            : base(pNumero, pFechaSalida, pFechaArribo, pCantidadAsientos, pUsuario, pDestino, pCompania)
        {
            ServicioABordo = pServicioABordo;
            Documentos = pDocumentos;
        }

        public ViajeInternacional()
            : base()
        {
        }

    }
}
