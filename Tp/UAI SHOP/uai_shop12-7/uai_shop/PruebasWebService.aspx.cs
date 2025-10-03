using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BE;
using BLL;
using Microsoft.Ajax.Utilities;
using ReportingWSReference;

public partial class PruebasWebService : System.Web.UI.Page
{
    private ReportingWebService webService;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Usuario usuariologeado = (Usuario)Session["Usuario"];
            if (usuariologeado != null)
            {
                habilitarMenusSegunRol(usuariologeado.Permiso.Nombre);
            }
            // Inicializar WebService
            webService = new ReportingWebService();
            
            // Mostrar URL del WSDL
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            lblWSDL.Text = baseUrl + "ReportingWebService.asmx?WSDL";
        }
    }

    protected void btnPing_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            string resultado = webService.Ping();
            MostrarResultado("✅ PING EXITOSO", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR EN PING", ex.Message, "error");
        }
    }



    protected void btnReporteV2PorPeriodo_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteV2PorPeriodo("ultimo_mes");
            
            string resultado = "📅 GANANCIAS ÚLTIMO MES - RESULTADOS:\n\n";
            resultado += "Total de categorías: " + reportes.Length + "\n\n";
            
            foreach (var reporte in reportes)
            {
                decimal porcentajeGanancia = reporte.VentaTotal > 0 ? (reporte.GananciaTotal / reporte.VentaTotal) * 100 : 0;
                resultado += "Categoría: " + reporte.Categoria + "\n";
                resultado += "  - Ventas: " + reporte.CantidadVentas + "\n";
                resultado += "  - Unidades: " + reporte.UnidadesVendidas + "\n";
                resultado += "  - Venta Total: $" + reporte.VentaTotal.ToString("N2") + "\n";
                resultado += "  - Ganancia Total: $" + reporte.GananciaTotal.ToString("N2") + "\n";
                resultado += "  - % Ganancia: " + porcentajeGanancia.ToString("N2") + "%\n\n";
            }
            
            MostrarResultado("✅ GANANCIAS ÚLTIMO MES OBTENIDAS", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER GANANCIAS ÚLTIMO MES", ex.Message, "error");
        }
    }

    protected void btnReporteV2Semanal_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteV2PorPeriodo("ultima_semana");
            
            string resultado = "⚡ GANANCIAS SEMANAL - RESULTADOS:\n\n";
            resultado += "Total de categorías: " + reportes.Length + "\n\n";
            
            foreach (var reporte in reportes)
            {
                decimal porcentajeGanancia = reporte.VentaTotal > 0 ? (reporte.GananciaTotal / reporte.VentaTotal) * 100 : 0;
                resultado += "Categoría: " + reporte.Categoria + "\n";
                resultado += "  - Ventas: " + reporte.CantidadVentas + "\n";
                resultado += "  - Unidades: " + reporte.UnidadesVendidas + "\n";
                resultado += "  - Venta Total: $" + reporte.VentaTotal.ToString("N2") + "\n";
                resultado += "  - Ganancia Total: $" + reporte.GananciaTotal.ToString("N2") + "\n";
                resultado += "  - % Ganancia: " + porcentajeGanancia.ToString("N2") + "%\n\n";
            }
            
            MostrarResultado("✅ GANANCIAS SEMANAL OBTENIDAS", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER GANANCIAS SEMANAL", ex.Message, "error");
        }
    }

    protected void btnCategoriaLider_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            string resumen = webService.GenerarResumenEjecutivoV2("ultimo_mes");
            
            string resultado = "🏆 RESUMEN EJECUTIVO:\n\n";
            resultado += resumen;
            
            MostrarResultado("✅ RESUMEN EJECUTIVO OBTENIDO", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER CATEGORÍA LÍDER", ex.Message, "error");
        }
    }

    #region Pruebas ReporteGananciasV2

    protected void btnReporteV2General_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteGananciasV2();
            
            string resultado = "🔥 REPORTE DINÁMICO V2 - TODOS LOS DATOS:\n\n";
            resultado += $"Total de categorías: {reportes.Length}\n\n";
            
            foreach (var reporte in reportes)
            {
                resultado += $"📂 {reporte.Categoria}\n";
                resultado += $"  • Ventas: {reporte.CantidadVentas} | Unidades: {reporte.UnidadesVendidas}\n";
                resultado += $"  • Facturación: ${reporte.VentaTotal:N2} | Costos: ${reporte.CostoTotal:N2}\n";
                resultado += $"  • 💎 Ganancia: ${reporte.GananciaTotal:N2} ({ReporteGananciasV2BLL.CalcularPorcentajeGanancia(reporte):N1}%)\n";
                resultado += $"  • Precio Prom: ${ReporteGananciasV2BLL.CalcularPrecioPromedio(reporte):N2} | Margen Unit: ${ReporteGananciasV2BLL.CalcularMargenUnitario(reporte):N2}\n\n";
            }
            
            MostrarResultado("✅ REPORTE V2 GENERAL OBTENIDO", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER REPORTE V2", ex.Message, "error");
        }
    }

    protected void btnReporteV2UltimoMes_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteV2PorPeriodo("ultimo_mes");
            
            string resultado = "📅 REPORTE V2 - ÚLTIMO MES (30 días):\n\n";
            resultado += $"Total de categorías: {reportes.Length}\n\n";
            
            foreach (var reporte in reportes)
            {
                resultado += $"📂 {reporte.Categoria}\n";
                resultado += $"  • Ventas último mes: {reporte.CantidadVentas}\n";
                resultado += $"  • Unidades vendidas: {reporte.UnidadesVendidas}\n";
                resultado += $"  • 💎 Ganancia: ${reporte.GananciaTotal:N2}\n";
                resultado += $"  • Margen: {ReporteGananciasV2BLL.CalcularPorcentajeGanancia(reporte):N1}%\n\n";
            }
            
            MostrarResultado("✅ REPORTE V2 ÚLTIMO MES OBTENIDO", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER REPORTE V2 ÚLTIMO MES", ex.Message, "error");
        }
    }

    protected void btnReporteV2Categoria_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            // Probar con categoría Hardware de Computacion (ID = 1)
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteV2PorCategoria(1);
            
            string resultado = "📂 REPORTE V2 - HARDWARE DE COMPUTACION:\n\n";
            
            if (reportes.Length > 0)
            {
                var reporte = reportes[0];
                resultado += $"Categoría: {reporte.Categoria}\n";
                resultado += $"Cantidad de Ventas: {reporte.CantidadVentas}\n";
                resultado += $"Unidades Vendidas: {reporte.UnidadesVendidas}\n";
                resultado += $"Venta Total: ${reporte.VentaTotal:N2}\n";
                resultado += $"Costo Total: ${reporte.CostoTotal:N2}\n";
                resultado += $"💎 Ganancia Total: ${reporte.GananciaTotal:N2}\n";
                resultado += $"📊 Porcentaje Ganancia: {ReporteGananciasV2BLL.CalcularPorcentajeGanancia(reporte):N2}%\n";
                resultado += $"Precio Promedio: ${ReporteGananciasV2BLL.CalcularPrecioPromedio(reporte):N2}\n";
                resultado += $"Costo Promedio: ${ReporteGananciasV2BLL.CalcularCostoPromedio(reporte):N2}\n";
                resultado += $"Margen Unitario: ${ReporteGananciasV2BLL.CalcularMargenUnitario(reporte):N2}";
            }
            else
            {
                resultado += "No se encontraron datos para esta categoría.";
            }
            
            MostrarResultado("✅ REPORTE V2 POR CATEGORÍA OBTENIDO", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER REPORTE V2 POR CATEGORÍA", ex.Message, "error");
        }
    }

    protected void btnReporteV2Filtros_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            // Probar con filtros: precios entre $1000 y $50000
            BE.ReporteGananciasV2[] reportes = webService.ObtenerReporteGananciasV2(
                null, null, 0, 1000, 50000, 0, 0);
            
            string resultado = "🎯 REPORTE V2 - CON FILTROS (Precios $1,000 - $50,000):\n\n";
            resultado += $"Total de categorías filtradas: {reportes.Length}\n\n";
            
            foreach (var reporte in reportes)
            {
                resultado += $"📂 {reporte.Categoria}\n";
                resultado += $"  • Ventas: {reporte.CantidadVentas} | Unidades: {reporte.UnidadesVendidas}\n";
                resultado += $"  • 💎 Ganancia: ${reporte.GananciaTotal:N2} ({ReporteGananciasV2BLL.CalcularPorcentajeGanancia(reporte):N1}%)\n";
                resultado += $"  • Precio Prom: ${ReporteGananciasV2BLL.CalcularPrecioPromedio(reporte):N2}\n\n";
            }
            
            MostrarResultado("✅ REPORTE V2 CON FILTROS OBTENIDO", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER REPORTE V2 CON FILTROS", ex.Message, "error");
        }
    }

    protected void btnEstadisticasV2_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            string estadisticas = webService.ObtenerEstadisticasV2("ultimo_mes");
            
            MostrarResultado("✅ ESTADÍSTICAS V2 OBTENIDAS", estadisticas, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER ESTADÍSTICAS V2", ex.Message, "error");
        }
    }

    protected void btnResumenEjecutivoV2_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            string resumen = webService.GenerarResumenEjecutivoV2("ultimo_mes");
            
            MostrarResultado("✅ RESUMEN EJECUTIVO V2 GENERADO", resumen, "info");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL GENERAR RESUMEN EJECUTIVO V2", ex.Message, "error");
        }
    }

    #endregion

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        lblResultado.Text = "Haga clic en un botón para ejecutar una prueba del WebService...";
        lblResultado.CssClass = "";
    }

    private void MostrarResultado(string titulo, string mensaje, string tipo)
    {
        lblResultado.Text = $"{titulo}\n{new string('=', titulo.Length)}\n\n{mensaje}";
        lblResultado.CssClass = tipo;
    }

    private void habilitarMenusSegunRol(String permiso)
    {

        //por defecto muestro SOLAMENTE los menus como si fuera un comprador
        //esto para que en caso de cualquier error no contemplado, no habilite todo por defecto

        HtmlGenericControl menuAdmin = (HtmlGenericControl)Master.FindControl("liAdmin");
        menuAdmin.Visible = false;
        HtmlGenericControl menuCategorias = (HtmlGenericControl)Master.FindControl("liCategorias");
        menuCategorias.Visible = true;
        HtmlGenericControl menuFacturasYPagos = (HtmlGenericControl)Master.FindControl("liFacturas");
        menuFacturasYPagos.Visible = true;
        HtmlGenericControl menuCarrito = (HtmlGenericControl)Master.FindControl("liCarrito");
        menuCarrito.Visible = true;
        HtmlGenericControl menuAbout = (HtmlGenericControl)Master.FindControl("liAbout");
        menuAbout.Visible = true;
        HtmlGenericControl menuReporte = (HtmlGenericControl)Master.FindControl("liReportes");
        menuReporte.Visible = false;

        if (!permiso.IsNullOrWhiteSpace())
        {
            if (permiso.ToLower().Equals("webmaster"))
            {
                menuAdmin.Visible = true;
                menuCategorias.Visible = false;
                menuFacturasYPagos.Visible = false;
                menuCarrito.Visible = false;
                menuAbout.Visible = false;
                menuReporte.Visible = true;

            }
            else if (permiso.ToLower().Equals("admin"))
            {
                menuAdmin.Visible = false;
                menuCategorias.Visible = true;
                menuFacturasYPagos.Visible = true;
                menuCarrito.Visible = false;
                menuAbout.Visible = true;
                menuReporte.Visible = true;
            }
        }
    }
}