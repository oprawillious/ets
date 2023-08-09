Public Class MenuSideBar
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblUserSession.Text = "Hi, " + Session("U")
    End Sub

    Protected Sub LogOut_Click(sender As Object, e As EventArgs)
        FormsAuthentication.SignOut()
        Response.Redirect("~/Login.aspx", True)
    End Sub

    Protected Sub LinkViewAccountSettings_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/ViewAccountSettings.aspx")
    End Sub

    Protected Sub btnSearchTicket_Click(sender As Object, e As EventArgs)
        Dim strTicketNumber = txtTicketNumber.Text
        Response.Redirect("")
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)

        Dim strTicketNumber = Me.GetText
        Response.Redirect("")
    End Sub

    Public Property GetText As String
        Get
            Return txtTicketNumber.Text
        End Get
        Set(ByVal value As String)
            txtTicketNumber.Text = value
        End Set
    End Property

    Protected Sub btnSearchTask_Click(sender As Object, e As EventArgs)

    End Sub
End Class