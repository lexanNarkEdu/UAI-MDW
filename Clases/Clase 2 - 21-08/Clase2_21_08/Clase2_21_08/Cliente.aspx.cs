using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Clase2_21_08
{
    public partial class Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlTextReader xmlTextReader = new XmlTextReader(Server.MapPath("xmldatos.xml"));
            int n;

            Session["Apellido"] = "";
            Session["Sueldo"] = "";

            while (xmlTextReader.Read())
            {
                xmlTextReader.MoveToElement();
                if (xmlTextReader.Name == "Apellido")
                {
                    Session["Apellido"] = xmlTextReader.ReadString();
                }
                if (xmlTextReader.Name == "Sueldo")
                {
                    Session["Sueldo"] = xmlTextReader.ReadString();
                }
            }
            xmlTextReader.Close();
            Response.Redirect("Respuesta.aspx");
        }
    }
}