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
    public partial class ABMEmpleado : Form
    {
        private Empleado unEmpleado;
        private Empleado usuLogueado;

        public ABMEmpleado(Empleado pUsuLogueado)
        {
            usuLogueado = pUsuLogueado;
            InitializeComponent();
            
        }

        private void ABMEmpleado_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        public void EstadoInicial()
        {
            Limpiar();
            lblError.Text = "";
        }

        
        //Estados
        public void EstadoEliminarModificar()
        {
            txtCedula.Enabled = false;
            txtPass.Enabled = true;
            txtNombre.Enabled = true;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            txtPass.Focus();
        }

        public void EstadoAgregar()
        {
            txtCedula.Enabled = false;
            txtPass.Enabled = true;
            txtNombre.Enabled = true;
            btnAgregar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtPass.Focus();
        }

        public void Limpiar()
        {
            epErrores.Clear();
            txtCedula.Enabled = true;
            txtCedula.Text = "";
            txtPass.Enabled = false;
            txtPass.Text = "";
            txtNombre.Enabled = false;
            txtNombre.Text = "";
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtCedula.Focus();
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
                Empleado empleado = new Empleado();
                empleado.Ci = txtCedula.Text.Trim();
                empleado.Contrasena = txtPass.Text.Trim();
                empleado.Nombre = txtNombre.Text.Trim();
                new ServicioTerminal().AgregarEmpleado(empleado);
                Limpiar();
                lblError.Text = "Empleado " + empleado.Nombre.Trim() + " agregado correctamente";
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
                if (unEmpleado.Ci == usuLogueado.Ci)
                {
                    throw new Exception("No se puede eliminar a si mismo.");
                }
                new ServicioTerminal().EliminarEmpleado(unEmpleado);
                Limpiar();
                lblError.Text = "Empleado " + unEmpleado.Nombre.Trim() + " eliminado correctamente.";
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
                //Uso otra variable por si da algun error el modificar y luego quiero eliminar el empleado.
                Empleado _unEmpleado = new Empleado();
                _unEmpleado.Ci = unEmpleado.Ci;
                _unEmpleado.Contrasena = unEmpleado.Contrasena;
                _unEmpleado.Nombre = unEmpleado.Nombre;

                _unEmpleado.Contrasena = txtPass.Text.Trim();
                _unEmpleado.Nombre = txtNombre.Text.Trim();
                
                new ServicioTerminal().ModificarEmpleado(_unEmpleado);
                if (_unEmpleado.Ci.ToLower() == usuLogueado.Ci.ToLower())
                {
                    usuLogueado = _unEmpleado;
                }
                Limpiar();
                lblError.Text = "Empleado " + _unEmpleado.Nombre.Trim() + " modificado correctamente.";
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
        private void txtCedula_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                if (txtCedula.Text.Trim() == "")
                {
                    Limpiar();
                    throw new Exception("La C.I. no puede ser vacia.");
                }
                if (txtCedula.Text.Length != 8)
                    throw new Exception("El numero de cedula es incorrecto.");

                if (!txtCedula.Text.All(char.IsNumber))
                    throw new Exception("La cedula solo puede contener numeros.");

                Empleado empleado = new ServicioTerminal().BuscarEmpleadoActivo(txtCedula.Text.Trim());
                if (empleado != null)
                {
                    unEmpleado = empleado;
                    txtPass.Text = empleado.Contrasena;
                    txtNombre.Text = empleado.Nombre;
                    lblError.Text = "Empleado " + empleado.Nombre.Trim() + " fue encontrado.";
                    EstadoEliminarModificar();
                }
                else
                {
                    lblError.Text = "No se ha encontrado un empleado con CI " + txtCedula.Text + ", si lo desea puede agregarlo.";
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
                epErrores.SetError(txtCedula, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtPass_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                if (string.IsNullOrEmpty(txtPass.Text))
                {
                    throw new Exception("La contraseña no puede ser vacia.");
                }

                if (txtPass.Text.Length != 6)
                {
                    throw new Exception("La contraseña debe tener 6 caracteres.");
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
                epErrores.SetError(txtPass, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epErrores.Clear();
                lblError.Text = "";

                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    throw new Exception("El nombre no puede ser Vacio.");
                }

                if (txtNombre.Text.Length > 50)
                {
                    throw new Exception("El nombre no debe exceder los 50 caracteres.");                    
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
                epErrores.SetError(txtNombre, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        

        
   }
}
