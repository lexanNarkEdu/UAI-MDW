using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Clase5_11_09
{
    /// <summary>
    /// Summary description for WebServiceCalculoContraPrestacion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceCalculoContraPrestacion : System.Web.Services.WebService
    {

        [WebMethod]
        public string CuantoCobra(string apellido, string[] dias, decimal valorMateria)
        {
            var total = dias.Length * valorMateria;
            return $"Sr {apellido}, por su contraprestación ud cobra {total:C}";
        }
    }
}
