<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:template match="/">
    <h4>Cuentas Agrupadas por Sucursal</h4>
    
    <!-- Barcelona -->
    <h5 style="color: #4CAF50; margin-top: 20px;">Barcelona</h5>
    <table border="2" cellpadding="5" style="margin-bottom: 20px;">
      <thead style="background-color: #4CAF50; color: white;">
        <tr>
          <th>Num.Cuenta</th>
          <th>Titular</th>
          <th>Saldo</th>
        </tr>
      </thead>
      <tbody>
        <xsl:for-each select="Banco/Cuenta[CuentaSucursal='Barcelona']">
          <tr align="center">
            <td><xsl:value-of select="@cuentanum"/></td>
            <td><xsl:value-of select="CuentaNombre"/></td>
            <td><xsl:value-of select="CuentaSaldo"/> €</td>
          </tr>
        </xsl:for-each>
        <tr style="background-color: #e8f5e9; font-weight: bold;">
          <td colspan="2">Subtotal Barcelona</td>
          <td>
            <xsl:value-of select="sum(Banco/Cuenta[CuentaSucursal='Barcelona']/CuentaSaldo)"/> €
          </td>
        </tr>
      </tbody>
    </table>
    
    <!-- Badajoz -->
    <h5 style="color: #2196F3; margin-top: 20px;">Badajoz</h5>
    <table border="2" cellpadding="5" style="margin-bottom: 20px;">
      <thead style="background-color: #2196F3; color: white;">
        <tr>
          <th>Num.Cuenta</th>
          <th>Titular</th>
          <th>Saldo</th>
        </tr>
      </thead>
      <tbody>
        <xsl:for-each select="Banco/Cuenta[CuentaSucursal='Badajoz']">
          <tr align="center">
            <td><xsl:value-of select="@cuentanum"/></td>
            <td><xsl:value-of select="CuentaNombre"/></td>
            <td><xsl:value-of select="CuentaSaldo"/> €</td>
          </tr>
        </xsl:for-each>
        <tr style="background-color: #e3f2fd; font-weight: bold;">
          <td colspan="2">Subtotal Badajoz</td>
          <td>
            <xsl:value-of select="sum(Banco/Cuenta[CuentaSucursal='Badajoz']/CuentaSaldo)"/> €
          </td>
        </tr>
      </tbody>
    </table>
    
    <!-- Burgos -->
    <h5 style="color: #FF9800; margin-top: 20px;">Burgos</h5>
    <table border="2" cellpadding="5" style="margin-bottom: 20px;">
      <thead style="background-color: #FF9800; color: white;">
        <tr>
          <th>Num.Cuenta</th>
          <th>Titular</th>
          <th>Saldo</th>
        </tr>
      </thead>
      <tbody>
        <xsl:for-each select="Banco/Cuenta[CuentaSucursal='Burgos']">
          <tr align="center">
            <td><xsl:value-of select="@cuentanum"/></td>
            <td><xsl:value-of select="CuentaNombre"/></td>
            <td><xsl:value-of select="CuentaSaldo"/> €</td>
          </tr>
        </xsl:for-each>
        <tr style="background-color: #fff3e0; font-weight: bold;">
          <td colspan="2">Subtotal Burgos</td>
          <td>
            <xsl:value-of select="sum(Banco/Cuenta[CuentaSucursal='Burgos']/CuentaSaldo)"/> €
          </td>
        </tr>
      </tbody>
    </table>
    
    <!-- Total General -->
    <div style="margin-top: 30px; padding: 15px; background-color: #f0f0f0; border: 2px solid #333;">
      <h5 style="margin: 0;">TOTAL GENERAL: 
        <span style="color: #4CAF50; font-size: 24px;">
          <xsl:value-of select="sum(Banco/Cuenta/CuentaSaldo)"/> €
        </span>
      </h5>
    </div>
  </xsl:template>
  
</xsl:stylesheet>
