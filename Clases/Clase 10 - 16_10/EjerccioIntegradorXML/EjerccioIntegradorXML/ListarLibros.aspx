<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListarLibros.aspx.cs" Inherits="ListarLibros" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Filtrar Libros</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="litLibros" runat="server"></asp:Literal>
        </div>
        <br /><br /><br />
                    <asp:Button ID="menuPrincipal" runat="server" Text="Volver MenuPrincipal" 
                Width="285px" OnClick="menuPrincipal_Click" />
    </form>
</body>
</html>

