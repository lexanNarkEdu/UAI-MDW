using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VentaDAL
    {
        private string connectionString = DAO.string_conexion;

        public void InsertarVenta(Venta venta)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    string sqlVenta = "INSERT INTO Venta (IDUsuario, Fecha, Total, MetodoPago) OUTPUT INSERTED.IDVenta VALUES (@IDUsuario, @Fecha, @Total, @MetodoPago)";
                    SqlCommand cmdVenta = new SqlCommand(sqlVenta, con, trans);
                    cmdVenta.Parameters.AddWithValue("@IDUsuario", venta.IDUsuario);
                    cmdVenta.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                    cmdVenta.Parameters.AddWithValue("@MetodoPago", venta.MetodoPago);
                    int ventaId = (int)cmdVenta.ExecuteScalar();

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
    }
}
