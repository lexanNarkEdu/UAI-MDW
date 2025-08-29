<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ejercicio2.aspx.cs" Inherits="Clase3_28_08.ejercicio2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Sumar
        <div>
            a: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            b: <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Sumar" OnClick="Button1_Click"/>
            <asp:Button ID="Button2" runat="server" Text="Restar" OnClick="Button2_Click"/>
            <asp:Button ID="Button3" runat="server" Text="Multiplicar" OnClick="Button3_Click"/>
            <asp:Button ID="Button4" runat="server" Text="Dividir" OnClick="Button4_Click"/>
            <asp:Label ID="lblResultado" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
