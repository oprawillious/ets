Imports System.Data.SqlClient
Imports System.Drawing

Public Class ViewDetailsTestCase
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If fn_ValidatePageAccess() Then
                If Not IsPostBack Then
                    If Session("Id") <> "" Then
                        Call sb_ViewDetailsTestCase("")
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btnSearchTestCase_Click(sender As Object, e As EventArgs)
        Call sb_ViewDetailsTestCase(txtTestCaseNumber.Text)
    End Sub

    Private Sub sb_ViewDetailsTestCase(strIdTestCase As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TEST_CASES"
        strSQL = strSQL + ", TEST_PAGE_NAME"
        strSQL = strSQL + ", ID_TEST_CASE_DEFECTS"
        strSQL = strSQL + ", ISSUE_DESCRIPTION"
        strSQL = strSQL + ", DEFECT_TYPE"
        strSQL = strSQL + ", TESTER_DEFECT"
        strSQL = strSQL + ", STATUS_DEFECT"
        strSQL = strSQL + ", DATE_OPEN_DEFECT"
        strSQL = strSQL + ", DATE_CLOSE_DEFECT"
        strSQL = strSQL + ", DATE_REOPEN_DEFECT"
        strSQL = strSQL + ", DATE_FIX_DEFECT"
        strSQL = strSQL + ", FLAG_OPEN_DEFECT"
        strSQL = strSQL + ", FLAG_CLOSE_DEFECT"
        strSQL = strSQL + ", FLAG_REOPEN_DEFECT"
        strSQL = strSQL + ", FLAG_FIX_DEFECT"
        strSQL = strSQL + "  FROM TEST_CASES_DETAILS"
        strSQL = strSQL + "  WHERE ID_TEST_CASES =" & Session("Id") & ""

        If strIdTestCase <> "" Then
            strSQL = strSQL + " AND ID_TEST_CASE_DEFECTS =" + "'" + strIdTestCase + "'" + ""
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
        gvViewDetailsTestCase.DataSource = myDataSet
        gvViewDetailsTestCase.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub gvViewDetailsTestCase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViewDetailsTestCase.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gvViewDetailsTestCase.Rows(index)
        Dim strIdTestCaseDetails As String = TryCast(row.FindControl("hdIdTestCaseDefect"), HiddenField).Value

        If e.CommandName = "Modifica" Then
            Session("Id") = strIdTestCaseDetails
            Call sb_LoadViewDetailTestCase(strIdTestCaseDetails)
            PopupTestCaseDetails.Show()
        End If

        If e.CommandName = "Remove" Then
            sb_DeleteViewDetailTestCase(strIdTestCaseDetails)
            Response.Redirect("ViewDetailsTestCase.aspx")
        End If

    End Sub

    Protected Sub btClose_Click(sender As Object, e As EventArgs)
        PopupTestCaseDetails.Hide()
    End Sub

    Private Sub sb_UpdateViewDetailsTestCase()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Details_TestCase"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "M")
                objCommand.Parameters.AddWithValue("@ID_TEST_CASE_DETAILS", Session("Id"))
                objCommand.Parameters.AddWithValue("@ID_TEST_CASES", 0)
                objCommand.Parameters.AddWithValue("@TEST_PAGE_NAME", txtTestPageName.Text)
                objCommand.Parameters.AddWithValue("@ISSUE_DESCRIPTION", txtIssueDescription.Text)
                objCommand.Parameters.AddWithValue("@DEFECT_TYPE", DropListDefectType.Text)
                objCommand.Parameters.AddWithValue("@TESTER_DEFECT", txtTestDefect.Text)
                objCommand.Parameters.AddWithValue("@STATUS_DEFECT", txtStatusdefect.Text)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblAssign.Text = strError
                    lblAssign.ForeColor = Color.Red

                Else
                    lblAssign.Text = "Test Case Details Updated successfully !"
                    lblAssign.ForeColor = Color.Green
                End If

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub

    Private Sub sb_LoadViewDetailTestCase(strIdTestCaseDetails As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If
        strSQL = "SELECT ID_TEST_CASE_DEFECTS"
        strSQL = strSQL + ", TEST_PAGE_NAME"
        strSQL = strSQL + ", ISSUE_DESCRIPTION"
        strSQL = strSQL + ", DEFECT_TYPE"
        strSQL = strSQL + ", TESTER_DEFECT"
        strSQL = strSQL + ", STATUS_DEFECT"
        strSQL = strSQL + "  FROM TEST_CASES_DETAILS"
        strSQL = strSQL + "  WHERE ID_TEST_CASE_DEFECTS =" & strIdTestCaseDetails & ""

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

            If Not IsNothing(objDataReader.Item("ID_TEST_CASE_DEFECTS")) Then
                lblEditTestCase.Text = "Update: Test Case ID - " + CStr(objDataReader.Item("ID_TEST_CASE_DEFECTS") & "")
            End If
            If Not IsNothing(objDataReader.Item("TEST_PAGE_NAME")) Then
                txtTestPageName.Text = CStr(objDataReader.Item("TEST_PAGE_NAME") & "")
            End If
            If Not IsNothing(objDataReader.Item("ISSUE_DESCRIPTION")) Then
                txtIssueDescription.Text = CStr(objDataReader.Item("ISSUE_DESCRIPTION") & "")
            End If
            If Not IsNothing(objDataReader.Item("DEFECT_TYPE")) Then
                DropListDefectType.Text = CStr(objDataReader.Item("DEFECT_TYPE") & "")
            End If
            If Not IsNothing(objDataReader.Item("TESTER_DEFECT")) Then
                txtTestDefect.Text = CStr(objDataReader.Item("TESTER_DEFECT") & "")
            End If
            If Not IsNothing(objDataReader.Item("STATUS_DEFECT")) Then
                txtStatusdefect.Text = CStr(objDataReader.Item("STATUS_DEFECT") & "")
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()
    End Sub

    Private Sub sb_DeleteViewDetailTestCase(strIdTestCaseDetails As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Details_TestCase"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "D")
                objCommand.Parameters.AddWithValue("@ID_TEST_CASE_DETAILS", strIdTestCaseDetails)
                objCommand.Parameters.AddWithValue("@ID_TEST_CASES", 0)
                objCommand.Parameters.AddWithValue("@TEST_PAGE_NAME", "")
                objCommand.Parameters.AddWithValue("@ISSUE_DESCRIPTION", "")
                objCommand.Parameters.AddWithValue("@DEFECT_TYPE", "")
                objCommand.Parameters.AddWithValue("@TESTER_DEFECT", "")
                objCommand.Parameters.AddWithValue("@STATUS_DEFECT", "")

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub btnUpdateDetailsTestCase_Click(sender As Object, e As EventArgs)
        sb_UpdateViewDetailsTestCase()
        Response.Redirect("ViewTestCase.aspx")
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

End Class