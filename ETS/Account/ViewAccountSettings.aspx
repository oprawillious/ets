<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewAccountSettings.aspx.vb" Inherits="ETS.ViewAccountSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    
<div style="margin: 0 auto; width: 700px;">
       <h3>Account Settings > View Account Settings</h3><hr style="background-color: #1e8449; height: 3px;"/><br />
       <h3><asp:label ID="lblChangePassword" runat="server" Text="Account Settings"></asp:label></h3><br />

    <table>
        <tr>
            <td><label for="inputFullName" class="col-sm-3 col-form-label">Name:</label></td>
            <td><asp:Label ID="lblFullName" runat="server" Text=""></asp:Label></td>         
        </tr>
        
        <tr>
            <td><label for="inputUsername" class="col-sm-3 col-form-label">Username:</label></td>
            <td><asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></td>
        </tr>

        <tr>
            <td><label for="inputEmail" class="col-sm-3 col-form-label">E-Mail:</label></td>
            <td><asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></td>
        </tr>

        <tr>
            <td><label for="inputRole" class="col-sm-3 col-form-label">User Group:</label></td>
            <td><asp:Label ID="lblRole" runat="server" Text=""></asp:Label></td>
        </tr>

        <tr>
            <td><label for="inputPassword" class="col-sm-3 col-form-label">Password:</label></td>
            <td><asp:Label ID="lblPassword" runat="server" Text="********"></asp:Label></td>
        </tr>
        
    </table>
    
    <br/>
    
   <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-success" OnClick="btnChangePassword_Click"/><br/><br/>
   <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
   <hr/>

 </div>
    
</asp:Content>

