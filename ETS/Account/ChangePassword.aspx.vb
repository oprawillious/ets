Imports System.Data.SqlClient

Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnResetPassword_Click(sender As Object, e As EventArgs)
        Call sb_ChangePassword()
    End Sub

    Private Sub sb_ChangePassword()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Change_Password"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@OLD_PASSWORD", txtOldPassword.Text)
                objCommand.Parameters.AddWithValue("@NEW_PASSWORD", txtNewPassword.Text)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblMessage.Text = strError
                Else
                    FormsAuthentication.SignOut()
                    Response.Redirect("~/Login.aspx")
                End If

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub
End Class