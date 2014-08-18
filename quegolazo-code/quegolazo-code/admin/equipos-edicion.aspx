<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="equipos-edicion.aspx.cs" Inherits="quegolazo_code.admin.equipos_edicion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rptEquiposAEdicion" runat="server" OnItemCommand="rptEquiposAEdicion_ItemCommand" OnItemDataBound="rptEquiposAEdicion_ItemDataBound">
            <ItemTemplate>
                <tr>
                    <asp:CheckBox ID="ckbEquiposAEdicion" runat="server"/>
                </tr>
                <tr>
                    <td><%# Eval("nombre") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Equipos" OnClick="btnAgregar_Click" />  
    </div>
    </form>
</body>
</html>
