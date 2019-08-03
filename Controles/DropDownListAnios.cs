using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI.WebControls;

namespace Controles
{
    public class DropDownListAnios : DropDownList
    {
        //atributos
        List<int> _Anios;

        public DropDownListAnios()
            : base()
        {
            _Anios = new List<int>();
            for (int i = DateTime.Now.Year; i <= DateTime.Now.Year + 20; i++)
            {
                _Anios.Add(i);
            }

            this.DataSource = _Anios;
            this.DataBind();
        }

        public int SelectedAnio
        {
            get { return _Anios[this.SelectedIndex]; }
            set 
            {
                if (value >= DateTime.Now.Year && value <= DateTime.Now.Year + 20)
                {
                    this.SelectedIndex = (value - DateTime.Now.Year); 
                }
                else
                    throw new InvalidCastException("El numero ingresado no esta dentro de los limites.");
            }
        }
    }
}
