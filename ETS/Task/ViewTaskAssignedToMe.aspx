<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewTaskAssignedToMe.aspx.vb" Inherits="ETS.ViewTaskAssignedToMe" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /> 
    <div style="width: 1800px; margin: 0 auto;">
        <h3>Task > My Task</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>
   
    <div class="box-shadow center" style="width: 65%;">
        <h5 class="form-title">Search</h5>
        
        <table>
            <tr>
                <td><label for="inputTaskNumber" class="col-sm-2 col-form-label">Task No: </label></td>
                <td><asp:TextBox ID="txtTaskNumber" runat="server" class="form-control form-control-sm" placeholder="task number..."></asp:TextBox></td>
                <td><asp:Button ID="btnSearchTicket" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchTicket_Click"/></td>
            </tr>
        </table>
        </div>
    <br />
    <br />
        <asp:GridView ID="gvMyTask" 
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
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTask" runat="server" Value='<%# Eval("ID_TASK")%>'/>
                        <asp:HiddenField ID="hdIdUser" runat="server" Value='<%# Eval("ID_USER")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>                

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task Number"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TASK_DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="CATEGORY" HeaderText="App Category"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="USERNAME" HeaderText="Assigned To"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_ASSIGNED" HeaderText="Date Assigned"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TASK" HeaderText="Status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARK" HeaderText="Admin Remarks" Visible="false"/> 
                
                <asp:TemplateField HeaderText="Flag Issue" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>          
                        <asp:LinkButton ID="LinkViewFailedTest" runat="server" CommandName="Flag"><asp:Image runat="server" ID="ImgPassed" ImageUrl="~/images/flags.png" Visible='<%#fn_CheckStatus(Eval("FLAG_ISSUES"))%>'/></asp:LinkButton>  
                        <asp:Image runat="server" ID="ImgFailed" ImageUrl="~/images/check.png" Visible='<%#Not fn_CheckStatus(Eval("FLAG_ISSUES"))%>'/>
                    </ItemTemplate>
               </asp:TemplateField>

                <asp:buttonfield ButtonType="Button" ItemStyle-Width="10%" ControlStyle-CssClass="button" CommandName="Modifica" Text="Details"/>
                <asp:buttonfield ButtonType="Button" ItemStyle-Width="10%" ControlStyle-CssClass="button" CommandName="Remove" Text="Delete"/>
                              
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>
            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Task assigned to you !</div>
            </EmptyDataTemplate>

        </asp:GridView>
    


<asp:ModalPopupExtender ID="PopupViewTestError" 
                        BehaviorID="mpe1" 
                        runat="server"
                        PopupControlID="pnlPopup" 
                        TargetControlID="hdCardRequest" 
                        BackgroundCssClass="modalBackground"/>
     
    <asp:HiddenField ID="hdCardRequest" runat="server"/>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog" style="overflow : auto; height: 800px;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 1100px; border-radius: 5px;">
                <div class="modal-content" style="width: 1000px; margin: 0 auto;">
                    <br />
                    <div class="modal-header">                        
                 
                        <asp:Label ID="lblViewErrors" runat="server" Text="View Failed Test(s)"/>
                    </div>
                    <hr />

                    <div class="modal-body">

               
                <asp:GridView ID="gvFailedTest" 
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
                    
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                          <asp:HiddenField ID="hdIdSLR" runat="server" Value='<%# Eval("ID_TEST_CASE_DEFECTS")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>  

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="SERVICE_LEVEL_REQUIREMENT" HeaderText="Service Level Req."/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TEST_SCENERIO" HeaderText="Test Scenerio"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TEST_STEPS" HeaderText="Test Steps"/>               
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_RESULT" HeaderText="Expected Result"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ACTUAL_RESULT" HeaderText="Actual Result"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER" HeaderText="Tester"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_DEFECT" HeaderText="Status"/>                
                    
               <asp:TemplateField HeaderText="Flag" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>          
                        <asp:Image runat="server" ID="ImgFailed" ImageUrl="~/images/warning.png"/>
                    </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                      <asp:Button ID="btnDefect" runat="server" Text="View Defects" CommandName="View" class="button primary"/> 
                   </ItemTemplate>
               </asp:TemplateField>
                </Columns>

                <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>
                <EmptyDataTemplate>
                    <div style="color: red; text-align: center;">No Issues</div>
                </EmptyDataTemplate>
              </asp:GridView>
                        <hr />

            
                <div id="divGridViewDefect" runat="server" visible="false">
                <asp:Label ID="lblCaptionDefects" runat="server" Text="Defects"/> 
                    
                <asp:GridView ID="gvDefects" 
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
                                    
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEFECT_TYPE" HeaderText="Defect Type"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STEPS_TO_REPRODUCE" HeaderText="Test Steps"/>               
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_RESULT" HeaderText="Expected Result"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ACTUAL_RESULT" HeaderText="Actual Result"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER" HeaderText="Tester"/>
                    
               <asp:TemplateField HeaderText="Flag" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>          
                        <asp:Image runat="server" ID="ImgFailed" ImageUrl="~/images/warning.png"/>
                    </ItemTemplate>
               </asp:TemplateField>
                </Columns>

                <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>
                <EmptyDataTemplate>
                    <div style="color: red; text-align: center;">No Defects</div>
                </EmptyDataTemplate>

                  </asp:GridView>
                        </div>
              </div>

                    <div class="modal-footer">
                        <asp:Button ID="btClose" class="btn btn-danger" runat="server" Text="Close" OnClick="btClose_Click"/>
                    </div>
                    <br />
                </div>
            </div>   
</ContentTemplate>
 </asp:UpdatePanel>
</asp:Panel>     
    <asp:HiddenField ID="hdOpIdTask" runat="server" />
    <asp:HiddenField ID="hdOpIdTicket" runat="server" />
</asp:Content>
