<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>XPathNavigator - Banco</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #f0f0f0;
        }
        .container {
            background-color: white;
            padding: 30px;
            border-radius: 5px;
            max-width: 600px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        h2 {
            color: #333;
            margin-bottom: 30px;
        }
        .form-row {
            margin-bottom: 20px;
            display: flex;
            align-items: center;
        }
        label {
            display: inline-block;
            width: 180px;
            font-weight: bold;
        }
        select, input[type="text"] {
            padding: 8px;
            font-size: 14px;
            border: 1px solid #333;
            width: 300px;
        }
        input[type="text"] {
            background-color: #f9f9f9;
        }
        .button-row {
            margin-top: 30px;
            text-align: left;
            padding-left: 180px;
        }
        input[type="button"] {
            padding: 10px 30px;
            background-color: #e0e0e0;
            border: 1px solid #999;
            cursor: pointer;
            font-size: 14px;
        }
        input[type="button"]:hover {
            background-color: #d0d0d0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>XPathNavigator</h2>
            
            <div class="form-row">
                <label>Seleccione una Cuenta:</label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" 
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="form-row">
                <label>Saldo Actual:</label>
                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="form-row">
                <label>Titular:</label>
                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="form-row">
                <label></label>
                <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" Text="Activo:"></asp:TextBox>
            </div>

            <div class="button-row">
                <asp:Button ID="Button1" runat="server" Text="Pedir Datos" OnClick="Button1_Click" />
            </div>
        </div>
        <br/>
        <br/>
        <div class="container">
            <label>Resultado:</label>
            <br />
            <br />
            <asp:Label runat="server" ID="resultado"></asp:Label>
        </div>
    </form>
</body>
</html>
