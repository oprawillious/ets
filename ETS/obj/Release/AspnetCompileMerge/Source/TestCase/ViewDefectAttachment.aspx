<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="ViewDefectAttachment.aspx.vb" Inherits="ETS.viewDefectAttachment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<div class="Alignments2">
    <br />
    <h3>Defects > View Attachments</h3>
    <hr class="greenLine"/>

    <h2><asp:Label ID="lblCaption" runat="server" Text="Defect Attachments"></asp:Label></h2>
        <asp:GridView ID="gvDefectAttachments" 
                      runat="server"
                      AutoGenerateColumns="False"
                      CellPadding="1"
                      CellSpacing="1"
                      GridLines="None"
                      AllowPaging="True"
                      PageSize ="10"
                      CssClass="table-bordered table-sm"
                      PagerStyle-CssClass="table pgr"     
                      AlternatingRowStyle-CssClass="normal11" 
                      Width="98%">
            <Columns>
                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:HiddenField ID="hdIdDefectDoc" runat="server" Value='<%# Eval("ID_DEFECT_DOCUMENT")%>'/>
                          <asp:HiddenField ID="hdFilePath" runat="server" Value='<%# Eval("FILE_PATH")%>'/>
                          <asp:Image runat="server" ID="imgVisible2" ImageUrl="~/images/attachments.png"/>
                      </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="FILE_NAME" HeaderText="Attachment"/>             

                 <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                          <asp:Button ID="btnViewImage" runat="server" Text="Download" CommandName="Download" class="button primary"/> 
                      </ItemTemplate>
                 </asp:TemplateField>


            </Columns>
            <SelectedRowStyle BackColor="#fba557" Font-Bold="True" ForeColor="#ffffff"/>

            <EmptyDataTemplate>
                <div style="color: red; text-align: center;">No Attachments !</div>
            </EmptyDataTemplate>

            <PagerStyle HorizontalAlign="Center" CssClass = "GridPager" />
        </asp:GridView>
          </div>

    <asp:ModalPopupExtender ID="PopupImageView" 
                        BehaviorID="mpe1" 
                        runat="server"
                        PopupControlID="pnlPopup" 
                        TargetControlID="hdCardRequest" 
                        BackgroundCssClass="modalBackground"/>
     
    <asp:HiddenField ID="hdCardRequest" runat="server"/>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <div class="modal-dialog" role="document" style="border: 2px solid #2aa85e; background-color: #f2f3f2; height: 700px; width: 1000px; border-radius: 10px;">
                <div class="modal-content" style="width: 700px; margin: 0 auto;">
                    <br />
                    <div class="modal-header">                        
                        <asp:Label ID="lblDetailsTestCase" runat="server" Text="Image View"/>
                    </div>
                    <hr />
                    <div class="modal-body">                    
                       <asp:Image ID="imgDefects" runat="server" style="height:100%; width: 100%; margin: 0 auto;"/>
                    </div>
                    <hr />
                    <div class="modal-footer">
                        <asp:Button ID="btClose" class="btn btn-danger" runat="server" Text="Close" OnClick="btClose_Click"/>                       
                    </div>
                </div>
            </div>   
                </ContentTemplate>
                    </asp:UpdatePanel>
        </asp:Panel>        
</asp:Content>
