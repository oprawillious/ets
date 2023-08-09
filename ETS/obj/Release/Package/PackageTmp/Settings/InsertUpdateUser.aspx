<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertUpdateUser.aspx.vb" Inherits="ETS.InsertUpdateUser" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<%@ Import Namespace="ETS.General" %>
<%@ Import Namespace="ETS.DataBase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
   
        <ContentTemplate>
            <div class="Alignments1">
        <br />
        <h3>Settings > View Application Users > Create Edit User</h3>
        <hr class="greenLine" />
        <br />
    </div>
           <br /> <br />
            
             <div class="box-shadow center" style="width: 80%;">
                  <h5 class="form-title"><asp:Label ID="lblCreateUser" runat="server" Text=""></asp:Label></h5>
        

        <table>
            <tr>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">First Name<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" class="form-control form-control-sm" placeholder="first name..." aria-label=".form-control-sm example" required="required"></asp:TextBox></td>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">Last Name<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" class="form-control form-control-sm" placeholder="last name..." aria-label=".form-control-sm example" required="required"></asp:TextBox></td>
            </tr>

            <tr>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">Username<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server" class="form-control form-control-sm" placeholder="username..." aria-label=".form-control-sm example" required="required"></asp:TextBox></td>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">Password<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-control form-control-sm" placeholder="password..." aria-label=".form-control-sm example" required="required"></asp:TextBox></td>
            </tr>

            <tr>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">Confirm Password<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CausesValidation="false" OnTextChanged="txtConfirmPassword_TextChanged" class="form-control form-control-sm" placeholder="confirm password..." aria-label=".form-control-sm example" required="required"></asp:TextBox></td>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">Email<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" class="form-control form-control-sm" placeholder="email..." aria-label=".form-control-sm example" required="required"></asp:TextBox></td>
            </tr>

            <tr>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">User Group<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListUserGroup" runat="server" AutoPostBack="true" DataValueField="ROLE_DESCRIPTION" CssClass="form-control form-control-sm" aria-label=".form-control-sm example" required="required"></asp:DropDownList></td>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">Department<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListDept" runat="server" DataValueField="DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required" OnSelectedIndexChanged="DropListDept_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>

            <%--<%If IsInRole(Session("R"), Roll_Kind.Administrator) Then %>--%>
            <%If Roll_Kind.Administrator Then %>
            <tr>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">HR Manager<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListHrManager" runat="server" DataValueField="HR_MANAGER" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required"></asp:DropDownList></td>
                <td>
                    <label for="inputPassword" class="col-sm-3 col-form-label">Manager<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListManager" runat="server" DataValueField="MANAGER_NAME" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:DropDownList></td>
            </tr>
            <%End If%>
        </table>
        <br />

        <asp:Button ID="btnSaveUser" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnSaveUser_Click" /><br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

    </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:HiddenField runat="server" ID="hdS_UserRole" />
</asp:Content>
