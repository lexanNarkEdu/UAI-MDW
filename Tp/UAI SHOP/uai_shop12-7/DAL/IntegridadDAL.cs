using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Servicios;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace DAL.Servicios
{
    public class IntegridadDAL
    {
        private DAO mDAO = new DAO();
        private UsuarioDAL mUsuarioDAL = new UsuarioDAL();
        private ProductoDAL mProductoDAL = new ProductoDAL();
        public void ActualizarDVH(string tabla, DataRow row)
        {
            string dvh = row["DVH"].ToString();
            
            if (tabla == "Usuario")
            {
                int id = Convert.ToInt32(row["Id_Usuario"]);
                mUsuarioDAL.ModificarDVH(dvh, id);
            }
            else if (tabla == "Producto")
            {
                int id = Convert.ToInt32(row["IDProducto"]);
                mProductoDAL.ModificarDVH(dvh, id);
            }
        }

        public int ActualizarDVV(string nombreTabla, string dvv)
        {
            string mCommandText = "";
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@DVV", dvv));
            parametros.Add(new SqlParameter("@NombreTabla", nombreTabla)); 
            
            if (this.ObtenerDVV(nombreTabla) == null)// osea que todavia no existe el DVV correspondiente a esa tabla
            {                   
                mCommandText = "Insert into  Vertical(DVV, NombreTabla) VALUES(@DVV,@NombreTabla)";
            }
            else // ya existe y tengo que actualizar
            {                
                mCommandText = "Update Vertical SET DVV=@DVV where NombreTabla=@NombreTabla";
            }               
            
            try
            {
                return mDAO.ExecuteNonQuery(mCommandText,parametros);
   
            }
            catch (Exception)
            {

                return 0;
            }
        }
        public int InsertarDVV(string nombreTabla, string dvv)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@DVV", dvv));
            parametros.Add(new SqlParameter("@NombreTabla", nombreTabla));

            string mCommandText = "Insert into  Vertical(DVV, NombreTabla) VALUES(@DVV,@NombreTabla)";
            try
            {
                return mDAO.ExecuteNonQuery(mCommandText,parametros);

            }
            catch (Exception)
            {

                return 0;
            }
        }

        public string ObtenerDVV(string nombreTabla)
        {
            var parametros = new List<SqlParameter>();            
            parametros.Add(new SqlParameter("@NombreTabla", nombreTabla));
            string mCommandText = "Select * from Vertical WHERE NombreTabla =@NombreTabla ";

            try
            {
                DataSet mDs =mDAO.ExecuteDataSet(mCommandText,parametros);
                if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
                {
                    DataRow mdr = mDs.Tables[0].Rows[0];
                    return mdr["DVV"].ToString();
                }
                return null;

            }
            catch (Exception)
            {

                throw new Exception("No se pudo obtener el DVV solicitado");
            }
        }



    }
}
