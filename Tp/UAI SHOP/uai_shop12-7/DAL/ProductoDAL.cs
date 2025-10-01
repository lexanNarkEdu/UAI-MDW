using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data.SqlClient;
using BE.Servicios;
using System.Data;

namespace DAL
{
  

    public class ProductoDAL: IVerificable
    {
        private string connectionString = DAO.string_conexion;

        private DAO mDAO = new DAO();

        public List<Producto> ObtenerTodos()
        {
            var lista = new List<Producto>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IDProducto, Nombre, Precio FROM Producto";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Producto
                    {
                        Id = Convert.ToInt32(reader["IDProducto"]),
                        Nombre = reader["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"])
                    });
                }
            }
            return lista;
        }

        public int ModificarDVH(string pDVH, int ID)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@DVH", pDVH));
            parametros.Add(new SqlParameter("@IDProducto", ID));

            string mCommandText = "UPDATE Producto SET DVH =@DVH where IDProducto=@IDProducto";
            try
            {
                return mDAO.ExecuteNonQuery(mCommandText, parametros);
            }
            catch (Exception)
            {

                throw new Exception("No se pudo actualizar el DVH del usuario");
            }
        }
        public Producto ObtenerPorId(int id)
        {
            Producto p = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IDProducto, Nombre, Precio FROM Producto WHERE IDProducto = @IDProducto";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IDProducto", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    p = new Producto
                    {
                        Id = Convert.ToInt32(reader["IDProducto"]),
                        Nombre = reader["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"])
                    };
                }
            }
            return p;
        }

        
            /// <summary>
            /// Metodo que brinda todos los registros de la base de datos de la tabla Usuario
            /// </summary>
            /// <returns></returns>
        public List<DataRow> ObtenerRegistros()
        {
            string mCommandText = "Select * from Producto";

            try
            {
                DataSet mDataSet = mDAO.ExecuteDataSet(mCommandText);
                List<DataRow> mlista = new List<DataRow>();
                if (mDataSet.Tables.Count > 0 && mDataSet.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow mdr in mDataSet.Tables[0].Rows)
                    {
                        mlista.Add(mdr);
                    }
                    return mlista;
                }

                else
                {
                    return mlista;
                }
            }
            catch (Exception)
            {

                throw new Exception("No se puede obtener los registros de los Productos");
            }
        }

        public List<BE.Categoria> ObtenerCategorias()
        {
            var lista = new List<BE.Categoria>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IDCategoria, Nombre, Descripcion FROM Categoria ORDER BY Nombre";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new BE.Categoria
                    {
                        IDCategoria = Convert.ToInt32(reader["IDCategoria"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : ""
                    });
                }
            }
            return lista;
        }
    }
    
}
