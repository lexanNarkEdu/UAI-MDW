<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EjercicioC.aspx.cs" Inherits="EjercicioC" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estrategia</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Producto: </label>
            <asp:DropDownList ID="ddlLibros" runat="server"></asp:DropDownList>
            <br /><br />
            
            <label style="margin-left:20px">Cantidad: </label>
            <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
            <br /><br />
            
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
            <br /><br />
            
            <div>
                <label>Precio: </label>
                <asp:Label ID="lblPrecio" runat="server"></asp:Label>
            </div>
            <br />
            
            <div>
                <label>Total: </label>
                <asp:Label ID="lblTotal" runat="server"></asp:Label>
            </div>
        </div>
        <br /><br />
                            <asp:Button ID="menuPrincipal" runat="server" Text="Volver MenuPrincipal" 
                Width="285px" OnClick="menuPrincipal_Click" />
    </form>
</body>
</html>
