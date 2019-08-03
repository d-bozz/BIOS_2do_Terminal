using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Administracion.Servicio;
namespace Administracion
{
    public partial class ABMViajesInternacionales : Form
    {
        private Empleado usuLogueado;
        private ViajeInternacional unViajeInternacional;

        public ABMViajesInternacionales(Empleado pUsuLogueado)
        {
            InitializeComponent();
            usuLogueado = pUsuLogueado;
        }

        private void ABMViajesInternacionales_Load(object sender, EventArgs e)
        {
            EstadoInicial();
            dtpArribo.MinDate = DateTime.Today;
            dtpSalida.MinDate = DateTime.Today;
        }

        //Estados
        public void EstadoInicial()
        {
            Limpiar();
            lblError.Text = "";
        }

        public void EstadoEliminarModificar()
        {
            txtNumero.Enabled = false;
            txtCompania.Enabled = true;
            txtDestino.Enabled = true;
            txtCapacidad.Enabled = true;
            txtDocumentos.Enabled = true;
            cbxServicioABordo.Enabled = true;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            dtpSalida.Enabled = true;
            dtpArribo.Enabled = true;
            txtCompania.Focus();
        }

        public void EstadoAgregar()
        {
            txtNumero.Enabled = false;
            txtCompania.Enabled = true;
            txtDestino.Enabled = true;
            txtCapacidad.Enabled = true;
            txtDocumentos.Enabled = true;
            cbxServicioABordo.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            dtpSalida.Enabled = true;
            dtpArribo.Enabled = true;
            txtCompania.Focus();
        }

        public void Limpiar()
        {
            txtNumero.Enabled = true;

            txtNumero.Text = "";

            txtDestino.Text = "";
            txtDestinoCiudad.Text = "";
            txtCompania.Text = "";
            //txtDestino.Enabled = false;
            //txtCompania.Enabled = false;
            //txtCapacidad.Enabled = false;
            txtCapacidad.Text = "";
            //txtDocumentos.Enabled = false;
            txtDocumentos.Text = "";
            //cbxServicioABordo.Enabled = false;
            cbxServicioABordo.Checked = false;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            //dtpSalida.Enabled = false;
            //dtpArribo.Enabled = false;
            lblError.Text = "";
            dtpSalida.Value = DateTime.Now;
            dtpArribo.Value = DateTime.Now;

            txtNumero.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        //Botones
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                //*********************************************************
                Destino destino = new ServicioTerminal().BuscarDestino(txtDestino.Text);
                if (destino == null)
                    throw new Exception("No existe el destino ingresado");

                Compania compania = new ServicioTerminal().BuscarCompaniaActiva(txtCompania.Text);
                if (compania == null)
                    throw new Exception("No existe la compania ingresada");
                //*********************************************************


                DateTime salida = dtpSalida.Value;
                DateTime arribo = dtpArribo.Value;

                ViajeInternacional _inter = new ViajeInternacional();

                _inter.CantidadAsientos = Convert.ToInt32(txtCapacidad.Text.Trim());
                _inter.Compania = compania;
                _inter.Destino = destino;
                _inter.Documentos = txtDocumentos.Text.Trim();
                _inter.FechaArribo = arribo;
                _inter.FechaSalida = salida;
                _inter.Numero = Convert.ToInt32(txtNumero.Text.Trim());
                _inter.ServicioABordo = cbxServicioABordo.Checked;
                _inter.Usuario = usuLogueado;
                new ServicioTerminal().AgregarViaje(_inter);
                Limpiar();
                lblError.Text = "Viaje Internacional " + _inter.Numero + " agregado correctamente";
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
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                new ServicioTerminal().EliminarViaje(unViajeInternacional);
                lblError.Text = "Viaje Internacional " + unViajeInternacional.Numero + " eliminado correctamente.";
                Limpiar();
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
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (unViajeInternacional == null)
                    throw new Exception("No existe el viaje ingresado.");

                unViajeInternacional.FechaSalida = dtpSalida.Value;
                unViajeInternacional.FechaArribo = dtpArribo.Value;
                unViajeInternacional.CantidadAsientos = Convert.ToInt32(txtCapacidad.Text);

                //Creo variable por si la modificacion falla y quiero eliminar el viaje luego.
                ViajeInternacional unVI = new ViajeInternacional();
                unVI.CantidadAsientos = unViajeInternacional.CantidadAsientos;
                unVI.Compania = unViajeInternacional.Compania;
                unVI.Destino = unViajeInternacional.Destino;
                unVI.Documentos = unViajeInternacional.Documentos;
                unVI.FechaArribo = unViajeInternacional.FechaArribo;
                unVI.FechaSalida = unViajeInternacional.FechaSalida;
                unVI.Numero = unViajeInternacional.Numero;
                unVI.ServicioABordo = unViajeInternacional.ServicioABordo;
                unVI.Usuario = unViajeInternacional.Usuario;

                //**********************************************************
                unVI.Destino = new ServicioTerminal().BuscarDestino(txtDestino.Text);
                if (unVI.Destino == null)
                    throw new Exception("No existe el destino ingresado");

                unVI.Compania = new ServicioTerminal().BuscarCompaniaActiva(txtCompania.Text);
                if (unVI.Compania == null)
                    throw new Exception("No existe la compania ingresada");
                //**********************************************************

                unVI.ServicioABordo = cbxServicioABordo.Checked;
                unVI.Documentos = txtDocumentos.Text;
                unVI.Usuario = usuLogueado;
                new ServicioTerminal().ModificarViaje(unVI);
                Limpiar();
                lblError.Text = "Viaje Internacional " + unVI.Numero + " modificado correctamente.";
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
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }    

