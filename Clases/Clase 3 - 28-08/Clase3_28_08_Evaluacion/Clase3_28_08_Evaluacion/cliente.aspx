<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cliente.aspx.cs" Inherits="Clase3_28_08_Evaluacion.ejercicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: flex; flex-direction:column">
            <div>
                <asp:Label ID="Label1" runat="server" Text="LibreriaLosada"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="LibreriaHernandez"></asp:Label>
            </div>
            <div>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                    <asp:ListItem Group="Grupo1" Text="750" Value="750">Compiladores ($750)</asp:ListItem>
                    <asp:ListItem Group="Grupo1" Text="630" Value="630">Teoria de Numeros ($630)</asp:ListItem>
                    <asp:ListItem Group="Grupo1" Text="880" Value="880">Ajax ($880)</asp:ListItem>
                    <asp:ListItem Group="Grupo2" Text="500" Value="500">Ficciones ($500)</asp:ListItem>
                    <asp:ListItem Group="Grupo2" Text="400" Value="400">Metamorfosis ($400)</asp:ListItem>
                    <asp:ListItem Group="Grupo2" Text="900" Value="900">Semiotica ($900)</asp:ListItem>
                </asp:CheckBoxList>
            </div>
            <asp:Button ID="Button1" runat="server" Text="Calcular importe" OnClick="Button1_Click"/>
        </div>
    </form>
</body>
</html>
