Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General
Imports System.Web.UI.WebControls
Public Class ViewDetailsTask

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If Not IsInRole(Session("R"), Roll_Kind.Administrator) Then
                gvDevelopers.Columns(7).Visible = False
            End If


            'btnStartTask.Visible = False

            'btnMarkAsComplete.Visible = False

            If fn_ValidatePageAccess() Or IsInRole(Session("R"), Roll_Kind.Developer) Or IsInRole(Session("R"), Roll_Kind.Administrator) Then

                If Not IsPostBack Then

                    hdOpIdTask.Value = Request.QueryString("Id")
                    hdOpIdTicket.Value = Request.QueryString("IdTicket")

                    If hdOpIdTask.Value <> "" Then

                        Call sb_LoadTaskDetails()
                        Call Popola_DropList_Users()
                        Call Popola_DropList_Testers()
                        Call Popola_DropList_Users1()
                        Call Popola_DropList_Users2()
                        Call sb_LoadTaskLog()
                        Call sb_ViewTestCases("")
                        'Call sb_CheckTaskStart()


                    End If

                End If
            Else

                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")

            End If

        End If

    End Sub



    Protected Sub btnStartTask_Click(sender As Object, e As EventArgs)

        'PopupStartTask.Show()

        If btnStartTask.Text = "Start this Task" Then
            Call sb_StartCompleteTask("S", hdOpIdTask.Value)
            'PopupStartTask.Show()
            btnStartTask.Text = "Mark as Completed"
            Call Popola_DropList_Users()
        Else
            Call sb_StartCompleteTask("C", hdOpIdTask.Value)
            Response.Redirect("ViewTaskAssignedToMe.aspx?Id=" & hdOpIdTask.Value)
        End If

    End Sub

    Private Sub Popola_DropList_Users1()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strSQL As String

        strSQL = " SELECT 0 ID_USERS,'' USERS"
        strSQL = strSQL & "  UNION"
        strSQL = strSQL & "  SELECT U.ID_USERS"
        strSQL = strSQL & ", (U.FIRST_NAME +' - '+R.ROLE_DESCRIPTION) USERS"
        strSQL = strSQL & "  FROM USERS U WITH(NOLOCK)"
        strSQL = strSQL & ", ROLES R WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1=1"
        strSQL = strSQL & "  AND U.ID_ROLE = R.ID_ROLES"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListDev1.DataSource = objDataReader
        DropListDev1.DataValueField = "ID_USERS"
        DropListDev1.DataTextField = "USERS"
        DropListDev1.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_Users2()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = " SELECT 0 ID_USERS,'' USERS"
        strSQL = strSQL & "  UNION"
        strSQL = strSQL & "  SELECT U.ID_USERS"
        strSQL = strSQL & ", (U.FIRST_NAME +' - '+R.ROLE_DESCRIPTION) USERS"
        strSQL = strSQL & "  FROM USERS U WITH(NOLOCK)"
        strSQL = strSQL & ", ROLES R WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1=1"
        strSQL = strSQL & "  AND U.ID_ROLE = R.ID_ROLES"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListDev2.DataSource = objDataReader
        DropListDev2.DataValueField = "ID_USERS"
        DropListDev2.DataTextField = "USERS"
        DropListDev2.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub


    Private Sub sb_LoadTaskDetails()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TASK"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", REMARKS"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_START_DATE) EXPECTED_START_DATE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_END_DATE) EXPECTED_END_DATE"
        strSQL = strSQL + ", ISNULL(FLAG_START,'N') FLAG_START"
        strSQL = strSQL + ", ISNULL(FLAG_COMPLETE,'N') FLAG_COMPLETE"
        strSQL = strSQL + ", ISNULL(FLAG_ISSUES,'N') FLAG_ISSUES"
        strSQL = strSQL + ", ISNULL(FLAG_ASSIGNED,'N') FLAG_ASSIGNED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + "  FROM vs_Task WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TASK =" & hdOpIdTask.Value & " "
        If hdOpIdTicket.Value <> "" Then
            strSQL = strSQL + "  AND ID_USER=" & hdOpIdTicket.Value & " "
        End If

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim myDataSet As New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        If objDataReader.HasRows Then

            objDataReader.Read()


            If Not IsNothing(objDataReader.Item("ID_TASK")) Then
                hdIdTask.Value = CStr(objDataReader.Item("ID_TASK") & "")
                lblDetailTask.Text = "Details - Task No: " + CStr(objDataReader.Item("ID_TASK") & "")
            End If

            If Not IsNothing(objDataReader.Item("TASK_DESCRIPTION")) Then
                lblDescription.Text = CStr(objDataReader.Item("TASK_DESCRIPTION") & "")
            End If

            If Not IsNothing(objDataReader.Item("CATEGORY")) Then
                lblCategory.Text = CStr(objDataReader.Item("CATEGORY") & "")
            End If

            If Not IsNothing(objDataReader.Item("USERNAME")) Then
                lblAssignedTo.Text = CStr(objDataReader.Item("USERNAME") & "")
            End If

            If Not IsNothing(objDataReader.Item("DATE_ASSIGNED")) Then
                lblDateAssigned.Text = CStr(objDataReader.Item("DATE_ASSIGNED") & "")
            End If

            If Not IsNothing(objDataReader.Item("STATUS_TASK")) Then
                lblStatus.Text = CStr(objDataReader.Item("STATUS_TASK") & "")
            End If

            'If Not IsNothing(objDataReader.Item("REMARK")) Then
            '    txt.Text = CStr(objDataReader.Item("REMARK") & "")
            'End If

            If Not IsNothing(objDataReader.Item("EXPECTED_START_DATE")) Then
                lblStartDate.Text = CStr(objDataReader.Item("EXPECTED_START_DATE") & "")
            End If

            If Not IsNothing(objDataReader.Item("EXPECTED_END_DATE")) Then
                lblEndDate.Text = CStr(objDataReader.Item("EXPECTED_END_DATE") & "")
            End If

            If Not IsNothing(objDataReader.Item("FLAG_START")) Then
                If CStr(objDataReader.Item("FLAG_START")) = "Y" And CStr(objDataReader.Item("FLAG_COMPLETE")) = "N" Then
                    'btnStartTask.Text = "Mark as Complete"
                    If IsInRole(Session("R"), Roll_Kind.Administrator) Then
                        btnStartTask.Visible = False
                        btnMarkAsComplete.Visible = False
                    Else
                        btnStartTask.Visible = False
                        btnMarkAsComplete.Visible = True
                    End If
                End If

                If Not IsNothing(objDataReader.Item("FLAG_COMPLETE")) Then
                    If CStr(objDataReader.Item("FLAG_COMPLETE")) = "Y" And CStr(objDataReader.Item("FLAG_START")) = "Y" Then
                        If IsInRole(Session("R"), Roll_Kind.Administrator) Then
                            btnStartTask.Visible = False
                            btnMarkAsComplete.Visible = False
                        Else
                            btnStartTask.Visible = False
                            btnMarkAsComplete.Visible = False
                            lblMessage.Text = "Task is completed."
                        End If
                    End If
                End If

                If Not IsNothing(objDataReader.Item("FLAG_ASSIGNED")) Then
                    'If CStr(objDataReader.Item("FLAG_ASSIGNED")) = "N" And IsInRole(Session("R"), Roll_Kind.Administrator) Then
                    If IsInRole(Session("R"), Roll_Kind.Administrator) Then
                        If IsInRole(Session("R"), Roll_Kind.Administrator) Then
                            btnStartTask.Visible = False
                            btnMarkAsComplete.Visible = False
                        Else
                            btnAssignAsTask.Visible = True
                            btnStartTask.Visible = False
                        End If
                    ElseIf CStr(objDataReader.Item("FLAG_ASSIGNED")) = "Y" And IsInRole(Session("R"), Roll_Kind.Administrator) Then
                        btnAssignAsTask.Visible = False
                        btnStartTask.Visible = False
                        btnMarkAsComplete.Visible = False
                        lblMessage.Text = "Task already assigned."

                    Else
                        btnAssignAsTask.Visible = False
                    End If

                End If

            End If
        End If
        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub btClose_Click(sender As Object, e As EventArgs)
        PopupStartTask.Hide()
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
                objCommand.Parameters.AddWithValue("@USER_ID", hdOpIdTicket.Value)
                'objCommand.Parameters.AddWithValue("@REMARK", txtDevRemarks.Text)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteReader()
                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strErrorStored <> "" Then
                    lblAssign.Text = strErrorStored
                Else
                    lblAssign.Text = "Task completed Successfully."
                End If
            End If
        End If

        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnStart_Click(sender As Object, e As EventArgs)

        Call sb_StartCompleteTask("S", hdOpIdTask.Value)
        Response.Redirect("ViewTaskAssignedToMe.aspx?Id=" & hdOpIdTask.Value)

    End Sub

    Private Sub sb_AssignTask(strIdUser As String, strFlagDev As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Task"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@ID_DEV_USER", strIdUser)
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@REMARKS", txtAdminRemarks.Text)
                objCommand.Parameters.AddWithValue("@PRIORITY", DropListPriority.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@TYPE_TASK", "")
                objCommand.Parameters.AddWithValue("@ESTIMATED_CLOSE_DATE", txtCloseDate.Text)
                objCommand.Parameters.AddWithValue("@START_DATE", txtStartDate.Text)
                objCommand.Parameters.AddWithValue("@CATEGORY", lblCategory.Text)
                objCommand.Parameters.AddWithValue("@DESCRIPTION", lblDescription.Text)
                objCommand.Parameters.AddWithValue("@ID_TASK", hdOpIdTask.Value)
                objCommand.Parameters.AddWithValue("@ID_TICKET", hdOpIdTicket.Value)
                objCommand.Parameters.AddWithValue("@NO_OF_DEV", strFlagDev)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblAssign.Text = strError
                Else
                    lblAssign.Text = "Task assigned successfully !"
                End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub btnAssignTask_Click(sender As Object, e As EventArgs)

        If DropListDev1.SelectedItem.Text <> "" And DropListDev2.SelectedItem.Text <> "" Then
            Call sb_AssignTask(DropListDev1.SelectedValue, 1)
            Call sb_AssignTask(DropListDev2.SelectedValue, 2)

        ElseIf DropListDev1.SelectedItem.Text <> "" And DropListDev2.SelectedItem.Text = "" Then
            Call sb_AssignTask(DropListDev1.SelectedValue, 1)

        ElseIf DropListDev2.SelectedItem.Text <> "" And DropListDev1.SelectedItem.Text = "" Then
            Call sb_AssignTask(DropListDev2.SelectedValue, 1)

        End If

        Response.Redirect("ViewTask.aspx")
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

    Protected Sub btnAssignAsTask_Click(sender As Object, e As EventArgs)
        PopUpAssignTask.Show()
    End Sub

    Protected Sub btnClosePopAssignTask_Click(sender As Object, e As EventArgs)
        PopUpAssignTask.Hide()
    End Sub

    Private Sub sb_LoadTaskLog()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TASK_LOG"
        strSQL = strSQL + ", ID_TASK"
        strSQL = strSQL + ", DESC_LOG"
        strSQL = strSQL + ", ID_USER"
        strSQL = strSQL + ", DATE_LOG"
        strSQL = strSQL + "  FROM TASK_LOG WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ID_TASK =" & hdOpIdTask.Value & ""
        strSQL = strSQL + "  ORDER BY ID_TASK DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvTasklog.DataSource = myDataSet

        gvTasklog.DataBind()
        dbConnect.ChiudiDb()
    End Sub

    Protected Sub btnMarkAsComplete_Click(sender As Object, e As EventArgs)

        Call sb_StartCompleteTask("C", hdOpIdTask.Value)

        Dim assignedUsers As DataTable = fn_Assigned_Users(hdOpIdTask.Value)
        Dim numberOfNulls = 0
        Dim prevDate = DateTime.Now.AddDays(-1000)

        If assignedUsers.Rows.Count > 0 Then

            For Each assignedUser As DataRow In assignedUsers.Rows

                Dim strExpectedResult = assignedUser("DATE_COMPLETED")

                'If DBNull.Value Is assignedUser("DATE_COMPLETED") Then
                '    numberOfNulls = numberOfNulls + 1
                'Else
                '    Dim startDate = Convert.ToDateTime(assignedUser("DATE_COMPLETED"))
                '    If startDate > 
                'End If

            Next

        End If

        If numberOfNulls > 0 Then



        Else

        End If



        Response.Redirect("ViewTaskAssignedToMe.aspx?Id=" & hdOpIdTask.Value)

    End Sub

    Protected Sub btn_duplicate(sender As Object, e As EventArgs)

        Response.Redirect("ViewTaskAssignedToMe.aspx?Id=" & hdOpIdTask.Value)

    End Sub

    Private Sub Popola_DropList_Users()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        hdOpIdTask.Value = Request.QueryString("Id")

        Dim strSQL As String
        strSQL = "SELECT "
        strSQL = strSQL & " ID_ROLES"
        strSQL = strSQL & ",ID_TASK"
        strSQL = strSQL & ",REMARKS"
        strSQL = strSQL & ",DATE_STARTED"
        strSQL = strSQL & ",DATE_COMPLETED"
        strSQL = strSQL & ",APP_CATEGORY"
        strSQL = strSQL & ",ID_USERS"
        strSQL = strSQL & ",ROLE_DESCRIPTION"
        strSQL = strSQL & ",FIRST_NAME"


        'strSQL = " SELECT DISTINCT ID_USERS, ROLE_DESCRIPTION, FIRST_NAME"
        strSQL = strSQL & "  FROM vs_Task_Assign"
        strSQL = strSQL & "  WHERE ID_TASK = " & hdOpIdTask.Value & ""
        strSQL = strSQL & "  AND ID_ROLES = 2"

        'strSQL = strSQL & "  AND R.ROLE_DESCRIPTION = 'Developer'"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        gvDevelopers.DataSource = objDataReader

        gvDevelopers.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub


    Private Sub Popola_DropList_Testers()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        hdOpIdTask.Value = Request.QueryString("Id")

        Dim strSQL As String

        strSQL = " SELECT DISTINCT ID_USERS, ID_ROLES, ROLE_DESCRIPTION, FIRST_NAME"
        strSQL = strSQL & "  FROM vs_Test_Assign"
        strSQL = strSQL & "  WHERE ID_TASK = " & hdOpIdTask.Value & ""
        strSQL = strSQL & "  AND ID_ROLES = 3"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        gvTesters.DataSource = objDataReader

        gvTesters.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub



    Private Sub sb_ViewTestCases(strIdTestCase As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        hdOpIdTask.Value = Request.QueryString("Id")

        strSQL = "SELECT ID_TEST_CASES"
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
        strSQL = strSQL + "  AND ID_TASK = " + "'" + hdOpIdTask.Value & "'" + ""

        If strIdTestCase <> "" Then
            strSQL = strSQL + " AND ID_TEST_CASES =" + "'" + strIdTestCase + "'" + ""
            strSQL = strSQL + " ORDER BY ID_TEST_CASES DESC"
        Else
            strSQL = strSQL + " ORDER BY ID_TEST_CASES DESC"
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

        gvTestCase.DataSource = myDataSet
        gvTestCase.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub gvTestCase_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTestCase.PageIndexChanging
        gvTestCase.PageIndex = e.NewPageIndex
        Call sb_ViewTestCases("")

    End Sub

    Protected Sub gvTestCase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTestCase.RowCommand

        If e.CommandName <> "Page" Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvTestCase.Rows(index)

            Dim strIdTestCase As String = TryCast(row.FindControl("hdIdTestCase"), HiddenField).Value
            Dim description As String = TryCast(row.FindControl("hdDescription"), HiddenField).Value
            Dim strIdTicket As String = TryCast(row.FindControl("hdIdTicket"), HiddenField).Value
            Dim strIdTask As String = TryCast(row.FindControl("hdIdTestCaseTask"), HiddenField).Value

            Dim strIdTaskTestCase As String = TryCast(row.FindControl("hdIdTestCase1"), HiddenField).Value

            If e.CommandName = "Duplicate" Then

                If strIdTask <> "" Then

                    Dim lastpart As String = description.Substring(description.LastIndexOf(" ") + 1)
                    Dim strDescription = ""

                    Dim strLast As Integer
                    Dim checkInteger = Int32.TryParse(lastpart, strLast)

                    If checkInteger Then
                        strDescription = description.Substring(0, description.LastIndexOf(" ")).Trim
                    Else
                        strDescription = description
                    End If


                    Dim strCountOfTestCase = fn_ViewTestCases(strDescription, strIdTask)
                    Dim strIdNewTestCase As String = fn_Insert_New_TestCases(strDescription, strIdTask, strIdTestCase, strCountOfTestCase)
                    Dim testCases As DataTable = fn_LoadTestDefects(strIdTestCase)
                    For Each testCase As DataRow In testCases.Rows
                        Dim strExpectedResult = testCase("EXPECTED_RESULT").ToString
                        Dim strTestSteps = testCase("TEST_STEPS").ToString
                        Dim strActualResult = testCase("ACTUAL_RESULT").ToString

                        Call sb_InsertUpdateTestCaseDetails(strIdNewTestCase, strTestSteps, strExpectedResult, strActualResult)
                    Next

                    Response.Redirect("ViewDetailsTask.aspx?Id=" & strIdTask & "&IdTicket=" & strIdTicket & "")

                    Response.Redirect("ViewDetailsTask.aspx?Id=" & strIdTask & "&IdTicket=" & strIdTicket & "")

                End If

            End If

        End If

    End Sub
    Protected Sub gvDevelopers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDevelopers.RowCommand

        If e.CommandName = "Modifica" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvDevelopers.Rows(Index)

            Dim strIdUser As String = TryCast(row.FindControl("hdIdDv"), HiddenField).Value
            Dim strIdTask As String = TryCast(row.FindControl("hdIdDvTask"), HiddenField).Value

            Response.Redirect("InsertTaskRemark.aspx?Id=" & strIdUser & "&IdTask=" & strIdTask & "")

        End If



    End Sub


    Protected Sub gvTestCase_OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTestCase.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim strIdTaskTestCase As String = gvTestCase.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim grdDetails As GridView = TryCast(e.Row.FindControl("grdDetails"), GridView)
            grdDetails.DataSource = fn_LoadTestDefectsDetails(strIdTaskTestCase)
            grdDetails.DataBind()
        End If
    End Sub


    Private Sub sb_Insert_New_TestCases(strDescription As String, strIdTask As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Test_Case"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@USER_ID ", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@TEST_DESCRIPTION", strDescription + "1")
                objCommand.Parameters.AddWithValue("@ID_TASK", strIdTask)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_TEST_CASES", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                End If

            End If
        End If
        connessioneDb.ChiudiDb()
    End Sub


    Private Function fn_Insert_New_TestCases(strDescription As String, strIdTask As String, strIdTestCase As Integer, strCountOfTestCase As Integer)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String
        Dim strIdTestCaseStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Test_Case"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@USER_ID ", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@TEST_DESCRIPTION", strDescription + " " + (strCountOfTestCase + 1).ToString)
                objCommand.Parameters.AddWithValue("@ID_TASK", strIdTask)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_TEST_CASES", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                strIdTestCaseStored = CStr(objCommand.Parameters("@ID_TEST_CASES").Value)
                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                End If

                Return strIdTestCaseStored

            End If
        End If
        connessioneDb.ChiudiDb()


    End Function


    Private Function fn_RetrieveTestCases(strIdTestCase As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASES"
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

        If strIdTestCase <> "" Then
            strSQL = strSQL + " AND ID_TEST_CASES =" + "'" + strIdTestCase + "'" + ""
            strSQL = strSQL + " ORDER BY ID_TEST_CASES DESC"
        Else
            strSQL = strSQL + " ORDER BY ID_TEST_CASES DESC"
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

        dbConnect.ChiudiDb()

        Return dt


    End Function


    Private Function fn_LoadTestDefects(strIdTestCase As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASES"
        strSQL = strSQL + ", ID_TEST_CASE_DEFECTS"
        strSQL = strSQL + ", TEST_STEPS"
        strSQL = strSQL + ", EXPECTED_RESULT"
        strSQL = strSQL + ", TEST_MARKED_AS_COMPLETED"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", FLAG_CLOSE_DEFECT"
        strSQL = strSQL + ", TESTER"
        strSQL = strSQL + ", STATUS_DEFECT"
        strSQL = strSQL + "  FROM TEST_CASES_DETAILS"
        strSQL = strSQL + "  WHERE ID_TEST_CASES = " & strIdTestCase & ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        dbConnect.ChiudiDb()

        Return dt

    End Function


    Private Function fn_LoadTestDefectsDetails(strIdTestCase As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If
        strSQL = "SELECT TCD.ID_TEST_CASE_DEFECTS"
        strSQL = strSQL + ", TCD.ID_TEST_CASES"
        strSQL = strSQL + ", TCD.TEST_STEPS"
        strSQL = strSQL + ", TCD.EXPECTED_RESULT"
        strSQL = strSQL + ", TCD.TEST_MARKED_AS_COMPLETED"
        strSQL = strSQL + ", TCD.ACTUAL_RESULT"
        strSQL = strSQL + ", TCD.FLAG_CLOSE_DEFECT"
        strSQL = strSQL + ", TCD.TESTER"
        strSQL = strSQL + ", TCD.STATUS_DEFECT"
        strSQL = strSQL + ", TCD.ID_USER"
        strSQL = strSQL + ", TCD.DATE_LOG"
        strSQL = strSQL + " FROM vs_Test_Cases_Details TCD"
        strSQL = strSQL + " INNER JOIN"
        strSQL = strSQL + " (SELECT ID_TEST_CASE_DEFECTS, MAX(DATE_LOG) AS MaxDateTime"
        strSQL = strSQL + " FROM vs_Test_Cases_Details WHERE ID_TEST_CASES = " & strIdTestCase & ""
        strSQL = strSQL + "  GROUP BY ID_TEST_CASE_DEFECTS) groupedTCD"
        strSQL = strSQL + "  ON TCD.ID_TEST_CASE_DEFECTS = groupedTCD.ID_TEST_CASE_DEFECTS"
        strSQL = strSQL + "  AND (TCD.DATE_LOG = groupedTCD.MaxDateTime OR TCD.DATE_LOG IS NULL)"

        'strSQL = "SELECT ID_TEST_CASES"
        'strSQL = strSQL + ", ID_TEST_CASE_DEFECTS"
        'strSQL = strSQL + ", TEST_STEPS"
        'strSQL = strSQL + ", EXPECTED_RESULT"
        'strSQL = strSQL + ", TEST_MARKED_AS_COMPLETED"
        'strSQL = strSQL + ", ACTUAL_RESULT"
        'strSQL = strSQL + ", FLAG_CLOSE_DEFECT"
        'strSQL = strSQL + ", TESTER"
        'strSQL = strSQL + ", STATUS_DEFECT"
        'strSQL = strSQL + ", ID_USER"
        'strSQL = strSQL + ", DATE_LOG"
        'strSQL = strSQL + "  FROM vs_Test_Cases_Details"
        'strSQL = strSQL + "  WHERE ID_TEST_CASES = " & strIdTestCase & ""


        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        dbConnect.ChiudiDb()

        Return dt

    End Function



    Private Sub sb_InsertUpdateTestCaseDetails(strIdTestCase As String, strTestSteps As String, strExpectedResult As String, strActualResult As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String
        Dim IdTestCaseDefect As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Details_TestCase"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "I")
                objCommand.Parameters.AddWithValue("@ID_TEST_CASES", strIdTestCase)
                objCommand.Parameters.AddWithValue("@ID_TEST_CASE_DETAILS", 0)
                objCommand.Parameters.AddWithValue("@TEST_STEPS", strTestSteps)
                objCommand.Parameters.AddWithValue("@EXPECTED_RESULT", strExpectedResult)
                objCommand.Parameters.AddWithValue("@ACTUAL_RESULT", strActualResult)
                objCommand.Parameters.AddWithValue("@USER_ID", "")
                objCommand.Parameters.AddWithValue("@ID_DEFECT", 0)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 1000

                Dim objOutputParameter1 As New SqlParameter("@ID_TESTCASE_DEFECTS", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 1000

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                IdTestCaseDefect = objCommand.Parameters("@ID_TESTCASE_DEFECTS").Value.ToString()

                Session("hdDfectId") = IdTestCaseDefect

                If strError <> "" Then
                    lblAssign.Text = strError
                Else
                    lblAssign.Text = "Test case details Inserted successfully !"
                End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub btnInsertTestCase_Click(sender As Object, e As EventArgs)

        Dim val = Request.QueryString("Id")

        Response.Redirect(String.Format("~/TestCase/InsertTestCase.aspx?Id={0}&Task={1}", val, lblDescription.Text))


        Response.Redirect("~/TestCase/InsertTestCase.aspx")
    End Sub

    Private Function fn_ViewTestCases(strDescriptionTestCase As String, strIdTask As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASES"
        strSQL = strSQL + "  FROM vs_Test_Cases WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND DESCRIPTION LIKE " + "'%" + strDescriptionTestCase & "%'" + ""
        strSQL = strSQL + "  AND ID_TASK = " + "'" + strIdTask & "'" + ""


        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        dbConnect.ChiudiDb()

        Return dt.Rows.Count

    End Function

    Public Function fn_CheckStatus(ByVal element As Object)
        If CStr(element) = "Passed" Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Function fn_CheckNotCompleteStatus(ByVal element As String)
        If CStr(element) <> "Passed" And CStr(element) <> "Failed" Then
            Return True
        End If
    End Function

    Private Sub sb_CheckTaskStart()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT "
        strSQL = strSQL & " ID_ROLES"
        strSQL = strSQL & ",ID_TASK"
        strSQL = strSQL & ",REMARKS"
        strSQL = strSQL & ",DATE_STARTED"
        strSQL = strSQL & ",DATE_COMPLETED"
        strSQL = strSQL & ",APP_CATEGORY"
        strSQL = strSQL & ",ID_USERS"
        strSQL = strSQL & ",ROLE_DESCRIPTION"
        strSQL = strSQL & ",FIRST_NAME"


        strSQL = strSQL & "  FROM vs_Task_Assign"
        strSQL = strSQL & "  WHERE ID_TASK = " & hdOpIdTask.Value & " AND ID_USERS=" & hdOpIdTicket.Value & ""
        strSQL = strSQL & "  AND ID_ROLES = 2"


        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim myDataSet As New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        If objDataReader.HasRows Then

            objDataReader.Read()


            If Not IsNothing(objDataReader.Item("DATE_STARTED")) Then
                btnMarkAsComplete.Visible = True
            Else
                btnStartTask.Visible = True
            End If



        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

    End Sub



    Private Function fn_Assigned_Users(strIdT As Integer)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        'Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = " SELECT ID_USER"
        strSQL = strSQL & "  ID_TASK"
        strSQL = strSQL & "  FROM TASK_USER_ASSIGN WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1=1"
        strSQL = strSQL & "  AND ID_TASK =" & strIdT & ""

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        connessioneDb.ChiudiDb()

        Return dt

    End Function



End Class