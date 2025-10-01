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

public partial class DesbloqueoUser : System.Web.UI.Page
{

    private UsuarioBll usuarioBll = new UsuarioBll();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Usuario usuariologeado = (Usuario)Session["Usuario"];
            if (usuariologeado == null || usuariologeado.Permiso.Nombre != "Webmaster")
            {
                Response.Redirect("Default.aspx");
            }

            habilitarMenusSegunRol(usuariologeado.Permiso.Nombre);

        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack)
        {
            CargarUsuarios(false);
        }

        

    }

    private void CargarUsuarios(Boolean postDesbloqueo)
    {

        List<String> usuariosBloqueados = usuarioBll.listarUsuariosBloqueados();

        if (usuariosBloqueados != null && usuariosBloqueados.Count > 0)
        {
            ddlUsuarios.DataSource = null;
            ddlUsuarios.DataBind();
            ddlUsuarios.Enabled = true;
            btnDesbloquear.Enabled = true;

            ddlUsuarios.DataSource = usuariosBloqueados;
            ddlUsuarios.DataBind();

            // Opción de selección por defecto
            ddlUsuarios.Items.Insert(0, "-- Seleccione un usuario --");
        }
        else
        {
            ddlUsuarios.DataSource = null;
            ddlUsuarios.ClearSelection();
            ddlUsuarios.DataBind();
            ddlUsuarios.Enabled = false;
            btnDesbloquear.Enabled = false;
            if (!postDesbloqueo)
            {
                MostrarResultado("No existen usuarios bloqueados al momento.", false);
            }
        }
    }

    protected void btnDesbloquear_Click(object sender, EventArgs e)
    {
        String usuario = ddlUsuarios.SelectedValue;

        if (string.IsNullOrWhiteSpace(usuario) || usuario.StartsWith("--"))
        {
            MostrarResultado("Por favor, seleccione un usuario válido.", false);
            return;
        }

        usuarioBll.bloquearDesbloquearUsuario(usuario);

        MostrarResultado($"El usuario <strong>{usuario}</strong> ha sido desbloqueado exitosamente.", true);

        // Opcional: recargar la lista sin el usuario desbloqueado
        CargarUsuarios(true);
    }

    /// Muestra un mensaje de éxito o error.
    private void MostrarResultado(string mensaje, bool exito)
    {
        lblResultado.Text = mensaje;
        lblResultado.CssClass = exito ? "text-success" : "text-danger";
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
            }
        }
    }
}