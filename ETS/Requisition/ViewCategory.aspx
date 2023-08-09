<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewCategory.aspx.vb" Inherits="ETS.ViewCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	  <br/>
    <div  class="Alignments1">
        <h3>REQUISITION > CATEGORY> VIEW CATEGORY</h3>
        <hr class="greenLine" />
        <br />
    <h3>
			<asp:Label ID="CAT" runat="server" Text="view Category" CssClass="heading"></asp:Label></h3>
		<br />

        <table>
            <tr>
                <td><label for="searcatt" class="col-sm-2 col-form-label">Search: </label></td>
                <td style="width:300px"><asp:TextBox ID="txtSearchCategory"  placeholder="Category..." runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
                <td><asp:Button ID="btnsearch" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btn_SearchCategory_Click" /></td>
                <td> <asp:Button ID="createCategory" runat="server" Text="Add Category" class="btn btn-primary btn-sm mb-3" OnClick="btn_CreateCategory_Click" /></td>
            </tr>
        </table>


        <asp:GridView ID="gvViewCategory"
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
                        <asp:HiddenField ID="hdItemCategory" runat="server" Value='<%# Eval("ID_ITEM_CATEGORY")%>' />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdDescription" runat="server" Value='<%# Eval("DESCRIPTION")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_ITEM_CATEGORY" HeaderText="Item code" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Description" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" ItemStyle-Width="1%" ControlStyle-CssClass="button" CommandName="Modifica" Text="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" ItemStyle-Width="1%" ControlStyle-CssClass="button" CommandName="Delete" Text="Delete" OnClientClick="return ConfirmDelete();" />
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Category !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
	<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
