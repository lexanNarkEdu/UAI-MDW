using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase3_28_08_Evaluacion
{
    public partial class ejercicio : System.Web.UI.Page
    {
        private readonly string[] _librosLibreriaLosada = new string[]
        {
            "Compiladores ($750)",
            "Teoria de Numeros ($630)",
            "Ajax ($880)"
        };

        private readonly string[] _librosLibreriaHernadez = new string[]
        {
            "Ficciones ($500)",
            "Metamorfosis ($400)",
            "Semiotica ($900)"
        };

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var items = CheckBoxList1.Items.Cast<ListItem>().Where(item => item.Selected);

            if (items.Count() < 4)
                return;

            var itemsGrupo1 = CheckBoxList1.Items.Cast<ListItem>()
                .Where(item => item.Selected && _librosLibreriaLosada.Contains(item.Text))
                .ToList();

            var itemsGrupo2 = CheckBoxList1.Items.Cast<ListItem>()
                .Where(item => item.Selected && _librosLibreriaHernadez.Contains(item.Text))
                .ToList();

            Response.Cookies["datos"]["itemGrupo1"] = string.Join(",", itemsGrupo1.Select(item => double.Parse(item.Value)));
            Response.Cookies["datos"]["itemGrupo2"] = string.Join(",", itemsGrupo2.Select(item => double.Parse(item.Value)));
            Response.Redirect("servidor.aspx");
        }
    }
}