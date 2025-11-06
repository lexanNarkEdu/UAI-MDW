<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
	<xsl:output method="xml" indent="yes"/>

	<xsl:template match="/">
		<table>
			<thead>
				<tr>
					<th>Apellido</th>
					<th>Sueldo</th>
					<th>Condicion</th>
				</tr>
			</thead>
			<tbody>
				<xsl:for-each select="Profesores/Profesor">
					<tr>
						<td><xsl:value-of select="Apellido" /></td>
						<xsl:choose>
							<xsl:when test="Condicion='titular'">
								<td>
									<xsl:value-of select="Sueldo * 2"/>
								</td>
							</xsl:when>
							<xsl:otherwise>
								<td>
									<xsl:value-of select="Sueldo"/>
								</td>
							</xsl:otherwise>
						</xsl:choose>
						<td>
							<xsl:value-of select="Condicion" />
						</td>
					</tr>
				</xsl:for-each>
			</tbody>
		</table>
	</xsl:template>
</xsl:stylesheet>
