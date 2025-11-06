using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

namespace Practica_2doParcial
{
    public partial class Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Append2Professors();
            ApplySalayPerTwoWithXsl();
        }

        private void Append2Professors()
        {
            XmlDocument miDoc = new XmlDocument();
            using (XmlTextReader miLector = new XmlTextReader(Server.MapPath("Profesores.xml")))
            {
                miLector.WhitespaceHandling = WhitespaceHandling.None;
                miDoc.Load(miLector);
            }

            XmlNode raiz = miDoc.DocumentElement;

            List<Profesor> listaProfesores = new List<Profesor>()
            {
                new Profesor() { Apellido = "Gomez", Sueldo = 1_500_000, Condicion = "Titular" },
                new Profesor() { Apellido = "Fernandez", Sueldo = 1_000_000, Condicion = "Adjunto" }
            };

            // Agregar cada profesor
            foreach (var profesor in listaProfesores)
            {
                XmlElement nodoProfesor = miDoc.CreateElement("Profesor");

                XmlElement nodoApellido = miDoc.CreateElement("Apellido");
                nodoApellido.InnerText = profesor.Apellido;

                XmlElement nodoSueldo = miDoc.CreateElement("Sueldo");
                nodoSueldo.InnerText = profesor.Sueldo.ToString();

                XmlElement nodoCondicion = miDoc.CreateElement("Condicion");
                nodoCondicion.InnerText = profesor.Condicion;

                nodoProfesor.AppendChild(nodoApellido);
                nodoProfesor.AppendChild(nodoSueldo);
                nodoProfesor.AppendChild(nodoCondicion);

                raiz.AppendChild(nodoProfesor);
            }

            // Guardar el XML actualizado
            miDoc.Save(Server.MapPath("Profesores.xml"));
        }

        private void ApplySalayPerTwoWithXsl()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("Profesores.xml"));

            // Cargar el XSLT
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(Server.MapPath("DuplicarSueldoTitulares.xslt"));

            // Crear un StringWriter para capturar el resultado
            StringWriter sw = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(sw, xslt.OutputSettings))
            {
                // Aplicar la transformación
                xslt.Transform(xmlDoc, writer);
            }

            // Cargar el resultado transformado
            XmlDocument xmlTransformado = new XmlDocument();
            xmlTransformado.LoadXml(sw.ToString());

            // Guardar el resultado (o trabajar con él)
            xmlTransformado.Save(Server.MapPath("ProfesoresTransformados.xml"));
        }
    }

    public class Profesor
    {
        public string Apellido { get; set; }
        public decimal Sueldo { get; set; }
        public string Condicion { get; set; }
    }
}