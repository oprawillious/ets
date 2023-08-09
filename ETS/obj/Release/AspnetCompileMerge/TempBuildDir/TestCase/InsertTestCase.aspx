<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertTestCase.aspx.vb" Inherits="ETS.InsertTestCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
 
    <div style="width: 1800px; margin: 0 auto;">
        <h3>TestCase > New TestCase</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>
    <br />
    <br />
    <div class="box-shadow center" style="width: 35%;">
        
        <table>
            <tr>
                <td><label for="inputDescription" class="col-sm-3 col-form-label">Description<span style="color: red;"> *</span></label></td>
                <td><asp:TextBox ID="txtDescription" runat="server" class="form-control form-control-sm" placeholder="description..." aria-label=".form-control-sm example" required="required" TextMode="MultiLine"></asp:TextBox></td>
            </tr>

            <tr>
                <td><label for="inputSubType" class="col-sm-3 col-form-label">Task<span style="color: red;"> *</span></label></td>
                <td><asp:DropDownList ID="DropListSubType" runat="server" DataValueField="SUB_DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required"></asp:DropDownList></td>
            </tr>
        </table>

        <br />
        <asp:Button ID="btnCreatTestCase" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnCreateNewTestCase_Click" /><br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="" Style="color: red;"></asp:Label><br />
        <br />
       
    </div>
</asp:Content>
