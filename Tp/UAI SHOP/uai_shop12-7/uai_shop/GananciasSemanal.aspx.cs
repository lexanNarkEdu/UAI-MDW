using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

public partial class GananciasSemanal : System.Web.UI.Page
{
    private ReportesBll reportesBll = new ReportesBll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
        }
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        CargarDatos();
    }

    private void CargarDatos()
    {
        try
        {
            // Obtener los datos del reporte
            List<BE.ReporteGanancias> reportes = reportesBll.ObtenerGananciasSemanal();

            // Vincular al GridView
            gvGanancias.DataSource = reportes;
            gvGanancias.DataBind();

            // Mostrar estadísticas
            string estadisticas = reportesBll.ObtenerEstadisticas(reportes);
            lblEstadisticas.Text = estadisticas + " (Últimos 7 días)";

            // Mostrar panel de estadísticas solo si hay datos
            panelEstadisticas.Visible = reportes.Any();
        }
        catch (Exception ex)
        {
            // En caso de error, mostrar mensaje en el label de estadísticas
            lblEstadisticas.Text = "Error al cargar los datos semanales: " + ex.Message;
            lblEstadisticas.ForeColor = System.Drawing.Color.Red;
            panelEstadisticas.Visible = true;
            
            // Limpiar el GridView
            gvGanancias.DataSource = null;
            gvGanancias.DataBind();
        }
    }
}