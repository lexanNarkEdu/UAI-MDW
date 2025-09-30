using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;

public partial class ReporteGanancias : System.Web.UI.Page
{
    private ReportingWebService webService = new ReportingWebService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Cargar reporte general por defecto
            CargarReporteGeneral();
        }
    }

    protected void btnReporteGeneral_Click(object sender, EventArgs e)
    {
        CargarReporteGeneral();
    }

    protected void btnReporteUltimoMes_Click(object sender, EventArgs e)
    {
        CargarReporteUltimoMes();
    }

    protected void btnReporteUltimoAnio_Click(object sender, EventArgs e)
    {
        CargarReporteUltimoAnio();
    }

    protected void btnBuscarFiltros_Click(object sender, EventArgs e)
    {
        CargarReporteConFiltros();
    }

    private void CargarReporteGeneral()
    {
        try
        {
            // Obtener todos los reportes usando WebService
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteGananciasV2("", "", 0, 0, 0, 0, 0);
            
            MostrarResultados(reportes, "Reporte General de Ganancias");
        }
        catch (Exception ex)
        {
            MostrarError("Error al cargar el reporte general: " + ex.Message);
        }
    }

    private void CargarReporteUltimoMes()
    {
        try
        {
            // Obtener reporte del último mes usando WebService
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteV2PorPeriodo("ultimo_mes");
            
            MostrarResultados(reportes, "Reporte del Último Mes");
        }
        catch (Exception ex)
        {
            MostrarError("Error al cargar el reporte del último mes: " + ex.Message);
        }
    }

    private void CargarReporteUltimoAnio()
    {
        try
        {
            // Obtener reporte del último año usando WebService
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteV2PorPeriodo("ultimo_anio");
            
            MostrarResultados(reportes, "Reporte del Último Año");
        }
        catch (Exception ex)
        {
            MostrarError("Error al cargar el reporte del último año: " + ex.Message);
        }
    }

    private void CargarReporteConFiltros()
    {
        try
        {
            // Declarar variables para reutilizar en WebService
            decimal precioMin = 0, precioMax = 0, costoMin = 0, costoMax = 0;
            
            // Crear filtros basados en los controles de la UI
            FiltrosReporteV2 filtros = new FiltrosReporteV2();
            
            // Aplicar filtro de fechas
            DateTime fechaDesde;
            if (!string.IsNullOrEmpty(txtFechaDesde.Text) && DateTime.TryParse(txtFechaDesde.Text, out fechaDesde))
            {
                filtros.FechaDesde = fechaDesde;
            }
            
            DateTime fechaHasta;
            if (!string.IsNullOrEmpty(txtFechaHasta.Text) && DateTime.TryParse(txtFechaHasta.Text, out fechaHasta))
            {
                filtros.FechaHasta = fechaHasta;
            }

            // Aplicar filtro de categoría
            if (ddlCategoria.SelectedValue != "")
            {
                filtros.Categoria = ddlCategoria.SelectedValue;
            }

            // Aplicar filtros de costo
            if (!string.IsNullOrEmpty(txtCostoMin.Text) && decimal.TryParse(txtCostoMin.Text, out costoMin))
            {
                filtros.CostoMinimo = costoMin;
            }
            
            if (!string.IsNullOrEmpty(txtCostoMax.Text) && decimal.TryParse(txtCostoMax.Text, out costoMax))
            {
                filtros.CostoMaximo = costoMax;
            }

            // Aplicar filtros de ventas
            int ventasMin;
            if (!string.IsNullOrEmpty(txtVentasMin.Text) && int.TryParse(txtVentasMin.Text, out ventasMin))
            {
                filtros.VentasMinimas = ventasMin;
            }
            
            int ventasMax;
            if (!string.IsNullOrEmpty(txtVentasMax.Text) && int.TryParse(txtVentasMax.Text, out ventasMax))
            {
                filtros.VentasMaximas = ventasMax;
            }

            // Aplicar filtros de precio
            if (!string.IsNullOrEmpty(txtPrecioMin.Text) && decimal.TryParse(txtPrecioMin.Text, out precioMin))
            {
                filtros.PrecioMinimo = precioMin;
            }
            
            if (!string.IsNullOrEmpty(txtPrecioMax.Text) && decimal.TryParse(txtPrecioMax.Text, out precioMax))
            {
                filtros.PrecioMaximo = precioMax;
            }

            // Obtener reporte con filtros usando WebService
            string fechaDesdeStr = filtros.FechaDesde != null ? filtros.FechaDesde.Value.ToString("yyyy-MM-dd") : "";
            string fechaHastaStr = filtros.FechaHasta != null ? filtros.FechaHasta.Value.ToString("yyyy-MM-dd") : "";
            int idCategoria = 0; // Necesitaríamos convertir el nombre de categoría a ID
            precioMin = filtros.PrecioMinimo ?? 0;
            precioMax = filtros.PrecioMaximo ?? 0;
            costoMin = filtros.CostoMinimo ?? 0;
            costoMax = filtros.CostoMaximo ?? 0;
            
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteGananciasV2(
                fechaDesdeStr, fechaHastaStr, idCategoria, precioMin, precioMax, costoMin, costoMax);
            
            string titulo = "Reporte Personalizado";
            if (filtros.TieneFiltros())
            {
                titulo += " (Filtros aplicados)";
            }
            
            MostrarResultados(reportes, titulo);
        }
        catch (Exception ex)
        {
            MostrarError("Error al cargar el reporte con filtros: " + ex.Message);
        }
    }

    private void MostrarResultados(ReporteGananciasV2[] reportes, string tipoReporte)
    {
        if (reportes != null && reportes.Length > 0)
        {
            // Vincular datos al GridView
            gvReporteGanancias.DataSource = reportes;
            gvReporteGanancias.DataBind();

            // Generar estadísticas
            GenerarEstadisticas(reportes, tipoReporte);
            
            // Mostrar panel de estadísticas
            panelEstadisticas.Visible = true;
        }
        else
        {
            // No hay datos
            gvReporteGanancias.DataSource = null;
            gvReporteGanancias.DataBind();
            
            lblEstadisticas.Text = "📊 " + tipoReporte + "<br/>No se encontraron resultados con los criterios especificados.";
            lblEstadisticas.ForeColor = System.Drawing.Color.Orange;
            panelEstadisticas.Visible = true;
        }
    }

    private void GenerarEstadisticas(ReporteGananciasV2[] reportes, string tipoReporte)
    {
        if (reportes == null || reportes.Length == 0)
            return;

        try
        {
            // Calcular estadísticas generales
            int totalCategorias = reportes.Length;
            int totalVentas = reportes.Sum(r => r.CantidadVentas);
            int totalUnidades = reportes.Sum(r => r.UnidadesVendidas);
            decimal totalVentasMonto = reportes.Sum(r => r.VentaTotal);
            decimal totalCostos = reportes.Sum(r => r.CostoTotal);
            decimal totalGanancias = reportes.Sum(r => r.GananciaTotal);
            
            // Calcular porcentaje de ganancia general
            decimal porcentajeGeneral = totalVentasMonto > 0 
                ? (totalGanancias / totalVentasMonto) * 100 
                : 0;

            // Encontrar la categoría más rentable
            var categoriaTopGanancia = reportes
                .OrderByDescending(r => r.GananciaTotal)
                .FirstOrDefault();

            // Encontrar la categoría con más ventas
            var categoriaTopVentas = reportes
                .OrderByDescending(r => r.CantidadVentas)
                .FirstOrDefault();

            // Construir texto de estadísticas
            string estadisticas = 
                "<strong>" + tipoReporte + "</strong><br/>" +
                "📊 <strong>Resumen General:</strong><br/>" +
                "• Categorías analizadas: <strong>" + totalCategorias + "</strong><br/>" +
                "• Total de ventas: <strong>" + totalVentas.ToString("N0") + "</strong> transacciones<br/>" +
                "• Total de unidades: <strong>" + totalUnidades.ToString("N0") + "</strong><br/>" +
                "• Facturación total: <strong>" + totalVentasMonto.ToString("C") + "</strong><br/>" +
                "• Costos totales: <strong>" + totalCostos.ToString("C") + "</strong><br/>" +
                "• 💰 <strong>Ganancias totales: " + totalGanancias.ToString("C") + "</strong><br/>" +
                "• 📈 <strong>Margen de ganancia: " + porcentajeGeneral.ToString("N1") + "%</strong><br/><br/>" +
                "🏆 <strong>Destacados:</strong><br/>" +
                "• Categoría más rentable: <strong>" + (categoriaTopGanancia != null ? categoriaTopGanancia.Categoria : "N/A") + "</strong> (" + (categoriaTopGanancia != null ? categoriaTopGanancia.GananciaTotal.ToString("C") : "$0") + ")<br/>" +
                "• Categoría con más ventas: <strong>" + (categoriaTopVentas != null ? categoriaTopVentas.Categoria : "N/A") + "</strong> (" + (categoriaTopVentas != null ? categoriaTopVentas.CantidadVentas.ToString("N0") : "0") + " ventas)";

            lblEstadisticas.Text = estadisticas;
            lblEstadisticas.ForeColor = System.Drawing.Color.FromName("#00ffc8");
        }
        catch (Exception ex)
        {
            lblEstadisticas.Text = "📊 " + tipoReporte + "<br/>Error al generar estadísticas: " + ex.Message;
            lblEstadisticas.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void MostrarError(string mensaje)
    {
        // Limpiar el grid
        gvReporteGanancias.DataSource = null;
        gvReporteGanancias.DataBind();
        
        // Mostrar mensaje de error
        lblEstadisticas.Text = "❌ " + mensaje;
        lblEstadisticas.ForeColor = System.Drawing.Color.Red;
        panelEstadisticas.Visible = true;
    }
}