using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ReportesDAL
    {
        private string connectionString = DAO.string_conexion;
        private DAO mDAO = new DAO();

        /// <summary>
        /// Obtiene el reporte de ganancias general por categorías
        /// </summary>
        /// <returns>Lista de reportes de ganancias</returns>
        public List<ReporteGanancias> ObtenerGananciasGeneral()
        {
            var reportes = new List<ReporteGanancias>();
            
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TOP_GANANCIAS_CATEGORIA_GENERAL", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        // Calcular precio promedio: VentaTotal / UnidadesVendidas
                        decimal precioPromedio = 0;
                        var unidadesVendidas = Convert.ToInt32(reader["UnidadesVendidas"]);
                        if (unidadesVendidas > 0)
                        {
                            precioPromedio = Convert.ToDecimal(reader["VentaTotal"]) / unidadesVendidas;
                        }
                        
                        var reporte = new ReporteGanancias(
                            categoria: reader["Categoria"].ToString(),
                            ventasConEstaCategoria: Convert.ToInt32(reader["CantidadVentas"]),
                            unidadesTotales: unidadesVendidas,
                            precioPromedio: precioPromedio,
                            gananciaTotal: Convert.ToDecimal(reader["GananciaTotal"])
                        );
                        reportes.Add(reporte);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ganancias generales: " + ex.Message);
            }
            
            return reportes;
        }

        /// <summary>
        /// Obtiene el reporte de ganancias del último mes por categorías
        /// </summary>
        /// <returns>Lista de reportes de ganancias</returns>
        public List<ReporteGanancias> ObtenerGananciasUltimoMes()
        {
            var reportes = new List<ReporteGanancias>();
            
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TOP_GANANCIAS_CATEGORIA_ULTIMO_MES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        // Calcular precio promedio: VentaTotal / UnidadesVendidas
                        decimal precioPromedio = 0;
                        var unidadesVendidas = Convert.ToInt32(reader["UnidadesVendidas"]);
                        if (unidadesVendidas > 0)
                        {
                            precioPromedio = Convert.ToDecimal(reader["VentaTotal"]) / unidadesVendidas;
                        }
                        
                        var reporte = new ReporteGanancias(
                            categoria: reader["Categoria"].ToString(),
                            ventasConEstaCategoria: Convert.ToInt32(reader["CantidadVentas"]),
                            unidadesTotales: unidadesVendidas,
                            precioPromedio: precioPromedio,
                            gananciaTotal: Convert.ToDecimal(reader["GananciaTotal"])
                        );
                        reportes.Add(reporte);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ganancias del último mes: " + ex.Message);
            }
            
            return reportes;
        }

        /// <summary>
        /// Obtiene el reporte de ganancias semanal por categorías
        /// </summary>
        /// <returns>Lista de reportes de ganancias</returns>
        public List<ReporteGanancias> ObtenerGananciasSemanal()
        {
            var reportes = new List<ReporteGanancias>();
            
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TOP_GANANCIAS_CATEGORIA_SEMANAL", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        // Calcular precio promedio: VentaTotal / UnidadesVendidas
                        decimal precioPromedio = 0;
                        var unidadesVendidas = Convert.ToInt32(reader["UnidadesVendidas"]);
                        if (unidadesVendidas > 0)
                        {
                            precioPromedio = Convert.ToDecimal(reader["VentaTotal"]) / unidadesVendidas;
                        }
                        
                        var reporte = new ReporteGanancias(
                            categoria: reader["Categoria"].ToString(),
                            ventasConEstaCategoria: Convert.ToInt32(reader["CantidadVentas"]),
                            unidadesTotales: unidadesVendidas,
                            precioPromedio: precioPromedio,
                            gananciaTotal: Convert.ToDecimal(reader["GananciaTotal"])
                        );
                        reportes.Add(reporte);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ganancias semanales: " + ex.Message);
            }
            
            return reportes;
        }
    }
}