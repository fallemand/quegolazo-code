<%@ Page Title="" Language="C#" MasterPageFile="~/admin/edicion/edicion.master" AutoEventWireup="true" CodeBehind="fases.aspx.cs" Inherits="quegolazo_code.admin.edicion.fases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeaderEdicion" runat="server">    
    <script src="../../resources/js/jquery.ui/jquery-ui.js"></script>  
    <script src="../../resources/js/quegolazo.js"></script>
    <script src="../../resources/js/widgetFases.js"></script>
    <script>  
        $(document).ready(function () {
            $("#panelFracaso").hide();
        });
    </script>
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentEdicion" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading panel-heading-master">
            <span class="glyphicon glyphicon-cog"></span>
            Administrar Fases
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12" id="contenedorFases">
                    <p class="bs-component">    
                        <input id="agregarFase" type="button" class="btn btn-lg btn-success" onclick="$('#contenedorFases').generadorDeFases('crearFase');" value="Agregar Fase" />                                        
                     
                </div>
            </div>
        <div class="row">
           <div class="col-md-12">
              <div class="panel-group" id="accordionFases">   
              
            </div>
          </div>
        </div> 
        <asp:Panel ID="panelFracaso" runat="server" CssClass="alert alert-danger margin-top" ClientIDMode="Static" >                
                <p id="msjFracaso" runat="server" ClientIDMode="Static"></p>
            </asp:Panel>
        
    </div>
        <div class="panel-footer clearfix ">
            <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="btn btn-success pull-right" OnClientClick="return $('#contenedorFases').generadorDeFases('guardarFasesEnSesion');" OnClick="btnSiguiente_Click"/>
             <%--<input id="btnSiguiente" type="button" class="btn btn-success pull-right"  onclick="$('#contenedorFases').generadorDeFases('guardarFasesEnSesion');" value="Siguiente" />--%>         
        </div>
    </div>
</asp:Content>
