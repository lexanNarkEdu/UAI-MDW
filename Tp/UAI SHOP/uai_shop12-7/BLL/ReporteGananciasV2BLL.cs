using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class ReporteGananciasV2BLL
    {
        private ReporteGananciasV2DAL reporteDAL;

        public ReporteGananciasV2BLL()
        {
            reporteDAL = new ReporteGananciasV2DAL();
        }

        /// <summary>
        /// Obtiene el reporte de ganancias V2 con validaciones de negocio
        /// </summary>
        /// <param name="filtros">Filtros del reporte</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReporteDinamico(FiltrosReporteV2 filtros)
        {
            try
            {
                // Validaciones de negocio
                ValidarFiltros(filtros);
                
                // Obtener datos del DAL
                List<ReporteGananciasV2> reporte = reporteDAL.ObtenerReporteDinamico(filtros);
                
                // Aplicar lógica de negocio adicional si es necesaria
                return ProcesarReporte(reporte);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BLL al obtener reporte dinámico: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Obtiene reporte por período con accesos rápidos
        /// </summary>
        /// <param name="tipoRango">ultima_semana, ultimo_mes, ultimos_3_meses, ultimo_año, todo</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReportePorPeriodo(string tipoRango)
        {
            try
            {
                if (string.IsNullOrEmpty(tipoRango))
                    throw new ArgumentException("Debe especificar un tipo de rango válido");

                return reporteDAL.ObtenerReportePorPeriodo(tipoRango.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reporte por período: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Obtiene reporte filtrado por categoría
        /// </summary>
        /// <param name="idCategoria">ID de la categoría</param>
        /// <param name="fechaDesde">Fecha desde (opcional)</param>
        /// <param name="fechaHasta">Fecha hasta (opcional)</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReportePorCategoria(int idCategoria, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            try
            {
                if (idCategoria <= 0)
                    throw new ArgumentException("El ID de categoría debe ser mayor a 0");

                ValidarRangoFechas(fechaDesde, fechaHasta);
                
                return reporteDAL.ObtenerReportePorCategoria(idCategoria, fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reporte por categoría: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Obtiene reporte por rango de precios
        /// </summary>
        /// <param name="precioMin">Precio mínimo</param>
        /// <param name="precioMax">Precio máximo</param>
        /// <param name="fechaDesde">Fecha desde (opcional)</param>
        /// <param name="fechaHasta">Fecha hasta (opcional)</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReportePorRangoPrecios(decimal precioMin, decimal precioMax, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            try
            {
                ValidarRangoPrecios(precioMin, precioMax);
                ValidarRangoFechas(fechaDesde, fechaHasta);
                
                return reporteDAL.ObtenerReportePorRangoPrecios(precioMin, precioMax, fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reporte por rango de precios: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Obtiene reporte por rango de costos
        /// </summary>
        /// <param name="costoMin">Costo mínimo</param>
        /// <param name="costoMax">Costo máximo</param>
        /// <param name="fechaDesde">Fecha desde (opcional)</param>
        /// <param name="fechaHasta">Fecha hasta (opcional)</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReportePorRangoCostos(decimal costoMin, decimal costoMax, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            try
            {
                ValidarRangoCostos(costoMin, costoMax);
                ValidarRangoFechas(fechaDesde, fechaHasta);
                
                return reporteDAL.ObtenerReportePorRangoCostos(costoMin, costoMax, fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reporte por rango de costos: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Obtiene estadísticas del reporte
        /// </summary>
        /// <param name="filtros">Filtros aplicados</param>
        /// <returns>Estadísticas del reporte</returns>
        public EstadisticasReporteV2 ObtenerEstadisticas(FiltrosReporteV2 filtros)
        {
            try
            {
                ValidarFiltros(filtros);
                return reporteDAL.ObtenerEstadisticas(filtros);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estadísticas: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Genera un resumen ejecutivo del reporte
        /// </summary>
        /// <param name="filtros">Filtros aplicados</param>
        /// <returns>String con resumen ejecutivo</returns>
        public string GenerarResumenEjecutivo(FiltrosReporteV2 filtros)
        {
            try
            {
                EstadisticasReporteV2 stats = ObtenerEstadisticas(filtros);
                List<ReporteGananciasV2> datos = ObtenerReporteDinamico(filtros);

                StringBuilder resumen = new StringBuilder();
                resumen.AppendLine("=== RESUMEN EJECUTIVO - REPORTE DE GANANCIAS V2 ===");
                resumen.AppendLine();
                
                resumen.AppendLine("📊 FILTROS APLICADOS: " + filtros);
                resumen.AppendLine();
                
                resumen.AppendLine("💰 RESUMEN GENERAL:");
                resumen.AppendLine("   • Categorías analizadas: " + stats.TotalCategorias);
                resumen.AppendLine("   • Ventas totales: " + stats.TotalVentas.ToString("N0"));
                resumen.AppendLine("   • Unidades vendidas: " + stats.TotalUnidades.ToString("N0"));
                resumen.AppendLine("   • Facturación total: $" + stats.TotalFacturacion.ToString("N2"));
                resumen.AppendLine("   • Costos totales: $" + stats.TotalCostos.ToString("N2"));
                resumen.AppendLine("   • Ganancias totales: $" + stats.TotalGanancias.ToString("N2"));
                resumen.AppendLine("   • Margen promedio: " + stats.PorcentajeGananciaPromedio.ToString("N2") + "%");
                resumen.AppendLine();

                resumen.AppendLine("🏆 TOP 3 CATEGORÍAS:");
                var top3 = datos.Take(3);
                int posicion = 1;
                foreach (var categoria in top3)
                {
                    resumen.AppendLine("   " + posicion + ". " + categoria.Categoria + ": $" + categoria.GananciaTotal.ToString("N2") + " " +
                                     "(" + CalcularPorcentajeGanancia(categoria).ToString("N1") + "% margen)");
                    posicion++;
                }

                return resumen.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar resumen ejecutivo: " + ex.Message, ex);
            }
        }

        #region Validaciones

        private void ValidarFiltros(FiltrosReporteV2 filtros)
        {
            if (filtros == null)
                return; // Filtros nulos = sin filtros, es válido

            ValidarRangoFechas(filtros.FechaDesde, filtros.FechaHasta);
                
            if (filtros.CostoMinimo.HasValue && filtros.CostoMaximo.HasValue)
                ValidarRangoCostos(filtros.CostoMinimo.Value, filtros.CostoMaximo.Value);
                
            if (filtros.VentasMinimas.HasValue && filtros.VentasMaximas.HasValue)
                ValidarRangoVentas(filtros.VentasMinimas.Value, filtros.VentasMaximas.Value);
                
            if (filtros.IDCategoria.HasValue && filtros.IDCategoria.Value <= 0)
                throw new ArgumentException("El ID de categoría debe ser mayor a 0");
        }

        private void ValidarRangoFechas(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            if (fechaDesde.HasValue && fechaHasta.HasValue)
            {
                if (fechaDesde.Value > fechaHasta.Value)
                    throw new ArgumentException("La fecha desde no puede ser mayor que la fecha hasta");
                    
                if (fechaHasta.Value > DateTime.Now)
                    throw new ArgumentException("La fecha hasta no puede ser futura");
            }
        }

        private void ValidarRangoPrecios(decimal precioMin, decimal precioMax)
        {
            if (precioMin < 0)
                throw new ArgumentException("El precio mínimo no puede ser negativo");
                
            if (precioMax < 0)
                throw new ArgumentException("El precio máximo no puede ser negativo");
                
            if (precioMin > precioMax)
                throw new ArgumentException("El precio mínimo no puede ser mayor que el máximo");
        }

        private void ValidarRangoCostos(decimal costoMin, decimal costoMax)
        {
            if (costoMin < 0)
                throw new ArgumentException("El costo mínimo no puede ser negativo");
                
            if (costoMax < 0)
                throw new ArgumentException("El costo máximo no puede ser negativo");
                
            if (costoMin > costoMax)
                throw new ArgumentException("El costo mínimo no puede ser mayor que el máximo");
        }

        private void ValidarRangoVentas(decimal ventasMin, decimal ventasMax)
        {
            if (ventasMin < 0)
                throw new ArgumentException("El monto mínimo de ventas no puede ser negativo");
                
            if (ventasMax < 0)
                throw new ArgumentException("El monto máximo de ventas no puede ser negativo");
                
            if (ventasMin > ventasMax)
                throw new ArgumentException("El monto mínimo de ventas no puede ser mayor que el máximo");
        }

        #endregion

        #region Procesamiento

        private List<ReporteGananciasV2> ProcesarReporte(List<ReporteGananciasV2> reporte)
        {
            // Aquí se pueden aplicar reglas de negocio adicionales
            // Por ejemplo: filtrar categorías con muy pocas ventas, 
            // aplicar descuentos especiales, etc.
            
            // Por ahora, solo ordenamos por ganancia descendente
            return reporte.OrderByDescending(x => x.GananciaTotal).ToList();
        }

        #endregion

        #region Métodos de Cálculo

        /// <summary>
        /// Calcula el porcentaje de ganancia de un reporte
        /// </summary>
        public static decimal CalcularPorcentajeGanancia(BE.ReporteGananciasV2 reporte)
        {
            return reporte.CostoTotal > 0 ? (reporte.GananciaTotal / reporte.CostoTotal) * 100 : 0;
        }

        /// <summary>
        /// Calcula el precio promedio de un reporte
        /// </summary>
        public static decimal CalcularPrecioPromedio(BE.ReporteGananciasV2 reporte)
        {
            return reporte.UnidadesVendidas > 0 ? reporte.VentaTotal / reporte.UnidadesVendidas : 0;
        }

        /// <summary>
        /// Calcula el costo promedio de un reporte
        /// </summary>
        public static decimal CalcularCostoPromedio(BE.ReporteGananciasV2 reporte)
        {
            return reporte.UnidadesVendidas > 0 ? reporte.CostoTotal / reporte.UnidadesVendidas : 0;
        }

        /// <summary>
        /// Calcula el margen unitario de un reporte
        /// </summary>
        public static decimal CalcularMargenUnitario(BE.ReporteGananciasV2 reporte)
        {
            return reporte.UnidadesVendidas > 0 ? reporte.GananciaTotal / reporte.UnidadesVendidas : 0;
        }

        #endregion
    }
}