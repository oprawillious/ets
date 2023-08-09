Imports System.Data.SqlClient


Public Class ResetPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub sb_ResetPassword()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Reset_Password"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@EMAIL", txtEmail.Text)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblMessage.Text = strError
                End If

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnResetPassword_Click(sender As Object, e As EventArgs)

        If txtEmail.Text <> "" Then
            Call sb_ResetPassword()
            lblMessage.Text = "password reset succesfully. please check your email."
            ''Response.Headers(3, "Refresh")
        Else
            lblMessage.Text = "Email address is mandatory !"
        End If

    End Sub
End Class