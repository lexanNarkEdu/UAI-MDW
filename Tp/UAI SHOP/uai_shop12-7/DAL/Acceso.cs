using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Acceso
    {
        private SqlConnection conexion;

        //metodo que se encarga de abrir la conexion con la BD
        public Boolean abrir()
        {
            Boolean exitoso = false;

            conexion = new SqlConnection();           
            conexion.ConnectionString = DAO.string_conexion;

            try
            {
                if (conexion != null && conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                    exitoso = true;
                }
            }
            catch (Exception e)
            {
                e.ToString();
                exitoso = false;
                cerrar();
            }
            return exitoso;
        }

        //metodo que se encarga de cerrar la conexion con la BD
        public void cerrar()
        {
            conexion.Close();
            conexion.Dispose();
            conexion = null;
            GC.Collect();
        }

        // metodo que crea el comando para ejecutar en la BD
        private SqlCommand crearComando(string Sql, List<SqlParameter> parametros = null,
            CommandType tipo = CommandType.StoredProcedure)
        {

            SqlCommand comando = new SqlCommand(Sql, conexion);
            comando.CommandType = tipo;

            if (parametros != null && parametros.Count > 0)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }

            return comando;
        }

        //metodo que modifica los datos en la BD, retorna la cantidad de registros afectados
        public int escribir(string Sql, List<SqlParameter> parametros = null, CommandType tipo = CommandType.StoredProcedure)
        {

            int resultado = 0;
            abrir();

            SqlCommand cmd = crearComando(Sql, parametros, tipo);

            try
            {
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                e.ToString();
                resultado = -1;
            }
            finally
            {
                cerrar();
            }

            return resultado;
        }

        //metodo que lee en la BD y completa un dataTable
        public DataTable leer(string Sql, List<SqlParameter> parametros = null)
        {
            abrir();
            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = crearComando(Sql, parametros);
            DataTable tabla = new DataTable();
            ad.Fill(tabla);
            cerrar();
            return tabla;
        }

        //metodo que crea un parametro del tipo STRING
        public SqlParameter crearParametro(string nom, string valor)
        {
            SqlParameter par = new SqlParameter(nom, valor);
            par.DbType = DbType.String;
            return par;
        }

        //metodo que crea un parametro del tipo INT32 entero corto
        public SqlParameter crearParametro(string nom, int valor)
        {
            SqlParameter par = new SqlParameter(nom, valor);
            par.DbType = DbType.Int32;
            return par;
        }

        //metodo que crea un parametro del tipo DATE
        public SqlParameter crearParametro(string nom, DateTime? valor)
        {
            SqlParameter par = new SqlParameter(nom, valor.HasValue ? (object)valor.Value : DBNull.Value);
            par.DbType = DbType.DateTime;
            return par;
        }

        //metodo que crea un parametro del tipo TIME
        public SqlParameter crearParametro(string nom, TimeSpan? valor)
        {
            var parametro = new SqlParameter(nom, SqlDbType.Time);

            if (valor.HasValue)
                parametro.Value = valor.Value;
            else
                parametro.Value = DBNull.Value;

            return parametro;
        }

        //metodo que crea un parametro del tipo INT64 entero largo
        public SqlParameter crearParametro(string nom, Int64 valor)
        {
            SqlParameter par = new SqlParameter(nom, valor);
            par.DbType = DbType.Int64;
            return par;
        }

        //metodo que crea un parametro del tipo BOOL para los flags
        public SqlParameter crearParametro(string nom, bool valor)
        {
            SqlParameter par = new SqlParameter(nom, valor);
            par.DbType = DbType.Boolean;
            return par;
        }

        public SqlParameter crearParametro(string nom, bool? valor)
        {
            SqlParameter par = new SqlParameter(nom, valor.HasValue ? (object)valor.Value : DBNull.Value);
            par.DbType = DbType.Boolean;
            return par;
        }

        public SqlParameter crearParametro(string nom, float valor)
        {
            SqlParameter par = new SqlParameter(nom, valor);
            par.DbType = DbType.Single;
            return par;
        }
    }
}
