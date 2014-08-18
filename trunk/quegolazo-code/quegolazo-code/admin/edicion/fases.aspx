<%@ Page Title="" Language="C#" MasterPageFile="~/admin/edicion/edicion.master" AutoEventWireup="true" CodeBehind="fases.aspx.cs" Inherits="quegolazo_code.admin.edicion.fases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeaderEdicion" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentEdicion" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading panel-heading-master">
            <span class="glyphicon glyphicon-cog"></span>
            Administrar Fases
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12">
                    <p class="bs-component"><asp:Button ID="btnAgregarFase" runat="server" Text="Agregar una Fase" CssClass="btn btn-lg btn-success" /></p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Fase N°1</a>
                                </h4>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
            </asp:Panel>
        </div>
        <div class="panel-footer clearfix ">
            <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn btn-success pull-right" />
        </div>
    </div>
</asp:Content>
