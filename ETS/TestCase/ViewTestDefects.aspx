<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewTestDefects.aspx.vb" Inherits="ETS.ViewTestDefects" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
   <ContentTemplate>
            <div class="Alignments2">
                <h3>SLR > View Test Defects</h3>
                <hr />
                <asp:Label ID="lblCaption" runat="server" Text=""/><br />
                <br />

                <table>
                    <tr>
                        <td><label for="subIssueType" class="col-sm-6 col-form-label">Status:<span style="color: red;"> *</span></label></td>
                        <td>
                            <asp:DropDownList ID="DropListStatus" Width="400" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" OnSelectedIndexChanged="DropListStatus_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="Y">Fixed</asp:ListItem>
                                <asp:ListItem Value="N">Not Fixed</asp:ListItem>
                            </asp:DropDownList></td>

                        <td><asp:Button ID="btnAddDefect" runat="server" Text="Add Defect" OnClick="btn_AddDefects" /></td>
                           
                    </tr>

                </table>
            </div>

            <div class="Alignments2">
                <asp:GridView ID="gvTestDefects"
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

                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdIdTestCaseDefect" runat="server" Value='<%# Eval("ID_TEST_DEFECT")%>'/>
                                <asp:CheckBox ID="chkDefect" runat="server" Text="."/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TEST_DEFECT" HeaderText="Defect Code" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="APP_CATEGORY" HeaderText="App Category" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="SLR" HeaderText="SLR" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEFECT_TYPE" HeaderText="Defect Type" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STEPS_TO_REPRODUCE" HeaderText="Steps to Reproduce" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_RESULT" HeaderText="Expected Result" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ACTUAL_RESULT" HeaderText="Actual Result" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PRIORITY" HeaderText="Priority" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER" HeaderText="Tester" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS" HeaderText="Status" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_OPENED" HeaderText="Date Opened" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_FIXED" HeaderText="Date Closed" />
                       

                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                                   <asp:Button ID="btnEdit" runat="server" Text="Details" CommandName="Modifica" class="button primary"/> 
                              </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                                   <asp:Button ID="btnAttachment" runat="server" Text="View Attachments" CommandName="Attachments" class="button primary"/> 
                              </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate>
                                   <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Remove" class="button primary" OnClientClick="return ConfirmDelete();"/> 
                              </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
                    <EmptyDataTemplate>
                        <div style="color: red; text-align: center;">No Defects Added <asp:LinkButton ID="lnkOpenPopUp" runat="server" OnClick="lnkOpenPopUp_Click" style="color: green;"> Click to Add Defect</asp:LinkButton></div>
                    </EmptyDataTemplate>

                </asp:GridView>
            </div>

            <asp:ModalPopupExtender ID="PopUpAddDefects"
                BehaviorID="mpe"
                runat="server"
                PopupControlID="pnlPopup1"
                TargetControlID="hdAssignDefects"
                BackgroundCssClass="modalBackground" />

            <asp:HiddenField ID="hdAssignDefects" runat="server" />
            <asp:Panel ID="pnlPopup1" runat="server" CssClass="confirm-dialog">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 800px;">
                            <div class="modal-content" style="width: 700px; margin: 0 auto;">
                                <br />
                                <div class="modal-header">
                                    <h3><asp:Label ID="lblPopUpCaption" runat="server" Text=""></asp:Label></h3>
                                </div>
                                <hr />
                                <div class="modal-body">
                                    <table>

                                        <tr>
                                            <td><label for="inputUser1" class="col-sm-6 col-form-label">Issue Description:<span style="color: red;"> *</span></label></td>
                                            <td><asp:TextBox ID="txtIssueDesc" runat="server" class="form-control form-control-sm" placeholder="Issue Description" aria-label=".form-control-sm example" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td><label for="inputUser2" class="col-sm-6 col-form-label">Defect Type:</label></td>
                                                
                                            <td>
                                                <asp:DropDownList ID="DropListDefectType" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Bug</asp:ListItem>
                                                    <asp:ListItem>Suggestion</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>

                                        <tr>
                                            <td><label for="inputUser2" class="col-sm-6 col-form-label">Steps To Reproduce:</label></td>
                                            <td><asp:TextBox ID="txtStepsToRepr" runat="server" class="form-control form-control-sm" placeholder="Remarks..." aria-label=".form-control-sm example" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td><label for="inputUser1" class="col-sm-6 col-form-label">Expected Result:<span style="color: red;"> *</span></label></td>
                                            <td><asp:TextBox ID="txtExpectedResult" runat="server" class="form-control form-control-sm" placeholder="Issue Description" aria-label=".form-control-sm example"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td><label for="inputUser1" class="col-sm-6 col-form-label">Actual Result:<span style="color: red;"> *</span></label></td>
                                            <td><asp:TextBox ID="txtActualResult" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td><label for="inputPriority" class="col-sm-6 col-form-label">Priority:<span style="color: red;"> *</span></label></td>
                                            <td>
                                                <asp:DropDownList ID="DropListPriority" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Highest</asp:ListItem>
                                                    <asp:ListItem>High</asp:ListItem>
                                                    <asp:ListItem>Medium</asp:ListItem>
                                                    <asp:ListItem>Low</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>

                                        <tr id="trStatus" runat="server" visible="false">
                                            <td><label for="inputPriority" class="col-sm-6 col-form-label">Status:<span style="color: red;"> *</span></label></td>
                                            <td>
                                                <asp:DropDownList ID="DropListDefectStatus" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Fixed</asp:ListItem>
                                                    <asp:ListItem>Not Fixed</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>

                                        <tr id="trAttachment" runat="server">
                                            <td><label for="inputPriority" class="col-sm-6 col-form-label">Add Attachments:<span style="color: red;"> *</span></label></td>
                                            <td><asp:FileUpload ID="DefectsFileUpload" runat="server" AllowMultiple="true" class="form-control"/></td>
                                        </tr>

                                    </table>

                                </div>

                                <div class="modal-footer">
                                    <asp:Button ID="btnClosePopDefect" class="btn btn-danger" runat="server" Text="Close" OnClick="btnClosePopDefect_Click"/>
                                    <asp:Button ID="btnCreateDefect" class="btn btn-success" runat="server" Text="Confirm" Style="float: right;" OnClick="btnCreateDefect_Click" /><br />
                                    <br />
                                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
         
       <asp:HiddenField ID="hdIdTC" runat="server" />
       <asp:HiddenField ID="hdIdTestDefect" runat="server" />
       <asp:HiddenField ID="hdOp" runat="server" />

        </ContentTemplate>

    <Triggers>
        <asp:PostBackTrigger ControlID="btnCreateDefect"/>
    </Triggers>

    </asp:UpdatePanel>
</asp:Content>
