<%@ Page Title="Catálogo (XML)" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="CatalogoXML.aspx.cs" Inherits="CatalogoXML" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- ⚠️ No toques el <body> global para no romper otras páginas.
         Estilamos SOLO dentro de .catalog-wrapper -->
    <style>
        .catalog-wrapper {
            background: #38131A;            /* mismo tono que tu login-wrapper */
            border-radius: 20px;
            box-shadow: 0 10px 25px rgba(0,0,0,.3);
            padding: 20px 24px;
            max-width: 980px;
            margin: 24px auto;
            color: #e0e1dd;
            animation: fadeIn .8s ease-in-out;
        }
        .catalog-title { font-size: 22px; margin: 0 0 10px; }
        .catalog-sub   { color: #cfd8dc; margin-bottom: 16px; }
        .catalog-row   { display: flex; gap: 10px; align-items: center; margin: 10px 0; flex-wrap: wrap; }

        select, 
select.form-control,
select:focus {
    background-color: #2a2d35 !important; /* fondo oscuro */
    color: #ffffff !important;            /* texto claro */
    border: none;
    border-radius: 8px;
    padding: 10px 12px;
    appearance: none;                     /* quita borde nativo */
    outline: none;
    box-shadow: 0 0 4px rgba(0,255,200,0.3);
}

        /* Evita que el select se encoja en el flex container */
.catalog-row { flex-wrap: nowrap; }
.catalog-row .btn-primary { flex: 0 0 auto; }        /* el botón no crece ni empuja */

/* Target del DropDownList (ASP.NET cambia el id ⇒ uso sufijo) */
.catalog-row select[id$="DdlCategorias"]{
  width: 420px;               /* ajustá a gusto */
  max-width: 100%;
  box-sizing: border-box;
  height: 44px;               /* mismo alto que inputs/botón */
  line-height: normal;        /* evita recorte vertical */
  padding: 10px 14px;
  border-radius: 12px;
  white-space: nowrap;        /* no partir el texto */
  flex: 0 0 420px;            /* fija ancho dentro del flex */
}

/* Opciones del desplegable en oscuro (ya lo tenías, lo dejo aquí por claridad) */
.catalog-row select[id$="DdlCategorias"] option{
  background-color:#2a2d35;
  color:#fff;
}

/* Opciones dentro del desplegable */
select option {
    background-color: #2a2d35; /* fondo oscuro */
    color: #ffffff;            /* texto claro */
}

/* Opción seleccionada */
select option:checked {
    background-color: #00ffc8;
    color: #0f0f0f;
}

        .form-control {
            background-color: #2a2d35; color: #fff !important;
            border: none; border-radius: 8px; padding: 10px 12px; min-width: 260px;
        }
        .btn-primary {
            background-color: #00ffc8; color:#0f0f0f; border:0; border-radius: 30px;
            padding: 10px 16px; font-weight: 700; cursor: pointer; transition: .2s;
        }
        .btn-primary:hover { transform: scale(1.03); background-color:#00d8a8; }

        .grid-wrapper { margin-top: 12px; }
        .msg { margin-top: 6px; display: inline-block; color: #cfd8dc; }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(8px); }
            to   { opacity: 1; transform: translateY(0); }
        }
    </style>

    <div class="catalog-wrapper">
        <h2 class="catalog-title">Catálogo (XML → XSLT)</h2>
        <div class="catalog-sub">Busqueda</div>

        <div class="catalog-row">
            <asp:Button ID="BtnRegenerar" runat="server" CssClass="btn-primary" Text="Regenerar XML"
                OnClick="BtnRegenerar_Click" />
        </div>

        <div class="catalog-row">
            <asp:Label ID="LblCat" runat="server" Text="Categoría:" AssociatedControlID="DdlCategorias"></asp:Label>
            <asp:DropDownList ID="DdlCategorias" runat="server" CssClass="form-control"></asp:DropDownList>
            <asp:Button ID="BtnBuscar" runat="server" CssClass="btn-primary" Text="Buscar"
                OnClick="BtnBuscar_Click" />
            <asp:Label ID="LblMsg" runat="server" CssClass="msg"></asp:Label>
        </div>

        <div class="grid-wrapper">
            <asp:GridView ID="GvProductos" runat="server" AutoGenerateColumns="false"
                CssClass="table table-dark table-striped" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="IdProducto" HeaderText="ID" />
                    <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>