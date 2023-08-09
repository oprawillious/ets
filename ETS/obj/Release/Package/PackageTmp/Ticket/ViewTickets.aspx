<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewTickets.aspx.vb" Inherits="ETS.ViewTickets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <br />
    
    <div class="Alignments2">
        <h3>Ticket > View Ticket</h3><hr class="greenLine"/><br />
        <h2>Tickets</h2>

        <table>
            <tr>
                <td><label for="inputTaskNumber" class="col-sm-2 col-form-label">Ticket No: </label></td>
                <td><asp:TextBox ID="txtTicketNumber" runat="server" class="form-control form-control-sm" placeholder="ticket number..." style="width: 200px;"></asp:TextBox></td>
                <td style="width: 163px"><label for="statusticket" class="col-sm-2 col-form-label" style="width: 138px">Status Ticket:<span style="color: red;"> </span></label></td>
                <td style="width:200px;">
                    <asp:DropDownList ID="DropListStatusTicket" runat="server"  class="form-control form-control-sm" aria-label=".form-control-sm example">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="Re-Opened">Re-Opened</asp:ListItem>
                        <asp:ListItem Value="Closed">Closed</asp:ListItem>
                        <asp:ListItem Value="Opened">Open</asp:ListItem>
                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                    </asp:DropDownList></td>

                <td><asp:Button ID="btnSearchTicket" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchTicket_Click"/></td>
                <td><asp:Button ID="btnNewTicket" runat="server" Text="New Ticket" class="btn btn-success btn-sm mb-3"  OnClick="btnNewTicket_Click"/></td>
            </tr>
        </table>

        <asp:GridView ID="gvTickets" 
                      runat="server"
                      AutoGenerateColumns="False"
                      CellPadding="1"
                      CellSpacing="1"
                      GridLines="None"
                      AllowPaging="True"
                      PageSize ="10"
                      CssClass="table-bordered table-sm"
                      PagerStyle-CssClass="table pgr"     
                      AlternatingRowStyle-CssClass="normal11" 
                      Width="98%">

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>'/>
                        <asp:HiddenField ID="hdIssueType" runat="server" Value='<%# Eval("ISSUE_TYPE")%>'/>
                        <asp:HiddenField ID="hdAssignedTo" runat="server" Value='<%# Eval("ASSIGNED_TO")%>'/>
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
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REQUEST_FROM" HeaderText="Requested From"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="INTERCOMMS" HeaderText="Intercomms"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARKS_HELPDESK" HeaderText="Helpdesk Remarks"/>

                <asp:TemplateField HeaderText="Is Resolved" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                
                          <asp:Image runat="server" ID="ImgPassed" ImageUrl="~/images/check.png" Visible='<%#fn_CheckStatus(Eval("FLAG_RESOLVED"))%>'/>
                          <asp:Image runat="server" ID="ImgFailed" ImageUrl="~/images/close.png" Visible='<%#Not fn_CheckStatus(Eval("FLAG_RESOLVED"))%>'/>
                    </ItemTemplate>
               </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:Button ID="btnEdit" runat="server" Text="Details" CommandName="Modifica" class="button primary"/> 
                      </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Remove" class="button primary"/> 
                      </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">Ticket Not found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager"/>
        </asp:GridView>
        
    </div>
</asp:Content>
