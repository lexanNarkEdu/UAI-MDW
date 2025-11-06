<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="/">
		<h4>Libros disponibles</h4>
		<xsl:for-each select="Inventario/Libros[precio>70]">
			<tr>
				<td>
					<xsl:value-of select="NombreLibro"/>
				</td>
			</tr>	
		</xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
