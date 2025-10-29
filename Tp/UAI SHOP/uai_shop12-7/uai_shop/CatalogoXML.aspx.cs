using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class CatalogoXML : System.Web.UI.Page
{

    private readonly CategoriaBLL _catBll = new CategoriaBLL();
    private readonly ProductoBll _prodBll = new ProductoBll();

    private string AppData => Server.MapPath("~/App_Data/");
    private string XmlFiltradoPath => Path.Combine(AppData, "catalogo_filtrado.xml");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AsegurarXml();             // si no existe, lo genera
            CargarCategoriasDesdeXml(); // llena el dropdown leyendo el XML filtrado
        }
    }

    protected void BtnBuscar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(DdlCategorias.SelectedValue))
        {
            GvProductos.DataSource = null;
            GvProductos.DataBind();
            LblMsg.Text = "Seleccioná una categoría para buscar.";
            return;
        }

        int idCategoria = int.Parse(DdlCategorias.SelectedValue);

        // Traer productos desde la BLL y bindear la grilla
        DataTable productos = _catBll.ObtenerCategoriasPorId(idCategoria);

        GvProductos.DataSource = productos;
        GvProductos.DataBind();

        LblMsg.Text = productos.Rows.Count == 0
            ? "No hay productos para la categoría seleccionada."
            : $"{productos.Rows.Count} producto(s) encontrado(s).";
    }
    protected void BtnRegenerar_Click(object sender, EventArgs e)
    {
        _catBll.GenerarXmlCategorias(AppData);
        CargarCategoriasDesdeXml(); // refresca el combo con el XML recién generado
        LblMsg.Text = "Archivo XML regenerado correctamente.";
    }
    private void AsegurarXml()
    {
        if (!File.Exists(XmlFiltradoPath))
        {
           
                _catBll.GenerarXmlCategorias(AppData); // genera categorias.xml y categorias_filtrado.xml
        }
    }
    private void CargarCategoriasDesdeXml()
    {
        DdlCategorias.Items.Clear();

        using (XmlReader reader = XmlReader.Create(XmlFiltradoPath))
        {
            string id = null, nombre = null, cantidad = null;

            while (reader.Read())
            {
                // Si el nodo es <Categoria>, reiniciamos variables
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Categoria")
                {
                    id = nombre = cantidad = null;
                }

                // Leemos cada elemento dentro de <Categoria>
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "IdCategoria":
                            id = reader.ReadElementContentAsString();
                            break;
                        case "NombreCategoria":
                            nombre = reader.ReadElementContentAsString();
                            break;
                        case "CantidadProductos":
                            cantidad = reader.ReadElementContentAsString();
                            break;
                    }
                }

                // Cuando llega al final de una categoría, la agregamos al combo
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Categoria")
                {
                    if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(nombre))
                    {
                        DdlCategorias.Items.Add(
                            new ListItem($"{nombre} (cant. productos {cantidad})", id)
                        );
                    }
                }
            }
        }
    }
}