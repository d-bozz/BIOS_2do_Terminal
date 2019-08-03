using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//espacio de nombres necesario para heredar
using System.Web.UI.WebControls;

namespace Controles
{
    public class ListBoxMeses:DropDownList
    {
        //atributos
        private List<string> _Meses;

        //constructor
        public ListBoxMeses()
            : base()
        { 
            //cargo los meses
            _Meses = new List<string>();
            _Meses.Add("Enero");
            _Meses.Add("Febrero");
            _Meses.Add("Marzo");
            _Meses.Add("Abril");
            _Meses.Add("Mayo");
            _Meses.Add("Junio");
            _Meses.Add("Julio");
            _Meses.Add("Agosto");
            _Meses.Add("Setiembre");
            _Meses.Add("Octubre");
            _Meses.Add("Noviembre");
            _Meses.Add("Diciembre");

            //asigno la lista al list
            this.DataSource = _Meses;
            this.SelectedIndex = DateTime.Today.Month - 1;
            this.DataBind();
        }

        //manejo mes por nombre
        public string SelectedMes
        {
            get { return (_Meses[this.SelectedIndex]); }
            set
            {
                switch (value.ToLower())
                { 
                    case "enero":
                        this.SelectedIndex = 0;
                        break;
                    case "febrero":
                        this.SelectedIndex = 1;
                        break;
                    case "marzo":
                        this.SelectedIndex = 2;
                        break;
                    case "abril":
                        this.SelectedIndex = 3;
                        break;
                    case "mayo":
                        this.SelectedIndex = 4;
                        break;
                    case "junio":
                        this.SelectedIndex = 5;
                        break;
                    case "julio":
                        this.SelectedIndex = 6;
                        break;
                    case "agosto":
                        this.SelectedIndex = 7;
                        break;
                    case "setiembre":
                        this.SelectedIndex = 8;
                        break;
                    case "octubre":
                        this.SelectedIndex = 9;
                        break;
                    case "noviembre":
                        this.SelectedIndex = 10;
                        break;
                    case "diciembre":
                        this.SelectedIndex = 11;
                        break;
                    default:
                        {
                            throw new InvalidCastException("El texto asignado no se corresponde con ningun mes.");
                        }
                }//final switch
            }//final set
        }//final propiedad

        //manejo mes por numero.
        public int SeleccionMes
        {
            get { return (this.SelectedIndex + 1); }
            set 
            {
                if (value >= 1 && value <= 12)
                    this.SelectedIndex = value - 1;
                else
                    throw new InvalidCastException("El numero asignado no se coresponde con ningun mes.");
            }
        }
    }
}
