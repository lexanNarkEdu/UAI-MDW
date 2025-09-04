using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Servicios
{
   public  class BitacoraBE
    {
        public int Id_Bitacora { get;  set; }
        public DateTime FechaHora { get; set; }
        public string Descripcion { get; set; }
        public TipoEvento TipodeEvento { get; set; }
        public Usuario Usuario { get; set; }




    }
}
