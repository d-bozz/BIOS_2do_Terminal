using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Controles
{
    class DropDownListHoras:DropDownList
    {
        //atributo
        List<int> _horas;

        //constructor
        public DropDownListHoras()
        {
            _horas = new List<int>();
            for (int i = 0; i <= 23; i++)
            {
                _horas.Add(i);
            }
            this.DataSource = _horas;
            this.SelectedIndex = DateTime.Now.Hour;
            this.DataBind();
        }

        public int SeleccionHora
        {
            get { return _horas[this.SelectedIndex]; }
            set
            {
                if (value >= 0 && value <= 23)
                {
                    this.SelectedIndex = value;
                }
                else
                {
                    throw new InvalidCastException("No es una hora valida.");
                }
            }
        }

    }
}
