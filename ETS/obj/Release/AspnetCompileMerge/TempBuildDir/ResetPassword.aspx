<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ResetPassword.aspx.vb" Inherits="ETS.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        @font-face {
            font-family: 'AmazonEmber_Bd';
            src: url('../../../fonts/AmazonEmber_Bd.ttf') format('truetype');
        }
        @font-face {
            font-family: 'AmazonEmber_Lt';
            src: url('../../../fonts/AmazonEmber_Lt.ttf') format('truetype');
        }

        body {           
            font-family: 'AmazonEmber_Bd';
            color: #444444;
        }
        h1, h2, h3, h4, h5, h6 {
            font-family: 'AmazonEmber_Bd';
            color: #444444;
          }
    </style>

    <br />
    
<div style="margin: 0 auto; width: 700px;">
       <h3>Account Settings > Reset Password</h3><hr style="background-color: #1e8449; height: 3px;" /><br />
       <h3><asp:label ID="lblResetPassword" runat="server" Text="Reset Password"></asp:label></h3><br />

    <table>

        <tr>
            <td><label for="EmailAddress" class="col-sm-3 col-form-label">Email Address:<span style="color: red;"> *</span></label></td>
            <td><asp:TextBox ID="txtEmail" runat="server" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required"></asp:TextBox></td>
        </tr>  
        
    </table>
    
    <br/>
    
   <asp:Button ID="btnResetPassword" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnResetPassword_Click"/><br/><br/>
   <asp:Label ID="lblMessage" runat="server" Text="" style="color: red;"></asp:Label>
   <hr/>

 </div>
</asp:Content>
