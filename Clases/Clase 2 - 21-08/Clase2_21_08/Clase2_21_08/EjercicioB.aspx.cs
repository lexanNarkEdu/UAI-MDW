using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Clase2_21_08
{
    public partial class EjercicioB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            XmlTextWriter miEscritor = new XmlTextWriter(Server.MapPath("Clientes.xml"), null);
            miEscritor.Formatting = Formatting.Indented;
            miEscritor.WriteStartDocument();
            miEscritor.WriteComment("Escribir datos en XML");
            miEscritor.WriteStartElement("DatosCliente");
            miEscritor.WriteAttributeString("NombreTipo", "Guardar");
            miEscritor.WriteStartElement("NombreNumero", "");
            miEscritor.WriteString(NombreTxt.Text);
            miEscritor.WriteEndElement();

            miEscritor.WriteStartElement("Apellidos", "");
            miEscritor.WriteString(ApellidoTxt.Text);
            miEscritor.WriteEndElement();

            miEscritor.WriteStartElement("Direccion", "");
            miEscritor.WriteString(DireccionTxt.Text);
            miEscritor.WriteEndElement();

            miEscritor.WriteEndDocument();
            miEscritor.Flush();
            miEscritor.Close();

            Response.Redirect("Clientes.xml");
        }
    }
}