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
    public partial class ABMCompanias : Form
    {
        private Compania unaComp;

        public ABMCompanias()
        {
            InitializeComponent();         
        }

        private void ABMCompanias_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        } 

        //estados
        private void EstadoInicial()
        {
            txtNombreC.Enabled = true;

            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;

            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            btnLimpiar.Enabled = true;


            txtNombreC.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";

            _ErrorProvider.Clear();
            txtNombreC.Focus();
        }

        private void EstadoAgregar()
        {
            txtNombreC.Enabled = false;

            txtDireccion.Enabled = true;
            txtTelefono.Enabled = true;

            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;


            btnLimpiar.Enabled = true;
            txtDireccion.Focus();
        }

        private void EstadoBM()
        {
            txtNombreC.Enabled = false;

            txtDireccion.Enabled = true;
            txtTelefono.Enabled = true;

            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;


            btnLimpiar.Enabled = true;
            txtDireccion.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblError.Text = "";
        }
        
        //botones
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Compania _compa = new Compania();
                _compa.Nombre = txtNombreC.Text.Trim();
                _compa.Direccion = txtDireccion.Text.Trim();
                _compa.Telefono = Convert.ToInt32(txtTelefono.Text);
                //ServicioTerminal STerminal = new ServicioTerminal();
                new ServicioTerminal().AgregarCompania(_compa);
                lblError.Text = "Compania " + _compa.Nombre.Trim() + " agregado correctamente";
                EstadoInicial();
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
                new ServicioTerminal().EliminarCompania(unaComp);
                EstadoInicial();
                lblError.Text = "Compania " + unaComp.Nombre.Trim() + " eliminada correctamente.";
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
                //Uso otra variable por si da error la modificacion y luego quiero eliminar la compania.
                Compania _unaComp = new Compania();
                _unaComp.Direccion = unaComp.Direccion;
                _unaComp.Nombre = unaComp.Nombre;
                _unaComp.Telefono = unaComp.Telefono;

                _unaComp.Direccion = txtDireccion.Text.Trim();
                _unaComp.Telefono = Convert.ToInt32(txtTelefono.Text.Trim());
                new ServicioTerminal().ModificarCompania(_unaComp);
                EstadoInicial();
                lblError.Text = "Compania " + _unaComp.Nombre.Trim() + " modificada correctamente.";
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
            lblError.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //validaciones
        private void txtNombreC_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _ErrorProvider.Clear();
                if (string.IsNullOrEmpty(txtNombreC.Text.Trim()))
                    throw new Exception("El campo Nombre esta vacio.");

                if (txtNombreC.Text.Any(char.IsNumber))
                    throw new Exception("La Compania no puede contener numeros.");

                if (txtNombreC.Text.Trim().Length > 50)
                    throw new Exception("El nombre no debe exceder los 50 caracteres.");

                Compania _compa = new ServicioTerminal().BuscarCompaniaActiva(txtNombreC.Text.Trim());
                if (_compa != null)
                {
                    unaComp = _compa;
                    txtDireccion.Text = _compa.Direccion;
                    txtTelefono.Text = _compa.Telefono.ToString();
                    lblError.Text = "Compania " + _compa.Nombre.Trim() + " fue encontrada.";
                    EstadoBM();
                }
                else
                {
                    lblError.Text = "No se ha encontrado la compania " + txtNombreC.Text + ", si lo desea puede agregarla.";
                    EstadoAgregar();
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
                _ErrorProvider.SetError(txtNombreC, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtDireccion_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _ErrorProvider.Clear();

                if (string.IsNullOrWhiteSpace(txtDireccion.Text.Trim()))
                    throw new Exception("Ingrese direccion.");
                
                if (string.IsNullOrEmpty(txtDireccion.Text.Trim()))
                    throw new Exception("El campo direccion esta vacio.");

                if (txtDireccion.Text.Trim().Length > 50)
                    throw new Exception("La direccion no debe exceder los 50 caracteres.");
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
                _ErrorProvider.SetError(txtDireccion, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtTelefono_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _ErrorProvider.Clear();

                if (string.IsNullOrWhiteSpace(txtTelefono.Text.Trim()))
                    throw new Exception("Ingrese telefono.");
                
                if (!txtTelefono.Text.Trim().All(char.IsNumber))
                    throw new Exception("El telefono solo puede contener numeros.");

                if (Convert.ToInt32(txtTelefono.Text.Trim()) < 0)
                    throw new Exception("El telefono debe ser positiva.");
            }
            catch (FormatException)
            {
                _ErrorProvider.SetError(txtTelefono, "El campo telefono debe ser un numero.");
                lblError.Text = "El campo telefono debe ser un numero.";
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
                _ErrorProvider.SetError(txtTelefono, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }       
    }
}
