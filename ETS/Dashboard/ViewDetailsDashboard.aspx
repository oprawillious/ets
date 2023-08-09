<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewDetailsDashboard.aspx.vb" Inherits="ETS.ViewDetailsDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <div style="margin: 0 auto; width: 2100px;">
    <h3>DashBoard > View Details DashBoard</h3><hr /><br />
    <h3><asp:Label ID="lblGridViewName" runat="server" Text="" ></asp:Label> - <asp:Label ID="lblcount" runat="server" Text="" ></asp:Label></h3>   
        <br />      
        <asp:GridView ID="gvOpenTask" 
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
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TASK_DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="CATEGORY" HeaderText="Category"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TYPE_TASK" HeaderText="Type"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TASK" HeaderText="Status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARK" HeaderText="Admin Remarks"/> 
                
            </Columns>           

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>       
        </asp:GridView> 
        
        <asp:GridView ID="gvCloseTask" 
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
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TASK_DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="CATEGORY" HeaderText="Category"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TYPE_TASK" HeaderText="Type"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TASK" HeaderText="Status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARK" HeaderText="Admin Remarks"/> 
                
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>   
       
        <asp:GridView ID="gvAllTicket" 
                      runat="server"
                      AutoGenerateColumns="False"
                      CellPadding="1"
                      CellSpacing="1"
                      GridLines="None"
                      AllowPaging="false"
                      PageSize ="10"
                      CssClass="table-bordered table-sm"
                      PagerStyle-CssClass="table pgr"     
                      AlternatingRowStyle-CssClass="normal11" 
                      Width="98%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_TYPE" HeaderText="Request Type"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_SUB_TYPE" HeaderText="Category"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TICKET" HeaderText="Current status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUEST_BY" HeaderText="Requested By"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Requested Date"/>
                 <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ASSIGNED_TO" HeaderText="Assigned To"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned"/>
                
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>

        <asp:GridView ID="gvOpenicket" 
                      runat="server"
                      AutoGenerateColumns="False"
                      CellPadding="1"
                      CellSpacing="1"
                      GridLines="None"
                      AllowPaging="false"
                      PageSize ="10"
                      CssClass="table-bordered table-sm"
                      PagerStyle-CssClass="table pgr"     
                      AlternatingRowStyle-CssClass="normal11" 
                      Width="98%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_TYPE" HeaderText="Request Type"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_SUB_TYPE" HeaderText="Category"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TICKET" HeaderText="Current status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUEST_BY" HeaderText="Requested By"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Requested Date"/>
                 <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ASSIGNED_TO" HeaderText="Assigned To"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned"/>
                
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>
           
        <asp:GridView ID="gvCloseTicket" 
                      runat="server"
                      AutoGenerateColumns="False"
                      CellPadding="1"
                      CellSpacing="1"
                      GridLines="None"
                      AllowPaging="false"
                      PageSize ="10"
                      CssClass="table-bordered table-sm"
                      PagerStyle-CssClass="table pgr"     
                      AlternatingRowStyle-CssClass="normal11" 
                      Width="98%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_TYPE" HeaderText="Request Type"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_SUB_TYPE" HeaderText="Category"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TICKET" HeaderText="Current status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUEST_BY" HeaderText="Requested By"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Requested Date"/>
                 <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ASSIGNED_TO" HeaderText="Assigned To"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned"/>
                
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>

        <asp:GridView ID="gvDailyTicket" 
                      runat="server"
                      AutoGenerateColumns="False"
                      CellPadding="1"
                      CellSpacing="1"
                      GridLines="None"
                      AllowPaging="false"
                      PageSize ="10"
                      CssClass="table-bordered table-sm"
                      PagerStyle-CssClass="table pgr"     
                      AlternatingRowStyle-CssClass="normal11" 
                      Width="98%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_TYPE" HeaderText="Request Type"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_SUB_TYPE" HeaderText="Category"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TICKET" HeaderText="Current status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUEST_BY" HeaderText="Requested By"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Requested Date"/>
                 <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ASSIGNED_TO" HeaderText="Assigned To"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned"/>
                
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>

        <asp:GridView ID="gvAllPreviousDayTicket" 
                      runat="server"
                      AutoGenerateColumns="False"
                      CellPadding="1"
                      CellSpacing="1"
                      GridLines="None"
                      AllowPaging="false"
                      PageSize ="10"
                      CssClass="table-bordered table-sm"
                      PagerStyle-CssClass="table pgr"     
                      AlternatingRowStyle-CssClass="normal11" 
                      Width="98%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_TYPE" HeaderText="Request Type"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_SUB_TYPE" HeaderText="Category"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TICKET" HeaderText="Current status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUEST_BY" HeaderText="Requested By"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REQUESTED" HeaderText="Requested Date"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ASSIGNED_TO" HeaderText="Assigned To"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned"/>
                
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>
 </div>
          
</asp:Content>