using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Servicios
{
    /// <summary>
    /// En esta clase se almacenan las configuraciones generales por default
    /// </summary>
    public static class ConfiguracionesGenerales
    {

        /// <summary>
        /// Representa la lista de tabla claves del sistema
        /// </summary>
        public static readonly List<string> tablasClaves = new List<string>() { "Usuario", "Producto" };
        // en este caso solo hay una que es la requerida por el profesor, pero en caso de querer agregar mas se puede.

        public static readonly Dictionary<string, string> identifierByTable = new Dictionary<string, string>()
        {
            { "Usuario", "Id_Usuario" },
            { "Producto", "Id" }
        };
    }
}
