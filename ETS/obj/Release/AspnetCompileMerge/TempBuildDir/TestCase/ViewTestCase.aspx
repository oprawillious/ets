<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewTestCase.aspx.vb" Inherits="ETS.ViewTestCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    
    <div style="width: 1800px; margin: 0 auto;">
        <h3>Test Case > View Test Cases</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>
        
    <div class="box-shadow center" style="width: 65%;">
        <h5 class="form-title">Search</h5>
        <table>
            <tr>
                <td><label for="inputPassword" class="col-sm-3 col-form-label">Test Case Id: </label></td>
                <td><asp:TextBox ID="txtTestCaseNumber" runat="server" class="form-control form-control-sm" placeholder="Test Case Id..." style="width: 250px;"></asp:TextBox></td>
                <td><asp:Button ID="btnSearchTestCase" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchTestCase_Click"/></td>
                <td><asp:Button ID="addNewTestCase" runat="server" Text="Add New TestCase" class="btn btn-primary btn-sm mb-3" OnClick="btnAddNewTestCase_Click"/></td>
            </tr>
        </table>           
        </div>
    <br />   <br />
        <asp:GridView ID="gvTestCase" 
             runat="server"
             AutoGenerateColumns="False"
             CellPadding="1"
             CellSpacing="1"
             GridLines="None"
             AllowPaging="true"
             CssClass="table-bordered table-sm box-shadow center"
             PagerStyle-CssClass="table pgr"     
             AlternatingRowStyle-CssClass="normal11" 
             Width="75%">
            <Columns>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTestCase" runat="server" Value='<%# Eval("ID_TEST_CASES")%>'/>
                        <asp:HiddenField ID="hdIdTask" runat="server" Value='<%# Eval("ID_TASK")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>'/>
                        <asp:HiddenField ID="hdAppCategory" runat="server" Value='<%# Eval("APP_CATEGORY")%>'/>
                        <asp:HiddenField ID="hdDescription" runat="server" Value='<%# Eval("DESCRIPTION")%>'/>

                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TEST_CASES" HeaderText="Test Case Id"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task Id"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description"/>             
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="APP_CATEGORY" HeaderText="App Category"/>
                <%--<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEVELOPER" HeaderText="Developer"/>--%>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER" HeaderText="Tester"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TEST" HeaderText="Test Status"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_START_TEST" HeaderText="Date Started"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_COMPLETE_TEST" HeaderText="Date Completed"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ENVIROMENT" HeaderText="Environment"/>
                
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                
                          <asp:Image runat="server" ID="ImgPassed" ImageUrl="~/images/check.png" Visible='<%#fn_CheckStatus(Eval("STATUS_TEST"))%>'/>
                          <asp:Image runat="server" ID="ImgFailed" ImageUrl="~/images/close.png" Visible='<%#Not fn_CheckStatus(Eval("STATUS_TEST"))%>'/>
                    </ItemTemplate>
               </asp:TemplateField>

                <asp:buttonfield ButtonType="Button" ControlStyle-CssClass="button" CommandName="Details" Text="Details"/>
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>
            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Test Case found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>

 

    <asp:HiddenField runat="server" ID="hdS_status" />
    <asp:HiddenField runat="server" ID="hd_AppCategory" />
    <asp:HiddenField runat="server" ID="hdDescription" />
</asp:Content>
