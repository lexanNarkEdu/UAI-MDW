using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BE;
using BE.Servicios;
using BLL.Servicios;
using uai_shop;
using Microsoft.Ajax.Utilities;
using ReportingWSReference;

public partial class ReporteGanancias : System.Web.UI.Page
{
    //private ReportingWebService webService = new ReportingWebService();
    private ReportingWS webService = new ReportingWS();
    private BLL.ProductoBll productoBll = new BLL.ProductoBll();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Usuario usuarioLogueado = (Usuario)Session["Usuario"];
            // Verificar autorización usando el sistema existente
            if (!AutorizacionHelper.PuedeAccederAPagina(usuarioLogueado, "ReporteGanancias.aspx"))
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (usuarioLogueado != null)
            {
                habilitarMenusSegunRol(usuarioLogueado.Permiso.Nombre);
            }

            // Registrar auditoría de acceso a la página
            RegistrarAuditoriaAcceso();

            if (!IsPostBack)
            {
                // Cargar categorías en el dropdown
                CargarCategorias();
                // Cargar reporte general por defecto
                CargarReporteGeneral();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx");
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
        // Mostrar loading state
        btnBuscarFiltros.Text = "⏳ Buscando...";
        btnBuscarFiltros.Enabled = false;
        try
        {
            CargarReporteConFiltros();
        }
        finally
        {
            // Restaurar estado del botón
            btnBuscarFiltros.Text = "🔍 Buscar";
            btnBuscarFiltros.Enabled = true;
        }
    }

