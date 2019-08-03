using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controles
{
    public class NuevoCalendario:WebControl,INamingContainer
    {

        //atributos
        Panel _unPanel;
        DropDownListDias _ddlDias;
        ListBoxMeses _lbxMeses;
        DropDownListAnios _ddlAnios;

        public void Activo(bool estado)
        {
            EnsureChildControls();
            _unPanel.Enabled = estado;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            _unPanel = new Panel();

            _ddlDias = new DropDownListDias();
            _unPanel.Controls.Add(_ddlDias);
            _unPanel.Controls.Add(new LiteralControl(" / "));
            
            _lbxMeses = new ListBoxMeses();
            _unPanel.Controls.Add(_lbxMeses);
            _unPanel.Controls.Add(new LiteralControl(" / "));

            _ddlAnios = new DropDownListAnios();
            _unPanel.Controls.Add(_ddlAnios);


            this.Controls.Add(_unPanel);
        }

        public DateTime Fecha 
        {
            set 
            {
                _ddlDias.SeleccionDia = value.Day;
                _lbxMeses.SeleccionMes = value.Month;
                _ddlAnios.SelectedAnio = value.Year;
            }

            get
            {
                try
                {
                    return new DateTime(_ddlAnios.SelectedAnio, _lbxMeses.SeleccionMes, _ddlDias.SeleccionDia);
                }
                catch
                {
                    throw new InvalidCastException("No es una fecha valida.");
                }
            }

        }

    }
}
