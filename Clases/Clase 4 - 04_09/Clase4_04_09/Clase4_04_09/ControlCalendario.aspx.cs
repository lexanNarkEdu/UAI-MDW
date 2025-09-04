using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase4_04_09
{
    public partial class ControlCalendario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CalendarioCAMBIARSELECCION(object sender, EventArgs e)
        {
            lblFechas.Text = "<h2>Usted eligió las siguientes fechas:</h2>";
            foreach (DateTime fechita in calendario.SelectedDates)
            {
                lblFechas.Text += "<li>" + fechita.ToString("D") + "</li>";
            }
        }
    }
}