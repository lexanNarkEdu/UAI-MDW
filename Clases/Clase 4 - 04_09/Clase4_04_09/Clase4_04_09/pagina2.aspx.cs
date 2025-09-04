using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase4_04_09
{
    public partial class pagina2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            fechareunion.MinimumValue = DateTime.Now.ToShortDateString();
            fechareunion.MaximumValue = DateTime.Now.AddMonths(2).ToShortDateString();

            TextBox1.Text = DateTime.Now.ToShortDateString();
        }

        protected void BotonCLICK(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Response.Redirect("fw.aspx");
            }
        }

        protected void elegirfecha(object sender, EventArgs e)
        {
            TextBox2.Text = Calendar1.SelectedDate.Date.ToShortDateString();
        }
    }
}