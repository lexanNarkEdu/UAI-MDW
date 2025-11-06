using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace Practica_2doParcial
{
    public partial class serverV2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Append2Professors();
            ApplySalayPerTwoWithXslt();
        }

        private void Append2Professors()
        {
            // Cargar el XML
            XDocument doc = XDocument.Load(Server.MapPath("Profesores.xml"));

            // Lista de profesores
            List<Profesor> listaProfesores = new List<Profesor>()
            {
                new Profesor() { Apellido = "Gomez", Sueldo = 1_500_000, Condicion = "Titular" },
                new Profesor() { Apellido = "Fernandez", Sueldo = 1_000_000, Condicion = "Adjunto" }
            };

            // Agregar profesores de forma simple
            foreach (var p in listaProfesores)
            {
                doc.Root.Add(
                    new XElement("Profesor",
                        new XElement("Apellido", p.Apellido),
                        new XElement("Sueldo", p.Sueldo),
                        new XElement("Condicion", p.Condicion)
                    )
                );
            }

            // Guardar
            doc.Save(Server.MapPath("Profesores.xml"));
        }

        public void ApplySalayPerTwoWithXslt() 
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(Server.MapPath("DuplicarSueldoTitulares.xslt"));

            xslt.Transform(
                Server.MapPath("Profesores.xml"),
                Server.MapPath("ProfesoresTransformados.xml")
            );
        }
    }
}