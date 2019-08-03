namespace Administracion
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuVertical = new System.Windows.Forms.Panel();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnInternacionales = new System.Windows.Forms.Button();
            this.btnNacionales = new System.Windows.Forms.Button();
            this.btnEmpleados = new System.Windows.Forms.Button();
            this.btnTerminales = new System.Windows.Forms.Button();
            this.btnCompanias = new System.Windows.Forms.Button();
            this.LogoImagen = new System.Windows.Forms.PictureBox();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.btnMenu = new System.Windows.Forms.PictureBox();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.MenuVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoImagen)).BeginInit();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuVertical
            // 
            this.MenuVertical.BackColor = System.Drawing.Color.Teal;
            this.MenuVertical.Controls.Add(this.btnEstadisticas);
            this.MenuVertical.Controls.Add(this.btnInternacionales);
            this.MenuVertical.Controls.Add(this.btnNacionales);
            this.MenuVertical.Controls.Add(this.btnEmpleados);
            this.MenuVertical.Controls.Add(this.btnTerminales);
            this.MenuVertical.Controls.Add(this.btnCompanias);
            this.MenuVertical.Controls.Add(this.LogoImagen);
            this.MenuVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuVertical.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuVertical.Location = new System.Drawing.Point(0, 0);
            this.MenuVertical.Name = "MenuVertical";
            this.MenuVertical.Size = new System.Drawing.Size(200, 561);
            this.MenuVertical.TabIndex = 1;
            // 
            // btnEstadisticas
            // 
            this.btnEstadisticas.BackColor = System.Drawing.Color.Teal;
            this.btnEstadisticas.CausesValidation = false;
            this.btnEstadisticas.FlatAppearance.BorderSize = 0;
            this.btnEstadisticas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnEstadisticas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadisticas.ForeColor = System.Drawing.Color.LightGray;
            this.btnEstadisticas.Image = global::Administracion.Properties.Resources.reportes;
            this.btnEstadisticas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstadisticas.Location = new System.Drawing.Point(-5, 340);
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(205, 50);
            this.btnEstadisticas.TabIndex = 5;
            this.btnEstadisticas.Text = "Estadisticas";
            this.btnEstadisticas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEstadisticas.UseVisualStyleBackColor = false;
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            // 
            // btnInternacionales
            // 
            this.btnInternacionales.BackColor = System.Drawing.Color.Teal;
            this.btnInternacionales.CausesValidation = false;
            this.btnInternacionales.FlatAppearance.BorderSize = 0;
            this.btnInternacionales.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnInternacionales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInternacionales.ForeColor = System.Drawing.Color.LightGray;
            this.btnInternacionales.Image = global::Administracion.Properties.Resources.compras;
            this.btnInternacionales.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInternacionales.Location = new System.Drawing.Point(-5, 280);
            this.btnInternacionales.Name = "btnInternacionales";
            this.btnInternacionales.Size = new System.Drawing.Size(205, 50);
            this.btnInternacionales.TabIndex = 4;
            this.btnInternacionales.Text = "Inter - nacionales";
            this.btnInternacionales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInternacionales.UseCompatibleTextRendering = true;
            this.btnInternacionales.UseVisualStyleBackColor = false;
            this.btnInternacionales.Click += new System.EventHandler(this.btnInternacionales_Click);
            // 
            // btnNacionales
            // 
            this.btnNacionales.BackColor = System.Drawing.Color.Teal;
            this.btnNacionales.CausesValidation = false;
            this.btnNacionales.FlatAppearance.BorderSize = 0;
            this.btnNacionales.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnNacionales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNacionales.ForeColor = System.Drawing.Color.LightGray;
            this.btnNacionales.Image = global::Administracion.Properties.Resources.compras;
            this.btnNacionales.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNacionales.Location = new System.Drawing.Point(-5, 220);
            this.btnNacionales.Name = "btnNacionales";
            this.btnNacionales.Size = new System.Drawing.Size(205, 50);
            this.btnNacionales.TabIndex = 3;
            this.btnNacionales.Text = "Nacionales   ";
            this.btnNacionales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNacionales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNacionales.UseVisualStyleBackColor = false;
            this.btnNacionales.Click += new System.EventHandler(this.btnNacionales_Click);
            // 
            // btnEmpleados
            // 
            this.btnEmpleados.BackColor = System.Drawing.Color.Teal;
            this.btnEmpleados.CausesValidation = false;
            this.btnEmpleados.FlatAppearance.BorderSize = 0;
            this.btnEmpleados.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnEmpleados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpleados.ForeColor = System.Drawing.Color.LightGray;
            this.btnEmpleados.Image = global::Administracion.Properties.Resources.empleados;
            this.btnEmpleados.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmpleados.Location = new System.Drawing.Point(-5, 110);
            this.btnEmpleados.Name = "btnEmpleados";
            this.btnEmpleados.Size = new System.Drawing.Size(205, 50);
            this.btnEmpleados.TabIndex = 1;
            this.btnEmpleados.Text = "    Empleados";
            this.btnEmpleados.UseVisualStyleBackColor = false;
            this.btnEmpleados.Click += new System.EventHandler(this.btnEmpleados_Click);
            // 
            // btnTerminales
            // 
            this.btnTerminales.BackColor = System.Drawing.Color.Teal;
            this.btnTerminales.CausesValidation = false;
            this.btnTerminales.FlatAppearance.BorderSize = 0;
            this.btnTerminales.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnTerminales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTerminales.ForeColor = System.Drawing.Color.LightGray;
            this.btnTerminales.Image = global::Administracion.Properties.Resources.Companias;
            this.btnTerminales.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTerminales.Location = new System.Drawing.Point(-5, 160);
            this.btnTerminales.Name = "btnTerminales";
            this.btnTerminales.Size = new System.Drawing.Size(205, 50);
            this.btnTerminales.TabIndex = 2;
            this.btnTerminales.Text = "    Terminales";
            this.btnTerminales.UseVisualStyleBackColor = false;
            this.btnTerminales.Click += new System.EventHandler(this.btnTerminales_Click);
            // 
            // btnCompanias
            // 
            this.btnCompanias.BackColor = System.Drawing.Color.Teal;
            this.btnCompanias.CausesValidation = false;
            this.btnCompanias.FlatAppearance.BorderSize = 0;
            this.btnCompanias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnCompanias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompanias.ForeColor = System.Drawing.Color.LightGray;
            this.btnCompanias.Image = global::Administracion.Properties.Resources.Companias;
            this.btnCompanias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCompanias.Location = new System.Drawing.Point(-5, 60);
            this.btnCompanias.Name = "btnCompanias";
            this.btnCompanias.Size = new System.Drawing.Size(205, 50);
            this.btnCompanias.TabIndex = 0;
            this.btnCompanias.Text = "    Companias";
            this.btnCompanias.UseVisualStyleBackColor = false;
            this.btnCompanias.Click += new System.EventHandler(this.btnCompanias_Click);
            // 
            // LogoImagen
            // 
            this.LogoImagen.Image = global::Administracion.Properties.Resources.LogoTerminal;
            this.LogoImagen.Location = new System.Drawing.Point(-1, 4);
            this.LogoImagen.Name = "LogoImagen";
            this.LogoImagen.Size = new System.Drawing.Size(200, 46);
            this.LogoImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoImagen.TabIndex = 0;
            this.LogoImagen.TabStop = false;
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.panelSuperior.Controls.Add(this.lblEmpleado);
            this.panelSuperior.Controls.Add(this.btnMenu);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(200, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(808, 50);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmpleado.BackColor = System.Drawing.Color.Transparent;
            this.lblEmpleado.CausesValidation = false;
            this.lblEmpleado.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblEmpleado.ForeColor = System.Drawing.Color.LightGray;
            this.lblEmpleado.Location = new System.Drawing.Point(403, 15);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblEmpleado.Size = new System.Drawing.Size(399, 21);
            this.lblEmpleado.TabIndex = 1;
            this.lblEmpleado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMenu
            // 
            this.btnMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMenu.Image = global::Administracion.Properties.Resources.Mobile_Menu_Icon;
            this.btnMenu.Location = new System.Drawing.Point(6, 9);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(35, 35);
            this.btnMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMenu.TabIndex = 0;
            this.btnMenu.TabStop = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.AutoScroll = true;
            this.panelContenedor.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelContenedor.Location = new System.Drawing.Point(200, 50);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(808, 511);
            this.panelContenedor.TabIndex = 3;
            // 
            // Menu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.LightGray;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.MenuVertical);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Menu";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terminal";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Menu_FormClosed);
            this.Load += new System.EventHandler(this.Menu_Load);
            this.MenuVertical.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogoImagen)).EndInit();
            this.panelSuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuVertical;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.PictureBox btnMenu;
        private System.Windows.Forms.PictureBox LogoImagen;
        private System.Windows.Forms.Button btnCompanias;
        private System.Windows.Forms.Button btnEmpleados;
        private System.Windows.Forms.Button btnTerminales;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnInternacionales;
        private System.Windows.Forms.Button btnNacionales;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Panel panelContenedor;
    }
}