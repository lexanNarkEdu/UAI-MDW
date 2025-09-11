using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Clase5_11_09
{
    public partial class Servidor : System.Web.UI.Page
    {
        private readonly string CTE_Python = "Python";

        protected void Page_Load(object sender, EventArgs e)
        {
            bool esNativo = bool.Parse(Request.Cookies["datos"]["esNativo"]);
            string[] diasSeleccionados = Request.Cookies["datos"]["diasSeleccionados"].Split(',');
            string materia = Request.Cookies["datos"]["materia"];

            Profesor profesor = JsonConvert.DeserializeObject<Profesor>(Request.Cookies["datos"]["datosProfesor"]);

            if (esNativo && materia == CTE_Python)
            {
                WebServiceCalculoContraPrestacion webService = new WebServiceCalculoContraPrestacion();
                LabelResultadoWebService.Text = webService.CuantoCobra(
                    profesor.Apellido,
                    diasSeleccionados,
                    profesor.ContraPrestacion);

                LabelResultadoWebService.Visible = true;
            }
            else 
            {
                LabelNoFuePosibleCalcular.Text = $"No fue posible calcular la contraprestación, del Sr {profesor.Apellido}";
                LabelNoFuePosibleCalcular.Visible = true;
            }
        }
    }
}