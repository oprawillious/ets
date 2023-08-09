<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewDetailsTicket.aspx.vb" Inherits="ETS.ViewDetailsTicket" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h3>Ticket > View Tickets > Details Ticket</h3>
      
<div class="Alignments">
    <hr class="greenLine"/>
    <h3><asp:label ID="lblDetailTicket" runat="server" Text=""></asp:label></h3><br/>
    <table>
        <tr>
            <td><label for="inputPassword" class="col-sm-2 col-form-label">Request Type: </label></td>
            <td><asp:DropDownList ID="DropListRequestType" runat="server" DataValueField="DESCRIPTION"  class="form-control form-control-sm" aria-label=".form-control-sm example" OnSelectedIndexChanged="DropListRequestType_SelectedIndexChanged">
            </asp:DropDownList></td>         
        </tr>
        <tr>
            <td><label for="inputPassword" class="col-sm-2 col-form-label">Category: </label></td>
            <td><asp:Label ID="lblCategory" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>
        <tr>
            <td><label for="inputPassword" class="col-sm-2 col-form-label">Description: </label></td>
            <td><asp:TextBox ID="txtDescription" runat="server" TextMode ="MultiLine" ></asp:TextBox></td>           
        </tr>
        <tr>
            <td><label for="inputPassword" class="col-sm-2 col-form-label">Status: </label></td>
            <td><asp:Label ID="lblStatus" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>
        <tr>
            <td><label for="inputPassword" class="col-sm-2 col-form-label">Priority: </label></td>

            <td><asp:DropDownList ID="DropListPriority" runat="server">                
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>Highest</asp:ListItem>
                <asp:ListItem>High</asp:ListItem>
                <asp:ListItem>Medium</asp:ListItem>
                <asp:ListItem>Low</asp:ListItem>
                </asp:DropDownList></td> 
        </tr>
        <tr>
            <td><label for="inputPassword" class="col-sm-2 col-form-label">Ticket Created By: </label></td>
            <td><asp:Label ID="lblCreatedBy" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>
        <tr>
            <td><label for="inputPassword" class="col-sm-2 col-form-label">Assigned To: </label></td>
            <td><asp:Label ID="lblAssignedTo" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>
        <tr>
            <td><label for="inputStatus" class="col-sm-2 col-form-label">Change Status: </label></td>
            <td><asp:DropDownList ID="DropListStatus" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">               
                <asp:ListItem>Open</asp:ListItem>
                <asp:ListItem>Closed</asp:ListItem>
                <asp:ListItem>Re-Open</asp:ListItem>
                </asp:DropDownList></td>         
        </tr>
        <tr>
            <td><label for="inputPassword" class="col-sm-2 col-form-label">Date Assigned: </label></td>
            <td><asp:Label ID="lblDateAssigned" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
        </tr>
        <tr>
            <td><label for="inputRemarks" class="col-sm-2 col-form-label">Remarks: </label></td>
            <td><asp:TextBox ID="txtHelpDeskRemarks" runat="server" TextMode="MultiLine" ></asp:TextBox></td>
        </tr>
    </table>

    <br />
  
   <asp:Button ID="btnUpdateTicket" runat="server" Text="Confirm" OnClick="btnUpdateTicket_Click"/><br/><br/> 
   <asp:Label ID="lblMessage" runat="server" Text="" style="color: green;"></asp:Label>
       <hr/>    
 </div>
      
        <div class="Alignments11">

         <asp:Label ID="Label1" runat="server" Text="File Attachments"></asp:Label>
            <asp:GridView ID="gvAttachments" 
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
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKET_DOCUMENT")%>'/>
                          <asp:HiddenField ID="hdFilePath" runat="server" Value='<%# Eval("FILE_PATH")%>'/>
                          <asp:Image runat="server" ID="imgVisible2" ImageUrl="~/images/attachments.png"/>
                      </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="FILE_NAME" HeaderText="Attachment"/>             

                <asp:buttonfield ButtonType="Button" ItemStyle-Width="10%" CausesValidation="false" ControlStyle-CssClass="button" CommandName="Download" Text="Download"/>
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Attachments !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>
            <hr />
    </div>

        
  <div class="Alignments11">

         <asp:Label ID="lblTaskLog" runat="server" Text="Operation Log"></asp:Label>
            <asp:GridView ID="gvTicketslog" 
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
               <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image runat="server" ID="imgVisible2" ImageUrl="~/images/check.png"/>
                    </ItemTemplate>
               </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESC_LOG" HeaderText="Log Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_USER" HeaderText="User ID"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_LOG" HeaderText="Log Date"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARKS" HeaderText="Remarks"/>
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>
</div>
    <asp:HiddenField ID="hDFlagResolved" runat="server" />
</asp:Content>
