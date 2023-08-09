<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ReportRequisition.aspx.vb" Inherits="ETS.ReportRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="Alignments2">
        <h3>Report > Report Requisition</h3>
        <hr class="greenLine" />
        <br />
        <h2>Requisition Report</h2>

        <table>
            <tr>
                <td align="right">
                    <label style="margin-left: 90px;" for="priority" class="col-sm-2 col-form-label">Status:</label></td>
                <td style="width: 250px; display: inline-block;" align="left">
                    <asp:DropDownList ID="DropListStatus" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="Y">Approved</asp:ListItem>
                        <asp:ListItem Value="N">Rejected</asp:ListItem>
                    </asp:DropDownList></td>

                <td align="right" id="trlblEmpName" runat="server">
                    <label for="inputTaskNumber" class="col-sm-2 col-form-label">Employee Name: </label>
                </td>
                <td id="trtxtEmpName" align="left" runat="server">
                    <asp:TextBox ID="txtEmployeeName" runat="server" class="form-control form-control-sm" placeholder="Enter Employee Name..." Style="width: 250px;"></asp:TextBox></td>

                <td align="right">
                    <label for="" class="col-sm-2 col-form-label">Request Date: </label>
                </td>

                <td align="left" style="width: 300px">
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
                <td>
                    <asp:ImageButton ID="ImageButton63" ImageUrl="~/images/_calendar.png" Width="30px" runat="server" /></td>

            </tr>
        </table>

        <div>
            <asp:Button ID="btnSearchRequisition" runat="server" Text="Search " class="btn btn-success btn-sm mb-3" OnClick="btn_SearchRequisition_Click" />
            <asp:Button ID="btnExportToPdf" runat="server" Text="Export " class="btn btn-success btn-sm mb-3" OnClick="btnExport_Requisition_ToPdf_Click" Visible="false" />
        </div>
        <br />
        <br />

        <asp:GridView ID="gvReportRequisition"
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
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdUser" runat="server" Value='<%# Eval("ID_USER")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdItemCategory" runat="server" Value='<%# Eval("ID_ITEM_CATEGORY")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdQuantity" runat="server" Value='<%# Eval("QUANTITY")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_REQUISITION" HeaderText="Requisition Code" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEPT" HeaderText="Department" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_USER" HeaderText="User Id" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ITEM_DESCRIPTION" HeaderText="Item Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUESTED_BY" HeaderText="Requested By" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Date Requested" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="QUANTITY" HeaderText="quantity" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="APPROVED_BY" HeaderText="Approved_by" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REASON_FOR_REQUEST" HeaderText="Reason For Request" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARKS" HeaderText="Admin Remarks" />

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="FLAG_ADMIN_APPROVE" HeaderText="Status" />

            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">Data Not Found!</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>

    </div>




</asp:Content>




