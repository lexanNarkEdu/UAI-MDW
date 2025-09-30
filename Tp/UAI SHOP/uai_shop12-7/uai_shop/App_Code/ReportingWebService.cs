using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for ReportingWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ReportingWebService : System.Web.Services.WebService
{

    private ReportesBll reportesBll = new ReportesBll();

    /// <summary>
    /// Obtiene el reporte de ganancias general por todas las categorías
    /// </summary>
    /// <returns>Array de objetos ReporteGanancias con datos generales</returns>
    [WebMethod(Description = "Obtiene el reporte de ganancias general por todas las categorías")]
    public ReporteGanancias[] ObtenerGananciasGeneral()
    {
        try
        {
            List<ReporteGanancias> reportes = reportesBll.ObtenerGananciasGeneral();
            return reportes.ToArray();
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al obtener ganancias generales: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    /// <summary>
    /// Obtiene el reporte de ganancias del último mes por categorías
    /// </summary>
    /// <returns>Array de objetos ReporteGanancias con datos del último mes</returns>
    [WebMethod(Description = "Obtiene el reporte de ganancias del último mes (30 días) por categorías")]
    public ReporteGanancias[] ObtenerGananciasUltimoMes()
    {
        try
        {
            List<ReporteGanancias> reportes = reportesBll.ObtenerGananciasUltimoMes();
            return reportes.ToArray();
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al obtener ganancias del último mes: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    /// <summary>
    /// Obtiene el reporte de ganancias semanal por categorías
    /// </summary>
    /// <returns>Array de objetos ReporteGanancias con datos semanales</returns>
    [WebMethod(Description = "Obtiene el reporte de ganancias semanal (7 días) por categorías")]
    public ReporteGanancias[] ObtenerGananciasSemanal()
    {
        try
        {
            List<ReporteGanancias> reportes = reportesBll.ObtenerGananciasSemanal();
            return reportes.ToArray();
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al obtener ganancias semanales: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    /// <summary>
    /// Obtiene estadísticas generales de ganancias
    /// </summary>
    /// <param name="tipoReporte">Tipo de reporte: "general", "mensual" o "semanal"</param>
    /// <returns>String con estadísticas resumidas</returns>
    [WebMethod(Description = "Obtiene estadísticas resumidas de ganancias por tipo de reporte")]
    public string ObtenerEstadisticasGanancias(string tipoReporte)
    {
        try
        {
            List<ReporteGanancias> reportes;

            switch (tipoReporte.ToLower())
            {
                case "general":
                    reportes = reportesBll.ObtenerGananciasGeneral();
                    break;
                case "mensual":
                    reportes = reportesBll.ObtenerGananciasUltimoMes();
                    break;
                case "semanal":
                    reportes = reportesBll.ObtenerGananciasSemanal();
                    break;
                default:
                    throw new ArgumentException("Tipo de reporte no válido. Use: 'general', 'mensual' o 'semanal'");
            }

            return reportesBll.ObtenerEstadisticas(reportes);
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al obtener estadísticas: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    /// <summary>
    /// Obtiene la categoría con mayor ganancia según el tipo de reporte
    /// </summary>
    /// <param name="tipoReporte">Tipo de reporte: "general", "mensual" o "semanal"</param>
    /// <returns>Objeto ReporteGanancias de la categoría líder</returns>
    [WebMethod(Description = "Obtiene la categoría con mayor ganancia según el tipo de reporte")]
    public ReporteGanancias ObtenerCategoriaLider(string tipoReporte)
    {
        try
        {
            List<ReporteGanancias> reportes;

            switch (tipoReporte.ToLower())
            {
                case "general":
                    reportes = reportesBll.ObtenerGananciasGeneral();
                    break;
                case "mensual":
                    reportes = reportesBll.ObtenerGananciasUltimoMes();
                    break;
                case "semanal":
                    reportes = reportesBll.ObtenerGananciasSemanal();
                    break;
                default:
                    throw new ArgumentException("Tipo de reporte no válido. Use: 'general', 'mensual' o 'semanal'");
            }

            if (reportes.Any())
            {
                return reportes.OrderByDescending(r => r.GananciaTotal).First();
            }
            else
            {
                return new ReporteGanancias("Sin datos", 0, 0, 0, 0);
            }
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al obtener categoría líder: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    /// <summary>
    /// Método de prueba para verificar el funcionamiento del WebService
    /// </summary>
    /// <returns>Mensaje de confirmación</returns>
    [WebMethod(Description = "Método de prueba para verificar el funcionamiento del WebService")]
    public string Ping()
    {
        return "WebService UAI SHOP - Reportes de Ganancias está funcionando correctamente. Fecha: " + DateTime.Now.ToString();
    }
}
