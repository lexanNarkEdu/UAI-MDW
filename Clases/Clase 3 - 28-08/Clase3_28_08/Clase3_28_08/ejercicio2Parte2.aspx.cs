using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase3_28_08
{
    public partial class ejercicio2Parte2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            double n1;
            double n2;

            //Converti el string a double TextBox1 y TextBox2

            n1 = double.Parse(TextBox1.Text);
            n2 = double.Parse(TextBox2.Text);

            WebServiceEjercicioBParte2 Pepe = new WebServiceEjercicioBParte2();
            double resultado = Pepe.Potencia(n1, n2);
            lblResultado.Text = resultado.ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            double n1;
            double n2;

            //Converti el string a double TextBox1 y TextBox2

            n1 = double.Parse(TextBox1.Text);
            n2 = double.Parse(TextBox2.Text);

            WebServiceEjercicioBParte2 Pepe = new WebServiceEjercicioBParte2();
            double resultado = Pepe.Raiz(n1, n2);
            lblResultado.Text = resultado.ToString();
        }
    }
}