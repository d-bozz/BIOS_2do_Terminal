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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void logueo1_Loguearse(object sender, EventArgs e)
        {
            try
            {
                if (logueo1._txtUser.Text == "CEDULA" || logueo1._txtPass.Text == "CONTRASENIA" || string.IsNullOrWhiteSpace(logueo1._txtUser.Text) || string.IsNullOrWhiteSpace(logueo1._txtPass.Text))
                { throw new Exception("Ingrese credenciales"); }
                Empleado unEmp = new ServicioTerminal().Login(logueo1._txtUser.Text, logueo1._txtPass.Text);
                if (unEmp == null)
                {
                    lblError.Text = "Datos invalidos";
                    logueo1._txtPass.Text = "";
                }
                else
                {
                    Menu unform = new Menu(unEmp,this);
                    unform.Show();
                    this.Hide();
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
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }     
        }
    }
}
