<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="NewPurchase.aspx.vb" Inherits="ETS.StockIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 40px;"></div>
    <div  class="Alignments1">
        <h3>REQUISITION > NEW PURCHASE</h3>
        <hr class="greenLine" />
        <br />

        <br />


        <div style="text-align: center; font-size: 30px;">
            <h3>
                <asp:Label ID="lblPageCaption" runat="server" CssClass="stockcss" Text="New Purchase"></asp:Label></h3>
        </div>

        <br />

        <table>

            <tr>
                <td><label for="inputPassword" class="col-sm-2 col-form-label">Category:<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListPeripheralDescription" runat="server"  OnSelectedIndexChanged="DropListItemDesc_SelectedIndexChanged" DataValueField="DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required" AutoPostBack="true"></asp:DropDownList></td>
            </tr>

            <tr>
                <td><label for="inputPassword" class="col-sm-2 col-form-label">Description<span style="color: red;"> *</span></label></td>
                <td>
                    <asp:DropDownList ID="DropListCatdesc" runat="server" DataValueField="DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 282px" class="col-sm-6 col-form-label">Date Purchased</td>

                <td style="width: 600px; padding-bottom: 0;">
                    <asp:TextBox ID="txtDatePurchased" OnClientDateSelectionChanged="saveDate" runat="server" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" onfocus='this.blur();' required="required" />
                    <ajaxToolkit:CalendarExtender runat="server"
                        ID="txtStartDate_CalendarExtender"
                        Format="MM/dd/yyyy"
                        PopupButtonID="ImageButton63"
                        TargetControlID="txtDatePurchased"></ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator77"
                        runat="server"
                        ControlToValidate="txtDatePurchased"
                        CssClass="txtObbligatorio"
                        Display="None"
                        SetFocusOnError="True" />
                </td>

                <td><asp:ImageButton ID="ImageButton63" ImageUrl="~/images/_calendar.png" Width="30px" runat="server" /> </td>
                    

            </tr>
            <tr>
                <td><asp:Label ID="Quantity" runat="server" class="col-sm-2 col-form-label" for="inputQuantity"> Quantity Purchased</asp:Label>  </td>
                <td> <asp:TextBox ID="txtQuantity" runat="server" aria-label=".form-control-sm example" class="form-control form-control-sm" placeholder="Quantity" required="required"></asp:TextBox></td>
            </tr>


        </table>
        <br />
        <asp:Label ID="msg1" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnAddStock" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnAddStock_Click" /><br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <br />
        <hr />

        <asp:HiddenField ID="hdIDStock" runat="server" />


    </div>

</asp:Content>
