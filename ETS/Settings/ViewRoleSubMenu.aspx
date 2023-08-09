<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewRoleSubMenu.aspx.vb" Inherits="ETS.ViewRoleSubMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />   
    <div class="Alignments1">
        <h3>Settings > View Menu Top > View Role Submenu</h3><hr class="greenLine"/><br />
        <h2>View Role SubMenu</h2><br/>   
        
    <div style="width: 1200px;">       
            <table>
                <tr>
                    <td><label for="Role" class="col-sm-3 col-form-label">Role<span style="color: red;">*</span></label></td>
                    <td><asp:DropDownList ID="DropListUserGroup" runat="server" DataValueField="ROLE_DESCRIPTION" class="form-control form-control-sm" required="required" style="width: 150px;"></asp:DropDownList></td>
                    <td><label for="Menu" class="col-sm-3 col-form-label">Menu<span style="color: red;">*</span></label></td>
                    <td><asp:DropDownList ID="DropListMenu" runat="server" DataValueField="MENU"  CausesValidation="true" class="form-control form-control-sm" required="required" OnSelectedIndexChanged="DropListMenu_SelectedIndexChanged" style="width: 150px;"></asp:DropDownList></td>
                    <td><label for="SubMenu" class="col-sm-3 col-form-label">SubMenu<span style="color: red;">*</span></label></td>
                    <td><asp:DropDownList ID="DropListSubMenu" runat="server" DataValueField="DESCRIPTION"  CausesValidation="true" class="form-control form-control-sm" required="required" style="width: 150px;"></asp:DropDownList></td>
                    <td><asp:Button ID="btnSearchRoleSubMenu" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchRoleSubMenu_Click"/></td>
                    <td><img src="../../images/plus.png"/><asp:LinkButton ID="LinkbtnAssignSubMenu" runat="server" style="color: green; font-weight: bold;" OnClick="LinkbtnAssignSubMenu_Click">Assign SubMenu</asp:LinkButton></td>
                </tr>
            </table>
    </div>

        <asp:GridView ID="gvRoleSubMenu" 
             runat="server"
             AutoGenerateColumns="False"
             CellPadding="1"
             CellSpacing="1"
             GridLines="None"
             AllowPaging="false"
             CssClass="table table-sm table-striped"
             PagerStyle-CssClass="table pgr"     
             AlternatingRowStyle-CssClass="normal11" 
             Width="98%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdIdRoleSubMenu" runat="server" Value='<%# Eval("ID_ROLES_SUB_MENU_TOP")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_ROLES_SUB_MENU_TOP" HeaderText="Code"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="MENU" HeaderText="Menu"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="SUB_MENU" HeaderText="SubMenu"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ROLE_DESCRIPTION" HeaderText="Role"/>                                
            
                 <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Remove" class="button primary" OnClientClick="return ConfirmDelete();"/> 
                      </ItemTemplate>
                 </asp:TemplateField>

            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>  
</asp:Content>
