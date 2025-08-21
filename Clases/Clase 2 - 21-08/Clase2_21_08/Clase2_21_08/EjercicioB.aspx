<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EjercicioB.aspx.cs" Inherits="Clase2_21_08.EjercicioB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                <strong>
                    <font size="4">ESCRIBIR DATOS EN UN DOCUMENTO XML</font>
                </strong>
            </p>
            
            <hr width="100%" noshade size="3" />
            <p>
                <asp:Label ID="Label" runat="server" Font-Bold="true" style="margin-right:10px">Nombre: </asp:Label>
                <asp:TextBox ID="NombreTxt" runat="server" Width="100px"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="Label1" runat="server" Font-Bold="true" style="margin-right:10px">Apellido: </asp:Label>
                <asp:TextBox ID="ApellidoTxt" runat="server" Width="100px"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="Label2" runat="server" Font-Bold="true" style="margin-right:10px">Direccion: </asp:Label>
                <asp:TextBox ID="DireccionTxt" runat="server" Width="100px"></asp:TextBox>
            </p>
            <p>
                <asp:Button ID="Button1" OnClick="Button1_Click" Text="Crear Documento" runat="server"
                    Font-Bold="true" Font-Italic="true" Font-Names="Microsoft Sans Serif" />
            </p>
        </div>
    </form>
</body>
</html>
