using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Clase3_28_08
{
    /// <summary>
    /// Summary description for WebServiceEjercicioB
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceEjercicioB : System.Web.Services.WebService
    {

        [WebMethod]
        public double Sumar(double a, double b)
        {
            return a + b;
        }

        [WebMethod]
        public double Restar(double a, double b)
        {
            return a - b;
        }

        [WebMethod]
        public double Multiplicar(double a, double b)
        {
            return a * b;
        }

        [WebMethod]
        public double Dividir(double a, double b)
        {
            return a / b;
        }
    }
}
