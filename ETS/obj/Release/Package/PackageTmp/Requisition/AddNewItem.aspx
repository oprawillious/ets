<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="AddNewItem.aspx.vb" Inherits="ETS.AddNewItem1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<br />

	<div class="Alignments1">
		<h3>Requisition > Register Item</h3>
		<br />
		<br />

		<div style="text-align: center; font-size: 30px;">
			<h3>
				<asp:Label ID="lblPageCaption" runat="server" Text="Register New Item"></asp:Label></h3>
		</div>

		<br />

		<table>
			<tr >
				<td><label for="inputPassword" class="col-sm-2 col-form-label">Category:<span style="color: red;"> *</span></label></td>
				<td><asp:DropDownList ID="DropListPeripheralDescription" OnSelectedIndexChanged="DropListComputerInfo_SelectedIndexChanged" runat="server" DataValueField="DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required" AutoPostBack="true"></asp:DropDownList></td>
				<td><label for="searchDept" class="col-sm-2 col-form-label">Item: </label></td>
				<td style="width: 400px;"><asp:TextBox ID="txtDescription" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
			</tr>

			<tr id="trmanufacturer_model" runat="server">
					<td><label for="manufacturer" class="col-sm-2 col-form-label">Manufacturer: </label></td>
					<td style="width: 400px;"><asp:TextBox ID="txtmanufacturer" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
					<td><label for="model" class="col-sm-2 col-form-label">Model: </label></td>
					<td style="width: 400px;"><asp:TextBox ID="txtmodel" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
			</tr>

				<tr id="trmodelno_serialno" runat="server">
					<td><label for="modelno" class="col-sm-2 col-form-label">Model No: </label></td>
					<td style="width: 400px;"><asp:TextBox ID="txtModelNo" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
					<td><label for="serialNo" class="col-sm-2 col-form-label">Serial No: </label></td>
					<td style="width: 400px;"><asp:TextBox ID="txtSerialNo" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
				</tr>

			<tr id="trprocessor_screensize" runat="server">
				<td><label for="processor" class="col-sm-2 col-form-label">Processor: </label>	</td>
				<td style="width: 400px;"><asp:TextBox ID="txtProcessor" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
				<td><label for="screensize" class="col-sm-2 col-form-label">Screen Size: </label></td>
				<td style="width: 400px;"><asp:TextBox ID="txtScreenSize" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
			</tr>

			<tr id="trRam_storage" runat="server">
				<td><label for="ram" class="col-sm-2 col-form-label">RAM: </label></td>
				<td style="width: 400px;"><asp:TextBox ID="txtRam" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
				<td><label for="storage" class="col-sm-2 col-form-label">Storage: </label></td>
				<td style="width: 400px;"><asp:TextBox ID="txtStorage" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
			</tr>

		</table>
		<br />
		<asp:Label ID="msg1" runat="server" Text=""></asp:Label>
		<br />
		<asp:Button ID="btnSaveStock" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnSave_Stock_Click" /><br />
		<br />
		<br />
		<hr />
		<asp:Label Text="" runat="server" ID="lblMessage" />
	</div>
	<asp:HiddenField ID="hdItemsId" runat="server" />
</asp:Content>
