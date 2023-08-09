<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewMenuTop.aspx.vb" Inherits="ETS.ViewMenuTop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
    <br />    

    <div class="Alignments1">
        <h3>Settings > View Menu Top</h3><hr  class="greenLine"/><br />
        
       <div class="box-shadow center" style="width: 80%;">
        <h5 class="form-title">View Menu Top</h5>

            <table style="border-collapse: collapse;">
                <tr>
                    <td><label for="inputPassword" class="col-sm-3 col-form-label">Menu Top:</label></td>
                    <td><asp:TextBox ID="txtMenuTop" runat="server" class="form-control form-control-sm" placeholder="menu name..." style="width: 150px;"></asp:TextBox></td>
                    <td><asp:Button ID="btnSearchMenuTop" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchMenuTop_Click"/></td>
                    <td><asp:LinkButton ID="LinkbtnAssignMenu" runat="server" style="color: green; font-weight: bold;" OnClick="LinkbtnAssignMenu_Click">View Menu to Role</asp:LinkButton>               </td>
                    <td><asp:LinkButton ID="LinkbtnSubMenu" runat="server" style="color: green; font-weight: bold;" OnClick="LinkbtnSubMenu_Click">View Sub Menu to Role</asp:LinkButton></td>
                </tr>
            </table>

            </div>
         <br /> 
        <asp:GridView ID="gvMenuTop" 
             runat="server"
             AutoGenerateColumns="False"
             CellPadding="1"
             CellSpacing="1"
             GridLines="None"
             AllowPaging="false"
             CssClass="table table-sm table-striped box-shadow center"
             PagerStyle-CssClass="table pgr"     
             AlternatingRowStyle-CssClass="normal11" 
             Width="98%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_MENU_TOP" HeaderText="Code"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DESCRIPTION" HeaderText="Menu Top"/>
                               
            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>
        
        <EmptyDataTemplate>
                <div style="color: red;">Data Not found !</div>
        </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
