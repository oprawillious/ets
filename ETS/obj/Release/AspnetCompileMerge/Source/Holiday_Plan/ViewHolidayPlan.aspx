<%@ Page Title="View Holiday Plan" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewHolidayPlan.aspx.vb" Inherits="ETS.ViewHolidayPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <div class="Alignments2">
        <h3>Holiday Plan > View Holiday Plan</h3>
        <hr class="greenLine" />

        <br />
        <h2>Holiday Plan </h2>

        <table>
            <tr>
                <td id="trlblEmpName" runat="server">
                    <label for="inputTaskNumber" class="col-sm-2 col-form-label">Employee Name: </label>
                </td>
                <td id="trtxtEmpName" runat="server">
                    <asp:TextBox ID="txtEmployeeName" runat="server" class="form-control form-control-sm" placeholder="Enter Employee Name..." Style="width: 250px;"></asp:TextBox></td>

                <td>
                    <label for="inputTaskNumber" class="col-sm-2 col-form-label">Request Date: </label>
                </td>
                <td>
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

                <td>
                    <asp:Button ID="btnSearchHoliday" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchHoliday_Click" /></td>
                <td>
                    <asp:Button ID="btnNewHoliday" runat="server" Text="New Holiday" class="btn btn-success btn-sm mb-3" OnClick="btnNewHoliday_Click" /></td>
            </tr>
        </table>


        <asp:GridView ID="gvHolidayDetails"
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
                        <asp:HiddenField ID="hdIdHolidayPlan" runat="server" Value='<%# Eval("ID_HOLIDAY_PLAN")%>' />
                        <asp:HiddenField ID="hdDays" runat="server" Value='<%# Eval("NO_OF_DAYS")%>' />
                        <asp:HiddenField ID="hdEndDate" runat="server" Value='<%# Eval("HOLIDAY_END_DATE")%>' />
                        <asp:HiddenField ID="hdIdUser" runat="server" Value='<%# Eval("ID_USER")%>' />
                        <asp:HiddenField ID="hdUsername" runat="server" Value='<%# Eval("USERNAME")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_HOLIDAY_PLAN" HeaderText="Holiday Code" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TYPE_HOLIDAY" HeaderText="Holiday Type" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="FULLNAME" HeaderText="Full Name" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="HOLIDAY_START_DATE" HeaderText="Start Date" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="HOLIDAY_END_DATE" HeaderText="End Date" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="NO_OF_DAYS" HeaderText="Numbers Of Days" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="USER_REMARKS" HeaderText="Reason For Leave" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="USERNAME" HeaderText="Username" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Requested Date" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="FLAG_APPROVED" HeaderText="Status" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ADMIN_REMARKS" HeaderText="Manager Remarks" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnApprove" runat="server" ItemStyle-Width="5%" ControlStyle-CssClass="button" CommandName="Approve" Text="Approve" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnReject" runat="server" ItemStyle-Width="5%" ControlStyle-CssClass="button" CommandName="Reject" Text="Reject" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Holiday requests yet !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

    <asp:HiddenField ID="iduser" runat="server" />
    <asp:HiddenField ID="hdAdminRemark" runat="server" />

</asp:Content>
