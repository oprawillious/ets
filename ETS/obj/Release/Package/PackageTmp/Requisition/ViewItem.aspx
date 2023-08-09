<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewItem.aspx.vb" Inherits="ETS.ViewStocks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


	<div class="Alignments2">
		<h3>Stocks> View Items</h3>
		<hr class="greenLine" />
		<br />
		<table>
			<tr>
			<%--	<td id="idcategory" runat="server" align="right">
					<label for="inputTaskNumber" class="col-sm-2 col-form-label">Category ID: </label>
				</td>
				<td id="idcat" runat="server" align="left">
					<asp:TextBox ID="txtCategory" runat="server" class="form-control form-control-sm" placeholder="Enter Id..." Style="width: 250px;"></asp:TextBox></td>--%>

				<td id="desc" runat="server" align="right"><label for="inputDesc" class="col-sm-2 col-form-label">Description: </label></td>
				<td id="descs" runat="server" align="left"><asp:TextBox ID="txtdescription" runat="server" class="form-control form-control-sm" placeholder="Enter Description..." Style="width: 250px;"></asp:TextBox></td>
				<td align="right"><label for="RequestDate" class="col-sm-2 col-form-label">Date Purchased: </label></td>
				<td style="width: 200px" align="left">
					<asp:TextBox ID="txtRequestDate" runat="server" Columns="10" MaxLength="10" class="form-control form-control-sm" aria-label=".form-control-sm example" onfocus='this.blur();' />
					<ajaxToolkit:CalendarExtender runat="server"
						ID="txtDateReOpened0_CalendarExtender"
						Format="MM/dd/yyyy"
						PopupButtonID="ImageButton63"
						TargetControlID="txtRequestDate"></ajaxToolkit:CalendarExtender>
					<asp:RequiredFieldValidator
						ID="RequiredFieldValidator77"
						runat="server"
						ControlToValidate="txtRequestDate"
						CssClass="txtObbligatorio"
						Display="None"
						SetFocusOnError="True" /></td>
				<td><asp:ImageButton ID="ImageButton63" ImageUrl="~/images/_calendar.png" Width="30px" runat="server" /></td>
				<td><asp:Button ID="btnSearchItem" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchItem_Click" /></td>
				<td><asp:Button ID="addNewItem" runat="server" Text="Register New Item" class="btn btn-success btn-sm mb-3" OnClick="btnAddItem_Click" /></td>
			</tr>
		</table>


		<asp:GridView ID="gvViewStocks"
			runat="server"
			AutoGenerateColumns="False"
			CellPadding="1"
			CellSpacing="1"
			GridLines="None"
			AllowPaging="True"
			PageSize="10"
			CssClass="table-bordered table-sm"
			PagerStyle-CssClass="table pgr"
			AlternatingRowStyle-CssClass="normal11"
			Width="98%">

			<Columns>

				<asp:TemplateField>
					<ItemTemplate>
						<asp:HiddenField ID="hdItems" runat="server" Value='<%# Eval("ID_ITEMS")%>' />
						<asp:HiddenField ID="hdquantityRemaining" runat="server" Value='<%# Eval("QUANTITY_REMAINING")%>' />
						<asp:HiddenField ID="hditemcategory" runat="server" Value='<%# Eval("ID_ITEM_CATEGORY")%>' />
					</ItemTemplate>
				</asp:TemplateField>


				<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_ITEMS" HeaderText="item code" />
				<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="description" />
				<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="SCREEN_SIZE" HeaderText="Screen Size" />
				<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="MANUFACTURER" HeaderText="manufacturer" />
				<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="PROCESSOR" HeaderText="processor" />
				<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="SERIAL_NO" HeaderText="serial no" />
				<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="QUANTITY_REMAINING" HeaderText="Quantity Remaining" />
				<asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="LAST_DATE_PURCHASED" HeaderText="last date purchased" />

		
				<asp:TemplateField>
                    <ItemTemplate>
                       <asp:Button ID="btnDelete" runat="server" ItemStyle-Width="5%" ControlStyle-CssClass="button" CommandName="Delete" Text="Delete" style="background-color: red;" />
                    </ItemTemplate>
                </asp:TemplateField>
			</Columns>
			<SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

			<EmptyDataTemplate>
				<div style="color: red; text-align: center;">No Item Purchased !</div>
			</EmptyDataTemplate>

			<PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
		</asp:GridView>

	</div>
</asp:Content>
