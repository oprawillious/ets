Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Configuration

Public Class ViewTestDefects
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If Not IsPostBack Then
                If Session("IdSLR") <> "" Then
                    hdIdTC.Value = Request.QueryString("ID")
                    Call sb_LoadTestDefect("")
                    lblCaption.Text = "View Test Defects - " + Session("SLR")
                End If
            End If
        End If

    End Sub

    Protected Sub btn_AddDefects()
        PopUpAddDefects.Show()
        hdOp.Value = "I"
        lblPopUpCaption.Text = "Add Defect"
    End Sub

    Protected Sub btClose_Click(sender As Object, e As EventArgs)
        PopUpAddDefects.Hide()
        Response.Redirect("ViewTestDefects.aspx")
    End Sub

    Private Sub sb_LoadTestDefect(strStatus As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT  ID_TEST_DEFECT"
        strSQL = strSQL + ", ID_TASK"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", APP_CATEGORY"
        strSQL = strSQL + ", IIF(ISNULL(FLAG_FIXED,'N')='Y','Fixed','Not Fixed') STATUS"
        strSQL = strSQL + ", SLR"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", EXPECTED_RESULT"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", TESTER"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_OPENED, 109) DATE_OPENED"
        strSQL = strSQL + ", DEFECT_TYPE"
        strSQL = strSQL + ", STEPS_TO_REPRODUCE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_FIXED, 109) DATE_FIXED"
        strSQL = strSQL + "  FROM TEST_DEFECT WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ID_TEST_SLR =" & Session("IdSLR") & ""

        If strStatus <> "" Then
            strSQL = strSQL + " AND FLAG_FIXED =" & "'" & strStatus & "'" & ""
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
        gvTestDefects.DataSource = myDataSet
        gvTestDefects.DataBind()
        dbConnect.ChiudiDb()
    End Sub

    Protected Sub btnClosePopDefect_Click(sender As Object, e As EventArgs)
        PopUpAddDefects.Hide()
        Response.Redirect("ViewTestDefects.aspx")
    End Sub

    Protected Sub btnCreateDefect_Click(sender As Object, e As EventArgs)
        If hdOp.Value = "I" Then
            If txtIssueDesc.Text <> "" And txtStepsToRepr.Text <> "" And txtActualResult.Text <> "" And txtExpectedResult.Text <> "" And DropListDefectType.Text <> "" Then
                Call sb_InsertUpdateDefect(hdOp.Value, 0)

                If DefectsFileUpload.HasFiles Then
                    Call sb_UploadDocument()
                    Response.Redirect("ViewTestDefects.aspx")
                Else
                    Response.Redirect("ViewTestDefects.aspx")
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "formValidation();", True)
            End If
        End If

        If hdOp.Value = "M" Then
            Call sb_InsertUpdateDefect(hdOp.Value, hdIdTestDefect.Value)
            If DefectsFileUpload.HasFiles Then
                Call sb_UploadDocument()
                Response.Redirect("ViewTestDefects.aspx")
            End If

        End If
    End Sub

    Private Sub sb_InsertUpdateDefect(strOp As String, strIdTestDefect As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Test_Defect"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_TEST_DEFECT", strIdTestDefect)
                objCommand.Parameters.AddWithValue("@ID_TEST_CASE", Session("IdTC"))
                objCommand.Parameters.AddWithValue("@ID_TEST_SLR", Session("IdSLR"))
                objCommand.Parameters.AddWithValue("@ID_TASK", Session("IdTask"))
                objCommand.Parameters.AddWithValue("@APP_CATEGORY", Session("AppCategory"))
                objCommand.Parameters.AddWithValue("@SLR", Session("SLR"))
                objCommand.Parameters.AddWithValue("@DESCRIPTION", txtIssueDesc.Text)
                objCommand.Parameters.AddWithValue("@DEFECT_TYPE", DropListDefectType.Text)
                objCommand.Parameters.AddWithValue("@STEPS_TO_REPRODUCE", txtStepsToRepr.Text)
                objCommand.Parameters.AddWithValue("@EXPECTED_RESULT", txtExpectedResult.Text)
                objCommand.Parameters.AddWithValue("@ACTUAL_RESULT", txtActualResult.Text)
                objCommand.Parameters.AddWithValue("@PRIORITY", DropListPriority.Text)
                objCommand.Parameters.AddWithValue("@FLAG_FIXED", IIf(DropListDefectStatus.Text = "Fixed", "Y", "N"))
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 1000

                Dim objOutputParameter1 As New SqlParameter("@ID_DEFECT", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 1000

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If CStr(objCommand.Parameters("@ID_DEFECT").Value) <> 0 Then
                    Session("TD") = CStr(objCommand.Parameters("@ID_DEFECT").Value)
                End If

                If strError <> "" Then
                    lblError.Text = strError
                Else
                    lblError.Text = "Test Defect added successfully !"
                End If
            End If
        End If

        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub lnkOpenPopUp_Click(sender As Object, e As EventArgs)
        lblPopUpCaption.Text = "Add Defect"
        PopUpAddDefects.Show()
        hdOp.Value = "I"
    End Sub

    Protected Sub gvTestDefects_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTestDefects.RowCommand

        If e.CommandName = "Modifica" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvTestDefects.Rows(Index)
            Dim strIdTestDefect As String = TryCast(row.FindControl("hdIdTestCaseDefect"), HiddenField).Value
            hdIdTestDefect.Value = strIdTestDefect
            Session("TD") = strIdTestDefect
            Call sb_LoadDefectDetails(hdIdTestDefect.Value)
            PopUpAddDefects.Show()
            trStatus.Visible = True
            hdOp.Value = "M"
            lblPopUpCaption.Text = "Edit Defect"
        End If

        If e.CommandName = "Remove" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvTestDefects.Rows(Index)
            Dim strIdTestDefect As String = TryCast(row.FindControl("hdIdTestCaseDefect"), HiddenField).Value
            Call sb_DeleteTestDefect(strIdTestDefect)
            Response.Redirect("ViewTestDefects.aspx")
        End If

        If e.CommandName = "Attachments" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvTestDefects.Rows(Index)
            Dim strIdTestDefect As String = TryCast(row.FindControl("hdIdTestCaseDefect"), HiddenField).Value
            Session("TD") = strIdTestDefect
            Response.Redirect("ViewDefectAttachment.aspx")
        End If
    End Sub

    Private Sub sb_LoadDefectDetails(strIdTestDefect As String)
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT  ID_TEST_DEFECT"
        strSQL = strSQL + ", ID_TASK"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", APP_CATEGORY"
        strSQL = strSQL + ", IIF(ISNULL(FLAG_FIXED,'N')='Y','Fixed','Not Fixed') STATUS"
        strSQL = strSQL + ", SLR"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", EXPECTED_RESULT"
        strSQL = strSQL + ", ACTUAL_RESULT"
        strSQL = strSQL + ", TESTER"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_OPENED, 109) DATE_OPENED"
        strSQL = strSQL + ", DEFECT_TYPE"
        strSQL = strSQL + ", STEPS_TO_REPRODUCE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_FIXED, 109) DATE_FIXED"
        strSQL = strSQL + "  FROM TEST_DEFECT WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TEST_DEFECT =" & strIdTestDefect & ""

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

            If Not IsNothing(objDataReader.Item("DESCRIPTION")) Then
                txtIssueDesc.Text = CStr(objDataReader.Item("DESCRIPTION") & "")
            End If
            If Not IsNothing(objDataReader.Item("DEFECT_TYPE")) Then
                DropListDefectType.Text = CStr(objDataReader.Item("DEFECT_TYPE") & "")
            End If
            If Not IsNothing(objDataReader.Item("STEPS_TO_REPRODUCE")) Then
                txtStepsToRepr.Text = CStr(objDataReader.Item("STEPS_TO_REPRODUCE") & "")
            End If
            If Not IsNothing(objDataReader.Item("EXPECTED_RESULT")) Then
                txtExpectedResult.Text = CStr(objDataReader.Item("EXPECTED_RESULT") & "")
            End If
            If Not IsNothing(objDataReader.Item("ACTUAL_RESULT")) Then
                txtActualResult.Text = CStr(objDataReader.Item("ACTUAL_RESULT") & "")
            End If
            If Not IsNothing(objDataReader.Item("PRIORITY")) Then
                DropListPriority.Text = CStr(objDataReader.Item("PRIORITY") & "")
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()
    End Sub

    Private Sub sb_DeleteTestDefect(strIdDefect As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Test_Defect"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "D")
                objCommand.Parameters.AddWithValue("@ID_TEST_DEFECT", strIdDefect)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 1000

                Dim objOutputParameter1 As New SqlParameter("@ID_DEFECT", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 1000

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

            End If
        End If

        connessioneDb.ChiudiDb()
    End Sub

    Private Sub sb_InsertFileDefect(strpaths As String, strfilename As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_File"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "D")
                objCommand.Parameters.AddWithValue("@ID_TEST_DEFECT", Session("TD"))
                objCommand.Parameters.AddWithValue("@FILE_PATH", strpaths)
                objCommand.Parameters.AddWithValue("@FILE_NAME", strfilename)

                Dim objDataReader As SqlDataReader
                objDataReader = objCommand.ExecuteReader()

                objDataReader.Close()
                objCommand = Nothing
                connessioneDb.ChiudiDb()

            End If
        End If

        connessioneDb.ChiudiDb()
    End Sub

    Private Sub sb_UploadDocument()
        Dim uploadfile As New General
        Dim strdirectory = Convert.ToString(WebConfigurationManager.AppSettings("PathDoxServiceRequest"))
        Dim paths = New List(Of String)
        Dim filenames = New List(Of String)
        Dim strUploadedFile = DefectsFileUpload.PostedFile

        Dim validFileTypes As String() = {".jpeg", ".jpg", ".JPEG", ".JPG", ".png", ".PNG", ".gif", ".GIF"}
        Dim fileExtension As String = Path.GetExtension(DefectsFileUpload.PostedFile.FileName)
        Dim isValidFile As Boolean = False

        For i As Integer = 0 To validFileTypes.Length - 1
            If fileExtension = validFileTypes(i) Then
                isValidFile = True
                Exit For
            End If
        Next

        If isValidFile Then
            If Not Directory.Exists(strdirectory) Then
                Directory.CreateDirectory(strdirectory)
            End If

            Dim strPath = uploadfile.fn_UploadFile(strdirectory, strUploadedFile)
            paths.Add(strPath)
            filenames.Add(strUploadedFile.FileName)

            Call sb_InsertFileDefect(String.Join(";", paths), String.Join(";", filenames))
            Response.Redirect("ViewTestDefects.aspx")
        Else
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "FileError();", True)
        End If

    End Sub

    Protected Sub DropListStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        Call sb_LoadTestDefect(DropListStatus.SelectedValue)
    End Sub

End Class