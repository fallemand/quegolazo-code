﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/edicion/edicion.master" AutoEventWireup="true" CodeBehind="equipos.aspx.cs" Inherits="quegolazo_code.admin.edicion.equipos" %>
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
            <asp:HiddenField ID="hfEquiposSeleccionados" ClientIDMode="Static" runat="server" />
            <asp:UpdatePanel ID="upSeleccionarEquipos" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSiguiente" EventName="Click"/>
                </Triggers>
                <ContentTemplate>                    
                    <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger col-md-12 text-center" Visible="False">
                        <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="panel-footer clearfix ">
            <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn btn-success pull-right" OnClick="btnSiguiente_Click"/>
        </div>
    </div>
    <script>
        $('#lstEquiposSeleccionados').multiSelect({
            selectableHeader: "<div class='well well-sm alert-success nomargin-bottom'>Listado de Equipos <a href='#' id='select-all' class='btn btn-xs btn-default pull-right'>Seleccionar Todos <span class='glyphicon glyphicon-chevron-right'></span></a></div>",
            selectionHeader: "<div class='well well-sm alert-success nomargin-bottom'>Equipos Seleccionados <a href='#' id='deselect-all' class='btn btn-xs btn-default pull-right'><span class='glyphicon glyphicon-chevron-right'></span> Quitar Todos</a></div>",
            afterSelect: function (values) {
                $('#hfEquiposSeleccionados').val($('#hfEquiposSeleccionados').val() + values + ',');
            },
            afterDeselect: function (values) {
                $('#hfEquiposSeleccionados').val($('#hfEquiposSeleccionados').val().replace(values + ',', ''));
            }
        });
        $('#select-all').click(function () {
            $('#lstEquiposSeleccionados').multiSelect('select_all');
            return false;
        });
        $('#deselect-all').click(function () {
            $('#lstEquiposSeleccionados').multiSelect('deselect_all');
            return false;
        });
    </script>
</asp:Content>