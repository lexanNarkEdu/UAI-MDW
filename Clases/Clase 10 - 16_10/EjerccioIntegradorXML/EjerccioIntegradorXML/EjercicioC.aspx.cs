using System;
using System.Xml;

public partial class EjercicioC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            XmlDocument mlDoc = new XmlDocument();
            
            XmlTextReader mlector = new XmlTextReader(Server.MapPath("EjercicioLibros.xml"));
            mlector.WhitespaceHandling = WhitespaceHandling.None;
            mlDoc.Load(mlector);
            Session.Add("DocumentoEnSesion", mlDoc);
            
            for (int i = 0; i < mlDoc.DocumentElement.ChildNodes.Count; i++)
            {
                ddlLibros.Items.Add(mlDoc.DocumentElement.ChildNodes[i].ChildNodes[1].InnerText);
            }
            mlector.Close();
        }
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        int n;
        int cantidad = 1;
        double precio;
        XmlDocument mlDoc;

        mlDoc = (XmlDocument)Session["DocumentoEnSesion"];
        n = ddlLibros.SelectedIndex;
        cantidad = Convert.ToInt32(txtCantidad.Text);
        precio = Convert.ToDouble(mlDoc.DocumentElement.ChildNodes[n].ChildNodes[4].InnerText);
        
        lblPrecio.Text = precio.ToString();
        lblTotal.Text = (cantidad * precio).ToString();
    }

    protected void menuPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuPrincipal.aspx");
    }
}
