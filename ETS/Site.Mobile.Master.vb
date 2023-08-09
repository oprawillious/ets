Public Class Site_Mobile
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub LinkBackToDashboard_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/DASHBOARD/ViewDashboard.aspx")
    End Sub
End Class