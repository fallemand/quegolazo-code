<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="personalizacion-edicion.aspx.cs" MasterPageFile="~/admin/admin.Master" Inherits="quegolazo_code.admin.personalizacion_edicion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <div class="container padding-top">
        <div class="row">
            <div class="container">
          <h2>Configuración de la Edición</h2>
                  <div class="form-group">
                 <h4>¿Desea trabajar con jugadores?</h4>
                 <div class="input-group">
                <asp:RadioButton ID="rbJugadores_si" OnCheckedChanged="rbJugadores_si_CheckedChanged" runat="server" Text="Si" GroupName="GJugadores"  CssClass="form-control " />
                <asp:RadioButton ID="rbJugadores_no" runat="server" Text="No" CssClass="form-control" GroupName="GJugadores" />
                     </div>
                <asp:Panel ID="Panel_jugadores" Visible="TRUE" runat="server">
                      <div class="form-group">
                    <h5>• ¿Desea registrar cuáles jugadores juegan cada partido?</h5>
                          <div class="input-group">
                <asp:RadioButton ID="rb1_si" runat="server" Text="Si" CssClass="form-control " GroupName="G1"  />
                <asp:RadioButton ID="rb1_no" runat="server" Text="No" CssClass="form-control" GroupName="G1" />
                          </div><br />
                          <h5>• ¿Desea registrar los cambios de jugadores que tienen lugar en un partido?</h5>
                <div class="input-group">
                 <asp:RadioButton ID="rb2_si" runat="server" Text="Si" CssClass="form-control " GroupName="G2"/>
                <asp:RadioButton ID="rb2_no" runat="server" Text="No" CssClass="form-control" GroupName="G2"/>
                        </div>  <br />
                           <h5>• ¿Desea registrar qué jugador realizó los goles?</h5>
                          <div class="input-group">
                <asp:RadioButton ID="rb3_si" runat="server" Text="Si" CssClass="form-control " GroupName="G3" />
                <asp:RadioButton ID="rb3_no" runat="server" Text="No" CssClass="form-control" GroupName="G3" />
                          </div>
                              <br />
                          <h5>• ¿Desea registrar tarjetas aplicadas a cada jugador?</h5>
                          <div class="input-group">
                <asp:RadioButton ID="rb4_si" runat="server" Text="Si" CssClass="form-control " GroupName="G4"/>
                <asp:RadioButton ID="rb4_no" runat="server" Text="No" CssClass="form-control" GroupName="G4" />
                          </div>
                          </div>
                </asp:Panel>
                        <h4>• ¿Desea trabajar con sanciones?</h4>
                 <div class="input-group">
                <asp:RadioButton ID="rbSanciones_si" OnCheckedChanged="rbSanciones_si_CheckedChanged"  runat="server" Text="Si" CssClass="form-control " GroupName="G5" />
                <asp:RadioButton ID="rbSanciones_no" runat="server" Text="No" CssClass="form-control" GroupName="G5"/>
                     </div>
                     <asp:Panel ID="Panel_sanciones" Visible="TRUE" runat="server">
                      <div class="form-group">
                 
                              <br />
                          <h5>• ¿Desea registrar sanciones a Jugadores?</h5>
                          <div class="input-group">
                <asp:RadioButton ID="rb6_si" runat="server" Text="Si" CssClass="form-control " GroupName="G7"/>
                <asp:RadioButton ID="rb6_no" runat="server" Text="No" CssClass="form-control" GroupName="G7"/>
                          </div>
                          </div>
                </asp:Panel>
                        <h4>• ¿Trabaja con árbitros?</h4>
                 <div class="input-group">
                <asp:RadioButton ID="rbArbitros_si" OnCheckedChanged="rbArbitros_si_CheckedChanged" runat="server" Text="Si" CssClass="form-control " GroupName="G8" />
                <asp:RadioButton ID="rbArbitros_no" runat="server" Text="No" CssClass="form-control" GroupName="G8"/>
                     </div>
                     <asp:Panel ID="Panel_Arbitros" Visible="TRUE" runat="server">
                      <div class="form-group">
                    <h5>•	¿Asigna árbitros a cada partido? </h5>
                          <div class="input-group">
                <asp:RadioButton ID="rb7_si" runat="server" Text="Si" CssClass="form-control " GroupName="G9"/>
                <asp:RadioButton ID="rb7_no" runat="server" Text="No" CssClass="form-control" GroupName="G9" />
                          </div>
                              <br />
                          <div class="input-group">
                          <h5>•  ¿Cuántos árbitros asignan por partido?</h5>
                          <asp:TextBox ID="txt_cantidadArbitros" CssClass="form-control" runat="server"></asp:TextBox>
                          </div>
                              <br />
                          <h5>•  ¿Registra el desempeño del árbitro?</h5>
                          <div class="input-group">
                <asp:RadioButton ID="rb8_si" runat="server" Text="Si" CssClass="form-control " GroupName="G10" />
                <asp:RadioButton ID="rb8_no" runat="server" Text="No" CssClass="form-control" GroupName="G10"/>
                          </div>
                          </div>
                </asp:Panel>
                      <h4>• ¿Trabaja con Canchas?</h4>
                 <div class="input-group">
                <asp:RadioButton ID="rbCanchas_si" runat="server" Text="Si" OnCheckedChanged="rbCanchas_si_CheckedChanged" CssClass="form-control " GroupName="G11" />
                <asp:RadioButton ID="rbCanchas_no" runat="server" Text="No" CssClass="form-control" GroupName="G11" />
                     </div>
                     <asp:Panel ID="Panel_Canchas" Visible="TRUE" runat="server">
                      <div class="form-group">
        
                          <h5>•  ¿Dónde se lleva a cabo el torneo?</h5>
                          <div class="input-group">
                <asp:RadioButton ID="rb_ComplejosEdicion" runat="server" Text="Uno/varios complejos" CssClass="form-control " GroupName="G13" />
                <asp:RadioButton ID="rb_canchaEquipo" runat="server" Text="Canchas de los equipos participantes" CssClass="form-control" GroupName="G13" />
                          </div>
                          </div>
                </asp:Panel>

                     
                      </div>
               
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnRegistrar_Click" CssClass="btn btn-success pull-right causesValidation" />
                    
            </div>
        </div>
    </div>


</asp:Content>
