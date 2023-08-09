Public Class Otp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnValidate_Click(sender As Object, e As EventArgs)
        If txtOtp.Text <> "" Then
            Dim newOrgID As Integer = 0
            If HttpContext.Current.Session("Otp") IsNot Nothing Then
                Integer.TryParse(HttpContext.Current.Session("Otp").ToString(), newOrgID)
            Else
                lblMessage.Text = "Session has Expire please try again Thanks"
                lblMessage.Visible = True
            End If
            If txtOtp.Text <> newOrgID Then
                lblMessage.Text = "Invalid Otp please check and try again Thanks"
            Else
                Response.Redirect("/DASHBOARD/ViewDashboard.aspx")
            End If
        Else
            lblMessage.Text = "please Enter Otp"
            txtOtp.Focus()
        End If


    End Sub
End Class