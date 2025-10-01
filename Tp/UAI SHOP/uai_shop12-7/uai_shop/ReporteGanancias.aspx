<%@ Page Title="Reporte de Ganancias V2" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ReporteGanancias.aspx.cs" Inherits="ReporteGanancias" %>

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
            max-width: 1400px;
            margin: 60px auto;
            background-color: #161b22;
            border-radius: 16px;
            padding: 40px;
            box-shadow: 0 0 25px #00ffc844;
        }

        h2 {
            color: #00ffc8 !important;
            font-size: 2.6rem !important;
            font-weight: 800 !important;
            margin-bottom: 2rem;
            text-align: center;
            text-shadow: 0 0 15px #00ffc8 !important;
        }

        /* Reportes Rápidos */
        .reportes-rapidos {
            background: linear-gradient(135deg, #1e2936, #26343f);
            border-radius: 12px;
            padding: 25px;
            margin-bottom: 30px;
            border: 1px solid #00ffc833;
            text-align: center;
        }

        .reportes-rapidos h3 {
            color: #00ffc8;
            margin-bottom: 20px;
            font-size: 1.4em;
            text-shadow: 0 0 10px #00ffc8;
        }

        .botones-rapidos {
            display: flex;
            justify-content: center;
            gap: 20px;
            flex-wrap: wrap;
        }

        /* Toggle para Filtros */
        .filtros-toggle {
            text-align: center;
            margin: 30px 0 15px 0;
            padding: 20px;
            background: rgba(0, 255, 200, 0.05);
            border-radius: 12px;
            border: 1px dashed #00ffc833;
        }

        .toggle-text {
            color: #e0e0e0;
            font-size: 1.1em;
            margin-right: 10px;
        }

        .toggle-link {
            color: #00ffc8;
            text-decoration: none;
            font-weight: bold;
            padding: 10px 20px;
            border: 2px solid #00ffc8;
            border-radius: 25px;
            transition: all 0.3s ease;
            display: inline-block;
            text-shadow: 0 0 5px #00ffc8;
        }

        .toggle-link:hover {
            background: #00ffc8;
            color: #0f1419;
            transform: scale(1.05);
            box-shadow: 0 0 15px #00ffc844;
        }

        /* Filtros Avanzados */
        .filtros-avanzados {
            background: linear-gradient(135deg, #1e2936, #26343f);
            border-radius: 12px;
            padding: 25px;
            margin-bottom: 30px;
            border: 1px solid #00ffc833;
        }

        .filtros-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 25px;
            padding-bottom: 15px;
            border-bottom: 1px solid #00ffc833;
        }

        .filtros-header h3 {
            color: #00ffc8;
            margin: 0;
            font-size: 1.3em;
            text-shadow: 0 0 10px #00ffc8;
        }

        .ocultar-btn {
            color: #ff6b6b;
            text-decoration: none;
            padding: 8px 16px;
            border: 1px solid #ff6b6b;
            border-radius: 20px;
            font-size: 0.9em;
            transition: all 0.3s ease;
        }

        .ocultar-btn:hover {
            background: #ff6b6b;
            color: white;
        }

        .filtros-content {
            display: flex;
            flex-direction: column;
            gap: 25px;
        }

        .filtro-row {
            display: flex;
            gap: 30px;
            flex-wrap: wrap;
        }

        .filtro-group {
            flex: 1;
            min-width: 200px;
        }

        .filtro-group label {
            display: block;
            margin-bottom: 8px;
            font-weight: bold;
            color: #00ffc8;
            font-size: 1em;
        }

        .fecha-inputs, .rango-inputs {
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .fecha-inputs span, .rango-inputs span {
            color: #e0e0e0;
            font-weight: bold;
        }

        .filtro-group input, .dropdown-categoria {
            padding: 12px 15px;
            border: 2px solid transparent;
            border-radius: 8px;
            background: #0f1419;
            color: #e0e0e0;
            font-size: 0.95em;
            transition: all 0.3s ease;
            flex: 1;
        }

        .filtro-group input:focus, .dropdown-categoria:focus {
            border-color: #00ffc8;
            outline: none;
            box-shadow: 0 0 10px rgba(0, 255, 200, 0.3);
        }

        .filtro-group input:hover, .dropdown-categoria:hover {
            border-color: rgba(0, 255, 200, 0.5);
        }

        .filtro-actions {
            text-align: center;
            margin-top: 25px;
        }

        .btn-buscar {
            padding: 15px 35px;
            border: none;
            border-radius: 8px;
            font-weight: bold;
            cursor: pointer;
            background: linear-gradient(135deg, #00ffc8, #00d4aa);
            color: #0f1419;
            font-size: 1.1em;
            transition: all 0.3s ease;
        }

        .btn-buscar:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 15px #00ffc844;
        }

        .resultados-info {
            display: block;
            margin-top: 15px;
            font-weight: bold;
            font-size: 1em;
            text-align: center;
            padding: 10px;
            border-radius: 6px;
            background-color: rgba(0, 255, 200, 0.1);
            border: 1px solid rgba(0, 255, 200, 0.3);
            animation: fadeInResult 0.5s ease-in-out;
        }

        @keyframes fadeInResult {
            from { opacity: 0; transform: translateY(-10px); }
            to { opacity: 1; transform: translateY(0); }
        }

        .kpis-container {
            margin-bottom: 30px;
        }

        .kpis-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
            gap: 20px;
            margin: 20px 0;
        }

        .kpi-card {
            background: linear-gradient(135deg, #1e2936, #26343f);
            border-radius: 12px;
            padding: 20px;
            border: 1px solid #00ffc833;
            display: flex;
            align-items: center;
            gap: 15px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            min-height: 80px;
            overflow: hidden;
        }

        .kpi-card:hover {
            transform: translateY(-2px);
            box-shadow: 0 8px 25px rgba(0, 255, 200, 0.15);
        }

        .kpi-icon {
            font-size: 2.5em;
            width: 60px;
            height: 60px;
            display: flex;
            align-items: center;
            justify-content: center;
            background: rgba(0, 255, 200, 0.1);
            border-radius: 10px;
            flex-shrink: 0;
        }

        .kpi-content {
            flex: 1;
        }

        .kpi-value {
            display: block;
            font-size: 1.5em;
            font-weight: bold;
            color: #00ffc8;
            margin-bottom: 5px;
            line-height: 1.2;
            word-wrap: break-word;
            overflow-wrap: break-word;
        }

        .kpi-label {
            display: block;
            font-size: 0.9em;
            color: #a0a0a0;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .grid-container {
            background: linear-gradient(135deg, #1e2936, #26343f);
            border-radius: 12px;
            padding: 25px;
            border: 1px solid #00ffc833;
            overflow-x: auto;
            animation: fadeInGrid 0.6s ease-in-out;
        }

        @keyframes fadeInGrid {
            from { 
                opacity: 0; 
                transform: translateY(20px) scale(0.98);
            }
            to { 
                opacity: 1; 
                transform: translateY(0) scale(1);
            }
        }

        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            min-width: 800px;
        }

        .grid-view th {
            background: linear-gradient(135deg, #00ffc8, #00d4aa);
            color: #0f1419;
            padding: 15px 10px;
            text-align: center;
            font-weight: bold;
            border: none;
            font-size: 0.9em;
        }

        .grid-view td {
            padding: 12px 10px;
            border-bottom: 1px solid #30363d;
            color: #e0e0e0;
            text-align: center;
            transition: background-color 0.3s ease;
        }

        .grid-view tr:nth-child(even) {
            background-color: #21262d;
        }

        .grid-view tr:hover {
            background-color: #30363d;
            transition: background-color 0.3s;
        }

        .btn-primary {
            background: linear-gradient(135deg, #00ffc8, #00d4aa);
            border: none;
            color: #0f1419;
            padding: 12px 24px;
            border-radius: 8px;
            cursor: pointer;
            font-weight: bold;
            font-size: 1em;
            transition: transform 0.2s;
            margin: 5px;
        }

        .btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px #00ffc844;
        }

        .btn-secondary {
            background: linear-gradient(135deg, #4a5568, #2d3748);
            border: none;
            color: #e0e0e0;
            padding: 12px 24px;
            border-radius: 8px;
            cursor: pointer;
            font-weight: bold;
            font-size: 1em;
            transition: transform 0.2s;
            margin: 5px;
        }

        .btn-secondary:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(74, 85, 104, 0.4);
        }

        .no-data {
            text-align: center;
            color: #ff6b6b;
            font-size: 1.2em;
            padding: 40px;
        }

        .filter-section {
            display: flex;
            gap: 15px;
            align-items: center;
            flex-wrap: wrap;
        }

        .filter-group {
            display: flex;
            flex-direction: column;
            gap: 5px;
        }

        .filter-group label {
            color: #00ffc8;
            font-weight: bold;
            font-size: 0.9em;
        }

        .filter-group input, .filter-group select {
            padding: 8px 12px;
            border-radius: 6px;
            border: 1px solid #30363d;
            background-color: #0d1117;
            color: #e0e0e0;
        }

        .currency {
            color: #00d4aa;
            font-weight: bold;
        }

        .percentage {
            color: #ffa500;
            font-weight: bold;
        }

        .category-col {
            text-align: left !important;
            font-weight: bold;
            color: #00ffc8;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="reporte-wrapper">
        <h2>� Reporte de Ganancias V2</h2>
        
        <asp:UpdatePanel ID="UpdatePanelReporte" runat="server">
            <ContentTemplate>
                
                <!-- Reportes Rápidos -->
                <div class="reportes-rapidos">
                    <h3>🚀 Reportes Rápidos</h3>
                    <div class="botones-rapidos">
                        <asp:Button ID="btnReporteGeneral" runat="server" Text="📊 Reporte General" 
                            CssClass="btn-primary" OnClick="btnReporteGeneral_Click" />
                        <asp:Button ID="btnReporteUltimoMes" runat="server" Text="📅 Último Mes" 
                            CssClass="btn-primary" OnClick="btnReporteUltimoMes_Click" />
                        <asp:Button ID="btnReporteUltimoAnio" runat="server" Text="� Último Año" 
                            CssClass="btn-primary" OnClick="btnReporteUltimoAnio_Click" />
                    </div>
                </div>

                <!-- Toggle para Filtros Avanzados -->
                <div class="filtros-toggle">
                    <span class="toggle-text">💡 ¿Necesitas filtros específicos?</span>
                    <a href="javascript:void(0);" id="toggleFiltros" class="toggle-link">⚙️ Mostrar Opciones Avanzadas</a>
                </div>

                <!-- Filtros Avanzados (Inicialmente ocultos) -->
                <div id="filtrosAvanzados" class="filtros-avanzados" style="display: none;">
                    <div class="filtros-header">
                        <h3>⚙️ Filtros Avanzados</h3>
                        <a href="javascript:void(0);" id="ocultarFiltros" class="ocultar-btn">❌ Ocultar</a>
                    </div>
                    
                    <div class="filtros-content">
                        <div class="filtro-row">
                            <div class="filtro-group">
                                <label>📅 Período:</label>
                                <div class="fecha-inputs">
                                    <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" placeholder="Desde" />
                                    <span>-</span>
                                    <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" placeholder="Hasta" />
                                </div>
                            </div>
                        </div>
                        
                        <div class="filtro-row">
                            <div class="filtro-group">
                                <label>🏷️ Categoría:</label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="dropdown-categoria">
                                    <asp:ListItem Value="">Todas las categorías</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="filtro-row">
                            <div class="filtro-group">
                                <label>💰 Rango de Costos:</label>
                                <div class="rango-inputs">
                                    <asp:TextBox ID="txtCostoMin" runat="server" placeholder="Mín. $0" />
                                    <span>-</span>
                                    <asp:TextBox ID="txtCostoMax" runat="server" placeholder="Máx. $50000" />
                                </div>
                            </div>
                            
                            <div class="filtro-group">
                                <label>� Monto de Ventas:</label>
                                <div class="rango-inputs">
                                    <asp:TextBox ID="txtVentasMin" runat="server" placeholder="Mín. $10000" />
                                    <span>-</span>
                                    <asp:TextBox ID="txtVentasMax" runat="server" placeholder="Máx. $500000" />
                                </div>
                            </div>
                        </div>
                        
                        <div class="filtro-actions">
                            <asp:Button ID="btnBuscarFiltros" runat="server" Text="🔍 Buscar" 
                                CssClass="btn-buscar" OnClick="btnBuscarFiltros_Click" />
                            <asp:Label ID="lblResultados" runat="server" CssClass="resultados-info" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
                
                <!-- Panel de Estadísticas KPIs -->
                <asp:Panel ID="panelEstadisticas" runat="server" CssClass="kpis-container" Visible="false">
                    <div class="kpis-grid">
                        <div class="kpi-card">
                            <div class="kpi-icon">💰</div>
                            <div class="kpi-content">
                                <asp:Label ID="lblGananciaTotal" runat="server" CssClass="kpi-value"></asp:Label>
                                <span class="kpi-label">Ganancia Total</span>
                            </div>
                        </div>
                        
                        <div class="kpi-card">
                            <div class="kpi-icon">📈</div>
                            <div class="kpi-content">
                                <asp:Label ID="lblMargenPromedio" runat="server" CssClass="kpi-value"></asp:Label>
                                <span class="kpi-label">Margen Promedio</span>
                            </div>
                        </div>
                        
                        <div class="kpi-card">
                            <div class="kpi-icon">🛒</div>
                            <div class="kpi-content">
                                <asp:Label ID="lblTotalVentas" runat="server" CssClass="kpi-value"></asp:Label>
                                <span class="kpi-label">Total Ventas</span>
                            </div>
                        </div>
                        
                        <div class="kpi-card">
                            <div class="kpi-icon">🏆</div>
                            <div class="kpi-content">
                                <asp:Label ID="lblCategoriaTop" runat="server" CssClass="kpi-value"></asp:Label>
                                <span class="kpi-label">Mejor Categoría</span>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                
                <!-- Grid de Resultados -->
                <div class="grid-container">
                    <asp:GridView ID="gvReporteGanancias" runat="server" 
                        CssClass="grid-view"
                        AutoGenerateColumns="false" 
                        EmptyDataText="No se encontraron datos de ganancias.">
                        <Columns>
                            <asp:TemplateField HeaderText="Categoría">
                                <ItemTemplate>
                                    <span class="category-col"><%# Eval("Categoria") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="CantidadVentas" HeaderText="# Ventas" />
                            
                            <asp:BoundField DataField="UnidadesVendidas" HeaderText="Unidades" />
                            
                            <asp:TemplateField HeaderText="Venta Total">
                                <ItemTemplate>
                                    <span class="currency"><%# String.Format("{0:C}", Eval("VentaTotal")) %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Costo Total">
                                <ItemTemplate>
                                    <span class="currency"><%# String.Format("{0:C}", Eval("CostoTotal")) %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Ganancia Total">
                                <ItemTemplate>
                                    <span class="currency"><%# String.Format("{0:C}", Eval("GananciaTotal")) %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="% Ganancia">
                                <ItemTemplate>
                                    <span class="percentage"><%# String.Format("{0:N1}%", BLL.ReporteGananciasV2BLL.CalcularPorcentajeGanancia((BE.ReporteGananciasV2)Container.DataItem)) %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="no-data">
                                📊 No hay datos de ganancias disponibles con los filtros aplicados.
                                <br />
                                <small>Intente ajustar los criterios de búsqueda.</small>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        function initializeFiltrosToggle() {
            var toggleFiltros = document.getElementById('toggleFiltros');
            var ocultarFiltros = document.getElementById('ocultarFiltros');
            var filtrosAvanzados = document.getElementById('filtrosAvanzados');
            
            if (toggleFiltros && filtrosAvanzados && ocultarFiltros) {
                // Limpiar eventos anteriores
                toggleFiltros.onclick = null;
                ocultarFiltros.onclick = null;
                
                // Mostrar filtros avanzados
                toggleFiltros.onclick = function() {
                    filtrosAvanzados.style.display = 'block';
                    filtrosAvanzados.style.animation = 'fadeIn 0.3s ease-in-out';
                    
                    // Scroll suave hacia los filtros
                    filtrosAvanzados.scrollIntoView({ 
                        behavior: 'smooth', 
                        block: 'start' 
                    });
                    return false;
                };
                
                // Ocultar filtros avanzados
                ocultarFiltros.onclick = function() {
                    filtrosAvanzados.style.animation = 'fadeOut 0.3s ease-in-out';
                    setTimeout(function() {
                        filtrosAvanzados.style.display = 'none';
                    }, 300);
                    return false;
                };
            }
        }
        
        // Usar pageLoad para compatibilidad con UpdatePanel
        function pageLoad() {
            initializeFiltrosToggle();
        }
        
        // Inicializar también al cargar por primera vez
        document.addEventListener('DOMContentLoaded', initializeFiltrosToggle);
        
        // Animaciones CSS
        const style = document.createElement('style');
        style.textContent = `
            @keyframes fadeIn {
                from { opacity: 0; transform: translateY(-10px); }
                to { opacity: 1; transform: translateY(0); }
            }
            
            @keyframes fadeOut {
                from { opacity: 1; transform: translateY(0); }
                to { opacity: 0; transform: translateY(-10px); }
            }
        `;
        document.head.appendChild(style);
        
        // Validación de rangos en tiempo real
        function initializeRangeValidation() {
            var costoMin = document.getElementById('txtCostoMin');
            var costoMax = document.getElementById('txtCostoMax');
            var ventasMin = document.getElementById('txtVentasMin');
            var ventasMax = document.getElementById('txtVentasMax');
            
            function validateRange(minInput, maxInput) {
                var minVal = parseFloat(minInput.value.replace(/[^0-9.-]/g, '')) || 0;
                var maxVal = parseFloat(maxInput.value.replace(/[^0-9.-]/g, '')) || 0;
                
                if (minInput.value && maxInput.value && minVal > maxVal) {
                    minInput.style.borderColor = '#ff6b6b';
                    maxInput.style.borderColor = '#ff6b6b';
                } else {
                    minInput.style.borderColor = '';
                    maxInput.style.borderColor = '';
                }
            }
            
            if (costoMin && costoMax) {
                costoMin.addEventListener('blur', function() { validateRange(costoMin, costoMax); });
                costoMax.addEventListener('blur', function() { validateRange(costoMin, costoMax); });
            }
            
            if (ventasMin && ventasMax) {
                ventasMin.addEventListener('blur', function() { validateRange(ventasMin, ventasMax); });
                ventasMax.addEventListener('blur', function() { validateRange(ventasMin, ventasMax); });
            }
        }
        
        // Inicializar validación después del DOM
        setTimeout(initializeRangeValidation, 100);
    </script>
</asp:Content>