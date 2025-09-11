<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="Clase5_11_09.EjercicioParcial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Label ID="LabelApellido" runat="server" Text=""></asp:Label>
                <asp:Label ID="LabelAsignatura" runat="server" Text=""></asp:Label>
                <asp:Label ID="LabelContraPrestacion" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <div>
                Nativo ?
                <asp:RadioButtonList ID="RadioButtonListNativo" runat="server">
                    <asp:ListItem Value="Si">Si</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <br />
            <div>
                Dias de clase
                <asp:CheckBoxList ID="CheckboxListDiasClase" runat="server">
                    <asp:ListItem Value="Lunes">Lunes</asp:ListItem>
                    <asp:ListItem Value="Martes">Martes</asp:ListItem>
                    <asp:ListItem Value="Miercoles">Miercoles</asp:ListItem>
                </asp:CheckBoxList>
            </div>
            <br />
            <div>
                Materia
                <asp:DropDownList ID="DropDownListMateria" runat="server">
                    <asp:ListItem Value="Csharp">C#</asp:ListItem>
                    <asp:ListItem Value="Python">Python</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button ID="Button1" runat="server" Text="Enviar" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
