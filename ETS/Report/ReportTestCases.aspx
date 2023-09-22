<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ReportTestCases.aspx.vb" Inherits="ETS.ReportTestCases" EnableEventValidation="false" %>

<%@ Register Assembly="DevExpress.Web.v23.1, Version=23.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />

    <div style="width: 1800px; margin: 0 auto;">
        <h3>Report > Test Cases</h3>
        <hr style="background-color: #1e8449; height: 3px;" />
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

        <dx:ASPxGridView ID="gvReportTestCase"
            runat="server"
            AutoGenerateColumns="False"
            Width="90%"
            KeyFieldName="ID_TASK"
            baseColor="Green"
            CssClass="box-shadow center"
            Font-Size="Large">

            <Toolbars>
                <dx:GridViewToolbar Name="toolbar">
                    <SettingsAdaptivity Enabled="true" EnableCollapseRootItemsToIcons="true" />
                    <Items>
                        <dx:GridViewToolbarItem Alignment="Right" Name="toolbarItemSearch">
                            <Template>
                                <dx:ASPxButtonEdit ID="tbToolbarSearch" runat="server" NullText="Search All Columns..." Height="100%">
                                    <Buttons>
                                        <dx:SpinButtonExtended />
                                    </Buttons>
                                </dx:ASPxButtonEdit>
                            </Template>
                        </dx:GridViewToolbarItem>
                    </Items>
                </dx:GridViewToolbar>
            </Toolbars>

            <Columns>


                <dx:GridViewDataTextColumn FieldName="ID_TEST_CASES" Caption="Test Case No" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="ID_TASK" Caption="Task No" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="TASK_DESCRIPTION" Caption="Description" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="CATEGORY" Caption="App Category" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="TEST_PASSED" Caption="Test Passed" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="TEST_FAILED" Caption="Test Failed" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="ASSIGNED_TO" Caption="Software Tester" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="CASE_COUNT" Caption="Test Count" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="DATE_COMPLETE" Caption="Date Assigned" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="DATE_START" Caption="Date Started" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="DATE_COMPLETE_TEST" Caption="Date Complete Test" Width="100%" />


            </Columns>


        </dx:ASPxGridView>

    </div>

</asp:Content>

