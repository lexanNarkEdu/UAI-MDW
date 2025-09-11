<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Servidor.aspx.cs" Inherits="Clase5_11_09.Servidor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label
                ID="LabelNoFuePosibleCalcular"
                runat="server"
                Text=""
                Visible="false"></asp:Label>
            
            <asp:Label
                ID="LabelResultadoWebService"
                runat="server"
                Text="Label"
                Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
