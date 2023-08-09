Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General

Public Class Home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then

            If Not Page.IsPostBack Then

                sb_ShowUserGridView()

                Dim TimeOfDay = DateTime.Now

                If (TimeOfDay.Hour >= 5 And TimeOfDay.Hour < 12) Then
                    lblGreeting.Text = "Good Morning, " + Session("U")

                ElseIf (TimeOfDay.Hour >= 12 And TimeOfDay.Hour < 16) Then
                    lblGreeting.Text = "Good Afternoon, " + Session("U")

                Else
                    lblGreeting.Text = "Good Evening, " + Session("U")
                End If

            End If

        End If

    End Sub

    Private Sub sb_ViewTaskAssignedToMe(strIdTask As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT  TOP 5  ID_TASK"
        strSQL = strSQL + ", ID_TICKETS"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_START_DATE, 109) EXPECTED_START_DATE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_END_DATE, 109) EXPECTED_END_DATE"
        strSQL = strSQL + ", REMARK"
        strSQL = strSQL + ", ISNULL(FLAG_ISSUES,'N') FLAG_ISSUES"
        strSQL = strSQL + "  FROM vs_Task WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND USERNAME = " + "'" + Page.User.Identity.Name + "'" + ""
        strSQL = strSQL + "  AND ISNULL(FLAG_COMPLETE,'N') = 'N'"

        If strIdTask <> "" Then
            strSQL = strSQL + " AND ID_TASK =" + "'" + strIdTask + "'" + ""
            strSQL = strSQL + " ORDER BY ID_TASK DESC"
        Else
            strSQL = strSQL + " ORDER BY ID_TASK DESC"
        End If

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        gvMyTask.DataSource = myDataSet
        gvMyTask.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Private Sub sb_StartCompleteTask(Op As String, idTask As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Start_Complete_Task"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", Op)
                objCommand.Parameters.AddWithValue("@ID_TASK", idTask)
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@REMARK", "")

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteReader()
                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

    Private Sub sb_ViewTestCases()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT TOP 5  ID_TEST_CASES"
        strSQL = strSQL + ", ID_TASK"
        strSQL = strSQL + ", ID_TICKETS"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", DEVELOPER"
        strSQL = strSQL + ", APP_CATEGORY"
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
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ISNULL(FLAG_COMPLETE_TEST,'N') = 'N'"
        strSQL = strSQL + "  ORDER BY ID_TEST_CASES DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        gvTestCase.DataSource = myDataSet
        gvTestCase.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub gvMyTask_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMyTask.RowCommand

        If e.CommandName = "CloseTask" Then

            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvMyTask.Rows(Index)
            Dim strIdTask As String = TryCast(row.FindControl("hdIdTask"), HiddenField).Value
            Dim strIdTicket As String = TryCast(row.FindControl("hdIdTicket"), HiddenField).Value

            Session("IdTicket") = strIdTicket
            sb_StartCompleteTask("C", strIdTask)

            Response.Redirect("Home.aspx")
        End If


    End Sub

    Protected Sub gvTestCase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTestCase.RowCommand

        If e.CommandName <> "Page" Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvTestCase.Rows(index)
            Dim strIdTestCase As String = TryCast(row.FindControl("hdIdTestCase"), HiddenField).Value

            If e.CommandName = "Details" Then
                Response.Redirect("~/TestCase/InsertUpdateTestCase.aspx?IdTC=" & strIdTestCase)
            End If

        End If

    End Sub

    Private Sub sb_ViewTickets()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT TOP 5 ID_TICKETS"
        strSQL = strSQL + ", ISSUE_TYPE"
        strSQL = strSQL + ", ISSUE_SUB_TYPE"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", STATUS_TICKET"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", INTERCOMMS"
        strSQL = strSQL + ", REQUEST_FROM"
        strSQL = strSQL + ", REMARKS_HELPDESK"
        strSQL = strSQL + ", REQUEST_BY"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", ISNULL(FLAG_RESOLVED,'N') FLAG_RESOLVED"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"
        strSQL = strSQL + "  AND ISNULL(FLAG_ASSIGNED,'N') = 'N'"
        strSQL = strSQL + "  ORDER BY ID_TICKETS DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvTickets.DataSource = myDataSet
        gvTickets.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Private Sub sb_ShowUserGridView()

        If IsInRole(Session("R"), Roll_Kind.Administrator) Then

            Call sb_ViewTickets()
            gvMyTask.Visible = False
            gvTestCase.Visible = False
            lblType.Text = "RECENT TICKETS"

        ElseIf IsInRole(Session("R"), Roll_Kind.Developer) Then

            Call sb_ViewTaskAssignedToMe("")
            gvMyTask.Visible = True
            gvTestCase.Visible = False
            gvTickets.Visible = False
            lblType.Text = "RECENT TASKS"

        ElseIf IsInRole(Session("R"), Roll_Kind.Tester) Then

            Call sb_ViewTestCases()
            gvMyTask.Visible = False
            gvTestCase.Visible = True
            gvTickets.Visible = False
            lblType.Text = "RECENT TESTCASES"

        ElseIf IsInRole(Session("R"), Roll_Kind.HelpDesk) Then

            Call sb_ViewTickets()
            gvMyTask.Visible = False
            gvTestCase.Visible = False
            lblType.Text = "RECENT TICKETS"

        Else

            Call sb_ViewTickets()
            gvMyTask.Visible = False
            gvTestCase.Visible = False
            lblType.Text = "RECENT TICKETS"

        End If

    End Sub

End Class