Imports System.Data.SqlClient

Public Class ReportDeveloper
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If fn_ValidatePageAccess() Then
                If Not Page.IsPostBack Then
                    Call Popola_DropList_AssignedTo()
                End If

                gvReportTask.DataSource = Nothing
                Me.btnExportToXLS.Attributes.Add("onmouseout", "javascript:this.src='/img/excel.png'")
                Me.btnExportToXLS.Attributes.Add("onmouseover", "javascript:this.src='/img/excel_over.png'")
            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")
            End If
        End If
    End Sub

    Protected Sub btnSearchTask_Click(sender As Object, e As EventArgs)
        Call fn_LoadData()
        'lblDetails.Visible = True
    End Sub

    Private Sub sb_ExportToXLS()
        Dim strAppDirectory As String = HttpContext.Current.Server.MapPath("~/" & "Reports\ETS_Report_Task.mrt")
        Dim strTablename As String = "vs_Report_Task"
        Dim Stimulsoft As New General
        Dim strSQL = fn_LoadData()
        Stimulsoft.sb_StimulsoftExcelReport(strSQL, strAppDirectory, strTablename)
    End Sub

    Protected Sub btnExportToXLS_Click(sender As Object, e As EventArgs)

        Call btExcel_OnClick()

    End Sub

    Private Sub btExcel_OnClick()

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment; filename=DeveloperReport.xls")
        Response.ContentType = "application/vnd.xls"
        Dim WriteItem As System.IO.StringWriter = New System.IO.StringWriter()
        Dim htmlText As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(WriteItem)
        'gvReportTestCase.Settings.ColumnMaxWidth = 100%
        'gvReportTestCase.Settings.ColumnMinWidth = 100%
        Dim dtSupplier As DataTable = CType(ViewState("dtReportTask"), DataTable)
        gvReportTask.DataSource = dtSupplier
        gvReportTask.DataBind()

        gvReportTask.RenderControl(htmlText)
        Response.Write(WriteItem.ToString())
        Response.End()

    End Sub

    'Protected Sub btnExportToXLS_Click(sender As Object, e As EventArgs)
    '    Call sb_ExportToXLS()
    'End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    End Sub

    Private Function fn_LoadData()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TASK"
        strSQL = strSQL + ", ID_TEST_CASES"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", TYPE_TASK"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", ASSIGNED_TO"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", STATUS_TEST"
        strSQL = strSQL + ", TEST_START_DATE"
        strSQL = strSQL + ", TEST_END_DATE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_START, 106) DATE_START"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_COMPLETE, 106) DATE_COMPLETE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_ASSIGNED, 106) DATE_ASSIGNED"
        strSQL = strSQL + "  FROM vs_Report_Task WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1 = 1"

        If txtTaskNumber.Text <> "" Then
            strSQL = strSQL + " AND ID_TASK =  '" + txtTaskNumber.Text + "'  "
        End If
        If txtDescription.Text <> "" Then
            strSQL = strSQL + " AND TASK_DESCRIPTION LIKE '%" + txtDescription.Text + "%'  "
        End If
        If DropListPriority.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND PRIORITY =  '" + DropListPriority.SelectedItem.Text + "'  "
        End If
        If DropListStatusTask.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND STATUS_TASK =  '" + DropListStatusTask.SelectedItem.Text + "'  "
        End If
        'If DropListAssignedTo.SelectedItem.Text <> "" Then
        '    strSQL = strSQL + " AND ASSIGNED_TO =  '" + DropListAssignedTo.SelectedItem.Text + "'  "
        'End If
        'If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Start Date" Then
        '    strSQL = strSQL + " AND (DATE_START >=  CONVERT(NVARCHAR(12), '" + txtStartDate.Text + "',101) AND (DATE_START <= CONVERT(NVARCHAR(12),'" + txtEndDate.Text + "',101 ))) "
        'End If
        'If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Completed Date" Then
        '    strSQL = strSQL + " AND (DATE_COMPLETE >= CONVERT(NVARCHAR(12),  '" + txtStartDate.Text + "',101) AND  (DATE_COMPLETE <= CONVERT(NVARCHAR(12), '" + txtEndDate.Text + "' ,101))) "
        'End If
        'If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Assigned Date" Then
        '    strSQL = strSQL + " AND (DATE_ASSIGNED >= CONVERT(NVARCHAR(12),  '" + txtStartDate.Text + "',101) AND  (DATE_ASSIGNED <= CONVERT(NVARCHAR(12),'" + txtEndDate.Text + "',101 ))) "
        'End If

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim myDataSet As New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        ViewState("dtReportTask") = dt

        'gvReportTask.DataSource = myDataSet
        'gvReportTask.DataBind()

        'If gvReportTask.Rows.Count <> 0 Then
        '    btnExportToXLS.Visible = True
        'End If


        gvReportTask.DataSource = myDataSet
        'gvTask.GridLines = 3
        gvReportTask.Settings.GridLines = CType([Enum].Parse(GetType(GridLines), "both", True), GridLines)
        gvReportTask.DataBind()

        If dt.Rows.Count > 0 Then
            btnExportToXLS.Visible = True
        End If

        dbConnect.ChiudiDb()
        Return strSQL

    End Function

    Private Sub Popola_DropList_AssignedTo()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT '' [FIRST_NAME]"
        strSQL = strSQL + " UNION  SELECT  U.FIRST_NAME"
        strSQL = strSQL + " From USERS U, ROLES R "
        strSQL = strSQL + " Where U.ID_ROLE = R.ID_ROLES "
        strSQL = strSQL + " And R.ID_ROLES <> 5 "

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListAssignedTo.DataSource = objDataReader
        DropListAssignedTo.DataTextField = "FIRST_NAME"
        DropListAssignedTo.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

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

End Class