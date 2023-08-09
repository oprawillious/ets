<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewRequisition.aspx.vb" Inherits="ETS.ViewRequisition" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="Alignments2">
        <h3>Requisition > View Requisition</h3>
        <hr class="greenLine" />
        <br />
        <h2>Requisition </h2>

        <table>
            <tr>
                <td align="right" id="trlblEmpName" runat="server"> <label for="inputTaskNumber" class="col-sm-2 col-form-label">Employee Name: </label></td>
                <td id="trtxtEmpName" align="left" runat="server"><asp:TextBox ID="txtEmployeeName" runat="server" class="form-control form-control-sm" placeholder="Enter Employee Name..." Style="width: 250px;"></asp:TextBox></td>
                <td align="right"> <label for="" class="col-sm-2 col-form-label">Request Date: </label></td>
                <td align="left" style="width: 200px">
                    <asp:TextBox ID="txtRequestDate" runat="server" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" onfocus='this.blur();' />
                    <ajaxToolkit:CalendarExtender runat="server"
                        ID="txtDateReOpened0_CalendarExtender"
                        Format="MM/dd/yyyy"
                        PopupButtonID="ImageButton63"
                        TargetControlID="txtRequestDate"></ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator77"
                        runat="server"
                        ControlToValidate="txtRequestDate"
                        CssClass="txtObbligatorio"
                        Display="None"
                        SetFocusOnError="True" /></td>
                <td><asp:ImageButton ID="ImageButton63" ImageUrl="~/images/_calendar.png" Width="30px" runat="server" /></td>
                <td><asp:Button ID="btnSearchRequisition" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchRequisition_Click" /></td>
                <td><asp:Button ID="btnAddRequisition" runat="server" Text="Add Requisition" class="btn btn-primary btn-sm mb-3" OnClick="btnAddRequisition_Click" /></td>
            </tr>
        </table>

        <asp:GridView ID="gvViewRequisition"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            AllowPaging="True"
            PageSize="10"
            CssClass="table-bordered table-sm"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="98%">

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                      <asp:HiddenField ID="hdIdRequisition" runat="server" Value='<%# Eval("ID_REQUISITION")%>' />
                      <asp:HiddenField ID="hdIdUser" runat="server" Value='<%# Eval("ID_USER")%>' />
                      <asp:HiddenField ID="hdItemCategory" runat="server" Value='<%# Eval("ID_ITEM_CATEGORY")%>' />
                      <asp:HiddenField ID="hdQuantity" runat="server" Value='<%# Eval("QUANTITY")%>' />
                      <asp:HiddenField ID="hdItemdesc" runat="server" Value='<%# Eval("DESCRIPTION_BY_CATEGORY")%>' />
                      <asp:HiddenField ID="hdIdItem" runat="server" Value='<%# Eval("ID_ITEM")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_REQUISITION" HeaderText="Requisition Code" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEPT" HeaderText="Department" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION_BY_CATEGORY" HeaderText="Item Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUESTED_BY" HeaderText="Requested By" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Date Requested" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="QUANTITY" HeaderText="quantity" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REASON_FOR_REQUEST" HeaderText="Reason For Request" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARKS" HeaderText="Admin Remarks" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="FLAG_ADMIN_APPROVE" HeaderText="Status" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnApprove" runat="server" ItemStyle-Width="5%" ControlStyle-CssClass="button" CommandName="Approve" Text="Approve"  />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnReject" runat="server" ItemStyle-Width="5%" ControlStyle-CssClass="button" CommandName="Reject" Text="Reject" OnClientClick="return RejectRequisition();" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Request Yet!</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <asp:HiddenField ID="hAdminRemark" runat="server" />
    <asp:HiddenField ID="hdITManagerRemarks" runat="server" />
</asp:Content>
