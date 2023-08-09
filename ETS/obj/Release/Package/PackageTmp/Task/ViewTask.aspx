﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewTask.aspx.vb" Inherits="ETS.ViewTask" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <style type="text/css">
    .Grid td
    {
        background-color: #eee;
        color: black;
        font-family: Arial;
        font-size: 10pt;
        line-height: 200%;
        cursor: pointer;
        width: 100px;
    }
    .header
    {
        background-color: #6C6C6C !important;
        color: White !important;
        font-family: Arial;
        font-size: 10pt;
        line-height: 200%;
        width: 100px;
        text-align: center;
    }

     .preformatted {
            white-space: pre-line;
        }
</style>
    

     
    <br />   
    
    <div style="width: 1800px; margin: 0 auto;">
        <h3>Task > View Task</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>
    <div class="box-shadow center" style="width: 65%;">
        <h5 class="form-title">Search</h5>
        
        <table>
            <tr>
                <td>
                    <label for="inputTaskNumber" class="col-sm-2 col-form-label">Task No: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtTaskNumber" runat="server" class="form-control form-control-sm" placeholder="task number..."></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearchTask" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchTask_Click" /></td>
                <td>
                    <asp:Button ID="btnNewTask" runat="server" Text="New Task" class="btn btn-success btn-sm mb-3" OnClick="btnNewTask_Click" /></td>
            </tr>
            <tr>
                <td>
                    <label for="Environment" class="col-sm-6 col-form-label">Filter Task By:<span style="color: red;"> </span></label></td>
                <td>
                    <asp:DropDownList ID="drplist" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>In Progress</asp:ListItem>
                        <asp:ListItem>Completed</asp:ListItem>
                        <asp:ListItem>Assigned</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>

        </table>

    </div>
    <br />
    <br />

        <asp:GridView ID="gvTask" 
             runat="server"
             AutoGenerateColumns="False"
             CellPadding="1" 
             CellSpacing="1"
             GridLines="Both"
             AllowPaging="True"
             CssClass="table-bordered table-sm box-shadow center"
             PagerStyle-CssClass="table pgr"     
             AlternatingRowStyle-CssClass="normal11" 
             Width="90%" GridLinesVisibility="True" >

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTask" runat="server" Value='<%# Eval("ID_TASK")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdStatus" runat="server" Value='<%# Eval("STATUS_TASK")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task Number" ItemStyle-CssClass="preformatted"/>
                <%--<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TICKETS" HeaderText="Ticket Number"/>--%>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TASK_DESCRIPTION" HeaderText="Description" ItemStyle-CssClass="preformatted"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="CATEGORY" HeaderText="App Category" ItemStyle-CssClass="preformatted"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority" ItemStyle-CssClass="preformatted"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TYPE_TASK" HeaderText="Type" ItemStyle-CssClass="preformatted"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_START_DATE" HeaderText="Expected Start Date" ItemStyle-CssClass="preformatted"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_END_DATE" HeaderText="Expected End Date" ItemStyle-CssClass="preformatted"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="USERNAME" HeaderText="Created By" ItemStyle-CssClass="preformatted"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned" ItemStyle-CssClass="preformatted"/>
                <%--<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TASK" HeaderText="Status"/>--%>
                  <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image runat="server" ID="ImgPass" ImageUrl="~/images/check.png" Visible='<%#fn_CheckStatus(Eval("ID_TASK"))%>' />
                        <asp:Image runat="server" ID="ImgFail" ImageUrl="~/images/close.png" Visible='<%#Not fn_CheckStatus(Eval("ID_TASK"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARK" HeaderText="Admin Remarks"/> 
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEV_REMARKS" HeaderText="Developer Remarks"/> 
                    --%>
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:Button ID="btnEdit" runat="server" Text="Details" CommandName="Modifica" class="button primary"/> 
                      </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Remove" class="button primary" /> 
                      </ItemTemplate>
                </asp:TemplateField>
            
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>                       
                          <asp:Button ID="btnComplete" runat="server" Text="Mark as Completed" CommandName="mark" class="button primary"  /> 
                      </ItemTemplate>
                </asp:TemplateField>
                 
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>


            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager"  />
        </asp:GridView>
   

    <asp:HiddenField ID="hdIdTask" runat="server"/>



      <!-- Modal Admin Remark-->
<asp:HiddenField ID="hdEnvironment" runat="server" />

    <asp:ModalPopupExtender ID="PopupAdminRemark"
        BehaviorID="mpe5"
        runat="server"
        PopupControlID="envPopup"
        TargetControlID="hdAdminRemark"
        BackgroundCssClass="modalBackground" />

    <asp:HiddenField ID="hdAdminRemark" runat="server" />
    <asp:Panel ID="envPopup" runat="server" CssClass="confirm-dialog">
        <asp:UpdatePanel ID="UpdatePanelEnvironment" runat="server">
            <ContentTemplate>
                <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 800px;">
                    <div class="modal-content" style="width: 700px; margin: 0 auto;">
                        <br />
                        <div class="modal-header">
                            <asp:Label ID="lblRemark" runat="server" Text="" />
                        </div>
                        <hr />
                        <div class="modal-body">
                            <table>
                                <tr id="tableRow" runat="server">
                                    <td>
                                        <label for="Environment" class="col-sm-6 col-form-label">Enter Remark:<span style="color: red;"> </span></label></td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" runat="server" class="form-control form-control-sm" placeholder="Enter Task Remarks..." aria-label=".form-control-sm example" TextMode="MultiLine">
                                            
                                        </asp:TextBox>
 <%--<asp:TextBox ID="txttaskremarks" runat="server" class="form-control form-control-sm" placeholder="Enter Task Remarks..." aria-label=".form-control-sm example" required="required" TextMode="MultiLine"></asp:TextBox>--%>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnCloseRemark" class="btn btn-danger" runat="server" Text="Close" OnClick="btnCloseRemark_Click" />
                            <asp:Button ID="btnConfirmRemark" class="btn btn-success" runat="server" Text="Confirm" OnClick="btnConfirmRemark_Click" Style="float: right;" /><br />
                            <br />
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


</asp:Content>
