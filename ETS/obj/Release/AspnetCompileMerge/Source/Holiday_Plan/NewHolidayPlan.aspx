<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="NewHolidayPlan.aspx.vb" Inherits="ETS.NewHolidayPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <div class="Alignments">

                <h3>Holiday Plan > New Holiday</h3>
                <hr class="greenLine" />
                <br />

                <div class="center-Alingment">
                 <h3><asp:Label ID="lblPageCaption" runat="server" Text="New Holiday Plan"></asp:Label></h3>
                </div>

                <br />
                <table>
                    <tr>
                        <td><asp:Label ID="lblCaptionRemainingDays" runat="server" Text="Remaining Leave Days: "></asp:Label></td>
                        <td><asp:Label ID="lblRemainingDays" runat="server" Text=""></asp:Label></td>
                    </tr>

                    <tr>
                        <td><asp:Label ID="lblCaptionUsedDays" runat="server" Text="Used Leave Days: "></asp:Label></td>
                        <td><asp:Label ID="lblUsedDays" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td><asp:Label ID="lblType" runat="server" >Leave Type<span style="color: red;">*</span></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="DropDownListHolidayType" runat="server" required="required">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Sick Leave</asp:ListItem>
                                <asp:ListItem>Annual/Vacation Leave</asp:ListItem>
                                <asp:ListItem>Paternity Leave</asp:ListItem>
                                <asp:ListItem>Maternity Leave</asp:ListItem>
                                <asp:ListItem>AccidentOnDuty Leave</asp:ListItem>
                                <asp:ListItem>Examination Leave</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label Text="Start Date" runat="server" /></td> 
                        <td><asp:TextBox ID="txtStartDate" OnClientDateSelectionChanged="saveDate" runat="server"  onfocus='this.blur();' required="required" />
                            <ajaxToolkit:CalendarExtender runat="server"
                                ID="txtStartDate_CalendarExtender"
                                Format="MM/dd/yyyy"
                                PopupButtonID="ImageButton63"
                                TargetControlID="txtStartDate"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator77"
                                runat="server"
                                ControlToValidate="txtStartDate"
                                CssClass="txtObbligatorio"
                                Display="None"
                                SetFocusOnError="True" />
                        </td>

                        <td><asp:ImageButton ID="ImageButton63" ImageUrl="~/images/_calendar.png"  runat="server" /></td>
                    </tr>

                    <tr>
                        <td>End Date</td>
                        <td><asp:TextBox ID="txtEndDate" AutoPostBack="true" runat="server" Columns="10" OnTextChanged="txtEndDate_TextChanged" MaxLength="10" required="required" />
                            <ajaxToolkit:CalendarExtender runat="server"
                                ID="txtEndDate_CalendarExtender"
                                Format="MM/dd/yyyy"
                                PopupButtonID="ImageButton65"
                                TargetControlID="txtEndDate"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1"
                                runat="server"
                                ControlToValidate="txtEndDate"
                                CssClass="txtObbligatorio"
                                Display="None"
                                SetFocusOnError="True" />
                        </td>

                        <td><asp:ImageButton ID="ImageButton65" ImageUrl="~/images/_calendar.png"  runat="server" /></td>
                    </tr>

                    <tr>
                        <td >No Of Days:</td>
                        <td><asp:TextBox ID="txtDays" Text="" runat="server" CssClass="time-textbox" ReadOnly="true"  required="required" /></td>
                   </tr>
                            
                   <tr>
                         <td><asp:Label ID="lblRemarks" runat="server"  for="inputRemarks" Text="Remarks" /></td>
                         <td><asp:TextBox ID="txtRemarks" runat="server"  placeholder="Reason for Leave.." required="required" TextMode="MultiLine" /></td>
                 </tr>

                </table>

                <br />

                <asp:Button ID="btnHolidayRequest" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnRequestHoliday_Click" /><br />
                <br />
                <asp:Label ID="lblMessage" runat="server" Text="" Style="color: red;"></asp:Label><br />

            </div>
            <asp:HiddenField ID="hDIdHoliday_plan" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
