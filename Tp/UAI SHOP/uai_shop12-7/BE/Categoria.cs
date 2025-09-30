using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Categoria
    {
        public int IDCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Categoria()
        {
        }

        public Categoria(int idCategoria, string nombre, string descripcion = "")
        {
            IDCategoria = idCategoria;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}