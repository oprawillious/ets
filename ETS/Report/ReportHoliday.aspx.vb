Imports System.Data.SqlClient
Imports System.IO
Imports ETS.DataBase
Imports ETS.General


Public Class ReportHoliday
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack() Then
                If fn_ValidatePageAccess() Then
                    'Call Popola_DropList_AppCategory()
                    gvReportHolidayDetails.DataSource = Nothing
                    Me.btnExportToXLS.Attributes.Add("onmouseout", "javascript:this.src='/img/excel.png'")
                    Me.btnExportToXLS.Attributes.Add("onmouseover", "javascript:this.src='/img/excel_over.png'")
                Else
                    FormsAuthentication.SignOut()
                    Response.Redirect("~/Login.aspx")
                End If
            End If
        End If

    End Sub


    Private Function sb_ViewHolidayReport()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_HOLIDAY_PLAN "
        strSQL = strSQL + ", ID_USER"
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

        If Not IsInRole(Session("R"), Roll_Kind.Administrator) Then
            strSQL = strSQL + " AND USERNAME =" + "'" + Page.User.Identity.Name + "'" + ""
        End If

        'If IsInRole(Session("R"), Roll_Kind.Administrator) Then
        '    strSQL = strSQL + " AND DATE_APPROVED IS NULL"
        'End If

        If DropListStatus.SelectedItem.Value <> "" Then
            strSQL = strSQL + " AND FLAG_APPROVED = " + "'" + DropListStatus.SelectedItem.Value + "'" + ""
        End If
        'If txtFirstname.Text <> "" Then
        '    strSQL = strSQL + " AND FULLNAME LIKE '%" + txtFirstname.Text + "%'"
        'End If

        'If txtLastname.Text <> "" Then
        '    strSQL = strSQL + " AND FULLNAME LIKE '%" + txtLastname.Text + "%'"
        'End If
        If txtFirstname.Text <> "" Then
            strSQL = strSQL + " AND FIRST_NAME =  " + "'" + txtFirstname.Text + "'" + ""
        End If

        If txtLastname.Text <> "" Then
            strSQL = strSQL + " AND LAST_NAME =  " + "'" + txtLastname.Text + "'" + ""
        End If


        If txtStartDate.Text <> "" And txtEndDate.Text <> "" Then
            strSQL = strSQL + " AND (HOLIDAY_START_DATE >= CONVERT(NVARCHAR(12),  '" + txtStartDate.Text + "',101) AND  (HOLIDAY_START_DATE <= CONVERT(NVARCHAR(12), '" + txtEndDate.Text + "' ,101))) "
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
        ViewState("dtHolidayReport") = dt

        gvReportHolidayDetails.DataSource = myDataSet
        gvReportHolidayDetails.DataBind()

        If gvReportHolidayDetails.Rows.Count <> 0 Then
            btnExportToXLS.Visible = True
        End If

        dbConnect.ChiudiDb()

        Return strSQL

    End Function

    Protected Sub btnSearchHoliday_Click()
        Call sb_ViewHolidayReport()

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Private Sub sb_ExportToPDF()
        Dim strAppDirectory As String = HttpContext.Current.Server.MapPath("~/" & "Reports\ETS_Report_Holiday.mrt")
        Dim strTablename As String = "vs_Holiday_Plan"
        Dim Stimulsoft As New General
        Dim strSQL = sb_ViewHolidayReport()
        Stimulsoft.sb_StimulsoftPdfReport(strSQL, strAppDirectory, strTablename)

    End Sub


    Protected Sub btnExportToXLS_Click(sender As Object, e As EventArgs)
        Call sb_ExportToPDF()
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

    Protected Sub gvReportHolidayDetails_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvReportHolidayDetails.PageIndexChanging
        gvReportHolidayDetails.PageIndex = e.NewPageIndex
        Call sb_ViewHolidayReport()

    End Sub
End Class