using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Xsl;
using System.Xml;

public partial class ListarLibros : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("EjercicioLibros.xml"));

        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(Server.MapPath("XSLTFILE1.xslt"));

        using (StringWriter sw = new StringWriter())
        {
            xslt.Transform(xmlDoc, null, sw);
            litLibros.Text = sw.ToString();
        }
    }

    protected void menuPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuPrincipal.aspx");    
    }
}