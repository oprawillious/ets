Imports System.Data.SqlClient

Public Class InsertUpdateDepartment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated() Then
            If Not IsPostBack Then
                If Session("UserDeptId") <> "" Then
                    Call sb_LoadDepartmentDetails()
                End If
            End If
        End If

    End Sub


    Private Sub sb_LoadDepartmentDetails()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_DEPARTMENT"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + "  FROM vs_department WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"
        strSQL = strSQL + "  AND ID_DEPARTMENT =" & Session("UserDeptId") & ""

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
                txtCreateDepartment.Text = CStr(objDataReader.Item("DESCRIPTION") & "")
            End If


        End If

        objDataReader.Close()
            objCommand = Nothing
            mySqlAdapter = Nothing
            dbConnect.ChiudiDb()


    End Sub

    Protected Sub btn_CreateDepartmentClick()
        If Session("UserDeptId") <> "" Then
            Call sb_InsertUpdateDepartment("M", Session("UserDeptId"))
        Else
            Call sb_InsertUpdateDepartment("I", 0)
        End If

    End Sub

    Private Sub sb_InsertUpdateDepartment(strOp As String, IdDept As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Department"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_DEPT", IdDept)
                objCommand.Parameters.AddWithValue("@DESCRIPTION", txtCreateDepartment.Text)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblMessage.ForeColor = System.Drawing.Color.Red
                    lblMessage.Text = strError
                    Response.AppendHeader("Refresh", "1")
                ElseIf strOp = "I" Then
                    lblMessage.Text = "Department Created successfully !"
                    Response.AppendHeader("Refresh", "1;url=ViewDepartment.aspx")
                Else
                    lblMessage.Text = "Department Updated successfully !"
                    Response.AppendHeader("Refresh", "1;url=ViewDepartment.aspx")
                End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

End Class