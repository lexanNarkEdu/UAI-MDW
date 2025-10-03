using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using BLL;
using BE;

/// <summary>
/// Summary description for ReportingWS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ReportingWS : System.Web.Services.WebService
{

    public ReportingWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    private BLL.ReporteGananciasV2BLL reporteV2BLL = new BLL.ReporteGananciasV2BLL();

    #region Métodos de Reporte de Ganancias V2

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
    public BE.ReporteGananciasV2[] ObtenerReporteGananciasV2(
        string fechaDesde = null,
        string fechaHasta = null,
        int idCategoria = 0,
        decimal costoMinimo = 0,
        decimal costoMaximo = 0,
        decimal ventasMinimas = 0,
        decimal ventasMaximas = 0)
    {
        try
        {
            BE.FiltrosReporteV2 filtros = new BE.FiltrosReporteV2();

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
            if (costoMinimo > 0) filtros.CostoMinimo = costoMinimo;
            if (costoMaximo > 0) filtros.CostoMaximo = costoMaximo;
            if (ventasMinimas > 0) filtros.VentasMinimas = ventasMinimas;
            if (ventasMaximas > 0) filtros.VentasMaximas = ventasMaximas;

            List<BE.ReporteGananciasV2> reportes = reporteV2BLL.ObtenerReporteDinamico(filtros);
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
    public BE.ReporteGananciasV2[] ObtenerReporteV2PorPeriodo(string tipoRango)
    {
        try
        {
            List<BE.ReporteGananciasV2> reportes = reporteV2BLL.ObtenerReportePorPeriodo(tipoRango);
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
    public BE.ReporteGananciasV2[] ObtenerReporteV2PorCategoria(int idCategoria, string fechaDesde = null, string fechaHasta = null)
    {
        try
        {
            DateTime? fechaD = null, fechaH = null;

            if (!string.IsNullOrEmpty(fechaDesde) && DateTime.TryParse(fechaDesde, out DateTime fDesde))
                fechaD = fDesde;

            if (!string.IsNullOrEmpty(fechaHasta) && DateTime.TryParse(fechaHasta, out DateTime fHasta))
                fechaH = fHasta;

            List<BE.ReporteGananciasV2> reportes = reporteV2BLL.ObtenerReportePorCategoria(idCategoria, fechaD, fechaH);
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
    public string ObtenerEstadisticasV2(string tipoRango = "todo")
    {
        try
        {
            BE.FiltrosReporteV2 filtros = new BE.FiltrosReporteV2();
            ConfigurarFiltrosPorTipoRango(filtros, tipoRango);
            BE.EstadisticasReporteV2 stats = reporteV2BLL.ObtenerEstadisticas(filtros);
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
    public string GenerarResumenEjecutivoV2(string tipoRango = "ultimo_mes")
    {
        try
        {
            BE.FiltrosReporteV2 filtros = new BE.FiltrosReporteV2();
            ConfigurarFiltrosPorTipoRango(filtros, tipoRango);
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

    /// <summary>
    /// Configura los filtros de reporte según el tipo de rango especificado
    /// </summary>
    private void ConfigurarFiltrosPorTipoRango(BE.FiltrosReporteV2 filtros, string tipoRango)
    {
        DateTime hoy = DateTime.Now;

        switch (tipoRango.ToLower())
        {
            case "ultima_semana":
                filtros.FechaDesde = hoy.AddDays(-7);
                filtros.FechaHasta = hoy;
                break;
            case "ultimo_mes":
                filtros.FechaDesde = hoy.AddMonths(-1);
                filtros.FechaHasta = hoy;
                break;
            case "ultimo_año":
            case "ultimo_anio":
                filtros.FechaDesde = hoy.AddYears(-1);
                filtros.FechaHasta = hoy;
                break;
            case "ultimos_3_meses":
                filtros.FechaDesde = hoy.AddMonths(-3);
                filtros.FechaHasta = hoy;
                break;
            default:
                // Sin filtros de fecha - obtener todo
                break;
        }
    }

}
