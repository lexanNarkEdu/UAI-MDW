<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="control.aspx.cs" Inherits="WebApplication1.control" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        Password
            <br />
            <asp:TextBox ID="password" Columns="30" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="AA"
                ControlToValidate="password"
                Display="Dynamic"
                ErrorMessage="Debe escribir la contraseña"
                runat="server"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="bb"
                ControlToValidate="password"
                ErrorMessage="Su password debe contener entre 3 y 20 caracteres"
                ValidationExpression="\w{3,20}"
                runat="server"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator
                ID="dd"
                ControlToValidate="password"
                ErrorMessage="Su password debe contener al menos 1 número y un caracter"
                ValidationExpression="[a-zA-Z]+\w*\d+\w*"
                runat="server"></asp:RegularExpressionValidator>
            <asp:Button ID="cc" runat="server" Text="Enviar" OnClick="cc_Click" />
        </div>
    </form>
</body>
</html>
