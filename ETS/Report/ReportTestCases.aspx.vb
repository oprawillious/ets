Imports System.Data.SqlClient

Public Class ReportTestCases
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If fn_ValidatePageAccess() Then
                If Not Page.IsPostBack Then
                    Call Popola_DropList_Tester()
                End If

                gvReportTestCase.DataSource = Nothing
                Me.Excelexport.Attributes.Add("onmouseout", "javascript:this.src='/img/excel.png'")
                Me.Excelexport.Attributes.Add("onmouseover", "javascript:this.src='/img/excel_over.png'")
            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")
            End If
        End If
    End Sub

    Private Sub Popola_DropList_Tester()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT '' USERNAME"
        strSQL = strSQL + " UNION SELECT U.USERNAME"
        strSQL = strSQL + " FROM USERS U, ROLES R "
        strSQL = strSQL + " WHERE U.ID_ROLE = R.ID_ROLES "
        strSQL = strSQL + " AND R.ID_ROLES = 3"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListTester.DataSource = objDataReader
        DropListTester.DataTextField = "USERNAME"
        DropListTester.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub btnSearchTickets_Click(sender As Object, e As EventArgs)
        Call fn_LoadData()
        'lblDetails.Visible = True
    End Sub

    Private Function fn_LoadData()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASES"
        strSQL = strSQL + ", ID_TASK"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_START, 106) DATE_START"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_COMPLETE, 106) DATE_COMPLETE"
        strSQL = strSQL + ", TEST_PASSED"
        strSQL = strSQL + ", TEST_FAILED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_COMPLETE_TEST, 106) DATE_COMPLETE_TEST"
        strSQL = strSQL + ", ASSIGNED_TO"
        strSQL = strSQL + "  FROM vs_Report_Test_Cases WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1 = 1"

        If DropListTester.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND ASSIGNED_TO =  '" + DropListTester.SelectedItem.Text + "'  "
        End If

        If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date Start Test" Then
            strSQL = strSQL + " AND DATE_START >=  CONVERT(NVARCHAR(12), '" + txtStartDate.Text + "',111) AND   DATE_START <=  CONVERT(NVARCHAR(12), '" + txtEndDate.Text + "',111)  "
        End If
        If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date Complete Test" Then
            strSQL = strSQL + " AND DATE_COMPLETE >=  CONVERT(NVARCHAR(12), '" + txtStartDate.Text + "',111) AND   DATE_COMPLETE <=  CONVERT(NVARCHAR(12), '" + txtEndDate.Text + "',111)  "
        End If

        strSQL = strSQL & " ORDER BY ID_TEST_CASES DESC"


        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim myDataSet As New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        ViewState("dtReport") = dt

        'gvReportTestCase.DataSource = myDataSet
        'gvReportTestCase.DataBind()

        'If gvReportTestCase.Rows.Count <> 0 Then
        '    Excelexport.Visible = True
        'End If

        gvReportTestCase.DataSource = myDataSet
        'gvTask.GridLines = 3
        gvReportTestCase.Settings.GridLines = CType([Enum].Parse(GetType(GridLines), "both", True), GridLines)
        gvReportTestCase.DataBind()

        If dt.Rows.Count > 0 Then
            Excelexport.Visible = True
        End If

        dbConnect.ChiudiDb()
        Return strSQL

    End Function


    'Private Function fn_LoadData()

    '    Dim dbConnect As New DataBase
    '    Dim strSQL As String

    '    If dbConnect.StatoConnessione = 0 Then
    '        dbConnect.connettidb()
    '    End If

    '    strSQL = "SELECT ID_TEST_CASES"
    '    strSQL = strSQL + ", DESCRIPTION"
    '    strSQL = strSQL + ", APP_CATEGORY"
    '    strSQL = strSQL + ", SERVICE_LEVEL_REQUIREMENT"
    '    strSQL = strSQL + ", SCENERIO_TEST_CASE"
    '    strSQL = strSQL + ", TEST_STEPS"
    '    strSQL = strSQL + ", EXPECTED_RESULTS"
    '    strSQL = strSQL + ", ACTUAL_RESULT"
    '    strSQL = strSQL + ", TESTER"
    '    strSQL = strSQL + ", STATUS_TEST"
    '    strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_START_TEST, 106) DATE_START_TEST"
    '    strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_COMPLETE_TEST, 106) DATE_COMPLETE_TEST"
    '    strSQL = strSQL + "  FROM vs_Report_Test_Cases WITH(NOLOCK)"
    '    strSQL = strSQL & "  WHERE 1 = 1"

    '    If DropListTester.SelectedItem.Text <> "" Then
    '        strSQL = strSQL + " AND TESTER =  '" + DropListTester.SelectedItem.Text + "'  "
    '    End If
    '    If txtServiceLevelRequirements.Text <> "" Then
    '        strSQL = strSQL + " AND SERVICE_LEVEL_REQUIREMENT LIKE  '% " + txtServiceLevelRequirements.Text + " %'  "
    '    End If
    '    If DropListStatusTest.SelectedItem.Text <> "" Then
    '        strSQL = strSQL + " AND STATUS_TEST =  '" + DropListStatusTest.SelectedItem.Text + "'  "
    '    End If
    '    If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date Start Test" Then
    '        strSQL = strSQL + " AND DATE_START_TEST >=  CONVERT(NVARCHAR(12), '" + txtStartDate.Text + "',111) AND   DATE_START_TEST <=  CONVERT(NVARCHAR(12), '" + txtEndDate.Text + "',111)  "
    '    End If
    '    If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date Complete Test" Then
    '        strSQL = strSQL + " AND DATE_COMPLETE_TEST >=  CONVERT(NVARCHAR(12), '" + txtStartDate.Text + "',111) AND   DATE_COMPLETE_TEST <=  CONVERT(NVARCHAR(12), '" + txtEndDate.Text + "',111)  "
    '    End If

    '    Dim objCommand As SqlCommand = New SqlCommand()
    '    objCommand.CommandText = strSQL
    '    objCommand.CommandType = CommandType.Text
    '    objCommand.Connection = dbConnect.Connessione

    '    Dim mySqlAdapter As New SqlDataAdapter(objCommand)
    '    Dim myDataSet As New DataSet()
    '    mySqlAdapter.Fill(myDataSet)

    '    Dim dt As DataTable = New DataTable()
    '    mySqlAdapter.Fill(dt)
    '    ViewState("dtReport") = dt

    '    gvReportTestCase.DataSource = myDataSet
    '    gvReportTestCase.DataBind()

    '    If gvReportTestCase.Rows.Count <> 0 Then
    '        Excelexport.Visible = True
    '    End If

    '    dbConnect.ChiudiDb()
    '    Return strSQL

    'End Function

    Protected Sub Excelexport_Click(sender As Object, e As EventArgs)

        Call btExcel_OnClick()

    End Sub

    Private Sub btExcel_OnClick()

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment; filename=TestCaseReport.xls")
        Response.ContentType = "application/vnd.xls"
        Dim WriteItem As System.IO.StringWriter = New System.IO.StringWriter()
        Dim htmlText As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(WriteItem)
        'gvReportTestCase.Settings.ColumnMaxWidth = 100%
        'gvReportTestCase.Settings.ColumnMinWidth = 100%
        Dim dtSupplier As DataTable = CType(ViewState("dtReport"), DataTable)
        gvReportTestCase.DataSource = dtSupplier
        gvReportTestCase.DataBind()

        gvReportTestCase.RenderControl(htmlText)
        Response.Write(WriteItem.ToString())
        Response.End()

    End Sub

    'Protected Sub Excelexport_Click(sender As Object, e As EventArgs)
    '    Call sb_ExportToXLS()
    'End Sub

    Private Sub sb_ExportToXLS()
        Dim strAppDirectory As String = HttpContext.Current.Server.MapPath("~/" & "Reports\ETS_Report_TestCases.mrt")
        Dim strTablename As String = "vs_Report_Test_Cases"
        Dim Stimulsoft As New General
        Dim strSQL = fn_LoadData()
        Stimulsoft.sb_StimulsoftExcelReport(strSQL, strAppDirectory, strTablename)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
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