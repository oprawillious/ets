<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="NewRequisition.aspx.vb" Inherits="ETS.NewRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div class="Alignments">
        <h3>Requisition > New Requisition</h3>
        <hr class="greenLine" />
        <br />
        <br />
        <div style="text-align: center; font-size: 30px;">
            <h3>
                <asp:Label ID="lblPageCaption" runat="server" Text="New Requisition"></asp:Label></h3>
        </div>

        <br />

        <table>

            <tr>
                <td><label for="inputPassword" class="col-sm-2 col-form-label">Category<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListPeripheralDescription"  runat="server" DataValueField="DESCRIPTION" class="form-control form-control-sm" OnSelectedIndexChanged="DropListItemDesc_SelectedIndexChanged" aria-label=".form-control-sm example" required="required" AutoPostBack="true"></asp:DropDownList></td>
            </tr>

            <tr>
                <td><label for="inputPassword" class="col-sm-2 col-form-label">Description<span style="color: red;"> *</span></label></td>
                <td> <asp:DropDownList ID="DropListCatdesc" runat="server" DataValueField="DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" AutoPostBack="true"></asp:DropDownList></td>
            </tr>

            <tr>
                <td> <label for="inputPassword" class="col-sm-2 col-form-label">Quantity:</label></td>
                <td style="width: 400px;"><asp:TextBox ID="txtQuantity" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
            </tr>

             <tr>
                <td> <asp:Label ID="lblRemarks" runat="server" class="col-sm-2 col-form-label" for="inputRemarks"> Remarks</asp:Label></td>
                <td><asp:TextBox ID="txtRemarks" runat="server" aria-label=".form-control-sm example" class="form-control form-control-sm" placeholder="Reason For Request" required="required" TextMode="MultiLine"></asp:TextBox></td>
            </tr>

        </table>
        <br />
        <asp:Label ID="msg1" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnSaveRequest" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnSaveRequest_Click" /><br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <br />
        <hr />

    </div>
    <asp:HiddenField ID="hdIDRequsition" runat="server" />
</asp:Content>
