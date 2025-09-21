<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
            font-family: 'Segoe UI', sans-serif;
            margin: 0;
            padding: 0;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
            font-family: 'Segoe UI', sans-serif;
            margin: 0;
            padding: 0;
            height: 100vh;

            
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login-wrapper {
           background: #38131A;
            border-radius: 20px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.3);
            padding: 20px 30px;
            width: 100%;
            max-width: 400px;

            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;

            text-align: center;
            animation: fadeIn 1s ease-in-out;
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .login-wrapper img {
            max-height: 100px;
            margin-bottom: 5px;
            transition: transform 0.4s ease;
        }

        .login-wrapper img:hover {
            transform: scale(1.05);
        }

        .form-group {
            margin-bottom: 20px;
            text-align: left;
        }

        label {
            display: block;
            margin-bottom: 6px;
            color: #cfd8dc;
            font-weight: 500;
        }

        .form-control {
            width: 100%;
            background-color: #2a2d35;
            border: none;
            border-radius: 8px;
            padding: 12px;
            color: #ffffff !important;
            font-size: 14px;
        }

        .form-control:focus {
            outline: none;
            border: 1px solid #00ffc8;
            box-shadow: 0 0 6px #00ffc8aa;
            background-color: #30333d;
        }

        .btn-login {
            background-color: #00ffc8;
            border: none;
            border-radius: 30px;
            padding: 12px;
            font-size: 16px;
            font-weight: bold;
            color: #0f0f0f;
            width: 100%;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }

        .btn-login:hover {
            background-color: #00d8a8;
            transform: scale(1.03);
        }

        .text-danger {
            color: #ff6b6b !important;
            font-size: 0.9rem;
            margin-top: 5px;
        }
        .wrapper-flex {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 10px;
        }
    </style>

    <div class="wrapper-flex">
    <div class="login-wrapper">
        <asp:Image ID="Image1" runat="server" ImageUrl="imagenes/uai_shop_logo.png" CssClass="img-fluid" />

        <div class="form-group">
            <label for="TextBox1User">Usuario</label>
            <asp:TextBox ID="TextBox1User" runat="server" CssClass="form-control" placeholder="Ingrese su usuario" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1User" ErrorMessage="Campo requerido" CssClass="text-danger" Display="Dynamic" />
        </div>

        <div class="form-group">
            <label for="TextBox2pass">Contraseña</label>
            <asp:TextBox ID="TextBox2pass" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su contraseña" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2pass" ErrorMessage="Campo requerido" CssClass="text-danger" Display="Dynamic" />
        </div>

        <asp:Button ID="ButtonLogin" runat="server" Text="Ingresar" CssClass="btn-login" OnClick="ButtonLogin_Click" />
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />
        
        <!-- Controles ocultos para manejar recálculo de hashes -->
        <asp:HiddenField ID="hdnRecalcularHashes" runat="server" />
        <asp:Button ID="btnRecalcularHashes" runat="server" OnClick="btnRecalcularHashes_Click" style="display:none;" />
    </div>
</div>
</asp:Content>
