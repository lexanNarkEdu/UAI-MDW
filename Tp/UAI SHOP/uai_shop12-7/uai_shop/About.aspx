<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
  body, html {
    margin: 0; 
    padding: 0;
    padding-top: 50px;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background: linear-gradient(135deg, #0f2027, #203a43, #2c5364);
    color: #e0e0e0;
    min-height: 100vh;
  }


  h3 {
    color: #00ffc8;
    font-weight: 800;
    font-size: 2.4rem;
    margin-bottom: 1rem;
  }

  p {
    font-size: 1.3rem;
    line-height: 1.5;
    color: #c9d1d9;
    max-width: 800px;
    margin-bottom: 1.6rem;
  }

  #mapa {
    width: 100%;
    max-width: 500px;
    height: 400px;
    border: none;
    margin-top: 2rem;
    border-radius: 12px;
    box-shadow: 0 0 15px #00ffc8aa;
  }

  /* Container centrado y con padding */
  .content-wrapper {
    max-width: 900px;
    margin: 40px auto;
    padding: 0 20px;
  }

  .main-content-wrapper {
  margin-top: 0;
}
</style>

<div class="content-wrapper">
  <h3>About UAIShop</h3>
  <p>
    UAIShop es una empresa de comercio electrónico fundada en 2025 con el objetivo de ofrecer una experiencia de compra online rápida, segura y personalizada.
    A través de nuestra plataforma intuitiva, los usuarios pueden explorar miles de productos, agregarlos fácilmente a su carrito de compras y realizar pagos
    de manera segura con múltiples métodos disponibles.
  </p>
  <p>
    Nuestro sistema de carrito de compras ha sido desarrollado utilizando las últimas tecnologías en desarrollo web y está diseñado para adaptarse a cualquier dispositivo,
    asegurando una experiencia fluida tanto desde computadoras como desde dispositivos móviles. Gracias a nuestras integraciones inteligentes, cada cliente recibe
    recomendaciones personalizadas basadas en su historial de navegación y compras anteriores.
  </p>
  <p>
    Ya sea que estés buscando productos electrónicos, muebles o papelería en UAIShop encontrarás todo lo que necesitás
    en un solo lugar. ¡Unite hoy a la comunidad de compradores inteligentes!
  </p>

  <h3>Mirá dónde nos encontramos:</h3>

  <iframe id="mapa" title="Mapa sede UAIShop"
      src="https://maps.google.com/maps?hl=es&q=Av.+San+Juan+951,+CABA,+Argentina&z=17&output=embed" allowfullscreen="" loading="lazy">
  </iframe>
</div>
</asp:Content>
