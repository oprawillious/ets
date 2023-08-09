Imports System.Data.SqlClient
Imports System.IO
Imports ETS.DataBase
Imports ETS.General

Public Class ReportRequisition
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Page.User.Identity.IsAuthenticated Then
			If Not Page.IsPostBack() Then
				If fn_ValidatePageAccess() Then
					'Call Popola_DropList_AppCategory()
					gvReportRequisition.DataSource = Nothing
					Me.btnExportToPdf.Attributes.Add("onmouseout", "javascript:this.src='/img/excel.png'")
					Me.btnExportToPdf.Attributes.Add("onmouseover", "javascript:this.src='/img/excel_over.png'")
				Else
					FormsAuthentication.SignOut()
					Response.Redirect("~/Login.aspx")
				End If
			End If
		End If

	End Sub



	Private Function sb_viewRequisition()

		Dim dbConnect As New DataBase
		Dim strSQL As String

		If dbConnect.StatoConnessione = 0 Then
			dbConnect.connettidb()
		End If

		strSQL = "SELECT ID_REQUISITION"
		strSQL = strSQL + ", ID_USER"
		strSQL = strSQL + ", ITEM_DESCRIPTION"
		strSQL = strSQL + ", ID_ITEM_CATEGORY"
		strSQL = strSQL + ", USERNAME"
		strSQL = strSQL + ", QUANTITY"
		strSQL = strSQL + ", APPROVED_BY"
		strSQL = strSQL + ", REMARKS"
		strSQL = strSQL + ", DATE_IT_MANAGER_APPROVE"
		strSQL = strSQL + ", REQUESTED_BY"
		strSQL = strSQL + ", DEPT"
		strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED, 109) DATE_REQUESTED"
		strSQL = strSQL + ", IIF(FLAG_ADMIN_APPROVE IS NULL,'Pending', IIF(FLAG_ADMIN_APPROVE= 'Y', 'Approved', 'Rejected'))FLAG_ADMIN_APPROVE"
		strSQL = strSQL + ", REASON_FOR_REQUEST"
		strSQL = strSQL + "  FROM vs_Requisition WITH(NOLOCK)"
		strSQL = strSQL + "  WHERE 1 = 1"


		If Not IsInRole(Session("R"), Roll_Kind.Administrator) And Not IsInRole(Session("R"), Roll_Kind.IT_Manager) Then
			strSQL = strSQL + " AND USERNAME =" + "'" + Page.User.Identity.Name + "'" + ""
		End If

		If txtEmployeeName.Text <> "" Then
			strSQL = strSQL + " AND REQUESTED_BY LIKE '%" + txtEmployeeName.Text + "%'"
		End If


		If DropListStatus.SelectedValue <> "" Then
			strSQL = strSQL + " AND FLAG_ADMIN_APPROVE = " + "'" + DropListStatus.SelectedValue + "'" + ""
		End If


		If txtRequestDate.Text <> "" Then
			strSQL = strSQL + " AND CONVERT(NVARCHAR(12),DATE_REQUESTED,109) =" & "CONVERT(NVARCHAR(12)," & "CAST('" & txtRequestDate.Text & "' AS DATETIME)" & ",109)"
		End If

		strSQL = strSQL + " ORDER BY ID_REQUISITION DESC"

		Dim objCommand As SqlCommand = New SqlCommand()
		objCommand.CommandText = strSQL
		objCommand.CommandType = CommandType.Text
		objCommand.Connection = dbConnect.Connessione

		Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
		Dim myDataSet As DataSet = New DataSet()
		mySqlAdapter.Fill(myDataSet)

		Dim dt As DataTable = New DataTable()
		mySqlAdapter.Fill(dt)
		ViewState("dtRequisitionReport") = dt

		gvReportRequisition.DataSource = myDataSet
		gvReportRequisition.DataBind()

		If gvReportRequisition.Rows.Count <> 0 Then
			btnExportToPdf.Visible = True
		End If
		dbConnect.ChiudiDb()

		Return strSQL


		dbConnect.ChiudiDb()

	End Function


	Protected Sub btn_SearchRequisition_Click()
		Call sb_viewRequisition()
	End Sub
	Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

	End Sub
	Private Sub sb_Export_ToPdf()
		Dim strAppDirectory As String = HttpContext.Current.Server.MapPath("~/" & "Reports\ETS_Report_Requisition.mrt")
		Dim strTablename As String = "vs_Requisition"
		Dim Stimulsoft As New General
		Dim strSQL = sb_viewRequisition()
		Stimulsoft.sb_StimulsoftPdfReport(strSQL, strAppDirectory, strTablename)

	End Sub

	Protected Sub btnExport_Requisition_ToPdf_Click()
		Call sb_Export_ToPdf()
	End Sub


	Private Function fn_ValidatePageAccess() As Boolean
		Dim general As New General
		Dim pageUrl = general.fn_GetAbsolutePath()
		Dim Access = False

		If general.fn_GrantWebInterface(pageUrl, Session("R")) Then
			Access = True
		End If

		Return Access
	End Function


	Protected Sub gvReportRequisition_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvReportRequisition.PageIndexChanging
		gvReportRequisition.PageIndex = e.NewPageIndex()
		Call sb_viewRequisition()

	End Sub
End Class