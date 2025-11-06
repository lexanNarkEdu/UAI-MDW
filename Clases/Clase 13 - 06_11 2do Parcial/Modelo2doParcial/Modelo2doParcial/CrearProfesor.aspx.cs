using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class CrearProfesor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void crearProfesor_Click(object sender, EventArgs e)
    {

        string Apellido = apellido.Text.ToString();
        int Sueldo = int.Parse(sueldo.Text.ToString());
        string Condicion = ddlCondicion.SelectedValue.ToString();

        string rutaXML = Server.MapPath("~/Profesores.xml");
        XDocument doc;
        doc = XDocument.Load(rutaXML);

        int nuevoID = doc.Root.Elements("Profesor").Count() + 1;

        doc.Root.Add(
            new XElement("Profesor",
                new XAttribute("ID", nuevoID.ToString("000")),
                new XElement("Apellido", Apellido),
                new XElement("Sueldo", Sueldo),
                new XElement("Condicion", Condicion)
            )
        );

        doc.Save(rutaXML);
        Response.Write("Profesor creado: " + Apellido);

    }

    protected void VolverMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}