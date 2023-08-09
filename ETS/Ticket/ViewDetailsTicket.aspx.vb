Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Net
Imports ETS.DataBase
Imports ETS.General

Public Class ViewDetailsTicket
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If IsInRole(Session("R"), Roll_Kind.Administrator) Or IsInRole(Session("R"), Roll_Kind.HelpDesk) Then
                If Not IsPostBack Then
                    If Session("Id") <> "" Then
                        Call sb_LoadTicketDetails()
                        Call sb_LoadTicketLog()
                        Call sb_LoadAttachments()
                    End If
                End If
            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")
            End If
        End If
    End Sub

    Private Sub sb_LoadTicketDetails()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TICKETS"
        strSQL = strSQL + ", ISSUE_TYPE"
        strSQL = strSQL + ", ISSUE_SUB_TYPE"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", STATUS_TICKET"
        strSQL = strSQL + ", REQUEST_BY"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", DATE_ASSIGNED"
        strSQL = strSQL + ", ISNULL(FLAG_RESOLVED,'N') FLAG_RESOLVED"
        strSQL = strSQL + ", ISNULL(FLAG_ASSIGNED,'N') FLAG_ASSIGNED"
        strSQL = strSQL + ", ISNULL(FLAG_CLOSE,'N') FLAG_CLOSE"
        strSQL = strSQL + ", ISNULL(FLAG_CREATE_AS_BUG,'N') FLAG_CREATE_AS_BUG"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ID_TICKETS =" & Session("Id") & ""

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

            If Not IsNothing(objDataReader.Item("ID_TICKETS")) Then
                lblDetailTicket.Text = "Details - Ticket No: " + CStr(objDataReader.Item("ID_TICKETS") & "")
            End If
            If Not IsNothing(objDataReader.Item("ISSUE_TYPE")) Then
                Call Popola_DropList_RequestType()
                DropListRequestType.Text = CStr(objDataReader.Item("ISSUE_TYPE") & "")

            End If
            If Not IsNothing(objDataReader.Item("ISSUE_SUB_TYPE")) Then
                lblCategory.Text = CStr(objDataReader.Item("ISSUE_SUB_TYPE") & "")
            End If
            If Not IsNothing(objDataReader.Item("DESCRIPTION")) Then
                txtDescription.Text = CStr(objDataReader.Item("DESCRIPTION") & "")
            End If
            If Not IsNothing(objDataReader.Item("STATUS_TICKET")) Then
                lblStatus.Text = CStr(objDataReader.Item("STATUS_TICKET") & "")
            End If
            If Not IsNothing(objDataReader.Item("PRIORITY")) Then
                DropListPriority.Text = CStr(objDataReader.Item("PRIORITY") & "")
            End If
            If Not IsNothing(objDataReader.Item("REQUEST_BY")) Then
                lblCreatedBy.Text = CStr(objDataReader.Item("REQUEST_BY") & "")
            End If
            If Not IsNothing(objDataReader.Item("ASSIGNED_TO")) Then
                lblAssignedTo.Text = CStr(objDataReader.Item("ASSIGNED_TO") & "")
            End If
            If Not IsNothing(objDataReader.Item("DATE_ASSIGNED")) Then
                lblDateAssigned.Text = CStr(objDataReader.Item("DATE_ASSIGNED") & "")
            End If

            If Not IsNothing(objDataReader.Item("FLAG_ASSIGNED")) Then
                If CStr(objDataReader.Item("FLAG_ASSIGNED")) = "Y" Then
                    txtDescription.Enabled = False
                    DropListRequestType.Enabled = False
                End If
            End If

            If Not IsNothing(objDataReader.Item("FLAG_CLOSE")) Then
                If CStr(objDataReader.Item("FLAG_CLOSE")) = "Y" Then
                    lblMessage.Text = "This ticket has been CLOSED."
                    txtDescription.Enabled = False
                    DropListRequestType.Enabled = False
                    btnUpdateTicket.Visible = False
                End If

            End If

            If Not IsNothing(objDataReader.Item("FLAG_CREATE_AS_BUG")) Then
                If CStr(objDataReader.Item("FLAG_CREATE_AS_BUG")) = "Y" Then
                    btnUpdateTicket.Visible = False
                    DropListRequestType.Enabled = False
                    txtDescription.Enabled = False
                    DropListPriority.Enabled = False
                    DropListStatus.Text = "Open"
                    lblMessage.Text = "Confirmed as Bug."
                End If
            End If

            'Work Here ---------> Done
            If Not IsNothing(objDataReader.Item("FLAG_RESOLVED")) Then
                hDFlagResolved.Value = CStr(objDataReader.Item("FLAG_RESOLVED"))

                If CStr(objDataReader.Item("FLAG_RESOLVED")) = "Y" And (CStr(objDataReader.Item("ISSUE_TYPE")) = "Bug" Or CStr(objDataReader.Item("ISSUE_TYPE")) = "Support") Then
                    DropListStatus.Text = "Close"
                    'DropListStatus.Enabled = False
                    btnUpdateTicket.Visible = True
                    lblMessage.Text = "Ticket Resolved"

                ElseIf CStr(objDataReader.Item("ISSUE_TYPE")) = "Support" Then
                    DropListStatus.Enabled = True
                Else
                    DropListStatus.Enabled = False

                End If
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

    End Sub

    Private Sub sb_LoadAttachments()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TICKET_DOCUMENT"
        strSQL = strSQL + ", FILE_PATH"
        strSQL = strSQL + ", FILE_NAME"
        strSQL = strSQL + "  FROM TICKET_DOCUMENT WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TICKETS =" & Session("Id") & ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvAttachments.DataSource = myDataSet
        gvAttachments.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_RequestType()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT DISTINCT DESCRIPTION"
        strSQL = strSQL & " FROM TICKET_TYPE WITH(NOLOCK);"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListRequestType.DataSource = objDataReader
        DropListRequestType.DataTextField = "DESCRIPTION"
        DropListRequestType.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub sb_UpdateTicket()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Ticket"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "M")
                objCommand.Parameters.AddWithValue("@ID_TICKET", Session("Id"))
                objCommand.Parameters.AddWithValue("@ISSUE_TYPE", DropListRequestType.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@PRIORITY", DropListPriority.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@ISSUE_SUB_TYPE", "")
                objCommand.Parameters.AddWithValue("@STATUS_TICKET", DropListStatus.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@DESCRIPTION", txtDescription.Text)
                objCommand.Parameters.AddWithValue("@INTERCOMMS", "")
                objCommand.Parameters.AddWithValue("@HELPDESK_REMARKS", txtHelpDeskRemarks.Text)
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_TICKETS", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                Else
                    lblMessage.Text = "Ticket Updated !!"
                End If

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub

    Private Sub sb_InsertTask()
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

                objCommand.Parameters.AddWithValue("@ID_DEV_USER", "")
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@REMARKS", "")
                objCommand.Parameters.AddWithValue("@PRIORITY", DropListPriority.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@ESTIMATED_CLOSE_DATE", "")
                objCommand.Parameters.AddWithValue("@TYPE_TASK", DropListRequestType.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@DESCRIPTION", txtDescription.Text)
                objCommand.Parameters.AddWithValue("@CATEGORY", lblCategory.Text)
                objCommand.Parameters.AddWithValue("@ID_TASK", 0)
                objCommand.Parameters.AddWithValue("@ID_TICKET", Session("Id"))

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblMessage.Text = strError
                End If

            End If

        End If
        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnUpdateTicket_Click(sender As Object, e As EventArgs)
        If DropListRequestType.Text = "Support" And DropListStatus.Text <> "Re-Open" Then
            Call sb_UpdateTicket()
        ElseIf DropListStatus.Text = "Re-Open" Then
            Call sb_ReOpenTicket()

        ElseIf hDFlagResolved.Value = "Y" Then
            Call sb_CloseTicket()
        Else
            Call sb_UpdateTicket()
            Call sb_InsertTask()
        End If
        Response.Redirect("ViewTickets.aspx")

    End Sub

    Private Sub sb_ReOpenTicket()
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String
        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Reopen_Ticket"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@ID_TICKET", Session("Id"))
                objCommand.Parameters.AddWithValue("@ISSUE", Session("issue_Type"))
                objCommand.Parameters.AddWithValue("@REMARKS", txtHelpDeskRemarks.Text)
                objCommand.Parameters.AddWithValue("@FIRSTNAME", Session("assignedTo"))
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)

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

    Protected Sub DropListRequestType_SelectedIndexChanged(sender As Object, e As EventArgs)
        btnUpdateTicket.Visible = True

        If DropListRequestType.Text = "Support" Then
            DropListStatus.Enabled = True
        Else
            DropListStatus.Enabled = False
            DropListStatus.Text = "Open"
        End If
    End Sub

    Private Sub sb_LoadTicketLog()
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TICKET_LOG"
        strSQL = strSQL + ", ID_TICKET"
        strSQL = strSQL + ", DESC_LOG"
        strSQL = strSQL + ", ID_USER"
        strSQL = strSQL + ", DATE_LOG"
        strSQL = strSQL + ", REMARKS"
        strSQL = strSQL + "  FROM TICKET_LOG WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ID_TICKET =" & Session("Id") & ""
        strSQL = strSQL + "  ORDER BY ID_TICKET_LOG DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvTicketslog.DataSource = myDataSet
        gvTicketslog.DataBind()
        dbConnect.ChiudiDb()
    End Sub

    Private Sub sb_DownloadAttachment(strFilePath As String)
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & IO.Path.GetFileName(strFilePath))
        Response.WriteFile(strFilePath)
        Response.End()

    End Sub

    Protected Sub gvAttachments_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAttachments.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gvAttachments.Rows(index)
        Dim strFilePath As String = TryCast(row.FindControl("hdFilePath"), HiddenField).Value

        If e.CommandName = "Download" Then
            sb_DownloadAttachment(strFilePath)
        End If

    End Sub

    Private Sub sb_CloseTicket()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Close_Ticket"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@REMARKS", txtHelpDeskRemarks.Text)
                objCommand.Parameters.AddWithValue("@ID_TICKETS", Session("Id"))

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblMessage.Text = strError
                End If

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub

End Class