using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BE;
using BE.Servicios;
using BLL.Servicios;
using Microsoft.Ajax.Utilities;

public partial class Bitacora : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Usuario usuariologeado = (Usuario)Session["Usuario"];

        try
        {            
            if (usuariologeado == null || usuariologeado.Permiso.Nombre != "Webmaster")
            {
                Response.Redirect("Default.aspx");
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx");
        }

        if (usuariologeado != null)
        {
            habilitarMenusSegunRol(usuariologeado.Permiso.Nombre);
        }

    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    { if (string.IsNullOrEmpty(txtFechaDesde.Text) || string.IsNullOrEmpty(txtFechaHasta.Text))
        {
            string script = "alert('Debe seleccionar un rango de fechas');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            return;
        }
        DateTime fechaDesde = DateTime.Parse(txtFechaDesde.Text).Date;
        DateTime fechaHasta = DateTime.Parse(txtFechaHasta.Text).AddHours(23).AddMinutes(59);


        if (fechaDesde > fechaHasta)
        {
            string script = "alert('El rango de fechas es invalido');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

            return;
        }
        BitacoraBL mBitacoraBL = new BitacoraBL();
        try
        {
            List<BitacoraBE> mlista = mBitacoraBL.Listar(fechaDesde, fechaHasta, DropDownList1.Text);
            if (!mlista.Any())
            {
                string script = "alert('No existen datos para los filtros ingresados');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            GridView1.DataSource = mlista.AsEnumerable().Reverse();
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            string script = "alert('Ocurrio un error');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false; // Oculta la primera celda de cada fila
        }

        //  oculta la celda del encabezado
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
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