Imports System.Data.SqlClient

Public Class ViewAccountSettings
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not IsPostBack Then
                Call sb_LoadUserDetails()
            End If
        End If
    End Sub

    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Account/ChangePassword.aspx")
    End Sub

    Private Sub sb_LoadUserDetails()
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT FIRST_NAME"
        strSQL = strSQL + ", LAST_NAME"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", EMAIL_ADDRESS"
        strSQL = strSQL + ", USER_ROLE"
        strSQL = strSQL + "  FROM vs_Application_Users WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE USERNAME =" & "'" & Page.User.Identity.Name & "'" & ""

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

            If Not IsNothing(objDataReader.Item("FIRST_NAME")) And Not IsNothing(objDataReader.Item("LAST_NAME")) Then
                lblFullName.Text = CStr(objDataReader.Item("FIRST_NAME") & "") + " " + CStr(objDataReader.Item("LAST_NAME") & "")
            End If
            If Not IsNothing(objDataReader.Item("USERNAME")) Then
                lblUserName.Text = CStr(objDataReader.Item("USERNAME") & "")
            End If
            If Not IsNothing(objDataReader.Item("EMAIL_ADDRESS")) Then
                lblEmail.Text = CStr(objDataReader.Item("EMAIL_ADDRESS") & "")
            End If
            If Not IsNothing(objDataReader.Item("USER_ROLE")) Then
                lblRole.Text = CStr(objDataReader.Item("USER_ROLE") & "")
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()
    End Sub
End Class