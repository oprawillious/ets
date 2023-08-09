<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ReportHoliday.aspx.vb" Inherits="ETS.ReportHoliday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <style>
 
        }
    </style>

    <br />
    <div class="Alignments2">
        <h3>Report > Holidaay</h3>
       
        <hr class="greenLine" />
        <br />
        <asp:Label ID="lblCaption" runat="server" Text="Report Holiday" /> <br/>
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

                <td style="padding:20px;">
                    <label for="searchFirstname" class="col-sm-2 col-form-label">Name: </label>
                </td>
                <td style="width: 300px;padding:20px;">
                    <asp:TextBox ID="txtFirstname" placeholder="Firstname ..." runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>

                <td>
                    <label for="searchLastname" class="col-sm-2 col-form-label">Surname: </label>
                </td>
                <td style="width: 300px">
                    <asp:TextBox ID="txtLastname" placeholder="Lastname ..." runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>



                <td align="right" style="width: 200px" class="col-sm-6 col-form-label">Start Date</td>

                <td align="left" style="width: 300px">
                    <asp:TextBox ID="txtStartDate" runat="server" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" />

                    <ajaxToolkit:CalendarExtender runat="server"
                        ID="txtDateReOpened0_CalendarExtender"
                        Format="MM/dd/yyyy"
                        PopupButtonID="ImageButton63"
                        TargetControlID="txtStartDate"></ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator77"
                        runat="server"
                        ControlToValidate="txtStartDate"
                        CssClass="txtObbligatorio"
                        Display="None"
                        SetFocusOnError="True" />
                </td>

                <td>
                    <asp:ImageButton ID="ImageButton63" ImageUrl="~/images/_calendar.png" Width="30px" runat="server" />
                </td>

                <td align="right" style="width: 250px">End Date</td>

                <td align="left" style="width: 300px;">
                    <asp:TextBox ID="txtEndDate" runat="server" />

                    <ajaxToolkit:CalendarExtender runat="server"
                        ID="txtDateReOpened1_CalendarExtender"
                        Format="MM/dd/yyyy"
                        PopupButtonID="ImageButton68"
                        TargetControlID="txtEndDate"></ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator71"
                        runat="server"
                        ControlToValidate="txtEndDate"
                        CssClass="txtObbligatorio"
                        Display="None"
                        SetFocusOnError="True" />
                </td>

                <td>
                    <asp:ImageButton ID="ImageButton68" ImageUrl="~/images/_calendar.png" Width="30px" runat="server" />
                </td>
            </tr>
        </table>

        <div>
            <asp:Button ID="btnSearchHoliday" runat="server" Text="Search " class="btn btn-success btn-sm mb-3" OnClick="btnSearchHoliday_Click" />
            <asp:Button ID="btnExportToXLS" runat="server" Text="Export " class="btn btn-success btn-sm mb-3" OnClick="btnExportToXLS_Click" Visible="false" />
        </div>
        <br />
        <br />





        <asp:GridView ID="gvReportHolidayDetails"
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



            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Report Available !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>


    </div>
</asp:Content>
