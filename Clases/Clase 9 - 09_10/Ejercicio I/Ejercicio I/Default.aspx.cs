using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

namespace Ejercicio1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ocultar el resultado inicialmente
                //divResultado.Visible = false;
                btnTransformar_Click(sender, e);
            }
        }

        protected void btnTransformar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener la plantilla XSLT seleccionada del DropDownList
                string plantillaSeleccionada = ddlPlantillas.SelectedValue;

                // Aplicar la transformación con la plantilla seleccionada
                TransformacionXSLT.DocumentSource = "~/XMLFILE.XML";
                TransformacionXSLT.TransformSource = plantillaSeleccionada;

                // Mostrar el contenedor de resultados
                divResultado.Visible = true;

                // Mostrar mensaje de éxito con el nombre de la plantilla
                string nombrePlantilla = ddlPlantillas.SelectedItem.Text;
                //Response.Write($"<script>alert('Transformación aplicada con: {nombrePlantilla}');</script>");
            }
            catch (Exception ex)
            {
                // Manejar errores
                divResultado.Visible = false;
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }
    }
}