using BE;
using BLL;
using BLL.Servicios;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Contact : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Usuario usuariologeado = (Usuario)Session["Usuario"];
            if (usuariologeado == null 
                || !usuariologeado.Permiso.Nombre.Trim().ToLower().Equals("webmaster"))
            {
                Response.Redirect("Default.aspx");
            }

            if (usuariologeado != null)
            {
                habilitarMenusSegunRol(usuariologeado.Permiso.Nombre);
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx");
        }
        if (IntegridadBL.IntegridadCorrupta)
        {
            Label1.Visible = false;
            Button1.Visible = false;
            OcultarMenus();

            string script = $"<script>alert('{Session["mensajeUnico"].ToString()}');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", script);

        }
       
    }

    void OcultarMenus()
    {
        // Lista de todos los IDs a ocultar
        string[] ids = {
        "navbarr",         // Inicio
        "liCategorias",    // Categorías
       "liFacturas",
       "liCarrito",
        "liDesbloqueoUsuario", // userdesbloqueo
        "liAuditoria",     // Auditoría
        //"RestoreBD",       // Base de datos
        "liAbout",         // About
       // "liCerrarSesion"   // Cerrar sesión
    };

        foreach (string id in ids)
        {
            HtmlGenericControl item = Master.FindControl(id) as HtmlGenericControl;
            if (item != null)
                item.Visible = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string nombreArchivo = "Backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";
            
            // Guardar directamente en la carpeta Temp del proyecto
            string rutaProyectoTemp = Server.MapPath("~/Temp/" + nombreArchivo);
            
            // Crear el backup directamente en la ubicación final
            BackupRestore backup = new BackupRestore();
            backup.CrearBackUp(rutaProyectoTemp);
            
            // Mostrar mensaje antes de finalizar
            string script = "alert('El backup se realizó con éxito!');";
            Page.ClientScript.RegisterStartupScript(typeof(Page), "alert", script, true);
            
            // Enviar el archivo al navegador para descarga
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
            Response.TransmitFile(rutaProyectoTemp);
            Response.End();
        }
        catch (Exception ex)
        {
            string script = $"alert('Error al crear backup: {ex.Message}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuRestore.HasFile)
            {
                // Validación adicional server-side
                if (!fuRestore.FileName.EndsWith(".bak", StringComparison.OrdinalIgnoreCase))
                {
                    string script = "alert('Por favor, seleccione un archivo .bak válido');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    return;
                }
                
                // Usar directamente el stream del archivo subido
                using (var fileStream = fuRestore.PostedFile.InputStream)
                {
                    // Crear un archivo temporal simple
                    string tempFile = System.IO.Path.GetTempFileName();
                    
                    try
                    {
                        // Copiar el stream directamente al archivo temporal
                        using (var outputStream = System.IO.File.Create(tempFile))
                        {
                            fileStream.CopyTo(outputStream);
                        }
                        
                        // Ejecutar restore
                        BackupRestore backup = new BackupRestore();
                        backup.EjecutarRestore(tempFile);
                        
                        string script = "alert('El Restore se realizó con éxito!');";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                        
                        Session.Clear();
                        Response.Redirect("~/LogIn.aspx");
                    }
                    finally
                    {
                        // Limpiar archivo temporal
                        if (System.IO.File.Exists(tempFile))
                            System.IO.File.Delete(tempFile);
                    }
                }
            }
            else
            {
                string script = "alert('Por favor, seleccione un archivo .bak');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }
        catch (Exception ex)
        {
            string script = $"alert('Error al restaurar: {ex.Message}');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
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
