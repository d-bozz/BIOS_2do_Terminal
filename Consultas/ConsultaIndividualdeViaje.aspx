<%@ Page Language="C#" MasterPageFile="~/MasterPageConsulta.master" AutoEventWireup="true"
    CodeFile="ConsultaIndividualdeViaje.aspx.cs" Inherits="ConsultaIndividualdeViaje" %>

<%@ Register Src="UserControls/ControlViaje.ascx" TagName="ControlViaje" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ControlViaje ID="ControlViaje1" runat="server" />
    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <br />
    <asp:LinkButton ID="lbtnVolver" runat="server" PostBackUrl="~/ConsultaDeViajes.aspx">Volver</asp:LinkButton>
</asp:Content>
