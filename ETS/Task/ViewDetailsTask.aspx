<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewDetailsTask.aspx.vb" Inherits="ETS.ViewDetailsTask" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div style="width: 1800px; margin: 0 auto;">
        <h3>Task > View Task > Details Task</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>

    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">

        <ContentTemplate>

            
               
                <br />

            <div class="box-shadow center" style="width: 90%;">
                <h5 class="form-title">
                    <asp:Label ID="lblDetailTask" runat="server" Text=""></asp:Label></h5>

                <table class="">

                    <tr>
                        <td>
                            <label for="inputDescription" class="col-sm-2 col-form-label">Description: </label>
                        </td>
                        <td>
                            <asp:Label ID="lblDescription" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>

                        <td>
                            <label for="inputAssignedTo" class="col-sm-2 col-form-label">Category: </label>
                        </td>
                        <td>
                            <asp:Label ID="lblCategory" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
                    </tr>

                    <tr>
                    </tr>

                    <tr>
                        <td>
                            <label for="inputAssignedTo" class="col-sm-2 col-form-label">Assign By: </label>
                        </td>
                        <td>
                            <asp:Label ID="lblAssignedTo" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>

                        <td>
                            <label for="inputDateAssigned" class="col-sm-2 col-form-label">Date Assigned: </label>
                        </td>
                        <td>
                            <asp:Label ID="lblDateAssigned" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
                    </tr>

                    <tr>
                    </tr>

                    <tr>
                        <td>
                            <label for="inputStatus" class="col-sm-2 col-form-label">Status: </label>
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>

                        <td>
                            <label for="inputStartDate" class="col-sm-2 col-form-label">Start Date: </label>
                        </td>
                        <td>
                            <asp:Label ID="lblStartDate" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
                    </tr>

                    <tr>
                    </tr>

                    <tr>
                        <td>
                            <label for="inputEndDate" class="col-sm-2 col-form-label">End Date: </label>
                        </td>
                        <td>
                            <asp:Label ID="lblEndDate" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>

                        <%--<td>
                    <label for="inputRemarks" class="col-sm-2 col-form-label">Remarks: </label>
                </td>
                <td>
                    <asp:Label ID="lblRemarks" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>--%>
                    </tr>

                    <tr>
                    </tr>

                </table>
            </div>

                <div hidden style="width: 1700px; margin: 0 auto;">
                <asp:Button ID="btnAssignAsTask" runat="server" Text="Assign as Task" CssClass="btn btn-success" OnClick="btnAssignAsTask_Click" />
                </div>
             <br />
            <div style="width: 90%; margin: 0 auto;">
                <asp:Button ID="btnStartTask" runat="server" Text="Start this Task" CssClass="btn btn-success" OnClick="btnStartTask_Click"/>
                <asp:Button ID="btnMarkAsComplete" class="btn btn-success" runat="server" Text="Mark As Complete" OnClick="btnMarkAsComplete_Click" Visible="false"/><br />
                </div>
                <asp:Label ID="lblMessage" runat="server" Text="" Style="color: green;" visible="false"></asp:Label>
                <%--<hr />--%>

               <br />  
            <div class="ubea-accordion" style="width: 2000px; margin: 0 auto;">
                <div class="ubea-accordion-heading">

                    <div class="icon" style="width: 90%;"><i class="icon-cross"></i></div>
                    <h3>Developers</h3>
                </div>

                <div class="ubea-accordion-content">
                    <div class="inner">

                        <asp:GridView ID="gvDevelopers"
                            runat="server"
                            AutoGenerateColumns="False"
                            CellPadding="1"
                            CellSpacing="1"
                            GridLines="None"
                            CssClass="table-bordered table-sm box-shadow center"
                            PagerStyle-CssClass="table pgr"
                            AlternatingRowStyle-CssClass="normal11" Width="90%">

                            <Columns>



                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdIdDv" Visible="false" runat="server" Value='<%# Eval("ID_USERS")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdIdDvTask" Visible="false" runat="server" Value='<%# Eval("ID_TASK")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="FIRST_NAME" HeaderText="User" />

                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="APP_CATEGORY" HeaderText="App Category" />

                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="REMARKS" HeaderText="Remark" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_STARTED" HeaderText="Date Started" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_COMPLETED" HeaderText="Date Completed" />

                                <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">

                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Remarks" CommandName="Modifica" class="button primary" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>

                            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
                            <EmptyDataTemplate>
                                <div style="color: red; text-align: center;">Data Not found!</div>
                            </EmptyDataTemplate>

                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                        </asp:GridView>

                    </div>
                </div>
            </div>



                <div class="ubea-accordion" style="width: 2000px; margin: 0 auto;">
					<div class="ubea-accordion-heading">

						<div class="icon" style="width: 90%;"><i class="icon-cross"></i></div>
							<h3>Testers</h3>
						</div>

						<div class="ubea-accordion-content">
						   <div class="inner">
                
              
                            <asp:GridView ID="gvTesters"
                                runat="server"
                                AutoGenerateColumns="False"
                                CellPadding="1"
                                CellSpacing="1"
                                GridLines="None"
                                CssClass="table-bordered table-sm box-shadow center"
                                PagerStyle-CssClass="table pgr"
                                AlternatingRowStyle-CssClass="normal11" Width="90%"
                                >
                                <columns>

                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdIdDv1" Visible="false" runat="server" Value='<%# Eval("ID_USERS")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="FIRST_NAME" HeaderText="User" />

                                    <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ROLE_DESCRIPTION" HeaderText="Role" />

        
                                </columns>

                                <selectedrowstyle backcolor="#fba557" font-bold="True" forecolor="#ffffff" />
                                <emptydatatemplate>
                                    <div style="color: red; text-align: center;">Data Not found!</div>
                                </emptydatatemplate>

                                <pagerstyle horizontalalign="Center" cssclass="GridPager" />
                            </asp:GridView>


                                 </div>
					</div>
				</div>

                <br />
                <br />
                <br />

            <div style="width: 90%; margin: 0 auto;">

                <asp:Button ID="btnInsertTestCase" runat="server" Text="Add Test Case" CssClass="btn btn-success" OnClick="btnInsertTestCase_Click" /></td>
                 
                <br />
                <br />
                <h2>Test Cases</h2>

                <asp:GridView ID="gvTestCase"
                    runat="server"
                    AutoGenerateColumns="False"
                    CellPadding="1"
                    CellSpacing="1"
                    GridLines="None"
                    AllowPaging="true"
                    CssClass="table-bordered table-sm box-shadow center"
                    PagerStyle-CssClass="table pgr"
                    DataKeyNames="ID_TEST_CASES"
                    AlternatingRowStyle-CssClass="normal11" Width="90%">
                    <Columns>

                        <asp:TemplateField ItemStyle-Width="20px">
                            <ItemTemplate>
                                <a href="JavaScript:divexpandcollapse ('div<%# Eval("ID_TEST_CASES") %>');">
                                    <img alt="Details" id="imgdiv<%# Eval("ID_TEST_CASES") %>" src="../images/plus.png" />
                                </a>
                                <div id="div<%# Eval("ID_TEST_CASES") %>" style="display: none;">
                                    <asp:GridView ID="grdDetails"
                                        runat="server"
                                        AutoGenerateColumns="false"
                                        DataKeyNames="ID_TEST_CASES"
                                        CssClass="ChildGrid">
                                        <Columns>
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="TEST_STEPS" HeaderText="Test Steps" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EXPECTED_RESULT" HeaderText="Expected Result" ItemStyle-CssClass="preformatted" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="ACTUAL_RESULT" HeaderText="Actual Result" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="ID_USER" HeaderText="User" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" DataField="DATE_LOG" HeaderText="Date" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%-- <asp:BoundField
                                                        ItemStyle-Width="150px"
                                                        DataField='<%#fn_CheckStatus(Eval("STATUS_DEFECT"))%>'
                                                        HeaderText="" />--%>
                                                    <asp:Image runat="server" ID="ImgPass" ImageUrl="~/images/check.png" Visible='<%#Eval("STATUS_DEFECT") = "Passed"%>' />
                                                    <asp:Image runat="server" ID="ImgFail" ImageUrl="~/images/close.png" Visible='<%#Eval("STATUS_DEFECT") = "Failed"%>' />
                                                    <asp:Image runat="server" ID="ImgNoStatus" ImageUrl="" AlternateText="" Visible='<%#Eval("STATUS_DEFECT") <> "Failed" And Eval("STATUS_DEFECT") <> "Passed"%>' />

                                                    <%-- <%# If(Eval("STATUS_DEFECT") <> "Failed" And Eval("STATUS_DEFECT") <> "Passed")  %>
                                                    <%# Dim tt = 98  %>
                                                    <%# End If  %>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdIdTestCase" runat="server" Value='<%# Eval("ID_TEST_CASES")%>' />
                                <asp:HiddenField ID="hdIdTestCaseTask" runat="server" Value='<%# Eval("ID_TASK")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdIdTestCase1" runat="server" Value='<%# Eval("ID_TEST_CASES")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdIdTicket" runat="server" Value='<%# Eval("ID_TICKETS")%>' />
                                <asp:HiddenField ID="hdAppCategory" runat="server" Value='<%# Eval("APP_CATEGORY")%>' />
                                <asp:HiddenField ID="hdDescription" runat="server" Value='<%# Eval("DESCRIPTION")%>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TEST_CASES" HeaderText="Test Case Id" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_TASK" HeaderText="Task Id" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="APP_CATEGORY" HeaderText="App Category" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEVELOPER" HeaderText="Developer" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="TESTER" HeaderText="Tester" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="STATUS_TEST" HeaderText="Test Status" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_START_TEST" HeaderText="Date Started" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_COMPLETE_TEST" HeaderText="Date Completed" />

                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:ButtonField ButtonType="Button" ControlStyle-CssClass="button" CommandName="Duplicate" Text="Duplicate" />

                    </Columns>

                    <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />
                    <EmptyDataTemplate>
                        <div style="color: red; text-align: center;">No Test Case found !</div>
                    </EmptyDataTemplate>

                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>


            </div>



                
                




            <asp:ModalPopupExtender ID="PopupStartTask"
                BehaviorID="mpe"
                runat="server"
                PopupControlID="pnlPopup"
                TargetControlID="hdCardRequest"
                BackgroundCssClass="modalBackground" />

            <asp:HiddenField ID="hdCardRequest" runat="server" />
            <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 600px;">
                            <div class="modal-content" style="width: 500px; margin: 0 auto;">
                                <br />
                                <div class="modal-header">
                                    <h3 align="center">Start Task:</h3>
                                </div>
                                <hr />

                                <div class="modal-body">
                                    <table>
                                        <tr>
                                            <td><label for="Remarks" class="col-sm-3 col-form-label">Remarks</label></td>
                                            <td><asp:TextBox ID="txtDevRemarks" runat="server" class="form-control form-control-sm" placeholder="Remarks..." aria-label=".form-control-sm example" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                  </table>
                                </div>

                                <div class="modal-footer">
                                    <asp:Button ID="btClose" class="btn btn-danger" runat="server" Text="Close" OnClick="btClose_Click" />
                                    <asp:Button ID="btnStart" class="btn btn-success" runat="server" Text="Confirm" OnClick="btnStart_Click" Style="float: right;" /><br />
                                    <br />
                                    <asp:Label ID="lblAssign" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>



   
            <asp:ModalPopupExtender ID="PopUpAssignTask"
                BehaviorID="mpe"
                runat="server"
                PopupControlID="pnlPopup1"
                TargetControlID="hdAssignTask"
                BackgroundCssClass="modalBackground" />

            <asp:HiddenField ID="hdAssignTask" runat="server" />
            <asp:Panel ID="pnlPopup1" runat="server" CssClass="confirm-dialog">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 100%; width: 600px;">
                            <div class="modal-content" style="width: 500px; margin: 0 auto;">
                                <br />
                                <div class="modal-header">
                                    <h3>Assign To:</h3>
                                </div>
                                <hr />
                                <div class="modal-body">
                                    <table>
                                        <tr>
                                            <td><label for="inputUser1" class="col-sm-6 col-form-label">Developer:<span style="color: red;"> *</span></label></td>
                                            <td><asp:DropDownList ID="DropListDev1" runat="server" DataValueField="USERS" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:DropDownList></td>
                                        </tr>

                                        <tr>
                                            <td><label for="inputUser2" class="col-sm-6 col-form-label">Developer:</label></td>
                                            <td><asp:DropDownList ID="DropListDev2" runat="server" DataValueField="USERS" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:DropDownList></td>
                                        </tr>
                              
                                        <tr>
                                            <td><label for="inputPriority" class="col-sm-6 col-form-label">Priority:<span style="color: red;"> *</span></label></td>
                                            <td><asp:DropDownList ID="DropListPriority" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Highest</asp:ListItem>
                                                    <asp:ListItem>High</asp:ListItem>
                                                    <asp:ListItem>Medium</asp:ListItem>
                                                    <asp:ListItem>Low</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        
                                        <tr>
                                            <td><label for="inputPassword" class="col-sm-6 col-form-label">Start Date:<span style="color: red;"> *</span></label></td>
                                            <td><asp:TextBox ID="txtStartDate" runat="server" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" />
                                                <ajaxToolkit:CalendarExtender runat="server"
                                                    ID="CalendarExtender2"
                                                    Format="MM/dd/yyyy"
                                                    PopupButtonID="ibtnCalendarCalendarStart"
                                                    TargetControlID="txtStartDate"></ajaxToolkit:CalendarExtender>
                                                <asp:MaskedEditExtender runat="server"
                                                    ID="MaskedEditExtender1"
                                                    TargetControlID="txtStartDate"
                                                    Mask="99/99/9999"
                                                    MaskType="Date"
                                                    DisplayMoney="Left"
                                                    AcceptNegative="Left" />
                                                <asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1"
                                                    runat="server"
                                                    ControlToValidate="txtStartDate"
                                                    CssClass="txtObbligatorio"
                                                    Display="None"
                                                    SetFocusOnError="True" /></td>
                                            <td><asp:ImageButton ID="ibtnCalendarCalendarStart" ImageUrl="~/images/_calendar.png" Width="20px" runat="server" /></td>
                                        </tr>
                                       
                                        <tr>
                                            <td><label for="inputPassword" class="col-sm-6 col-form-label">End Date:<span style="color: red;"> *</span></label></td>
                                            <td><asp:TextBox ID="txtCloseDate" runat="server" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" />
                                                <ajaxToolkit:CalendarExtender runat="server"
                                                    ID="CalendarExtender1"
                                                    Format="MM/dd/yyyy"
                                                    PopupButtonID="ibtnCalendarCalendarLandend"
                                                    TargetControlID="txtCloseDate"></ajaxToolkit:CalendarExtender>
                                                <asp:MaskedEditExtender runat="server"
                                                    ID="MaskedEditExtender7"
                                                    TargetControlID="txtCloseDate"
                                                    Mask="99/99/9999"
                                                    MaskType="Date"
                                                    DisplayMoney="Left"
                                                    AcceptNegative="Left" />
                                                <asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator3"
                                                    runat="server"
                                                    ControlToValidate="txtCloseDate"
                                                    CssClass="txtObbligatorio"
                                                    Display="None"
                                                    SetFocusOnError="True" /></td>
                                            <td><asp:ImageButton ID="ibtnCalendarCalendarLandend" ImageUrl="~/images/_calendar.png" Width="20px" runat="server" /></td>
                                        </tr>

                                        <tr>
                                            <td><label for="inputRemarks" class="col-sm-3 col-form-label">Remarks:</label></td>
                                            <td><asp:TextBox ID="txtAdminRemarks" runat="server" class="form-control form-control-sm" placeholder="Remarks..." aria-label=".form-control-sm example" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>

                                    </table>
                                </div>

                                <div class="modal-footer">
                                    <asp:Button ID="btnClosePopAssignTask" class="btn btn-danger" runat="server" Text="Close" OnClick="btnClosePopAssignTask_Click" />
                                    <asp:Button ID="btnAssignTask" class="btn btn-success" runat="server" Text="Confirm" OnClick="btnAssignTask_Click" Style="float: right;" />
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>



    <br />

    <div style="width: 2150px; margin: 0 auto;">
        <asp:Label ID="lblTaskLog" runat="server" Text="Operation Log"></asp:Label>
        <asp:GridView ID="gvTasklog"
            runat="server"
            AutoGenerateColumns="False"
            CellPadding="1"
            CellSpacing="1"
            GridLines="None"
            AllowPaging="False"
            PageSize="10"
            CssClass="table-bordered table-sm box-shadow center"
            PagerStyle-CssClass="table pgr"
            AlternatingRowStyle-CssClass="normal11"
            Width="90%">
            <Columns>
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image runat="server" ID="imgVisible2" ImageUrl="~/images/check.png" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESC_LOG" HeaderText="Log Description" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_USER" HeaderText="User ID" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DATE_LOG" HeaderText="Log Date" />
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

    <asp:HiddenField ID="hdIdTask" runat="server" />
    <asp:HiddenField ID="hdOpIdTicket" runat="server" />
    <asp:HiddenField ID="hdOpIdTask" runat="server" />
    <asp:HiddenField ID="hdOpCountTestCase" runat="server" />

    <br />
    <br />
    <br />


    <script>
        function divexpandcollapse(divname) {
            var img = "img" + divname;
            if ($("#" + img).attr("src") == "../images/plus.png") {
                $("#" + img)
                    .closest("tr")
                    .after("<tr><td></td><td colspan = '100%' > " + $("#" + divname).html() + "</td></tr>")
                $("#" + img).attr("src", "../images/close.png");
            } else {
                $("#" + img).closest("tr").next().remove();
                $("#" + img).attr("src", "../images/plus.png");
            }
        }
    </script>
    
</asp:Content>
