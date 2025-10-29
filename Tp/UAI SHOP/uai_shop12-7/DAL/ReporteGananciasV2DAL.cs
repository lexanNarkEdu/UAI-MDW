using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class ReporteGananciasV2DAL
    {
        private Acceso acceso;

        public ReporteGananciasV2DAL()
        {
            acceso = new Acceso();
        }

        /// <summary>
        /// Obtiene el reporte de ganancias V2 con filtros dinámicos
        /// </summary>
        /// <param name="filtros">Objeto con todos los filtros opcionales</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReporteDinamico(FiltrosReporteV2 filtros)
        {
            List<ReporteGananciasV2> listaReporte = new List<ReporteGananciasV2>();
            
            try
            {
                // Preparar parámetros del SP
                List<SqlParameter> parametros = new List<SqlParameter>();
                
                if (filtros.FechaDesde.HasValue)
                {
                    parametros.Add(new SqlParameter("@FechaDesde", SqlDbType.Date) { Value = filtros.FechaDesde.Value.Date });
                }
                
                if (filtros.FechaHasta.HasValue)
                {
                    parametros.Add(new SqlParameter("@FechaHasta", SqlDbType.Date) { Value = filtros.FechaHasta.Value.Date });
                }
                
                if (filtros.IDCategoria.HasValue)
                {
                    parametros.Add(new SqlParameter("@IDCategoria", SqlDbType.Int) { Value = filtros.IDCategoria.Value });
                }
                
                if (filtros.CostoMinimo.HasValue)
                {
                    parametros.Add(new SqlParameter("@CostoMinimo", SqlDbType.Decimal) { Value = filtros.CostoMinimo.Value });
                }
                
                if (filtros.CostoMaximo.HasValue)
                {
                    parametros.Add(new SqlParameter("@CostoMaximo", SqlDbType.Decimal) { Value = filtros.CostoMaximo.Value });
                }
                
                if (filtros.VentasMinimas.HasValue)
                {
                    parametros.Add(new SqlParameter("@VentasMinimas", SqlDbType.Decimal) { Value = filtros.VentasMinimas.Value });
                }
                
                if (filtros.VentasMaximas.HasValue)
                {
                    parametros.Add(new SqlParameter("@VentasMaximas", SqlDbType.Decimal) { Value = filtros.VentasMaximas.Value });
                }

                // Ejecutar SP
                DataTable dt = acceso.leer("SP_REPORTE_GANANCIAS_V2", parametros);
                
                // Mapear resultados
                foreach (DataRow row in dt.Rows)
                {
                    ReporteGananciasV2 reporte = new ReporteGananciasV2
                    {
                        Categoria = row["Categoria"].ToString(),
                        CantidadVentas = Convert.ToInt32(row["CantidadVentas"]),
                        UnidadesVendidas = Convert.ToInt32(row["UnidadesVendidas"]),
                        VentaTotal = Convert.ToDecimal(row["VentaTotal"]),
                        CostoTotal = Convert.ToDecimal(row["CostoTotal"]),
                        GananciaTotal = Convert.ToDecimal(row["GananciaTotal"]),
                        //PorcentajeGanancia = Convert.ToDecimal(row["PorcentajeGanancia"])

                    };
                    
                    listaReporte.Add(reporte);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reporte dinámico V2: " + ex.Message, ex);
            }
            
            return listaReporte;
        }

        /// <summary>
        /// Obtiene reporte con filtro de período predefinido
        /// </summary>
        /// <param name="tipoRango">ultima_semana, ultimo_mes, ultimo_año, etc.</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReportePorPeriodo(string tipoRango)
        {
            FiltrosReporteV2 filtros = new FiltrosReporteV2();
            ConfigurarFiltrosPorTipoRango(filtros, tipoRango);
            return ObtenerReporteDinamico(filtros);
        }

        /// <summary>
        /// Obtiene reporte filtrado por categoría específica
        /// </summary>
        /// <param name="idCategoria">ID de la categoría</param>
        /// <param name="fechaDesde">Fecha desde (opcional)</param>
        /// <param name="fechaHasta">Fecha hasta (opcional)</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReportePorCategoria(int idCategoria, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            FiltrosReporteV2 filtros = new FiltrosReporteV2
            {
                IDCategoria = idCategoria,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta
            };
            
            return ObtenerReporteDinamico(filtros);
        }

        /// <summary>
        /// Obtiene reporte filtrado por rango de precios
        /// </summary>
        /// <param name="precioMin">Precio mínimo</param>
        /// <param name="precioMax">Precio máximo</param>
        /// <param name="fechaDesde">Fecha desde (opcional)</param>
        /// <param name="fechaHasta">Fecha hasta (opcional)</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReportePorRangoPrecios(decimal precioMin, decimal precioMax, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            FiltrosReporteV2 filtros = new FiltrosReporteV2
            {
                PrecioMinimo = precioMin,
                PrecioMaximo = precioMax,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta
            };
            
            return ObtenerReporteDinamico(filtros);
        }

        /// <summary>
        /// Obtiene reporte filtrado por rango de costos
        /// </summary>
        /// <param name="costoMin">Costo mínimo</param>
        /// <param name="costoMax">Costo máximo</param>
        /// <param name="fechaDesde">Fecha desde (opcional)</param>
        /// <param name="fechaHasta">Fecha hasta (opcional)</param>
        /// <returns>Lista de ReporteGananciasV2</returns>
        public List<ReporteGananciasV2> ObtenerReportePorRangoCostos(decimal costoMin, decimal costoMax, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            FiltrosReporteV2 filtros = new FiltrosReporteV2
            {
                CostoMinimo = costoMin,
                CostoMaximo = costoMax,
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta
            };
            
            return ObtenerReporteDinamico(filtros);
        }

        /// <summary>
        /// Obtiene estadísticas generales del reporte
        /// </summary>
        /// <param name="filtros">Filtros aplicados</param>
        /// <returns>Objeto con estadísticas</returns>
        public EstadisticasReporteV2 ObtenerEstadisticas(FiltrosReporteV2 filtros)
        {
            List<ReporteGananciasV2> datos = ObtenerReporteDinamico(filtros);
            
            EstadisticasReporteV2 estadisticas = new EstadisticasReporteV2();
            
            if (datos.Count > 0)
            {
                estadisticas.TotalCategorias = datos.Count;
                estadisticas.TotalVentas = datos.Sum(x => x.CantidadVentas);
                estadisticas.TotalUnidades = datos.Sum(x => x.UnidadesVendidas);
                estadisticas.TotalFacturacion = datos.Sum(x => x.VentaTotal);
                estadisticas.TotalCostos = datos.Sum(x => x.CostoTotal);
                estadisticas.TotalGanancias = datos.Sum(x => x.GananciaTotal);
                estadisticas.PorcentajeGananciaPromedio = estadisticas.TotalCostos > 0 ? 
                    (estadisticas.TotalGanancias / estadisticas.TotalCostos) * 100 : 0;
                estadisticas.CategoriaLider = datos.OrderByDescending(x => x.GananciaTotal).First().Categoria;
            }
            
            return estadisticas;
        }

        /// <summary>
        /// Configura los filtros de reporte según el tipo de rango especificado
        /// </summary>
        private void ConfigurarFiltrosPorTipoRango(FiltrosReporteV2 filtros, string tipoRango)
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
                case "ultimo_ano":
                    filtros.FechaDesde = hoy.AddYears(-1);
                    filtros.FechaHasta = hoy;
                    break;
                case "hoy":
                    filtros.FechaDesde = hoy.Date;
                    filtros.FechaHasta = hoy.Date.AddDays(1).AddSeconds(-1);
                    break;
                default:
                    // Sin filtro de fecha para "todo" o cualquier otro valor
                    break;
            }
        }
    }
}