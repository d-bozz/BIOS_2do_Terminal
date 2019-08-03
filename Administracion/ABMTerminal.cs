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
    public partial class ABMTerminal : Form
    {
        List<Facilidades> _listaFacilidades = null;
        Destino _unDestino = null;
        Facilidades _unaFacilidad = null;

        public ABMTerminal()
        {
            InitializeComponent();

        }

        private void ABMTerminal_Load(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        private void EstadoInicial()
        {
            txtCodigo.Enabled = true;

            txtCiudad.Enabled = false;
            txtFacilidad.Enabled = false;
            txtPais.Enabled = false;

            btnAgregar.Enabled = false;
            btnElminar.Enabled = false;
            btnModificar.Enabled = false;

            btnDeshacer.Enabled = true;

            DGVFacilidades.DataSource = null;
            _unDestino = null;
            _listaFacilidades = null;

            txtCodigo.Text = "";
            txtCiudad.Text = "";
            txtPais.Text = "";
            txtFacilidad.Text = "";
            txtCodigo.Focus();
        }

        private void EstadoAgregar()
        {
            txtCodigo.Enabled = false;

            txtCiudad.Enabled = true;
            txtPais.Enabled = true;
            txtFacilidad.Enabled = true;

            btnAgregar.Enabled = true;
            btnElminar.Enabled = false;
            btnModificar.Enabled = false;


            btnDeshacer.Enabled = true;
            txtCiudad.Focus();
        }

        private void EstadoBM()
        {
            txtCodigo.Enabled = false;

            txtCiudad.Enabled = true;
            txtPais.Enabled = true;
            txtFacilidad.Enabled = true;

            btnAgregar.Enabled = false;
            btnElminar.Enabled = true;
            btnModificar.Enabled = true;


            btnDeshacer.Enabled = true;
            txtCiudad.Focus();
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

                if (_listaFacilidades == null)
                    _listaFacilidades = new List<Facilidades>();

                _unDestino = new Destino();
                _unDestino.Ciudad = txtCiudad.Text.Trim();
                _unDestino.Cod = txtCodigo.Text.Trim();
                _unDestino.Pais = txtPais.Text.Trim();
                _unDestino.LasFacilidades = _listaFacilidades.ToArray();
                new ServicioTerminal().AgregarDestino(_unDestino);
                EstadoInicial();
                lblError.Text = "Alta exitosa.";
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

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            EstadoInicial();
        }

        private void btnElminar_Click(object sender, EventArgs e)
        {
            try
            {
                new ServicioTerminal().EliminarDestino(_unDestino);
                EstadoInicial();
                lblError.Text = "Eliminacion exitosa.";
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
                if (_unDestino == null)
                    throw new Exception("No hay destino seleccionado.");
                if (_listaFacilidades == null)
                    _listaFacilidades = new List<Facilidades>();

                //Creo otra variable por si el modificar no funciona y luego quiero eliminar el destino.
                Destino unDest = new Destino();
                unDest.Ciudad = _unDestino.Ciudad;
                unDest.Cod = _unDestino.Cod;
                unDest.LasFacilidades = _unDestino.LasFacilidades;
                unDest.Pais = _unDestino.Pais;

                unDest.Ciudad = txtCiudad.Text.Trim();
                unDest.Pais = txtPais.Text.Trim();
                unDest.LasFacilidades = _listaFacilidades.ToArray();
                new ServicioTerminal().ModificarDestino(unDest);
                EstadoInicial();
                lblError.Text = "Modificacion existosa.";
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

        private void btnAgregarFaciliad_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFacilidad.Text))
                    throw new Exception("La facilidad no puede ser vacia.");

                _unaFacilidad = new Facilidades();
                _unaFacilidad.Facilidad = txtFacilidad.Text;

                if (_listaFacilidades == null)
                    _listaFacilidades = new List<Facilidades>();

                if (_listaFacilidades.Count > 0)
                {
                    int i = 0;
                    bool esta = false;
                    while (i < _listaFacilidades.Count && !esta)
                    {
                        if (txtFacilidad.Text.ToUpper() == _listaFacilidades[i].Facilidad.ToUpper())
                            esta = true;
                        i++;
                    }
                    if (!esta)
                    {
                        _listaFacilidades.Add(_unaFacilidad);
                        DGVFacilidades.DataSource = null;
                        DGVFacilidades.DataSource = _listaFacilidades;
                        lblError.Text = "Facilidad agregada correctamente.";
                        txtFacilidad.Text = "";
                    }
                    else
                        throw new Exception("Facilidad Repetida.");
                }
                else
                {
                    _listaFacilidades.Add(_unaFacilidad);
                    DGVFacilidades.DataSource = null;
                    DGVFacilidades.DataSource = _listaFacilidades;
                    lblError.Text = "Facilidad agregada correctamente.";
                    txtFacilidad.Text = "";
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

        private void btnBorrarFacilidad_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVFacilidades.SelectedRows.Count == 0)
                    throw new Exception("Seleccione una facilidad primero");

                _listaFacilidades.RemoveAt(DGVFacilidades.SelectedRows[0].Index);
                DGVFacilidades.DataSource = null;
                DGVFacilidades.DataSource = _listaFacilidades;

                lblError.Text = "Facilidad eliminada correctamente.";
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //validaciones 

        private void txtCodigo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _ErrorProvider.Clear();

                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                    throw new Exception("El codigo no puede estar vacio.");

                if (txtCodigo.Text.Trim().Length != 3)
                    throw new Exception("Ingrese tres letras.");


                if (!txtCodigo.Text.Trim().All(char.IsLetter))
                    throw new Exception("El codigo solo puede contener letras.");

                _unDestino = new ServicioTerminal().BuscarDestino(txtCodigo.Text.ToUpper().Trim());

                if (_unDestino == null)
                {
                    EstadoAgregar();
                    lblError.Text = "No existe el destino" + txtCodigo.Text + ", si lo desea puede agregarlo.";
                }
                else
                {
                    EstadoBM();
                    txtCiudad.Text = _unDestino.Ciudad;
                    txtPais.Text = _unDestino.Pais;
                    if (_unDestino.LasFacilidades.Count() == 0)
                    {
                        _listaFacilidades = null;
                    }
                    else
                    {
                        _listaFacilidades = _unDestino.LasFacilidades.ToList();
                    }
                    DGVFacilidades.DataSource = _listaFacilidades;
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
                _ErrorProvider.SetError(txtCodigo, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtPais_Validating(object sender, CancelEventArgs e)
        {
            _ErrorProvider.Clear();
            try
            {
                if (string.IsNullOrWhiteSpace(txtPais.Text.Trim()))
                    throw new Exception("Ingrese pais");
                if (!(txtPais.Text.ToLower() == "argentina" || txtPais.Text.ToLower() == "brasil" || txtPais.Text.ToLower() == "paraguay" ||
                        txtPais.Text.ToLower() == "uruguay"))
                    throw new Exception("Solo estan permitidos paises del mercosur");
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
                _ErrorProvider.SetError(txtPais, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }

        private void txtCiudad_Validating(object sender, CancelEventArgs e)
        {

            _ErrorProvider.Clear();
            try
            {
                if (string.IsNullOrWhiteSpace(txtCiudad.Text.Trim()))
                    throw new Exception("Ingrese ciudad");
                if (txtCiudad.Text.Trim().Any(char.IsNumber))
                    throw new Exception("La ciudad no puede contener numeros");
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
                _ErrorProvider.SetError(txtCiudad, ex.Message);
                if (ex.Message.Length > 80)
                    lblError.Text = ex.Message.Substring(0, 80);
                else
                    lblError.Text = ex.Message;
            }
        }
    }
}
