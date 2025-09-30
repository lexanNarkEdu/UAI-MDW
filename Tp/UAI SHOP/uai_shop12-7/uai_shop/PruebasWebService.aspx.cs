using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;

public partial class PruebasWebService : System.Web.UI.Page
{
    private ReportingWebService webService;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

    protected void btnGananciasGeneral_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            ReporteGanancias[] reportes = webService.ObtenerGananciasGeneral();
            
            string resultado = "📊 GANANCIAS GENERAL - RESULTADOS:\n\n";
            resultado += "Total de categorías: " + reportes.Length + "\n\n";
            
            foreach (var reporte in reportes)
            {
                resultado += $"Categoría: {reporte.Categoria}\n";
                resultado += $"  - Ventas: {reporte.VentasConEstaCategoria}\n";
                resultado += $"  - Unidades: {reporte.UnidadesTotales}\n";
                resultado += $"  - Precio Promedio: ${reporte.PrecioPromedio:N2}\n";
                resultado += $"  - Ganancia Total: ${reporte.GananciaTotal:N2}\n\n";
            }
            
            MostrarResultado("✅ GANANCIAS GENERAL OBTENIDAS", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER GANANCIAS GENERAL", ex.Message, "error");
        }
    }

    protected void btnGananciasUltimoMes_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            ReporteGanancias[] reportes = webService.ObtenerGananciasUltimoMes();
            
            string resultado = "📅 GANANCIAS ÚLTIMO MES - RESULTADOS:\n\n";
            resultado += "Total de categorías: " + reportes.Length + "\n\n";
            
            foreach (var reporte in reportes)
            {
                resultado += $"Categoría: {reporte.Categoria}\n";
                resultado += $"  - Ventas (30 días): {reporte.VentasConEstaCategoria}\n";
                resultado += $"  - Unidades: {reporte.UnidadesTotales}\n";
                resultado += $"  - Precio Promedio: ${reporte.PrecioPromedio:N2}\n";
                resultado += $"  - Ganancia Total: ${reporte.GananciaTotal:N2}\n\n";
            }
            
            MostrarResultado("✅ GANANCIAS ÚLTIMO MES OBTENIDAS", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER GANANCIAS ÚLTIMO MES", ex.Message, "error");
        }
    }

    protected void btnGananciasSemanal_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            ReporteGanancias[] reportes = webService.ObtenerGananciasSemanal();
            
            string resultado = "⚡ GANANCIAS SEMANAL - RESULTADOS:\n\n";
            resultado += "Total de categorías: " + reportes.Length + "\n\n";
            
            foreach (var reporte in reportes)
            {
                resultado += $"Categoría: {reporte.Categoria}\n";
                resultado += $"  - Ventas (7 días): {reporte.VentasConEstaCategoria}\n";
                resultado += $"  - Unidades: {reporte.UnidadesTotales}\n";
                resultado += $"  - Precio Promedio: ${reporte.PrecioPromedio:N2}\n";
                resultado += $"  - Ganancia Total: ${reporte.GananciaTotal:N2}\n\n";
            }
            
            MostrarResultado("✅ GANANCIAS SEMANAL OBTENIDAS", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER GANANCIAS SEMANAL", ex.Message, "error");
        }
    }

    protected void btnEstadisticasGeneral_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            string estadisticas = webService.ObtenerEstadisticasGanancias("general");
            
            string resultado = "📈 ESTADÍSTICAS GENERAL:\n\n" + estadisticas;
            
            MostrarResultado("✅ ESTADÍSTICAS OBTENIDAS", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER ESTADÍSTICAS", ex.Message, "error");
        }
    }

    protected void btnCategoriaLider_Click(object sender, EventArgs e)
    {
        try
        {
            webService = new ReportingWebService();
            ReporteGanancias lider = webService.ObtenerCategoriaLider("general");
            
            string resultado = "🏆 CATEGORÍA LÍDER:\n\n";
            resultado += $"Categoría: {lider.Categoria}\n";
            resultado += $"Ventas: {lider.VentasConEstaCategoria}\n";
            resultado += $"Unidades: {lider.UnidadesTotales}\n";
            resultado += $"Precio Promedio: ${lider.PrecioPromedio:N2}\n";
            resultado += $"Ganancia Total: ${lider.GananciaTotal:N2}";
            
            MostrarResultado("✅ CATEGORÍA LÍDER OBTENIDA", resultado, "success");
        }
        catch (Exception ex)
        {
            MostrarResultado("❌ ERROR AL OBTENER CATEGORÍA LÍDER", ex.Message, "error");
        }
    }

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
}