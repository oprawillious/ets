Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO

Public Class ReportTickets
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If fn_ValidatePageAccess() Then
                If Not Page.IsPostBack Then
                    Call Popola_DropList_Type()
                    Call Popola_DropList_Category()
                    Call Popola_DropList_RequestedBy()
                    Call Popola_DropList_AssignedTo()
                End If

                Excelexport.Visible = False
                gvReportAllTickets.DataSource = Nothing
                Me.Excelexport.Attributes.Add("onmouseout", "javascript:this.src='/img/excel.png'")
                Me.Excelexport.Attributes.Add("onmouseover", "javascript:this.src='/img/excel_over.png'")
            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")
            End If
        End If

    End Sub

    Private Sub Popola_DropList_Type()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT '' [DESCRIPTION]"
        strSQL = strSQL & " UNION "
        strSQL = strSQL & " SELECT DISTINCT "
        strSQL = strSQL & " DESCRIPTION  "
        strSQL = strSQL & " FROM TICKET_TYPE WITH(NOLOCK);"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListType.DataSource = objDataReader
        DropListType.DataTextField = "DESCRIPTION"
        DropListType.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_Category()
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT '' [SUB_DESCRIPTION]"
        strSQL = strSQL & " UNION "
        strSQL = strSQL & " SELECT DISTINCT "
        strSQL = strSQL & " SUB_DESCRIPTION  "
        strSQL = strSQL & " FROM TICKET_TYPE WITH(NOLOCK);"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListCategory.DataSource = objDataReader
        DropListCategory.DataTextField = "SUB_DESCRIPTION"
        DropListCategory.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_RequestedBy()
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT '' [USERNAME]"
        strSQL = strSQL & " UNION "
        strSQL = strSQL & " SELECT DISTINCT USERNAME "
        strSQL = strSQL & " FROM USERS   WITH(NOLOCK);"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListRequestedBy.DataSource = objDataReader
        DropListRequestedBy.DataTextField = "USERNAME"
        DropListRequestedBy.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_AssignedTo()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT '' FIRST_NAME"
        strSQL = strSQL + " UNION  SELECT  U.FIRST_NAME"
        strSQL = strSQL + " From USERS U, ROLES R "
        strSQL = strSQL + " Where U.ID_ROLE = R.ID_ROLES "
        strSQL = strSQL + " And R.ID_ROLES <> 5 "

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListAssignedTo.DataSource = objDataReader
        DropListAssignedTo.DataTextField = "FIRST_NAME"
        DropListAssignedTo.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub btnSearchTickets_Click(sender As Object, e As EventArgs)
        Call fn_LoadData()
        lblDetails.Visible = True
    End Sub

    Private Function fn_LoadData()

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
        strSQL = strSQL + ", REQUEST_BY"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_REOPENED, 106) DATE_REOPENED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_CLOSED, 106) DATE_CLOSED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_RESOLVED, 106) DATE_RESOLVED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_REQUESTED, 106) DATE_REQUESTED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_OPENED, 106) DATE_OPENED"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12), DATE_ASSIGNED, 106) DATE_ASSIGNED"
        strSQL = strSQL + ", ASSIGNED_TO"
        strSQL = strSQL + "  FROM vs_Report_Tickets WITH(NOLOCK)"
        strSQL = strSQL & "  WHERE 1 = 1"

        If txtTicketNumber.Text <> "" Then
            strSQL = strSQL + " AND ID_TICKETS =  '" + txtTicketNumber.Text + "'"
        End If
        If DropListType.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND ISSUE_TYPE =  '" + DropListType.SelectedItem.Text + "'"
        End If
        If DropListCategory.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND ISSUE_SUB_TYPE =  '" + DropListCategory.SelectedItem.Text + "'"
        End If
        If txtDescription.Text <> "" Then
            strSQL = strSQL + " AND DESCRIPTION LIKE  '% " + txtDescription.Text + " %'"
        End If
        If txtDescription.Text <> "" Then
            strSQL = strSQL + " OR DESCRIPTION LIKE  '% " + txtDescription.Text + "'"
        End If
        If txtDescription.Text <> "" Then
            strSQL = strSQL + " OR DESCRIPTION LIKE '" + txtDescription.Text + " %'"
        End If
        If DropListStatusTicket.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND STATUS_TICKET =  '" + DropListStatusTicket.SelectedItem.Text + "'"
        End If
        If DropListPriority.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND PRIORITY =  '" + DropListPriority.SelectedItem.Text + "'"
        End If
        If DropListRequestedBy.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND REQUEST_BY =  '" + DropListRequestedBy.SelectedItem.Text + "'"
        End If
        If DropListAssignedTo.SelectedItem.Text <> "" Then
            strSQL = strSQL + " AND ASSIGNED_TO =  '" + DropListAssignedTo.SelectedItem.Text + "'"
        End If
        If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date ReOpened" Then
            strSQL = strSQL + " AND (DATE_REOPENED >=  CONVERT(NVARCHAR(12), '" + txtStartDate.Text + "',101) AND (DATE_REOPENED <= CONVERT(NVARCHAR(12),'" + txtEndDate.Text + "',101 ))) "
        End If
        If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date Closed" Then
            strSQL = strSQL + " AND (DATE_CLOSED >= CONVERT(NVARCHAR(12),  '" + txtStartDate.Text + "',101) AND  (DATE_CLOSED <= CONVERT(NVARCHAR(12), '" + txtEndDate.Text + "' ,101))) "
        End If
        If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date Assigned" Then
            strSQL = strSQL + " AND (DATE_ASSIGNED >= CONVERT(NVARCHAR(12),  '" + txtStartDate.Text + "',101) AND  (DATE_ASSIGNED <= CONVERT(NVARCHAR(12),'" + txtEndDate.Text + "',101 ))) "
        End If
        If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date Resolved" Then
            strSQL = strSQL + " AND (DATE_RESOLVED >= CONVERT(NVARCHAR(12),  '" + txtStartDate.Text + "',101) AND  (DATE_RESOLVED <= CONVERT(NVARCHAR(12),'" + txtEndDate.Text + "',101 ))) "
        End If
        If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date Requested" Then
            strSQL = strSQL + " AND (DATE_REQUESTED >= CONVERT(NVARCHAR(12),  '" + txtStartDate.Text + "',101) AND  (DATE_REQUESTED <= CONVERT(NVARCHAR(12),'" + txtEndDate.Text + "',101 ))) "
        End If
        If txtStartDate.Text <> "" And txtEndDate.Text <> "" And DropListDate.SelectedItem.Text = "Date Opened" Then
            strSQL = strSQL + " AND (DATE_OPENED >=  CONVERT(NVARCHAR(12), '" + txtStartDate.Text + "',101) AND  (DATE_OPENED <= CONVERT(NVARCHAR(12),'" + txtEndDate.Text + "' ,101))) "
        End If

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim myDataSet As New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        ViewState("dtAllRepoprts") = dt

        gvReportAllTickets.DataSource = myDataSet
        gvReportAllTickets.DataBind()

        If gvReportAllTickets.Rows.Count <> 0 Then
            Excelexport.Visible = True
        End If

        dbConnect.ChiudiDb()
        Return strSQL

    End Function

    Protected Sub Excelexport_Click(sender As Object, e As EventArgs)
        Call sb_ExportToXLS()
    End Sub

    Private Sub sb_ExportToXLS()
        Dim strAppDirectory As String = HttpContext.Current.Server.MapPath("~/" & "Reports\ETS_Report_Tickets.mrt")
        Dim strTablename As String = "vs_Report_Tickets"
        Dim Stimulsoft As New General
        Dim strSQL = fn_LoadData()
        Stimulsoft.sb_StimulsoftExcelReport(strSQL, strAppDirectory, strTablename)

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
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
                objCommand.CommandText = "GUI_Reopen_Closed_Ticket"
                objCommand.Connection = connessioneDb.Connessione
                objCommand.Parameters.AddWithValue("@ID_TICKET", Session("Id"))
                objCommand.Parameters.AddWithValue("@ISSUE", Session("issue_Type"))
                objCommand.Parameters.AddWithValue("@REMARKS", txtAdminRemarks.Text)
                objCommand.Parameters.AddWithValue("@USER_ID", Session("assignedTo"))

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

    Private Function fn_ValidatePageAccess() As Boolean
        Dim general As New General
        Dim pageUrl = general.fn_GetAbsolutePath()
        Dim Access = False
        If general.fn_GrantWebInterface(pageUrl, Session("R")) Then
            Access = True
        End If

        Return Access
    End Function

    Protected Sub btnClosePopRemarks_Click(sender As Object, e As EventArgs)
        PopupRemarks.Hide()
        txtAdminRemarks.Text = ""
    End Sub

    Protected Sub btnConfirmRemarks_Click(sender As Object, e As EventArgs)
        Call sb_ReOpenTicket()
        Response.Redirect("ReportTickets.aspx")
    End Sub

End Class