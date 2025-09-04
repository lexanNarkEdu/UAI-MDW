using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<Producto> Productos { get; set; }
        public string MetodoPago { get; set; }

        public int IDUsuario { get; set; }
    }
}
