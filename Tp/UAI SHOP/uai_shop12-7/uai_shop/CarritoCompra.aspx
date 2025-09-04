<%@ Page Title="Carrito de Compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="CarritoCompra.aspx.cs" Inherits="CarritoCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <style>
    body {
      background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
      color: #e0e0e0;
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }

    .carrito-container {
      background: #1f2937;
      padding: 30px;
      border-radius: 16px;
      box-shadow: 0 0 20px rgba(0, 0, 0, 0.3);
      max-width: 1000px;
      margin: 0 auto;
    }

    h2 {
      text-align: center;
      font-size: 2.4rem;
      font-weight: bold;
      color: #00ffc8;
      margin-bottom: 30px;
    }

    .gridview {
      width: 100%;
      border-collapse: collapse;
      margin-bottom: 30px;
    }

    .gridview th,
    .gridview td {
      padding: 14px;
      border: 1px solid #2c3e50;
      text-align: left;
      color: #ffffff;
    }

    .gridview th {
      background-color: #2c3e50;
      color: #00ffc8;
    }

    .gridview tr:nth-child(even) {
      background-color: #263544;
    }

    .gridview tr:hover {
      background-color: #34495e;
    }

    .label-total {
      font-size: 1.3rem;
      font-weight: bold;
      color: #00ffc8;
      text-align: right;
      margin-bottom: 20px;
      display: block;
    }

    .radio-buttons {
      margin: 30px 0;
      color: #e0e0e0;
      font-weight: 500;
    }

    .radio-buttons input[type="radio"] {
      margin-right: 6px;
    }

    .btn-finalizar {
      background-color: #00ffc8;
      color: #0f0f0f;
      padding: 14px 28px;
      border: none;
      font-weight: bold;
      font-size: 1.5rem;
      border-radius: 8px;
      cursor: pointer;
      transition: background 0.3s ease;
      display: block;
      width: 100%;
      max-width: 250px;
      margin: 0 auto;
      box-shadow: 0 0 10px #00ffc880;
    }

    .btn-finalizar:hover {
      background-color: #00d8a8;
    }
  </style>

  <div class="carrito-container">
    <h2>Carrito de Compras</h2>

    <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="false" CssClass="gridview">
      <Columns>
        <asp:BoundField DataField="Nombre" HeaderText="Producto" />
        <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
      </Columns>
    </asp:GridView>

    <asp:Label ID="lblTotal" runat="server" CssClass="label-total" />

    <div class="radio-buttons">
      <asp:RadioButtonList ID="rblMetodoPago" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Text="Efectivo" Value="Efectivo" Selected="True" />
        <asp:ListItem Text="Tarjeta" Value="Tarjeta" />
      </asp:RadioButtonList>
    </div>

    <asp:Button ID="btnFinalizarVenta" runat="server" Text="Finalizar Venta" OnClick="btnFinalizarVenta_Click" CssClass="btn-finalizar" />
  </div>
</asp:Content>
