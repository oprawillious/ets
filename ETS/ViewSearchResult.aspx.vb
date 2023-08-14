Imports System.Data.SqlClient


Public Class ViewSearchResult
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack Then
                Call sb_LoadSearchResult()
            End If
        End If
    End Sub

    Private Sub sb_LoadSearchResult()
        Dim strOp As String = Request.QueryString("Op")
        Dim strId As String = Request.QueryString("Id")

        If strOp = "TK" Then
            sb_LoadTicket(strId)
        End If
        If strOp = "TS" Then
            sb_LoadTask(strId)
        End If
        If strOp = "TC" Then
            sb_LoadTestCase(strId)
        End If

        lblSearchId.Text = strId

    End Sub

    Private Sub sb_LoadTicket(strId As String)
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TICKETS"
        strSQL = strSQL + ", ISSUE_TYPE"
        strSQL = strSQL + ", ISSUE_SUB_TYPE"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", STATUS_TICKET"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", INTERCOMMS"
        strSQL = strSQL + ", REMARKS_HELPDESK"
        strSQL = strSQL + ", REQUEST_BY"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", ISNULL(FLAG_RESOLVED,'N') FLAG_RESOLVED"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TICKETS =" + "'" + strId + "'" + ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvSearchResultsTicket.DataSource = myDataSet
        gvSearchResultsTicket.DataBind()
        dbConnect.ChiudiDb()
    End Sub

    Private Sub sb_LoadTask(strId As String)
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TASK"
        strSQL = strSQL + ", ID_TICKETS"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", TYPE_TASK"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", REMARK"
        strSQL = strSQL + ", DEV_REMARKS"
        strSQL = strSQL + "  FROM vs_Task_Dropdown WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TASK =" + "'" + strId + "'" + ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvSearchResultTask.DataSource = myDataSet
        gvSearchResultTask.DataBind()
        dbConnect.ChiudiDb()
    End Sub

    Private Sub sb_LoadTestCase(strId As String)
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASES"
        strSQL = strSQL + ", ID_TASK"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", DEVELOPER"
        strSQL = strSQL + ", SERVICE_LEVEL_REQUIREMENT"
        strSQL = strSQL + ", SCENERIO_TEST_CASE"
        strSQL = strSQL + ", TEST_STEPS"
        strSQL = strSQL + ", EXPECTED_RESULTS"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", TESTER"
        strSQL = strSQL + ", ISNULL(STATUS_TEST,'status not set') STATUS_TEST"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_START_TEST,109) DATE_START_TEST"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_COMPLETE_TEST,109) DATE_COMPLETE_TEST"
        strSQL = strSQL + "  FROM vs_Test_Cases WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TEST_CASES =" + "'" + strId + "'" + ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvSearchResultTestCase.DataSource = myDataSet
        gvSearchResultTestCase.DataBind()
        dbConnect.ChiudiDb()
    End Sub

End Class