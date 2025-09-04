using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Carrito
    {
        public List<Producto> Productos { get; set; } = new List<Producto>();

      

        public void AgregarProducto(Producto producto)
        {
            Productos.Add(producto); // agrega sin controlar duplicados ni cantidad
        }

        public decimal CalcularTotal()
        {
            return Productos.Sum(p => p.Precio);
        }
        public void Vaciar()
        {
            Productos.Clear();
        }
    }



}

