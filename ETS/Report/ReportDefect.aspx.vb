Imports System.Data.SqlClient
Imports System.IO

Public Class ReportDefect
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack() Then
                If fn_ValidatePageAccess() Then
                    Call Popola_DropList_AppCategory()
                    gvReportDefect.DataSource = Nothing
                    Me.btnExportToXLS.Attributes.Add("onmouseout", "javascript:this.src='/img/excel.png'")
                    Me.btnExportToXLS.Attributes.Add("onmouseover", "javascript:this.src='/img/excel_over.png'")
                Else
                    FormsAuthentication.SignOut()
                    Response.Redirect("~/Login.aspx")
                End If
            End If
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
    Protected Sub btnExportToXLS_Click(sender As Object, e As EventArgs)
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment; filename=DownloadFile.xls")
        Response.ContentType = "application/vnd.xls"
        Dim WriteItem As StringWriter = New StringWriter()
        Dim htmlText As HtmlTextWriter = New HtmlTextWriter(WriteItem)
        gvReportDefect.AllowPaging = False
        Dim dtReportDefect As DataTable = CType(ViewState("dtReportDefect"), DataTable)
        gvReportDefect.DataSource = dtReportDefect
        gvReportDefect.DataBind()
        gvReportDefect.RenderControl(htmlText)
        Response.Write(WriteItem.ToString())
        Response.End()
    End Sub

    Private Sub sb_LoadDefectReport()
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT  ID_TEST_DEFECT"
        strSQL = strSQL + ", ID_TASK"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", APP_CATEGORY"
        strSQL = strSQL + ", ID_TEST_SLR"
        strSQL = strSQL + ", SLR"
        strSQL = strSQL + ", DEFECT_TYPE"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", EXPECTED_RESULT"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", TESTER"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_OPENED,109) DATE_OPENED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_FIXED, 109) DATE_FIXED"
        strSQL = strSQL + ", DEFECT_TYPE"
        strSQL = strSQL + ", STEPS_TO_REPRODUCE"
        strSQL = strSQL + ", IIF(ISNULL(FLAG_FIXED,'N')='Y','Fixed','Not Fixed') STATUS"
        strSQL = strSQL + "  FROM TEST_DEFECT WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1 = 1"

        If DropListCategory.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND APP_CATEGORY = '" + DropListCategory.SelectedItem.Text + "'"
        End If

        If DropListPriority.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND PRIORITY = '" + DropListPriority.SelectedItem.Text + "'"
        End If

        If DropDownListDefectType.SelectedItem.Text <> "" Then
            strSQL = strSQL + "AND  DEFECT_TYPE = '" + DropDownListDefectType.SelectedItem.Text + "'"
        End If

        If DropListStatus.SelectedItem.Text <> "" Then
            strSQL = strSQL + "AND FLAG_FIXED = '" + DropListStatus.SelectedValue + "'"
        End If

        If DropDownListDefectType.SelectedItem.Text <> "" Then
            strSQL = strSQL + "AND  DEFECT_TYPE = '" + DropDownListDefectType.SelectedItem.Text + "'"
        End If

        If txtStartDate.Text <> "" And txtEndDate.Text <> "" Then
            strSQL = strSQL + " AND (DATE_OPENED >= CONVERT(NVARCHAR(12),  '" + txtStartDate.Text + "',101) AND  (DATE_OPENED <= CONVERT(NVARCHAR(12), '" + txtEndDate.Text + "' ,101))) "
        End If

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim myDataSet As New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        ViewState("dtReportDefect") = dt

        gvReportDefect.DataSource = myDataSet
        gvReportDefect.DataBind()

        If gvReportDefect.Rows.Count <> 0 Then
            btnExportToXLS.Visible = True
        End If

        dbConnect.ChiudiDb()
    End Sub

    Private Sub Popola_DropList_AppCategory()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strSQL As String

        strSQL = "SELECT '' SUB_DESCRIPTION"
        strSQL = strSQL & " FROM TICKET_TYPE WITH(NOLOCK)"
        strSQL = strSQL & " UNION "
        strSQL = strSQL & " SELECT DISTINCT SUB_DESCRIPTION "
        strSQL = strSQL & " FROM TICKET_TYPE WITH(NOLOCK);"
        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione
        Dim objDataReader As SqlDataReader

        objDataReader = objCommand.ExecuteReader()
        DropListCategory.DataSource = objDataReader
        DropListCategory.DataTextField = "SUB_DESCRIPTION"
        DropListCategory.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnSearchDefect_Click()
        Call sb_LoadDefectReport()
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