<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuPrincipal.aspx.cs" Inherits="MenuPrincipal" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menú Principal</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Cargar datos en el archivo XML" 
                Width="291px" OnClick="Button1_Click" />
            <br /><br /><br />
            
            <asp:Button ID="Button2" runat="server" Text="Establecer una estrategia" 
                Width="287px" OnClick="Button2_Click" />
            <br /><br /><br />
            
            <asp:Button ID="Button3" runat="server" Text="Filtrar libros mayores a un precio determinado" 
                Width="285px" OnClick="Button3_Click" />
        </div>
    </form>
</body>
</html>
