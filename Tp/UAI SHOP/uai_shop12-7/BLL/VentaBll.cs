using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VentaBll
    {
        private VentaDAL dal = new VentaDAL();

        public void RealizarVenta(Carrito carrito, string metodoPago, int IDusuario)
        {
            if (carrito.Productos == null || carrito.Productos.Count == 0)
                throw new Exception("El carrito está vacío.");

            Venta venta = new Venta
            {
                IDUsuario = IDusuario,
                Fecha = DateTime.Now,
                Productos = carrito.Productos,
                MetodoPago = metodoPago,
                Total = carrito.CalcularTotal()
            };

            dal.InsertarVenta(venta);
        }
    }
}
