<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="Home.aspx.vb" Inherits="ETS.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />

    <div style="margin: 0 auto; width: 2000px;">

        <h1>ETS - Home</h1>
        <hr style="background-color: #1e8449; height: 3px;" />
        <h1>
            <asp:Label ID="lblGreeting" runat="server" Text="" /></h1>

        <br />
        <h1>
            <asp:Label ID="lblType" runat="server" Text="" /></h1>

        <asp:GridView ID="gvTickets"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            PageSize="10"
            CssClass="table-bordered table-sm"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="98%">

            <Columns>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket Number" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_TYPE" HeaderText="Request Type" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_SUB_TYPE" HeaderText="Category" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TICKET" HeaderText="Current status" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUEST_BY" HeaderText="Requested By" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Requested Date" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ASSIGNED_TO" HeaderText="Assigned To" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUEST_FROM" HeaderText="Requested From" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="INTERCOMMS" HeaderText="Intercomms" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARKS_HELPDESK" HeaderText="Helpdesk Remarks" />
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">Ticket Not found !</div>
            </EmptyDataTemplate>

        </asp:GridView>




        <asp:GridView ID="gvMyTask"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            AllowPaging="false"
            CssClass="table-bordered table-sm"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="98%">

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTask" runat="server" Value='<%# Eval("ID_TASK")%>' />
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task Number" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TASK_DESCRIPTION" HeaderText="Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="CATEGORY" HeaderText="App Category" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="USERNAME" HeaderText="Assigned To" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TASK" HeaderText="Status" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_START_DATE" HeaderText="Expected Start Date" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_END_DATE" HeaderText="Expected End Date" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARK" HeaderText="Admin Remarks" />
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="btnCloseTask" runat="server" Text="Close" CommandName="CloseTask" class="button primary" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Recent Task</div>
            </EmptyDataTemplate>

        </asp:GridView>


        <asp:GridView ID="gvTestCase"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            AllowPaging="false"
            CssClass="table-bordered table-sm"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="98%">
            <Columns>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTestCase" runat="server" Value='<%# Eval("ID_TEST_CASES")%>' />
                        <asp:HiddenField ID="hdIdTask" runat="server" Value='<%# Eval("ID_TASK")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>' />
                        <asp:HiddenField ID="hdAppCategory" runat="server" Value='<%# Eval("APP_CATEGORY")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TEST_CASES" HeaderText="Test Case Id" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task Id" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="APP_CATEGORY" HeaderText="App Category" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEVELOPER" HeaderText="Developer" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER" HeaderText="Tester" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TEST" HeaderText="Test Status" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_START_TEST" HeaderText="Date Started" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_COMPLETE_TEST" HeaderText="Date Completed" />
                <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="button" CommandName="Details" Text="Details" />

            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Test Case found !</div>
            </EmptyDataTemplate>
        </asp:GridView>

    </div>

</asp:Content>
