<%@ Page Title="Tienda Minimalista" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
  html, body {
    height: 100%;
    margin: 0;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
    color: #e0e0e0;
  }

  .main-wrapper {
    padding: 40px 20px;
    display: flex;
    justify-content: center;
    align-items: flex-start;
  }

  .main-content {
    max-width: 1100px;
    width: 100%;
  }

  h1 {
    font-weight: 800;
    font-size: 2.8rem;
    margin-bottom: 0.5rem;
    color: #fff;
    text-align: center;
  }

  h2 {
    font-weight: 700;
    font-size: 2.8rem;
    margin: 1.8rem 0 1rem;
    border-bottom: 2px solid #fff;
    padding-bottom: 0.3rem;
  }

  #carrito {
    background: #1f2937;
    padding: 20px;
    border-radius: 12px;
    margin-bottom: 40px;
  }

  #carrito h2 {
    margin-top: 0;
    color: #fff;
  }

  #lblTotal {
    font-weight: 700;
    font-size: 2.4rem;
    color: #fff;
    margin-bottom: 15px;
    display: block;
  }

  .btn-comprar {
    background: #fff;
    border: none;
    padding: 14px 24px;
    border-radius: 6px;
    font-size: 2.1rem;
    font-weight: 700;
    color: #0f0f0f;
    cursor: pointer;
    transition: background-color 0.3s ease;
    width: 100%;
    max-width: 280px;
    display: block;
    margin: 40px auto 0 auto;
    box-shadow: 0 0 8px #ffffff88;
  }

  .btn-comprar:hover {
    background: #e6e6e6;
    box-shadow: 0 0 12px #ffffffcc;
  }

  .carrito-item {
    display: flex;
    justify-content: space-between;
    background: #2c3e50;
    padding: 12px 16px;
    border-radius: 8px;
    margin-bottom: 10px;
    font-weight: 600;
    font-size: 2rem;
    color: #d9d9d9;
  }

  .productos-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
    gap: 20px;
  }

  .producto-card {
    background: #1f2937;
      border-radius: 14px;
      box-shadow: 0 4px 10px rgba(0,0,0,0.4);
      display: flex;
      flex-direction: column;
      overflow: hidden;
      transition: transform 0.25s ease;
      min-height: 380px;
  }

  .producto-card:hover {
    transform: translateY(-4px);
  }

.producto-img {
  width: 100%;
  height: 160px;
  object-fit: contain;
  background-color: #1f2937;
}

.producto-body {
  padding: 12px 15px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  flex-grow: 1;
}
.producto-titulo {
  font-weight: 700;
  font-size: 1.4rem;
  color: #fff;
  margin-bottom: 6px;
}

.producto-desc {
  font-size: 1rem;
  color: #ccc;
  flex-grow: 1;
  margin-bottom: 12px;
  line-height: 1.5;
}

.producto-acciones {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: auto;
}

.btn-vermas, .btn-agregar {
  font-size: 1rem;
}
  .btn-vermas,
  .btn-agregar {
    background: none;
    border: 2px solid #fff;
    padding: 8px 16px;
    border-radius: 6px;
    color: #fff;
    font-weight: 700;
    cursor: pointer;
    transition: background-color 0.3s ease, color 0.3s ease;
    text-decoration: none;
    font-size: 1.9rem;
    text-align: center;
  }

  .btn-vermas:hover,
  .btn-agregar:hover {
    background: #fff;
    color: #0f0f0f;
  }

  .btn-agregar-asp {
    background: none;
    border: 2px solid #fff;
    padding: 8px 16px;
    border-radius: 6px;
    color: #fff;
    font-weight: 700;
    cursor: pointer;
    transition: background-color 0.3s ease, color 0.3s ease;
    font-size: 1.9rem;
    text-align: center;
    width: 100%;
  }

  .btn-agregar-asp:hover {
    background: #fff;
    color: #0f0f0f;
  }
  .carrito-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #2c3e50;
  padding: 12px 16px;
  border-radius: 8px;
  margin-bottom: 10px;
  font-weight: 600;
  font-size: 2rem;
  color: #d9d9d9;
  flex-wrap: wrap;
}
  .producto-info {
  display: flex;
  align-items: center;
  gap: 16px;
}

.producto-nombre {
  font-weight: bold;
  color: #ffffff;
}

.producto-precio {
  color: #00ffc8;
  font-size: 1.5rem;

}
.total-wrapper {
  text-align: right;
  margin-top: 20px;
  font-size: 1.8rem;
  font-weight: bold;
  color: #00ffc8;
}
</style>

<div class="main-wrapper">
  <div class="main-content">
    <h1>Bienvenido a la Tienda</h1>

    <section id="carrito">
      <h2>Carrito de Compras - <asp:Label ID="Label1" runat="server" Text="" CssClass="info-label" /></h2>

<asp:Repeater ID="rptProductos" runat="server">
  <ItemTemplate>
    <div class="carrito-item">
      <div class="producto-info">
        <span class="producto-nombre"><%# Eval("Nombre") %></span>
        <span class="producto-precio">$ <%# Eval("Precio") %></span>
      </div>
      <asp:Button ID="btnAgregar" runat="server" Text="Agregar al carrito" CommandName="Agregar" CommandArgument='<%# Eval("Id") %>' CssClass="btn-agregar-asp" OnCommand="btnAgregar_Command" />
    </div>
  </ItemTemplate>
</asp:Repeater>

      <div class="total-wrapper">
  <asp:Label ID="lblTotal" runat="server" Text="Total: $0" />
</div>
      <asp:Button ID="btnComprar" runat="server" CssClass="btn-comprar" Text="Finalizar Compra" OnClick="btnComprar_Click" />
    </section>

    <section id="productos">
      <h2>Productos Disponibles</h2>
      <div class="productos-grid">
        <div class="producto-card">
          <img src="/imagenes/escritorio.jpg" alt="Escritorio Moderno" class="producto-img" />
          <div class="producto-body">
            <h3 class="producto-titulo">Escritorio Moderno</h3>
            <p class="producto-desc">Ideal para tu espacio de estudio, madera sólida y diseño ergonómico.</p>
            <a href="~/Escritorios" class="btn-vermas">Ver más</a>
          </div>
        </div>

        <div class="producto-card">
          <img src="/imagenes/monitor.png" alt="Monitor Full HD" class="producto-img" />
          <div class="producto-body">
            <h3 class="producto-titulo">Monitor Full HD</h3>
            <p class="producto-desc">Pantalla de 24 pulgadas, ideal para trabajos y estudio.</p>
              <a href="~/Monitores" class="btn-vermas">Ver más</a>
              
            </div>
        </div>

        <div class="producto-card">
          <img src="/imagenes/libro.png" alt="Libros de Ingeniería" class="producto-img" />
          <div class="producto-body">
            <h3 class="producto-titulo">Libros de Ingeniería</h3>
            <p class="producto-desc">Material actualizado para tus carreras técnicas y científicas.</p>
            <a href="~/Libros" class="btn-vermas">Ver más</a>
          </div>
        </div>

        <div class="producto-card">
          <img src="/imagenes/calculadora.jpg" alt="Calculadora Científica" class="producto-img" />
          <div class="producto-body">
            <h3 class="producto-titulo">Calculadora Científica</h3>
            <p class="producto-desc">Herramienta clave para tus cálculos y exámenes.</p>
            <a href="~/Calculadoras" class="btn-vermas">Ver más</a>
          </div>
        </div>
      </div>
    </section>
  </div>
</div>
</asp:Content>
