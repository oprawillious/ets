<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewDetailsTestCase1.aspx.vb" Inherits="ETS.ViewDetailsTestCase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <h3>Test Case > View Test Case Details</h3><br />

    <div class="Alignments2">

        <table>
            <tr>
                <td><label for="inputPassword" class="col-sm-3 col-form-label">Test Case Defect Id: </label></td>
                <td><asp:TextBox ID="txtTestCaseNumber" runat="server" class="form-control form-control-sm" placeholder="Test Case Defect Id..."></asp:TextBox></td>
                <td><asp:Button ID="btnSearchTestCase" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchTestCase_Click"/></td>
            </tr>
        </table>           

        <asp:GridView ID="gvViewDetailsTestCase" 
             runat="server"
             AutoGenerateColumns="False"
             CellPadding="1"
             CellSpacing="1"
             GridLines="None"
             AllowPaging="false"
             CssClass="table-bordered table-sm"
             PagerStyle-CssClass="table pgr"     
             AlternatingRowStyle-CssClass="normal11" 
             Width="100%">
            <Columns> 
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTestCaseDefect" runat="server" Value='<%# Eval("ID_TEST_CASE_DEFECTS")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TEST_CASE_DEFECTS" HeaderText="ID Test Case Defects"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TEST_PAGE_NAME" HeaderText="Test Page Name"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ISSUE_DESCRIPTION" HeaderText="Issue Description"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEFECT_TYPE" HeaderText="Defect Type"/>               
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER_DEFECT" HeaderText="Tester Defect"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_DEFECT" HeaderText="Status Defect"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_OPEN_DEFECT" HeaderText="Date Open Defect"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_CLOSE_DEFECT" HeaderText="Date Close Defect"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_REOPEN_DEFECT" HeaderText="Date Reopen Defect"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_FIX_DEFECT" HeaderText="Date Fix Defect"/>
                <asp:buttonfield ButtonType="Button" ControlStyle-CssClass="button" CommandName="Modifica" Text="Edit "/>
                <asp:buttonfield ButtonType="Button" ControlStyle-CssClass="button" CommandName="Remove" Text="Delete "/>
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>
            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>
        </asp:GridView>

    </div>

<asp:ModalPopupExtender ID="PopupTestCaseDetails" 
                        BehaviorID="mpe1" 
                        runat="server"
                        PopupControlID="pnlPopup" 
                        TargetControlID="hdCardRequest" 
                        BackgroundCssClass="modalBackground"/>
     
    <asp:HiddenField ID="hdCardRequest" runat="server"/>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 600px;">
                <div class="modal-content" style="width: 500px; margin: 0 auto;">
                    <br />
                    <div class="modal-header">                        
                        <h3>Update Test Case Detail</h3>                           
                    </div>
                    <hr />
                    <div class="modal-body">
                        <h3><asp:label ID="lblEditTestCase" runat="server" Text=""></asp:label></h3><br />
                        <table>                      
                            <tr>
                                <td><label for="TEST_PAGE_NAME" class="col-sm-6 col-form-label">Test Page Name:<span style="color: red;"> *</span></label></td>
                                <td><asp:TextBox ID="txtTestPageName" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><label for="TESTER_DEFECT" class="col-sm-6 col-form-label">Tester Defect:<span style="color: red;"> *</span></label></td>
                                <td><asp:TextBox ID="txtTestDefect" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:TextBox></td>
                            </tr>
                            
                            <tr>
                                <td><label for="STATUS_DEFECT" class="col-sm-6 col-form-label">Status Defect:<span style="color: red;"> *</span></label></td>
                                <td><asp:TextBox ID="txtStatusdefect" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><label for="DEFECT_TYPE" class="col-sm-6 col-form-label">Defect Type:<span style="color: red;"> *</span></label></td>
                                <td><asp:DropDownList ID="DropListDefectType" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>Defect</asp:ListItem>
                                    <asp:ListItem>Suggestion</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>             
                            <tr>
                                <td><label for="inputPassword" class="col-sm-6 col-form-label">Issue Description<span style="color: red;"> *</span></label></td>
                                <td><asp:TextBox ID="txtIssueDescription" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>                       
                        </table>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btClose" class="btn btn-danger" runat="server" Text="Close" OnClick="btClose_Click"/>
                        <asp:Button ID="btnUpdateDetailsTestCase" class="btn btn-success" runat="server" Text="Confirm" OnClick="btnUpdateDetailsTestCase_Click"/><br /><br />
                      <asp:Label ID="lblAssign" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>   
                </ContentTemplate>
                    </asp:UpdatePanel>
        </asp:Panel>        
</asp:Content>

