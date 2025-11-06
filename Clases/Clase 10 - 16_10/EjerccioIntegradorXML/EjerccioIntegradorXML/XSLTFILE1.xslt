<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<h4>Libros disponibles</h4>
		<table border="2" cellpadding="5">
			<thead>
				<tr>
					<th>NombreLibro</th>
					<th>Autor</th>
					<th>Editorial</th>
					<th>Precio</th>
				</tr>
			</thead>
			<tbody>
				<xsl:for-each select="Inventario/Libro[Precio>70]">
					<tr>
						<td><xsl:value-of select="NombreLibro"/></td>
						<td><xsl:value-of select="Autor"/></td>
						<td><xsl:value-of select="Editorial"/></td>
						<td>S<xsl:choose>
								<xsl:when test="Autor='xxx'">
									<xsl:value-of select="Precio * 2"/>
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="Precio"/>
								</xsl:otherwise>
							</xsl:choose>
						</td>
					</tr>
				</xsl:for-each>
			</tbody>
		</table>
	</xsl:template>

</xsl:stylesheet>

