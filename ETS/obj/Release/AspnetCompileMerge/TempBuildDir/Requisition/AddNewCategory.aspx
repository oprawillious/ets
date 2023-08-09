<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="AddNewCategory.aspx.vb" Inherits="ETS.AddNewItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<br />
	<div class="Alignments">
		<br />
		<h3>Requsition > Add Item</h3>
		<hr class="greenLine"/>
		<br />
		<h3>
			<asp:Label ID="lblCreateUser" runat="server" Text="Add Item" CssClass="heading"></asp:Label></h3>
		<br />
		<table>
			<tr>
				<td><label for="searchDept" class="col-sm-2 col-form-label">Item Name: </label></td>
				<td style="width: 400px;"><asp:TextBox ID="txtAddNewItem" required="required" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>	
			</tr>
		</table>
		<asp:Button ID="AddItem" runat="server" Text="Confirm" class="btn btn-primary btn-sm mb-3" CssClass="mybutton" OnClick="btn_AddNewItem_Click" />
		<br />
		<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
	</div>

	<asp:HiddenField ID="hdAddItem" runat="server" />
</asp:Content>