        //Validaciones
        private void txtNumero_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                int num = 0;
                if (!int.TryParse(txtNumero.Text.Trim(), out num))
                    throw new Exception("El numero debe estar compuesto unicamente por digitos.");

                Viaje v = new ServicioTerminal().BuscarViaje(num);

                if (v == null)
                {
                    EstadoAgregar();
                    lblError.Text = "No se ha encontrado un viaje con el numero " + num + ", puede agregarlo si lo desea.";
                }
                if (v is ViajeNacional)
                    throw new Exception("El numero " + num + " corresponde a un viaje Nacional.");

                if (v is ViajeInternacional)
                {
                    unViajeInternacional = (ViajeInternacional)v;
                    EstadoEliminarModificar();

                    txtDestino.Text = v.Destino.Cod;
                    txtDestinoCiudad.Text = v.Destino.Ciudad;
                    txtCompania.Text = v.Compania.Nombre;
                    txtCapacidad.Text = v.CantidadAsientos.ToString();
                    dtpSalida.Value = v.FechaSalida;
                    dtpArribo.Value = v.FechaArribo;
                    txtDocumentos.Text = ((ViajeInternacional)v).Documentos;
                    cbxServicioABordo.Checked = ((ViajeInternacional)v).ServicioABordo;
                    lblError.Text = "Se ha encontrado el viaje.";
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
                epErrores.SetError(txtNumero, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtCompania_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                if (txtCompania.Text.Trim().Length > 50)
                    throw new Exception("El nombre de la compania no puede superar los 50 caracteres.");

                if (txtCompania.Text.Trim().Length == 0)
                    throw new Exception("El nombre de la compania no puede ser vacio.");

                Compania compania = new ServicioTerminal().BuscarCompaniaActiva(txtCompania.Text);
                if (compania == null)
                    throw new Exception("No existe la compania ingresada");
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
                epErrores.SetError(txtCompania, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtDestino_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";
                txtDestinoCiudad.Text = "";

                if (txtDestino.Text.Trim().Length != 3)
                {
                    throw new Exception("El codigo de destino deben ser tres letras.");
                }
                if (!txtDestino.Text.Trim().All(char.IsLetter))
                {
                    throw new Exception("El codigo de destino solo puede contener letras.");
                }

                Destino destino = new ServicioTerminal().BuscarDestino(txtDestino.Text);
                if (destino == null)
                    throw new Exception("No existe el destino ingresado");

                if (destino is Destino)
                    txtDestinoCiudad.Text = destino.Ciudad;
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
                epErrores.SetError(txtDestino, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtCapacidad_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                if (string.IsNullOrEmpty(txtCapacidad.Text))
                {
                    throw new Exception("El campo capacidad esta vacio.");
                }

                int capacidad = 0;
                if (!int.TryParse(txtCapacidad.Text, out capacidad))
                    throw new Exception("La capacidad debe ser un numero.");

                if (capacidad < 0 || capacidad > 60)
                    throw new Exception("La capacidad debe ser entre 0 y 60");
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
                epErrores.SetError(txtCapacidad, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }

        }

        private void txtDocumentos_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                if (txtDocumentos.Text.Length != 0)
                {
                    if (txtDocumentos.Text.Length > 50)
                    { throw new Exception("El documento no puede tener mas de 50 caracteres."); }
                }
                else
                {
                    throw new Exception("El documento no puede ser vacio.");
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
                epErrores.SetError(txtDocumentos, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void dtpSalida_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                DateTime salida = dtpSalida.Value;
                DateTime arribo = dtpArribo.Value;

                if (salida.CompareTo(arribo) > 0)
                    throw new Exception("La fecha de salida debe ser anterior a la de arribo.");
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
                epErrores.SetError(dtpSalida, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void dtpArribo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                DateTime salida = dtpSalida.Value;
                DateTime arribo = dtpArribo.Value;

                if (salida.CompareTo(arribo) > 0)
                    throw new Exception("La fecha de arribo debe ser posterior a la de salida.");
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
                epErrores.SetError(dtpArribo, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }
    }
}
