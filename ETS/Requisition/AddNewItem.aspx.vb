Imports System.Data.SqlClient

Public Class AddNewItem1
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Page.User.Identity.IsAuthenticated Then
			If Not IsPostBack Then
				'If Session("RequsitionId") <> "" Then
				'	Call sb_LoadRequisitionDetails()
				'End If
				Call Popola_DropList_ItemCategory_Description()
			End If
		Else
			FormsAuthentication.SignOut()
			Response.Redirect("~/Login.aspx")
		End If

	End Sub

	Protected Sub DropListComputerInfo_SelectedIndexChanged(sender As Object, e As EventArgs)

	End Sub

	Private Sub Popola_DropList_ItemCategory_Description()

		Dim connessioneDb As New DataBase
		Dim objCommand As New SqlCommand
		Dim mySqlAdapter As New SqlDataAdapter(objCommand)

		Dim strSQL As String
		strSQL = "SELECT '' DESCRIPTION, 0 ID_ITEM_CATEGORY UNION"
		strSQL = strSQL & " SELECT DISTINCT DESCRIPTION "
		strSQL = strSQL & ",ID_ITEM_CATEGORY"
		strSQL = strSQL & " FROM  vs_Item_Category WITH(NOLOCK);"

		If connessioneDb.StatoConnessione = 0 Then
			connessioneDb.connettidb()
		End If

		objCommand.CommandText = strSQL
		objCommand.CommandType = CommandType.Text
		objCommand.Connection = connessioneDb.Connessione

		Dim objDataReader As SqlDataReader
		objDataReader = objCommand.ExecuteReader()

		DropListPeripheralDescription.DataSource = objDataReader
		DropListPeripheralDescription.DataValueField = "ID_ITEM_CATEGORY"
		DropListPeripheralDescription.DataTextField = "DESCRIPTION"
		DropListPeripheralDescription.DataBind()

		objDataReader.Close()
		connessioneDb.ChiudiDb()

	End Sub

	Private Sub sb_InsertUpdateDeleteStock(strOp As String)

		Dim connessioneDb As New DataBase
		Dim objCommand As New SqlCommand
		Dim mySqlAdapter As New SqlDataAdapter(objCommand)
		Dim strErrorStored As String

		If connessioneDb.StatoConnessione = 0 Then
			connessioneDb.connettidb()

			If connessioneDb.StatoConnessione > 0 Then
				objCommand.CommandType = CommandType.StoredProcedure
				objCommand.CommandText = "GUI_Insert_Update_Delete_Item"
				objCommand.Connection = connessioneDb.Connessione
				objCommand.Parameters.AddWithValue("@OP", strOp)
				objCommand.Parameters.AddWithValue("@ID_ITEM_CAT", DropListPeripheralDescription.SelectedValue)
				objCommand.Parameters.AddWithValue("@ID_ITEMS", 0)
				objCommand.Parameters.AddWithValue("@DESCRIPTION", txtDescription.Text)
				objCommand.Parameters.AddWithValue("@FLAG_AVAILABLE", "")
				objCommand.Parameters.AddWithValue("@MANUFACTURER", txtmanufacturer.Text)
				objCommand.Parameters.AddWithValue("@MODEL", txtmodel.Text)
				objCommand.Parameters.AddWithValue("@MODEL_NO", txtModelNo.Text)
				objCommand.Parameters.AddWithValue("@SERIALNO", txtSerialNo.Text)
				objCommand.Parameters.AddWithValue("@PROCESSOR", txtProcessor.Text)
				objCommand.Parameters.AddWithValue("@SCREENSIZE", txtScreenSize.Text)
				objCommand.Parameters.AddWithValue("@RAM", txtRam.Text)
				objCommand.Parameters.AddWithValue("@STORAGE", txtStorage.Text)


				Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
				objCommand.Parameters.Add(objOutputParameter)
				objOutputParameter.Direction = ParameterDirection.Output
				objOutputParameter.Size = 100

				Dim objOutputParameter1 As New SqlParameter("@ID_ITEM", SqlDbType.BigInt)
				objCommand.Parameters.Add(objOutputParameter1)
				objOutputParameter1.Direction = ParameterDirection.Output
				objOutputParameter1.Size = 100

				objCommand.ExecuteReader()

				strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
				hdItemsId.Value = CInt(objCommand.Parameters("@ID_ITEM").Value)
				Session("hdIdITem") = hdItemsId.Value

				If strErrorStored <> "" Then
					lblMessage.Text = strErrorStored

				ElseIf strOp = "I" Then
					lblMessage.ForeColor = System.Drawing.Color.Green
					lblMessage.Text = "Item Added Succesfully !"
					Response.AppendHeader("Refresh", "2;url=ViewItem.aspx")


				Else
					lblMessage.ForeColor = System.Drawing.Color.Green
					lblMessage.Text = "Item Updated Successfully !"
					Response.AppendHeader("Refresh", "2;url=ViewItem.aspx")
				End If

			End If

		End If

		connessioneDb.ChiudiDb()
	End Sub

	Protected Sub btnSave_Stock_Click()
		Call sb_InsertUpdateDeleteStock("I")

	End Sub


End Class