Imports System.Data.SqlClient

Public Class InsertTester
    Inherits System.Web.UI.Page

    Dim connessioneDb As New DataBase
    Dim objCommand As New SqlCommand
    Dim mySqlAdapter As New SqlDataAdapter(objCommand)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then

            ' If fn_ValidatePageAccess() Then

            If Not IsPostBack Then

                Dim id = Request.QueryString("id")

                'Dim yyy = Session("TYPE_TASK")

                'Dim gg = Request.QueryString("TASK_ID")
                hdOpIdTask.Value = Request.QueryString("TASK_ID")


                Call sb_LoadData()
                Call Popola_DropList_Users()

                Dim gg = hdOpIdTask.Value


                If hdOpIdTask.Value <> "" Then

                    Call sb_LoadTaskDetails()
                    Call Popola_gvDeveloper()

                End If

            End If

            ' End If
        Else
            Response.Redirect("~/Login.aspx")
        End If


        'If Page.User.Identity.IsAuthenticated Then

        '    If fn_ValidatePageAccess() Then

        '        If Not IsPostBack Then
        '            'Call Popola_DropList_TaskType()
        '            'Call Popola_DropList_Users1()
        '            'Call Popola_DropList_Users2()
        '            'Call Popola_DropList_cc2()
        '            'Call Popola_DropList_cc1()
        '        End If

        '    Else
        '        FormsAuthentication.SignOut()
        '        Response.Redirect("~/Login.aspx")

        '    End If
        'End If
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
        'strSQL = strSQL + ", REMARKS"
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

            'If Not IsNothing(objDataReader.Item("DATE_ASSIGNED")) Then
            '    lblDateAssigned.Text = CStr(objDataReader.Item("DATE_ASSIGNED") & "")
            'End If

            If Not IsNothing(objDataReader.Item("STATUS_TASK")) Then
                lblStatus.Text = CStr(objDataReader.Item("STATUS_TASK") & "")
            End If

            'If Not IsNothing(objDataReader.Item("REMARK")) Then
            '    lblRemarks.Text = CStr(objDataReader.Item("REMARK") & "")
            'End If

            If Not IsNothing(objDataReader.Item("EXPECTED_START_DATE")) Then
                lblStartDate.Text = CStr(objDataReader.Item("EXPECTED_START_DATE") & "")
            End If

            If Not IsNothing(objDataReader.Item("EXPECTED_END_DATE")) Then
                lblEndDate.Text = CStr(objDataReader.Item("EXPECTED_END_DATE") & "")
            End If

            'If Not IsNothing(objDataReader.Item("FLAG_START")) Then
            '    If CStr(objDataReader.Item("FLAG_START")) = "Y" And CStr(objDataReader.Item("FLAG_COMPLETE")) = "N" Then
            '        'btnStartTask.Text = "Mark as Complete"
            '        btnStartTask.Visible = False
            '        btnMarkAsComplete.Visible = True
            '    End If
            'End If

            'If Not IsNothing(objDataReader.Item("FLAG_COMPLETE")) Then
            '    If CStr(objDataReader.Item("FLAG_COMPLETE")) = "Y" And CStr(objDataReader.Item("FLAG_START")) = "Y" Then
            '        btnStartTask.Visible = False
            '        btnMarkAsComplete.Visible = False
            '        lblMessage.Text = "Task is completed."
            '    End If
            'End If

            'If Not IsNothing(objDataReader.Item("FLAG_ASSIGNED")) Then
            '    If CStr(objDataReader.Item("FLAG_ASSIGNED")) = "N" And IsInRole(Session("R"), Roll_Kind.Administrator) Then
            '        btnAssignAsTask.Visible = True
            '        btnStartTask.Visible = False

            '    ElseIf CStr(objDataReader.Item("FLAG_ASSIGNED")) = "Y" And IsInRole(Session("R"), Roll_Kind.Administrator) Then
            '        btnAssignAsTask.Visible = False
            '        btnStartTask.Visible = False
            '        btnMarkAsComplete.Visible = False
            '        lblMessage.Text = "Task already assigned."

            '    Else
            '        btnAssignAsTask.Visible = False
            '    End If

            'End If

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

        'If DropListDev1.SelectedItem.Text <> "" And DropListDev2.SelectedItem.Text <> "" Then
        '    'Call sb_AssignTask(DropListDev1.SelectedValue, 1)
        '    'Call sb_AssignTask(DropListDev2.SelectedValue, 2)

        'ElseIf DropListDev1.SelectedItem.Text <> "" And DropListDev2.SelectedItem.Text = "" Then
        '    'Call sb_AssignTask(DropListDev1.SelectedValue, 1)

        'ElseIf DropListDev2.SelectedItem.Text <> "" And DropListDev1.SelectedItem.Text = "" Then
        '    'Call sb_AssignTask(DropListDev2.SelectedValue, 1)

        'End If

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
                'lblEditTestCase.Text = CStr(objDataReader.Item("ID_TEST_CASES") & "") & " - " & CStr(objDataReader.Item("DESCRIPTION") & "")
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
        'popExtenderRegistry.Show()
    End Sub

    Protected Sub gvDeveloper_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)

        ' gvRegistry.PageIndex = e.NewPageIndex
        Call Popola_gvRegistry()

    End Sub

    Protected Sub gvDeveloper_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)


        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        'Dim row As GridViewRow = gvRegistry.Rows(index)
        Dim item As New ListItem()

        If e.CommandName = "Seleziona" Then


            '  gvRegistry.SelectedIndex = index
            'hdRg.Value = row.Cells(0).Text

            'txtDescConsegnee.Text = row.Cells(1).Text
            'txtNotify.Text = row.Cells(1).Text
            'txtAddress1Notify.Text = row.Cells(2).Text
            'lblNotifyDescAddress1.Text = row.Cells(2).Text

            'hdConsegnee2.Value = row.Cells(5).Text
            'hdConsegnee3.Value = row.Cells(6).Text

            '    udpOutterUpdatePanel.Update()
            ''    upGeneral.Update()
            '   upGeneralData.Update()

        ElseIf e.CommandName = "DeSeleziona" Then
            '     gvRegistry.SelectedIndex = -1
            'hdRg.Value = ""

            'txtDescConsegnee.Text = ""

            'txtNotify.Text = ""
            'txtAddress1Notify.Text = ""
            'lblNotifyDescAddress1.Text = ""
            'hdConsegnee2.Value = ""
            'hdConsegnee3.Value = ""

            '  upGeneral.Update()
            '  udpOutterUpdatePanel.Update()

        End If

        ' popExtenderRegistry.Show()
    End Sub


    Protected Sub Popola_gvRegistry()


        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        ' If txtSearch.Text <> "" Then
        If "ll" <> "" Then

            'Dim strSQL As String = "SELECT ID_REGISTRY,NAME,ADDRESS1,ADDRESS2,ADDRESS3,CONTACT_PERSON,EMAIL,TELEFONE FROM REGISTRY  WHERE NAME LIKE '" & txtSearch.Text & "%'"
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

            '   gvRegistry.DataSource = myDataSet
            '   gvRegistry.DataBind()


            'If gvRegistry.Rows.Count = 0 Then

            '    lblRegistryNonDataFound.Text = "No data found"
            'Else
            '    lblRegistryNonDataFound.Text = ""
            '    gvRegistry.Visible = True
            'End If

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

        'strSQL = " SELECT 0 ID_USERS,'' USERS"
        'strSQL = strSQL & "  UNION"
        strSQL = strSQL & "  SELECT U.ID_USERS"
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

        'strSQL = " SELECT 0 ID_USERS,'' USERS"
        'strSQL = strSQL & "  UNION"
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

        'Call sb_Delete_Developers(hdOpIdTask.Value)

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

        Dim ff = e.Row.RowIndex
        'Dim index As Integer = Convert.ToInt32(e.Row.)
        'Dim gvRow As GridViewRow = gvRequestCompanyAgent.Rows(index)
        'Dim row As GridViewRow = gvRequestCompanyAgent.Rows(index)

        'gvRequestCompanyAgent.SelectedIndex = index

        'hdIdC.Value = row.Cells(1).Text


        'If CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png" Then
        '    CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkDawn.png"

        'Else
        '    CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png"

        'End If
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim g As String = TryCast(e.Row.FindControl("hdIdDv"), HiddenField).Value
            'Dim t = (CType(sender, GridView)).DataKeys(e.Row.RowIndex).Value.ToString()
            Dim strTestCase = fn_Selected_Users_Assigned(hdOpIdTask.Value, g)




            If strTestCase Then
                CType(e.Row.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png"

            End If
            ' Dim ffff = gvRequestCompanyAgent.Rows(e.Row.RowIndex - 1)

            'For Each row As GridViewRow In gvRequestCompanyAgent.Rows

            '    Dim strIdQR As String = TryCast(row.FindControl("hdIdDv"), HiddenField).Value

            '    Dim strTestCase = fn_Selected_Users_Assigned(hdOpIdTask.Value, strIdQR)




            '    If strTestCase Then
            '        CType(row.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png"
            '        'Dim strIdQR As String = TryCast(row.FindControl("hdIdDv"), HiddenField).Value
            '        'Call sb_Insert_New_Developer(strIdQR)

            '    End If

            'Next

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
                    'Dim checkStarted = userData.Rows(0)("FLAG_START").ToString
                    'If checkStarted <> Nothing And checkStarted = "Y" Then
                    'Response.Write("<script language=""javascript"">alert('Cannot remove developer that has started task!');</script>")
                    lblMessageText.Text = "Cannot remove tester that has started task!"

                    'Else
                    ' lblMessageText.Text = ""
                    ' CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkDawn.png"
                    'End If
                Else
                    lblMessageText.Text = ""
                    CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkDawn.png"

                End If

                'CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkDawn.png"

            Else
                CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png"
                lblMessageText.Text = ""
            End If

            'If CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png" Then
            '    CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkDawn.png"

            'Else
            '    CType(gvRow.Controls(0).Controls(0), ImageButton).ImageUrl = "~/img/checkUp.png"

            'End If

        End If

    End Sub


    Private Sub sb_InsertQuickRemarks()

        For Each row As GridViewRow In gvRequestCompanyAgent.Rows

            If CType(row.Controls(0).Controls(0), ImageButton).ImageUrl = "~/Images/checkUp.png" Then
                Dim strIdQR As Integer = TryCast(row.FindControl("hdIdQR"), HiddenField).Value
                'Call sb_Insert_New_Developer(strIdQR)

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
                'objCommand.Parameters.AddWithValue("@USERNAME", Page.User.Identity.Name)

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
        'Dim mySqlAdapter As New SqlDataAdapter(objCommand)

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

        'Dim objDataReader As SqlDataReader
        'objDataReader = objCommand.ExecuteReader()

        'objDataReader.Close()
        'connessioneDb.ChiudiDb()

        'Dim myDataSet As DataSet = New DataSet()
        'mySqlAdapter.Fill(myDataSet)

        'Dim dt As DataTable = New DataTable()
        'mySqlAdapter.Fill(dt)

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
        'Dim mySqlAdapter As New SqlDataAdapter(objCommand)

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