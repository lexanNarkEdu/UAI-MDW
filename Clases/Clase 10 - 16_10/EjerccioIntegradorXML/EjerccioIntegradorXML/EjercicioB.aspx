<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EjercicioB.aspx.cs" Inherits="EjercicioB" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Escribir XML</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label4" runat="server" Text="ID:"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtIdLibros" runat="server"></asp:TextBox>
            <br /><br />
            
            <asp:Label ID="Label1" runat="server" Text="Nombre" Font-Bold="true"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            <br /><br />
            
            <asp:Label ID="Label2" runat="server" Text="Autor" Font-Bold="True"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtAutor" runat="server"></asp:TextBox>
            <br /><br />
            
            <asp:Label ID="Label3" runat="server" Text="Editorial" Font-Bold="True"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtEditorial" runat="server"></asp:TextBox>
            <br /><br />
            
            <asp:Label ID="lblprecio" runat="server" Text="Precio" Font-Bold="True"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
            <br /><br />
            
            <asp:Button ID="btnEscribir" runat="server" Text="Escribir XML" Width="239px"
                OnClick="btnEscribir_Click" />
        </div>
    </form>
</body>
</html>
