Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Threading
Imports Stimulsoft.Data.Expressions.NCalc

Public Class Login1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Request.Cookies("USER") IsNot Nothing And Request.Cookies("PASS") IsNot Nothing Then
                txtUsername.Text = Request.Cookies("USER").Value
                txtPassword.Attributes("value") = Request.Cookies("PASS").Value
                chkPersistCookie.Checked = True
                Response.Cookies("USER").Expires = New DateTime(2050, 4, 1)
                Response.Cookies("PASS").Expires = New DateTime(2050, 4, 1)
            End If
        End If

        Call sb_DetectMobile()
        Call sb_ResetHolidayUsedDays()

    End Sub

    Private Function fn_ValidateUser(Username As String, Password As String) As Boolean

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim boolVal As Boolean

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_LOGIN"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@USERNAME", Username)
                objCommand.Parameters.AddWithValue("@PASSWORD", Password)

                Dim objOutputParameter As New SqlParameter("@RETURN_VALUE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_ROLE", SqlDbType.Int)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                Dim objOutputParameter2 As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter2)
                objOutputParameter2.Direction = ParameterDirection.Output
                objOutputParameter2.Size = 100

                Dim objOutputParameter3 As New SqlParameter("@ID_DEPT", SqlDbType.Int)
                objCommand.Parameters.Add(objOutputParameter3)
                objOutputParameter3.Direction = ParameterDirection.Output
                objOutputParameter3.Size = 100

                objCommand.ExecuteNonQuery()
                Session("R") = CInt(objCommand.Parameters("@ID_ROLE").Value)
                Session("D") = CInt(objCommand.Parameters("@ID_DEPT").Value)
                boolVal = CBool(objCommand.Parameters("@RETURN_VALUE").Value)
            End If
        End If

        connessioneDb.ChiudiDb()
        Return boolVal
        'Call CheckDomain()
    End Function



    Private Function CheckDomain()

        Dim allowedDomains As HashSet(Of String) =
            New HashSet(Of String)() From {
        "ptml.lan",
        "PTML"
    }
        Dim domainName As String = NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName
        If Not allowedDomains.Contains(domainName) Then
            Call GenerateOTP()

            Response.Redirect("Otp.aspx")
        End If

    End Function
    Public Function GenerateOTP()
        Dim OTPLength As String = "4"
        Dim OTP As String = String.Empty
        Dim Chars As String = String.Empty
        Chars = "1,2,3,4,5,6,7,8,9,0"
        Dim seplitChar As Char() = {","c}
        Dim arr As String() = Chars.Split(seplitChar)
        Dim NewOTP As String = ""
        Dim temp As String = ""
        Dim rand As Random = New Random()

        For i As Integer = 0 To Convert.ToInt32(OTPLength) - 1
            temp = arr(rand.[Next](0, arr.Length))
            NewOTP += temp
            OTP = NewOTP
        Next

        Session("Otp") = OTP
        Call sb_SendOtp(OTP, txtUsername.Text)
    End Function
    Private Sub sb_SendOtp(otp As String, userId As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        If connessioneDb.StatoConnessione > 0 Then

            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "GUI_Send_Otp"
            objCommand.Connection = connessioneDb.Connessione

            objCommand.Parameters.AddWithValue("@Otp", otp)
            objCommand.Parameters.AddWithValue("@UserId", userId)

            Dim objOutputParameter As New SqlParameter("@EMAIL", SqlDbType.NVarChar)
            objCommand.Parameters.Add(objOutputParameter)
            objOutputParameter.Direction = ParameterDirection.Output
            objOutputParameter.Size = 100

            Dim objDataReader As SqlDataReader
            objDataReader = objCommand.ExecuteReader()

            objDataReader.Close()
            objCommand = Nothing
            connessioneDb.ChiudiDb()
        End If
    End Sub


    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)

        divloader.Visible = True
        If chkPersistCookie.Checked Then
            Response.Cookies("USER").Value = txtUsername.Text
            Response.Cookies("PASS").Value = txtPassword.Text
        Else
            'Response.Cookies("USER").Value = ""
            'Response.Cookies("PASS").Value = ""

            Response.Cookies("USER").Expires = DateTime.Now
            Response.Cookies("PASS").Expires = DateTime.Now

        End If

        If txtUsername.Text <> "" And txtPassword.Text <> "" Then
            divloader.Visible = True
            If fn_ValidateUser(txtUsername.Text, txtPassword.Text) Then
                Session("U") = txtUsername.Text
                'Thread.Sleep("5000")
                FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, chkPersistCookie.Checked)
                Call CheckDomain()
            Else
                lblMessage.Text = "Invalid username or password !"
                divloader.Visible = False
            End If
        Else
            lblMessage.Text = "Please enter your Credentials !"
            divloader.Visible = False
        End If

    End Sub

    Private Sub sb_ResetHolidayUsedDays()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Reset_Holiday_Used_Days"
                objCommand.Connection = connessioneDb.Connessione

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()

            End If
        End If

        connessioneDb.ChiudiDb()
    End Sub

    Private Sub sb_DetectMobile()
        Dim u As String = Request.ServerVariables("HTTP_USER_AGENT")
        Dim b As New Regex("(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase)
        Dim v As New Regex("1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase)

        If b.IsMatch(u) Or v.IsMatch(Left(u, 4)) Then
            Response.Redirect("http://detectmobilebrowser.com/mobile")
        End If
    End Sub

End Class