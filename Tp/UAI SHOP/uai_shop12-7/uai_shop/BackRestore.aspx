<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="BackRestore.aspx.cs" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #e0e0e0;
        }

        .restore-wrapper {
            max-width: 700px;
            margin: 100px auto 40px auto;
            padding: 40px;
            background-color: #161b22;
            border-radius: 12px;
            box-shadow: 0 0 25px #00ffc844;
            text-align: center;
        }

        .restore-wrapper h2 {
            color: #00ffc8;
            font-size: 2.5rem;
            font-weight: 800;
            margin-bottom: 1rem;
        }

        .restore-wrapper label, 
        .restore-wrapper .section-title {
            color: #00ffc8;
            font-size: 1.4rem;
            font-weight: 600;
            display: block;
            margin-top: 30px;
            margin-bottom: 10px;
        }

        .restore-wrapper .asp-buttons input[type="submit"], 
        .restore-wrapper .asp-buttons button {
            background-color: #00ffc8;
            color: #0f0f0f;
            border: none;
            border-radius: 8px;
            padding: 12px 30px;
            font-size: 1.2rem;
            font-weight: 700;
            cursor: pointer;
            margin-top: 10px;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }

        .restore-wrapper .asp-buttons input[type="submit"]:hover,
        .restore-wrapper .asp-buttons button:hover {
            background-color: #00d9b8;
            transform: scale(1.03);
        }

        .restore-wrapper .asp-buttons {
            margin-top: 10px;
        }

        .restore-wrapper input[type="file"] {
            margin-top: 20px;
            color: #e0e0e0;
            font-size: 1rem;
        }
    </style>

    <div class="restore-wrapper">
        <h2>Gestión de Base de Datos</h2>

        <span class="section-title">Generar Backup</span>
        <asp:Label ID="Label1" runat="server" Text="Esto generará un respaldo completo del sistema."></asp:Label>
        <div class="asp-buttons">
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Backup" />
        </div>

        <span class="section-title">Generar Restore</span>
        <asp:FileUpload ID="fuRestore" runat="server" 
                        accept=".bak" 
                        onchange="validateFileType(this)" />
        <div class="asp-buttons">
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Restore" />
        </div>
    </div>

    <script type="text/javascript">
        function validateFileType(input) {
            if (input.files && input.files[0]) {
                var fileName = input.files[0].name;
                var fileExtension = fileName.split('.').pop().toLowerCase();
                
                if (fileExtension !== 'bak') {
                    alert('Por favor, seleccione solo archivos .bak');
                    input.value = '';
                    return false;
                }
            }
        }
    </script>
</asp:Content>
