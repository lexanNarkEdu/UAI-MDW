using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase2_21_08
{
    public partial class Respuesta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (float.Parse(Session["Sueldo"].ToString()) > 100000)
            {
                Response.Write("Sueldo es alto");
            }
            else
            {
                Response.Write("Sueldo es bajo");
            }
        }
    }
}