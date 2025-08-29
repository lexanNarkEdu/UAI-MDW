using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase3_28_08
{
    public partial class cliente : System.Web.UI.Page
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

            WebService1 Pepe = new WebService1();
            double resultado = Pepe.Sumar(n1, n2);
            lblResultado.Text = resultado.ToString();
        }
    }
}