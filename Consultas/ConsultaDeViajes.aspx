<%@ Page Language="C#" MasterPageFile="~/MasterPageConsulta.master" AutoEventWireup="true" CodeFile="ConsultaDeViajes.aspx.cs"
    Inherits="ConsultaDeViajes" %>

<%@ Register assembly="Controles" namespace="Controles" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Cod Destino:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDestinos" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Compania:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCompania" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Desde: "></asp:Label>
                </td>
                <td>
                    <cc1:NuevoCalendario ID="calDesde" runat="server" />
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Hasta: "></asp:Label>
                </td>
                <td>
                    <cc1:NuevoCalendario ID="calHasta" runat="server" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" 
                        onclick="btnFiltrar_Click" CssClass="Button1" ValidationGroup="Filtro" />
                </td>
                <td>
                    <asp:Button ID="btnBorrarFiltro" runat="server" onclick="btnBorrarFiltro_Click" 
                        Text="Borrar Filtro" CssClass="Button1" CausesValidation="False" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
        <asp:Label ID="lblError" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">
        <asp:Repeater ID="RTViajes" runat="server" onitemcommand="RTViajes_ItemCommand">

            <ItemTemplate>
                <table class="listadoViajes">
                    <tr class="fila">
                        <td class= "celda">
                            <asp:Label ID="IDNumero" runat = "server" Text="Numero: "></asp:Label>
                            <asp:Label ID="TxtNumero" runat="server" Text='<%# Bind("Numero") %>'></asp:Label>
                            <br />
                        </td>
                        <td class= "celda">
                            <asp:Label ID="IDCompania" runat = "server" Text="Compania: "></asp:Label>
                            <asp:Label ID="TxtCompania" runat="server" Text='<%# Bind("Compania.Nombre") %>'></asp:Label>
                            <br />
                        </td>
                        <td class= "celda">
                            <asp:Label ID="IDPartida" runat = "server" Text="Partida: "></asp:Label>
                            <asp:Label ID="TxtPartida" runat="server" Text='<%# Bind("FechaSalida") %>'></asp:Label>
                            <br />
                        </td>
                        <td class= "celda">
                            <asp:Label ID="IDArribo" runat = "server" Text="Arribo: "></asp:Label>
                            <asp:Label ID="TxtArribo" runat="server" Text='<%# Bind("FechaArribo") %>'></asp:Label>
                            <br />
                        </td>
                        <td class= "celda">
                            <asp:Label ID="IDDestino" runat = "server" Text="Destino: "></asp:Label>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Destino.Cod") %>'></asp:Label>
                            <asp:Label ID="Label6" runat = "server" Text=" - "></asp:Label>
                            <asp:Label ID="TxtDestino" runat="server" Text='<%# Bind("Destino.Ciudad") %>'></asp:Label>
                            <br />
                        </td>
                        <td class= "celda">
                            <asp:Button ID="btnVerViaje" runat="server" CommandName="Ver" Style="text-align: center"
                                Text="Ver Detalle" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>

        </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
