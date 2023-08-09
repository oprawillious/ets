<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertUpdateTestCase1.aspx.vb" Inherits="ETS.InsertUpdateTestCase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="Alignments2">
        <br />
        <h3>Test Case > View Test Case > Update Test Case</h3>
        <hr class="greenLine" />
</div>
    <div class="box-shadow center" style="width: 80%;">
        <h5 class="form-title"><asp:Label ID="lblEditTestCase" runat="server" Text=""></asp:Label></h5>
        
        <h3>
            <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label></h3>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnInsertTestCaseDetails" runat="server" Text="+ Add TestCase Details" CssClass="btn btn-success" OnClick="btnInsertTestCaseDetails_Click" /></td>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Text="" Style="color: red;"></asp:Label></td>
                <td colspan="8"></td>
                <td id="tdmarkAsComplete" runat="server">
                    <asp:Button ID="Button3" runat="server" Text="Mark as Complete" CssClass="btn btn-success" OnClick="btnMarkAsComplete_Click" /></td>
                <td>
                    <asp:Label Text="" runat="server" ID="lbcompleteTest" /></td>
            </tr>
        </table>

       </div>

    
    <br /><br />

    <div class="Alignments2">
        <h3>Test Case Details</h3>         
        <asp:GridView ID="gvViewDetailsTestCase"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            AllowPaging="false"
            CssClass="table-bordered table-sm box-shadow center"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="90%">
            <Columns>



                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TEST_CASE_DEFECTS" HeaderText="Id TestCase Details" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TEST_STEPS" HeaderText="Test Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_RESULT" HeaderText="Expected Result" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ACTUAL_RESULT" HeaderText="Actual Result" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER" HeaderText="Tester" />
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">

                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdTestCaseDefect" runat="server" Value='<%# Eval("ID_TEST_CASE_DEFECTS")%>' />
                        <asp:HiddenField ID="hdActualResult" runat="server" Value='<%# Eval("ACTUAL_RESULT")%>' />
                        <asp:HiddenField ID="hdflagcloseDefect" runat="server" Value='<%# Eval("FLAG_CLOSE_DEFECT")%>' />
                        <asp:HiddenField ID="hdTestmarkedAsCompleted" runat="server" Value='<%# Eval("TEST_MARKED_AS_COMPLETED")%>' />
                    </ItemTemplate>

                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image runat="server" ID="ImgPass" ImageUrl="~/images/check.png" Visible='<%#fn_CheckStatus(Eval("STATUS_DEFECT"))%>' />
                        <asp:Image runat="server" ID="ImgFail" ImageUrl="~/images/close.png" Visible='<%#Not fn_CheckStatus(Eval("STATUS_DEFECT"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Modifica" class="button primary" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="btnStatus" runat="server" Text="status" CommandName="status" class="button primary" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="btnDeleteSLR" runat="server" Text="Delete" CommandName="Remove" class="button primary" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No TestCase Detail Added Yet</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <br />
    <br />

    <!-- Modal Test Case Details-->
    <asp:ModalPopupExtender ID="PopupTestCaseDetails"
        BehaviorID="mpe1"
        runat="server"
        PopupControlID="pnlPopup"
        TargetControlID="hdCardRequest"
        BackgroundCssClass="modalBackground" />

    <asp:HiddenField ID="hdCardRequest" runat="server" />
    <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 800px;">
                    <div class="modal-content" style="width: 700px; margin: 0 auto;">
                        <br />
                        <div class="modal-header">
                            <%--<h3>Details Test Case</h3>--%>
                            <asp:Label ID="lblDetailsTestCase" runat="server" Text="" />
                        </div>
                        <hr />
                        <div class="modal-body">
                            <table>
                                <tr>
                                    <td>
                                        <label for="DEFECT_TYPE" class="col-sm-6 col-form-label">Test Description:<span style="color: red;"> *</span></label></td>
                                    <td>
                                        <asp:TextBox ID="txtTestSteps" runat="server" class="form-control form-control-sm" TextMode="MultiLine" aria-label=".form-control-sm example"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td>
                                        <label for="inputPassword" class="col-sm-6 col-form-label">Expected Result<span style="color: red;"> *</span></label></td>
                                    <td>
                                        <asp:TextBox ID="txtExpectedResult" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" TextMode="MultiLine"></asp:TextBox></td>
                                </tr>

                                <tr>
                                    <td>
                                        <label for="inputPassword" class="col-sm-6 col-form-label">Actual Result<span style="color: red;"> *</span></label></td>
                                    <td>
                                        <asp:TextBox ID="txtActualResult" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" TextMode="MultiLine"></asp:TextBox></td>
                                </tr>

                            </table>

                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btClose" class="btn btn-danger" runat="server" Text="Close" OnClick="btClose_Click" />
                            <asp:Button ID="btnConfirmDetailsTestCase" class="btn btn-success" runat="server" Text="Confirm" OnClick="btnConfirmDetailsTestCase_Click" Style="float: right;" /><br />
                            <br />
                            <asp:Label ID="lblAssign" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:HiddenField ID="hdId" runat="server" />

    <!--change status pop up starts here-->
    <asp:ModalPopupExtender ID="ChangeStatusTestPopUp"
        BehaviorID="mpe3"
        runat="server"
        PopupControlID="statusPopUp"
        TargetControlID="hdCard"
        BackgroundCssClass="modalBackground" />


    <asp:HiddenField ID="hdCard" runat="server" />
    <asp:Panel ID="statusPopUp" runat="server" CssClass="confirm-dialog">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 800px;">
                    <div class="modal-content" style="width: 700px; margin: 0 auto;">
                        <br />
                        <div class="modal-header">
                            <%--<h3>Details Test Case</h3>--%>
                            <asp:Label ID="lblstatus" runat="server" Text="" />
                        </div>
                        <hr />
                        <div class="modal-body">
                            <table>
                                <tr id="tr1" runat="server">
                                    <td>
                                        <label for="STATUS_TEST" class="col-sm-6 col-form-label">Test Status:<span style="color: red;"> *</span></label></td>
                                    <td>
                                        <asp:DropDownList ID="DropListStatusTest" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Passed</asp:ListItem>
                                            <asp:ListItem>Failed</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                            </table>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="Button1" class="btn btn-danger" runat="server" Text="Close" OnClick="btnClose_Status_Test_Click" />
                            <asp:Button ID="Button2" class="btn btn-success" runat="server" Text="Confirm" OnClick="btnChange_Status_Click" Style="float: right;" /><br />
                            <br />
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:HiddenField ID="hdStatus" runat="server" />

    <asp:ModalPopupExtender ID="errMsgPopup"
        BehaviorID="mpe4"
        runat="server"
        PopupControlID="errPopup"
        TargetControlID="hpop"
        BackgroundCssClass="modalBackground" />

    <asp:HiddenField ID="hpop" runat="server" />
    <asp:Panel ID="errPopup" runat="server" CssClass="confirm-dialog">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 800px;">
                    <div class="modal-content" style="width: 700px; margin: 0 auto;">
                        <br />
                        <div class="modal-header">
                            <%--<h3>Details Test Case</h3>--%>
                            <asp:Label ID="Label1" runat="server" Text="" />
                        </div>
                        <hr />
                        <div class="modal-body">
                            <table>
                                <tr id="tr6" runat="server">
                                    <td>
                                        <asp:Label Text="" runat="server" ID="lblpopUpErrmsg" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button4" class="btn btn-danger" runat="server" Text="Close" OnClick="btn_Err_msg_Close_Click" />
                            <br />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:HiddenField ID="HiddenField2" runat="server" />

    <div class="Alignments2">
        <asp:Label ID="lblTaskLog" runat="server" Text="Test Case Log"></asp:Label>
        <asp:GridView ID="gvTestCaseLog"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            AllowPaging="True"
            PageSize="10"
            CssClass="table-bordered table-sm box-shadow center"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="90%">
            <Columns>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TEST_CASE_DEFECTS" HeaderText="Code" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_LOG" HeaderText="Date" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESC_LOG" HeaderText="Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ACTUAL_RESULT" HeaderText="Actual Result" />
               <%-- <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <asp:Image runat="server" ID="ImgPassed" ImageUrl="~/images/check.png" Visible='<%#fn_CheckStatus(Eval("STATUS_TEST"))%>' />
                        <asp:Image runat="server" ID="ImgFailed" ImageUrl="~/images/close.png" Visible='<%#Not fn_CheckStatus(Eval("STATUS_TEST"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_USER" HeaderText="User ID" />

            </Columns>

            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red;">No Log</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>

        <asp:HiddenField ID="hdDefectId_log" runat="server" />
        <asp:HiddenField runat="server" ID="hdOpIdTestcase" />
        <asp:HiddenField runat="server" ID="hdActualresult" />


    </div>
</asp:Content>
