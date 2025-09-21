using System;
using System.Collections.Generic;
using System.Linq;
using BE;


namespace uai_shop 
{
    /// <summary>
    /// Helper para manejo de autorización usando el sistema de Perfiles existente
    /// </summary>
    public static class AutorizacionHelper
    {
        // Mapear páginas a permisos específicos (usando los permisos ya definidos en UsuarioBll)
        private static readonly Dictionary<string, string> MapaPaginasAPermisos = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"Bitacora.aspx", "Bitácora"},
        {"DesbloqueoUser.aspx", "Bitácora"}, // Solo webmaster tiene este permiso
        {"BackRestore.aspx", "Bitácora"}, // Solo webmaster tiene este permiso
        {"Categorias.aspx", "Agregar productos"},
        {"CarritoCompra.aspx", "Agregar al carrito"},
        {"Facturas.aspx", "Ver catálogo"} // Cualquier usuario logueado
    };

        /// <summary>
        /// Obtiene el permiso requerido para acceder a una página específica
        /// </summary>
        /// <param name="nombrePagina">Nombre de la página (ej: Bitacora.aspx)</param>
        /// <returns>Nombre del permiso requerido o null si no requiere permiso específico</returns>
        public static string ObtenerPermisoRequerido(string nombrePagina)
        {
            return MapaPaginasAPermisos.TryGetValue(nombrePagina, out string permiso) ? permiso : null;
        }

        /// <summary>
        /// Verifica si un usuario tiene un permiso específico
        /// </summary>
        /// <param name="usuario">Usuario a verificar</param>
        /// <param name="permisoRequerido">Permiso que debe tener</param>
        /// <returns>True si tiene el permiso, False en caso contrario</returns>
        public static bool UsuarioTienePermiso(Usuario usuario, string permisoRequerido)
        {
            if (usuario?.Permiso == null || string.IsNullOrEmpty(permisoRequerido))
                return false;

            // Usar el sistema de perfiles existente (patrón Composite)
            List<Perfil> permisosUsuario = usuario.Permiso.GetPermisos();

            // Verificar si el usuario tiene el permiso específico
            return permisosUsuario.Any(p => p.Nombre.Equals(permisoRequerido, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Verifica si un usuario puede acceder a una página específica
        /// </summary>
        /// <param name="usuario">Usuario a verificar</param>
        /// <param name="nombrePagina">Nombre de la página</param>
        /// <returns>True si puede acceder, False en caso contrario</returns>
        public static bool PuedeAccederAPagina(Usuario usuario, string nombrePagina)
        {
            string permisoRequerido = ObtenerPermisoRequerido(nombrePagina);

            // Si la página no requiere permiso específico, permitir acceso
            if (string.IsNullOrEmpty(permisoRequerido))
                return true;

            return UsuarioTienePermiso(usuario, permisoRequerido);
        }
    }
}