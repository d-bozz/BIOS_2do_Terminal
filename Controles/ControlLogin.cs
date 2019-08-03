using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Controles
{
    public partial class ControlLogin : ContainerControl
    {
        //atributos
        //controles
        Label _titulo;
        public TextBox _txtUser;
        public TextBox _txtPass;
        Button _btnLogin;

        

        public ControlLogin()//: base()
        {
            InitializeComponent();

            _titulo = new Label();
            _titulo.Text = "INICIAR SESION";
            _titulo.ForeColor = System.Drawing.Color.MintCream;
            _titulo.Font = new System.Drawing.Font(_titulo.Font.FontFamily, 14/*, System.Drawing.FontStyle.Bold*/);
            _titulo.Width = 335;
            _titulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            _titulo.TabIndex = 0;
            this.Controls.Add(_titulo);

            _txtUser = new TextBox();
            _txtUser.Text = "CEDULA";
            _txtUser.ForeColor = System.Drawing.Color.DimGray;
            _txtUser.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            _txtUser.BorderStyle = BorderStyle.None;
            _txtUser.Width = 330;
            _txtUser.Height = 40;
            _txtUser.Enter += new EventHandler(_txtUser_Enter);
            _txtUser.Leave += new EventHandler(_txtUser_Leave);
            _txtUser.TabIndex = 1;
            this.Controls.Add(_txtUser);

            _txtPass = new TextBox();
            _txtPass.Text = "CONTRASENIA";
            _txtPass.ForeColor = System.Drawing.Color.DimGray;
            _txtPass.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            _txtPass.BorderStyle = BorderStyle.None;
            _txtPass.Width = 330;
            _txtPass.Height = 40;
            _txtPass.UseSystemPasswordChar = false;
            _txtPass.Enter += new EventHandler(_txtPass_Enter);
            _txtPass.Leave += new EventHandler(_txtPass_Leave);
            _txtPass.TabIndex = 2;
            this.Controls.Add(_txtPass);

            _btnLogin = new Button();
            _btnLogin.FlatStyle = FlatStyle.Flat;
            _btnLogin.Width = 330;
            _btnLogin.Height = 40;
            _btnLogin.Text = "Acceder";
            _btnLogin.ForeColor = System.Drawing.Color.DarkGray;
            _btnLogin.BackColor = System.Drawing.Color.Teal;
            _btnLogin.Cursor = Cursors.Hand;
            _btnLogin.Click += new EventHandler(_btnLogin_Click);
            _btnLogin.TabIndex = 3;
            this.Controls.Add(_btnLogin);

            _btnLogin.TabIndex = 0;
            _txtUser.TabIndex = 1;
            _txtPass.TabIndex = 2;


        }

        /*
        public Logueo(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        */

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //posicion x,y de donde se despliegara cada control en la pantalla
            _titulo.Location = new System.Drawing.Point(0, 5);
            _txtUser.Location = new System.Drawing.Point(0, 50);
            _txtPass.Location = new System.Drawing.Point(0, 95);
            _btnLogin.Location = new System.Drawing.Point(0, 140);

            this.ParentForm.AcceptButton = _btnLogin;
        }

        private void _txtPass_Enter(object sender, EventArgs e)
        {
            if (_txtPass.Text == "CONTRASENIA")
            {
                _txtPass.Text = "";
                _txtPass.ForeColor = System.Drawing.Color.LightGray;
                _txtPass.UseSystemPasswordChar = true;
            }
        }

        private void _txtPass_Leave(object sender, EventArgs e)
        {
            if (_txtPass.Text == "")
            {
                _txtPass.Text = "CONTRASENIA";
                _txtPass.UseSystemPasswordChar = false;
                _txtPass.ForeColor = System.Drawing.Color.DimGray;
            }
        }

        private void _txtUser_Enter(object sender, EventArgs e)
        {
            if (_txtUser.Text == "CEDULA")
            {
                _txtUser.Text = "";
                _txtUser.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void _txtUser_Leave(object sender, EventArgs e)
        {
            if (_txtUser.Text == "")
            {
                _txtUser.Text = "CEDULA";
                _txtUser.ForeColor = System.Drawing.Color.DimGray;
            }
        }

        public event EventHandler Loguearse;

        public void _btnLogin_Click(object sender, EventArgs e)
        {
            Loguearse(this, new EventArgs());
        }
    }
}
