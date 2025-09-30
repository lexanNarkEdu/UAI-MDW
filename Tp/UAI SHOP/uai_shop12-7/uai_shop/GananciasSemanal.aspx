<%@ Page Title="Ganancias Semanal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="GananciasSemanal.aspx.cs" Inherits="GananciasSemanal" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        body, html {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
            color: #e0e0e0;
            min-height: 100vh;
        }

        .reporte-wrapper {
            max-width: 1200px;
            margin: 60px auto;
            background-color: #161b22;
            border-radius: 16px;
            padding: 40px;
            box-shadow: 0 0 25px #00ffc844;
        }

        h2 {
            color: #00ffc8;
            font-size: 2.6rem;
            font-weight: 800;
            margin-bottom: 2rem;
            text-align: center;
        }

        .periodo-info {
            background: linear-gradient(135deg, #5f27cd, #341f97);
            color: white;
            padding: 15px;
            border-radius: 8px;
            text-align: center;
            margin-bottom: 20px;
            font-weight: bold;
        }

        .estadisticas {
            background: linear-gradient(135deg, #1e2936, #26343f);
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 30px;
            border: 1px solid #00ffc833;
            text-align: center;
            font-size: 1.1em;
            color: #00ffc8;
        }

        .grid-container {
            background: linear-gradient(135deg, #1e2936, #26343f);
            border-radius: 12px;
            padding: 25px;
            border: 1px solid #00ffc833;
        }

        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .grid-view th {
            background: linear-gradient(135deg, #00ffc8, #00d4aa);
            color: #0f1419;
            padding: 15px;
            text-align: left;
            font-weight: bold;
            border: none;
        }

        .grid-view td {
            padding: 12px 15px;
            border-bottom: 1px solid #30363d;
            color: #e0e0e0;
        }

        .grid-view tr:nth-child(even) {
            background-color: #21262d;
        }

        .grid-view tr:hover {
            background-color: #30363d;
            transition: background-color 0.3s;
        }

        .btn-refresh {
            background: linear-gradient(135deg, #00ffc8, #00d4aa);
            border: none;
            color: #0f1419;
            padding: 12px 24px;
            border-radius: 8px;
            cursor: pointer;
            font-weight: bold;
            font-size: 1em;
            margin-bottom: 20px;
            transition: transform 0.2s;
        }

        .btn-refresh:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px #00ffc844;
        }

        .no-data {
            text-align: center;
            color: #ff6b6b;
            font-size: 1.2em;
            padding: 40px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="reporte-wrapper">
        <h2>⚡ Reporte de Ganancias - Semanal</h2>
        
        <div class="periodo-info">
            📊 Mostrando ganancias de los últimos 7 días por categoría
        </div>
        
        <asp:UpdatePanel ID="UpdatePanelReporte" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnActualizar" runat="server" Text="🔄 Actualizar Datos" 
                    CssClass="btn-refresh" OnClick="btnActualizar_Click" />
                
                <asp:Panel ID="panelEstadisticas" runat="server" CssClass="estadisticas">
                    <asp:Label ID="lblEstadisticas" runat="server" Text=""></asp:Label>
                </asp:Panel>
                
                <div class="grid-container">
                    <asp:GridView ID="gvGanancias" runat="server" 
                        CssClass="grid-view"
                        AutoGenerateColumns="false" 
                        EmptyDataText="No se encontraron datos de ganancias semanales.">
                        <Columns>
                            <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                            <asp:BoundField DataField="VentasConEstaCategoria" HeaderText="# Ventas (7 días)" />
                            <asp:BoundField DataField="UnidadesTotales" HeaderText="Unidades Vendidas" />
                            <asp:BoundField DataField="PrecioPromedio" HeaderText="Precio Promedio" 
                                DataFormatString="{0:C}" />
                            <asp:BoundField DataField="GananciaTotal" HeaderText="Ganancia Total" 
                                DataFormatString="{0:C}" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="no-data">
                                📈 No hay datos de ganancias disponibles en esta semana.
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>