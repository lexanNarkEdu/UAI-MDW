<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:template match="/">
    <h4>Todas las Cuentas (Ordenadas por Saldo)</h4>
    <table border="2" cellpadding="5">
      <thead style="background-color: #4CAF50; color: white;">
        <tr>
          <th>Num.Cuenta</th>
          <th>Titular</th>
          <th>Saldo</th>
          <th>Sucursal</th>
        </tr>
      </thead>
      <tbody>
        <xsl:for-each select="Banco/Cuenta">
          <!-- Ordenar por saldo de mayor a menor -->
          <xsl:sort select="CuentaSaldo" data-type="number" order="descending"/>
          <tr align="center">
            <td>
              <xsl:value-of select="@cuentanum"/>
            </td>
            <td>
              <xsl:value-of select="CuentaNombre"/>
            </td>
            <td>
              <!-- Aplicar color verde si saldo > 10000 -->
              <xsl:choose>
                <xsl:when test="CuentaSaldo &gt; 10000">
                  <span style="color: green; font-weight: bold;">
                    <xsl:value-of select="CuentaSaldo"/> €
                  </span>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="CuentaSaldo"/> €
                </xsl:otherwise>
              </xsl:choose>
            </td>
            <td>
              <xsl:value-of select="CuentaSucursal"/>
            </td>
          </tr>
        </xsl:for-each>
        <!-- Fila de totales -->
        <tr style="background-color: #f0f0f0; font-weight: bold;">
          <td colspan="2">TOTAL</td>
          <td>
            <xsl:value-of select="sum(Banco/Cuenta/CuentaSaldo)"/> €
          </td>
          <td></td>
        </tr>
      </tbody>
    </table>
  </xsl:template>
  
</xsl:stylesheet>
