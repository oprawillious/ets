Imports System.Data.SqlClient

Public Class AddNewItem
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Page.User.Identity.IsAuthenticated() Then
			If Not IsPostBack Then
				If Session("IdCategory") <> "" Then
					Call sb_LoadItems()
				End If
			End If
		End If

	End Sub

	Private Sub sb_LoadItems()

		Dim dbConnect As New DataBase
		Dim strSQL As String

		If dbConnect.StatoConnessione = 0 Then
			dbConnect.connettidb()
		End If

		strSQL = "SELECT ID_ITEM_CATEGORY"
		strSQL = strSQL + ", DESCRIPTION"
		strSQL = strSQL + "  FROM vs_Item_Category WITH(NOLOCK)"
		strSQL = strSQL + "  WHERE 1 = 1"
		strSQL = strSQL + "  AND ID_ITEM_CATEGORY =" & Session("IdCategory") & ""



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

			If Not IsNothing(objDataReader.Item("DESCRIPTION")) Then
				txtAddNewItem.Text = CStr(objDataReader.Item("DESCRIPTION") & "")
			End If


		End If

		objDataReader.Close()
		objCommand = Nothing
		mySqlAdapter = Nothing
		dbConnect.ChiudiDb()

	End Sub


	Protected Sub btn_AddNewItem_Click()
		If Session("IdCategory") <> "" Then
			Call sb_InsertUpdateDeleteItemCategory("M", Session("IdCategory"))
		Else
			Call sb_InsertUpdateDeleteItemCategory("I", 0)
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
				objCommand.Parameters.AddWithValue("@DESCRIPTION", txtAddNewItem.Text)
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
					lblMessage.ForeColor = System.Drawing.Color.Green
					lblMessage.Text = "Item Added Successfully !"
					Response.AppendHeader("Refresh", "1;url=ViewCategory.aspx")
				Else
					lblMessage.ForeColor = System.Drawing.Color.Green
					lblMessage.Text = "Item Updated Successfully !"
					Response.AppendHeader("Refresh", "1;url=ViewCategory.aspx")
				End If

			End If

		End If

		connessioneDb.ChiudiDb()




	End Sub


End Class