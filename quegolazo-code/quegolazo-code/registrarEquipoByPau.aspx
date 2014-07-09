﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrarEquipoByPau.aspx.cs" Inherits="quegolazo_code.registrarEquipoByPau" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <input type="text" class="form-control" id="txtTorneoAsociado" runat="server" name="nombreTorneoEdicion" placeholder="Nombre del Torneo" disabled>
         <input type="hidden" class="form-control" id="txtIdTorneo" runat="server">
         <span class="help-block">Torneo para el cual esta creando un nuevo Equipo<br />
         </span>Nombre:  <input type="text" class="form-control" id="txtNombreEquipo" runat="server" name="nombreEquipo" rangelength="3, 50" required="true" placeholder="Nombre del Equipo">
         <br />
    Color camiseta primario: <input type="text" class="form-control" id="txtColorCamisetaPrimario" runat="server" name="colorCamisetaPrimario" required="true" placeholder="Color Camiseta Primario">
         <br />
    Color camiseta secundario: <input type="text" class="form-control" id="txtColorCamisetaSecundario" runat="server" name="colorCamisetaSecundario" required="true" placeholder="Color Camiseta Secundario">
         <br />
    Director Técnico: <input type="text" class="form-control" id="txtDirectorTecnico" runat="server" name="directorTecnico" required="true" placeholder="Director Técnico">
         <br />
         <br />

    Delegados:
         <br />
         Delegado Principal<br />
         Nombre: <input type="text" class="form-control" id="txtNombreDelegadoPrincipal" runat="server" name="nombreDelegadoPrincipal" rangelength="3, 50" required="true" placeholder="Nombre del Delegado Principal">
         <br />
        Email: <input type="text" class="form-control" id="txtEmailDelegadoPrincipal" runat="server" name="emailDelegadoPrincipal" rangelength="5, 100" required="true" placeholder="Email del Delegado Principal">
         <br />
        Teléfono: <input type="text" class="form-control" id="txtTelefonoDelegadoPrincipal" runat="server" name="telefonoDelegadoPrincipal" required="true" placeholder="Telefono del Delegado Principal">
         <br />
        Domicilio: <input type="text" class="form-control" id="txtDomicilioDelegadoPrincipal" runat="server" name="domicilioDelegadoPrincipal" placeholder="Domicilio del Delegado Principal"><br />
         <input type="hidden" class="form-control" id="txtDelegadoPrincipalAgregado" runat="server">
         <asp:Button ID="btnAgregarDelegadoPrincipal" runat="server" Text="Agregar Delegado" OnClick="btnAgregarDelegadoPrincipal_Click" CausesValidation="False" />

         <br />

         <br />
        Delegado Opcional<br />
         Nombre: <input type="text" class="form-control" id="txtNombreDelegadoOpcional" runat="server" name="nombreDelegadoOpcional" rangelength="3, 50" placeholder="Nombre del Delegado Opcional">
         <br />
        Email: <input type="text" class="form-control" id="txtEmailDelegadoOpcional" runat="server" name="emailDelegadoOpcional" rangelength="5, 100" placeholder="Email del Delegado Opcional">
         <br />
        Teléfono: <input type="text" class="form-control" id="txtTelefonoDelegadoOpcional" runat="server" name="telefonoDelegadoOpcional"  placeholder="Telefono del Delegado Opcional">
         <br />
        Domicilio: <input type="text" class="form-control" id="txtDomicilioDelegadoOpcional" runat="server" name="domicilioDelegadoOpcional" placeholder="Domicilio del Delegado Opcional"><br />
        <input type="hidden" class="form-control" id="txtDelegadoOpcionalAgregado" runat="server"> 
        <asp:Button ID="btnAgregarDelegadoOpcional" runat="server" Text="AgregarDelegado" OnClick="btnAgregarDelegadoOpcional_Click" />
         <br />
         <br />
&nbsp;<asp:Button ID="btnRegistrarEquipo" runat="server" Text="Registrar Equipo" OnClick="btnRegistrarEquipo_Click" />

    </div>
    </form>
</body>
</html>