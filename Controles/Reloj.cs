using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controles
{
    public class Reloj : WebControl, INamingContainer
    {
        //atributos
        Panel _unPanel;
        DropDownListHoras _horas;
        DropDownListMinutos _minutos;

        public void Activo(bool estado)
        {
            EnsureChildControls();
            _unPanel.Enabled = estado;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            _unPanel = new Panel();

            _horas = new DropDownListHoras();
            _unPanel.Controls.Add(_horas);
            _unPanel.Controls.Add(new LiteralControl(" / "));

            _minutos = new DropDownListMinutos();
            _unPanel.Controls.Add(_minutos);


            this.Controls.Add(_unPanel);
        }

        public DateTime Hora
        {
            set
            {
                _horas.SeleccionHora = value.Hour;
                _minutos.SeleccionMinuto = value.Minute;
            }

            get
            {
                try
                {
                    return new DateTime(1, 1, 1, _horas.SeleccionHora, _minutos.SeleccionMinuto,0);
                }
                catch
                {
                    throw new InvalidCastException("No es una hora valida.");
                }
            }

        }
    }
}
