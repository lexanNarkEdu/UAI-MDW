using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase4_04_09
{
    public partial class pagina3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fechaREUNION.MinimumValue = DateTime.Now.ToShortDateString();
            fechaREUNION.MaximumValue = DateTime.Now.AddMonths(2).ToShortDateString();
        }

        protected void botonCLICK(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Response.Redirect("fw.aspx");
            }
        }
    }
}