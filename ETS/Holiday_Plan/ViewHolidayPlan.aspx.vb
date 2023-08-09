Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General

Public Class ViewHolidayPlan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack Then
                If Not IsInRole(Session("R"), Roll_Kind.Administrator) Then

                    gvHolidayDetails.Columns(12).Visible = False
                    gvHolidayDetails.Columns(13).Visible = False

                    trlblEmpName.Visible = False
                    trtxtEmpName.Visible = False
                End If

                'If IsInRole(Session("R"), Roll_Kind.Administrator) Then
                '    gvHolidayDetails.Columns(13).Visible = False
                'End If

                Call sb_ViewHoliday()
            End If
        End If

    End Sub

    Private Sub sb_ViewHoliday()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_HOLIDAY_PLAN "
        strSQL = strSQL + ", ID_USER"
        strSQL = strSQL + ", ID_DEPARTMENT"
        strSQL = strSQL + ", TYPE_HOLIDAY"
        strSQL = strSQL + ", FULLNAME"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),HOLIDAY_START_DATE, 109) HOLIDAY_START_DATE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),HOLIDAY_END_DATE, 109) HOLIDAY_END_DATE"
        strSQL = strSQL + ", IIF(FLAG_APPROVED IS NULL,'Pending', IIF(FLAG_APPROVED = 'Y', 'Approved', 'Rejected')) FLAG_APPROVED"
        strSQL = strSQL + ", NO_OF_DAYS"
        strSQL = strSQL + ", USER_REMARKS"
        strSQL = strSQL + ", ADMIN_REMARKS"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED,109) DATE_REQUESTED"
        strSQL = strSQL + "  FROM vs_Holiday_Plan WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"

        If Not IsInRole(Session("R"), Roll_Kind.Administrator) And Not IsInRole(Session("R"), Roll_Kind.HR_Manager) And Not IsInRole(Session("R"), Roll_Kind.Senior_Manager) Then
            strSQL = strSQL + " AND USERNAME =" + "'" + Page.User.Identity.Name + "'" + ""
        End If

        If IsInRole(Session("R"), Roll_Kind.Administrator) Then
            strSQL = strSQL + " AND DATE_APPROVED IS NULL"
            strSQL = strSQL + " AND DATE_HR_APPROVE IS NULL"
        End If

        If IsInRole(Session("R"), Roll_Kind.HR_Manager) Then
            strSQL = strSQL + " AND ISNULL(FLAG_APPROVE,'N') = 'Y'"
            strSQL = strSQL + " AND ISNULL(FLAG_HR_APPROVE,'N') = 'N'"
        End If

        If IsInRole(Session("R"), Roll_Kind.Senior_Manager) Then
            strSQL = strSQL + " AND ID_DEPARTMENT=" + "'" + Session("D") + "'" + ""
        End If

        If txtEmployeeName.Text <> "" Then
            strSQL = strSQL + " AND FULLNAME LIKE '%" + txtEmployeeName.Text + "%'"
        End If

        If txtRequestDate.Text <> "" Then
            strSQL = strSQL + " AND CONVERT(NVARCHAR(12),DATE_REQUESTED,109) =" & "CONVERT(NVARCHAR(12)," & "CAST('" & txtRequestDate.Text & "' AS DATETIME)" & ",109)"
        End If

        strSQL = strSQL + " ORDER BY ID_HOLIDAY_PLAN DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvHolidayDetails.DataSource = myDataSet
        gvHolidayDetails.DataBind()
        dbConnect.ChiudiDb()

        'If IsInRole(Session("R"), Roll_Kind.Administrator) Then
        '    For Each row As GridViewRow In gvHolidayDetails.Rows
        '        If CDate(TryCast(row.FindControl("hdEndDate"), HiddenField).Value) < DateTime.Now() Then
        '            row.Cells(14).Enabled = False
        '            TryCast(row.Cells(14).FindControl("btnApprove"), Button).ToolTip = "Leave End Date has elapsed. Click the Edit Button to Approve."
        '        End If
        '    Next
        'End If

    End Sub


    Protected Sub gvHolidayDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvHolidayDetails.RowCommand

        If e.CommandName <> "Page" Then

            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvHolidayDetails.Rows(Index)
            Dim strIdHoliday As String = TryCast(row.FindControl("hdIdHolidayPlan"), HiddenField).Value
            Dim strIdUser As String = TryCast(row.FindControl("hdIdUser"), HiddenField).Value
            Dim strDays As String = TryCast(row.FindControl("hdDays"), HiddenField).Value
            Dim strUsername As String = TryCast(row.FindControl("hdUsername"), HiddenField).Value

            If e.CommandName = "Modifica" Then
                Session("Username") = strUsername
                Response.Redirect("NewHolidayPlan.aspx?Op=M&Id=" + strIdHoliday)
            End If

            If e.CommandName = "Remove" Then
                Call sb_DeleteLeaveRequest(strIdHoliday)
                Response.Redirect("ViewHolidayPlan.aspx")
            End If

            If e.CommandName = "Approve" Then
                Call sb_ApproveRejectLeave("Y", strDays, strIdUser, strIdHoliday)
                Response.Redirect("ViewHolidayPlan.aspx")
            End If

            If e.CommandName = "Reject" Then
                Call sb_ApproveRejectLeave("N", strDays, strIdUser, strIdHoliday)
                Response.Redirect("ViewHolidayPlan.aspx")
            End If

        End If

    End Sub

    Private Sub sb_DeleteLeaveRequest(strIdHoliday As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Holiday_Plan"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "D")
                objCommand.Parameters.AddWithValue("@ID_HOLIDAY", CInt(strIdHoliday))

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_HOLIDAY_PLAN", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteReader()
                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)

            End If
        End If

        connessioneDb.ChiudiDb()

    End Sub

    Private Sub sb_ApproveRejectLeave(strFlag As String, strDays As String, strIdUser As String, strIdHolidayPlan As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Approve_Reject_Holiday_Plan"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@FLAG_APPROVE_REJECT", strFlag)
                objCommand.Parameters.AddWithValue("@USED_DAYS", strDays)
                objCommand.Parameters.AddWithValue("@ADMIN_REMARKS", hdAdminRemark.Value)
                objCommand.Parameters.AddWithValue("@ID_USER", strIdUser)
                objCommand.Parameters.AddWithValue("@ID_ROLE", Session("R"))
                objCommand.Parameters.AddWithValue("@ID_HOLIDAY_PLAN", strIdHolidayPlan)

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

    Protected Sub btnSearchHoliday_Click()
        Call sb_ViewHoliday()
    End Sub

    Protected Sub btnNewHoliday_Click()
        Response.Redirect("NewHolidayPlan.aspx")
    End Sub

    Protected Sub gvHolidayDetails_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvHolidayDetails.PageIndexChanging
        gvHolidayDetails.PageIndex = e.NewPageIndex
        Call sb_ViewHoliday()
    End Sub

    Protected Sub btClose_Click(sender As Object, e As EventArgs)
        'PopupViewHolidayAttachment.Hide()
    End Sub

    'Private Sub sb_LoadAttachments(strIdHolidayPlan As String)
    '    Dim dbConnect As New DataBase
    '    Dim strSQL As String

    '    If dbConnect.StatoConnessione = 0 Then
    '        dbConnect.connettidb()
    '    End If

    '    strSQL = "SELECT ID_HOLIDAY_DOCUMENT"
    '    strSQL = strSQL + ", FILE_PATH"
    '    strSQL = strSQL + ", FILE_NAME"
    '    strSQL = strSQL + "  FROM HOLIDAY_DOCUMENT WITH(NOLOCK)"
    '    strSQL = strSQL + "  WHERE ID_HOLIDAY_PLAN =" & strIdHolidayPlan & ""

    '    Dim objCommand As SqlCommand = New SqlCommand()
    '    objCommand.CommandText = strSQL
    '    objCommand.CommandType = CommandType.Text
    '    objCommand.Connection = dbConnect.Connessione

    '    Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
    '    Dim myDataSet As DataSet = New DataSet()
    '    mySqlAdapter.Fill(myDataSet)
    '    Dim dt As DataTable = New DataTable()

    '    mySqlAdapter.Fill(dt)
    '    gvHolidayAttachment.DataSource = myDataSet
    '    gvHolidayAttachment.DataBind()
    '    dbConnect.ChiudiDb()
    'End Sub

    'Protected Sub gvHolidayAttachment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvHolidayAttachment.RowCommand

    '    Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '    Dim row As GridViewRow = gvHolidayAttachment.Rows(index)
    '    Dim strFilePath As String = TryCast(row.FindControl("hdFilePath"), HiddenField).Value

    '    If e.CommandName = "DownloadFile" Then
    '        Call sb_DownloadAttachment(strFilePath)
    '    End If

    '    Response.Redirect("ViewHolidayPlan.aspx")
    'End Sub

    Private Sub sb_DownloadAttachment(strFilePath As String)
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & IO.Path.GetFileName(strFilePath))
        Response.WriteFile(strFilePath)
        Response.End()

    End Sub
End Class