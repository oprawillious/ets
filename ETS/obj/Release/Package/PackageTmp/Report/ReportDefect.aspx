<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ReportDefect.aspx.vb" Inherits="ETS.ReportDefect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <br />

     <div style="width: 1800px; margin: 0 auto;">
        <h3>Report > Defect Report</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>
    <br /><br />
    <div class="box-shadow center" style="width: 80%;">
        <h5 class="form-title">Defect Report</h5>
        <table>
            <tr>

                <td>
                    <label for="Category" class="col-sm-6 col-form-label">Category:</label></td>
                <td style="width: 300px">
                    <asp:DropDownList ID="DropListCategory" runat="server" DataValueField="SUB_DESCRIPTION">
                    </asp:DropDownList></td>
                <td>

                    <label for="priority" class="col-sm-6 col-form-label">Priority:</label></td>
                <td style="width: 300px">
                    <asp:DropDownList ID="DropListPriority" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Medium</asp:ListItem>
                        <asp:ListItem>High</asp:ListItem>
                        <asp:ListItem>Low</asp:ListItem>
                        <asp:ListItem>Highest</asp:ListItem>
                    </asp:DropDownList></td>

                <td>
                    <label for="defectType" class="col-sm-6 col-form-label">DefectType:</label></td>
                <td style="width: 250px">
                    <asp:DropDownList ID="DropDownListDefectType" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Bug</asp:ListItem>
                        <asp:ListItem>Suggestion</asp:ListItem>
                    </asp:DropDownList></td>

                <td style="width: 250px">
                    <label for="defectType" class="col-sm-6 col-form-label">Defect Status:</label></td>
                <td style="width: 250px">
                    <asp:DropDownList ID="DropListStatus" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="Y">Fixed</asp:ListItem>
                        <asp:ListItem Value="N">Not Fixed</asp:ListItem>
                    </asp:DropDownList></td>

                <td style="width: 282px" class="col-sm-6 col-form-label">Start Date</td>

                <td style="width: 400px">
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
                    <asp:ImageButton ID="ImageButton63" ImageUrl="~/images/_calendar.png" Width="20px" runat="server" />
                </td>

                <td style="width: 250px">End Date</td>

                <td style="width: 400px">
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
                    <asp:ImageButton ID="ImageButton68" ImageUrl="~/images/_calendar.png" Width="20px" runat="server" />
                </td>

            </tr>
        </table>

        <div>
            <asp:Button ID="btnSearchDefect" runat="server" Text="Search " class="btn btn-success btn-sm mb-3" OnClick="btnSearchDefect_Click" />
            <asp:Button ID="btnExportToXLS" runat="server" Text="Export " class="btn btn-success btn-sm mb-3" OnClick="btnExportToXLS_Click" Visible="false" />
        </div>

    </div>
    <br />
    <br />
    <div class="Alignments2">

        <asp:Label ID="lblDetails" runat="server" Text="Details" Visible="false"></asp:Label>
        <asp:GridView ID="gvReportDefect"
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
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task No" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_OPENED" HeaderText="Date Started" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="APP_CATEGORY" HeaderText="App Category" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEFECT_TYPE" HeaderText="Defect Type" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER" HeaderText="Tester" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STEPS_TO_REPRODUCE" HeaderText="Step To Reproduce" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_RESULT" HeaderText="Expected Result" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ACTUAL_RESULT" HeaderText="Actual Result" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS" HeaderText="Defect Status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_FIXED" HeaderText="Date Fixed" />
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">Data Not found !</div>
            </EmptyDataTemplate>

        </asp:GridView>
    </div>
</asp:Content>
