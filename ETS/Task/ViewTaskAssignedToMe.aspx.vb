Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General

Public Class ViewTaskAssignedToMe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then

            If fn_ValidatePageAccess() Then

                If Not IsPostBack Then

                    Call sb_ViewTaskAssignedToMe("")

                    If Not IsInRole(Session("R"), Roll_Kind.Administrator) Then
                        gvMyTask.Columns(10).Visible = False
                    End If

                End If
            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")

            End If

        End If

    End Sub

    Private Sub sb_ViewTaskAssignedToMe(strIdTask As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TASK"
        strSQL = strSQL + ", ID_TICKETS"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", ID_USER"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", REMARKS"
        strSQL = strSQL + ", ISNULL(FLAG_ISSUES,'N') FLAG_ISSUES"
        strSQL = strSQL + "  FROM vs_Task_Assigned_To_Me WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND USERNAME = " + "'" + Page.User.Identity.Name + "'" + ""
        'strSQL = strSQL + "  AND ISNULL(FLAG_COMPLETE,'N') = 'N'"

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

    Protected Sub gvTask_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMyTask.RowCommand

        If e.CommandName = "Modifica" Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvMyTask.Rows(index)
            Dim strIdTask As String = TryCast(row.FindControl("hdIdTask"), HiddenField).Value
            Dim strIdTicket As String = TryCast(row.FindControl("hdIdUser"), HiddenField).Value

            Response.Redirect("ViewDetailsTask.aspx?Id=" & strIdTask & "&IdTicket=" & strIdTicket & "")
        End If

        If e.CommandName = "Flag" Then

            Dim Id As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvMyTask.Rows(Id)
            Dim strIdTask As String = TryCast(row.FindControl("hdIdTask"), HiddenField).Value

            sb_LoadFailedTests(strIdTask)
            PopupViewTestError.Show()
        End If

    End Sub

    Protected Sub btnSearchTicket_Click(sender As Object, e As EventArgs)
        Call sb_ViewTaskAssignedToMe(txtTaskNumber.Text)
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

    Public Function fn_CheckStatus(ByVal element As Object) As Boolean

        If CStr(element) = "Y" Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub btClose_Click(sender As Object, e As EventArgs)
        PopupViewTestError.Hide()
    End Sub

    Private Sub sb_LoadFailedTests(strIdTask As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT TD.ID_TEST_CASE_DEFECTS"
        strSQL = strSQL + ", TC.ID_TASK"
        strSQL = strSQL + ", TD.SERVICE_LEVEL_REQUIREMENT"
        strSQL = strSQL + ", TD.TEST_SCENERIO"
        strSQL = strSQL + ", TD.TEST_STEPS"
        strSQL = strSQL + ", TD.EXPECTED_RESULT"
        strSQL = strSQL + ", TD.ACTUAL_RESULT"
        strSQL = strSQL + ", TD.TESTER"
        strSQL = strSQL + ", TD.STATUS_DEFECT"
        strSQL = strSQL + "  FROM TEST_CASES_DETAILS TD WITH(NOLOCK)"
        strSQL = strSQL + ", TEST_CASES TC WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE TD.ID_TEST_CASES = TC.ID_TEST_CASES"
        strSQL = strSQL + "  AND TC.ID_TASK =" + "'" + strIdTask + "'" + ""
        strSQL = strSQL + "  AND TD.STATUS_DEFECT = 'Failed'"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        gvFailedTest.DataSource = myDataSet
        gvFailedTest.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub gvFailedTest_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFailedTest.RowCommand

        Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
        Dim row As GridViewRow = gvFailedTest.Rows(Index)
        Dim strIdSLR As String = TryCast(row.FindControl("hdIdSLR"), HiddenField).Value

        If e.CommandName = "View" Then

            divGridViewDefect.Visible = True
            Call sb_LoadDefects(strIdSLR)

        End If

    End Sub

    Private Sub sb_LoadDefects(strIdSLR As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT DESCRIPTION"
        strSQL = strSQL + ", DEFECT_TYPE"
        strSQL = strSQL + ", STEPS_TO_REPRODUCE"
        strSQL = strSQL + ", EXPECTED_RESULT"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", TESTER"
        strSQL = strSQL + "  FROM TEST_DEFECT WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TEST_SLR =" + "'" + strIdSLR + "'" + ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        gvDefects.DataSource = myDataSet
        gvDefects.DataBind()
        dbConnect.ChiudiDb()

    End Sub

End Class

