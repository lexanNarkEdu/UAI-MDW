using BE;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Facturas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Usuario usuariologeado = (Usuario)Session["Usuario"];

        if (usuariologeado != null)
        {
            habilitarMenusSegunRol(usuariologeado.Permiso.Nombre.Trim());
        }

        if (!IsPostBack)
        {
            CargarFacturas();
            CargarPagos();
        }
    }

    private void CargarFacturas()
    {
        gvFacturas.DataSource = new List<dynamic>
        {
            new { Numero = "F001", Fecha = DateTime.Today.AddDays(-10), Estado = "Pagada" },
            new { Numero = "F002", Fecha = DateTime.Today.AddDays(-5), Estado = "Impaga" },
            new { Numero = "F003", Fecha = DateTime.Today.AddDays(-20), Estado = "Vencida" },
        };
        gvFacturas.DataBind();
    }

    private void CargarPagos()
    {
        gvPagos.DataSource = new List<dynamic>
        {
            new { PagoId = 101, Fecha = DateTime.Today.AddDays(-8), Importe = 15000.00 },
            new { PagoId = 102, Fecha = DateTime.Today.AddDays(-3), Importe = 20000.00 }
        };
        gvPagos.DataBind();
    }

    protected void gvFacturas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerDetalleFactura")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string numero = gvFacturas.Rows[index].Cells[0].Text;
            string fecha = gvFacturas.Rows[index].Cells[1].Text;
            string estado = gvFacturas.Rows[index].Cells[2].Text;

            lblDetalleTitulo.Text = "Detalle de Factura";
            litDetalle.Text = $"<strong>N°:</strong> {numero}<br /><strong>Fecha:</strong> {fecha}<br /><strong>Estado:</strong> {estado}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrir", "abrirModal();", true);
        }
    }

    protected void gvPagos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerDetallePago")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string id = gvPagos.Rows[index].Cells[0].Text;
            string fecha = gvPagos.Rows[index].Cells[1].Text;
            string importe = gvPagos.Rows[index].Cells[2].Text;

            lblDetalleTitulo.Text = "Detalle de Pago";
            litDetalle.Text = $"<strong>ID:</strong> {id}<br /><strong>Fecha:</strong> {fecha}<br /><strong>Importe:</strong> {importe}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrir", "abrirModal();", true);
        }
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
        HtmlGenericControl menuReporte = (HtmlGenericControl)Master.FindControl("liReportes");
        menuReporte.Visible = false;

        if (!permiso.IsNullOrWhiteSpace())
        {
            if (permiso.ToLower().Equals("webmaster"))
            {
                menuAdmin.Visible = true;
                menuCategorias.Visible = false;
                menuFacturasYPagos.Visible = false;
                menuCarrito.Visible = false;
                menuAbout.Visible = false;
                menuReporte.Visible = true;

            }
            else if (permiso.ToLower().Equals("admin"))
            {
                menuAdmin.Visible = false;
                menuCategorias.Visible = true;
                menuFacturasYPagos.Visible = true;
                menuCarrito.Visible = false;
                menuAbout.Visible = true;
                menuReporte.Visible = true;
            }
        }
    }
}