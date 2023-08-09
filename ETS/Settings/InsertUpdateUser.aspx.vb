Imports System.Data.SqlClient
Imports System.Drawing
Imports ETS.DataBase
Imports ETS.General
Imports System.Web.UI.UpdatePanel

Public Class InsertUpdateUser
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            'If fn_ValidatePageAccess() Or IsInRole(Session("R"), Roll_Kind.Administrator) Then
            If fn_ValidatePageAccess() Or Roll_Kind.Administrator Then
                If Not Page.IsPostBack Then
                    If Session("Id") <> "" Then
                        btnSaveUser.Text = "Update"
                        lblCreateUser.Text = "Edit User"
                        Call Popola_DropList_UserGroup()
                        Call Popola_DropList_HRManagers()
                        Call Popola_DropList_Dept()
                        Call Popola_DropList_Managers()
                        Call sb_LoadUserDetails()
                    Else
                        lblCreateUser.Text = "Create User"
                        Call Popola_DropList_UserGroup()
                        Call Popola_DropList_HRManagers()
                        Call Popola_DropList_Dept()
                        Call Popola_DropList_Managers()
                    End If
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
        DropListUserGroup.SelectedValue = 2
        DropListUserGroup.DataValueField = "ID_ROLES"
        DropListUserGroup.DataTextField = "ROLE_DESCRIPTION"
        DropListUserGroup.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_Dept()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT DISTINCT DESCRIPTION"
        strSQL = strSQL & ",ID_DEPARTMENT"
        strSQL = strSQL & " FROM DEPARTMENT WITH(NOLOCK);"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListDept.DataSource = objDataReader
        DropListDept.DataValueField = "ID_DEPARTMENT"
        DropListDept.DataTextField = "DESCRIPTION"
        DropListDept.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_HRManagers()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT DEPT+' - '+MANAGER_FULLNAME HR_MANAGER"
        strSQL = strSQL & ",ID_MANAGER_SETTING"
        strSQL = strSQL & " FROM vs_Managers WITH(NOLOCK)"
        strSQL = strSQL & " WHERE ISNULL(FLAG_HR,'N') = 'Y';"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListHrManager.DataSource = objDataReader
        DropListHrManager.DataValueField = "ID_MANAGER_SETTING"
        DropListHrManager.DataTextField = "HR_MANAGER"
        DropListHrManager.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_Managers()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strSQL As String

        strSQL = "SELECT  FIRST_NAME + ' ' + LAST_NAME MANAGER_NAME"
        strSQL = strSQL & " ,ID_MANAGER_SETTING"
        strSQL = strSQL & " FROM MANAGER_SETTING WITH(NOLOCK)"
        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione
        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListManager.DataSource = objDataReader
        DropListManager.DataValueField = "ID_MANAGER_SETTING"
        DropListManager.DataTextField = "MANAGER_NAME"
        DropListManager.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnSaveUser_Click(sender As Object, e As EventArgs)

        If btnSaveUser.Text = "Confirm" Then
            Call sb_InsertUpdateDeleteUser("I", 0)
        Else
            Call sb_InsertUpdateDeleteUser("M", Session("Id"))
        End If
        Response.Redirect("ViewApplicationUsers.aspx")
    End Sub

    Private Sub sb_InsertUpdateDeleteUser(strOp As String, IdUser As Integer)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String
        Dim response = hdS_UserRole.Value

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_User"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_USERS", IdUser)
                objCommand.Parameters.AddWithValue("@ID_ROLE", DropListUserGroup.SelectedValue)
                objCommand.Parameters.AddWithValue("@USERNAME", txtUsername.Text)
                objCommand.Parameters.AddWithValue("@FIRST_NAME", txtFirstName.Text)
                objCommand.Parameters.AddWithValue("@LAST_NAME", txtLastName.Text)
                objCommand.Parameters.AddWithValue("@EMAIL_ADDRESS", txtEmail.Text)
                objCommand.Parameters.AddWithValue("@PASSWORD", txtPassword.Text)

                objCommand.Parameters.AddWithValue("@ID_DEPT", DropListDept.SelectedValue)
                objCommand.Parameters.AddWithValue("@ID_HR_MANAGER", DropListHrManager.SelectedValue)
                objCommand.Parameters.AddWithValue("@ID_MANAGER_SETTING", DropListManager.SelectedValue)
                objCommand.Parameters.AddWithValue("@FLAG_ENABLED", "Y")

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strError <> "" Then
                    lblMessage.Text = strError
                ElseIf strOp = "I" Then
                    lblMessage.Text = "User Created successfully !"
                Else
                    lblMessage.Text = "User Updated successfully !"
                End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub txtConfirmPassword_TextChanged(sender As Object, e As EventArgs)
        If txtPassword.Text <> txtConfirmPassword.Text Then
            lblMessage.Text = "password does not match !!"
            lblMessage.ForeColor = Color.Red
            txtConfirmPassword.Text = ""
            txtPassword.Text = ""
        Else
            lblMessage.Text = ""
            txtPassword.Attributes("value") = txtPassword.Text
            txtConfirmPassword.Attributes("value") = txtConfirmPassword.Text
        End If

    End Sub

    Private Sub sb_LoadUserDetails()

        Dim dbConnect As New DataBase

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        Dim strSQL As String = ""
        strSQL = "SELECT FIRST_NAME"
        strSQL = strSQL + ", LAST_NAME"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", EMAIL_ADDRESS"
        strSQL = strSQL + ", USER_ROLE"
        strSQL = strSQL + ", DEPT"
        strSQL = strSQL + ", MANAGER"
        strSQL = strSQL + ", ID_ROLES"
        strSQL = strSQL + ", HR_MANAGER"
        strSQL = strSQL + ", HR_ID"
        strSQL = strSQL + "  FROM vs_Application_Users WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_USERS=" & Session("Id") & ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim myDataSet As New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        If objDataReader.HasRows Then
            objDataReader.Read()

            If Not IsNothing(objDataReader.Item("FIRST_NAME")) Then
                txtFirstName.Text = CStr(objDataReader.Item("FIRST_NAME") & "")
            End If
            If Not IsNothing(objDataReader.Item("LAST_NAME")) Then
                txtLastName.Text = CStr(objDataReader.Item("LAST_NAME") & "")
            End If
            If Not IsNothing(objDataReader.Item("USERNAME")) Then
                txtUsername.Text = CStr(objDataReader.Item("USERNAME") & "")
            End If
            If Not IsNothing(objDataReader.Item("EMAIL_ADDRESS")) Then
                txtEmail.Text = CStr(objDataReader.Item("EMAIL_ADDRESS") & "")
            End If
            If Not IsNothing(objDataReader.Item("USER_ROLE")) Then
                DropListUserGroup.SelectedValue = CStr(objDataReader.Item("ID_ROLES") & "")

            End If

            If Not IsNothing(objDataReader.Item("DEPT")) Then
                DropListDept.SelectedItem.Text = CStr(objDataReader.Item("DEPT") & "")
            End If

            If Not IsNothing(objDataReader.Item("MANAGER")) Then
                DropListManager.SelectedItem.Text = CStr(objDataReader.Item("MANAGER") & "")
            End If

            If Not IsNothing(objDataReader.Item("HR_MANAGER")) Then
                DropListHrManager.SelectedValue = CStr(objDataReader.Item("HR_ID") & "")
            End If
        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()

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

    Protected Sub DropListDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropListDept.SelectedIndexChanged
        Call Popola_DropList_Managers()
    End Sub

End Class