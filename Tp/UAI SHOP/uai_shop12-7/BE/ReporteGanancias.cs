using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Entidad que representa los datos de reporte de ganancias por categoría
    /// </summary>
    public class ReporteGanancias
    {
        public string Categoria { get; set; }
        public int VentasConEstaCategoria { get; set; }
        public int UnidadesTotales { get; set; }
        public decimal PrecioPromedio { get; set; }
        public decimal GananciaTotal { get; set; }

        public ReporteGanancias()
        {
            Categoria = string.Empty;
            VentasConEstaCategoria = 0;
            UnidadesTotales = 0;
            PrecioPromedio = 0;
            GananciaTotal = 0;
        }

        public ReporteGanancias(string categoria, int ventasConEstaCategoria, int unidadesTotales, decimal precioPromedio, decimal gananciaTotal)
        {
            Categoria = categoria;
            VentasConEstaCategoria = ventasConEstaCategoria;
            UnidadesTotales = unidadesTotales;
            PrecioPromedio = precioPromedio;
            GananciaTotal = gananciaTotal;
        }
    }
}