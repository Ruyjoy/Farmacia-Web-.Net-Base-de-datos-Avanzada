<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="/">
      <table>
        <tr>
          <td style="border: thin double #800000">Codigo</td>
          <td style="border: thin double #800000">Nombre</td>
          <td style="border: thin double #800000">Tipo</td>
          <td style="border: thin double #800000">Descripcion</td>
          <td style="border: thin double #800000">Farmaceutica</td>
          <td style="border: thin double #800000">Direccion</td>
          <td style="border: thin double #800000">Telefono</td>
          <td style="border: thin double #800000">Email</td>
          <td style="border: thin double #800000">Precio</td>
          <td style="border: thin double #800000">Stock</td>
        </tr>
        <xsl:for-each select ="Medicamentos/Medicamento">
          <tr>
            <td style="background-color: #CCFFFF"><xsl:value-of select ="Codigo"/></td>
            <td style="background-color: #FFFF99"><xsl:value-of select ="Nombre"/></td>
            <td style="background-color: #CCFFFF"><xsl:value-of select ="Tipo"/></td>
            <td style="background-color: #FFFF99"><xsl:value-of select ="Descripcion"/></td>
            <td style="background-color: #CCFFFF"><xsl:value-of select ="Farmaceutica/Nombre"/></td>
            <td style="background-color: #FFFF99">
              <xsl:value-of select ="Farmaceutica/Direccion"/>
            </td>
            <td style="background-color: #CCFFFF">
              <xsl:value-of select ="Farmaceutica/Telefono"/>
            </td>
            <td style="background-color: #FFFF99">
              <xsl:value-of select ="Farmaceutica/Email"/>
            </td>
            <td style="background-color: #CCFFFF"><xsl:value-of select ="Precio"/></td>
            <td style="background-color: #FFFF99"><xsl:value-of select ="Stock"/></td>
          </tr>
        </xsl:for-each>
      </table>
      
    </xsl:template>
</xsl:stylesheet>
