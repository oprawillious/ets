Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Web.Configuration
Imports ETS.General
Imports ETS.DataBase
Imports System.Globalization.CultureInfo
Public Class NewTask
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then

            If fn_ValidatePageAccess() Then

                If Not IsPostBack Then

                    hdIdTask.Value = Request.QueryString("Id")


                    Call Popola_DropList_TaskType()
                    Call Popola_DropList_Users1()
                    Call Popola_DropList_Users2()
                    Call Popola_DropList_cc2()
                    Call Popola_DropList_cc1()


                    If hdIdTask.Value <> "" Then

                        Call Popola_DropList_TaskCategory()

                        Call sb_LoadTaskDetails()
                        'Call Popola_gvDeveloper()

                    End If
                End If

            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")

            End If
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

        'DropListDev1.DataSource = objDataReader
        'DropListDev1.DataValueField = "ID_USERS"
        'DropListDev1.DataTextField = "USERS"
        'DropListDev1.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub


    Private Sub Popola_DropList_cc1()

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

        DropDowncc1.DataSource = objDataReader
        DropDowncc1.DataValueField = "ID_USERS"
        DropDowncc1.DataTextField = "USERS"
        DropDowncc1.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_cc2()

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

        DropDownListcc2.DataSource = objDataReader
        DropDownListcc2.DataValueField = "ID_USERS"
        DropDownListcc2.DataTextField = "USERS"
        DropDownListcc2.DataBind()

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

        'DropListDev2.DataSource = objDataReader
        'DropListDev2.DataValueField = "ID_USERS"
        'DropListDev2.DataTextField = "USERS"
        'DropListDev2.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_TaskType()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT '' DESCRIPTION"
        strSQL = strSQL & " UNION"
        strSQL = strSQL & " SELECT DISTINCT DESCRIPTION"
        strSQL = strSQL & " FROM TICKET_TYPE WITH(NOLOCK)"
        strSQL = strSQL & " WHERE DESCRIPTION NOT IN ('Support');"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListTaskType.DataSource = objDataReader
        DropListTaskType.DataTextField = "DESCRIPTION"
        DropListTaskType.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_TaskCategory()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT DISTINCT SUB_DESCRIPTION"
        strSQL = strSQL & " ,ID_TICKET_TYPE"
        strSQL = strSQL & " FROM TICKET_TYPE WITH(NOLOCK)"

        If Not hdIdTask.Value <> Nothing Then

            strSQL = strSQL & " WHERE DESCRIPTION =" + "'" + DropListTaskType.SelectedItem.Text + "'" + ""
        Else
            Dim strCategory = fn_GetCategory(hdIdTask.Value)
            strSQL = strSQL & " WHERE DESCRIPTION =" + "'" + strCategory + "'" + ""

        End If
        strSQL = strSQL + "  ORDER BY SUB_DESCRIPTION DESC"
        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListCategory.DataSource = objDataReader
        DropListCategory.DataValueField = "ID_TICKET_TYPE"
        DropListCategory.DataTextField = "SUB_DESCRIPTION"
        DropListCategory.DataBind()

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

    Protected Sub DropListTaskType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Call Popola_DropList_TaskCategory()
    End Sub
    Private Sub btnCreateTask_Click(sender As Object, e As EventArgs)
        'Protected Sub btnCreateTask_Click(sender As Object, e As EventArgs)

        'If DropListDev1.SelectedItem.Text <> "" And DropListDev2.SelectedItem.Text <> "" Then
        '    Call sb_InsertNewTask(DropListDev1.SelectedValue)
        '    Call sb_InsertNewTask(DropListDev2.SelectedValue)

        'ElseIf DropListDev1.SelectedItem.Text <> "" And DropListDev2.SelectedItem.Text = "" Then
        '    Call sb_InsertNewTask(DropListDev1.SelectedValue)

        'Else
        '    Call sb_InsertNewTask(DropListDev2.SelectedValue)

        'End If

        If hdIdTask.Value <> Nothing Then
            Call sb_UpdateTask(hdIdTask.Value)
        Else
            Call sb_InsertNewTask("")

        End If


        'Session("TYPE_TASK") = DropListTaskType.SelectedItem.Text
        'Session("CATEGORY") = DropListCategory.SelectedItem.Text
        'Session("DESCRIPTION") = txtDescription.Text
        'Session("PRIORITY") = DropListPriority.SelectedItem.Text
        'Session("EXPECTED_START_DATE") = txtExpectedStartDate.Text
        'Session("EXPECTED_END_DATE") = txtExpectedEndDate.Text
        'Session("CC1") = DropDowncc1.SelectedValue
        'Session("CC2") = DropDownListcc2.SelectedValue
        'Session("USER_ID") = Page.User.Identity.Name
        'Session("TASK_ID") = hdIdTask.Value
        'Session("REMARKS") = txtRemarks.Text

        Response.Redirect(String.Format("InsertDeveloper.aspx?TASK_ID={0}", hdIdTask.Value))


        'Response.Redirect("InsertDeveloper.aspx")

    End Sub

    Private Sub sb_InsertNewTask(strIdUser As String)

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

                objCommand.Parameters.AddWithValue("@OP", "I")
                objCommand.Parameters.AddWithValue("@TYPE_TASK", DropListTaskType.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@CATEGORY", DropListCategory.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@DESCRIPTION", txtDescription.Text)
                objCommand.Parameters.AddWithValue("@PRIORITY", DropListPriority.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@EXPECTED_START_DATE", txtExpectedStartDate.Text)
                objCommand.Parameters.AddWithValue("@EXPECTED_END_DATE", txtExpectedEndDate.Text)
                objCommand.Parameters.AddWithValue("@DEV_USER_ID", "")
                objCommand.Parameters.AddWithValue("@CC1", DropDowncc1.SelectedValue)
                objCommand.Parameters.AddWithValue("@CC2", DropDownListcc2.SelectedValue)
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@TASK_ID", "")
                'objCommand.Parameters.AddWithValue("@REMARKS", txtRemarks.Text)

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
                hdIdTask.Value = CStr(objCommand.Parameters("@ID_TASK").Value)

                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                Else
                    lblMessage.Text = "Task Created Successfully."
                End If

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub


    Private Sub sb_UpdateTask(strIdTask As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Update_Task"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "U")
                objCommand.Parameters.AddWithValue("@TYPE_TASK", DropListTaskType.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@CATEGORY", DropListCategory.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@DESCRIPTION", txtDescription.Text)
                objCommand.Parameters.AddWithValue("@PRIORITY", DropListPriority.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@EXPECTED_START_DATE", txtExpectedStartDate.Text)
                objCommand.Parameters.AddWithValue("@EXPECTED_END_DATE", txtExpectedEndDate.Text)
                objCommand.Parameters.AddWithValue("@DEV_USER_ID", "")
                objCommand.Parameters.AddWithValue("@CC1", DropDowncc1.SelectedValue)
                objCommand.Parameters.AddWithValue("@CC2", DropDownListcc2.SelectedValue)
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@TASK_ID", strIdTask)
                'objCommand.Parameters.AddWithValue("@REMARKS", txtRemarks.Text)

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
                'hdIdTask.Value = CStr(objCommand.Parameters("@ID_TASK").Value)

                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                Else
                    lblMessage.Text = "Task Updated Successfully."
                End If

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub


    Protected Sub txtExpectedEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtExpectedEndDate.TextChanged

        Dim strStartDate As DateTime
        Dim strEndDate As DateTime
        If txtExpectedStartDate.Text <> Nothing Then
            strStartDate = DateTime.ParseExact(txtExpectedStartDate.Text, "MM/dd/yyyy", CurrentUICulture.DateTimeFormat)
        End If

        If txtExpectedStartDate.Text <> Nothing Then
            strEndDate = DateTime.ParseExact(txtExpectedEndDate.Text, "MM/dd/yyyy", CurrentUICulture.DateTimeFormat)
        End If

        If txtExpectedStartDate.Text = "" Then
            ImageButton2.Enabled = False
        Else
            ImageButton2.Enabled = True
        End If

        If (strStartDate > strEndDate) Then
            dateError.Text = "Error! Incorrect end date."
            txtExpectedStartDate.Text = ""
            txtExpectedEndDate.Text = ""
        Else
            dateError.Text = ""
        End If

    End Sub

    Protected Sub txtExpectedStartDate_TextChanged(sender As Object, e As EventArgs) Handles txtExpectedStartDate.TextChanged
        txtExpectedEndDate.Text = ""
    End Sub

    Protected Sub btnCreateTask_Click1(sender As Object, e As EventArgs)
        'Call sb_InsertNewTask("")

        If hdIdTask.Value <> Nothing Then
            Call sb_UpdateTask(hdIdTask.Value)
        Else
            Call sb_InsertNewTask("")

        End If

        Response.Redirect(String.Format("InsertDeveloper.aspx?TASK_ID={0}", hdIdTask.Value))
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
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", TYPE_TASK"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", REMARK"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_START_DATE) EXPECTED_START_DATE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_END_DATE) EXPECTED_END_DATE"
        strSQL = strSQL + ", ISNULL(FLAG_START,'N') FLAG_START"
        strSQL = strSQL + ", ISNULL(FLAG_COMPLETE,'N') FLAG_COMPLETE"
        strSQL = strSQL + ", ISNULL(FLAG_ISSUES,'N') FLAG_ISSUES"
        strSQL = strSQL + ", ISNULL(FLAG_ASSIGNED,'N') FLAG_ASSIGNED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + "  FROM vs_Task_Assigned WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TASK =" & hdIdTask.Value & ""

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

            'If Not IsNothing(objDataReader.Item("ID_TASK")) Then
            '    hdIdTask.Value = CStr(objDataReader.Item("ID_TASK") & "")
            '    DropListTaskType.Text = "Details - Task No: " + CStr(objDataReader.Item("ID_TASK") & "")
            'End If

            If Not IsNothing(objDataReader.Item("PRIORITY")) Then
                DropListPriority.SelectedValue = CStr(objDataReader.Item("PRIORITY") & "")
            End If

            If Not IsNothing(objDataReader.Item("CATEGORY")) Then
                DropListCategory.SelectedValue = CStr(objDataReader.Item("CATEGORY") & "")
            End If

            If Not IsNothing(objDataReader.Item("TASK_DESCRIPTION")) Then
                txtDescription.Text = CStr(objDataReader.Item("TASK_DESCRIPTION") & "")
            End If

            If Not IsNothing(objDataReader.Item("TYPE_TASK")) Then
                DropListTaskType.SelectedValue = CStr(objDataReader.Item("TYPE_TASK") & "")
            End If

            If Not IsNothing(objDataReader.Item("EXPECTED_START_DATE")) Then
                txtExpectedStartDate.Text = CStr(objDataReader.Item("EXPECTED_START_DATE") & "")
            End If

            If Not IsNothing(objDataReader.Item("EXPECTED_END_DATE")) Then
                txtExpectedEndDate.Text = CStr(objDataReader.Item("EXPECTED_END_DATE") & "")
            End If

            'If Not IsNothing(objDataReader.Item("REMARK")) Then
            '    lblRemarks.Text = CStr(objDataReader.Item("REMARK") & "")
            'End If

            'If Not IsNothing(objDataReader.Item("EXPECTED_START_DATE")) Then
            '    lblStartDate.Text = CStr(objDataReader.Item("EXPECTED_START_DATE") & "")
            'End If

            'If Not IsNothing(objDataReader.Item("EXPECTED_END_DATE")) Then
            '    lblEndDate.Text = CStr(objDataReader.Item("EXPECTED_END_DATE") & "")
            'End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

    End Sub


    Public Function fn_GetCategory(element As String) As String

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT TOP 1 "
        strSQL = strSQL + "TYPE_TASK "
        strSQL = strSQL + "FROM vs_Task "
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

        Dim strCategory = dt.Rows(0)("TYPE_TASK")

        Return strCategory

    End Function

    'Protected Sub btntask_Click(sender As Object, e As EventArgs)
    '    Call sb_InsertNewTask("")

    '    Response.Redirect(String.Format("InsertDeveloper.aspx?TASK_ID={0}", hdIdTask.Value))
    'End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs)
    '    Call sb_InsertNewTask("")

    '    Response.Redirect(String.Format("InsertDeveloper.aspx?TASK_ID={0}", hdIdTask.Value))
    'End Sub
End Class