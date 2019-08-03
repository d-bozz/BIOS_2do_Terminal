using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Controles
{
    class DropDownListMinutos:DropDownList
    {
        //atributo
        List<int> _minutos;

        //constructor
        public DropDownListMinutos()
        {
            _minutos = new List<int>();
            for (int i = 0; i <= 59; i++)
            {
                _minutos.Add(i);
                this.DataSource = _minutos;
                this.SelectedIndex = DateTime.Now.Minute;
                this.DataBind();
            }
        }

        public int SeleccionMinuto
        {
            get { return _minutos[this.SelectedIndex]; }
            set 
            {
                if (value >= 0 && value <= 59)
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
