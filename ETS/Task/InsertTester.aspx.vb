﻿Imports System.Data.SqlClient

Public Class InsertTester
    Inherits System.Web.UI.Page

    Dim connessioneDb As New DataBase
    Dim objCommand As New SqlCommand
    Dim mySqlAdapter As New SqlDataAdapter(objCommand)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then


            If Not IsPostBack Then

                Dim id = Request.QueryString("id")


                hdOpIdTask.Value = Request.QueryString("TASK_ID")


                Call sb_LoadData()
                Call Popola_DropList_Users()

                Dim gg = hdOpIdTask.Value


                If hdOpIdTask.Value <> "" Then

                    Call sb_LoadTaskDetails()
                    Call Popola_gvDeveloper()

                End If

            End If

        Else
            Response.Redirect("~/Login.aspx")
        End If


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
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_START_DATE) EXPECTED_START_DATE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_END_DATE) EXPECTED_END_DATE"
        strSQL = strSQL + ", ISNULL(FLAG_START,'N') FLAG_START"
        strSQL = strSQL + ", ISNULL(FLAG_COMPLETE,'N') FLAG_COMPLETE"
        strSQL = strSQL + ", ISNULL(FLAG_ISSUES,'N') FLAG_ISSUES"
        strSQL = strSQL + ", ISNULL(FLAG_ASSIGNED,'N') FLAG_ASSIGNED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + "  FROM vs_Task_Dropdown WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TASK =" & hdOpIdTask.Value & ""

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


            If Not IsNothing(objDataReader.Item("STATUS_TASK")) Then
                lblStatus.Text = CStr(objDataReader.Item("STATUS_TASK") & "")
            End If


            If Not IsNothing(objDataReader.Item("EXPECTED_START_DATE")) Then
                lblStartDate.Text = CStr(objDataReader.Item("EXPECTED_START_DATE") & "")
            End If

            If Not IsNothing(objDataReader.Item("EXPECTED_END_DATE")) Then
                lblEndDate.Text = CStr(objDataReader.Item("EXPECTED_END_DATE") & "")
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

    End Sub


    Public Shared Function IsInRole(Role As Integer, IsRole As Integer) As Boolean

        Dim blReturn As Boolean = False

        If Role = IsRole Then
            blReturn = True
        End If

        Return blReturn

    End Function


    Protected Sub btnAssignTask_Click(sender As Object, e As EventArgs)

        Response.Redirect("ViewTask.aspx")
    End Sub


    Private Sub sb_LoadTaskDetails1()

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
        strSQL = strSQL + "  WHERE ID_TEST_CASES = " & hdOpIdTask.Value & ""

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
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

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


    Protected Sub btn_CheckDeveloper_OnClick(sender As Object, e As ImageClickEventArgs)
        Call Popola_gvRegistry()
    End Sub

    Protected Sub gvDeveloper_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)

        Call Popola_gvRegistry()

    End Sub

    Protected Sub gvDeveloper_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)


        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim item As New ListItem()

        If e.CommandName = "Seleziona" Then

        ElseIf e.CommandName = "DeSeleziona" Then

        End If

    End Sub


    Protected Sub Popola_gvRegistry()


        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        If "ll" <> "" Then

            Dim strSQL As String = "SELECT ID_REGISTRY,NAME,ADDRESS1,ADDRESS2,ADDRESS3,CONTACT_PERSON,EMAIL,TELEFONE FROM REGISTRY  WHERE NAME LIKE '" & "a" & "%'"

            Dim objCommand As New SqlCommand
            objCommand.CommandText = strSQL
            objCommand.CommandType = CommandType.Text
            objCommand.Connection = connessioneDb.Connessione

            Dim mySqlAdapter As New SqlDataAdapter(objCommand)
            Dim myDataSet As New DataSet()
            mySqlAdapter.Fill(myDataSet)

            mySqlAdapter.SelectCommand = New SqlCommand()
            mySqlAdapter.SelectCommand.Connection = connessioneDb.Connessione
            mySqlAdapter.SelectCommand.CommandText = strSQL
            mySqlAdapter.SelectCommand.CommandType = CommandType.Text

            mySqlAdapter.Fill(myDataSet, "REGISTRY")

            mySqlAdapter = Nothing
            objCommand = Nothing
            connessioneDb.ChiudiDb()
        End If


    End Sub


    Protected Sub Popola_gvDeveloper()
        Call Popola_DropList_Users()
    End Sub


    Private Sub Popola_DropList_Users1()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT U.ID_USERS"
        strSQL = strSQL & ", (U.FIRST_NAME +' - '+R.ROLE_DESCRIPTION) USERS"
        strSQL = strSQL & "  FROM USERS U WITH(NOLOCK)"
        strSQL = strSQL & ", ROLES R WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1=1"
        strSQL = strSQL & "  AND U.ID_ROLE = R.ID_ROLES"
        strSQL = strSQL & "  AND R.ROLE_DESCRIPTION = 'Developer'"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()


        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub


    Protected Sub gvRequestCompanyAgent_OnRowCommand1(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)

        Dim item As New ListItem()
        Dim objGeneral As New General

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gvRequestCompanyAgent.Rows(index)
        Dim StrRequestCompanyAgentId As String = TryCast(row.FindControl("RequestCompanyAgentId"), HiddenField).Value

        If e.CommandName = "Select" Then

            Dim lblCountry As ImageButton = CType(row.Cells(0).Controls(0), ImageButton)
            lblCountry.ImageUrl = "~/img/checkUp.png"

            Dim i As Integer = 0

            Do While (i < gvRequestCompanyAgent.Rows.Count)

                If index <> i Then
                    Dim gvRowSelected As GridViewRow = gvRequestCompanyAgent.Rows(i)
                    CType(gvRowSelected.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkDawn.png"
                End If

                i += 1
            Loop

        End If

    End Sub


    Private Sub sb_LoadData()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "  SELECT U.ID_USERS"
        strSQL = strSQL & ", (U.FIRST_NAME +' - '+R.ROLE_DESCRIPTION) USERS"
        strSQL = strSQL & "  FROM USERS U WITH(NOLOCK)"
        strSQL = strSQL & ", ROLES R WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1=1"
        strSQL = strSQL & "  AND U.ID_ROLE = R.ID_ROLES"
        strSQL = strSQL & "  AND R.ID_ROLES = 3"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim ds As DataSet = New DataSet()
        mySqlAdapter.Fill(ds)

        gvRequestCompanyAgent.DataSource = ds
        gvRequestCompanyAgent.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_Users()

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

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub btnAddTester_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnAddDeveloper_Click(sender As Object, e As EventArgs)


        Dim strUserIds = "("

        For Each row As GridViewRow In gvRequestCompanyAgent.Rows

            If CType(row.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png" Then
                Dim strIdQR As String = TryCast(row.FindControl("hdIdDv"), HiddenField).Value

                Dim userData As DataTable = fn_Get_Selected_User(hdOpIdTask.Value, strIdQR)

                If userData.Rows.Count > 0 Then

                Else
                    Call sb_Insert_New_Developer(strIdQR)

                End If


                strUserIds = strUserIds + strIdQR + ","

            End If

        Next

        strUserIds = strUserIds + "0)"

        Call sb_Delete_Other_User(hdOpIdTask.Value, strUserIds)

        Response.Redirect(String.Format("ViewTask.aspx?TASK_ID={0}", hdOpIdTask.Value))

    End Sub

    Protected Sub gvRequestCompanyAgent_OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvRequestCompanyAgent.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim g As String = TryCast(e.Row.FindControl("hdIdDv"), HiddenField).Value
            Dim strTestCase = fn_Selected_Users_Assigned(hdOpIdTask.Value, g)




            If strTestCase Then
                CType(e.Row.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png"

            End If


        End If
    End Sub


    Protected Sub gvRequestCompanyAgent_OnRowCommand(sender As Object, e As GridViewCommandEventArgs)

        If e.CommandName = "Select" Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim gvRow As GridViewRow = gvRequestCompanyAgent.Rows(index)
            Dim row As GridViewRow = gvRequestCompanyAgent.Rows(index)

            gvRequestCompanyAgent.SelectedIndex = index

            hdIdC.Value = row.Cells(1).Text

            If CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png" Then

                Dim strIdQR As String = TryCast(row.FindControl("hdIdDv"), HiddenField).Value
                Dim userData As DataTable = fn_Get_Selected_User(hdOpIdTask.Value, strIdQR)
                If userData.Rows.Count > 0 Then

                    lblMessageText.Text = "Cannot remove tester that has started task!"
                    
                Else
                    lblMessageText.Text = ""
                    CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkDawn.png"

                End If


            Else
                CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png"
                lblMessageText.Text = ""
            End If


        End If

    End Sub


    Private Sub sb_InsertQuickRemarks()

        For Each row As GridViewRow In gvRequestCompanyAgent.Rows

            If CType(row.Controls(0).Controls(0), ImageButton).ImageUrl = "~/Images/checkUp.png" Then
                Dim strIdQR As Integer = TryCast(row.FindControl("hdIdQR"), HiddenField).Value

            End If

        Next

    End Sub


    Private Sub sb_Insert_New_Developer(strIdQR As Integer)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        hdOpIdTask.Value = Request.QueryString("TASK_ID")

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Assigned_Tester"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@ID_USER", strIdQR)
                objCommand.Parameters.AddWithValue("@ID_TASK", hdOpIdTask.Value)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                End If

            End If
        End If
        connessioneDb.ChiudiDb()
    End Sub


    Private Function fn_Selected_Users_Assigned(strIdT As Integer, strIdU As Integer) As Boolean

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand

        Dim strSQL As String

        strSQL = " SELECT ID_USER"
        strSQL = strSQL & "  ID_TASK"
        strSQL = strSQL & "  FROM TEST_TASK_USER_ASSIGN WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1=1"
        strSQL = strSQL & "  AND ID_TASK =" & strIdT & ""
        strSQL = strSQL & "  AND ID_USER =" & strIdU & ""

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

        If dt.Rows.Count > 0 Then
            Return True
        End If
        Return False

    End Function

    Private Sub sb_Delete_Developers(strIdQR As Integer)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        hdOpIdTask.Value = Request.QueryString("TASK_ID")

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Delete_Tester"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@ID_TASK", strIdQR)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                End If

            End If
        End If
        connessioneDb.ChiudiDb()
    End Sub


    Private Function fn_Get_Selected_User(strIdT As Integer, strIdU As Integer)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand

        Dim strSQL As String

        strSQL = " SELECT ID_USER"
        strSQL = strSQL & "  ,ID_TASK"
        strSQL = strSQL & "  FROM vs_Test_User WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1=1"
        strSQL = strSQL & "  AND ID_TASK =" & strIdT & ""
        strSQL = strSQL & "  AND ID_USER =" & strIdU & ""

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


    Private Sub sb_Delete_Other_User(strIdT As Integer, strCount As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand

        Dim strSQL As String

        strSQL = "DELETE FROM"
        strSQL = strSQL & "  TEST_TASK_USER_ASSIGN"
        strSQL = strSQL & "  WHERE ID_USER"
        strSQL = strSQL & "  NOT IN " & strCount & ""
        strSQL = strSQL & "  AND ID_TASK = " & strIdT & ""

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

    End Sub


End Class