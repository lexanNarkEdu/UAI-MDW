using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BackupRestoreDal : Acceso
    {
        public void GenerarBackUp(string rutaCompletaBak)
        {
            using (SqlConnection conn = new SqlConnection(DAO.string_conexion))
            {
                conn.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conn;
                    comando.CommandText = "BACKUP DATABASE [ProyectoWEB] TO DISK = N'" + rutaCompletaBak + "' WITH NOFORMAT, NOINIT, NAME = N'ProyectoWEB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                    comando.ExecuteNonQuery();
                }
            }
        }
        public void Restore(string ruta)
        {
            using (SqlConnection conn = new SqlConnection(DAO.string_conexion))
            {
                conn.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conn;
                    comando.CommandText = "ALTER DATABASE [ProyectoWEB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                    comando.ExecuteNonQuery();
                    comando.CommandText = "USE MASTER; RESTORE DATABASE [ProyectoWEB] FROM DISK='" + ruta + "' WITH REPLACE";
                    comando.ExecuteNonQuery();
                    comando.CommandText = "ALTER DATABASE [ProyectoWEB] SET MULTI_USER";
                    comando.ExecuteNonQuery();

                }
            }
        }
    }
}
