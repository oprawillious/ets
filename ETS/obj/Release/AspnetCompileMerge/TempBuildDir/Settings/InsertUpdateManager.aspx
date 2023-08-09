<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertUpdateManager.aspx.vb" Inherits="ETS.InsertUpdateManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <br />
    <div class="Alignments">
        <br />
        <h3>Manager > Create Manager </h3>
        <hr class="greenLine"/>
        <br />

     
        <asp:Label ID="lblCaption" runat="server" Text="Create New Manager"></asp:Label><br /><br />
        <table>
            <tr>
                <td><label for="inputPassword" class="col-sm-3 col-form-label">Department<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListUserDepartment" runat="server" DataValueField="DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required" AutoPostBack="true"></asp:DropDownList></td>
            </tr>

            <tr>
                <td><label for="searchDept" class="col-sm-2 col-form-label">First Name: </label></td>
                <td style="width: 400px;"><asp:TextBox ID="txtFirstname" runat="server" required="required" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
            </tr>

            <tr>
                <td> <label for="searchDept" class="col-sm-2 col-form-label">Last Name: </label> </td>
                <td style="width: 400px;"><asp:TextBox ID="txtLastname" runat="server" required="required" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
            </tr>

            <tr>
                <td><label for="searchDept" class="col-sm-2 col-form-label">Email: </label></td>
                <td style="width: 400px;"><asp:TextBox ID="txtEmail" runat="server" required="required" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
            </tr>

            <tr>
                <td><label for="leavepriviledge" class="col-sm-2 col-form-label">Leave Approval Priviledge:</label></td>
                <td>
                    <asp:DropDownList ID="DropDownAprroveHolidayPriviledge" runat="server">
                      <asp:ListItem></asp:ListItem>
                      <asp:ListItem>Yes</asp:ListItem>
                      <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr style="background-color: transparent;">
                <td><asp:CheckBox ID="chkHrmanager" runat="server" Text="Hr Manager" class="form-control form-control-sm" aria-label=".form-control-sm example"  /></td>
                <td><asp:CheckBox ID="chkAccountManager" runat="server" Text="Accounts Manager" class="form-control form-control-sm" aria-label=".form-control-sm example"  /></td>
            </tr>
        </table>
        <br />

        <asp:Button ID="btn_CreateManager" runat="server" Text="Confirm" class="btn btn-primary btn-sm mb-3" CssClass="mybutton" OnClick="btn_CreateManagerClick" /><br />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>

    <asp:HiddenField ID="hdManager_settings_id" runat="server" />
</asp:Content>
