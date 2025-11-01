using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.XPath;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
        }
    }

    private void CargarDatos()
    {
        // Cargar el documento XML usando XPathDocument
        XPathDocument xpathDoc = new XPathDocument(Server.MapPath("banco.xml"));
        XPathNavigator xnavegador = xpathDoc.CreateNavigator();

        // Limpiar los dropdowns
        DropDownList1.Items.Clear();
        DropDownList2.Items.Clear();

        // Agregar opción por defecto
        DropDownList1.Items.Add(new ListItem("-- Seleccione --", ""));
        DropDownList2.Items.Add(new ListItem("-- Seleccione --", ""));

        // Cargar números de cuenta en DropDownList1
        XPathNodeIterator iterador = xnavegador.Select("Banco/Cuenta/@CuentaNum");
        while (iterador.MoveNext())
        {
            DropDownList1.Items.Add(iterador.Current.Value);
        }

        // Cargar nombres de titulares en DropDownList2
        iterador = xnavegador.Select("Banco/Cuenta/CuentaNombre");
        while (iterador.MoveNext())
        {
            DropDownList2.Items.Add(iterador.Current.Value);
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cuentaNum = DropDownList1.SelectedValue;
        //string cuentaNumAnterior = "";

        //if (!string.IsNullOrEmpty(cuentaNum) && cuentaNum == cuentaNumAnterior)
            if (!string.IsNullOrEmpty(cuentaNum))
            {
            // Cargar datos usando XPathNavigator
            XPathDocument xpathDoc = new XPathDocument(Server.MapPath("banco.xml"));
            XPathNavigator xnavegador = xpathDoc.CreateNavigator();

            // Buscar la cuenta seleccionada por su número
            XPathNodeIterator iterador = xnavegador.Select($"Banco/Cuenta[@CuentaNum='{cuentaNum}']");

            //ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{iterador.Current}');", true);

            if (iterador.MoveNext())
            {
                XPathNavigator cuentaNav = iterador.Current;


                // Obtener el saldo
                string saldo = cuentaNav.SelectSingleNode("CuentaSaldo").Value;
                TextBox1.Text = saldo + " €";

                // Obtener el nombre del titular
                string nombreTitular = cuentaNav.SelectSingleNode("CuentaNombre").Value;

                // Sincronizar DropDownList2
                DropDownList2.SelectedValue = nombreTitular;

                // Mostrar el saldo en TextBox2 con formato "Activo:"
                TextBox2.Text = "Activo: " + saldo + " €";
            }
            //cuentaNumAnterior = cuentaNum;

        }
        else
        {
            LimpiarCampos();
        }
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string nombreTitular = DropDownList2.SelectedValue;

        if (!string.IsNullOrEmpty(nombreTitular) && nombreTitular != "-- Seleccione --")
        {
            // Cargar datos usando XPathNavigator
            XPathDocument xpathDoc = new XPathDocument(Server.MapPath("banco.xml"));
            XPathNavigator xnavegador = xpathDoc.CreateNavigator();

            // Buscar la cuenta por nombre del titular
            XPathNodeIterator iterador = xnavegador.Select($"Banco/Cuenta[CuentaNombre='{nombreTitular}']");

            if (iterador.MoveNext())
            {
                XPathNavigator cuentaNav = iterador.Current;

                // Obtener el número de cuenta
                string cuentaNum = cuentaNav.GetAttribute("CuentaNum", "");

                // Obtener el saldo
                string saldo = cuentaNav.SelectSingleNode("CuentaSaldo").Value;

                // Sincronizar DropDownList1
                DropDownList1.SelectedValue = cuentaNum;

                // Mostrar saldos
                TextBox1.Text = saldo + " €";
                TextBox2.Text = "Activo: " + saldo + " €";
            }
        }
        else
        {
            LimpiarCampos();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string cuentaNum = DropDownList1.SelectedValue;
        string nombreTitular = DropDownList2.SelectedValue;

        if (string.IsNullOrEmpty(cuentaNum) || cuentaNum == "-- Seleccione --")
        {
            // Si no hay cuenta seleccionada, mostrar mensaje
            TextBox1.Text = "Seleccione una cuenta";
            TextBox2.Text = "Activo: --";
            return;
        }

        // Cargar información completa usando XPathNavigator
        XPathDocument xpathDoc = new XPathDocument(Server.MapPath("banco.xml"));
        XPathNavigator xnavegador = xpathDoc.CreateNavigator();

        // Buscar la cuenta por número
        XPathNodeIterator iterador = xnavegador.Select($"Banco/Cuenta[@CuentaNum='{cuentaNum}']");

        if (iterador.MoveNext())
        {
            XPathNavigator cuentaNav = iterador.Current;

            // Obtener todos los datos
            string nombre = cuentaNav.SelectSingleNode("CuentaNombre").Value;
            string saldo = cuentaNav.SelectSingleNode("CuentaSaldo").Value;
            string sucursal = cuentaNav.SelectSingleNode("CuentaSucursal").Value;

            // Mostrar en los campos
            TextBox1.Text = saldo + " €";
            TextBox2.Text = "Activo: " + saldo + " €";

            // Asegurar sincronización
            DropDownList2.SelectedValue = nombre;

            // Opcional: Mostrar mensaje con información completa
            string mensaje = $"Cuenta: {cuentaNum} <br /> " +
                           $"Titular: {nombre} <br /> " +
                           $"Saldo: {saldo} € <br /> " +
                           $"Sucursal: {sucursal}";

            resultado.Text = mensaje;

            //ClientScript.RegisterStartupScript(this.GetType(), "alert",
            //    $"alert('{mensaje}');", true);
        }
    }

    private void LimpiarCampos()
    {
        TextBox1.Text = "";
        TextBox2.Text = "Activo:";
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
    }
}