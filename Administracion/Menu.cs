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
    public partial class Menu : Form
    {
        private Empleado usuLogueado;
        private Form formLogueo;
        private Form formAbierto = null;
        private Button btnPulsado = null;

        public Menu(Empleado pUsuLogueado, Form pFormLogueo)
        {
            usuLogueado = pUsuLogueado;
            formLogueo = pFormLogueo;
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 200)
                MenuVertical.Width = 50;
            else
                MenuVertical.Width = 200;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            if (lblEmpleado.Text == "")
                lblEmpleado.Text = usuLogueado.Nombre;
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            formLogueo.Close();
        }

        private void AbrirFormulario(Form pFormHijo)
        {
            if (panelContenedor.Controls.Count > 0)
                panelContenedor.Controls.RemoveAt(0);

            Form form = pFormHijo;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(pFormHijo);
            panelContenedor.Tag = form;
            form.Show();
        }

        private void CambiarFormulario(Form pForm, Button pBtnPulsado)
        {
            if (formAbierto == null)
            {
                formAbierto = pForm;
                AbrirFormulario(formAbierto);
            }
            else
            {
                formAbierto.Close();
                formAbierto = pForm;
                AbrirFormulario(formAbierto);
            }
            if (btnPulsado == null)
            {
                btnPulsado = pBtnPulsado;
                btnPulsado.BackColor = Color.DarkSlateGray;
            }
            else
            {
                btnPulsado.BackColor = Color.Teal;
                btnPulsado = pBtnPulsado;
                btnPulsado.BackColor = Color.DarkSlateGray;
            }
        }

        #region BotonesMenu
        private void btnTerminales_Click(object sender, EventArgs e)
        {
            CambiarFormulario(new ABMTerminal(),btnTerminales);
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            CambiarFormulario(new ABMEmpleado(usuLogueado),btnEmpleados);
        }

        private void btnInternacionales_Click(object sender, EventArgs e)
        {
            CambiarFormulario(new ABMViajesInternacionales(usuLogueado),btnInternacionales);
        }

        private void btnNacionales_Click(object sender, EventArgs e)
        {
            CambiarFormulario(new ABMViajesNacionales(usuLogueado),btnNacionales);
        }

        private void btnCompanias_Click(object sender, EventArgs e)
        {
            CambiarFormulario(new ABMCompanias(),btnCompanias);
            
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            CambiarFormulario(new Estadisticas(),btnEstadisticas);
        }

        
        #endregion





    }
}
