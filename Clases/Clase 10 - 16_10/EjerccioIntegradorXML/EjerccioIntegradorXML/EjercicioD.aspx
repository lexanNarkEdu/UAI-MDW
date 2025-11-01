<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EjercicioD.aspx.cs" Inherits="EjercicioD" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Filtrar Libros</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p><asp:Xml ID="TransformacionXSLT" runat="server"
                DocumentSource="~/EjercicioLibros.xml"
                TransformSource="~/XSLTFILE1.xslt"></asp:Xml></p>
        </div>
    </form>
</body>
</html>
