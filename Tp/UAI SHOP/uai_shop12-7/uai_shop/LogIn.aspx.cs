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
        Usuario usuariobd = usuarioBll.Buscar(usuario);
       
        if (usuariobd != null)
        {//new
            List<string> erroresIntegridad = new List<string>();
            try
            {
                erroresIntegridad=integridadBL.VerificarIntegridad();
                if(erroresIntegridad.Count > 0)//hay errores
                {
                    if (usuariobd.Permiso.Nombre != "Webmaster")
                    {
                        string script = $"<script>alert('Error de integridad. Contacte al WebMaster');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alerta", script);
                        return;
                    }
                    string mensajeUnico = HttpUtility.JavaScriptStringEncode(string.Join("\n", erroresIntegridad));
                    Session["mensajeUnico"] = mensajeUnico;
                    BitacoraBL.RegistrarBitacora(TipoEvento.Message, usuariobd, "Se logeo el " + usuariobd.Permiso.Nombre + ": " + usuariobd.USERNAME);
                    Session["Usuario"] = usuariobd;
                    Response.Redirect("BackRestore.aspx");
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
}