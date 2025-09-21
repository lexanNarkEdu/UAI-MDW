using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
   public class DAO
    {        
        SqlConnection mConexion = new SqlConnection(string_conexion);
        public static readonly string string_conexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=ProyectoWEB;Integrated Security=True"; //Conexion pablito
        //public static readonly string string_conexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=ProyectoWEB;Integrated Security=True";// Conexion matias
        //public static readonly string string_conexion = @"Data Source=(localdb)\serverSQL;Initial Catalog=master;Integrated Security=True";// Conexion kevo
        //public static readonly string string_conexion = @"Data Source=.\SQLEXPRESS;Initial Catalog=ProyectoWEB2;Integrated Security=True"; //conexion pato
        public SqlConnection AbrirConexion()
        {
            try
            {
                if (mConexion.State != ConnectionState.Open)
                {
                    mConexion.Open();
                }
                return mConexion;
            }
            catch (Exception ex)
            {
                throw (ex);

            }

        }
        public void CerrarConexion()
        {
            try
            {
                if (mConexion.State == ConnectionState.Open)
                {
                    mConexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);

            }


        }
        public int ExecuteNonQuery(string pCommandText,List<SqlParameter> parametros)
        {
            try
            {
                foreach (SqlParameter parametro in parametros)
                {
                    if (parametro.Value == null && parametro.SqlDbType == SqlDbType.NVarChar)
                    {
                        parametro.Value = "";
                    }
                    else if (parametro.Value == null)
                    {
                        parametro.Value = DBNull.Value;
                    }
                }
                SqlCommand command = new SqlCommand(pCommandText, mConexion);
                command.Parameters.AddRange(parametros.ToArray());
                mConexion.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (mConexion.State == ConnectionState.Open)
                    mConexion.Close();
            }
        }
        public int ExecuteNonQuery(string pCommandText)
        {
            try
            {
                SqlCommand command = new SqlCommand(pCommandText, mConexion);
                mConexion.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                if (mConexion.State == ConnectionState.Open)
                    mConexion.Close();
            }


        }

        public DataSet ExecuteDataSet(string pCommandText)
        {
            DataSet mDs = new DataSet();
            try
            {
                SqlDataAdapter mDa = new SqlDataAdapter(pCommandText, mConexion);
                mConexion.Open();
                mDa.Fill(mDs);
                return mDs;
            }

            catch (Exception ex)
            {


                throw ex;
            }
            finally
            {
                mConexion.Close();

            }
        }
        public DataSet ExecuteDataSet(string pCommandText, List<SqlParameter> parametros)
        {
            DataSet mDs = new DataSet();
            try
            {
                SqlCommand mCommand = new SqlCommand(pCommandText, this.mConexion);
                mCommand.Parameters.AddRange(parametros.ToArray());
                SqlDataAdapter mDa = new SqlDataAdapter(mCommand);
                mConexion.Open();
                mDa.Fill(mDs);
                return mDs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mConexion.Close();
            }
        }

    }
}
