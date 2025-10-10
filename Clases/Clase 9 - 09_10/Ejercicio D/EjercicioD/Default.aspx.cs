using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;

namespace EjercicioD
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar datos automáticamente la primera vez
                //CargarCuentasBarcelona();
            }
        }
        protected void Btn_Barcelona_Click(object sender, EventArgs e)
        {
            CargarCuentasPorSucursal("Barcelona");
        }

        protected void Btn_Todas_Cuentas_Click(object sender, EventArgs e)
        {
            CargarTodasLasCuentas();
        }

        protected void Btn_saldo_Minimo_Cuentas_Click(object sender, EventArgs e)
        {
            CargarCuentasPorSaldoMinimo(1000);
        }
        // MÉTODOS ADICIONALES ÚTILES

        // Método para obtener todas las cuentas con su información completa
        private void CargarTodasLasCuentas()
        {
            try
            {
                ListBox1.Items.Clear();

                XPathDocument XPathDocum = new XPathDocument(Server.MapPath("Banco.xml"));
                XPathNavigator XNavegador = XPathDocum.CreateNavigator();

                // Seleccionar todas las cuentas
                XPathNodeIterator Iterador = XNavegador.Select("Banco/Cuenta");

                while (Iterador.MoveNext())
                {
                    XPathNavigator cuentaNav = Iterador.Current;

                    string numero = cuentaNav.SelectSingleNode("CuentaNum").Value;
                    string nombre = cuentaNav.SelectSingleNode("CuentaNombre").Value;
                    string saldo = cuentaNav.SelectSingleNode("CuentaSaldo").Value;
                    string sucursal = cuentaNav.SelectSingleNode("CuentaSucursal").Value;

                    string info = $"{nombre} - {sucursal} - Saldo: {saldo}€";
                    ListBox1.Items.Add(info);
                }
            }
            catch (Exception ex)
            {
                ListBox1.Items.Add("Error: " + ex.Message);
            }
        }

        // Método para buscar cuentas por sucursal específica
        private void CargarCuentasPorSucursal(string sucursal)
        {
            try
            {
                ListBox1.Items.Clear();

                XPathDocument XPathDocum = new XPathDocument(Server.MapPath("Banco.xml"));
                XPathNavigator XNavegador = XPathDocum.CreateNavigator();

                // Consulta XPath dinámica
                string xpath = $"Banco/Cuenta[CuentaSucursal='{sucursal}']/CuentaNombre";
                XPathNodeIterator Iterador = XNavegador.Select(xpath);

                while (Iterador.MoveNext())
                {
                    ListBox1.Items.Add(Iterador.Current.Value);
                }

                if (ListBox1.Items.Count == 0)
                {
                    ListBox1.Items.Add($"No se encontraron cuentas en {sucursal}");
                }
            }
            catch (Exception ex)
            {
                ListBox1.Items.Add("Error: " + ex.Message);
            }
        }

        // Método para obtener cuentas con saldo mayor a cierto valor
        private void CargarCuentasPorSaldoMinimo(decimal saldoMinimo)
        {
            try
            {
                ListBox1.Items.Clear();

                XPathDocument XPathDocum = new XPathDocument(Server.MapPath("Banco.xml"));
                XPathNavigator XNavegador = XPathDocum.CreateNavigator();

                XPathNodeIterator Iterador = XNavegador.Select("Banco/Cuenta");

                while (Iterador.MoveNext())
                {
                    XPathNavigator cuentaNav = Iterador.Current;

                    string saldoStr = cuentaNav.SelectSingleNode("CuentaSaldo").Value;
                    // Convertir el saldo (reemplazar coma por punto para decimal)
                    decimal saldo = decimal.Parse(saldoStr.Replace(",", "."));

                    Response.Write(saldoStr + " | " + saldoMinimo.ToString());

                    if (saldo > saldoMinimo)
                    {
                        string nombre = cuentaNav.SelectSingleNode("CuentaNombre").Value;
                        string info = $"{nombre} - Saldo: {saldoStr}€";
                        ListBox1.Items.Add(info);
                    }
                }

                if (ListBox1.Items.Count == 0)
                {
                    ListBox1.Items.Add($"No se encontraron cuentas con saldo mayor a {saldoMinimo}€");
                }
            }
            catch (Exception ex)
            {
                ListBox1.Items.Add("Error: " + ex.Message);
            }
        }

        // Método para contar cuentas por sucursal
        private Dictionary<string, int> ContarCuentasPorSucursal()
        {
            Dictionary<string, int> conteo = new Dictionary<string, int>();

            try
            {
                XPathDocument XPathDocum = new XPathDocument(Server.MapPath("Banco.xml"));
                XPathNavigator XNavegador = XPathDocum.CreateNavigator();

                XPathNodeIterator Iterador = XNavegador.Select("Banco/Cuenta/CuentaSucursal");

                while (Iterador.MoveNext())
                {
                    string sucursal = Iterador.Current.Value;

                    if (conteo.ContainsKey(sucursal))
                        conteo[sucursal]++;
                    else
                        conteo[sucursal] = 1;
                }
            }
            catch (Exception ex)
            {
                // Manejar error
            }

            return conteo;
        }

    }
}