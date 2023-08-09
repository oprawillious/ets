<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ReportTickets.aspx.vb" Inherits="ETS.ReportTickets" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <br />


    <br />

    <div style="width: 1800px; margin: 0 auto;">
        <h3>Report > All Tickets</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>
    <div class="box-shadow center" style="width: 65%;">
        <h5 class="form-title">
            <asp:Label ID="lblCaption" runat="server" Text="Report Tickets" /></h5>


        <table>
            <tr>
                <td style="width: 163px">
                    <label for="TicketNumber" class="col-sm-6 col-form-label">Ticket No:<span style="color: red;"> *</span></label></td>
                <td style="width: 163px">
                    <asp:TextBox ID="txtTicketNumber" runat="server" class="form-control form-control-sm" placeholder="Ticket No.." aria-label=".form-control-sm example" Width="147px"></asp:TextBox></td>
                <td style="width: 163px">
                    <label for="Issuetype" class="col-sm-6 col-form-label" style="width: 138px">Request Type:<span style="color: red;"> *</span></label></td>
                <td style="width: 163px">
                    <asp:DropDownList ID="DropListType" runat="server" DataValueField="DESCRIPTION" AutoPostBack="true" class="form-control form-control-sm" aria-label=".form-control-sm example" Width="147px"></asp:DropDownList></td>
                <td>
                    <label for="subIssueType" class="col-sm-6 col-form-label">Category:<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListCategory" runat="server" DataValueField="SUB_DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:DropDownList></td>
                <td style="width: 163px">
                    <label for="Description" class="col-sm-3 col-form-label">Description<span style="color: red;"> *</span></label></td>
                <td style="width: 163px">
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control form-control-sm" placeholder="Description" aria-label=".form-control-sm example"></asp:TextBox></td>

            </tr>

            <tr>
                <td style="width: 163px">
                    <label for="statusticket" class="col-sm-6 col-form-label" style="width: 138px">Status Ticket:<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListStatusTicket" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Open</asp:ListItem>
                        <asp:ListItem>Pending</asp:ListItem>
                        <asp:ListItem>Closed</asp:ListItem>
                    </asp:DropDownList></td>
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
                    <label for="subIssueType" class="col-sm-6 col-form-label" style="width: 135px">Requested By:<span style="color: red;"> *</span></label></td>
                <td style="width: 600px">

                    <asp:DropDownList ID="DropListRequestedBy" runat="server" DataValueField="USERNAME" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:DropDownList>

                </td>
                <td style="width: 280px">
                    <label for="assignedTo" class="col-sm-6 col-form-label">Assigned To:<span style="color: red;"> *</span></label></td>
                <td style="width: 600px">
                    <asp:DropDownList ID="DropListAssignedTo" runat="server" DataValueField="FIRST_NAME" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:DropDownList>
                </td>


            </tr>
            <tr>
                <td class="col-sm-6 col-form-label">Start Date</td>

                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" Style="width: 150px;" />

                    <ajaxToolkit:CalendarExtender runat="server"
                        ID="txtDateReOpened0_CalendarExtender"
                        Format="MM/dd/yyyy"
                        PopupButtonID="ImageButton63"
                        TargetControlID="txtStartDate" />

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

                <td class="col-sm-6 col-form-label">End Date</td>

                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" Style="width: 150px;" />

                    <ajaxToolkit:CalendarExtender runat="server"
                        ID="txtDateReOpened1_CalendarExtender"
                        Format="MM/dd/yyyy"
                        PopupButtonID="ImageButton68"
                        TargetControlID="txtEndDate" />
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

                <td class="col-sm-6 col-form-label" style="width: 50px;">Date Type</td>
                <td>
                    <asp:DropDownList ID="DropListDate" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>Date Opened</asp:ListItem>
                        <asp:ListItem>Date Requested</asp:ListItem>
                        <asp:ListItem>Date Resolved</asp:ListItem>
                        <asp:ListItem>Date Assigned</asp:ListItem>
                        <asp:ListItem>Date Closed</asp:ListItem>
                        <asp:ListItem>Date ReOpened</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

        <div>
            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-success btn-sm mb-3" OnClick="btnSearchTickets_Click" />
            <asp:Button ID="Excelexport" runat="server" Text="Export" class="btn btn-success btn-sm mb-3" OnClick="Excelexport_Click" />
        </div>
    </div>
    
    <br />
    <br />

    <div class="Alignments2">

        <asp:ModalPopupExtender ID="PopupRemarks"
            BehaviorID="mpe"
            runat="server"
            PopupControlID="pnlPopup"
            TargetControlID="hdCardRequest"
            BackgroundCssClass="modalBackground" />

        <asp:HiddenField ID="hdCardRequest" runat="server" />
        <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 600px;">
                        <div class="modal-content" style="width: 500px; margin: 0 auto;">
                            <br />
                            <div class="modal-header">
                                <h3>Re-Open Ticket:</h3>
                            </div>
                            <hr />
                            <div class="modal-body">
                                <table>
                                    <tr>
                                        <td>
                                            <label for="inputRemarks" class="col-sm-3 col-form-label">Remarks:<span style="color: red;"> *</span></label></td>
                                        <td>
                                            <asp:TextBox ID="txtAdminRemarks" runat="server" class="form-control form-control-sm" placeholder="Remarks..." aria-label=".form-control-sm example" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>

                            <div class="modal-footer">
                                <asp:Button ID="btnClosePopRemarks" class="btn btn-danger" runat="server" Text="Close" OnClick="btnClosePopRemarks_Click" />
                                <asp:Button ID="btnConfirmRemarks" class="btn btn-success" runat="server" Text="Confirm" OnClick="btnConfirmRemarks_Click" Style="float: right;" /><br />
                                <br />
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

        <asp:Label ID="lblDetails" runat="server" Text="Details Ticket" Visible="false"></asp:Label><br />
        <br />
        <asp:GridView ID="gvReportAllTickets"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            AllowPaging="false"
            CssClass="table-bordered table-sm"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="75%">

            <Columns>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdassignedTo" runat="server" Value='<%# Eval("ASSIGNED_TO")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIssue" runat="server" Value='<%# Eval("ISSUE_TYPE")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket No" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_TYPE" HeaderText="Request Type" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_SUB_TYPE" HeaderText="Category" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TICKET" HeaderText="Status Ticket" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUEST_BY" HeaderText="Requested By" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REOPENED" HeaderText="Date ReOpened" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_CLOSED" HeaderText="Date Closed" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date assigned" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_RESOLVED" HeaderText="Date Resolved" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Date Requested" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ASSIGNED_TO" HeaderText="Assigned To" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_OPENED" HeaderText="Date Opened" />

            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">Data Not found !</div>
            </EmptyDataTemplate>

        </asp:GridView>
    </div>

</asp:Content>

