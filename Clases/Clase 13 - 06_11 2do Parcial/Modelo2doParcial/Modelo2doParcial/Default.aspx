<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                        <p><asp:Xml ID="TransformacionXSLT" runat="server"
                DocumentSource="~/Profesores.xml"
                TransformSource="~/XSLTFile.xslt"></asp:Xml></p>
            <br />
            <br />
            <br />
            <asp:Button runat="server" ID="CrearProfesor" Text="Crear Profesor" OnClick="CrearProfesor_Click"/>
        </div>
    </form>
</body>
</html>
