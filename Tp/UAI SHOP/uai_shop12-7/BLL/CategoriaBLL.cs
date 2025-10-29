using BE;
using DAL;
using SERVICES;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoriaBLL
    {
        private readonly CategoriaDAL _dal = new CategoriaDAL();
        public (string xmlPath, string xmlFiltradoPath) GenerarXmlCategorias(string appDataPath)
        {
            // 1) Traer datos (negocio) desde DAL
            DataTable dt = _dal.ObtenerCategoriasConCantidad();
            // 2) Mapear a DTOs (acá podés aplicar reglas/validaciones)
            var categorias = dt.AsEnumerable().Select(r => new Categoria
            {
                IDCategoria = r.Field<int>("IdCategoria"),
                Nombre = r.Field<string>("NombreCategoria"),
                Descripcion = r.Field<string>("Descripcion"),
                CantidadProductos = r.Field<int>("CantidadProductos")
            }).ToList();
            // 3) Generar XML y aplicar XSLT usando el Service
            string xmlPath = Path.Combine(appDataPath, "catalogo.xml");
            string xsltPath = Path.Combine(appDataPath, "catalogo_filter.xslt");
            string xmlFiltradoPath = Path.Combine(appDataPath, "catalogo_filtrado.xml");
            CategoriaXmlService.GenerarCategoriasXml(categorias, xmlPath);
            CategoriaXmlService.AplicarXslt(xmlPath, xsltPath, xmlFiltradoPath);
            return (xmlPath, xmlFiltradoPath);
        }
        public DataTable ObtenerCategoriasPorId(int id)
        {
            return _dal.ObtenerPorCategoriaID(id);
        }
    }
}