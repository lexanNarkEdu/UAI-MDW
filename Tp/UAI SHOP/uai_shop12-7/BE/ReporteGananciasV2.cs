using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Entidad que representa los datos de reporte de ganancias V2 con filtros dinámicos
    /// </summary>
    public class ReporteGananciasV2
    {
        public string Categoria { get; set; }
        public int CantidadVentas { get; set; }
        public int UnidadesVendidas { get; set; }
        public decimal VentaTotal { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal GananciaTotal { get; set; }
        public decimal PorcentajeGanancia { get; set; }

        public ReporteGananciasV2()
        {
            Categoria = string.Empty;
            CantidadVentas = 0;
            UnidadesVendidas = 0;
            VentaTotal = 0;
            CostoTotal = 0;
            GananciaTotal = 0;
            PorcentajeGanancia = 0;
        }

        public override string ToString()
        {
            return "Categoría: " + Categoria + ", Ganancias: $" + GananciaTotal.ToString("N2") + ", " +
                   "Ventas: " + CantidadVentas + ", Unidades: " + UnidadesVendidas;
        }

        public override bool Equals(object obj)
        {
            ReporteGananciasV2 other = obj as ReporteGananciasV2;
            if (other != null)
            {
                return Categoria == other.Categoria;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Categoria != null ? Categoria.GetHashCode() : 0;
        }
    }

    /// <summary>
    /// Clase para estadísticas del reporte V2
    /// </summary>
    public class EstadisticasReporteV2
    {
        public int TotalCategorias { get; set; }
        public int TotalVentas { get; set; }
        public int TotalUnidades { get; set; }
        public decimal TotalFacturacion { get; set; }
        public decimal TotalCostos { get; set; }
        public decimal TotalGanancias { get; set; }
        public decimal PorcentajeGananciaPromedio { get; set; }
        public string CategoriaLider { get; set; }

        public override string ToString()
        {
            return "Categorías: " + TotalCategorias + ", Ganancias Totales: $" + TotalGanancias.ToString("N2") + ", " +
                   "Margen: " + PorcentajeGananciaPromedio.ToString("N1") + "%, Líder: " + CategoriaLider;
        }
    }
}