using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;

public partial class ClienteSaldo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Cargar el documento XML
            XPathDocument xpathDocument = new XPathDocument(Server.MapPath("Banco.xml"));
            XPathNavigator xnavegador;
            XPathNodeIterator iterador;

            xnavegador = xpathDocument.CreateNavigator();

            // Cargar números de cuenta en el primer DropDownList
            iterador = xnavegador.Select("//Banco/Cuenta/@CuentaNum");
            while (iterador.MoveNext())
            {
                DropDownList1.Items.Add(iterador.Current.Value);
            }

            // Cargar nombres de cuenta en el segundo DropDownList
            iterador = xnavegador.Select("//Banco/Cuenta/CuentaNombre");
            while (iterador.MoveNext())
            {
                DropDownList2.Items.Add(iterador.Current.Value);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // Valores elegidos por el cliente
        string cuentaNum = DropDownList1.SelectedItem.Text.Trim();
        string cuentaNombre = DropDownList2.SelectedItem.Text.Trim();

        // Cargar el documento XML nuevamente
        XPathDocument xpathDocument = new XPathDocument(Server.MapPath("Banco.xml"));
        XPathNavigator xnavegador;
        XPathNodeIterator iterador;

        xnavegador = xpathDocument.CreateNavigator();

        // Buscar el saldo de la cuenta seleccionada por número de cuenta
        iterador = xnavegador.Select($"//Banco/Cuenta[@CuentaNum='{cuentaNum}']/CuentaSaldo");

        if (iterador.MoveNext())
        {
            // Formatear el saldo como moneda
            decimal saldo = Convert.ToDecimal(iterador.Current.Value);
            TextBox1.Text = string.Format("{0:C}", saldo);
        }
        else
        {
            TextBox1.Text = "Cuenta no encontrada";
        }
    }

    protected void MostrarNombre(object sender, EventArgs e)
    {
        // Valores elegidos por el cliente
        string cuentaNomb = DropDownList1.SelectedItem.Text.Trim();
        string cuentaNomB = DropDownList2.SelectedItem.Text.Trim();

        // Cargar el documento XML
        XPathDocument xpathDocument = new XPathDocument(Server.MapPath("Banco.xml"));
        XPathNavigator xnavegador;
        XPathNodeIterator iterador;

        xnavegador = xpathDocument.CreateNavigator();

        // Buscar el saldo por el nombre de la cuenta
        iterador = xnavegador.Select($"//Banco/Cuenta[CuentaNombre='{cuentaNomb}']/CuentaSaldo");

        if (iterador.MoveNext())
        {
            // Formatear el saldo como moneda
            decimal saldo = Convert.ToDecimal(iterador.Current.Value);
            TextBox1.Text = string.Format("{0:C}", saldo);
        }
        else
        {
            TextBox1.Text = "Cuenta no encontrada";
        }
    }
}