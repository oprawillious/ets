﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ReportTask.aspx.vb" Inherits="ETS.ReportTask" EnableEventValidation="false" %>

<%@ Register Assembly="DevExpress.Web.v23.1, Version=23.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />


    <div style="width: 1800px; margin: 0 auto;">
        <h3>Report > Task Report</h3>
        <hr style="background-color: #1e8449; height: 3px;" />
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
                        <asp:ListItem>Assigned</asp:ListItem>
                        <asp:ListItem>Completed</asp:ListItem>

                    </asp:DropDownList></td>
            </tr>
        </table>

        <table style="width: 100%">

            <tr>
                <td style="width: 300px" hidden>
                    <label for="assignedTo" class="col-sm-6 col-form-label">Assigned To:<span style="color: red;"> *</span></label></td>
                <td hidden style="width: 300px">
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



            </tr>
        </table>

        <div>
            <asp:Button ID="btnSearchTask" runat="server" Text="Search " class="btn btn-success btn-sm mb-3" OnClick="btnSearchTask_Click" />
            <asp:Button ID="btnExportToXLS" runat="server" Text="Export " class="btn btn-success btn-sm mb-3" OnClick="btnExportToXLS_Click" Visible="false" />

        </div>
    </div>


    <br />

    <div class="Alignments2">




        <dx:ASPxGridView ID="gvReportTask"
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





                <dx:GridViewDataTextColumn FieldName="ID_TASK" Caption="Task No" Width="45%" />
                <dx:GridViewDataTextColumn FieldName="ID_TEST_CASES" Caption="Test Case Id" Width="45%" />
                <dx:GridViewDataTextColumn FieldName="TASK_DESCRIPTION" Caption="Description" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="CATEGORY" Caption="App Category" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="PRIORITY" Caption="Priority" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="TYPE_TASK" Caption="Type" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="ASSIGNED_TO" Caption="Created By" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="STATUS_TASK" Caption="Status Task" Width="100%" />
                <dx:GridViewDataTextColumn FieldName="DATE_ASSIGNED" Caption="Date Assigned" />

                <dx:GridViewDataTextColumn FieldName="DATE_START" Caption="Dev  Start Date" />

                <dx:GridViewDataTextColumn FieldName="DATE_COMPLETE" Caption="Dev End Date" />
                <dx:GridViewDataTextColumn FieldName="TEST_START_DATE" Caption="Test Start Date" />
                <dx:GridViewDataTextColumn FieldName="TEST_END_DATE" Caption="Test End Date" />
                <dx:GridViewDataTextColumn FieldName="STATUS_TEST" Caption="Test Status" />


            </Columns>


        </dx:ASPxGridView>

    </div>
</asp:Content>
