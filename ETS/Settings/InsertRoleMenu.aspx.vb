Imports System.Data.SqlClient
Imports System.Drawing
Imports ETS.DataBase
Imports ETS.General

Public Class InsertUpdateMenu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If fn_ValidatePageAccess() Or IsInRole(Session("R"), Roll_Kind.Administrator) Then
                If Not IsPostBack Then
                    Call Popola_DropList_UserGroup()
                    Call Popola_DropList_MenuTop()
                End If
            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")
            End If
        End If

    End Sub

    Private Sub Popola_DropList_UserGroup()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT DISTINCT ROLE_DESCRIPTION"
        strSQL = strSQL & ",ID_ROLES"
        strSQL = strSQL & " FROM ROLES WITH(NOLOCK);"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListUserGroup.DataSource = objDataReader
        DropListUserGroup.DataValueField = "ID_ROLES"
        DropListUserGroup.DataTextField = "ROLE_DESCRIPTION"
        DropListUserGroup.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_MenuTop()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT DISTINCT DESCRIPTION"
        strSQL = strSQL & ",ID_MENU_TOP"
        strSQL = strSQL & " FROM MENU_TOP WITH(NOLOCK);"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListMenu.DataSource = objDataReader
        DropListMenu.DataValueField = "ID_MENU_TOP"
        DropListMenu.DataTextField = "DESCRIPTION"
        DropListMenu.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub btnSaveRoleMenu_Click(sender As Object, e As EventArgs)
        Call sb_InsertRoleMenu(DropListUserGroup.SelectedValue, DropListMenu.SelectedValue)
        Response.Redirect("ViewRoleMenu.aspx")
    End Sub

    Private Sub sb_InsertRoleMenu(strIdRole As String, strIdMenuTop As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Delete_Role_Menu"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "I")
                objCommand.Parameters.AddWithValue("@ID_ROLES_MENU_TOP", 0)
                objCommand.Parameters.AddWithValue("@ID_ROLE", strIdRole)
                objCommand.Parameters.AddWithValue("@ID_MENU_TOP", strIdMenuTop)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblMessage.Text = strError
                    lblMessage.ForeColor = Color.Red

                Else
                    lblMessage.Text = "Menu updated successfully !"
                    lblMessage.ForeColor = Color.Green
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

End Class