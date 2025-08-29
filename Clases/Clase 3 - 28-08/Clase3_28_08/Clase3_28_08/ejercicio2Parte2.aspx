<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ejercicio2Parte2.aspx.cs" Inherits="Clase3_28_08.ejercicio2Parte2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            a: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            b: <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Potencia" OnClick="Button1_Click"/>
            <asp:Button ID="Button2" runat="server" Text="Raiz" OnClick="Button2_Click"/>
            <asp:Label ID="lblResultado" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
