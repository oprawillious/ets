Imports System.Data.SqlClient
Public Class ViewApplicationUsers
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If fn_ValidatePageAccess() Then
                If Not IsPostBack Then
                    Call sb_LoadViewUsers("")
                End If
            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")
            End If
        End If
    End Sub

    Private Sub sb_LoadViewUsers(strUsername As String)
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_USERS"
        strSQL = strSQL + ", FIRST_NAME"
        strSQL = strSQL + ", LAST_NAME"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", EMAIL_ADDRESS"
        strSQL = strSQL + ", USER_ROLE"
        strSQL = strSQL + "  FROM vs_Application_Users WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"

        If strUsername <> "" Then
            strSQL = strSQL + "AND USERNAME LIKE" + "'%" + strUsername + "%'" + ""
        End If

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvUsers.DataSource = myDataSet
        gvUsers.DataBind()
        dbConnect.ChiudiDb()
    End Sub

    Protected Sub btnSearchUser_Click(sender As Object, e As EventArgs)
        Call sb_LoadViewUsers(txtUsername.Text)
    End Sub

    Protected Sub btnNewUser_Click(sender As Object, e As EventArgs)
        Session("Id") = String.Empty
        Response.Redirect("InsertUpdateUser.aspx")
    End Sub

    Protected Sub gvUsers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUsers.RowCommand
        If e.CommandName <> "Page" Then
            Call sb_LoadViewUsers("")
        End If

        If e.CommandName = "Modifica" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            'Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvUsers.Rows(Index)
            Dim strIdUser As String = TryCast(row.FindControl("hdUserId"), HiddenField).Value
            Session("Id") = strIdUser
            Response.Redirect("InsertUpdateUser.aspx")
        End If

        If e.CommandName = "Remove" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            'Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvUsers.Rows(Index)
            Dim strIdUser As String = TryCast(row.FindControl("hdUserId"), HiddenField).Value
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", "Confirm()", True)
            Call sb_InsertUpdateDeleteUser("D", strIdUser)
            Response.Redirect("ViewApplicationUsers.aspx", True)
        End If

    End Sub

    Private Sub sb_InsertUpdateDeleteUser(strOp As String, IdUser As Integer)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_User"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_USERS", IdUser)
                objCommand.Parameters.AddWithValue("@ID_ROLE", "")
                objCommand.Parameters.AddWithValue("@USERNAME", "")
                objCommand.Parameters.AddWithValue("@FIRST_NAME", "")
                objCommand.Parameters.AddWithValue("@LAST_NAME", "")
                objCommand.Parameters.AddWithValue("@EMAIL_ADDRESS", "")
                objCommand.Parameters.AddWithValue("@PASSWORD", "")
                objCommand.Parameters.AddWithValue("@ID_DEPT", "")
                objCommand.Parameters.AddWithValue("@ID_HR_MANAGER", "")
                objCommand.Parameters.AddWithValue("@FLAG_ENABLED", "")
                objCommand.Parameters.AddWithValue("@ID_MANAGER_SETTING", "")

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strOp = "D" And strError = "" Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", "Confirm()", True)
                End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

    Private Function fn_ValidatePageAccess() As Boolean
        Dim general As New General
        Dim pageUrl = general.fn_GetAbsolutePath()
        Dim Access = False

        If general.fn_GrantWebInterface(pageUrl, Session("R")) Then
            Access = True
        End If

        Return Access
    End Function


    Protected Sub gvTestCase_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvUsers.PageIndexChanging
        gvUsers.PageIndex = e.NewPageIndex
        Call sb_LoadViewUsers("")

    End Sub
End Class