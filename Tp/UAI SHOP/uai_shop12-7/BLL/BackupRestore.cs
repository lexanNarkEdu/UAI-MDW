using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BackupRestore
    {

        BackupRestoreDal backup = new BackupRestoreDal();

        public void CrearBackUp(string ruta)
        {
            backup.GenerarBackUp(ruta);
        }
        public void EjecutarRestore(string ruta)
        {
            backup.Restore(ruta);
        }
    }
}

