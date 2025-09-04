<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlCalendario.aspx.cs" Inherits="Clase4_04_09.ControlCalendario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Calendar
                ID="calendario"
                SelectionMode="DayWeekMonth"
                SelectWeekText="Seleccionar semana"
                SelectMonthText="Seleccionar mes"
                OnSelectionChanged="CalendarioCAMBIARSELECCION"
                runat="server" />
            <asp:Label
                ID="lblFechas"
                runat="server" />
        </div>
    </form>
</body>
</html>
