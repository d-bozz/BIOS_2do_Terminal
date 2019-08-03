using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Web.Services.Protocols;
using EntidadesCompartidas;
using Logica;

/// <summary>
/// Descripción breve de ServicioTerminal
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class ServicioTerminal : System.Web.Services.WebService
{

    public ServicioTerminal()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    #region ************ Compania ************
    [WebMethod]
    public void AgregarCompania(Compania C)
    {
        try
        {
            ILogicaCompanias LCompania = FabricaLogica.getLogicaCompanias();
            LCompania.AgregarCompania(C);
        }
        catch (Exception ex)
        {
             throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void EliminarCompania(Compania C)
    {
        try
        {
            ILogicaCompanias LCompania = FabricaLogica.getLogicaCompanias();
            LCompania.EliminarCompania(C);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void ModificarCompania(Compania C)
    {
        try
        {
            ILogicaCompanias LCompania = FabricaLogica.getLogicaCompanias();
            LCompania.ModificarCompania(C);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public Compania BuscarCompaniaActiva(string pNombre)
    {
        try
        {
            ILogicaCompanias LCompania = FabricaLogica.getLogicaCompanias();
            return (LCompania.BuscarCompaniaActiva(pNombre));
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public List<Compania> ListarCompanias()
    {
        try
        {
            ILogicaCompanias LCompania = FabricaLogica.getLogicaCompanias();
            return (LCompania.ListarCompanias());
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    #endregion

    #region ************ Destinos ************
    [WebMethod]
    public void AgregarDestino(Destino D)
    {
        try
        {
            ILogicaDestinos LDestino = FabricaLogica.getLogicaDestinos();
            LDestino.AgregarDestino(D);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void EliminarDestino(Destino D)
    {
        try
        {
            ILogicaDestinos LDestino = FabricaLogica.getLogicaDestinos();
            LDestino.EliminarDestino(D);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void ModificarDestino(Destino D)
    {
        try
        {
            ILogicaDestinos LDestino = FabricaLogica.getLogicaDestinos();
            LDestino.ModificarDestino(D);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public Destino BuscarDestino(string pCod)
    {
        try
        {
            ILogicaDestinos LDestino = FabricaLogica.getLogicaDestinos();
            return (LDestino.BuscarDestino(pCod));
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public List<Destino> ListarDestinos()
    {
        try
        {
            ILogicaDestinos LDestino = FabricaLogica.getLogicaDestinos();
            return (LDestino.ListarDestinos());
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }
    #endregion

    #region ************ Viajes ************
    [WebMethod]
    public string ListarEstadisticas()
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            List<Viaje> listaViajes = Logica.FabricaLogica.getLogicaViajes().Listar();
            XmlNode NodoRaiz = doc.CreateNode(XmlNodeType.Element, "Viajes", "");
            foreach (Viaje v in listaViajes)
            {
                XmlNode NodoViaje = doc.CreateNode(XmlNodeType.Element, "Viaje", "");

                XmlNode NodoNumero = doc.CreateNode(XmlNodeType.Element, "Numero", "");
                NodoNumero.InnerText = v.Numero.ToString();
                NodoViaje.AppendChild(NodoNumero);

                XmlNode NodoCiudadDestino = doc.CreateNode(XmlNodeType.Element, "CiudadDestino", "");
                NodoCiudadDestino.InnerText = v.Destino.Ciudad;
                NodoViaje.AppendChild(NodoCiudadDestino);

                XmlNode NodoPaisDestino = doc.CreateNode(XmlNodeType.Element, "PaisDestino", "");
                NodoPaisDestino.InnerText = v.Destino.Pais;
                NodoViaje.AppendChild(NodoPaisDestino);

                XmlNode NodoCompania = doc.CreateNode(XmlNodeType.Element, "Compania", "");
                NodoCompania.InnerText = v.Compania.Nombre;
                NodoViaje.AppendChild(NodoCompania);

                XmlNode NodoFechaSalida = doc.CreateNode(XmlNodeType.Element, "FechaPartida", "");
                NodoFechaSalida.InnerText = v.FechaSalida.ToString();
                NodoViaje.AppendChild(NodoFechaSalida);

                NodoRaiz.AppendChild(NodoViaje);
            }
            doc.AppendChild(NodoRaiz);

            return doc.OuterXml;

        }
        catch (Exception ex)
        {

            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void AgregarViaje(Viaje V)
    {
        try
        {
            ILogicaViajes LViajes = FabricaLogica.getLogicaViajes();
            LViajes.AgregarViaje(V);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void EliminarViaje(Viaje V)
    {
        try
        {
            ILogicaViajes LViajes = FabricaLogica.getLogicaViajes();
            LViajes.EliminarViaje(V);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void ModificarViaje(Viaje V)
    {
        try
        {
            ILogicaViajes LViajes = FabricaLogica.getLogicaViajes();
            LViajes.ModificarViaje(V);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public List<Viaje> Listar()
    {
        try
        {
            ILogicaViajes LViajes = FabricaLogica.getLogicaViajes();
            return (LViajes.Listar());
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public List<Viaje> ListarSinPartir()
    {
        try
        {
            ILogicaViajes LViajes = FabricaLogica.getLogicaViajes();
            return (LViajes.ListarSinPartir());
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public Viaje BuscarViaje(int pNumero)
    {
        try
        {
            ILogicaViajes LViajes = FabricaLogica.getLogicaViajes();
            return (LViajes.BuscarViaje(pNumero));
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void ParaQueSeVean(ViajeInternacional vi, ViajeNacional vn)
    { }
    
    #endregion

    #region ************ Empleados ************

    [WebMethod]
    public Empleado Login(string pCi, string pPass)
    {
        try
        {
            ILogicaEmpleados LEmpleado = FabricaLogica.getLogicaEmpleados();
            return (LEmpleado.Login(pCi,pPass));
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }
    
    [WebMethod]
    public void AgregarEmpleado(Empleado E)
    {
        try
        {
            ILogicaEmpleados LEmpleado = FabricaLogica.getLogicaEmpleados();
            LEmpleado.AgregarEmpleado(E);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void EliminarEmpleado(Empleado E)
    {
        try
        {
            ILogicaEmpleados LEmpleado = FabricaLogica.getLogicaEmpleados();
            LEmpleado.EliminarEmpleado(E);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public void ModificarEmpleado(Empleado E)
    {
        try
        {
            ILogicaEmpleados LEmpleado = FabricaLogica.getLogicaEmpleados();
            LEmpleado.ModificarEmpleado(E);
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    [WebMethod]
    public Empleado BuscarEmpleadoActivo(string pCi)
    {
        try
        {
            ILogicaEmpleados LEmpleado = FabricaLogica.getLogicaEmpleados();
            return (LEmpleado.BuscarEmpleadoActivo(pCi));
        }
        catch (Exception ex)
        {
            throw devolverSoapException(ex);
        }
    }

    
    #endregion

    private SoapException devolverSoapException(Exception ex)
    {
        //generacion manual de excepcion SOAP - para poder obtener solo el mensaje enviado por alguna de las capas

        //1.- se debe crear un nodo xml (NodoError) el cual sera utilizado  para cargar el atributo Details de la exception SOAP
        XmlDocument _undoc = new System.Xml.XmlDocument();
        XmlNode _NodoError = _undoc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);

        //2.- Se crea un nodo xml (NodoDetalle) q contendra el texto del error
        XmlNode _NodoDetalle = _undoc.CreateNode(XmlNodeType.Element, "Error", "");
        _NodoDetalle.InnerText = ex.Message;
        _NodoError.AppendChild(_NodoDetalle);

        //4. Creacion manual y lanzamiento de la exception SOAP
        SoapException _MiEx = new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, _NodoError);
        throw _MiEx;
    }
}
