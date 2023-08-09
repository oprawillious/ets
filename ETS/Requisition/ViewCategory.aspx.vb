Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General


Public Class ViewCategory
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Page.User.Identity.IsAuthenticated Then
			If Not Page.IsPostBack Then
				If Not IsInRole(Session("R"), Roll_Kind.Administrator) Then

				End If

				If IsInRole(Session("R"), Roll_Kind.Administrator) Then

				End If
				Call sb_ViewCategory()
			End If
		End If

	End Sub




	Protected Sub btn_SearchCategory_Click()
		Call sb_ViewCategory()

	End Sub

	Protected Sub btn_CreateCategory_Click()
		Session("IdCategory") = String.Empty
		Response.Redirect("AddNewCategory.aspx")

	End Sub


	Private Sub sb_ViewCategory()

		Dim dbConnect As New DataBase
		Dim strSQL As String

		If dbConnect.StatoConnessione = 0 Then
			dbConnect.connettidb()
		End If

		strSQL = "SELECT ID_ITEM_CATEGORY"
		strSQL = strSQL + ", DESCRIPTION"
		strSQL = strSQL + "  FROM vs_Item_Category WITH(NOLOCK)"
		strSQL = strSQL + "  WHERE 1 = 1"

		If txtSearchCategory.Text <> "" Then
			'strSQL = strSQL + " AND DESCRIPTION = " + "'" + txtSearchCategory.Text + "'"
			strSQL = strSQL + " AND DESCRIPTION LIKE '%" + txtSearchCategory.Text + "%'"
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
		gvViewCategory.DataSource = myDataSet
		gvViewCategory.DataBind()
		dbConnect.ChiudiDb()

	End Sub
	Protected Sub gvViewCategory_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViewCategory.RowCommand
		If e.CommandName <> "Page" Then
			Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
			Dim row As GridViewRow = gvViewCategory.Rows(Index)
			Dim strIdCategory As String = TryCast(row.FindControl("hdItemCategory"), HiddenField).Value
			Dim strDescription As String = TryCast(row.FindControl("hdDescription"), HiddenField).Value

			If e.CommandName = "Modifica" Then
				Session("IdCategory") = strIdCategory
				Response.Redirect("AddNewCategory.aspx")
			End If

			If e.CommandName = "Delete" Then
				Call sb_InsertUpdateDeleteItemCategory("D", strIdCategory)
				Response.Redirect("ViewCategory.aspx")
			End If


		End If

	End Sub

	Private Sub sb_InsertUpdateDeleteItemCategory(strOp As String, IdItem As String)
		Dim connessioneDb As New DataBase
		Dim objCommand As New SqlCommand
		Dim mySqlAdapter As New SqlDataAdapter(objCommand)
		Dim strError As String

		If connessioneDb.StatoConnessione = 0 Then
			connessioneDb.connettidb()

			If connessioneDb.StatoConnessione > 0 Then

				objCommand.CommandType = CommandType.StoredProcedure
				objCommand.CommandText = "GUI_Insert_Update_Delete_Item_Category"
				objCommand.Connection = connessioneDb.Connessione

				objCommand.Parameters.AddWithValue("@OP", strOp)
				objCommand.Parameters.AddWithValue("@DESCRIPTION", "")
				objCommand.Parameters.AddWithValue("@ID_ITEM", IdItem)

				Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
				objCommand.Parameters.Add(objOutputParameter)
				objOutputParameter.Direction = ParameterDirection.Output
				objOutputParameter.Size = 100

				Dim objOutputParameter1 As New SqlParameter("@ID_ITEM_CATEGORY", SqlDbType.BigInt)
				objCommand.Parameters.Add(objOutputParameter1)
				objOutputParameter1.Direction = ParameterDirection.Output
				objOutputParameter1.Size = 100

				objCommand.ExecuteNonQuery()
				strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)

				If strError <> "" Then
					lblMessage.ForeColor = System.Drawing.Color.Red
					lblMessage.Text = strError
					Response.AppendHeader("Refresh", "1")
				ElseIf strOp = "I" Then
					lblMessage.Text = "Item Added Successfully !"
					Response.AppendHeader("Refresh", "1;url=ViewCategory.aspx")
				Else
					lblMessage.Text = "Item Updated Successfully !"
					Response.AppendHeader("Refresh", "1;url=ViewCategory.aspx")
				End If

			End If

		End If

		connessioneDb.ChiudiDb()




	End Sub


	Protected Sub gvViewCategory_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvViewCategory.PageIndexChanging
		gvViewCategory.PageIndex = e.NewPageIndex()
		Call sb_ViewCategory()

	End Sub


End Class