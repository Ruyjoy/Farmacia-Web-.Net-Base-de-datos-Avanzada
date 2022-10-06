<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="ConsultaMedicamentos, App_Web_5m3jffy4" %>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table style="width:100%;">
            <tr>
                <td align="justify" colspan="2" class="titulo">
                    Medicamentos en Stock</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    </td>
                <td class="style9">
                    </td>
                <td class="style9">
                    </td>
            </tr>
            <tr>
                <td align="right" class="style11">
                    <asp:Label ID="Label1" runat="server" Text="Tipo de medicamento:"></asp:Label>
                </td>
                <td align="left" class="style7">
                    <asp:DropDownList ID="ddlTipos" runat="server" AutoPostBack="True" 
                        Height="39px" onselectedindexchanged="ddlTipos_SelectedIndexChanged" 
                        Width="143px">
                        <asp:ListItem Selected="True">Todos</asp:ListItem>
                        <asp:ListItem>Cardiologico</asp:ListItem>
                        <asp:ListItem>Diabeticos</asp:ListItem>
                        <asp:ListItem>Otros</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style7">
                    </td>
            </tr>
            <tr>
                <td class="style4">
                    </td>
                <td class="style5">
                    </td>
                <td class="style5">
                    </td>
            </tr>
            <tr>
                <td align="right" class="style18">
       <asp:Repeater ID="rplistado" runat="server" OnItemCommand="rpResultado_ItemCommand">
            <ItemTemplate>
                <table id="tablaRepeater">
                    <tr bgcolor="#6666FF">                        <td>
                      
                            Nombre Medicamento: <br />  
                            <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                            <br /> 
                        </td>
                        
                        <td>
                            <br />
                            <asp:Button ID="Button1" runat="server" CommandName="Listar" 
                                style="text-align: center" Text="Mostrar Detalle" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <table>
                    <tr bgcolor="#333377">
                        <td>
                            Nombre Medicamento: <br />  
                            <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                            <br /> 
                        </td>
                        
                        <td>
                            <br />
                            <asp:Button ID="Button1" runat="server" CommandName="Listar" 
                                style="text-align: center" Text="Mostrar detalle" />
                        </td>
                    </tr>
                </table>
            </AlternatingItemTemplate>
        </asp:Repeater>
                </td>
                <td align="left">
        <asp:Xml ID="XmlListar" runat="server" 
            TransformSource="~/App_Data/Medicamentos.xslt"></asp:Xml>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" class="style16">
                    </td>
                <td align="left" class="style11">
                    </td>
                <td class="style11">
                    </td>
            </tr>
            <tr>
                <td align="right" class="style17">
                    </td>
                <td align="left" class="style11">
                    <asp:Label ID="LblError" runat="server"></asp:Label>
                </td>
                <td class="style13">
                    </td>
            </tr>
        </table>
</asp:Content>

