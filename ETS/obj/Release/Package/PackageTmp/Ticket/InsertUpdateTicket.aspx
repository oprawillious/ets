<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="InsertUpdateTicket.aspx.vb" Inherits="ETS.InsertUpdateTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCreateTicket" />
        </Triggers>

        <ContentTemplate>

            <div class="Alignments">
                <h3>Ticket > New Ticket</h3>
                <hr class="greenLine" />
                <br />
                <h3>
                    <asp:Label ID="lblCreateUser" runat="server" Text="New Ticket"></asp:Label></h3>
                <br />

                <table>
                    <tr>
                        <td><label for="inputPassword" class="col-sm-3 col-form-label">Request Type<span style="color: red;"> *</span></label></td>
                        <td><asp:DropDownList ID="DropListTicketType" runat="server" DataValueField="DESCRIPTION" AutoPostBack="true" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required" OnSelectedIndexChanged="DropListTicketType_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>

                    <tr>
                        <td><label for="inputSubType" class="col-sm-3 col-form-label">Category<span style="color: red;"> *</span></label></td>
                        <td><asp:DropDownList ID="DropListSubType" runat="server" DataValueField="SUB_DESCRIPTION" class="form-control form-control-sm" aria-label=".form-control-sm example" required="required"></asp:DropDownList></td>
                    </tr>

                    <tr>
                        <td> <label for="inputDescription" class="col-sm-3 col-form-label">Description<span style="color: red;"> *</span></label></td>
                        <td><asp:TextBox ID="txtDescription" runat="server" class="form-control form-control-sm" placeholder="description..." aria-label=".form-control-sm example" required="required" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>

                    <tr>
                       
                       <td><label for="inputDescription" class="col-sm-3 col-form-label">Priority<span style="color: red;"> *</span></label></td>
                        <td>
                            <asp:DropDownList ID="DropListPriority" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Highest</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>

                    <tr>
                        <td><label for="inputIntercomms" class="col-sm-3 col-form-label">Intercomms/Phone No</label></td>
                        <td><asp:TextBox ID="txtIntercomms" runat="server" class="form-control form-control-sm" placeholder="intercomms..." aria-label=".form-control-sm example" required="required"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td><label for="inputAttachment" class="col-sm-3 col-form-label">Add Attachment</label></td>
                        <td><asp:FileUpload ID="TicketFileUpload" runat="server" class="form-control form-control-sm" placeholder="Upload File..." aria-label=".form-control-sm example" AllowMultiple="true" /></td>
                    </tr>

                </table>

                <br />
                <asp:Button ID="btnCreateTicket" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnCreateTicket_Click" /><br />
                <br />
                <asp:Label ID="lblMessage" runat="server" Text="" class="ErrMsg"></asp:Label><br />
                <br />
                <h4>* Supported file types - PNG or jpg</h4>
                <hr />

            </div>
            <asp:HiddenField ID="hDTicketId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
