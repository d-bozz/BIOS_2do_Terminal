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
    public partial class ABMViajesNacionales : Form
    {
        private Empleado usuLogueado;
        private ViajeNacional unViajeNacional;

        public ABMViajesNacionales(Empleado pUsuLogueado)
        {
            InitializeComponent();
            usuLogueado = pUsuLogueado;
        }

        private void ABMViajesNacionales_Load(object sender, EventArgs e)
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
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            dtpSalida.Enabled = true;
            dtpArribo.Enabled = true;
            txtParadas.Enabled = true;
        }

        public void EstadoAgregar()
        {
            txtNumero.Enabled = false;
            txtCompania.Enabled = true;
            txtDestino.Enabled = true;
            txtCapacidad.Enabled = true;
            txtParadas.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            dtpSalida.Enabled = true;
            dtpArribo.Enabled = true;
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
            //txtParadas.Enabled = false;
            txtParadas.Text = "";
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

                Destino destino = new ServicioTerminal().BuscarDestino(txtDestino.Text);
                if (destino == null)
                    throw new Exception("No existe el destino ingresado");

                Compania compania = new ServicioTerminal().BuscarCompaniaActiva(txtCompania.Text);
                if (compania == null)
                    throw new Exception("No existe la compania ingresada");

                DateTime salida = dtpSalida.Value;
                DateTime arribo = dtpArribo.Value;

                ViajeNacional nacional = new ViajeNacional();

                nacional.Numero = Convert.ToInt32(txtNumero.Text.Trim());
                nacional.FechaSalida = salida;
                nacional.FechaArribo = arribo;
                nacional.CantidadAsientos = Convert.ToInt32(txtCapacidad.Text.Trim());
                nacional.Compania = compania;
                nacional.Destino = destino;
                nacional.Paradas = Convert.ToInt32(txtParadas.Text.Trim());
                nacional.Usuario = usuLogueado;


               new ServicioTerminal().AgregarViaje(nacional);
               Limpiar();
                lblError.Text = "Viaje Nacional " + nacional.Numero + " agregado correctamente";
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
                new ServicioTerminal().EliminarViaje(unViajeNacional);
                Limpiar();
                lblError.Text = "Viaje Nacional " + unViajeNacional.Numero + " eliminado correctamente.";
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

                if (unViajeNacional == null)
                    throw new Exception("No existe el viaje ingresado.");

                //Creo otra variable por si la modificacion falla y luego quiero modificar el viaje.
                ViajeNacional unVN = new ViajeNacional();
                unVN.CantidadAsientos = unViajeNacional.CantidadAsientos;
                unVN.Compania = unViajeNacional.Compania;
                unVN.Destino = unViajeNacional.Destino;
                unVN.FechaArribo = unViajeNacional.FechaArribo;
                unVN.FechaSalida = unViajeNacional.FechaSalida;
                unVN.Numero = unViajeNacional.Numero;
                unVN.Paradas = unViajeNacional.Paradas;
                unVN.Usuario = unViajeNacional.Usuario;

                unVN.FechaSalida = dtpSalida.Value;
                unVN.FechaArribo = dtpArribo.Value;
                unVN.CantidadAsientos = Convert.ToInt32(txtCapacidad.Text);

                unVN.Destino = new ServicioTerminal().BuscarDestino(txtDestino.Text);
                if (unVN.Destino == null)
                    throw new Exception("No existe el destino ingresado");
                unVN.Compania = new ServicioTerminal().BuscarCompaniaActiva(txtCompania.Text);
                if (unVN.Compania == null)
                    throw new Exception("No existe la compania ingresada");

                unVN.Paradas = Convert.ToInt32(txtParadas.Text);
                unVN.Usuario = usuLogueado;

                new ServicioTerminal().ModificarViaje(unVN);
                Limpiar();
                lblError.Text = "Viaje Nacional " + unVN.Numero + " modificado correctamente.";
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
                if (v is ViajeInternacional)
                    throw new Exception("El numero " + num + " corresponde a un viaje Internacional.");

                if (v is ViajeNacional)
                {
                    unViajeNacional = (ViajeNacional)v;
                    EstadoEliminarModificar();

                    txtDestino.Text = v.Destino.Cod;
                    txtDestinoCiudad.Text = v.Destino.Ciudad;
                    txtCompania.Text = v.Compania.Nombre;
                    txtCapacidad.Text = v.CantidadAsientos.ToString();
                    dtpSalida.Value = v.FechaSalida;
                    dtpArribo.Value = v.FechaArribo;
                    txtParadas.Text = ((ViajeNacional)v).Paradas.ToString();
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

        private void txtParadas_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                if (string.IsNullOrEmpty(txtParadas.Text))
                {
                    throw new Exception("El campo paradas esta vacio.");
                }

                if (!txtParadas.Text.Trim().All(char.IsNumber))
                {
                    throw new Exception("La cantidad de paradas debe ser un numero.");
                }

                if (Convert.ToInt32(txtParadas.Text.Trim()) < 0)
                {
                    throw new Exception("La cantidad de paradas debe ser positiva.");
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
                epErrores.SetError(txtParadas, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }
    }
}
