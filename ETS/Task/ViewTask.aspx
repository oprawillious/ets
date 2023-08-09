<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewTask.aspx.vb" Inherits="ETS.ViewTask" %>
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


                <asp:TemplateField HeaderText="Test Cases">
                        <ItemTemplate>
                            <%--<asp:Label runat="server" Text='<%# GetTotalSallery(Convert.ToDouble(Eval("BasicSallery")),Convert.ToDouble(Eval("convRate")),Convert.ToDouble(Eval("houseRent"))) %>'></asp:Label>--%>
                            <asp:Label runat="server" Text='<%# fn_RetrieveTestCaseDetailsCount(Eval("ID_TASK")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TASK" HeaderText="Task Status"/>

                  <asp:TemplateField HeaderText="Test Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div ID="statusDiv" runat="server" visible='<%#fn_CheckCompleteStatus(Eval("ID_TASK"))%>'>
                        
                        <asp:Image runat="server" ID="ImgPass" ImageUrl="~/images/check.png" Visible='<%#fn_CheckCompleteStatus(Eval("ID_TASK"))%>' />
                        <asp:Image runat="server" ID="ImgFail" ImageUrl="~/images/close.png" Visible='<%#Not fn_CheckCompleteStatus(Eval("ID_TASK"))%>' />
                            </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARK" HeaderText="Admin Remarks"/> 
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEV_REMARKS" HeaderText="Developer Remarks"/> 
                    --%>
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:Button ID="btnModify" runat="server" Text="Update" CommandName="Update" class="button primary"/> 
                      </ItemTemplate>
                </asp:TemplateField>
                
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
            
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>


            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager"  />
        </asp:GridView>
   

    <asp:HiddenField ID="hdIdTask" runat="server"/>
</asp:Content>
