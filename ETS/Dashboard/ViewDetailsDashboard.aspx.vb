Imports System.Data.SqlClient

Public Class ViewDetailsDashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack Then
                Call LoadData()
            End If
        End If
    End Sub

    Private Sub LoadData()
        Dim Parameter As String = Request.QueryString("Op")

        If Parameter = "CloseTask" Then
            Call sb_LoadCloseTask()
            lblGridViewName.Text = "Total Close Task"
        End If
        If Parameter = "OpenTask" Then
            Call sb_LoadOpenTask()
            lblGridViewName.Text = "Total Open Task"
        End If
        If Parameter = "OpenTicket" Then
            Call sb_LoadOpenTicket()
            lblGridViewName.Text = "Total Open Ticket"
        End If
        If Parameter = "CloseTicket" Then
            Call sb_LoadCloseTicket()
            lblGridViewName.Text = "Total Close Ticket"
        End If
        If Parameter = "AllTicket" Then
            Call sb_LoadAllTicket()
            lblGridViewName.Text = "All Tickets"
        End If
        If Parameter = "TotalDaily" Then
            Call sb_LoadDailyTicket()
            lblGridViewName.Text = "Total Daily Ticket"
        End If
        If Parameter = "AllPreviousDayTicket" Then
            Call sb_LoadAllPreviousDayTicket()
            lblGridViewName.Text = "All Previous Day Ticket"
        End If

    End Sub
    Private Sub sb_LoadOpenTask()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TASK"
        strSQL = strSQL + ", ID_TICKETS"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", TYPE_TASK"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", REMARK"
        strSQL = strSQL + " FROM TASK WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ISNULL(FLAG_COMPLETE,'N') = 'N'"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvOpenTask.DataSource = myDataSet
        gvOpenTask.DataBind()

        Dim strRowCount = gvOpenTask.Rows.Count.ToString()
        lblcount.Text = strRowCount
        dbConnect.ChiudiDb()

    End Sub
    Private Sub sb_LoadCloseTask()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TASK"
        strSQL = strSQL + ", ID_TICKETS"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", TYPE_TASK"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", REMARK"
        strSQL = strSQL + "  FROM TASK WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND FLAG_COMPLETE = 'Y'"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvCloseTask.DataSource = myDataSet
        gvCloseTask.DataBind()

        Dim strRowCount As String = gvCloseTask.Rows.Count.ToString()
        lblcount.Text = strRowCount
        dbConnect.ChiudiDb()

    End Sub
    Private Sub sb_LoadAllTicket()

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
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvAllTicket.DataSource = myDataSet
        gvAllTicket.DataBind()

        Dim strRowCount As String = gvAllTicket.Rows.Count.ToString()
        lblcount.Text = strRowCount
        dbConnect.ChiudiDb()

    End Sub

    Private Sub sb_LoadOpenTicket()

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
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE STATUS_TICKET='Open'"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvOpenicket.DataSource = myDataSet
        gvOpenicket.DataBind()

        Dim strRowCount As String = gvOpenicket.Rows.Count.ToString()
        lblcount.Text = strRowCount
        dbConnect.ChiudiDb()

    End Sub

    Private Sub sb_LoadCloseTicket()

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
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"
        strSQL = strSQL + " WHERE STATUS_TICKET='Closed'"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvCloseTicket.DataSource = myDataSet

        gvCloseTicket.DataBind()
        Dim strRowCount As String = gvCloseTicket.Rows.Count.ToString()
        lblcount.Text = strRowCount
        dbConnect.ChiudiDb()

    End Sub

    Private Sub sb_LoadDailyTicket()

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
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"
        strSQL = strSQL + " WHERE CONVERT(NVARCHAR(12),DATE_REQUESTED,109) = CONVERT(NVARCHAR(12),GETDATE(),109);"
        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvDailyTicket.DataSource = myDataSet
        gvDailyTicket.DataBind()

        Dim strRowCount As String = gvDailyTicket.Rows.Count.ToString()
        lblcount.Text = strRowCount
        dbConnect.ChiudiDb()

    End Sub

    Private Sub sb_LoadAllPreviousDayTicket()

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
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(ASSIGNED_TO IS NULL,'Not Yet Assigned', ASSIGNED_TO)ASSIGNED_TO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + "  FROM TICKETS WITH(NOLOCK)"
        strSQL = strSQL + "WHERE CONVERT(NVARCHAR(12),DATE_REQUESTED, 109) = CONVERT(NVARCHAR(12),GETDATE()-1,109)"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvAllPreviousDayTicket.DataSource = myDataSet
        gvAllPreviousDayTicket.DataBind()

        Dim strRowCount As String = gvAllPreviousDayTicket.Rows.Count.ToString()
        lblcount.Text = strRowCount
        dbConnect.ChiudiDb()

    End Sub

End Class



