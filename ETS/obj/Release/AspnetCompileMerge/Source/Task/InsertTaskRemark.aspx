<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertTaskRemark.aspx.vb" Inherits="ETS.InsertTaskRemark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="Alignments">
        <h3>Task Remark</h3>
        <hr class="greenLine" />
        <br />
</div>
    <br />
    <br />
    <div class="box-shadow center" style="width: 35%;">
        <h5 class="form-title"><asp:Label ID="lblCreateUser" runat="server" Text="Task Remark"></asp:Label></h5>
        <table>
        <tr>
            <td>
                <label for="inputDescription" class="col-sm-3 col-form-label">Task Remarks<span style="color: red;"> *</span></label></td>
            <td>
                <asp:TextBox ID="txttaskremarks" runat="server" class="form-control form-control-sm" placeholder="Enter Task Remarks..." aria-label=".form-control-sm example" required="required" TextMode="MultiLine"></asp:TextBox></td>
        </tr>


        <tr>
            <td>
                <label for="inputDescription" class="col-sm-2 col-form-label">Task Description: </label>
            </td>
            <td>
                <asp:Label ID="lblDescription" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <label for="inputAssignedTo" class="col-sm-2 col-form-label">App Category: </label>
            </td>
            <td>
                <asp:Label ID="lblCategory" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>

        <tr>
            <td>
                <label for="inputAssignedTo" class="col-sm-2 col-form-label">Task Type: </label>
            </td>
            <td>
                <asp:Label ID="lblTaskType" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>

            <tr>
            <td>
                <label for="lblExpectedStartDate" class="col-sm-2 col-form-label">Expected Start Date: </label>
            </td>
            <td>
                <asp:Label ID="lblExpectedStartDate" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>
<tr>
            <td>
                <label for="lblExpectedEndDate" class="col-sm-2 col-form-label">Expected End Date: </label>
            </td>
            <td>
                <asp:Label ID="lblExpectedEndDate" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>
    </table>

        <br />
        <asp:Button ID="btnTaskRemarks" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnTaskRemarks_Click" /><br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="" Style="color: red;"></asp:Label><br />
        <br />
       
    </div>
</asp:Content>
