using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Servicio;

public partial class User_Controls_ControlViaje : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //viaje: num, fechaS, fechaLl, cantA, [paradas, servicioAbordo y documentos ]
    public Viaje Viaje
    {
        set
        {
            //viaje
            lblNumero.Text = value.Numero.ToString();
            lblFechaSalida.Text = value.FechaSalida.ToString();
            lblFechaArribo.Text = value.FechaArribo.ToString();
            lblCantidadAsientos.Text = value.CantidadAsientos.ToString();
            if (value is ViajeNacional)
            {
                lblParadasT.Visible = true;
                lblDocumentost.Visible = false;
                lblParadas.Text = ((ViajeNacional)value).Paradas.ToString(); 
            }
            else
            {
                lblParadasT.Visible = false;
                lblDocumentost.Visible = true;
                if (((ViajeInternacional)value).ServicioABordo)
                    lblServicioABordo.Text = "Con servicio a bordo";
                else lblServicioABordo.Text = "Sin servicio a bordo";

                lblDocumentos.Text = ((ViajeInternacional)value).Documentos;
            }
            //empleado
            lblCIEmpleado.Text = value.Usuario.Ci;
            lblNombreEmpleado.Text = value.Usuario.Nombre;
            //compania
            lblNombreComp.Text = value.Compania.Nombre;
            lblDireccionComp.Text = value.Compania.Direccion;
            lblTelComp.Text = value.Compania.Telefono.ToString();
            //destino
            lblCodigoD.Text = value.Destino.Cod;
            lblCiudadD.Text = value.Destino.Ciudad;
            lblPaisD.Text = value.Destino.Pais;

            if (value.Destino.LasFacilidades != null && value.Destino.LasFacilidades.Length > 0)
            {
                lbxFacilidades.Enabled = true;
                lbxFacilidades.DataSource = value.Destino.LasFacilidades;
                lbxFacilidades.DataBind();
            }
            else
                lbxFacilidades.Enabled = false;

        }
    }
}