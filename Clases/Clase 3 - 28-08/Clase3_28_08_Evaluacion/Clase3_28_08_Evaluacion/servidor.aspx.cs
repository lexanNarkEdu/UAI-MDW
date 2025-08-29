using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase3_28_08_Evaluacion
{
    public partial class servidor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var itemsGrupo1 = Request.Cookies["datos"]["itemGrupo1"].ToString().Split(',').Select(double.Parse).ToArray();
            var itemsGrupo2 = Request.Cookies["datos"]["itemGrupo2"].ToString().Split(',').Select(double.Parse).ToArray();
            WebService1 pepe = new WebService1();
            LblTotalLibreriaLosada.Text = pepe.Calcular(itemsGrupo1).ToString();
            LblTotalLibreriaHernadez.Text = pepe.Calcular(itemsGrupo2).ToString();
        }
    }
}