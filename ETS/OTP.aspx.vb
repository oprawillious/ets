Imports System.Net
Imports System.Threading

Public Class OTP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSubmitOTP_Click(sender As Object, e As EventArgs)

        Dim strUserName = Session("U")
        Dim strOtp = Session("O")
        Dim strPersistCookie As Boolean = Session("C")

        If strOtp = txtOtp.Text Then
            divloader.Visible = True
            Thread.Sleep("5000")
            FormsAuthentication.RedirectFromLoginPage(strUserName, strPersistCookie)
        End If

        lblMessage.Text = "Invalid OTP"
        divloader.Visible = False
        txtOtp.Text = ""

    End Sub

End Class