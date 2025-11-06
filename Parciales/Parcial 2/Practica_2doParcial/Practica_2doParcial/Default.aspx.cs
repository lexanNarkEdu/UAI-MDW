using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Practica_2doParcial
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument miDoc = new XmlDocument();
            XmlTextReader miLector = new XmlTextReader(Server.MapPath("Profesores.xml"));
            miLector.WhitespaceHandling = WhitespaceHandling.None;
            miDoc.Load(miLector);
        }
    }
}