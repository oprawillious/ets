Imports System.Data.SqlClient

Public Class InsertUpdateManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated() Then
            If Not IsPostBack() Then
                If Session("IdMgtSettings") <> "" Then
                    lblCaption.Text = "Edit Manager"
                    Call Popola_DropList_Departments()
                    Call sb_LoadManagerDetails()
                    DropListUserDepartment.Enabled = False
                Else
                    Call Popola_DropList_Departments()
                End If
            End If
        End If
    End Sub

    Private Sub Popola_DropList_Departments()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String
        strSQL = "SELECT '' DESCRIPTION, 0 ID_DEPARTMENT UNION"
        strSQL = strSQL & " SELECT DISTINCT DESCRIPTION "
        strSQL = strSQL & ",ID_DEPARTMENT"
        strSQL = strSQL & " FROM vs_Department WITH(NOLOCK);"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListUserDepartment.DataSource = objDataReader
        DropListUserDepartment.DataValueField = "ID_DEPARTMENT"
        DropListUserDepartment.DataTextField = "DESCRIPTION"
        DropListUserDepartment.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub sb_LoadManagerDetails()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT DEPT"
        strSQL = strSQL + ", FIRST_NAME"
        strSQL = strSQL + ", LAST_NAME"
        strSQL = strSQL + ", EMAIL"
        strSQL = strSQL + ", APPROVAL_PRIVILEDGE"
        strSQL = strSQL + ", ISNULL(FLAG_HR,'N') FLAG_HR"
        strSQL = strSQL + ", ISNULL(FLAG_ACCOUNT_MANAGER,'N') FLAG_ACCOUNT_MANAGER"
        strSQL = strSQL + "  FROM vs_Managers WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"
        strSQL = strSQL + "  AND ID_MANAGER_SETTING=" & Session("IdMgtSettings") & ""

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

            If Not IsNothing(objDataReader.Item("DEPT")) Then
                DropListUserDepartment.SelectedItem.Text = CStr(objDataReader.Item("DEPT") & "")
            End If

            If Not IsNothing(objDataReader.Item("FIRST_NAME")) Then
                txtFirstname.Text = CStr(objDataReader.Item("FIRST_NAME") & "")
            End If

            If Not IsNothing(objDataReader.Item("LAST_NAME")) Then
                txtLastname.Text = CStr(objDataReader.Item("LAST_NAME") & "")
            End If

            If Not IsNothing(objDataReader.Item("EMAIL")) Then
                txtEmail.Text = CStr(objDataReader.Item("EMAIL") & "")
            End If

            If Not IsNothing(objDataReader.Item("APPROVAL_PRIVILEDGE")) Then
                DropDownAprroveHolidayPriviledge.Text = CStr(objDataReader.Item("APPROVAL_PRIVILEDGE") & "")
            End If

            If Not IsNothing(objDataReader.Item("FLAG_HR")) Then
                If (objDataReader.Item("FLAG_HR")) = "Y" Then
                    chkHrmanager.Checked = True
                End If
            End If

            If Not IsNothing(objDataReader.Item("FLAG_ACCOUNT_MANAGER")) Then
                If (objDataReader.Item("FLAG_ACCOUNT_MANAGER")) = "Y" Then
                    chkAccountManager.Checked = True
                End If
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()
    End Sub


    Private Sub sb_InsertUpdateManager(strOp As String, strIdManager As Integer)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Managers"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_MANAGER", strIdManager)
                objCommand.Parameters.AddWithValue("@FIRST_NAME", txtFirstname.Text)
                objCommand.Parameters.AddWithValue("@ID_DEPARTMENT", DropListUserDepartment.SelectedValue)
                objCommand.Parameters.AddWithValue("@LAST_NAME ", txtLastname.Text)
                objCommand.Parameters.AddWithValue("@EMAIL", txtEmail.Text)
                objCommand.Parameters.AddWithValue("@APPROVAL_PRIVILEDGE", DropDownAprroveHolidayPriviledge.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@FLAG_HR ", IIf(chkHrmanager.Checked, "Y", "N"))
                'objCommand.Parameters.AddWithValue("@FLAG_ACC_MANAGER ", IIf(chkAccountManager.Checked, "Y", "N"))

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_MANAGER_SETTING", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100


                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                'hdManager_settings_id.Value = CInt(objCommand.Parameters("@ID_MANAGER_SETTING").Value)

                If strError <> "" Then
                    lblMessage.Text = strError
                ElseIf strOp = "I" Then
                    lblMessage.Text = "Manager Created successfully !"
                    Response.AppendHeader("Refresh", "1;url=ViewManagers.aspx")
                Else
                    lblMessage.Text = "Manager Updated successfully !"
                    Response.AppendHeader("Refresh", "1;url=ViewManagers.aspx")
                End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub btn_CreateManagerClick()
        If Session("IdMgtSettings") <> "" Then
            Call sb_InsertUpdateManager("M", Session("IdMgtSettings"))
        Else
            Call sb_InsertUpdateManager("I", 0)
        End If

    End Sub

    Protected Sub chkHrmanager_CheckedChanged(sender As Object, e As EventArgs) Handles chkHrmanager.CheckedChanged
        If chkHrmanager.Checked Then
            chkAccountManager.Enabled = False
        Else
            chkAccountManager.Enabled = True
        End If
    End Sub

    Protected Sub chkAccountManager_CheckedChanged(sender As Object, e As EventArgs) Handles chkAccountManager.CheckedChanged
        If chkAccountManager.Checked Then
            chkHrmanager.Enabled = False
        Else
            chkHrmanager.Enabled = True
        End If
    End Sub
End Class