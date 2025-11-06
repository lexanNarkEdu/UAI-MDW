<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="xml" indent="yes"/>

  <!-- Copiar todo tal cual por defecto -->
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>

  <!-- Template específico para el sueldo de profesores titulares -->
  <xsl:template match="Profesor[Condicion='Titular']/Sueldo">
    <Sueldo>
      <xsl:value-of select=". * 2"/>
    </Sueldo>
  </xsl:template>

</xsl:stylesheet>