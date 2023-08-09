<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ReportTestCases.aspx.vb" Inherits="ETS.ReportTestCases" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <br />

     <div style="width: 1800px; margin: 0 auto;">
        <h3>Report > Test Cases</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>
      <br />
    <div class="box-shadow center" style="width: 70%;">
        <h5 class="form-title">Report Test Cases</h5>
        <table>
            <tr>
                <td style="width: 100px">
                    <label for="Issuetype" class="col-sm-6 col-form-label" style="width: 138px">Tester:<span style="color: red;"> *</span></label></td>
                <td style="width: 163px">
                    <asp:DropDownList ID="DropListTester" runat="server" Width="400px" DataValueField="USERNAME" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:DropDownList></td>
                <td>
                    <label for="Description" class="col-sm-3 col-form-label" style="width: 138px; height: 25px">Service Level Requirements<span style="color: red;"> *</span></label></td>
                <td style="width: 400px">
                    <asp:TextBox ID="txtServiceLevelRequirements" runat="server" Width="400px" class="form-control form-control-sm" placeholder="Service Level Requirements" aria-label=".form-control-sm example"></asp:TextBox></td>
                <td style="width: 163px">
                    <label for="statusticket" class="col-sm-6 col-form-label" style="width: 138px">Status Test:<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListStatusTest" runat="server" Width="400px" class="form-control form-control-sm" aria-label=".form-control-sm example">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Failed</asp:ListItem>
                        <asp:ListItem>Passed</asp:ListItem>
                    </asp:DropDownList></td>

        </table>

        <table>
            <tr>

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
                <td style="width: 600px">
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
                <td style="width: 600px">
                    <asp:DropDownList ID="DropListDate" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Date Start Test</asp:ListItem>
                        <asp:ListItem>Date Complete Test</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
        </table>

        <div>
            <asp:Button ID="btnSearchTickets" runat="server" Text="Search " class="btn btn-success btn-sm mb-3" OnClick="btnSearchTickets_Click" />
            <asp:Button ID="Excelexport" runat="server" Text="Export " class="btn btn-success btn-sm mb-3" OnClick="Excelexport_Click" />
        </div>
    </div>
      <br />

    <br />

    <div class="Alignments2">
        <asp:Label ID="lblDetails" runat="server" Text="Details" Visible="false"></asp:Label>
        <asp:GridView ID="gvReportTestCase"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            AllowPaging="false"
            CssClass="table-bordered table-sm box-shadow center"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="100%">

            <Columns>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TEST_CASES" HeaderText="Test Case No" />         
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="APP_CATEGORY" HeaderText="App Category" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="SERVICE_LEVEL_REQUIREMENT" HeaderText="Service Level Requirement" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="SCENERIO_TEST_CASE" HeaderText="Test Case Scenerio" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TEST_STEPS" HeaderText="Test Steps" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_RESULTS" HeaderText="Expected Results" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ACTUAL_RESULT" HeaderText="Actual Result" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER" HeaderText="Software Tester" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TEST" HeaderText="Status" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_START_TEST" HeaderText="Date Start Test" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_COMPLETE_TEST" HeaderText="Date Complete Test" />
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">Data Not found !</div>
            </EmptyDataTemplate>

        </asp:GridView>
    </div>

</asp:Content>

