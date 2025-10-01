<%@ Page Title="Pruebas WebService" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="PruebasWebService.aspx.cs" Inherits="PruebasWebService" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <style>
        body, html {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
            color: #FFF;
            min-height: 100vh;
        }

        .pruebas-wrapper {
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

        .webservice-info {
            background: linear-gradient(135deg, #1e2936, #26343f);
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 30px;
            border: 1px solid #00ffc833;
            color: #ffffff;
        }

        .webservice-info h3 {
            color: #00ffc8 !important;
            margin-bottom: 15px;
        }

        .webservice-info p {
            color: #ffffff !important;
            margin-bottom: 8px;
        }

        .webservice-info ul {
            color: #ffffff !important;
        }

        .webservice-info li {
            color: #ffffff !important;
            margin-bottom: 4px;
        }

        .test-section {
            background: linear-gradient(135deg, #1e2936, #26343f);
            border-radius: 12px;
            padding: 25px;
            margin-bottom: 20px;
            border: 1px solid #00ffc833;
            color: #ffffff;
        }

        .test-section h4 {
            color: #00ffc8 !important;
            margin-bottom: 15px;
        }

        .btn-test {
            background: linear-gradient(135deg, #00ffc8, #00d4aa);
            border: none;
            color: #0f1419;
            padding: 12px 24px;
            border-radius: 8px;
            cursor: pointer;
            font-weight: bold;
            font-size: 1em;
            margin: 5px;
            transition: transform 0.2s;
        }

        .btn-test:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px #00ffc844;
        }

        .result-panel {
            background-color: #0f1419;
            border: 1px solid #30363d;
            border-radius: 8px;
            padding: 15px;
            margin-top: 15px;
            font-family: 'Courier New', monospace;
            font-size: 0.9em;
            white-space: pre-wrap;
            max-height: 400px;
            overflow-y: auto;
        }

        .success {
            color: #00ffc8;
        }

        .error {
            color: #ff6b6b;
        }

        .info {
            color: #74b9ff !important;
        }

        /* Reglas adicionales para mejorar visibilidad */
        .webservice-info strong {
            color: #00ffc8 !important;
        }

        .result-panel {
            color: #ffffff !important;
        }

        /* Asegurar que todos los labels sean visibles */
        label, span {
            color: #ffffff !important;
        }

        /* Mejorar contraste del título principal */
        h2 {
            color: #00ffc8 !important;
            text-shadow: 0 0 10px #00ffc844 !important;
            font-size: 2.6rem !important;
            font-weight: 800 !important;
        }

        /* Mejorar contraste general */
        * {
            text-shadow: none !important;
        }

        /* Sobrescribir el h2 anterior para el título */
        .pruebas-wrapper h2 {
            color: #00ffc8 !important;
            text-shadow: 0 0 15px #00ffc8 !important;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pruebas-wrapper">
        <h2>🔧 Pruebas WebService - Reportes de Ganancias</h2>
        
        <div class="webservice-info">
            <h3 style="color: #00ffc8;">📡 Información del WebService</h3>
            <p><strong>Namespace:</strong> http://uai-shop.com/reportes/</p>
            <p><strong>URL WSDL:</strong> <asp:Label ID="lblWSDL" runat="server" CssClass="info"></asp:Label></p>
            <p><strong>Métodos disponibles:</strong></p>
            <ul>
                <li>ObtenerReporteGananciasV2() - Filtros dinámicos completos</li>
                <li>ObtenerReporteV2PorPeriodo() - Acceso rápido por períodos</li>
                <li>ObtenerReporteV2PorCategoria() - Filtrado por categoría</li>
                <li>ObtenerEstadisticasGanancias(string tipoReporte)</li>
                <li>ObtenerCategoriaLider(string tipoReporte)</li>
                <li>Ping()</li>
            </ul>
        </div>

        <asp:UpdatePanel ID="UpdatePanelPruebas" runat="server">
            <ContentTemplate>
                <div class="test-section">
                    <h4 style="color: #00ffc8;">🧪 Métodos de Prueba</h4>
                    
                    <asp:Button ID="btnPing" runat="server" Text="🏓 Ping WebService" 
                        CssClass="btn-test" OnClick="btnPing_Click" />
                    
                    <asp:Button ID="btnReporteV2PorPeriodo" runat="server" Text="� Reporte V2 - Último Mes" 
                        CssClass="btn-test" OnClick="btnReporteV2PorPeriodo_Click" />
                    
                    <asp:Button ID="btnReporteV2Semanal" runat="server" Text="⚡ Reporte V2 - Semanal" 
                        CssClass="btn-test" OnClick="btnReporteV2Semanal_Click" />
                    
                    <asp:Button ID="btnEstadisticasV2" runat="server" Text="📈 Estadísticas V2" 
                        CssClass="btn-test" OnClick="btnEstadisticasV2_Click" />
                    
                    <asp:Button ID="btnCategoriaLider" runat="server" Text="🏆 Categoría Líder" 
                        CssClass="btn-test" OnClick="btnCategoriaLider_Click" />
                    
                    
                    <br />
                    <hr style="border-color: #00ffc833; margin: 20px 0;" />
                    <h5 style="color: #a29bfe; margin: 15px 0;">⭐ Reporte Dinámico V2</h5>
                    
                    <asp:Button ID="btnReporteV2General" runat="server" Text="🔥 Reporte V2 - Todo" 
                        CssClass="btn-test" OnClick="btnReporteV2General_Click" />
                    
                    <asp:Button ID="btnReporteV2UltimoMes" runat="server" Text="📅 V2 - Último Mes" 
                        CssClass="btn-test" OnClick="btnReporteV2UltimoMes_Click" />
                    
                    <asp:Button ID="btnReporteV2Categoria" runat="server" Text="📂 V2 - Por Categoría" 
                        CssClass="btn-test" OnClick="btnReporteV2Categoria_Click" />
                    
                    <asp:Button ID="btnReporteV2Filtros" runat="server" Text="🎯 V2 - Con Filtros" 
                        CssClass="btn-test" OnClick="btnReporteV2Filtros_Click" />
                    
                    <asp:Button ID="btnResumenEjecutivoV2" runat="server" Text="📋 Resumen Ejecutivo V2" 
                        CssClass="btn-test" OnClick="btnResumenEjecutivoV2_Click" />

                    <br />
                    <asp:Button ID="btnLimpiar" runat="server" Text="🧹 Limpiar Resultado" 
                        CssClass="btn-test" OnClick="btnLimpiar_Click" />
                </div>

                <div class="test-section">
                    <h4 style="color: #00ffc8;">📋 Resultado de Prueba</h4>
                    <asp:Panel ID="panelResultado" runat="server" CssClass="result-panel">
                        <asp:Label ID="lblResultado" runat="server" Text="Haga clic en un botón para ejecutar una prueba del WebService..."></asp:Label>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>