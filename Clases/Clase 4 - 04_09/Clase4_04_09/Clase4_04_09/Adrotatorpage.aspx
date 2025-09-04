<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adrotatorpage.aspx.cs" Inherits="Clase4_04_09.Adrotatorpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:AdRotator
            AdvertisementFile="ADS.xml"
            BorderColor="Black"
            BorderWidth="1"
            runat="server" />
        
        <h3>
            Nombre:
            <asp:TextBox 
                ID="Nombre"
                runat="server" />
        </h3>
        
        <h3>
            Categoría:
            <asp:DropDownList ID="Categoria" runat="server">
                <asp:ListItem>Motor</asp:ListItem>
                <asp:ListItem>Ciclismo</asp:ListItem>
                <asp:ListItem>Natación</asp:ListItem>
            </asp:DropDownList>
        </h3>
        
        <asp:Button
            Text="Enviar"
            OnClick="SubmitBtn_Click"
            runat="server"
            ID="Button1" />
    </form>
</body>
</html>
