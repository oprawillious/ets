
Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General

Public Class ViewStocks
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack Then
                Call sb_view_Stocks()
                If Not IsInRole(Session("R"), Roll_Kind.Administrator) Then
                End If

            End If
        End If

    End Sub

    Private Sub sb_view_Stocks()
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_ITEMS"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + ", MANUFACTURER"
        strSQL = strSQL + ",PROCESSOR"
        strSQL = strSQL + ",SCREEN_SIZE"
        strSQL = strSQL + ",ID_ITEM_CATEGORY"
        strSQL = strSQL + ",QUANTITY_REMAINING"
        strSQL = strSQL + ", SERIAL_NO"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),LAST_DATE_PURCHASED, 109) LAST_DATE_PURCHASED"
        strSQL = strSQL + "  FROM vs_itemsavailable WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"

        'If txtCategory.Text <> "" Then
        '    strSQL = strSQL + " AND ID_ITEM_CATEGORY =" + "'" + txtCategory.Text + "'" + ""
        'End If

        If txtdescription.Text <> "" Then
            strSQL = strSQL + " AND DESCRIPTION LIKE '%" + txtdescription.Text + "%'"
        End If

        If txtRequestDate.Text <> "" Then
            strSQL = strSQL + " AND CONVERT(NVARCHAR(12),LAST_DATE_PURCHASED,109) =" & "CONVERT(NVARCHAR(12)," & "CAST('" & txtRequestDate.Text & "' AS DATETIME)" & ",109)"
        End If

        strSQL = strSQL + " ORDER BY ID_ITEMS DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvViewStocks.DataSource = myDataSet
        gvViewStocks.DataBind()
        dbConnect.ChiudiDb()

    End Sub


    Protected Sub gvViewStocks_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViewStocks.RowCommand
        If e.CommandName <> "Page" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvViewStocks.Rows(Index)
            Dim strId_items As String = TryCast(row.FindControl("hdItems"), HiddenField).Value

            If e.CommandName = "Delete" Then
                Call sb_InsertUpdateDeleteStock("D", strId_items)
                Response.Redirect("ViewItem.aspx")
            End If

        End If


    End Sub


    Private Sub sb_InsertUpdateDeleteStock(strOp As String, strditems As String)

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
                objCommand.Parameters.AddWithValue("@ID_ITEM_CAT", "")
                objCommand.Parameters.AddWithValue("@ID_ITEMS", strditems)
                objCommand.Parameters.AddWithValue("@DESCRIPTION", "")
                objCommand.Parameters.AddWithValue("@FLAG_AVAILABLE", "")
                objCommand.Parameters.AddWithValue("@MANUFACTURER", "")
                objCommand.Parameters.AddWithValue("@MODEL", "")
                objCommand.Parameters.AddWithValue("@MODEL_NO", "")
                objCommand.Parameters.AddWithValue("@SERIALNO", "")
                objCommand.Parameters.AddWithValue("@PROCESSOR", "")
                objCommand.Parameters.AddWithValue("@SCREENSIZE", "")
                objCommand.Parameters.AddWithValue("@RAM", "")
                objCommand.Parameters.AddWithValue("@STORAGE", "")


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

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnSearchItem_Click()
        Call sb_view_Stocks()
    End Sub

    Protected Sub btnAddItem_Click()
        Response.Redirect("AddNewItem.aspx")
    End Sub



End Class