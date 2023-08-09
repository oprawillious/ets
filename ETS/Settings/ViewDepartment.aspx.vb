Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General
Public Class ViewDepartment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack Then
                If Not IsInRole(Session("R"), Roll_Kind.Administrator) Then

                End If

                If IsInRole(Session("R"), Roll_Kind.Administrator) Then

                End If
                Call sb_ViewDepartment()
            End If
        End If
    End Sub



    Private Sub sb_ViewDepartment()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_DEPARTMENT"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + "  FROM vs_department WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"

        If txtSearchDepartment.Text <> "" Then
            strSQL = strSQL + " AND DESCRIPTION = " + "'" + txtSearchDepartment.Text + "'"
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
        gvViewDepartment.DataSource = myDataSet
        gvViewDepartment.DataBind()
        dbConnect.ChiudiDb()

    End Sub


    Protected Sub btn_SearchClick()
        Call sb_ViewDepartment()
    End Sub

    Protected Sub btn_CreateDepartmentClick()
        Session("UserDeptId") = String.Empty
        Response.Redirect("InsertUpdateDepartment.aspx")

    End Sub

    Protected Sub gvViewDepartment_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvViewDepartment.PageIndexChanging
        gvViewDepartment.PageIndex = e.NewPageIndex()
        Call sb_ViewDepartment()
    End Sub

    Protected Sub gvViewDepartment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViewDepartment.RowCommand

        If e.CommandName <> "Page" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvViewDepartment.Rows(Index)
            Dim strIdDepartment As String = TryCast(row.FindControl("hdIdDepartment"), HiddenField).Value
            Dim strDescription As String = TryCast(row.FindControl("hdDescription"), HiddenField).Value

            If e.CommandName = "Modifica" Then
                Session("UserDeptId") = strIdDepartment
                Response.Redirect("InsertUpdateDepartment.aspx")
            End If

            If e.CommandName = "Delete" Then
                Call sb_InsertUpdateDepartment("D", strIdDepartment)
                Response.Redirect("ViewDepartment.aspx")
            End If


        End If
    End Sub



    Private Sub sb_InsertUpdateDepartment(strOp As String, IdDept As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Department"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_DEPT", IdDept)
                objCommand.Parameters.AddWithValue("@DESCRIPTION", "")

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
End Class