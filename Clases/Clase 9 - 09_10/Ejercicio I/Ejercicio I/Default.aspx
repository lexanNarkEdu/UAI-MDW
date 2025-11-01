<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Ejercicio1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Transformaciones XSLT</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #f5f5f5;
        }
        .container {
            background-color: white;
            padding: 30px;
            border-radius: 5px;
            max-width: 900px;
            margin: 0 auto;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        h2 {
            color: #333;
            text-align: center;
            font-size: 28px;
            margin-bottom: 30px;
        }
        .selector-container {
            text-align: center;
            margin: 30px 0;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 5px;
            border: 2px solid #4CAF50;
        }
        .selector-container label {
            font-weight: bold;
            margin-right: 15px;
            font-size: 16px;
            color: #333;
        }
        select {
            padding: 10px 15px;
            font-size: 14px;
            border: 2px solid #4CAF50;
            border-radius: 5px;
            background-color: white;
            cursor: pointer;
            min-width: 300px;
        }
        select:hover {
            border-color: #45a049;
        }
        select:focus {
            outline: none;
            border-color: #45a049;
            box-shadow: 0 0 5px rgba(76, 175, 80, 0.3);
        }
        .button-container {
            text-align: center;
            margin: 30px 0;
        }
        input[type="button"], input[type="submit"] {
            padding: 12px 40px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            transition: background-color 0.3s;
        }
        input[type="button"]:hover, input[type="submit"]:hover {
            background-color: #45a049;
            transform: scale(1.05);
        }
        .result-container {
            margin-top: 30px;
            padding: 20px;
            background-color: #ffffff;
            border: 2px solid #4CAF50;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }
        th {
            background-color: #4CAF50;
            color: white;
            padding: 12px;
            text-align: center;
        }
        td {
            padding: 10px;
            border: 2px solid #ddd;
            text-align: center;
        }
        tr:hover {
            background-color: #f5f5f5;
        }
        h4 {
            color: #333;
            margin-bottom: 10px;
            font-size: 18px;
        }
        h5 {
            color: #4CAF50;
            margin-top: 20px;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2><strong>Transformaciones XSLT</strong></h2>
            
            <div class="selector-container">
                <label>Seleccione Plantilla XSLT:</label>
                <asp:DropDownList ID="ddlPlantillas" runat="server" AutoPostBack="false">
                    <asp:ListItem Value="~/XSLTFILE1.XSLT" Selected="True">Solo Barcelona (XSLTFILE1)</asp:ListItem>
                    <asp:ListItem Value="~/XSLTFILE2.XSLT">Todas ordenadas (XSLTFILE2)</asp:ListItem>
                    <asp:ListItem Value="~/XSLTFILE3.XSLT">Agrupadas por Sucursal (XSLTFILE3)</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="button-container">
                <asp:Button ID="btnTransformar" runat="server" Text="Transformar" OnClick="btnTransformar_Click" />
            </div>

            <div class="result-container" id="divResultado" runat="server" visible="false">
                <asp:Xml ID="TransformacionXSLT" runat="server" 
                         DocumentSource="~/XMLFILE.XML">
                </asp:Xml>
            </div>
        </div>
    </form>
</body>
</html>
