<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Facturas.aspx.cs" Inherits="Facturas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="color:#00ffc8; font-weight:800; font-size:2.5rem;">Facturación</h2>

    <!-- Sección 1: Facturas -->
    <h3 style="color:#00ffc8; font-weight:700; font-size:1.8rem; margin-top:2rem;">Facturas</h3>
   <asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="False" CssClass="table-dark"
    OnRowCommand="gvFacturas_RowCommand" GridLines="None" CellPadding="12" CellSpacing="10">
    <Columns>
        <asp:BoundField DataField="Numero" HeaderText="N° Factura" />
        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" />
        
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnVerDetalleFactura" runat="server" CommandName="VerDetalleFactura" Text="Ver Detalle"
                    CssClass="btn-ver-detalle" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

    <!-- Espacio -->
    <br />

    <!-- Sección 2: Pagos realizados -->
    <h3 style="color:#00ffc8; font-weight:700; font-size:1.8rem; margin-top:2rem;">Pagos realizados</h3>
    <asp:GridView ID="gvPagos" runat="server" AutoGenerateColumns="False" CssClass="table-dark"
    OnRowCommand="gvPagos_RowCommand" GridLines="None" CellPadding="12" CellSpacing="10">
    <Columns>
        <asp:BoundField DataField="PagoId" HeaderText="ID Pago" />
        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:C}" />
        
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnVerDetallePago" runat="server" CommandName="VerDetallePago" Text="Ver Detalle"
                    CssClass="btn-ver-detalle" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

    <!-- Modal / Pop-up de detalle -->
    <asp:Panel ID="pnlDetalle" runat="server" CssClass="modal" Style="display:none;">
        <div class="modal-content">
            <span onclick="cerrarModal()" class="close" title="Cerrar">&times;</span>
            <asp:Label ID="lblDetalleTitulo" runat="server" CssClass="modal-title" /><br /><br />
            <div class="modal-body">
    <asp:Literal ID="litDetalle" runat="server" />
</div>
        </div>
    </asp:Panel>

    <style>
        body {
            background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #e0e0e0;
        }

        /* Tablas estilo oscuro */
        .table-dark {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0 12px;
            font-size: 1rem;
            color: #e0e0e0;
            margin-bottom: 2.5rem;
        }
        .table-dark th, .table-dark td {
            background-color: #1f2937;
            padding: 14px 20px;
            text-align: left;
            border: none;
            border-radius: 10px;
            transition: background-color 0.3s ease;
        }
        .table-dark th {
            color: #00ffc8;
            font-weight: 700;
            font-size: 1.1rem;
        }
        .table-dark tr:hover td {
            background-color: #00ffc8;
            color: #0f0f0f;
            cursor: pointer;
        }
     /* Botón Ver Detalle - sobreescribe la regla general */
.table-dark input[type="submit"][value="Ver Detalle"] {
    background-color: #00ffc8 !important;
    color: #0f0f0f !important;
    font-weight: 700;
    padding: 10px 16px;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    font-family: 'Segoe UI', sans-serif;
    font-size: 1rem;
    transition: background-color 0.3s ease, transform 0.2s ease;
}

.table-dark input[type="submit"][value="Ver Detalle"]:hover {
    background-color: #00d9b8 !important;
    transform: scale(1.05);
}
        /* Modal */
        .modal {
            position: fixed;
            z-index: 9999;
            left: 0; top: 0;
            width: 100%; height: 100%;
            overflow: auto;
            background-color: rgba(0, 0, 0, 0.85);
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 20px;
            box-sizing: border-box;
        }
        .modal-content {
            background-color: #161b22;
            padding: 30px 35px;
            border-radius: 12px;
            max-width: 600px;
            width: 100%;
            box-shadow: 0 0 25px #00ffc8aa;
            position: relative;
            animation: fadeInModal 0.3s ease forwards;
            color: #c9d1d9;
            font-size: 1.1rem;
        }
        @keyframes fadeInModal {
            from { opacity: 0; transform: translateY(-20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        .close {
            position: absolute;
            right: 15px;
            top: 15px;
            font-size: 28px;
            font-weight: bold;
            color: #00ffc8;
            cursor: pointer;
            transition: color 0.2s ease;
        }
        .close:hover {
            color: #00d9b8;
        }
        .modal-title {
            color: #00ffc8;
            font-weight: 800;
            font-size: 2rem;
        }
        .modal-body {
            margin-top: 10px;
            color: #c9d1d9;
            font-size: 1rem;
            line-height: 1.4;
            white-space: pre-wrap;
        }
input[type="submit"][value="Ver Detalle"] {
  color: #0f0f0f !important; 
  background-color: transparent !important;
  border: 2px solid #00ffc8 !important;
  font-weight: 700;
  padding: 6px 12px;
  border-radius: 6px;
  cursor: pointer;
  transition: background-color 0.3s ease, color 0.3s ease;
}
input[type="submit"][value="Ver Detalle"]:hover {
  background-color: #00ffc8 !important;
  color: #0f0f0f !important; 
}

.btn-ver-detalle {
    background-color: #00ffc8;
    color: #0f0f0f;
    font-weight: 700;
    padding: 10px 16px;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    font-family: 'Segoe UI', sans-serif;
    font-size: 1rem;
    transition: background-color 0.3s ease, transform 0.2s ease;
    box-shadow: 0 0 10px #00ffc8aa;
}

.btn-ver-detalle:hover {
    background-color: #00d9b8;
    transform: scale(1.05);
}
    </style>

    <script type="text/javascript">
        function abrirModal() {
            document.getElementById('<%= pnlDetalle.ClientID %>').style.display = 'flex';
        }
        function cerrarModal() {
            document.getElementById('<%= pnlDetalle.ClientID %>').style.display = 'none';
        }
    </script>
</asp:Content>
