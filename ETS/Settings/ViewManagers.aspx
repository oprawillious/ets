<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewManagers.aspx.vb" Inherits="ETS.ViewManagers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br/>
    <div class="Alignments11">
        <h3>Managers > View Managers</h3>
        <hr class="greenLine"/>
        <br />
        <h2>Managers </h2>

        <table>
            <tr>
                <td><label for="searchDept" class="col-sm-2 col-form-label">Search: </label></td>
                <td style="width: 300px"><asp:TextBox ID="txtSearchManager" placeholder="manager's fullname ..." runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" /></td>
                <td><asp:Button ID="btnsearch" runat="server" Text="Search" class="btn btn-primary btn-sm mb-3" OnClick="btn_SearchClick" /></td>
                <td><asp:Button ID="btnCreateManager" runat="server" Text="Add Manager" class="btn btn-primary btn-sm mb-3" OnClick="btn_CreateManagerClick" /></td>
            </tr>
        </table>


        <asp:GridView ID="gvViewManagers"
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
                        <asp:HiddenField ID="hdIdManagerSettings" runat="server" Value='<%# Eval("ID_MANAGER_SETTING")%>' />
                        <asp:HiddenField ID="hdDescription" runat="server" Value='<%# Eval("DEPT")%>' />
                        <asp:HiddenField ID="hdflaghr" runat="server" Value='<%# Eval("FLAG_HR")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="ID_MANAGER_SETTING" HeaderText="Dept-Code" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="DEPT" HeaderText="Department" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="MANAGER_FULLNAME" HeaderText="fullname" />
                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="EMAIL" HeaderText="Mail" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" ItemStyle-Width="1%" ControlStyle-CssClass="button" CommandName="Modifica" Text="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" ItemStyle-Width="1%" ControlStyle-CssClass="button" CommandName="Delete" Text="Delete"/>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff" />

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Managers !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>

    </div>
</asp:Content>
