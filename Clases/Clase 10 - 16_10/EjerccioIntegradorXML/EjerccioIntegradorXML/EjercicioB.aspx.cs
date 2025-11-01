using System;
using System.Xml;

public partial class EjercicioB : System.Web.UI.Page
{
    protected void btnEscribir_Click(object sender, EventArgs e)
    {
        string pathxml = Server.MapPath("EjercicioLibros.xml");
        
        XmlDocument doc = new XmlDocument();
        doc.Load(pathxml);
        
        XmlNode nodeLib = doc.CreateElement("Libro");
        
        XmlElement nodeID = doc.CreateElement("ID");
        nodeID.InnerText = txtIdLibros.Text;
        
        XmlElement NameBook = doc.CreateElement("NombreLibro");
        NameBook.InnerText = txtNombre.Text;
        
        XmlElement nodeAutor = doc.CreateElement("Autor");
        nodeAutor.InnerText = txtAutor.Text;
        
        XmlElement nodeEdit = doc.CreateElement("Editorial");
        nodeEdit.InnerText = txtEditorial.Text;
        
        XmlNode nodePre = doc.CreateElement("Precio");
        nodePre.InnerText = txtPrecio.Text;
        
        nodeLib.AppendChild(nodeID);
        nodeLib.AppendChild(NameBook);
        nodeLib.AppendChild(nodeAutor);
        nodeLib.AppendChild(nodeEdit);
        nodeLib.AppendChild(nodePre);
        
        doc.SelectSingleNode("Inventario").AppendChild(nodeLib);
        
        doc.Save(pathxml);
        
        Response.Redirect("ListarLibros.aspx");
    }
}
