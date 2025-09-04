using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BE;
using BLL;
using DAL;
using Microsoft.Ajax.Utilities;

public partial class _Default : Page
{
    static bool mostrado = false ;
    ProductoBll productoBll = new ProductoBll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                Usuario usuariologeado = (Usuario)Session["Usuario"];
                
                if (usuariologeado != null)
                {
                    Label1.Text = "PERMISO: " + usuariologeado.Permiso.Nombre;

                    /* HtmlGenericControl menuAdmin = (HtmlGenericControl)Master.FindControl("liAdmin");
                     HtmlGenericControl liRestoreBD = (HtmlGenericControl)Master.FindControl("RestoreBD");
                     if (menuAdmin != null)
                         menuAdmin.Visible = usuariologeado.Permiso.Nombre.Equals("Webmaster"); */

                    habilitarMenusSegunRol(usuariologeado.Permiso.Nombre.Trim());

                }
                CargarProductos();
                MostrarTotal();
                // También podés mostrar un alert opcional
                if (mostrado)
                {
                        return;
                }
                string script = "alert('Se logueó el " + usuariologeado.Permiso.Nombre + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                mostrado = true;
            }
        }
        

    }
    private Carrito ObtenerCarrito()
    {
        if (Session["Carrito"] == null)
            Session["Carrito"] = new Carrito();
        return (Carrito)Session["Carrito"];
    }

    private void CargarProductos()
    {
        ProductoBll bll = new ProductoBll();
        var productos = bll.ObtenerTodos();
        rptProductos.DataSource = productos;
        rptProductos.DataBind();
    }
    protected void btnAgregar_Command(object sender, CommandEventArgs e)
    {
        int idProducto = Convert.ToInt32(e.CommandArgument);
        ProductoBll bll = new ProductoBll();
        Producto producto = bll.ObtenerPorId(idProducto);

        Carrito carrito = ObtenerCarrito();
        carrito.AgregarProducto(producto);
        Session["Carrito"] = carrito;

        MostrarTotalCarrito();
        
    }
    protected void btnAgregar2_Click(object sender, EventArgs e)
    {
        
    }

    private void MostrarTotalCarrito()
    {
        Carrito carrito = ObtenerCarrito();
        lblTotal.Text = $"Total del carrito: ${carrito.CalcularTotal()}";
    }
    protected void btnComprar_Click(object sender, EventArgs e)
    {
        Response.Redirect("CarritoCompra.aspx");
    }
    private void MostrarTotal()
    {
        Carrito carrito = ObtenerCarrito();
        lblTotal.Text = $"Total del carrito: ${carrito.CalcularTotal()}";
    }
    void ArmarForm(Usuario pusuario)
        {
        }

    private void habilitarMenusSegunRol(String permiso)
    {

        //por defecto muestro SOLAMENTE los menus como si fuera un comprador
        //esto para que en caso de cualquier error no contemplado, no habilite todo por defecto

        HtmlGenericControl menuAdmin = (HtmlGenericControl)Master.FindControl("liAdmin");
        menuAdmin.Visible = false;
        HtmlGenericControl menuCategorias = (HtmlGenericControl)Master.FindControl("liCategorias");
        menuCategorias.Visible = true;
        HtmlGenericControl menuFacturasYPagos = (HtmlGenericControl)Master.FindControl("liFacturas");
        menuFacturasYPagos.Visible = true;
        HtmlGenericControl menuCarrito = (HtmlGenericControl)Master.FindControl("liCarrito");
        menuCarrito.Visible = true;
        HtmlGenericControl menuAbout = (HtmlGenericControl)Master.FindControl("liAbout");
        menuAbout.Visible = true;

        if (!permiso.IsNullOrWhiteSpace())
        {
            if (permiso.ToLower().Equals("webmaster"))
            {
                menuAdmin.Visible = true;
                menuCategorias.Visible = false;
                menuFacturasYPagos.Visible = false;
                menuCarrito.Visible = false;
                menuAbout.Visible = false;

            }
            else if (permiso.ToLower().Equals("admin"))
            {
                menuAdmin.Visible = false;
                menuCategorias.Visible = true;
                menuFacturasYPagos.Visible = true;
                menuCarrito.Visible = false;
                menuAbout.Visible = true;
            }
        }
    }
}
