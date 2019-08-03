using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI.WebControls;

namespace Controles
{
    public class DropDownListDias : DropDownList
    {
        //atributos
        List<int> _Dias;

        //constructor
        public DropDownListDias()
            : base()
        {
            _Dias = new List<int>();
            
            for (int i = 1; i<=31;i++)
            {
                _Dias.Add(i);
            }

            this.DataSource = _Dias;
            this.SelectedIndex = DateTime.Today.Day - 1;
            this.DataBind();
        }

        public int SeleccionDia
        {
            get { return _Dias[this.SelectedIndex]; }
            set 
            {
                if (value >= 1 && value <= 31)
                {
                    this.SelectedIndex = value - 1;
                }
                else
                {
                    throw new InvalidCastException("No es un dia valido.");
                }
            }
        }
    }
}
