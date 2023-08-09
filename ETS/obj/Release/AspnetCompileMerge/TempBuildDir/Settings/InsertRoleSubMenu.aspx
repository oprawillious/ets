<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertRoleSubMenu.aspx.vb" Inherits="ETS.InsertRoleSubMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   
    <br />   

  <div class="Alignments">
    <h3>Settings > Menu Setup > View Role Sub Menu > Assign Sub Menu</h3>
    <hr class="greenLine"/><br />
    <h3><asp:label ID="lblCreateUser" runat="server" Text="Assign Sub Menu"></asp:label></h3><br />

    <table>
        <tr>
            <td><label for="inputPassword" class="col-sm-3 col-form-label">User Group<span style="color: red;"> *</span></label></td>
            <td><asp:DropDownList ID="DropListUserGroup" runat="server" DataValueField="ROLE_DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><label for="inputPassword" class="col-sm-3 col-form-label">Menu<span style="color: red;"> *</span></label></td>
            <td><asp:DropDownList ID="DropListMenu" runat="server" DataValueField="DESCRIPTION" AutoPostBack="true" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required" OnSelectedIndexChanged="DropListMenu_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><label for="inputPassword" class="col-sm-3 col-form-label">Sub Menu<span style="color: red;"> *</span></label></td>
            <td><asp:DropDownList ID="DropListSubMenu" runat="server" DataValueField="DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required"></asp:DropDownList></td>
        </tr>
    </table>
<br />

   <asp:Button ID="btnSaveRoleSubMenu" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnSaveRoleSubMenu_Click"/><br/><br/>
   <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <hr/>

    </div>
</asp:Content>
