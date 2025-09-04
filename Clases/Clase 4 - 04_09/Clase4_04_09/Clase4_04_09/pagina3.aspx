<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagina3.aspx.cs" Inherits="Clase4_04_09.pagina3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            ELIJA UNA FECHA PARA LA REUNIÓN A LLEVARSE A CABO EN 2 MESES
            <asp:TextBox 
                ID="feREUNION"
                Columns="10"
                runat="server" />
            <br />
            <asp:RangeValidator
                ID="fechaREUNION"
                ControlToValidate="feREUNION"
                Display="Dynamic"
                Text="La fecha de reunión no debe superar los 2 meses"
                Type="Date"
                runat="server" />
            <asp:Button
                ID="BB"
                runat="server"
                Text="Enviar" 
                OnClick="botonCLICK"/>
        </div>
    </form>
</body>
</html>
