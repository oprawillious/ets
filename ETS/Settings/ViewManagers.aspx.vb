Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General
Public Class viewManagers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack Then
                If IsInRole(Session("R"), Roll_Kind.Administrator) Then
                    Call sb_ViewManagers()
                End If
            End If
        End If
    End Sub

    Private Sub sb_ViewManagers()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_MANAGER_SETTING"
        strSQL = strSQL + ", DEPT"
        strSQL = strSQL + ", FLAG_HR"
        strSQL = strSQL + ", MANAGER_FULLNAME"
        strSQL = strSQL + ", EMAIL"
        strSQL = strSQL + "  FROM vs_managers WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"

        If txtSearchManager.Text <> "" Then
            strSQL = strSQL + " AND MANAGER_FULLNAME LIKE '%" + txtSearchManager.Text + "%'"
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
        gvViewManagers.DataSource = myDataSet
        gvViewManagers.DataBind()
        dbConnect.ChiudiDb()
    End Sub

    Protected Sub gvViewManagers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViewManagers.RowCommand

        Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
        Dim row As GridViewRow = gvViewManagers.Rows(Index)
        Dim strIdManager As String = TryCast(row.FindControl("hdIdManagerSettings"), HiddenField).Value
        Dim strDept As String = TryCast(row.FindControl("hdDescription"), HiddenField).Value
        'Dim strFlagHr As Boolean = TryCast(row.FindControl(" hdflaghr"), HiddenField).Value

        If e.CommandName = "Modifica" Then
            Session("IdMgtSettings") = strIdManager
            'Session("FlagHr") = strFlagHr
            Response.Redirect("InsertUpdateManager.aspx")
        End If

        If e.CommandName = "Delete" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", "Confirm()", True)
            Call sb_InsertUpdateManager("D", strIdManager)
            Response.Redirect("ViewManagers.aspx")

        End If

    End Sub


    Private Sub sb_InsertUpdateManager(strOp As String, strIdManager As String)
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
                objCommand.Parameters.AddWithValue("@ID_DEPARTMENT", "")
                objCommand.Parameters.AddWithValue("@FIRST_NAME", "")
                objCommand.Parameters.AddWithValue("@LAST_NAME ", "")
                objCommand.Parameters.AddWithValue("@EMAIL", "")
                objCommand.Parameters.AddWithValue("@FLAG_HR ", "")
                'objCommand.Parameters.AddWithValue("@FLAG_ACC_MANAGER ", "")

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

                If strOp = "D" And strError = "" Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm", "Confirm()", True)
                End If


                'hdManager_settings_id.Value = CInt(objCommand.Parameters("@ID_MANAGER_SETTING").Value)

                'If strError <> "" Then
                '    lblMessage.Text = strError
                'ElseIf strOp = "I" Then
                '    lblMessage.Text = "Manager Created successfully !"
                '    Response.AppendHeader("Refresh", "1;url=ViewManagers.aspx")
                'Else
                '    lblMessage.Text = "Manager Updated successfully !"
                '    Response.AppendHeader("Refresh", "1;url=ViewManagers.aspx")
                'End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub


    Protected Sub btn_SearchClick()
        Call sb_ViewManagers()
    End Sub

    Protected Sub btn_CreateManagerClick()
        Session("IdMgtSettings") = String.Empty
        Response.Redirect("InsertUpdateManager.aspx")
    End Sub

    Protected Sub gvViewManagers_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvViewManagers.PageIndexChanging
        gvViewManagers.PageIndex = e.NewPageIndex()
        Call sb_ViewManagers()
    End Sub
End Class