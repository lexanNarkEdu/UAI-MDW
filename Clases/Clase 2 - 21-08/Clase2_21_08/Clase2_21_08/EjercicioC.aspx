<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EjercicioC.aspx.cs" Inherits="Clase2_21_08.EjercicioC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3>DOM: Elija un producto</h3>
            <br />
            <div>
                <label>Producto: </label>
                <asp:DropDownList ID="ddlProducto" runat="server"></asp:DropDownList>
                <label style="margin-left:20px">Cantidad: </label>
                <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
            </div>
            <br />
            
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
            <div>
                <label>Precio: </label>
                <asp:Label ID="lblPrecio" runat="server"></asp:Label> pta
            </div>
            <div>
                <label>Total: </label>
                <asp:Label ID="lblTotal" runat="server"></asp:Label> pta
            </div>
        </div>
    </form>
</body>
</html>
