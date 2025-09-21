using BE;
using BLL;
using BLL.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string currentPage = System.IO.Path.GetFileName(Request.Path);
        if (currentPage.Equals("Login.aspx", StringComparison.OrdinalIgnoreCase))
        {
          
        }
    }
    IntegridadBL integridadBL = new IntegridadBL();


    protected void ButtonLogin_Click(object sender, EventArgs e)

    {
        Usuario usuario = new Usuario();
        usuario.USERNAME = TextBox1User.Text;
        usuario.Clave = TextBox2pass.Text;
        UsuarioBll usuarioBll = new UsuarioBll();
        List<string> erroresIntegridad = integridadBL.VerificarIntegridad();
        Usuario usuariobd = usuarioBll.Buscar(usuario, erroresIntegridad.Count == 0);
       
        if (usuariobd != null)
        {//new
            try
            {
                if(erroresIntegridad.Count > 0)//hay errores
                {
                    if (usuariobd.Permiso.Nombre != "Webmaster")
                    {
                        Response.Write("<script>alert('Error de integridad. Contacte al WebMaster');</script>");
                        return;
                    }
                    
                    // Para webmaster: mostrar modal con opciones
                    string erroresDetallados = HttpUtility.JavaScriptStringEncode(string.Join("\\n", erroresIntegridad));
                    Session["Usuario"] = usuariobd;
                    Session["mensajeUnico"] = HttpUtility.JavaScriptStringEncode(string.Join("\n", erroresIntegridad)); ;
                    BitacoraBL.RegistrarBitacora(TipoEvento.Message, usuariobd, "Webmaster detectó error de integridad");
                    
                    string modalScript = $@"
                        if(confirm('ERRORES DE INTEGRIDAD DETECTADOS:\\n\\n{erroresDetallados}\\n\\n¿Desea recalcular los hashes automáticamente?\\n\\nSí: Recalcular hashes y cerrar sesión\\nNo: Ir a página de backup')) {{
                            document.getElementById('{hdnRecalcularHashes.ClientID}').value = 'true';
                            document.getElementById('{btnRecalcularHashes.ClientID}').click();
                        }} else {{
                            window.location.href = 'BackRestore.aspx';
                        }}";
                    
                    ClientScript.RegisterStartupScript(this.GetType(), "modalIntegridad", modalScript, true);
                    return;
                }
            }
            catch (Exception)
            {
                string mensajeUnico = HttpUtility.JavaScriptStringEncode(string.Join("\n", erroresIntegridad));
                string script = $"<script>alert('{mensajeUnico}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alerta", script);

            }
            //end new
            BitacoraBL.RegistrarBitacora(TipoEvento.Message, usuariobd, "Se logeo el "+usuariobd.Permiso.Nombre+": "+usuariobd.USERNAME);
            Session["Usuario"] = usuariobd;            
         Response.Redirect("Default.aspx");
            
        }
        else 
        {//pato
            //busco la cantidad de intentos fallidos y si esta bloqueado
            Usuario userIntentando = usuarioBll.listar(usuario.USERNAME);

            if (userIntentando != null)
            {
                if (userIntentando.ESTA_BLOQUEADO)
                {
                    BitacoraBL.RegistrarBitacoraSinUsuario(TipoEvento.Warning, "Se registro un intento de ingreso de un usuario bloqueado: " + usuario.USERNAME);
                    lblError.Text = "El usuario está bloqueado, contacte a un WEBMASTER."; lblError.Visible = true;
                }
                else
                {
                    //si llego hasta aca y no esta bloqueado, que siga intentado
                    BitacoraBL.RegistrarBitacoraSinUsuario(TipoEvento.Warning, "Se registro un intento de ingreso erroneo de usuario: " + usuario.USERNAME);
                    lblError.Text = "Usuario o contraseña incorrectos, intente nuevamente."; lblError.Visible = true;
                }
            }


            //end pato
      
        }
    }

    protected void btnRecalcularHashes_Click(object sender, EventArgs e)
    {
        if (hdnRecalcularHashes.Value == "true")
        {
            try
            {
                IntegridadBL integridadBL = new IntegridadBL();
                integridadBL.ActualizarDVH();
                integridadBL.ActualizarDVV();
                
                string script = @"
                    alert('Hashes recalculados exitosamente. Cerrando sesión...');
                    setTimeout(function() { window.location.href = 'LogIn.aspx'; }, 2000);
                ";
                ClientScript.RegisterStartupScript(this.GetType(), "recalculoExitoso", script, true);
                
                Session.Clear();
                hdnRecalcularHashes.Value = ""; // Limpiar el campo oculto
            }
            catch (Exception ex)
            {
                string script = $"alert('Error al recalcular hashes: {ex.Message}');";
                ClientScript.RegisterStartupScript(this.GetType(), "errorRecalculo", script, true);
            }
        }
    }
}