using BE;
using BE.Servicios;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductoBll : IVerificable
    {
        private ProductoDAL dal = new ProductoDAL();

        public List<Producto> ObtenerTodos()
        {
            return dal.ObtenerTodos();
        }

        public Producto ObtenerPorId(int id)
        {
            return dal.ObtenerPorId(id);
        }

        public List<DataRow> ObtenerRegistros()
        {
            return  dal.ObtenerRegistros();
        }
    }
}
