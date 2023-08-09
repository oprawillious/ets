<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertDeveloper.aspx.vb" Inherits="ETS.InsertDeveloper" %>
<%@ Register Assembly="DevExpress.Web.v23.1, Version=23.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
       
.mGrid {
    color: #000;
}

.mGrid a {
    background-color: #ccc;
    padding: 5px 7px;
    text-decoration: none;
    border: 0px solid #fff;
}

.mGrid a:hover {
    background-color: #ccc;
    color: #787878;
    border: 0px solid #000;
}

.mGrid td {
    border: 1px solid #000;
    text-align: left;
}

.mGrid th {
    background-color: #1B8257;
    color: #fff;
    border: 1px solid #fff;
}
    </style>
    	1
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">



        <ContentTemplate>

            <div class="Alignments2">

                <div class="box-shadow center" style="width: 90%;">

                    <h3>
                        <asp:Label ID="lblDetailTask" runat="server" Text=""></asp:Label></h3>
                    <br />

                    <table class="">

                        <tr>
                            <td>
                                <label for="inputDescription" class="col-sm-2 col-form-label">Description: </label>
                            </td>
                            <td>
                                <asp:Label ID="lblDescription" runat="server" Text="" class="form-control form-control-sm"></asp:Label>
                            </td>
                            <td>
                                <label for="inputAssignedTo" class="col-sm-2 col-form-label">App Category: </label>
                            </td>
                            <td>
                                <asp:Label ID="lblCategory" runat="server" Text="" class="form-control form-control-sm"></asp:Label>
                            </td>

                        </tr>

                        <tr>
                        </tr>

                        <tr>
                            <td>
                                <label for="inputAssignedTo" class="col-sm-2 col-form-label">Created By: </label>
                            </td>
                            <td>
                                <asp:Label ID="lblAssignedTo" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>

                            <td>
                                <label for="inputStatus" class="col-sm-2 col-form-label">Status: </label>
                            </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
                        </tr>

                        <tr>
                        </tr>

                        <tr>
                            <td>
                                <label for="inputStartDate" class="col-sm-2 col-form-label">Start Date: </label>
                            </td>
                            <td>
                                <asp:Label ID="lblStartDate" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>

                            <td>
                                <label for="inputEndDate" class="col-sm-2 col-form-label">End Date: </label>
                            </td>
                            <td>
                                <asp:Label ID="lblEndDate" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>
                        </tr>

                        <tr>
                        </tr>

                        <%--    <tr>
                <td>
                    <label for="inputRemarks" class="col-sm-2 col-form-label">Remarks: </label>
                </td>
                <td>
                    <asp:Label ID="lblRemarks" runat="server" Text="" class="form-control form-control-sm"></asp:Label></td>


                <td></td>
                <td></td>
            </tr>--%>
                    </table>

                    <br />

                    <asp:Label ID="lblMessage" runat="server" Text="" Style="color: green;"></asp:Label>
                </div>
                <br /><br />
                <h3>Add Developer(s)</h3>
        <%--        <dx:ASPxGridView ID="gvRequestCompanyAgent"
                    runat="server"
                    AutoGenerateColumns="False"
                    Width="90%"
                    KeyFieldName="ID_USERS"
                    baseColor="Green" 
                   
                     OnBeforeGetCallbackResult="gvRequestCompanyAgent_BeforeGetCallbackResult"
                    CssClass="box-shadow center"
                    Font-Size="Large">

                    <SettingsResizing ColumnResizeMode="Control" Visualization="Postponed" />
                    <Settings HorizontalScrollBarMode="Visible" />

                    <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
                    <SettingsBehavior AllowFocusedRow="true"
                        AllowSort="true"
                        SortMode="Custom"
                        AllowDragDrop="true" />
                    <SettingsPager NumericButtonCount="10" />

                    <Settings
                        HorizontalScrollBarMode="auto"
                        VerticalScrollBarMode="auto"
                        VerticalScrollableHeight="600" ShowGroupPanel="true" />

                    <SettingsPager Position="Bottom" PageSize="50">
                        <PageSizeItemSettings Items="10,20,50,100,150,200" Visible="true" ShowAllItem="true" />
                    </SettingsPager>
                    <SettingsBehavior AllowSelectByRowClick="true" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ID_USERS">
                            <DataItemTemplate>
                               <dx:ASPxCheckBox ID="cb" runat="server" Checked='<%#Eval("ID_USERS")%>' OnInit="cb_Init" />
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                       
                        <dx:GridViewDataTextColumn FieldName="" Caption="" Visible="false">
                            <DataItemTemplate>
                                <dx:ASPxHiddenField ID="hdIdDv" runat="server" ClientInstanceName="hf" value='<%# Eval("ID_USERS")%>' />
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn FieldName="USERS" Caption="Users" Width="100%" />
                       
                    </Columns>


                </dx:ASPxGridView>--%>

                <asp:GridView ID="gvRequestCompanyAgent"
                    runat="server"
                    AutoGenerateColumns="false"
                    CellPadding="1"
                    CellSpacing="1"
                    GridLines="None"
                    AllowPaging="false"
                    CssClass="table-bordered table-sm box-shadow center"
                    PagerStyle-CssClass="table pgr"
                    AlternatingRowStyle-CssClass="normal11"
                    OnRowCommand="gvRequestCompanyAgent_OnRowCommand" Width="90%">

                    <Columns>
                        <asp:ButtonField ButtonType="Image" ItemStyle-Width="4" CommandName="Select" Text="Select" ImageUrl="~/img/checkDawn.png" />

                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdIdDv" Visible="false" runat="server" Value='<%# Eval("ID_USERS")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="USERS" HeaderText="Users" />

                    </Columns>

                    <EmptyDataTemplate>
                        <div align="center">Data Not found.</div>
                    </EmptyDataTemplate>

                    <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

                </asp:GridView>
                <br />
        <asp:Button ID="Button1" runat="server" Text="Add Developer(s)" CssClass="btn btn-success" OnClick="btnAddDeveloper_Click" /><br />

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
                                            <td>
                                                <label for="Remarks" class="col-sm-3 col-form-label">Remarks</label></td>
                                            <td>
                                                <asp:TextBox ID="txtDevRemarks" runat="server" class="form-control form-control-sm" placeholder="Remarks..." aria-label=".form-control-sm example" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="modal-footer">
                                    <br />
                                    <asp:Label ID="lblAssign" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>




        </ContentTemplate>
    </asp:UpdatePanel>



    <br />
    <br />
    <br />

    <br />

    <script>

        function createNewInputFields() {
            var container = document.getElementById('new-input-container');

            const newElem = document.createElement("input");
            newElem.setAttribute("type", "text");
            container.appendChild(newElem);
        }
        </script>


    <asp:HiddenField ID="hdIdTask" runat="server" />
    <asp:HiddenField ID="hdOpIdTicket" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hdIdC" runat="server" />



    <asp:HiddenField ID="hdOpIdTask" runat="server" />

</asp:Content>

