using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Administracion.Servicio;
using System.Xml;
using System.Xml.Linq;

namespace Administracion
{
    public partial class Estadisticas : Form
    {
        XElement _doc = null;
        string ninguno = "_NINGUNO_";
        public Estadisticas()
        {
            InitializeComponent();
        }

        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                cbPais.SelectedValue = ninguno;
                dtpDesde.Value = DateTime.Today;
                dtpHasta.Value = DateTime.Today.AddYears(1);

                if (_doc != null)
                {
                    var resultado = (from unViaje in _doc.Elements("Viaje")
                                     select new
                                     {
                                         Nro = unViaje.Element("Numero").Value,
                                         CiudadDestino = unViaje.Element("CiudadDestino").Value,
                                         PaisDestino = unViaje.Element("PaisDestino").Value,
                                         Compania = unViaje.Element("Compania").Value,
                                         FechaPartida = unViaje.Element("FechaPartida").Value
                                     }).ToList();

                    GVDatos.DataSource = resultado;

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

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_doc != null)
                {
                      var resultado = (from unViaje in _doc.Elements("Viaje")
                                     where Convert.ToDateTime(unViaje.Element("FechaPartida").Value).Date >= dtpDesde.Value.Date && Convert.ToDateTime(unViaje.Element("FechaPartida").Value).Date <= dtpHasta.Value.Date
                                     && (unViaje.Element("PaisDestino").Value.Equals(cbPais.SelectedValue) || cbPais.SelectedValue.Equals(ninguno))
                                     select new
                                     {
                                         Nro = unViaje.Element("Numero").Value,
                                         CiudadDestino = unViaje.Element("CiudadDestino").Value,
                                         PaisDestino = unViaje.Element("PaisDestino").Value,
                                         Compania = unViaje.Element("Compania").Value,
                                         FechaPartida = unViaje.Element("FechaPartida").Value
                                     }).ToList();

                    GVDatos.DataSource = resultado;
                    lblError.Text = "Mostrando datos filtrados.";
                }
                else
                {
                    lblError.Text = "No hay datos cargados para filtrar.";
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

        private void Estadisticas_Load(object sender, EventArgs e)
        {
            try
            {
                _doc = XElement.Parse(new ServicioTerminal().ListarEstadisticas());

                var resultado = (from unViaje in _doc.Elements("Viaje")
                                 select new
                                 {
                                     Nro = unViaje.Element("Numero").Value,
                                     CiudadDestino = unViaje.Element("CiudadDestino").Value,
                                     PaisDestino = unViaje.Element("PaisDestino").Value,
                                     Compania = unViaje.Element("Compania").Value,
                                     FechaPartida = unViaje.Element("FechaPartida").Value
                                 }).ToList();

                GVDatos.DataSource = resultado; 

                //cargar combobox paises 
                var paises = (from unViaje in _doc.Elements("Viaje")
                              group unViaje by unViaje.Element("PaisDestino").Value into pais
                              select new
                              {
                                  PaisDestino = pais.Key
                              }).ToList();

                paises.Add(new { PaisDestino = ninguno });
                cbPais.DataSource = paises;

                cbPais.SelectedValue = ninguno;
                dtpDesde.Value = DateTime.Today;
                dtpHasta.Value = DateTime.Today.AddYears(1);
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

        private void btnVxC_Click(object sender, EventArgs e)
        {
            try
            {
                if (_doc != null)
                {
                    var resultado = (from unViaje in _doc.Elements("Viaje")
                                     group unViaje by new { Compania = unViaje.Element("Compania").Value, Año = Convert.ToDateTime(unViaje.Element("FechaPartida").Value).Year }
                                         into viajesCompanias
                                         select new
                                         {
                                             Compania = viajesCompanias.Key.Compania,
                                             Año = viajesCompanias.Key.Año,
                                             Cantidad = viajesCompanias.Count()
                                         }).ToList();

                    GVDatos.DataSource = resultado;
                    lblError.Text = "Mostrando datos filtrados.";
                }
                else
                {
                    lblError.Text = "No hay datos cargados para filtrar.";
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
