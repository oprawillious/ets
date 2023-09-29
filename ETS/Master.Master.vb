Imports System.Data.SqlClient

Public Class Master
    Inherits System.Web.UI.MasterPage

    Private ReadOnly connessioneDb As New DataBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Call fn_LoadMenuTop()
            lblUserSession.Text = "Hi, " + Session("U")

            If Session("U") = "" Then
                Response.Redirect("~/Login.aspx")
            End If

            lblAppVersion.Text = "Version " + Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
        End If
    End Sub


    Protected Function fn_LoadMenuTop() As Boolean

        Dim strIdRoles As String
        Dim blReturn As Boolean

        Try
            connessioneDb.connettidb()
            strIdRoles = Session("R")

            If strIdRoles <> "" Then

                Dim cmd1 As SqlDataAdapter = New SqlDataAdapter("SELECT DISTINCT ID_MENU_TOP, MENU FROM vs_Grant_Menu_Top WHERE ID_ROLES = " & strIdRoles & "" & " AND DISPLAY = 1", connessioneDb.Connessione)
                Dim ds As DataSet = New DataSet()
                cmd1.Fill(ds, "Menu")

                Dim cmd2 As SqlDataAdapter = New SqlDataAdapter("SELECT DISTINCT ID_MENU_TOP,ID_SUB_MENU_TOP,SUB_MENU ,URL_LINK FROM vs_Grant_Menu_Top WHERE ID_ROLES = " & strIdRoles & "" & " AND DISPLAY = 1", connessioneDb.Connessione)
                cmd2.Fill(ds, "SubMenu")
                ds.Relations.Add("SubMenu", ds.Tables("Menu").Columns("ID_MENU_TOP"), ds.Tables("SubMenu").Columns("ID_MENU_TOP"))

                RepeaterMenu.DataSource = ds.Tables("Menu")
                RepeaterMenu.DataBind()

                RepeaterMenuMobile.DataSource = ds.Tables("Menu")
                RepeaterMenuMobile.DataBind()

                blReturn = True
            End If

            connessioneDb.ChiudiDb()

        Catch ex As Exception
            blReturn = False
        End Try

        Return blReturn

    End Function

    Protected Sub LogOut_Click(sender As Object, e As EventArgs)
        FormsAuthentication.SignOut()
        Session.Abandon()
        Response.Redirect("~/Login.aspx", True)
    End Sub

    Protected Sub LinkViewAccountSettings_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Account/ViewAccountSettings.aspx")
    End Sub

    Protected Sub LnkSearchTicket_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/ViewSearchResult.aspx?Op=TK&Id=" + hdIdTicket.Value)
    End Sub

    Protected Sub LnkSearchTask_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/ViewSearchResult.aspx?Op=TS&Id=" + hdIdTask.Value)
    End Sub

    Protected Sub LnkSearchTestCase_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/ViewSearchResult.aspx?Op=TC&Id=" + hdIdTestCase.Value)
    End Sub

    Public Function fn_ReturnHomeUrl() As String
        Dim homeUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Dashboard/ViewDashboard.aspx"

        'Dim homeUrl As String = "https://" + Request.Url.DnsSafeHost + ":" + CStr(Request.Url.Port) + "/Dashboard/ViewDashboard.aspx"
        Return homeUrl
    End Function

    Protected Sub linkHomePage_Click(sender As Object, e As EventArgs)
        Response.Redirect(fn_ReturnHomeUrl())
    End Sub
End Class