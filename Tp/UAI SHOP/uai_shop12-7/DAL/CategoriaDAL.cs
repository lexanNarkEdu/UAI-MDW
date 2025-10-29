using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoriaDAL
    {
        private readonly Acceso _acceso = new Acceso(); // tu clase de conexión ya existente
        public DataTable ObtenerCategoriasConCantidad()
        {
            var dt = new DataTable();
            string sql = @"
        SELECT 
            c.IdCategoria,
            c.Nombre AS NombreCategoria,
            c.Descripcion,
            COUNT(p.IdProducto) AS CantidadProductos
        FROM Categoria c
        LEFT JOIN Producto p ON p.IdCategoria = c.IdCategoria
        GROUP BY c.IdCategoria, c.Nombre, c.Descripcion
        ORDER BY c.Nombre;";
            using (SqlConnection conn = new SqlConnection(DAO.string_conexion))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                conn.Open();
                da.Fill(dt);
            }
            return dt;
        }
        public DataTable ObtenerPorCategoriaID(int idCategoria)
        {
            string sql = @"
            SELECT 
                IdProducto,
                Nombre AS NombreProducto,
                Precio
            FROM Producto
            WHERE IdCategoria = @IdCategoria
            ORDER BY Nombre;";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(DAO.string_conexion))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
