 <%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.torneo.master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="quegolazo_code.admin.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdminTorneo" runat="server">
    <!-- Contenido -->
    <asp:Literal ID="LitEdicion" runat="server"></asp:Literal>
    <div class="container padding-top">
        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon flaticon-trophy5"></span>
                    Tabla de Posiciones
                </div>
                <div class="panel-body">
                    <asp:GridView ID="gvPosiciones" runat="server" CssClass="table"></asp:GridView>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-calendar"></span>
                    Fixture de la Fecha
                    <asp:Literal ID="Literal1" Text="1" runat="server"></asp:Literal>
                </div>
                <div class="panel-body">
                      <asp:GridView ID="gvFixture" runat="server" CssClass="col-md-6"></asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
     <div class="col-md-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon-circle-arrow-up"></span>
                    Avance del Torneo
                </div>
                <div class="panel-body" >
                     
                </div>
            </div>
        </div>
    <div class="col-md-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon-circle-arrow-up"></span>
                    Amarillas y Rojas
                </div>
                ( como en la pag de olé)
            </div>
        </div>
      <div class="col-md-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="glyphicon-circle-arrow-up"></span>
                    Avance de la Fecha 
                </div>
                
            </div>
        </div>
        </div>
    <!-- Contenido -->
    <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger" Visible="False">
                            <asp:Literal ID="litFracaso" runat="server"></asp:Literal>
                        </asp:Panel>
</asp:Content>
