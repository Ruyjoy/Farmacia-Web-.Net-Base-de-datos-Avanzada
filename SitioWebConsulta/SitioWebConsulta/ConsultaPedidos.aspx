<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultaPedidos.aspx.cs" Inherits="ConsultaPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table style="width:100%;">
        <tr>
                <td align="justify" colspan="2" class="titulo">
                    Estado de pedido</td>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style14">
                    &nbsp;</td>
                
            </tr>
            <tr>
                <td class="style11">
                    Ingrese un codigo</td>
                <td class="style14">
                    <asp:TextBox ID="txtbuscar" runat="server"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                        onclick="btnBuscar_Click" />
                </td>
                
            </tr>
            <tr>
                <td class="style11">
                    Estado del pedido</td>
                <td class="style15">
                    <asp:Label ID="lblEstado" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style14">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style15">
                    <asp:Label ID="LblError" runat="server" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style14
        {
            width: 278px;
        }
        .style15
        {
            font-family: "Microsoft JhengHei";
            margin-left: 40px;
            width: 278px;
            text-align: right;
            height: 28px;
        }
        .style17
        {
            height: 55px;
        }
    </style>
</asp:Content>


