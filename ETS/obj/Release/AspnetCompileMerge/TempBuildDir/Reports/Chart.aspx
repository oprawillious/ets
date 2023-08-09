<%@ Page Title="ETS_DASHBOARD.mrt - Viewer" Language="VB" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="Chart.aspx.vb" Inherits="ETS.Chart" %>
<%@ Register assembly="Stimulsoft.Report.Web" namespace="Stimulsoft.Report.Web" tagprefix="cc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<div>
		<cc1:StiWebViewer ID="StiWebViewer1" runat="server"
		OnGetReport="StiWebViewer1_GetReport">
	</cc1:StiWebViewer>
	</div>
	
</asp:Content>