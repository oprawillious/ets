Imports System.Data.SqlClient

Public Class ViewTestCase
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then

            If Not IsPostBack Then
                Call sb_ViewTestCases("")
            End If

        Else
            Response.Redirect("~/Login.aspx")
        End If
    End Sub

    Protected Sub btnSearchTestCase_Click(sender As Object, e As EventArgs)
        Call sb_ViewTestCases(txtTestCaseNumber.Text)
    End Sub


    Private Sub sb_ViewTestCases(strIdTestCase As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASES"
        strSQL = strSQL + ", ID_TASK"
        strSQL = strSQL + ", ID_TICKETS"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", DEVELOPER"
        strSQL = strSQL + ", APP_CATEGORY"
        strSQL = strSQL + ", SERVICE_LEVEL_REQUIREMENT"
        strSQL = strSQL + ", SCENERIO_TEST_CASE"
        strSQL = strSQL + ", TEST_STEPS"
        strSQL = strSQL + ", EXPECTED_RESULTS"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", TESTER"
        strSQL = strSQL + ", ENVIROMENT"
        strSQL = strSQL + ", ISNULL(STATUS_TEST,'status not set') STATUS_TEST"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_START_TEST,109) DATE_START_TEST"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_COMPLETE_TEST,109) DATE_COMPLETE_TEST"
        strSQL = strSQL + "  FROM vs_Test_Cases WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ISNULL(FLAG_COMPLETE_TEST,'N') = 'N'"

        If strIdTestCase <> "" Then
            strSQL = strSQL + " AND ID_TEST_CASES =" + "'" + strIdTestCase + "'" + ""
            strSQL = strSQL + " ORDER BY ID_TEST_CASES DESC"
        Else
            strSQL = strSQL + " ORDER BY ID_TEST_CASES DESC"
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

        gvTestCase.DataSource = myDataSet
        gvTestCase.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub gvTestCase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTestCase.RowCommand

        If e.CommandName <> "Page" Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvTestCase.Rows(index)

            Dim strIdTestCase As String = TryCast(row.FindControl("hdIdTestCase"), HiddenField).Value
            Dim strIdTicket As String = TryCast(row.FindControl("hdIdTicket"), HiddenField).Value
            Dim strAppCategory As String = TryCast(row.FindControl("hdAppCategory"), HiddenField).Value
            Dim strIdTask As String = TryCast(row.FindControl("hdIdTask"), HiddenField).Value
            Dim strDescription As String = TryCast(row.FindControl("hdDescription"), HiddenField).Value

            If e.CommandName = "Details" Then
                Response.Redirect("InsertUpdateTestCase.aspx?IdTC=" & strIdTestCase & "&IdT=" & strIdTask & "")
            End If

        End If

    End Sub

    Protected Sub gvTestCase_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvTestCase.PageIndexChanging
        gvTestCase.PageIndex = e.NewPageIndex
        Call sb_ViewTestCases("")

    End Sub

    Public Function fn_CheckStatus(ByVal element As Object) As Boolean
        If CStr(element) = "Passed" Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub btnAddNewTestCase_Click()

        Response.Redirect("InsertTestCase.aspx")

    End Sub

    Protected Sub gvTestCase_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTestCase.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim ImgStatus As Image = TryCast(e.Row.FindControl("ImgFailed"), Image)

            If e.Row.DataItem("STATUS_TEST").ToString() = "status not set" Then
                ImgStatus.Visible = False
            End If

        End If

    End Sub

End Class