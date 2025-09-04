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
    public class BitacoraDAL
    {
        private DAO mDAO = new DAO();

        // le pido un TipoEvento por string asi, si es null, quiere decir que no eligio ningun evento. Me ahorro hacer una sobrecarga del metodo Listar
        public List<BitacoraBE> Listar(DateTime FechaDesde, DateTime FechaHasta, string Evento) 
        {
            TipoEvento Tipo;
            string mCommandText = null;
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@FechaDesde", FechaDesde));
            parametros.Add(new SqlParameter("@FechaHasta", FechaHasta));          
            if (!Enum.TryParse(Evento, out Tipo))// o sea que no selecciono ningun evento
            {
                mCommandText = "SELECT * FROM Bitacora WHERE FechaHora BETWEEN @FechaDesde AND @FechaHasta ";
            }
            else // me traigo los registros que sean de ese tipo de evento
            {
                parametros.Add(new SqlParameter("@TipoEvento", Evento));
                mCommandText = "SELECT * FROM Bitacora WHERE TipoEvento = @TipoEvento AND FechaHora BETWEEN @FechaDesde AND @FechaHasta ";
            }


            try
            {
                DataSet mDataSet = mDAO.ExecuteDataSet(mCommandText,parametros);
                List<BitacoraBE> mLista = new List<BitacoraBE>();
                if (mDataSet.Tables.Count > 0 && mDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mdr in mDataSet.Tables[0].Rows)
                    {
                        BitacoraBE mbitacora = new BitacoraBE();
                        mbitacora.Id_Bitacora = Convert.ToInt32(mdr["Id_Bitacora"]);
                        mbitacora.Descripcion = mdr["Descripcion"].ToString();
                        mbitacora.FechaHora = Convert.ToDateTime(mdr["FechaHora"].ToString());
                        UsuarioDAL musuarioDAL = new UsuarioDAL();
                        int id_usuario=0;
                        if (int.TryParse(mdr["Id_Usuario"].ToString(), out id_usuario))
                        {
                            mbitacora.Usuario = musuarioDAL.ObtenerUsuarioxID(Convert.ToInt32(mdr["Id_Usuario"].ToString()));
                        }                       
                        if (!Enum.TryParse(mdr["TipoEvento"].ToString(), out Tipo))// en caso de que en un futuro un evento pueda ser null
                        {

                            Tipo = TipoEvento.Message;

                        }
                        mbitacora.TipodeEvento = Tipo; 
                        mLista.Add(mbitacora);
                    }
                    return mLista;
                }

                else
                {
                    return mLista;
                }
            }
            catch (Exception )
            {

                throw new Exception("No se puede obtener los registros de la bitacora");
            }
            
        }

        public int Agregar(BitacoraBE pBitacora)
        {
            
            string CommandText = "";
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@FechaHora", pBitacora.FechaHora));
            parametros.Add(new SqlParameter("@TipoEvento", pBitacora.TipodeEvento.ToString()));
            parametros.Add(new SqlParameter("@Descripcion", pBitacora.Descripcion));
           
            if (pBitacora.Usuario != null&& pBitacora.Usuario.Id_Usuario!=0)
            {
                parametros.Add( new SqlParameter("@Id_Usuario", pBitacora.Usuario.Id_Usuario));
                CommandText = "INSERT INTO Bitacora(FechaHora, TipoEvento, Descripcion, Id_Usuario) VALUES(@FechaHora,@TipoEvento,@Descripcion,@Id_Usuario)";            
            }
            else
            {
                CommandText = "INSERT INTO Bitacora(FechaHora, Descripcion,TipoEvento) VALUES(@FechaHora,@Descripcion,@TipoEvento)";
            }

            try
            {
               return mDAO.ExecuteNonQuery(CommandText, parametros);
            }
            catch (Exception )
            {
                return 1;
            }
        }

        
    }
}
