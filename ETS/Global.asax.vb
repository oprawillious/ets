Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

    End Sub

    Public Sub Application_PostAuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        'Dim strIp As String = HttpContext.Current.Request.UserHostAddress
        'If Request.Url.Scheme = "https" Then
        '    Dim localSslPort As String = 443
        '    Dim strpath = "https://" & Request.Url.Host & ":" + localSslPort + Request.Url.PathAndQuery
        '    If strIp IsNot strpath And
        '    HttpContext.Current IsNot Nothing And
        '    HttpContext.Current.User IsNot Nothing And
        '    HttpContext.Current.User.Identity.IsAuthenticated Then
        '        Response.Redirect("~/Login.aspx")
        '    End If
        'End If
        'Throw New NotImplementedException

    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)

        Dim cookieName As String
        cookieName = FormsAuthentication.FormsCookieName
        Dim authCookie As HttpCookie
        authCookie = Context.Request.Cookies(cookieName)

        If (authCookie IsNot Nothing) Then
            authCookie.Expires = Now
            authCookie.Value = String.Empty
            authCookie.Values.Remove(cookieName)
            Return
        End If

        If (authCookie Is Nothing) Then
            Return
        End If

        Dim authTicket As FormsAuthenticationTicket
        authTicket = Nothing

        Try
            authTicket = FormsAuthentication.Decrypt(authCookie.Value)
        Catch ex As Exception
            ' Log exception details (omitted for simplicity)
            Return
        End Try
    End Sub

    'Private Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim context = HttpContext.Current
    '    If Not context.Request.Path.EndsWith("Login.aspx") Then
    '        Response.Redirect("~/Login.aspx")
    '    End If
    'End Sub

End Class