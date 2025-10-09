<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClienteSaldo.aspx.cs" Inherits="ClienteSaldo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            Titular:
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            Saldo actual:
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            Titular
            <asp:Button ID="Button1" runat="server" Text="Pedir datos" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
