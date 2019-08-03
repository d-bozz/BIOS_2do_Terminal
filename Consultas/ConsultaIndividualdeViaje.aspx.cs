using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servicio;

public partial class ConsultaIndividualdeViaje : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Viaje v = (Viaje)Session["viaje"];
                if (v != null)
                {
                    ControlViaje1.Viaje = v;
                }
                else
                {
                    Response.Redirect("~/ConsultaDeViajes.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }

    }
}