    private void CargarReporteGeneral()
    {
        try
        {
            // Obtener todos los reportes usando WebService
            ReportingWSReference.ReporteGananciasV2[] reportes = webService.ObtenerReporteGananciasV2("", "", 0, 0, 0, 0, 0);

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
            ReportingWSReference.ReporteGananciasV2[] reportes = webService.ObtenerReporteV2PorPeriodo("ultimo_mes");

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
            ReportingWSReference.ReporteGananciasV2[] reportes = webService.ObtenerReporteV2PorPeriodo("ultimo_anio");

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
            decimal costoMin = 0, costoMax = 0;

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
            int idCategoria = 0;
            if (ddlCategoria.SelectedValue != "" && int.TryParse(ddlCategoria.SelectedValue, out idCategoria))
            {
                filtros.IDCategoria = idCategoria;
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

            // Aplicar filtros de ventas (monto)
            decimal ventasMin;
            if (!string.IsNullOrEmpty(txtVentasMin.Text) && decimal.TryParse(txtVentasMin.Text, out ventasMin))
            {
                filtros.VentasMinimas = ventasMin;
            }
            decimal ventasMax;
            if (!string.IsNullOrEmpty(txtVentasMax.Text) && decimal.TryParse(txtVentasMax.Text, out ventasMax))
            {
                filtros.VentasMaximas = ventasMax;
            }

            // Obtener reporte con filtros usando WebService
            string fechaDesdeStr = filtros.FechaDesde != null ? filtros.FechaDesde.Value.ToString("yyyy-MM-dd") : "";
            string fechaHastaStr = filtros.FechaHasta != null ? filtros.FechaHasta.Value.ToString("yyyy-MM-dd") : "";
            costoMin = filtros.CostoMinimo ?? 0;
            costoMax = filtros.CostoMaximo ?? 0;

            decimal ventasMinParam = filtros.VentasMinimas ?? 0;
            decimal ventasMaxParam = filtros.VentasMaximas ?? 0;

            ReportingWSReference.ReporteGananciasV2[] reportes = webService.ObtenerReporteGananciasV2(
                fechaDesdeStr, fechaHastaStr, idCategoria, costoMin, costoMax, ventasMinParam, ventasMaxParam);

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

    private void MostrarResultados(ReportingWSReference.ReporteGananciasV2[] reportes, string tipoReporte)
    {
        if (reportes != null && reportes.Length > 0)
        {
            // Vincular datos al GridView
            gvReporteGanancias.DataSource = reportes;
            gvReporteGanancias.DataBind();

            // Mostrar feedback de resultados
            lblResultados.Text = string.Format("✅ {0} resultado{1} encontrado{1}",
                reportes.Length, reportes.Length == 1 ? "" : "s");
            lblResultados.ForeColor = System.Drawing.Color.LightGreen;
            lblResultados.Visible = true;

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
            // Mostrar feedback de sin resultados
            lblResultados.Text = "⚠️ No se encontraron resultados";
            lblResultados.ForeColor = System.Drawing.Color.Orange;
            lblResultados.Visible = true;
            // No mostrar KPIs cuando no hay datos
            panelEstadisticas.Visible = false;
        }
    }

    private void GenerarEstadisticas(ReportingWSReference.ReporteGananciasV2[] reportes, string tipoReporte)
    {
        if (reportes == null || reportes.Length == 0)
            return;

        try
        {
            // Calcular estadísticas generales
            int totalVentas = reportes.Sum(r => r.CantidadVentas);
            decimal totalVentasMonto = reportes.Sum(r => r.VentaTotal);
            decimal totalGanancias = reportes.Sum(r => r.GananciaTotal);

            // Calcular porcentaje de ganancia general
            decimal porcentajeGeneral = totalVentasMonto > 0
                ? (totalGanancias / totalVentasMonto) * 100
                : 0;

            // Encontrar la categoría más rentable
            var categoriaTopGanancia = reportes
                .OrderByDescending(r => r.GananciaTotal)
                .FirstOrDefault();

            // Poblar KPIs
            lblGananciaTotal.Text = totalGanancias.ToString("C");
            lblMargenPromedio.Text = porcentajeGeneral.ToString("N1") + "%";
            lblTotalVentas.Text = totalVentas.ToString("N0");
            lblCategoriaTop.Text = categoriaTopGanancia != null ? categoriaTopGanancia.Categoria : "N/A";
        }
        catch (Exception ex)
        {
            // En caso de error, mostrar valores por defecto
            lblGananciaTotal.Text = "Error";
            lblMargenPromedio.Text = "N/A";
            lblTotalVentas.Text = "0";
            lblCategoriaTop.Text = "Error";
        }
    }

    private void MostrarError(string mensaje)
    {
        // Limpiar el grid
        gvReporteGanancias.DataSource = null;
        gvReporteGanancias.DataBind();
        // Mostrar mensaje de error en resultados
        lblResultados.Text = "❌ " + mensaje;
        lblResultados.ForeColor = System.Drawing.Color.Red;
        lblResultados.Visible = true;
        // No mostrar KPIs cuando hay error
        panelEstadisticas.Visible = false;
    }

    private void CargarCategorias()
    {
        try
        {
            var categorias = productoBll.ObtenerCategorias();

            // Limpiar dropdown
            ddlCategoria.Items.Clear();

            // Agregar opción por defecto
            ddlCategoria.Items.Add(new ListItem("Todas las categorías", ""));

            // Agregar categorías
            foreach (var categoria in categorias)
            {
                ddlCategoria.Items.Add(new ListItem(categoria.Nombre, categoria.IDCategoria.ToString()));
            }
        }
        catch (Exception ex)
        {
            // Log del error pero no mostrar al usuario ya que es carga inicial
            System.Diagnostics.Debug.WriteLine("Error al cargar categorías: " + ex.Message);
        }
    }

    /// <summary>
    /// Registra en bitácora el acceso a la página de reportes
    /// </summary>
    private void RegistrarAuditoriaAcceso()
    {
        try
        {
            Usuario usuarioLogueado = (Usuario)Session["Usuario"];

            if (usuarioLogueado != null)
            {
                BitacoraBL.RegistrarBitacora(TipoEvento.Message, usuarioLogueado,
                    "Acceso a página de Reporte de Ganancias");
            }
        }
        catch (Exception ex)
        {
            // No afectar la funcionalidad si falla la auditoría
            System.Diagnostics.Debug.WriteLine("Error al registrar auditoría de acceso: " + ex.Message);
        }
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