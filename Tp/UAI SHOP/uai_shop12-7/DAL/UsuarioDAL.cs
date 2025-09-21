using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE.Servicios;
using System.Data.SqlClient;
using BE;

namespace DAL.Servicios
{
   public class UsuarioDAL: IVerificable
    {

       private DAO mDAO = new DAO();
               
        public Usuario ObtenerUsuarioxID(int ID_usuario)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id_Usuario", ID_usuario));

            string mCommandText = "SELECT Id_Usuario, Username, Clave, TipoUsuario, cant_intentos, bloqueado, DVH FROM Usuario WHERE Id_Usuario = @Id_Usuario";

            try
            {

                DataSet mDs = mDAO.ExecuteDataSet(mCommandText,parametros);

                if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
                {
                    Usuario mUsuario = new Usuario();
                    DataRow mdr = mDs.Tables[0].Rows[0];
                    ValorizarEntidad(mUsuario, mdr);
                    return mUsuario;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw new Exception("No_se_pudo_Obtener_el_Usuario_Solicitado");
            }


        }
       
       
        private void ValorizarEntidad(Usuario pUsuario, DataRow dataRow)
        {
            pUsuario.Id_Usuario = Convert.ToInt32(dataRow["Id_Usuario"]);
            pUsuario.USERNAME = dataRow["Username"].ToString();
            pUsuario.Clave = dataRow["Clave"].ToString();
            pUsuario.TipoUser= Convert.ToInt32(dataRow["TipoUsuario"]);
            pUsuario.CANT_INTENTOS= Convert.ToInt32(dataRow["cant_intentos"]);
            pUsuario.ESTA_BLOQUEADO = Convert.ToBoolean(dataRow["bloqueado"]);
            pUsuario.dvh= dataRow["DVH"].ToString();
        }

        // codigo P
        private Acceso ac;

        public UsuarioDAL()
        {
            ac = new Acceso();
        }
        public Usuario Buscar(Usuario user)
        {
            Usuario us = null;
            
            string consulta = @"SELECT Id_Usuario, Username, Clave, TipoUsuario, cant_intentos, bloqueado, DVH 
                    FROM Usuario 
                    WHERE Username = @user AND Clave = @pass";

            using (SqlConnection conn = new SqlConnection(DAO.string_conexion))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                {
                    // Asegurate de que no agregás este parámetro más de una vez
                    cmd.Parameters.AddWithValue("@user", user.USERNAME);
                    cmd.Parameters.AddWithValue("@pass", user.Clave);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string passwordBD = reader["Clave"].ToString();

                            if (passwordBD == user.Clave)
                            {
                                us = new Usuario();
                                int iduser = Convert.ToInt32(reader["Id_Usuario"]);
                                us.Id_Usuario = iduser;
                                us.USERNAME = reader["Username"].ToString();
                                us.Clave = passwordBD;
                                int tipo = Convert.ToInt32(reader["TipoUsuario"]);
                                us.TipoUser = tipo;
                                us.ESTA_BLOQUEADO = Convert.ToBoolean(reader["bloqueado"].ToString());
                                us.CANT_INTENTOS= Convert.ToInt32(reader["cant_intentos"]);
                                us.dvh = reader["DVH"].ToString();

                            }
                        }
                    }
                }
            }

            return us;
        }



        public int ModificarDVH(string pDVH,int ID)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@DVH", pDVH));
            parametros.Add(new SqlParameter("@Id_Usuario", ID));

            string mCommandText = "UPDATE Usuario SET DVH =@DVH where Id_Usuario=@Id_Usuario";
            try
            {
                return mDAO.ExecuteNonQuery(mCommandText, parametros);
            }
            catch (Exception)
            {

                throw new Exception("No se pudo actualizar el DVH del usuario");
            }
        }

        /// <summary>
        /// Metodo que brinda todos los registros de la base de datos de la tabla Usuario
        /// </summary>
        /// <returns></returns>
        public List<DataRow> ObtenerRegistros()
        {
            string mCommandText = "Select * from Usuario";

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

                throw new Exception("No se puede obtener los registros de los usuarios");
            }
        }

        // pato
        /**
         * 
         */
        public Boolean sumarIntentoErroneo(string username)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(ac.crearParametro("@Username", username.Trim()));
            int modificados = ac.escribir("SUMAR_INTENTO", parametros);

            return modificados != 0 ? true : false;
        }

        /*
         * 
         */
        public Boolean desbloquearUsuario(string username)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(ac.crearParametro("@Username", username.Trim()));
            int modificados = ac.escribir("DESBLOQUEAR_USUARIO", parametros);

            return modificados != 0 ? true : false;
        }

        /**
         * Resetea los intentos fallidos de un usuario a 0
         */
        public Boolean resetearIntentos(string username)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(ac.crearParametro("@Username", username.Trim()));
            int modificados = ac.escribir("RESET_INTENTOS_USUARIO", parametros);

            return modificados != 0 ? true : false;
        }

        /**
         * solo lista username, cant_intentos y si esta bloqueado
         * se utiliza para la logica de bloqueo y 3 intentos fallidos
         */
        public Usuario listar(String usernameIn)
        {
            Usuario userEncontrado = null;

            string consulta = @"SELECT Username, cant_intentos, bloqueado 
                    FROM Usuario 
                    WHERE Username = @user ";

            using (SqlConnection conn = new SqlConnection(DAO.string_conexion))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                {
                    // Asegurate de que no agregás este parámetro más de una vez
                    cmd.Parameters.AddWithValue("@user", usernameIn);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userEncontrado = new Usuario();
                            userEncontrado.USERNAME = reader["Username"].ToString();
                            userEncontrado.CANT_INTENTOS = int.Parse(reader["cant_intentos"].ToString());
                            userEncontrado.ESTA_BLOQUEADO = Boolean.Parse(reader["bloqueado"].ToString());
                        }
                    }
                }
            }
            return userEncontrado;
        }

        /**
         * Guarda en la BD la pass encriptada
         */
        public Boolean guardarPassword(string username, string pass)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(ac.crearParametro("@Username", username.Trim()));
            parametros.Add(ac.crearParametro("@pass", pass.Trim()));
            int modificados = ac.escribir("GUARDAR_PASS_ENCRIPTADA", parametros);

            return modificados != 0 ? true : false;
        }

        /**
         * Metodo para buscar los usuarios bloqueados
         */
        public List<String> listarUsuariosBloqueados()
        {
            List<String> usuariosEncontrados = new List<String>();

            string consulta = @"SELECT Username 
                    FROM Usuario 
                    WHERE bloqueado = 1 ";

            using (SqlConnection conn = new SqlConnection(DAO.string_conexion))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                {

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuariosEncontrados.Add(reader["Username"].ToString());
                        }
                    }
                }
            }
            return usuariosEncontrados;
        }



    }
}
