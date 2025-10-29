using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace SERVICES
{
    public class CategoriaXmlService
    {
    public static void GenerarCategoriasXml(
        IEnumerable<Categoria> categorias,
        string xmlPath)
        {
            var root = new XElement("Categorias");
            foreach (var c in categorias)
            {
                root.Add(new XElement("Categoria",
                    new XElement("IdCategoria", c.IDCategoria),
                    new XElement("NombreCategoria", c.Nombre),
                    new XElement("Descripcion", c.Descripcion ?? string.Empty),
                    new XElement("CantidadProductos", c.CantidadProductos)
                ));
            }
            Directory.CreateDirectory(Path.GetDirectoryName(xmlPath));
            root.Save(xmlPath);
        }
        public static void AplicarXslt(string xmlPath, string xsltPath, string outputPath)
        {
            var transform = new XslCompiledTransform();

            // ✅ Cargar desde la ruta física
            transform.Load(xsltPath);

            // ✅ Ejecutar la transformación
            transform.Transform(xmlPath, outputPath);
        }
    }
}

