<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertUpdateTask.aspx.vb" Inherits="ETS.NewTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>


            <br />

<div style="width: 1500px; margin: 0 auto;">
        <h3>Task > New Task</h3>
       <hr style="background-color: #1e8449; height: 3px;"/>
    </div>
             <div class="box-shadow center" style="width: 65%;">
                    
               <h5 class="form-title"><asp:Label ID="lblCreateUser" runat="server" Text="New Task"></asp:Label></h5>

                <br />
                <table>
                    <tr>
                       <td><label for="inputTypeTask" class="col-sm-3 col-form-label">Task Type<span style="color: red;"> *</span></label></td>
                       <td><asp:DropDownList ID="DropListTaskType" runat="server" DataValueField="DESCRIPTION" AutoPostBack="true" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required" OnSelectedIndexChanged="DropListTaskType_SelectedIndexChanged"></asp:DropDownList></td>
                       <td align="right"><label for="inputCategory" class="col-sm-3 col-form-label">App Category<span style="color: red;"> *</span></label></td>
                       <td><asp:DropDownList ID="DropListCategory" runat="server" DataValueField="SUB_DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required"></asp:DropDownList></td>
                   </tr>

                    <tr>
                        <td><label for="inputDescription" class="col-sm-3 col-form-label">Description<span style="color: red;"> *</span></label></td>
                        <td><asp:TextBox ID="txtDescription" runat="server" class="form-control form-control-sm" placeholder="Description..." aria-label=".form-control-sm example" required="required" TextMode="MultiLine"></asp:TextBox></td>
                        <td align="right"><label for="inputPriority" class="col-sm-3 col-form-label">Priority</label></td>
                        <td>
                            <asp:DropDownList ID="DropListPriority" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Highest</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>


                    <tr>
                        <td class="col-sm-6 col-form-label">Copy 1:</td>
                        <td><asp:DropDownList ID="DropDowncc1" runat="server" DataValueField="USERS" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required"></asp:DropDownList></td>
                        <td align="right">Copy 2:</td>
                        <td><asp:DropDownList ID="DropDownListcc2" runat="server" DataValueField="USERS" class="form-control form-control-sm" aria-label=".form-control-sm example"></asp:DropDownList></td>
                    </tr>

                    <tr style="width: 200%; height: 100px;">
                        <td>Start Date</td>
                        <td>
                            <asp:TextBox ID="txtExpectedStartDate" OnClientDateSelectionChanged="saveDate" AutoPostBack="True"  OnTextChanged="txtExpectedStartDate_TextChanged" runat="server" Columns="10" MaxLength="10" Width="200px" class="form-control form-control-sm" aria-label=".form-control-sm example" onfocus='this.blur();'/>
                            <ajaxToolkit:CalendarExtender runat="server"
                                ID="txtStartDate_CalendarExtender"
                                Format="MM/dd/yyyy"
                                PopupButtonID="ImageButton63"
                                TargetControlID="txtExpectedStartDate"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator77"
                                runat="server"
                                ControlToValidate="txtExpectedStartDate"
                                CssClass="txtObbligatorio"
                                Display="None"
                                SetFocusOnError="True" />
                        </td>

                        <td>
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/_calendar.png" CssClass="imgbutton" Width="30px" runat="server" />
                            <span style="float: right;">End Date</span><br />
                            <asp:Label Text="" runat="server" ID="dateError" CssClass="errlabel" Style="color: red;" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtExpectedEndDate" OnClientDateSelectionChanged="saveDate" AutoPostBack="True" OnTextChanged="txtExpectedEndDate_TextChanged" runat="server" Width="200px" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" onfocus='this.blur();'/>
                            <ajaxToolkit:CalendarExtender runat="server"
                                ID="txtEndDate_CalendarExtender"
                                Format="MM/dd/yyyy"
                                PopupButtonID="ImageButton65"
                                TargetControlID="txtExpectedEndDate"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1"
                                runat="server"
                                ControlToValidate="txtExpectedEndDate"
                                CssClass="txtObbligatorio"
                                Display="None"
                                SetFocusOnError="True" />
                        </td>

                        <td><asp:ImageButton ID="ImageButton2" ImageUrl="~/images/_calendar.png" Width="30px" CssClass="imgbutton2" runat="server" /></td>
                    </tr>


                </table>

                <asp:Button ID="btnCreateTask" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnCreateTask_Click1" /><br />
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label><br />
                <br />
                <asp:Label ID="lblNewRequestNote" runat="server" Text="*Kindly attach requirements Docs & Workflow Schema for New Requests." Visible="false"></asp:Label></i>
       

            </div>

 



            <asp:HiddenField ID="hdIdTicket" runat="server" />
            <asp:HiddenField ID="hdIdTask" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
