<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EjercicioD._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container">
        <h2>XpathDocument - Cuentas en Barcelona</h2>

        <p><strong>Cuentas en Barcelona:</strong></p>

        <asp:ListBox ID="ListBox1" runat="server" Width="100%" Rows="6" ></asp:ListBox>

        <br/><br/><br/>

        <asp:Button ID="Button1" runat="server" Text="Actualizar Cuentas Barcelona"
            Font-Bold="True" OnClick="Btn_Barcelona_Click" />
         <asp:Button ID="Button2" runat="server" Text="Actualizar todas las Cuentas"
            Font-Bold="True" OnClick="Btn_Todas_Cuentas_Click" />
         <asp:Button ID="Button3" runat="server" Text="Actualizar con cuentas saldo minimo "
            Font-Bold="True" OnClick="Btn_saldo_Minimo_Cuentas_Click" />
    </div>


</asp:Content>
