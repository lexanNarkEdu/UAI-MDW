<%@ Page Title="Bitácora" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Bitacora.aspx.cs" Inherits="Bitacora" %>

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

        .bitacora-wrapper {
            max-width: 950px;
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

        label {
            font-size: 1.1rem;
            color: #00ffc8;
            font-weight: 600;
            margin-bottom: 6px;
        }

        .bitacora-form {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            margin-bottom: 30px;
            align-items: center;
        }

        .bitacora-form input[type="date"],
        .bitacora-form select {
            padding: 10px 14px;
            border-radius: 8px;
            border: 1px solid #00ffc8;
            background-color: #1f2937;
            color: #e0e0e0;
            font-size: 1rem;
            width: 220px;
        }

        .bitacora-form input[type="submit"],
        .bitacora-form button,
        .bitacora-form input[type="button"] {
            background-color: #00ffc8;
            color: #0f0f0f;
            font-weight: 700;
            padding: 10px 22px;
            border-radius: 8px;
            border: none;
            cursor: pointer;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }

        .bitacora-form input[type="submit"]:hover,
        .bitacora-form button:hover,
        .bitacora-form input[type="button"]:hover {
            background-color: #00d9b8;
            transform: scale(1.03);
        }

        .bitacora-grid {
            margin-top: 30px;
        }

        .aspNetGrid {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0 12px;
            font-size: 1rem;
            color: #e0e0e0;
        }

        .aspNetGrid th, .aspNetGrid td {
            background-color: #1f2937;
            padding: 14px 20px;
            text-align: left;
            border: none;
            border-radius: 10px;
        }

        .aspNetGrid th {
            color: #00ffc8;
            font-weight: 700;
            font-size: 1.1rem;
        }

        .aspNetGrid tr:hover td {
            background-color: #00ffc8;
            color: #0f0f0f;
            cursor: pointer;
        }

        @media screen and (max-width: 768px) {
            .bitacora-form {
                flex-direction: column;
                align-items: flex-start;
            }

            .bitacora-form input,
            .bitacora-form select {
                width: 100%;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="bitacora-wrapper">
        <h2>Consulta de Bitácora</h2>

        <div class="bitacora-form">
            <label for="txtFechaDesde">Fecha Desde</label>
            <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="input-date" />

            <label for="DropDownList1">Tipo de evento</label>
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>Seleccione una opcion</asp:ListItem>
                <asp:ListItem>Warning</asp:ListItem>
                <asp:ListItem>Error</asp:ListItem>
                <asp:ListItem>Message</asp:ListItem>
            </asp:DropDownList>

            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
        </div>

        <div class="bitacora-form">
            <label for="txtFechaHasta">Fecha Hasta</label>
            <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="input-date" />

            <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />
        </div>

        <div class="bitacora-grid">
            <asp:GridView ID="GridView1" runat="server" CssClass="aspNetGrid" AutoGenerateColumns="true" OnRowDataBound="GridView1_RowDataBound" />
        </div>
    </div>
</asp:Content>
