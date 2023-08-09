Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General
Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary
Imports Stimulsoft.Report.Web
Imports System.Web.HttpUtility
Imports System.Security.Cryptography
Imports System.IO

Public Class ViewDashboard
    Inherits Page

    Shared Sub New()
        Dim path = HttpContext.Current.Server.MapPath("~/license.key")
        Stimulsoft.Base.StiLicense.LoadFromFile(path)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If Not IsPostBack Then

                If fn_CheckPasswordReset() = "Y" Then
                    Response.Redirect("/ChangePassword.aspx")
                Else
                    Call sb_UrlRedirect()
                End If

            End If
        End If

    End Sub


    Protected Sub StiWebViewer1_GetReport(ByVal sender As Object, ByVal e As StiReportDataEventArgs)
        Dim report = StiReport.CreateNewDashboard()
        Dim path = Server.MapPath("~/Reports/ETS_NEWDASHBOARD1.mrt")
        report.Load(path)
        e.Report = report
    End Sub

    Public Sub sb_UrlRedirect()
        If IsInRole(Session("R"), Roll_Kind.Developer) Then
            Response.Redirect("/TASK/ViewTaskAssignedToMe.aspx")
        End If
        If IsInRole(Session("R"), Roll_Kind.Tester) Then
            Response.Redirect("/TESTCASE/ViewTestCase.aspx")
        End If
        If IsInRole(Session("R"), Roll_Kind.HelpDesk) Then
            Response.Redirect("/TICKET/ViewTickets.aspx")
        End If
        If IsInRole(Session("R"), Roll_Kind.HR_Manager) Then
            Response.Redirect("/HOLIDAY_PLAN/ViewHolidayPlan.aspx")
        End If
    End Sub

    Private Function fn_CheckPasswordReset()

        Dim dbConnect As New DataBase
        Dim strSQL As String
        Dim strFlag As String = String.Empty

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ISNULL(FLAG_RESET_PASSWORD, 'N') FLAG_RESET_PASSWORD"
        strSQL = strSQL + " FROM USERS WITH(NOLOCK)"
        strSQL = strSQL + " WHERE USERNAME =" & "'" & Session("U") & "'"

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

            If Not IsNothing(objDataReader.Item("FLAG_RESET_PASSWORD")) Then
                strflag = CStr(objDataReader.Item("FLAG_RESET_PASSWORD") & "")
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

        Return strFlag

    End Function

End Class