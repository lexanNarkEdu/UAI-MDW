<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes"/>
	<xsl:template match="/">
		<Categorias>
			<xsl:for-each select="Categorias/Categoria">
				<Categoria>
					<IdCategoria>
						<xsl:value-of select="IdCategoria"/>
					</IdCategoria>
					<NombreCategoria>
						<xsl:value-of select="NombreCategoria"/>
					</NombreCategoria>
					<CantidadProductos>
						<xsl:value-of select="CantidadProductos"/>
					</CantidadProductos>
				</Categoria>
			</xsl:for-each>
		</Categorias>
	</xsl:template>
</xsl:stylesheet>