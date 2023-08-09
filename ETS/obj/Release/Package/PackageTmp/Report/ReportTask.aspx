<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ReportTask.aspx.vb" Inherits="ETS.ReportTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
    <br />

  
    <div style="width: 1800px; margin: 0 auto;">
        <h3>Report > Task Report</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>
    <div class="box-shadow center" style="width: 80%;">
        <h5 class="form-title">Task Report</h5>

        <table>
            <tr>
                <td>
                    <label for="taskNumber" class="col-sm-6 col-form-label" style="width: 138px">Task No:<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:TextBox ID="txtTaskNumber" runat="server" class="form-control form-control-sm" placeholder="task No..." aria-label=".form-control-sm example" Width="147px"></asp:TextBox></td>
                <td>
                    <label for="description" class="col-sm-6 col-form-label">Description:<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control form-control-sm" placeholder="Description" aria-label=".form-control-sm example" Width="400px"></asp:TextBox></td>
                <td>
                    <label for="priority" class="col-sm-6 col-form-label">Priority:<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListPriority" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Medium</asp:ListItem>
                        <asp:ListItem>High</asp:ListItem>
                        <asp:ListItem>Low</asp:ListItem>
                        <asp:ListItem>Highest</asp:ListItem>
                    </asp:DropDownList></td>

                <td>
                    <label for="statusticket" class="col-sm-6 col-form-label">Status Task:<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListStatusTask" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>In Progress</asp:ListItem>
                        <asp:ListItem>Pending</asp:ListItem>
                        <asp:ListItem>Completed</asp:ListItem>
                        <asp:ListItem>On Test</asp:ListItem>
                        <asp:ListItem>Test Case Failed</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
        </table>

        <table style="width: 100%">

            <tr>
                <td style="width: 300px">
                    <label for="assignedTo" class="col-sm-6 col-form-label">Assigned To:<span style="color: red;"> *</span></label></td>
                <td style="width: 300px">
                    <asp:DropDownList ID="DropListAssignedTo" runat="server" DataValueField="FIRST_NAME" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:DropDownList></td>
                <td style="width: 282px" class="col-sm-6 col-form-label">Start Date</td>
                <td style="width: 600px">
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
                    <asp:ImageButton ID="ImageButton63" ImageUrl="~/images/_calendar.png" Width="20px" runat="server" /></td>
                <td style="width: 250px">End Date</td>
                <td style="width: 300px">
                    <asp:TextBox ID="txtEndDate" runat="server" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" />
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
                    <asp:ImageButton ID="ImageButton68" ImageUrl="~/images/_calendar.png" Width="20px" runat="server" /></td>
                <td style="width: 281px">Date Type</td>
                <td style="width: 300px">
                    <asp:DropDownList ID="DropListDate" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Start Date</asp:ListItem>
                        <asp:ListItem>Completed Date</asp:ListItem>
                        <asp:ListItem>Assigned Date</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
        </table>

        <div>
            <asp:Button ID="btnSearchTask" runat="server" Text="Search " class="btn btn-success btn-sm mb-3" OnClick="btnSearchTask_Click" />
            <asp:Button ID="btnExportToXLS" runat="server" Text="Export " class="btn btn-success btn-sm mb-3" OnClick="btnExportToXLS_Click" Visible="false" />

        </div>
    </div>
    

    <br />

    <div class="Alignments2">
        
        <asp:Label ID="lblDetails" runat="server" Text="Details" Visible="false"></asp:Label>
        <asp:GridView ID="gvReportTask"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            AllowPaging="false"
            CssClass="table-bordered table-sm box-shadow center"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="75%">

            <Columns>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task No"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TASK_DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ASSIGNED_TO" HeaderText="Assigned To"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TASK" HeaderText="Status Task"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_START" HeaderText="Date Started"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_COMPLETE" HeaderText="Date Completed"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned"/>
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">Data Not found !</div>
            </EmptyDataTemplate>

        </asp:GridView>
    </div>
</asp:Content>
