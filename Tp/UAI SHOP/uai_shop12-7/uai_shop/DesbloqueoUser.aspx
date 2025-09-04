<%@ Page Title="Desbloqueo de Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="DesbloqueoUser.aspx.cs" Inherits="DesbloqueoUser" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        html, body {
            margin: 0;
            padding: 0;
            background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #e0e0e0;
            min-height: 100vh;
        }

        .desbloqueo-wrapper {
            max-width: 700px;
            margin: 60px auto;
            background-color: #161b22;
            border-radius: 12px;
            padding: 40px 30px;
            box-shadow: 0 0 25px #00ffc844;
        }

        h2 {
            color: #00ffc8;
            font-size: 2.4rem;
            font-weight: 800;
            margin-bottom: 30px;
            text-align: center;
        }

        label {
            font-size: 1.1rem;
            color: #00ffc8;
            font-weight: 600;
            display: block;
            margin-bottom: 8px;
        }

        .form-control {
            width: 100%;
            padding: 10px 14px;
            border-radius: 8px;
            border: 1px solid #00ffc8;
            background-color: #1f2937;
            color: #e0e0e0;
            font-size: 1rem;
        }

        .btn-primary {
            background-color: #00ffc8;
            color: #0f0f0f;
            font-weight: 700;
            padding: 10px 20px;
            border-radius: 8px;
            border: none;
            cursor: pointer;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }

        .btn-primary:hover {
            background-color: #00d9b8;
            transform: scale(1.03);
        }

        .resultado {
            margin-top: 20px;
            font-size: 1.1rem;
            color: #00ffc8;
        }

        .form-group {
            margin-bottom: 25px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="desbloqueo-wrapper">
        <h2>Desbloquear Usuario</h2>

        <asp:Panel runat="server">
            <!-- Lista de usuarios -->
            <div class="form-group">
                <label for="ddlUsuarios">Seleccioná un usuario:</label>
                <asp:DropDownList ID="ddlUsuarios" runat="server" CssClass="form-control" />
            </div>

            <!-- Botón desbloquear -->
            <div class="form-group">
                <asp:Button ID="btnDesbloquear" runat="server" Text="Desbloquear" CssClass="btn-primary" OnClick="btnDesbloquear_Click" />
            </div>

            <!-- Resultado -->
            <div class="form-group resultado">
                <asp:Label ID="lblResultado" runat="server" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
