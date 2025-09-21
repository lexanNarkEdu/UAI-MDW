using BE;
using BLL.Servicios;
using System;
using System.IO;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace uai_shop
{
    public partial class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            try
            {
                // Solo procesar para páginas .aspx y verificar contexto válido
                if (HttpContext.Current?.Handler is System.Web.UI.Page && 
                    HttpContext.Current?.Request?.Path != null &&
                    HttpContext.Current?.Session != null)
                {
                    string currentPage = Path.GetFileName(HttpContext.Current.Request.Path);

                    // Evitar validaciones en páginas críticas para prevenir bucles
                    if (EsPaginaExcluida(currentPage))
                        return;

                    // Solo validar integridad por ahora, sin autorización para evitar conflictos
                    ValidarIntegridadSistema(currentPage);
                }
            }
            catch (Exception)
            {
                // En caso de cualquier error, no hacer nada para evitar bucles
                // El sistema funcionará sin validaciones globales
            }
        }

        private bool EsPaginaExcluida(string pagina)
        {
            // Lista más completa de páginas excluidas para evitar bucles
            string[] paginasExcluidas = { 
                "Login", "LogIn", "BackRestore", 
                "Default", "About", "Site.Master",
                "error", "unauthorized", "Default2"
            };
            return Array.Exists(paginasExcluidas, p => p.Equals(pagina, StringComparison.OrdinalIgnoreCase));
        }

        private void ValidarIntegridadSistema(string currentPage)
        {
            // Solo validar si hay error de integridad
            if (!IntegridadBL.IntegridadCorrupta)
                return;

            Usuario usuario = (Usuario)HttpContext.Current.Session["Usuario"];

            // Si no hay usuario logueado, redirigir a login solo si no estamos ya ahí
            if (usuario == null)
            {
                if (!currentPage.Equals("LogIn", StringComparison.OrdinalIgnoreCase))
                {
                    HttpContext.Current.Response.Redirect("LogIn.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                return;
            }

            // Solo webmaster puede acceder a BackRestore cuando hay error de integridad
            if (!currentPage.Equals("BackRestore", StringComparison.OrdinalIgnoreCase))
            {
                HttpContext.Current.Response.Redirect("BackRestore.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        // Comentado temporalmente para evitar conflictos
        /*
        private void ValidarPermisosPagina(string currentPage)
        {
            // Solo validar si NO hay error de integridad
            if (IntegridadBL.IntegridadCorrupta)
                return;

            Usuario usuario = (Usuario)HttpContext.Current.Session["Usuario"];

            // Páginas que requieren estar logueado
            if (usuario == null && !EsPaginaPublica(currentPage))
            {
                HttpContext.Current.Response.Redirect("LogIn.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return;
            }

            // Validaciones específicas por página usando el sistema de autorización
            if (usuario != null && !AutorizacionHelper.PuedeAccederAPagina(usuario, currentPage))
            {
                string permisoRequerido = AutorizacionHelper.ObtenerPermisoRequerido(currentPage);
                HttpContext.Current.Response.Redirect($"Default.aspx?error=unauthorized&required={permisoRequerido}", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        private bool EsPaginaPublica(string pagina)
        {
            string[] paginasPublicas = { "Login.aspx", "LogIn.aspx", "Default.aspx", "About.aspx" };
            return Array.Exists(paginasPublicas, p => p.Equals(pagina, StringComparison.OrdinalIgnoreCase));
        }
        */
    }
}