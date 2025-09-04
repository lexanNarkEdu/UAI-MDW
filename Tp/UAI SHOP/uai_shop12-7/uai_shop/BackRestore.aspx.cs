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
            string rutaBackupSqlServer = @"C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup" + nombreArchivo;

            
            // 3. Crear el backup usando la clase BackupRestore
            BackupRestore backup = new BackupRestore();
            backup.CrearBackUp(rutaBackupSqlServer);

            // 4. Copiar ese archivo a una carpeta temporal del sitio para que el usuario lo descargue
            string rutaWebTemporal = Server.MapPath("~/Temp/" + nombreArchivo);
            System.IO.File.Copy(rutaBackupSqlServer, rutaWebTemporal, true);

            // 5. Enviar el archivo al navegador para que el usuario lo descargue
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
            Response.TransmitFile(rutaWebTemporal);
            Response.End();
            string script = "alert('El backup se realizo con exito!');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
        catch (Exception ex)
        {
          //  lblResultado.ForeColor = System.Drawing.Color.Red;
          //  lblResultado.Text = "Error: " + ex.Message;
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuRestore.HasFile)
            {
                string nombreArchivo = System.IO.Path.GetFileName(fuRestore.FileName);
                string carpetaDestino = Server.MapPath("~/TempRestore/");

                if (!System.IO.Directory.Exists(carpetaDestino))
                    System.IO.Directory.CreateDirectory(carpetaDestino);

                string rutaTemporalWeb = System.IO.Path.Combine(carpetaDestino, nombreArchivo);
                fuRestore.SaveAs(rutaTemporalWeb);

                // Copiar archivo al directorio de SQL Server
                string rutaSQLServer = @"C:\Users\realp\Downloads" + nombreArchivo;
                System.IO.File.Copy(rutaTemporalWeb, rutaSQLServer, true);

                // Ejecutar restore desde la ubicación donde SQL Server sí tiene acceso
                BackupRestore backup = new BackupRestore();
                backup.EjecutarRestore(rutaSQLServer);
                string script = "alert('El Restore se realizo con exito!');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                Session.Clear();
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                //lblResultado.Text = "Por favor, seleccioná un archivo .bak.";
            }
        }
        catch (Exception ex)
        {
            //lblResultado.ForeColor = System.Drawing.Color.Red;
            //lblResultado.Text = "Error al restaurar: " + ex.Message;
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
