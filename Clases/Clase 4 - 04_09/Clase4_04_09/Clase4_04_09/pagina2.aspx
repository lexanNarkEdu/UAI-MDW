<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina2.aspx.cs" Inherits="Clase4_04_09.pagina2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Fecha actual:&nbsp;
            <asp:TextBox ID="TextBox1" runat="server" />
            <br />
            Fecha seleccionada:&nbsp;
            <asp:TextBox ID="TextBox2" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Validar" OnClick="BotonCLICK" />
            <asp:RangeValidator
                ID="fechareunion"
                ControlToValidate="TextBox2"
                Display="Dynamic"
                Text="Su elección no debe superar los 2 meses desde la fecha actual"
                Type="Date"
                runat="server" />
            <br />
            <br />
            <asp:Calendar
                ID="Calendar1"
                runat="server"
                SelectionMode="Day"
                OnSelectionChanged="elegirfecha" />
        </div>
    </form>
</body>
</html>
