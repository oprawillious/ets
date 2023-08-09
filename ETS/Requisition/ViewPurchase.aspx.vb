Imports System.Data.SqlClient

Public Class ViewPurchase
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack Then
                Call sb_view_purchase()
            End If
        End If

    End Sub

    Private Sub sb_view_purchase()
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_ITEMS"
        strSQL = strSQL + ", ID_ITEM_PURCHASE_LOG"
        strSQL = strSQL + ", QUANTITY"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", ITEM"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_PURCHASED, 109) DATE_PURCHASED"
        strSQL = strSQL + ", ID_ITEM_CATEGORY"
        strSQL = strSQL + "  FROM vs_Item_Purchased WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"

        'If txtCategory.Text <> "" Then
        '    strSQL = strSQL + " AND ID_ITEM_CATEGORY =" + "'" + txtCategory.Text + "'" + ""
        'End If


        If txtdescription.Text <> "" Then
            strSQL = strSQL + " AND DESCRIPTION LIKE '%" + txtdescription.Text + "%'"
        End If

        If txtRequestDate.Text <> "" Then
            strSQL = strSQL + " AND CONVERT(NVARCHAR(12),DATE_PURCHASED,109) =" & "CONVERT(NVARCHAR(12)," & "CAST('" & txtRequestDate.Text & "' AS DATETIME)" & ",109)"
        End If

        strSQL = strSQL + " ORDER BY ID_ITEM_PURCHASE_LOG DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvViewPurchase.DataSource = myDataSet
        gvViewPurchase.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub btnSearchItem_Click()
        Call sb_view_purchase()
    End Sub

    Protected Sub btnAddPurchase_Click()
        Response.Redirect("NewPurchase.aspx")
    End Sub

    Protected Sub gvViewPurchase_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViewPurchase.RowCommand

        If e.CommandName <> "Page" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvViewPurchase.Rows(Index)
            Dim strId_items As String = TryCast(row.FindControl("hdItems"), HiddenField).Value
            Dim strIdPurchased As String = TryCast(row.FindControl("hdItemPurchase"), HiddenField).Value
            Dim strCat As String = TryCast(row.FindControl("hdIdCategory"), HiddenField).Value
            Dim strquantity As String = TryCast(row.FindControl("hdQuantity"), HiddenField).Value

            If e.CommandName = "Delete" Then
                Call sb_InsertUpdateDeletePurchasedItem("D", strIdPurchased, strquantity, strId_items)
                Response.Redirect("ViewPurchase.aspx")
            End If

        End If

    End Sub
    Private Sub sb_InsertUpdateDeletePurchasedItem(strOp As String, strid_purchase As String, strquantity As String, strId_items As String)
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
                objCommand.Parameters.AddWithValue("@ID_PURCHASE", strid_purchase)
                objCommand.Parameters.AddWithValue("@ITEM_DESCRIPTION", "")
                objCommand.Parameters.AddWithValue("@ID_ITEM", strId_items)
                objCommand.Parameters.AddWithValue("@ID_ITEM_CATEGORY", "")
                objCommand.Parameters.AddWithValue("@QUANTITY", strquantity)
                objCommand.Parameters.AddWithValue("@DATE_PURCHASED", "")

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

            End If

        End If
        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub gvViewPurchase_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvViewPurchase.PageIndexChanging
        gvViewPurchase.PageIndex = e.NewPageIndex()
        Call sb_view_purchase()
    End Sub

End Class