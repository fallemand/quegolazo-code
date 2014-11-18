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
            <asp:HiddenField ID="hfEquiposSeleccionados" ClientIDMode="Static" runat="server" />
            <asp:UpdatePanel ID="upSeleccionarEquipos" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSiguiente" EventName="Click"/>
                    <asp:AsyncPostBackTrigger ControlID="btnModificar" EventName="Click"/>
                </Triggers>
                <ContentTemplate>
                                       
                    <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger col-md-12 text-center" Visible="False">
                        <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="panel-footer clearfix ">
            <asp:Button ID="btnAtras" runat="server" Text="Atrás" CssClass="btn btn-success pull-left" OnClick="btnAtras_Click"/>
            <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn btn-success pull-right" OnClick="btnSiguiente_Click"/>
        </div>
    </div>
     <div class="modal fade bs-example-modal-sm" id="modificarEquipos" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title" id="myModalLabel">Modificar Equipo</h4>
                </div>
                <div class="modal-body">
                    Si modifica los equipos que participan en la edición, deberá volver a generar el fixture.¿Está seguro que desea continuar?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnModificar_Click" />
                </div>
            </div>
        </div>
    </div>
   
    <script>
        $('#lstEquiposSeleccionados').multiSelect({
            selectableHeader: "<div class='well well-sm alert-success nomargin-bottom'>Listado de Equipos <a href='#' id='select-all' class='btn btn-xs btn-default pull-right'>Seleccionar Todos <span class='glyphicon glyphicon-chevron-right'></span></a></div>",
            selectionHeader: "<div class='well well-sm alert-success nomargin-bottom'>Equipos Seleccionados: <span id='spanSeleccionados'>0</span>  <a href='#' id='deselect-all' class='btn btn-xs btn-default pull-right'><span class='glyphicon glyphicon-chevron-left'></span> Quitar Todos</a></div>",
            afterSelect: function (values) {
                $('#hfEquiposSeleccionados').val($('#hfEquiposSeleccionados').val() + values + ',');
                actualizarCantidades();
            },
            afterDeselect: function (values) {
                $('#hfEquiposSeleccionados').val($('#hfEquiposSeleccionados').val().replace(values + ',', ''));
                actualizarCantidades();
            }
        });
        $('#select-all').click(function () {
            $('#lstEquiposSeleccionados').multiSelect('select_all');
            return false;
        });
        $('#deselect-all').click(function () {
            $('#lstEquiposSeleccionados').multiSelect('deselect_all');
            $('#hfEquiposSeleccionados').val("");
            $("#spanSeleccionados").text("0");
            return false;
        });
        $(function () {
            actualizarCantidades();
        });
        function actualizarCantidades() {
            var arr = $("#hfEquiposSeleccionados").val().split(",");
            var valor = $.grep(arr, function( a ) {
                return a != "";
            }).length;
            $("#spanSeleccionados").text(valor);                
        }
    </script>
</asp:Content>