using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICES
{
    public class SecurityService
    {
        public SecurityService() { }

        private static SecurityService _instance;

        //singleton de la instancia del servicio
        public static SecurityService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SecurityService();
                }
                return _instance;
            }
        }

        private void ocultarMenusSegunPermiso()
        {

        }



    }
}
