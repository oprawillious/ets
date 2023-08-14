Imports System.Data.SqlClient
Imports System.Threading.Tasks

Public Class InsertTaskRemark
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not IsPostBack Then

                Dim idUser = Request.QueryString("Id")
                Dim strTask = Request.QueryString("IdTask")
                Call sb_LoadTaskDetails(strTask, idUser)

            End If
        End If

    End Sub
    Private Sub sb_LoadTaskDetails(strTask As String, idUser As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TASK"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", TYPE_TASK"
        strSQL = strSQL + ", REMARKS"
        strSQL = strSQL + ", ID_USER"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_START_DATE) EXPECTED_START_DATE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_END_DATE) EXPECTED_END_DATE"
        strSQL = strSQL + "  FROM vs_Task_Dropdown WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TASK =" & strTask & " AND ID_USER=" & idUser & ""

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


            If Not IsNothing(objDataReader.Item("TASK_DESCRIPTION")) Then
                lblDescription.Text = CStr(objDataReader.Item("TASK_DESCRIPTION") & "")
            End If
            If Not IsNothing(objDataReader.Item("REMARKS")) Then
                txttaskremarks.Text = CStr(objDataReader.Item("REMARKS") & "")
            End If

            If Not IsNothing(objDataReader.Item("CATEGORY")) Then
                lblCategory.Text = CStr(objDataReader.Item("CATEGORY") & "")
            End If

            If Not IsNothing(objDataReader.Item("TYPE_TASK")) Then
                lblTaskType.Text = CStr(objDataReader.Item("TYPE_TASK") & "")
            End If

            If Not IsNothing(objDataReader.Item("EXPECTED_START_DATE")) Then
                lblExpectedStartDate.Text = CStr(objDataReader.Item("EXPECTED_START_DATE") & "")
            End If

            If Not IsNothing(objDataReader.Item("EXPECTED_END_DATE")) Then
                lblExpectedEndDate.Text = CStr(objDataReader.Item("EXPECTED_END_DATE") & "")
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

    End Sub


    Private Sub sb_Insert_Task_Remarks(idUser As String, strTask As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Task_Remark"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@USER_ID ", idUser)
                objCommand.Parameters.AddWithValue("@REMARKS", txttaskremarks.Text)
                objCommand.Parameters.AddWithValue("@ID_TASK", strTask)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                End If

            End If
        End If
        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnTaskRemarks_Click(sender As Object, e As EventArgs)
        Dim idUser = Request.QueryString("Id")
        Dim strTask = Request.QueryString("IdTask")
        Call sb_Insert_Task_Remarks(idUser, strTask)
        Response.Redirect("ViewDetailsTask.aspx?Id=" & strTask & "")
    End Sub
End Class