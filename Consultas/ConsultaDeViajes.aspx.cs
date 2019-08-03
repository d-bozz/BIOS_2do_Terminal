using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using Controles;

using Servicio;

public partial class ConsultaDeViajes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ddlDestinos.DataTextField = "Ciudad";
                ddlDestinos.DataValueField = "Cod";
                ddlCompania.DataTextField = "Nombre";
                ddlCompania.DataValueField = "Nombre";

                List<Destino> destinos = new List<Destino>();
                List<Facilidades> f = new List<Facilidades>();
                Destino d = new Destino();
                d.Cod = "Nin";
                d.Ciudad = "Ninguno";
                d.Pais = "Uruguay";
                d.LasFacilidades = f.ToArray();

                destinos.Add(d);
                destinos.AddRange(new ServicioTerminal().ListarDestinos());
                ddlDestinos.DataSource = destinos;
                ddlDestinos.DataBind();

                List<Compania> companias = new List<Compania>();
                Compania c = new Compania();
                c.Nombre = "Ninguna";
                c.Direccion = "Ninguna";
                c.Telefono = 1;
                companias.Add(c);
                companias.AddRange(new ServicioTerminal().ListarCompanias());
                ddlCompania.DataSource = companias;
                ddlCompania.DataBind();

                if (Session["IndiceComp"] != null && Session["IndiceDes"] != null && Session["ListadoViajes"] != null
                    && Session["FechaDesde"] != null && Session["FechaHasta"] != null && Session["UltimoFiltro"] != null)
                {
                    if ((int)Session["IndiceComp"] != 0 || (int)Session["IndiceDes"] != 0)
                    {
                        ddlCompania.SelectedIndex = (int)Session["IndiceComp"];
                        ddlDestinos.SelectedIndex = (int)Session["IndiceDes"];
                        calDesde.Activo(true);
                        calHasta.Activo(true);
                        calDesde.Fecha = (DateTime)Session["FechaDesde"];
                        calHasta.Fecha = (DateTime)Session["FechaHasta"];
                        RTViajes.DataSource = (List<Viaje>)Session["UltimoFiltro"];
                        RTViajes.DataBind();
                    }
                }
                else
                {
                    List<Viaje> viajes = ViajesSinPartir();
                    RTViajes.DataSource = viajes;
                    RTViajes.DataBind();
                }
            }
        }
        catch (System.Web.Services.Protocols.SoapException ex)
        {
            if (ex.Detail.InnerText == "")
            {
                if (ex.Message.Length > 263)
                {
                    lblError.Text = ex.Message.Substring(183, 80);
                }
                else if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(ex.Message.Length - 80, 80);
                else
                    lblError.Text = ex.Message;
            }
            else
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblError.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblError.Text = ex.Detail.InnerText;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }

    }

    protected List<Viaje> ViajesSinPartir()
    {
            List<Viaje> losViajes = new ServicioTerminal().ListarSinPartir().ToList();
            Session["ListadoViajes"] = losViajes;
            return losViajes;
    }


    protected void RTViajes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Ver")
        {
            try
            {
                Session["viaje"] = new ServicioTerminal().BuscarViaje(Convert.ToInt32(((Label)e.Item.Controls[3]).Text));
                Response.Redirect("~/ConsultaIndividualdeViaje.aspx");
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if (ex.Detail.InnerText == "")
                {
                    if (ex.Message.Length > 263)
                    {
                        lblError.Text = ex.Message.Substring(183, 80);
                    }
                    else if (ex.Message.Length > 80)
                        lblError.Text = ex.Message.Substring(ex.Message.Length - 80, 80);
                    else
                        lblError.Text = ex.Message;
                }
                else
                {
                    if (ex.Detail.InnerText.Length > 80)
                        lblError.Text = ex.Detail.InnerText.Substring(0, 80);
                    else
                        lblError.Text = ex.Detail.InnerText;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message; //siempre entra aca...	
                //{No se puede evaluar la expresión porque el código está optimizado o existe un marco nativo en la parte superior de la pila de llamadas.
                //}	System.Exception {System.Threading.ThreadAbortException}
            }
        }
    }


    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            if (DateTime.Compare(calDesde.Fecha, calHasta.Fecha) > 0)
                throw new Exception("La fecha de salida debe ser anterior a la de arribo.");

            if (DateTime.Compare(calDesde.Fecha, DateTime.Today) < 0)
                throw new Exception("La fecha y hora no puede ser anterior a la del momento : " + DateTime.Today.ToShortDateString());

            if (ddlDestinos.SelectedIndex == 0)
                throw new Exception("Seleccinar un destino es obligatorio.");

            List<Viaje> losViajes = (List<Viaje>)Session["ListadoViajes"];            

            //List<Viaje> resultado = Filtrar(ddlDestinos.SelectedIndex, ddlCompania.SelectedIndex, losViajes);

            List<Viaje> resultado = null;
            if (ddlCompania.SelectedIndex != 0 && calHasta.Fecha == DateTime.Today)
            {
                resultado = (from unViaje in losViajes
                             where unViaje.Destino.Cod == ddlDestinos.SelectedValue &&
                             unViaje.Compania.Nombre == ddlCompania.SelectedValue
                             select unViaje).ToList();
            }
            else if (ddlCompania.SelectedIndex == 0 && calHasta.Fecha == DateTime.Today)
            {
                resultado = (from unViaje in losViajes
                             where unViaje.Destino.Cod == ddlDestinos.SelectedValue
                             select unViaje).ToList();
            }
            else if (ddlCompania.SelectedIndex != 0 && calHasta.Fecha != DateTime.Today)
            {
                resultado = (from unViaje in losViajes
                             where unViaje.Destino.Cod == ddlDestinos.SelectedValue &&
                             unViaje.Compania.Nombre == ddlCompania.SelectedValue &&
                             (DateTime.Compare(unViaje.FechaSalida, calDesde.Fecha) > 0 && DateTime.Compare(unViaje.FechaSalida, calHasta.Fecha) < 0)
                             select unViaje).ToList();
            }
            else if (ddlCompania.SelectedIndex == 0 && calHasta.Fecha != DateTime.Today)
            {
                resultado = (from unViaje in losViajes
                             where unViaje.Destino.Cod == ddlDestinos.SelectedValue &&
                             (DateTime.Compare(unViaje.FechaSalida, calDesde.Fecha) > 0 && DateTime.Compare(unViaje.FechaSalida, calHasta.Fecha) < 0)
                             select unViaje).ToList();
            }

            RTViajes.DataSource = resultado;
            RTViajes.DataBind();
            lblError.Text = "Mostrando datos filtrados.";
            Session["UltimoFiltro"] = resultado;
            Session["IndiceComp"] = ddlCompania.SelectedIndex;
            Session["IndiceDes"] = ddlDestinos.SelectedIndex;
            Session["FechaDesde"] = calDesde.Fecha;
            Session["FechaHasta"] = calHasta.Fecha;
        }
        catch (System.Web.Services.Protocols.SoapException ex)
        {
            if (ex.Detail.InnerText == "")
            {
                if (ex.Message.Length > 263)
                {
                    lblError.Text = ex.Message.Substring(183, 80);
                }
                else if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(ex.Message.Length - 80, 80);
                else
                    lblError.Text = ex.Message;
            }
            else
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblError.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblError.Text = ex.Detail.InnerText;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnBorrarFiltro_Click(object sender, EventArgs e)
    {
        try
        {
            Session["UltimoFiltro"] = null;

            RTViajes.DataSource = (List<Viaje>)Session["ListadoViajes"];
            RTViajes.DataBind();

            ddlCompania.SelectedIndex = 0;
            ddlDestinos.SelectedIndex = 0;
            calDesde.Fecha = DateTime.Today;
            calHasta.Fecha = DateTime.Today;
            lblError.Text = "";
        }
        catch (System.Web.Services.Protocols.SoapException ex)
        {
            if (ex.Detail.InnerText == "")
            {
                if (ex.Message.Length > 263)
                {
                    lblError.Text = ex.Message.Substring(183, 80);
                }
                else if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(ex.Message.Length - 80, 80);
                else
                    lblError.Text = ex.Message;
            }
            else
            {
                if (ex.Detail.InnerText.Length > 80)
                    lblError.Text = ex.Detail.InnerText.Substring(0, 80);
                else
                    lblError.Text = ex.Detail.InnerText;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}