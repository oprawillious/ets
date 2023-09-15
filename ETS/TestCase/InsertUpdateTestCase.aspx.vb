Imports System.Data.SqlClient
Imports System.Drawing.Color
Imports System.IO
Imports System.Web.Configuration

Public Class InsertUpdateTestCase
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then

            If Not Page.IsPostBack Then

                tdmarkAsComplete.Visible = False
                hdOpIdTestcase.Value = Request.QueryString("IdTC")
                hdOpIdTask.Value = Request.QueryString("IdT")

                If hdOpIdTask.Value <> "" Then
                    Dim boolFlagCompleted = fn_CompleteStatus(hdOpIdTask.Value)

                    If boolFlagCompleted Then
                        'btnInsertTestCaseDetails.Visible = False
                        'btnInsertTestEnvironment.Visible = False

                    End If

                End If

                If hdOpIdTestcase.Value <> "" Then

                    Call sb_LoadTestCaseDetails()
                    Call sb_LoadTestDefects()
                    Call sb_LoadTestCaseLog()

                    Call Popola_DropList_Users()
                    Call Popola_DropList_Testers()

                End If

            End If
        End If

    End Sub
    Protected Sub btClose_Click(sender As Object, e As EventArgs)
        PopupTestCaseDetails.Hide()
        Response.Redirect("InsertUpdateTestCase.aspx?IdTC=" & hdOpIdTestcase.Value & "&IdT=" & hdOpIdTask.Value & "")
    End Sub
    Protected Sub btnUpdateTestCase_Click(sender As Object, e As EventArgs)
        sb_UpdateTestCase()
        Response.Redirect("ViewTestCase.aspx")

    End Sub

    Private Sub sb_LoadTestCaseDetails()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASES"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", SCENERIO_TEST_CASE"
        strSQL = strSQL + ", TEST_STEPS"
        strSQL = strSQL + ", EXPECTED_RESULTS"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", TESTER"
        strSQL = strSQL + ", ISNULL(STATUS_TEST,'status_not_set') STATUS_TEST"
        strSQL = strSQL + ", ISNULL(FLAG_START_TEST,'N') FLAG_START_TEST"
        strSQL = strSQL + ", FLAG_COMPLETE_TEST"
        strSQL = strSQL + "  FROM vs_Test_Cases WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TEST_CASES = " & hdOpIdTestcase.Value & ""

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

            If Not IsNothing(objDataReader.Item("ID_TEST_CASES")) Then
                lblEditTestCase.Text = CStr(objDataReader.Item("ID_TEST_CASES") & "") & " - " & CStr(objDataReader.Item("DESCRIPTION") & "")
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

    End Sub

    Private Sub sb_UpdateTestCase()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Update_Test_Case"

                objCommand.Connection = connessioneDb.Connessione
                objCommand.Parameters.AddWithValue("@TESTER", Page.User.Identity.Name)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblMessage.Text = strError
                Else
                    lblMessage.Text = "Test Case updated successfully !"
                End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

    Private Sub sb_ChangeTestCaseDetailsStatus(strIdTestCaseDetails As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Change_TestCase_Test_Status"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@ID_TEST_CASE_DETAILS", strIdTestCaseDetails)
                objCommand.Parameters.AddWithValue("@ID_TEST_CASES", hdOpIdTestcase.Value)
                objCommand.Parameters.AddWithValue("@STATUS_TEST", DropListStatusTest.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@ACTUAL_RESULT", Session("ActualResult"))
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@ID_DEFECT", Session("hdIdTestCaseDetails"))

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 1000

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblAssign.Text = strError
                Else
                    lblAssign.Text = "Test case details status updated successfully !"
                    Response.Redirect("InsertUpdateTestCase.aspx?IdTC=" & hdOpIdTestcase.Value & "&IdT=" & hdOpIdTask.Value & "")
                End If

            End If

        End If
        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnClose_Status_Test_Click()
        ChangeStatusTestPopUp.Hide()
    End Sub

    Protected Sub btnChange_Status_Click()
        Call sb_ChangeTestCaseDetailsStatus(hdStatus.Value)
    End Sub

    Private Sub sb_InsertUpdateTestCaseDetails(strOp As String, strIdTestCaseDetails As String, strIdDefect As String)

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

                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_TEST_CASES", hdOpIdTestcase.Value)
                objCommand.Parameters.AddWithValue("@ID_TEST_CASE_DETAILS", strIdTestCaseDetails)
                objCommand.Parameters.AddWithValue("@TEST_STEPS", txtTestSteps.Text)
                objCommand.Parameters.AddWithValue("@EXPECTED_RESULT", txtExpectedResult.Text)
                objCommand.Parameters.AddWithValue("@ACTUAL_RESULT", txtActualResult.Text)
                objCommand.Parameters.AddWithValue("@SCREENSHOT", txtScreenshotLink.Text)
                objCommand.Parameters.AddWithValue("@ISSUE_TYPE", DropListIssueType.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@SEVERITY", DropListSeverity.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@ID_DEFECT", strIdDefect)

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
                    Response.Redirect("InsertUpdateTestCase.aspx?IdTC=" & hdOpIdTestcase.Value & "&IdT=" & hdOpIdTask.Value & "")
                End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub



    Protected Sub btnInsertTestCaseDetails_Click(sender As Object, e As EventArgs)
        PopupTestCaseDetails.Show()
        lblDetailsTestCase.Text = "New TestCase Details"
    End Sub

    Private Sub sb_LoadTestDefects()

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
        strSQL = strSQL + ", SCREENSHOT"
        strSQL = strSQL + ", ISSUE_TYPE"
        strSQL = strSQL + ", SEVERITY"
        strSQL = strSQL + ", DATE_OPEN_DEFECT"
        strSQL = strSQL + "  FROM TEST_CASES_DETAILS"
        strSQL = strSQL + "  WHERE ID_TEST_CASES = " & hdOpIdTestcase.Value & ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        gvViewDetailsTestCase.DataSource = myDataSet
        gvViewDetailsTestCase.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub gvViewDetailsTestCase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViewDetailsTestCase.RowCommand

        Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
        Dim row As GridViewRow = gvViewDetailsTestCase.Rows(Index)
        Dim strIdTestCaseDetails As String = TryCast(row.FindControl("hdIdTestCaseDefect"), HiddenField).Value
        Dim strActualResult As String = TryCast(row.FindControl("hdActualResult"), HiddenField).Value

        hdDefectId_log.Value = strIdTestCaseDetails
        hdId.Value = strIdTestCaseDetails

        Session("ActualResult") = strActualResult
        Session("hdIdTestCaseDetails") = hdId.Value

        hdStatus.Value = strIdTestCaseDetails

        If e.CommandName = "Modifica" Then
            Call sb_LoadViewDetailTestCase()
            PopupTestCaseDetails.Show()
            lblDetailsTestCase.Text = "Edit TestCase Details"
        End If

        If e.CommandName = "status" Then
            lblstatus.Text = "Update Test Status"
            ChangeStatusTestPopUp.Show()
        End If

        If e.CommandName = "Remove" Then
            sb_DeleteDetailsTestCase(strIdTestCaseDetails)
            Response.Redirect("InsertUpdateTestCase.aspx?IdTC=" & hdOpIdTestcase.Value & "&IdT=" & hdOpIdTask.Value & "")
        End If

    End Sub

    Private Sub sb_LoadViewDetailTestCase()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASE_DEFECTS"
        strSQL = strSQL + ", TEST_STEPS"
        strSQL = strSQL + ", EXPECTED_RESULT"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", SCREENSHOT"
        strSQL = strSQL + ", ISSUE_TYPE"
        strSQL = strSQL + ", SEVERITY"
        strSQL = strSQL + "  FROM TEST_CASES_DETAILS"
        strSQL = strSQL + "  WHERE ID_TEST_CASE_DEFECTS =" & hdId.Value & ""

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

            If Not IsNothing(objDataReader.Item("ID_TEST_CASE_DEFECTS")) Then
                lblEditTestCase.Text = "Update: Test Case ID - " + CStr(objDataReader.Item("ID_TEST_CASE_DEFECTS") & "")
            End If

            If Not IsNothing(objDataReader.Item("TEST_STEPS")) Then
                txtTestSteps.Text = CStr(objDataReader.Item("TEST_STEPS") & "")
            End If
            If Not IsNothing(objDataReader.Item("EXPECTED_RESULT")) Then
                txtExpectedResult.Text = CStr(objDataReader.Item("EXPECTED_RESULT") & "")
            End If
            If Not IsNothing(objDataReader.Item("ACTUAL_RESULT")) Then
                txtActualResult.Text = CStr(objDataReader.Item("ACTUAL_RESULT") & "")
            End If
            If Not IsNothing(objDataReader.Item("SCREENSHOT")) Then
                txtScreenshotLink.Text = CStr(objDataReader.Item("SCREENSHOT") & "")
            End If
            If Not IsNothing(objDataReader.Item("ISSUE_TYPE")) Then
                DropListIssueType.SelectedValue = CStr(objDataReader.Item("ISSUE_TYPE") & "")
            End If
            If Not IsNothing(objDataReader.Item("SEVERITY")) Then
                DropListSeverity.SelectedValue = CStr(objDataReader.Item("SEVERITY") & "")
            End If
        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()
    End Sub

    Private Sub sb_DeleteDetailsTestCase(strIdTestCaseDetails As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Details_TestCase"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "D")
                objCommand.Parameters.AddWithValue("@ID_TEST_CASE_DETAILS", strIdTestCaseDetails)
                objCommand.Parameters.AddWithValue("@ID_TEST_CASES", 0)
                objCommand.Parameters.AddWithValue("@ID_TESTCASE_DEFECTS", 0)
                objCommand.Parameters.AddWithValue("@ID_DEFECT", 0)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)
            End If
        End If

        connessioneDb.ChiudiDb()

    End Sub

    Private Sub sb_LoadTestCaseLog()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TESTCASE"
        strSQL = strSQL + ", ID_TEST_CASE_DEFECTS"
        strSQL = strSQL + ", DESC_LOG"
        strSQL = strSQL + ", ID_USER"
        strSQL = strSQL + ", DATE_LOG"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", STATUS_TEST"
        strSQL = strSQL + "  FROM TESTCASE_LOG WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ID_TESTCASE =" & hdOpIdTestcase.Value & ""
        strSQL = strSQL + "  ORDER BY ID_TESTCASE_LOG DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        gvTestCaseLog.DataSource = myDataSet
        gvTestCaseLog.DataBind()
        dbConnect.ChiudiDb()
    End Sub

    Public Function fn_CheckStatus(ByVal element As Object) As Boolean
        If CStr(element) = "Passed" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function fn_CompleteStatus(ByVal element As String) As Boolean

        Dim strFlagCompleted = fn_RetrieveCompleteStatus(element)

        If strFlagCompleted = "Y" Then
            Return True
        Else
            Return False
        End If

    End Function


    Public Function fn_RetrieveCompleteStatus(element As String) As String

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT "
        strSQL = strSQL + "FLAG_COMPLETE "
        strSQL = strSQL + "FROM vs_Task_Dropdown WITH(NOLOCK) "
        strSQL = strSQL + "WHERE ID_TASK = " & element & ""


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


        Dim strComplete = dt.Rows(0)("FLAG_COMPLETE").ToString

        Return strComplete

    End Function


    Protected Sub gvTestCaseLog_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTestCaseLog.PageIndexChanging
        gvTestCaseLog.PageIndex = e.NewPageIndex
        Call sb_LoadTestCaseLog()
    End Sub

    Protected Sub gvViewDetailsTestCase_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvViewDetailsTestCase.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim imageFail As Image = CType(e.Row.FindControl("ImgFail"), Image)

            If e.Row.DataItem("STATUS_DEFECT").ToString() = "status not set" Then
                imageFail.Visible = False
                lbcompleteTest.Text = ""

            ElseIf e.Row.DataItem("TEST_MARKED_AS_COMPLETED").ToString() = "Y" Then
                lbcompleteTest.Text = "Testcase marked as completed"

            Else
                tdmarkAsComplete.Visible = True
                lbcompleteTest.Text = ""

            End If

            If e.Row.DataItem("SCREENSHOT").ToString() = "" Then
                'imageFail.Visible = False
                lbcompleteTest.Text = ""

            ElseIf e.Row.DataItem("TEST_MARKED_AS_COMPLETED").ToString() = "Y" Then
                lbcompleteTest.Text = "Testcase marked as completed"

            Else
                tdmarkAsComplete.Visible = True
                lbcompleteTest.Text = ""

            End If

        End If

    End Sub

    Protected Sub btnMarkAsComplete_Click()

        If Session("hdIdTestCaseDetails") <> "" Then
            Call sb_Mark_As_Complete()

        Else
            lblpopUpErrmsg.ForeColor = Red
            lblpopUpErrmsg.Text = "please set test status"
            errMsgPopup.Show()

        End If
    End Sub

    Private Sub sb_Mark_As_Complete()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Mark_Test_Case_As_Complete"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@ID_TEST_CASES", hdOpIdTestcase.Value)
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@ID_DEFECT", Session("hdIdTestCaseDetails"))
                objCommand.Parameters.AddWithValue("@ACTUAL_RESULT", Session("ActualResult"))

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 1000

                objCommand.ExecuteNonQuery()
                strError = objCommand.Parameters("@ERROR_CODE").Value.ToString()

                If strError <> "" Then
                    lblpopUpErrmsg.ForeColor = Red
                    lblpopUpErrmsg.Text = strError
                    errMsgPopup.Show()
                Else
                    Response.Redirect("ViewTestCase.aspx")
                End If

            End If
        End If

        connessioneDb.ChiudiDb()

    End Sub
    Protected Sub btn_Err_msg_Close_Click()
        errMsgPopup.Hide()
        Response.Redirect("InsertUpdateTestCase.aspx?IdTC=" & hdOpIdTestcase.Value & "&IdT=" & hdOpIdTask.Value & "")
    End Sub

    Protected Sub btnInsertTestEnvironment_Click(sender As Object, e As EventArgs)
        PopupTestEnvironment.Show()
        lblEnvironment.Text = "Select Environment"
    End Sub

    Protected Sub btnClose_Environment_Click()
        PopupTestEnvironment.Hide()
    End Sub

    Protected Sub btnChange_Environment_Click()
        Call sb_ChangeTestCaseEnvironment()
    End Sub


    Private Sub sb_ChangeTestCaseEnvironment()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Change_TestCase_Environment"
                objCommand.Connection = connessioneDb.Connessione

                'objCommand.Parameters.AddWithValue("@ID_TEST_CASE_DETAILS", hdId.Value)
                objCommand.Parameters.AddWithValue("@ID_TEST_CASES", hdOpIdTestcase.Value)
                objCommand.Parameters.AddWithValue("@ENVIRONMENT", DropListEnvironment.SelectedItem.Text)
                'objCommand.Parameters.AddWithValue("@ACTUAL_RESULT", Session("ActualResult"))
                'objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                'objCommand.Parameters.AddWithValue("@ID_DEFECT", Session("hdIdTestCaseDetails"))

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 1000

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblAssign.Text = strError
                Else
                    lblAssign.Text = "Test Environment updated successfully !"
                    Response.Redirect("InsertUpdateTestCase.aspx?IdTC=" & hdOpIdTestcase.Value & "&IdT=" & hdOpIdTask.Value & "")
                End If

            End If

        End If
        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnConfirmDetailsTestCase_Click(sender As Object, e As EventArgs)
        If hdId.Value <> "" Then
            Call sb_InsertUpdateTestCaseDetails("M", hdId.Value, hdDefectId_log.Value)
        Else
            If txtTestSteps.Text <> "" And txtExpectedResult.Text <> "" And txtActualResult.Text <> "" Then
                Call sb_InsertUpdateTestCaseDetails("I", 0, Session("hdDfectId"))
                Response.Redirect("InsertUpdateTestCase.aspx?IdTC=" & hdOpIdTestcase.Value & "&IdT=" & hdOpIdTask.Value & "")

            Else
                lblmsg.Text = "All Fields marked (*) must be filled"
                lblmsg.Visible = True
                'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "formValidation();", True)
            End If

        End If
    End Sub


    Private Sub Popola_DropList_Users()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        'hdOpIdTask.Value = Request.QueryString("Id")

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


    Protected Sub gvDevelopers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDevelopers.RowCommand

        If e.CommandName = "Modifica" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvDevelopers.Rows(Index)

            Dim strIdUser As String = TryCast(row.FindControl("hdIdDv"), HiddenField).Value
            Dim strIdTask As String = TryCast(row.FindControl("hdIdDvTask"), HiddenField).Value

            Response.Redirect("InsertTaskRemark.aspx?Id=" & strIdUser & "&IdTask=" & strIdTask & "")

        End If



    End Sub

    Private Sub Popola_DropList_Testers()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        'hdOpIdTask.Value = Request.QueryString("Id")

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

End Class