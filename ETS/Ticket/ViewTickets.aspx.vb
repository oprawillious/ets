Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General

Public Class ViewTickets
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack Then
                Call sb_ViewTickets()

                If IsInRole(Session("R"), Roll_Kind.Administrator) Then
                    gvTickets.Columns(15).Visible = True
                    gvTickets.Columns(16).Visible = True

                ElseIf IsInRole(Session("R"), Roll_Kind.HelpDesk) Then
                    gvTickets.Columns(15).Visible = True
                    gvTickets.Columns(16).Visible = False

                Else
                    gvTickets.Columns(15).Visible = False
                    gvTickets.Columns(16).Visible = False

                End If

            End If
        End If
    End Sub

    Private Sub sb_SearchTickets(strIdTicket As String, strStatusTicket As String)

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
        strSQL = strSQL + ", REQUEST_FROM"
        strSQL = strSQL + ", REQUEST_BY"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", ISNULL(FLAG_RESOLVED,'N') FLAG_RESOLVED"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"

        If strIdTicket <> "" Then
            strSQL = strSQL + " AND ID_TICKETS =" + "'" + strIdTicket + "'" + ""
        End If

        If strStatusTicket <> "" Then
            strSQL = strSQL + " AND STATUS_TICKET =" + "'" + strStatusTicket + "'" + ""
        End If

        If Not IsInRole(Session("R"), Roll_Kind.Administrator) And Not IsInRole(Session("R"), Roll_Kind.HelpDesk) Then
            strSQL = strSQL + " AND REQUEST_BY =" + "'" + Page.User.Identity.Name + "'" + ""
        End If

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

    Private Sub sb_ViewTickets()

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
        strSQL = strSQL + ", REQUEST_FROM"
        strSQL = strSQL + ", REMARKS_HELPDESK"
        strSQL = strSQL + ", REQUEST_BY"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", ISNULL(FLAG_RESOLVED,'N') FLAG_RESOLVED"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"
        strSQL = strSQL + "  AND ISNULL(FLAG_CLOSE,'N') = 'N'"

        If Not IsInRole(Session("R"), Roll_Kind.Administrator) And Not IsInRole(Session("R"), Roll_Kind.HelpDesk) Then
            strSQL = strSQL + " AND REQUEST_BY =" + "'" + Page.User.Identity.Name + "'" + ""
            strSQL = strSQL + " ORDER BY ID_TICKETS DESC"
        Else
            strSQL = strSQL + "  ORDER BY ID_TICKETS DESC"
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
        gvTickets.DataSource = myDataSet
        gvTickets.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub btnSearchTicket_Click(sender As Object, e As EventArgs)
        If txtTicketNumber.Text <> "" Or DropListStatusTicket.Text <> "" Then
            Call sb_SearchTickets(txtTicketNumber.Text, DropListStatusTicket.Text)
        Else
            Call sb_ViewTickets()
        End If

    End Sub

    Protected Sub gvTickets_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTickets.RowCommand

        If e.CommandName = "Modifica" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvTickets.Rows(Index)
            Dim strIdTicket As String = TryCast(row.FindControl("hdIdTicket"), HiddenField).Value
            Dim strAssignedTo As String = TryCast(row.FindControl("hdAssignedTo"), HiddenField).Value
            Dim strIssueType As String = TryCast(row.FindControl("hdIssueType"), HiddenField).Value

            Session("Id") = strIdTicket
            Session("assignedTo") = strAssignedTo
            Session("issue_Type") = strIssueType

            Response.Redirect("ViewDetailsTicket.aspx")
        End If

        If e.CommandName = "Remove" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvTickets.Rows(Index)
            Dim strIdTicket As String = TryCast(row.FindControl("hdIdTicket"), HiddenField).Value
            Call sb_DeleteTicket(strIdTicket)
            Response.Redirect("ViewTickets.aspx")
        End If

    End Sub

    Protected Sub btnNewTicket_Click(sender As Object, e As EventArgs)
        Response.Redirect("InsertUpdateTicket.aspx")
    End Sub

    Protected Sub gvTickets_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTickets.PageIndexChanging
        gvTickets.PageIndex = e.NewPageIndex

        If txtTicketNumber.Text <> "" Or DropListStatusTicket.Text <> "" Then
            Call sb_SearchTickets(txtTicketNumber.Text, DropListStatusTicket.Text)
        Else
            Call sb_ViewTickets()
        End If
        'e.NewPageIndex = Int32.MaxValue

    End Sub

    Public Function fn_CheckStatus(ByVal element As Object) As Boolean

        If CStr(element) = "Y" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub sb_DeleteTicket(strIdTicket As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Ticket"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "D")
                objCommand.Parameters.AddWithValue("@ID_TICKET", CInt(strIdTicket))
                objCommand.Parameters.AddWithValue("@STATUS_TICKET", "")
                objCommand.Parameters.AddWithValue("@ISSUE_TYPE", "")
                objCommand.Parameters.AddWithValue("@PRIORITY", "")
                objCommand.Parameters.AddWithValue("@ISSUE_SUB_TYPE", "")
                objCommand.Parameters.AddWithValue("@DESCRIPTION", "")
                objCommand.Parameters.AddWithValue("@INTERCOMMS", "")
                objCommand.Parameters.AddWithValue("@HELPDESK_REMARKS", "")
                objCommand.Parameters.AddWithValue("@REQUEST_FROM", "")
                objCommand.Parameters.AddWithValue("@USER_ID", "")

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_TICKETS", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteReader()

            End If
        End If

        connessioneDb.ChiudiDb()
    End Sub

End Class