<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlViaje.ascx.cs"
    Inherits="User_Controls_ControlViaje" %>
<table class="tablaViaje">
    <tr>
        <td colspan="4">
            <asp:Label ID="Label14" runat="server" CssClass="encabezadoEntidad" Text="Datos del viaje seleccionado."></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
            <table class="tablas">
                <tr class="grillaFila">
                    <td class="gillaCelda">
                        <h2 class="encabezadoEntidad">
                            Viaje</h2>
                    </td>
                </tr>
                <tr class="grillaFila">
                    <td class="gillaCelda">
                        <asp:Label ID="Label10" runat="server" CssClass="etiqueta" Text="Numero: "></asp:Label>
                        <asp:Label ID="lblNumero" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="grillaFila">
                    <td class="gillaCelda">
                        <asp:Label ID="Label11" runat="server" CssClass="etiqueta" Text="Salida: "></asp:Label>
                        <asp:Label ID="lblFechaSalida" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="grillaFila">
                    <td class="gillaCelda">
                        <asp:Label ID="Label12" runat="server" CssClass="etiqueta" Text="Arribo: "></asp:Label>
                        <asp:Label ID="lblFechaArribo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="grillaFila">
                    <td class="gillaCelda">
                        <asp:Label ID="Label13" runat="server" CssClass="etiqueta" Text="Capacidad: "></asp:Label>
                        &nbsp;<asp:Label ID="lblCantidadAsientos" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="grillaFila">
                    <td class="gillaCelda">
                        <asp:Label ID="lblParadasT" runat="server" Text="Paradas: " CssClass="etiqueta"></asp:Label>
                        <asp:Label ID="lblParadas" runat="server"></asp:Label>
                        <asp:Label ID="lblServicioABordo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="grillaFila">
                    <td class="gillaCelda">
                        <asp:Label ID="lblDocumentost" runat="server" CssClass="etiqueta">Documentos: </asp:Label>
                        <asp:Label ID="lblDocumentos" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            &nbsp;
            <table class="tablas">
                <tr>
                    <td>
                        <h2 class="encabezadoEntidad">
                            Empleado</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label4" runat="server" Text="Nombre: " CssClass="etiqueta"></asp:Label>
                        <asp:Label ID="lblNombreEmpleado" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label5" runat="server" Text="C.I. :" CssClass="etiqueta"></asp:Label>
                        <asp:Label ID="lblCIEmpleado" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <table class="tablas">
                <tr>
                    <td>
                        <h2 class="encabezadoEntidad">
                            Destino</h2>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Codigo: " CssClass="etiqueta"></asp:Label>
                        <asp:Label ID="lblCodigoD" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" CssClass="etiqueta" Text="Facilidades"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Ciudad: " CssClass="etiqueta"></asp:Label>
                        <asp:Label ID="lblCiudadD" runat="server"></asp:Label>
                    </td>
                    <td rowspan="2">
                        <asp:ListBox ID="lbxFacilidades" runat="server" DataTextField="Facilidad"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Pais: " CssClass="etiqueta"></asp:Label>
                        <asp:Label ID="lblPaisD" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            &nbsp;
            <table class="tablas">
                <tr>
                    <td>
                        <h2 class="encabezadoEntidad">
                            Compania</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Nombre: " CssClass="etiqueta"></asp:Label>
                        <asp:Label ID="lblNombreComp" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Direccion: " CssClass="etiqueta"></asp:Label>
                        <asp:Label ID="lblDireccionComp" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Telefono: " CssClass="etiqueta"></asp:Label>
                        <asp:Label ID="lblTelComp" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
