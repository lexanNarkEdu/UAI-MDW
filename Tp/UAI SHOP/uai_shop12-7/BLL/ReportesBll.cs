using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Clase de lógica de negocio para reportes de ganancias
    /// </summary>
    public class ReportesBll
    {
        private ReportesDAL dal = new ReportesDAL();

        /// <summary>
        /// Obtiene el reporte de ganancias general por categorías
        /// </summary>
        /// <returns>Lista ordenada por ganancia total descendente</returns>
        public List<ReporteGanancias> ObtenerGananciasGeneral()
        {
            try
            {
                var reportes = dal.ObtenerGananciasGeneral();
                
                // Aplicar lógica de negocio si es necesaria
                // Por ejemplo, filtrar categorías con ganancia negativa
                var reportesFiltrados = reportes.Where(r => r.GananciaTotal >= 0).ToList();
                
                return reportesFiltrados.OrderByDescending(r => r.GananciaTotal).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio - Ganancias General: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de ganancias del último mes por categorías
        /// </summary>
        /// <returns>Lista ordenada por ganancia total descendente</returns>
        public List<ReporteGanancias> ObtenerGananciasUltimoMes()
        {
            try
            {
                var reportes = dal.ObtenerGananciasUltimoMes();
                
                // Aplicar lógica de negocio si es necesaria
                var reportesFiltrados = reportes.Where(r => r.GananciaTotal >= 0).ToList();
                
                return reportesFiltrados.OrderByDescending(r => r.GananciaTotal).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio - Ganancias Último Mes: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de ganancias semanal por categorías
        /// </summary>
        /// <returns>Lista ordenada por ganancia total descendente</returns>
        public List<ReporteGanancias> ObtenerGananciasSemanal()
        {
            try
            {
                var reportes = dal.ObtenerGananciasSemanal();
                
                // Aplicar lógica de negocio si es necesaria
                var reportesFiltrados = reportes.Where(r => r.GananciaTotal >= 0).ToList();
                
                return reportesFiltrados.OrderByDescending(r => r.GananciaTotal).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio - Ganancias Semanal: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene estadísticas generales de los reportes
        /// </summary>
        /// <param name="reportes">Lista de reportes</param>
        /// <returns>Información estadística</returns>
        public string ObtenerEstadisticas(List<ReporteGanancias> reportes)
        {
            if (reportes == null || !reportes.Any())
                return "No hay datos disponibles";

            var totalGanancias = reportes.Sum(r => r.GananciaTotal);
            var totalVentas = reportes.Sum(r => r.VentasConEstaCategoria);
            var totalUnidades = reportes.Sum(r => r.UnidadesTotales);
            var categorias = reportes.Count;

            return "Total Categorías: " + categorias + " | " +
                   "Ganancias Totales: $" + totalGanancias.ToString("N2") + " | " +
                   "Ventas Totales: " + totalVentas + " | " +
                   "Unidades Vendidas: " + totalUnidades;
        }
    }
}