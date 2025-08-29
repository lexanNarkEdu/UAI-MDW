using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Clase3_28_08
{
    /// <summary>
    /// Summary description for WebServiceEjercicioBParte2
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceEjercicioBParte2 : System.Web.Services.WebService
    {

        [WebMethod]
        public double Potencia(double a, double b)
        {
            return Math.Pow(a, b);
        }

        [WebMethod]
        public double Raiz(double a, double b)
        {
            return Math.Pow(a, 1.0 / b);
        }
    }
}
