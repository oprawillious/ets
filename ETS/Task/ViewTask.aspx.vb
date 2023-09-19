Imports System.Data.SqlClient
Imports System.Web.Services
Imports ETS.DataBase
Imports ETS.General

Public Class ViewTask
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then

            If fn_ValidatePageAccess() Then

                If Not IsPostBack Then

                    Call sb_ViewTask("")

                    If Not IsInRole(Session("R"), Roll_Kind.Administrator) Then
                        gvTask.Columns(13).Visible = False
                    End If

                End If

            End If
        Else
            Response.Redirect("~/Login.aspx")
        End If

    End Sub

    Private Sub sb_ViewTask(strIdTask As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If
        strSQL = "SELECT DISTINCT ID_TASK"
        strSQL = strSQL + ", ID_TICKETS"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", TYPE_TASK"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_START_DATE, 109) EXPECTED_START_DATE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_END_DATE, 109) EXPECTED_END_DATE"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", FLAG_COMPLETE"
        'strSQL = strSQL + ", REMARKS"
        strSQL = strSQL + ", DEV_REMARKS"
        strSQL = strSQL + "  FROM vs_Task WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        'strSQL = strSQL + "  AND FLAG_COMPLETE IS NOT NULL"
        strSQL = strSQL + "  AND (ISNULL(FLAG_COMPLETE,'N') = 'N'"
        strSQL = strSQL + "  OR ISNULL(FLAG_COMPLETE,'Y') = 'Y')"

        'strSQL = "SELECT ID_TASK"
        'strSQL = strSQL + ", ID_TICKETS"
        'strSQL = strSQL + ", TASK_DESCRIPTION"
        'strSQL = strSQL + ", CATEGORY"
        'strSQL = strSQL + ", PRIORITY"
        'strSQL = strSQL + ", USERNAME"
        'strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        'strSQL = strSQL + ", TYPE_TASK"
        'strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_START_DATE, 109) EXPECTED_START_DATE"
        'strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_END_DATE, 109) EXPECTED_END_DATE"
        'strSQL = strSQL + ", STATUS_TASK"
        'strSQL = strSQL + ", REMARK"
        'strSQL = strSQL + ", DEV_REMARKS"
        'strSQL = strSQL + "  FROM vs_Task_Dropdown WITH(NOLOCK)"
        'strSQL = strSQL + "  WHERE 1=1"
        'strSQL = strSQL + "  AND ISNULL(FLAG_COMPLETE,'N') = 'N'"

        If strIdTask <> "" Or DropListStatus.SelectedItem.Value <> "" Then
            If strIdTask <> "" Then
                strSQL = strSQL + " AND ID_TASK =" + "'" + strIdTask + "'" + ""
            End If

            If DropListStatus.SelectedItem.Value <> "" Then
                strSQL = strSQL + " AND STATUS_TASK =" + "'" + DropListStatus.SelectedItem.Value + "'" + ""
            End If
        Else
            strSQL = strSQL + " AND STATUS_TASK <> 'Completed'"
        End If

        strSQL = strSQL + " ORDER BY ID_TASK DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)


        gvTask.DataSource = myDataSet
        'gvTask.GridLines = 3

        gvTask.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub gvTask_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTask.RowCommand

        If e.CommandName = "Modifica" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvTask.Rows(Index)

            Dim strIdTask As String = TryCast(row.FindControl("hdIdTask"), HiddenField).Value
            Dim strIdTicket As String = TryCast(row.FindControl("hdIdTicket"), HiddenField).Value

            Response.Redirect("ViewDetailsTask.aspx?Id=" & strIdTask & "&IdTicket=" & strIdTicket & "")

        End If

        If e.CommandName = "Update" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvTask.Rows(Index)

            Dim strIdTask As String = TryCast(row.FindControl("hdIdTask"), HiddenField).Value
            Dim strIdTicket As String = TryCast(row.FindControl("hdIdTicket"), HiddenField).Value

            Response.Redirect("InsertUpdateTask.aspx?Id=" & strIdTask & "")

        End If

        If e.CommandName = "Remove" Then

            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvTask.Rows(Index)
            Dim strIdTask As String = TryCast(row.FindControl("hdIdTask"), HiddenField).Value
            Dim strIdTicket As String = TryCast(row.FindControl("hdIdTicket"), HiddenField).Value

            hdIdTask.Value = strIdTask
            Call sb_DeleteTask()

            Response.Redirect("ViewTask.aspx")
        End If

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

    Protected Sub gvTask_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTask.PageIndexChanging
        gvTask.PageIndex = e.NewPageIndex
        Call sb_ViewTask("")
    End Sub

    Protected Sub btnNewTask_Click(sender As Object, e As EventArgs)
        Response.Redirect("InsertUpdateTask.aspx")
    End Sub

    Protected Sub btnSearchTask_Click(sender As Object, e As EventArgs)
        Call sb_ViewTask(txtTaskNumber.Text)
    End Sub

    Protected Sub btnConfirmDelete_Click(sender As Object, e As EventArgs)
        Call sb_DeleteTask()
        Response.Redirect("ViewTask.aspx")
    End Sub

    Private Sub sb_DeleteTask()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Assign_Delete_Task"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "D")
                objCommand.Parameters.AddWithValue("@TYPE_TASK", "")
                objCommand.Parameters.AddWithValue("@CATEGORY", "")
                objCommand.Parameters.AddWithValue("@DESCRIPTION", "")
                objCommand.Parameters.AddWithValue("@PRIORITY", "")
                objCommand.Parameters.AddWithValue("@DEV_USER_ID", "")
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@TASK_ID", hdIdTask.Value)
                'objCommand.Parameters.AddWithValue("@REMARKS", "")

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_TASK", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100
                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)

            End If
        End If

        connessioneDb.ChiudiDb()

    End Sub


    Protected Sub gvTask_OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTask.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim strIdTask As String = TryCast(e.Row.FindControl("hdIdTask"), HiddenField).Value
            'fn_CheckStatus(strIdTask)
            fn_CheckCompleteStatus(strIdTask)

        End If
    End Sub

    Public Function fn_CheckCompleteStatus(element As String) As Boolean

        Dim strStatus = fn_ViewTestCase(element)

        'If CStr()
        If CStr(strStatus) = "Passed" Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function fn_RetrieveTestCaseDetailsCount(element As String) As String

        Dim strTestCase = fn_RetrievetTestCase(element)

        If strTestCase <> Nothing Then
            Dim strTotalCount = fn_RetrieveCount(strTestCase)
            Return strTotalCount

        End If

        Return strTestCase
    End Function

    Public Function fn_CheckStatus(element As String) As Boolean

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If


        'strSQL = "SELECT "
        'strSQL = strSQL + "COUNT(CASE TCD.STATUS_DEFECT WHEN 'PASSED' THEN 0 END) AS PASSED, "
        'strSQL = strSQL + "COUNT(CASE TCD.STATUS_DEFECT WHEN 'FAILED' THEN 0 END) AS FAILED, "
        'strSQL = strSQL + "COUNT(*) - (COUNT(CASE TCD.STATUS_DEFECT WHEN 'PASSED' THEN 0 END) + "
        'strSQL = strSQL + "COUNT(CASE TCD.STATUS_DEFECT WHEN 'FAILED' THEN 0 END) ) AS OTHERS, "
        'strSQL = strSQL + "COUNT(*) AS ALL_COUNT "
        'strSQL = strSQL + "From TASK T "
        'strSQL = strSQL + "INNER JOIN TEST_CASES TC ON TC.ID_TASK = T.ID_TASK "
        'strSQL = strSQL + "INNER JOIN TEST_CASES_DETAILS TCD ON TCD.ID_TEST_CASES = TC.ID_TEST_CASES "
        'strSQL = strSQL + "WHERE T.ID_TASK = " & element & ""



        strSQL = "SELECT "
        strSQL = strSQL + "COUNT(CASE TCD.STATUS_DEFECT WHEN 'PASSED' THEN 0 END) AS PASSED, "
        strSQL = strSQL + "COUNT(CASE TCD.STATUS_DEFECT WHEN 'FAILED' THEN 0 END) AS FAILED, "
        strSQL = strSQL + "COUNT(*) - (COUNT(CASE TCD.STATUS_DEFECT WHEN 'PASSED' THEN 0 END) + "
        strSQL = strSQL + "COUNT(CASE TCD.STATUS_DEFECT WHEN 'FAILED' THEN 0 END) ) AS OTHERS, "
        strSQL = strSQL + "COUNT(*) AS ALL_COUNT "
        strSQL = strSQL + "From TASK T "
        strSQL = strSQL + "INNER JOIN TEST_CASES TC ON TC.ID_TASK = T.ID_TASK "
        strSQL = strSQL + "INNER JOIN TEST_CASES_DETAILS TCD ON TCD.ID_TEST_CASES = TC.ID_TEST_CASES "
        strSQL = strSQL + "WHERE TC.ID_TEST_CASES = "
        strSQL = strSQL + "(SELECT TOP 1 "
        strSQL = strSQL + "(SELECT TOP 1 ID_TEST_CASES from TEST_CASES WHERE ID_TASK = " & element & " "
        strSQL = strSQL + "ORDER BY ID_TEST_CASES DESC) ID_TEST_CASES From TASK) "


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


        Dim strFailed = Convert.ToInt32(dt.Rows(0)("FAILED"))
        Dim strPassed = Convert.ToInt32(dt.Rows(0)("PASSED"))
        Dim strOthers = Convert.ToInt32(dt.Rows(0)("OTHERS"))
        Dim strAllCount = Convert.ToInt32(dt.Rows(0)("ALL_COUNT"))


        'If strAllCount = 0 Then
        '    Return False
        'Else
        If strFailed > 0 Then
            Return False
        ElseIf strOthers > 0 Then
            Return False
        Else
            Return True
        End If

    End Function



    Public Function fn_CheckCount(element As String) As Boolean

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT "
        strSQL = strSQL + "COUNT(CASE TCD.STATUS_DEFECT WHEN 'PASSED' THEN 0 END) AS PASSED, "
        strSQL = strSQL + "COUNT(CASE TCD.STATUS_DEFECT WHEN 'FAILED' THEN 0 END) AS FAILED, "
        strSQL = strSQL + "COUNT(*) - (COUNT(CASE TCD.STATUS_DEFECT WHEN 'PASSED' THEN 0 END) + "
        strSQL = strSQL + "COUNT(CASE TCD.STATUS_DEFECT WHEN 'FAILED' THEN 0 END) ) AS OTHERS, "
        strSQL = strSQL + "COUNT(*) AS ALL_COUNT "
        strSQL = strSQL + "From TASK T "
        strSQL = strSQL + "INNER JOIN TEST_CASES TC ON TC.ID_TASK = T.ID_TASK "
        strSQL = strSQL + "INNER JOIN TEST_CASES_DETAILS TCD ON TCD.ID_TEST_CASES = TC.ID_TEST_CASES "
        strSQL = strSQL + "WHERE TC.ID_TEST_CASES = "
        strSQL = strSQL + "(SELECT TOP 1 "
        strSQL = strSQL + "(SELECT TOP 1 ID_TEST_CASES from TEST_CASES WHERE ID_TASK = " & element & " "
        strSQL = strSQL + "ORDER BY ID_TEST_CASES DESC) ID_TEST_CASES From TASK) "


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

        Dim strAllCount = Convert.ToInt32(dt.Rows(0)("ALL_COUNT"))
        Dim strFailed = Convert.ToInt32(dt.Rows(0)("FAILED"))
        Dim strPassed = Convert.ToInt32(dt.Rows(0)("PASSED"))


        If strAllCount = 0 Then
            Return False
        ElseIf strPassed > 0 And strFailed = 0 Then
            Return True
        ElseIf strFailed = 0 Then
            Return False
        Else
            Return True
        End If

    End Function



    Public Sub sb_CheckStatus(ByVal element As Object)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If


        strSQL = "   	
        SELECT 
            COUNT(CASE TCD.STATUS_DEFECT WHEN 'PASSED' THEN 0 END) AS PASSED,
	        COUNT(CASE TCD.STATUS_DEFECT WHEN 'FAILED' THEN 0 END) AS FAILED,
	        COUNT(*) - (COUNT(CASE TCD.STATUS_DEFECT WHEN 'PASSED' THEN 0 END) + 
	        COUNT(CASE TCD.STATUS_DEFECT WHEN 'FAILED' THEN 0 END) ) AS OTHERS,
	        COUNT(*) AS ALL_COUNT
	        FROM TASK T
	        INNER JOIN TEST_CASES TC ON TC.ID_TASK = T.ID_TASK
	        INNER JOIN TEST_CASES_DETAILS TCD ON TCD.ID_TEST_CASES = TC.ID_TEST_CASES
	        WHERE T.ID_TASK = '126'"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        Dim hhh1 = dt.Rows(0)("FAILED").ToString
        Dim hhh2 = dt.Rows(0)("PASSED").ToString
        Dim hhh3 = dt.Rows(0)("OTHERS").ToString
        Dim hhh4 = dt.Rows(0)("ALL_COUNT").ToString

        dbConnect.ChiudiDb()

    End Sub



    Private Function fn_ViewTestCase(strIdTestCase As String) As String

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT TOP 1 ID_TEST_CASES"
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
        strSQL = strSQL + ", ENVIROMENT"
        strSQL = strSQL + ", ISNULL(STATUS_TEST,'status not set') STATUS_TEST"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_START_TEST,109) DATE_START_TEST"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_COMPLETE_TEST,109) DATE_COMPLETE_TEST"
        strSQL = strSQL + "  FROM vs_Test_Cases WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ID_TASK =" + "'" + strIdTestCase + "'" + ""
        strSQL = strSQL + "  AND ISNULL(FLAG_COMPLETE_TEST,'N') = 'N'"
        strSQL = strSQL + " ORDER BY ID_TEST_CASES DESC"

        'If strIdTestCase <> "" Then
        '    strSQL = strSQL + " AND ID_TEST_CASES =" + "'" + strIdTestCase + "'" + ""
        '    strSQL = strSQL + " ORDER BY ID_TEST_CASES DESC"
        'Else
        '    strSQL = strSQL + " ORDER BY ID_TEST_CASES DESC"
        'End If

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


        If dt.Rows.Count > 0 Then
            Dim hhh1 = dt.Rows(0)("STATUS_TEST").ToString
            Return hhh1

        End If

        Return ""


    End Function


    Private Function fn_RetrievetTestCase(strIdTestCase As String) As String

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT TOP 1 ID_TEST_CASES"
        strSQL = strSQL + ", ID_TASK"
        strSQL = strSQL + "  FROM vs_Retrieve_TestCase_DetailsCount WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ID_TASK =" + "'" + strIdTestCase + "'" + ""
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

        dbConnect.ChiudiDb()


        If dt.Rows.Count > 0 Then
            Dim strTotalCount = dt.Rows(0)("ID_TEST_CASES").ToString
            'Dim FF = dt.Rows.Find(strTotalCount1)

            'Dim strTotalCount = dt.Rows.Count.ToString
            Return strTotalCount

        End If

        Return ""


    End Function


    Private Function fn_RetrieveCount(strIdTestCase As String) As String

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASE_DEFECTS"
        strSQL = strSQL + ", ID_TEST_CASES"
        strSQL = strSQL + "  FROM vs_Test_Cases_Details WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ID_TEST_CASES =" + "'" + strIdTestCase + "'" + ""
        'strSQL = strSQL + "  ORDER BY ID_TEST_CASES DESC"

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


        If dt.Rows.Count > 0 Then
            'Dim strTotalCount = dt.Rows(0)("ID_TEST_CASES").ToString
            'Dim FF = dt.Rows.Find(strTotalCount1)

            Dim strTotalCount = dt.Rows.Count.ToString
            Return strTotalCount

        End If

        Return ""


    End Function

End Class