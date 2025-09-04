using BE;
using BLL;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class CarritoCompra : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Carrito carrito = Session["Carrito"] as Carrito;
            if (carrito != null)
            {
                gvCarrito.DataSource = carrito.Productos;
                gvCarrito.DataBind();
                lblTotal.Text = $"Total del carrito: ${carrito.CalcularTotal():N2}";
            }
            else
            {
                lblTotal.Text = "El carrito está vacío.";
            }
        }

        Usuario usuariologeado = (Usuario)Session["Usuario"];

        if (usuariologeado != null)
        {
            habilitarMenusSegunRol(usuariologeado.Permiso.Nombre.Trim());
        }
    }

  

    protected void btnFinalizarVenta_Click(object sender, EventArgs e)
    {
        Carrito carrito = Session["Carrito"] as Carrito;
        Usuario usuarioLogueado = Session["Usuario"] as Usuario;
        string metodoPagoSeleccionado = rblMetodoPago.SelectedValue;

        if (carrito != null && carrito.Productos.Any() && usuarioLogueado != null)
        {
            VentaBll ventaBll = new VentaBll();
            ventaBll.RealizarVenta(carrito, metodoPagoSeleccionado, usuarioLogueado.Id_Usuario);

            Session["Carrito"] = null;
           // Response.Redirect("Confirmacion.aspx");
        }
        else
        {
            lblTotal.Text = "No hay productos en el carrito o no hay sesión activa.";
        }
    }

    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
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