using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class FiltrosReporteV2
    {
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int? IdCategoria { get; set; }
        public int? IDCategoria 
        { 
            get { return IdCategoria; } 
            set { IdCategoria = value; } 
        }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public decimal? CostoMinimo { get; set; }
        public decimal? CostoMaximo { get; set; }
        public decimal? GananciaMinima { get; set; }
        public decimal? VentasMinimas { get; set; }
        public decimal? VentasMaximas { get; set; }
        public string Categoria { get; set; }
        public bool? SoloConStock { get; set; }
        public string OrdenarPor { get; set; }
        public bool OrdenarDescendente { get; set; }

        public bool TieneFiltros()
        {
            return FechaDesde.HasValue || FechaHasta.HasValue || IDCategoria.HasValue ||
                   CostoMinimo.HasValue || CostoMaximo.HasValue ||
                   GananciaMinima.HasValue || VentasMinimas.HasValue || VentasMaximas.HasValue ||
                   !string.IsNullOrEmpty(Categoria);
        }
    }
}