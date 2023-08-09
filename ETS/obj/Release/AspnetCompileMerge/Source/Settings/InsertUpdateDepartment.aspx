<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertUpdateDepartment.aspx.vb" Inherits="ETS.InsertUpdateDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="Alignments">
        <br/>
        <h3>Department > Create Department</h3>
        <hr class="greenLine" />
        <br />
        <h2>Create Department </h2>
        <table>
            <tr>
                <td><label for="searchDept" class="col-sm-2 col-form-label">Department: </label></td>
                <td style="width: 400px;"><asp:TextBox ID="txtCreateDepartment" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
            </tr>
        </table>
        <asp:Button ID="Button1" runat="server" Text="Confirm" class="btn btn-primary btn-sm mb-3" CssClass="mybutton" OnClick="btn_CreateDepartmentClick" />
        <br />
          <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>

    <asp:HiddenField ID="hdIdDepartment" runat="server" />
</asp:Content>
