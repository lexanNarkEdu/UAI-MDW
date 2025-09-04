using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase4_04_09
{
    public partial class Adrotatorpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            Response.Write("Hola " + Nombre.Text + " has elegido:" + Categoria.SelectedItem.Text);
        }
    }
}