using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Clase5_11_09
{
    public partial class EjercicioParcial : System.Web.UI.Page
    {
        private readonly string CTE_Nativo_Si = "Si";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                XmlDocument miDoc = new XmlDocument();
                XmlTextReader miLector = new XmlTextReader(Server.MapPath("datos.xml"));
                miLector.WhitespaceHandling = WhitespaceHandling.None;
                miDoc.Load(miLector);

                Profesor profesor = new Profesor();

                if (miDoc.DocumentElement != null)
                {
                    profesor.Apellido = miDoc.DocumentElement["Apellido"].InnerText;
                    LabelApellido.Text = profesor.Apellido;

                    profesor.Asignatura = miDoc.DocumentElement["Asignatura"].InnerText;
                    LabelAsignatura.Text = profesor.Asignatura;

                    profesor.ContraPrestacion = decimal.Parse(miDoc.DocumentElement["Contraprestacion"].InnerText);
                    LabelContraPrestacion.Text = profesor.ContraPrestacion.ToString("C");
                }

                Session.Add("datosProfesor", JsonConvert.SerializeObject(profesor));
                miLector.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bool esNativo = RadioButtonListNativo.SelectedItem.Text == CTE_Nativo_Si;
            string[] diasSeleccionados = CheckboxListDiasClase.Items.Cast<ListItem>()
                .Where(item => item.Selected)
                .Select(item => item.Text)
                .ToArray();
            
            string materia = DropDownListMateria.SelectedItem.Text;

            Response.Cookies["datos"]["esNativo"] = esNativo.ToString();
            Response.Cookies["datos"]["diasSeleccionados"] = string.Join(",", diasSeleccionados);
            Response.Cookies["datos"]["materia"] = materia;
            Response.Cookies["datos"]["datosProfesor"] = Session["datosProfesor"].ToString();

            Response.Redirect("Servidor.aspx");
        }
    }

    public class Profesor
    {
        public string Apellido { get; set; }
        public string Asignatura { get; set; }
        public decimal ContraPrestacion { get; set; }
    }
}