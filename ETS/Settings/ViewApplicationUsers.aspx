<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewApplicationUsers.aspx.vb" Inherits="ETS.ViewApplicationUsers" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
     
    <br />

    <div class="Alignments2">
       
        <h3>Settings > View Application Users</h3><hr class="greenLine"/><br />
</div>
       <div class="box-shadow center" style="width: 80%;">
        <h5 class="form-title">Users</h5>
        
        <table>
            <tr>
                <td>Enter Username:</td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server" class="form-control form-control-sm" placeholder="Username..."></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearchUser" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btnSearchUser_Click" /></td>
                <td>
                    <asp:Button ID="btnNewUser" runat="server" Text="Create New" class="btn btn-success btn-sm mb-3" OnClick="btnNewUser_Click" /></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
        <asp:GridView ID="gvUsers"
             runat="server"
             AutoGenerateColumns="False"
             CellPadding="1"
             CellSpacing="1"
             GridLines="None"
             AllowPaging="true"         
             PagerStyle-CssClass="table pgr"    
             AlternatingRowStyle-CssClass="normal11"
             Width="90%" CssClass="box-shadow center">

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdUserId" runat="server" Value='<%# Eval("ID_USERS")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="FIRST_NAME" HeaderText="First Name"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="LAST_NAME" HeaderText="Last Name"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="USERNAME" HeaderText="Username"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EMAIL_ADDRESS" HeaderText="Email Address"/>
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="USER_ROLE" HeaderText="Role"/>
               
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:Button ID="btnEditUser" runat="server" Text="Edit" CommandName="Modifica" class="button primary"/> 
                      </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:Button ID="btnDeleteUser" runat="server" Text="Delete" CommandName="Remove" class="button danger" /> 
                      </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red;">User Not found !</div>
            </EmptyDataTemplate>
             <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>

    
     
</asp:Content>
