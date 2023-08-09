Imports System.Data.SqlClient

Public Class StockIn
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Page.User.Identity.IsAuthenticated Then
			If Not IsPostBack Then
				Call Popola_DropList_ItemCategory_Description()
			End If
		Else
			FormsAuthentication.SignOut()
			Response.Redirect("~/Login.aspx")
		End If

	End Sub


    Protected Sub btnAddStock_Click()
        Call sb_InsertUpdateDeletePurchasedItem("I")
    End Sub

    Private Sub sb_InsertUpdateDeletePurchasedItem(strOp As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Purchased_Item"
                objCommand.Connection = connessioneDb.Connessione
                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_PURCHASE", 0)
                objCommand.Parameters.AddWithValue("@ID_ITEM", DropListCatdesc.SelectedValue)
                objCommand.Parameters.AddWithValue("@ID_ITEM_CATEGORY", DropListPeripheralDescription.SelectedValue)
                objCommand.Parameters.AddWithValue("@ITEM_DESCRIPTION", DropListCatdesc.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@QUANTITY", txtQuantity.Text)
                objCommand.Parameters.AddWithValue("@DATE_PURCHASED", txtDatePurchased.Text)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_ITEM_PURCHASE_LOG", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                'hdIDRequsition.Value = CInt(objCommand.Parameters("@ID_REQUISITION").Value)

                If strError <> "" Then
                    Response.AppendHeader("Refresh", "1")
                    msg1.ForeColor = System.Drawing.Color.Red
                    msg1.Visible = True
                    msg1.Text = "ERROR!!!"
                ElseIf strOp = "I" Then
                    lblMessage.ForeColor = System.Drawing.Color.Green
                    lblMessage.Text = "Stocked In Successfully!"
                    Response.AppendHeader("Refresh", "2;url=ViewPurchase.aspx")


                Else
                    lblMessage.ForeColor = System.Drawing.Color.Green
                    lblMessage.Text = "Stock Updated Successfully!"
                    Response.AppendHeader("Refresh", "2;url=ViewPurchase.aspx")
                End If

            End If

        End If

        connessioneDb.ChiudiDb()


    End Sub

    Private Sub Popola_DropList_Description_Of_Item()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strSQL As String

        strSQL = "SELECT '' DESCRIPTION,0 ID_ITEMS UNION"
        strSQL = strSQL & " SELECT (MODEL +' - '+ DESCRIPTION) DESCRIPTION"
        strSQL = strSQL & ",ID_ITEMS"
        strSQL = strSQL & " FROM  vs_ItemsAvailable WITH(NOLOCK)"
        strSQL = strSQL & " WHERE ID_ITEM_CATEGORY =" + "'" + DropListPeripheralDescription.SelectedItem.Value + "'" + ""

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()
        DropListCatdesc.DataSource = objDataReader
        DropListCatdesc.DataValueField = "ID_ITEMS"
        DropListCatdesc.DataTextField = "DESCRIPTION"
        DropListCatdesc.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

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

    Protected Sub DropListItemDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        Call Popola_DropList_Description_Of_Item()
    End Sub


End Class