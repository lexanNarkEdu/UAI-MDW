using BE;
using BLL;
using DAL;
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
    private ReporteGananciasV2BLL reporteV2BLL = new ReporteGananciasV2BLL();

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

    #region ReporteGananciasV2 - Métodos Dinámicos

    /// <summary>
    /// Obtiene reporte de ganancias V2 con filtros dinámicos
    /// </summary>
    /// <param name="fechaDesde">Fecha desde (formato: yyyy-mm-dd, opcional)</param>
    /// <param name="fechaHasta">Fecha hasta (formato: yyyy-mm-dd, opcional)</param>
    /// <param name="idCategoria">ID de categoría (opcional)</param>
    /// <param name="precioMinimo">Precio mínimo (opcional)</param>
    /// <param name="precioMaximo">Precio máximo (opcional)</param>
    /// <param name="costoMinimo">Costo mínimo (opcional)</param>
    /// <param name="costoMaximo">Costo máximo (opcional)</param>
    /// <returns>Array de ReporteGananciasV2 con filtros aplicados</returns>
    [WebMethod(Description = "Obtiene reporte de ganancias V2 con filtros dinámicos avanzados")]
    public ReporteGananciasV2[] ObtenerReporteGananciasV2(
        string fechaDesde = null,
        string fechaHasta = null,
        int idCategoria = 0,
        decimal precioMinimo = 0,
        decimal precioMaximo = 0,
        decimal costoMinimo = 0,
        decimal costoMaximo = 0)
    {
        try
        {
            FiltrosReporteV2 filtros = new FiltrosReporteV2();

            // Parsear fechas si se proporcionan
            if (!string.IsNullOrEmpty(fechaDesde))
            {
                if (DateTime.TryParse(fechaDesde, out DateTime fechaD))
                    filtros.FechaDesde = fechaD;
                else
                    throw new ArgumentException("Formato de fechaDesde inválido. Use: yyyy-mm-dd");
            }

            if (!string.IsNullOrEmpty(fechaHasta))
            {
                if (DateTime.TryParse(fechaHasta, out DateTime fechaH))
                    filtros.FechaHasta = fechaH;
                else
                    throw new ArgumentException("Formato de fechaHasta inválido. Use: yyyy-mm-dd");
            }

            // Aplicar filtros solo si tienen valores válidos
            if (idCategoria > 0) filtros.IDCategoria = idCategoria;
            if (precioMinimo > 0) filtros.PrecioMinimo = precioMinimo;
            if (precioMaximo > 0) filtros.PrecioMaximo = precioMaximo;
            if (costoMinimo > 0) filtros.CostoMinimo = costoMinimo;
            if (costoMaximo > 0) filtros.CostoMaximo = costoMaximo;

            List<ReporteGananciasV2> reportes = reporteV2BLL.ObtenerReporteDinamico(filtros);
            return reportes.ToArray();
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al obtener reporte dinámico V2: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    /// <summary>
    /// Obtiene reporte V2 por período predefinido
    /// </summary>
    /// <param name="tipoRango">Tipo de rango: "ultima_semana", "ultimo_mes", "ultimos_3_meses", "ultimo_año", "todo"</param>
    /// <returns>Array de ReporteGananciasV2</returns>
    [WebMethod(Description = "Obtiene reporte V2 por período predefinido (acceso rápido)")]
    public ReporteGananciasV2[] ObtenerReporteV2PorPeriodo(string tipoRango)
    {
        try
        {
            List<ReporteGananciasV2> reportes = reporteV2BLL.ObtenerReportePorPeriodo(tipoRango);
            return reportes.ToArray();
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al obtener reporte V2 por período: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    /// <summary>
    /// Obtiene reporte V2 filtrado por categoría específica
    /// </summary>
    /// <param name="idCategoria">ID de la categoría</param>
    /// <param name="fechaDesde">Fecha desde (opcional)</param>
    /// <param name="fechaHasta">Fecha hasta (opcional)</param>
    /// <returns>Array de ReporteGananciasV2</returns>
    [WebMethod(Description = "Obtiene reporte V2 filtrado por categoría específica")]
    public ReporteGananciasV2[] ObtenerReporteV2PorCategoria(int idCategoria, string fechaDesde = null, string fechaHasta = null)
    {
        try
        {
            DateTime? fechaD = null, fechaH = null;

            if (!string.IsNullOrEmpty(fechaDesde) && DateTime.TryParse(fechaDesde, out DateTime fDesde))
                fechaD = fDesde;

            if (!string.IsNullOrEmpty(fechaHasta) && DateTime.TryParse(fechaHasta, out DateTime fHasta))
                fechaH = fHasta;

            List<ReporteGananciasV2> reportes = reporteV2BLL.ObtenerReportePorCategoria(idCategoria, fechaD, fechaH);
            return reportes.ToArray();
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al obtener reporte V2 por categoría: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    /// <summary>
    /// Obtiene estadísticas del reporte V2
    /// </summary>
    /// <param name="tipoRango">Tipo de rango para estadísticas</param>
    /// <returns>Estadísticas en formato string</returns>
    [WebMethod(Description = "Obtiene estadísticas del reporte V2")]
    public string ObtenerEstadisticasV2()
    {
        try
        {
            FiltrosReporteV2 filtros = new FiltrosReporteV2();
            EstadisticasReporteV2 stats = reporteV2BLL.ObtenerEstadisticas(filtros);
            return stats.ToString();
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al obtener estadísticas V2: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    /// <summary>
    /// Genera resumen ejecutivo completo del reporte V2
    /// </summary>
    /// <param name="tipoRango">Período para el resumen</param>
    /// <returns>Resumen ejecutivo detallado</returns>
    [WebMethod(Description = "Genera resumen ejecutivo completo del reporte V2")]
    public string GenerarResumenEjecutivoV2()
    {
        try
        {
            FiltrosReporteV2 filtros = new FiltrosReporteV2();
            return reporteV2BLL.GenerarResumenEjecutivo(filtros);
        }
        catch (Exception ex)
        {
            throw new SoapException(
                "Error al generar resumen ejecutivo V2: " + ex.Message,
                SoapException.ServerFaultCode,
                ex);
        }
    }

    #endregion

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
