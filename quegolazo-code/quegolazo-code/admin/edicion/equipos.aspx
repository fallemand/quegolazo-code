<%@ Page Title="" Language="C#" MasterPageFile="~/admin/edicion/edicion.master" AutoEventWireup="true" CodeBehind="equipos.aspx.cs" Inherits="quegolazo_code.admin.edicion.equipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeaderEdicion" runat="server">
    <script type="text/javascript" src="/resources/js/jquery.multi-select.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentEdicion" runat="server">
     <div class="panel panel-default">
        <div class="panel-heading panel-heading-master">
            <span class="glyphicon glyphicon-cog"></span>
            Seleccionar Equipos
        </div>
        <div class="panel-body">
            <asp:ListBox ClientIDMode="Static" ID="lstEquiposSeleccionados" runat="server" SelectionMode="Multiple"></asp:ListBox>
            <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
            </asp:Panel>
        </div>
        <div class="panel-footer clearfix ">
            <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn btn-success pull-right" OnClick="btnSiguiente_Click"/>
        </div>
    </div>
    <script>
        $('#lstEquiposSeleccionados').multiSelect({
            selectableHeader: "<div class='well well-sm alert-success nomargin-bottom'>Listado de Equipos</div>",
            selectionHeader: "<div class='well well-sm alert-success nomargin-bottom'>Equipos Seleccionados</div>",
        });
    </script>
</asp:Content>