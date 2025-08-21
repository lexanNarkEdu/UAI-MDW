using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Clase2_21_08
{
    public partial class EjercicioC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                XmlDocument miDoc = new XmlDocument();
                XmlTextReader miLector = new XmlTextReader(Server.MapPath("inventario2.xml"));
                miLector.WhitespaceHandling = WhitespaceHandling.None;
                miDoc.Load(miLector);
                Session.Add("DocumentoEnSession", miDoc);

                for (int i = 0; i < miDoc.DocumentElement.ChildNodes.Count; i++)
                {
                    ddlProducto.Items.Add(miDoc.DocumentElement.ChildNodes[i].ChildNodes[1].InnerText);
                }
                miLector.Close();
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            int n;
            int cantidad = 1;
            double precio;
            XmlDocument miDoc;
            miDoc = (XmlDocument)Session["DocumentoEnSession"];
            n = ddlProducto.SelectedIndex;
            cantidad = Convert.ToInt32(txtCantidad.Text);

            precio = Convert.ToDouble(miDoc.DocumentElement.ChildNodes[n].ChildNodes[2].InnerText);

            lblPrecio.Text = precio.ToString();
            lblTotal.Text = (cantidad * precio).ToString();
        }
    }
}