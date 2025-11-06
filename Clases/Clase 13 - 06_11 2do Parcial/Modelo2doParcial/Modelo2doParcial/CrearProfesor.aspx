<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrearProfesor.aspx.cs" Inherits="CrearProfesor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <asp:Label runat="server" Text="Apellido"></asp:Label>
           <asp:TextBox runat="server" ID="apellido"></asp:TextBox>
           <br /><br />            
           <asp:Label runat="server" Text="Sueldo"></asp:Label>
           <asp:TextBox runat="server" ID="sueldo" TextMode="Number"></asp:TextBox>
           <br /><br /> 
            <asp:DropDownList runat="server" ID="ddlCondicion">
                <asp:ListItem Text="Titular" Value="titular" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Adjunto" Value="adjunto"></asp:ListItem>
            </asp:DropDownList>
            <br /><br />
            <br /><br />
            <asp:Button runat="server" ID="crearProfesor" Text="Crear" OnClick="crearProfesor_Click" />
            <br /><br />
            <asp:Button runat="server" ID="VolverMenu" Text="Volver a Listar" OnClick="VolverMenu_Click" />
        </div>
    </form>
</body>
</html>
